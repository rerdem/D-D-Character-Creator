using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Easy_DnD_Character_Creator.DataTypes;

namespace Easy_DnD_Character_Creator.WizardComponents.ExtraClassComponents
{
    public partial class MetamagicControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private int lastLevel;

        List<int> metamagicOrderedSelection;
        List<Metamagic> metamagicSourceList;
        int metamagicKnown;

        public event EventHandler MetamagicChosen;

        public MetamagicControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            lastLevel = 0;

            metamagicOrderedSelection = new List<int>();
            metamagicSourceList = new List<Metamagic>();
            metamagicKnown = 0;

            InitializeComponent();
        }

        public bool Visited
        {
            get
            {
                return visited;
            }
            set
            {
                visited = value;
            }
        }

        public string getInvalidElements()
        {
            string output = "";

            if (metamagicListBox.SelectedItems.Count <= metamagicKnown)
            {
                output = $"select {metamagicKnown - metamagicListBox.SelectedItems.Count} more metamagics";
            }

            return output;
        }

        public bool isValid()
        {
            return (metamagicListBox.SelectedItems.Count == metamagicKnown);
        }

        public void populateForm()
        {
            metamagicKnown = wm.DBManager.ExtraClassChoiceData.MetamagicData.getMetamagicAmount(wm.Choices.Class, wm.Choices.Level);
            metamagicIntroLabel.Text = $"Please choose {metamagicKnown} metamagics from the list below:";

            refreshMetamagicList();

            if (Visited && !hasLevelChanged())
            {
                loadPreviousSelection();
            }

            lastLevel = wm.Choices.Level;
            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.SorcererMetamagic.Clear();
            foreach (Metamagic magic in metamagicListBox.SelectedItems)
            {
                if (magic != null)
                {
                    wm.Choices.SorcererMetamagic.Add(magic);
                }
            }
        }

        private void loadPreviousSelection()
        {
            for (int i = 0; i < metamagicSourceList.Count; i++)
            {
                if (wm.Choices.SorcererMetamagic.Contains(metamagicSourceList[i]))
                {
                    metamagicListBox.SetSelected(i, true);
                }
                else
                {
                    metamagicListBox.SetSelected(i, false);
                }
            }
        }

        private void refreshMetamagicList()
        {
            metamagicSourceList = wm.DBManager.ExtraClassChoiceData.MetamagicData.getMetamagicOptions();

            metamagicListBox.BeginUpdate();
            metamagicListBox.DataSource = null;
            metamagicListBox.DataSource = metamagicSourceList;
            metamagicListBox.DisplayMember = "Name";
            metamagicListBox.EndUpdate();

            if (metamagicKnown > 1)
            {
                metamagicListBox.SelectionMode = SelectionMode.MultiSimple;
            }
            else
            {
                metamagicListBox.SelectionMode = SelectionMode.One;
            }
        }

        private void syncMetamagicSelectionOrder()
        {
            //add new selected items
            foreach (int index in metamagicListBox.SelectedIndices)
            {
                if (!metamagicOrderedSelection.Contains(index))
                {
                    metamagicOrderedSelection.Add(index);
                }
            }

            //remove deselected items
            metamagicOrderedSelection.RemoveAll(index => !metamagicListBox.SelectedIndices.Contains(index));
        }

        private bool hasLevelChanged()
        {
            return lastLevel != wm.Choices.Level;
        }

        private void metamagicListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            syncMetamagicSelectionOrder();
            if (metamagicListBox.SelectedItems.Count > 0)
            {
                if (metamagicListBox.SelectedIndices.Count <= metamagicKnown)
                {
                    int lastSelectedIndex = metamagicOrderedSelection.ElementAt(metamagicOrderedSelection.Count - 1);
                    Metamagic currentSelectedItem = (Metamagic)metamagicListBox.Items[lastSelectedIndex];
                    if (currentSelectedItem != null)
                    {
                        metamagicDescriptionLabel.Text = currentSelectedItem.Name + 
                                                         Environment.NewLine + 
                                                         currentSelectedItem.Description;
                    }
                }
                else
                {
                    int lastSelectedIndex = metamagicOrderedSelection.ElementAt(metamagicOrderedSelection.Count - 1);
                    metamagicListBox.SelectedIndices.Remove(lastSelectedIndex);
                }

                OnMetamagicChosen(null);
            }
        }

        protected virtual void OnMetamagicChosen(EventArgs e)
        {
            EventHandler handler = MetamagicChosen;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
