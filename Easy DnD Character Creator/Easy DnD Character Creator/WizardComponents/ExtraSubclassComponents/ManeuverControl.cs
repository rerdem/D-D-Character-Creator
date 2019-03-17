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

namespace Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents
{
    public partial class ManeuverControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private int maneuverAmount;
        private List<Maneuver> maneuverSourceList;
        private List<int> maneuverOrderedSelection;

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

        public event EventHandler ManeuverChosen;

        public ManeuverControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            maneuverAmount = 0;
            maneuverSourceList = new List<Maneuver>();
            maneuverOrderedSelection = new List<int>();

            InitializeComponent();
        }

        public void populateForm()
        {
            maneuverAmount = wm.DBManager.ExtraSubclassChoiceData.ManeuverData.getManeuverAmount(wm.Choices.Subclass, wm.Choices.Level);

            resetContent();

            //if there was a previous choice, load it
            if (wm.Choices.Maneuvers.Count > 0)
            {
                for (int i = 0; i < maneuverListBox.Items.Count; i++)
                {
                    if (wm.Choices.Maneuvers.Contains(maneuverListBox.Items[i]))
                    {
                        maneuverListBox.SetSelected(i, true);
                    }
                }
            }
            else
            {
                if (maneuverListBox.Items.Count > 0)
                {
                    maneuverListBox.SetSelected(0, true);
                }
            }

            Visited = true;
        }

        private void resetContent()
        {
            //set intro text
            introLabel.Text = $"Please choose {maneuverAmount} maneuver(s) below:";

            //fill choice list
            maneuverSourceList = wm.DBManager.ExtraSubclassChoiceData.ManeuverData.getManeuvers();
            maneuverListBox.BeginUpdate();
            maneuverListBox.DataSource = null;
            maneuverListBox.DataSource = maneuverSourceList;
            maneuverListBox.DisplayMember = "Name";
            maneuverListBox.EndUpdate();

            //set selection mode
            if (maneuverAmount > 1)
            {
                maneuverListBox.SelectionMode = SelectionMode.MultiSimple;
            }
            else
            {
                maneuverListBox.SelectionMode = SelectionMode.One;
            }
        }

        public void saveContent()
        {
            if (maneuverListBox.SelectedItems.Count > 0)
            {
                wm.Choices.Maneuvers.Clear();

                foreach (Maneuver maneuver in maneuverListBox.SelectedItems)
                {
                    wm.Choices.Maneuvers.Add(maneuver);
                }
            }
            else
            {
                wm.Choices.Maneuvers.Clear();
            }
        }

        public bool isValid()
        {
            return (maneuverListBox.SelectedItems.Count == maneuverAmount);
        }

        public string getInvalidElements()
        {
            string output = "";

            if (maneuverListBox.SelectedItems.Count < maneuverAmount)
            {
                output = $"select {maneuverAmount - maneuverListBox.SelectedItems.Count} more maneuver(s)";
            }

            return output;
        }

        private void syncManeuverSelectionOrder()
        {
            //add new selected items
            foreach (int index in maneuverListBox.SelectedIndices)
            {
                if (!maneuverOrderedSelection.Contains(index))
                {
                    maneuverOrderedSelection.Add(index);
                }
            }

            //remove deselected items
            maneuverOrderedSelection.RemoveAll(index => !maneuverListBox.SelectedIndices.Contains(index));
        }

        private void maneuverListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            syncManeuverSelectionOrder();
            if (maneuverListBox.SelectedItems.Count > 0)
            {
                if (maneuverListBox.SelectedIndices.Count <= maneuverAmount)
                {
                    int lastSelectedIndex = maneuverOrderedSelection.ElementAt(maneuverOrderedSelection.Count - 1);
                    Maneuver currentManeuver = (Maneuver)maneuverListBox.Items[lastSelectedIndex];
                    descriptionLabel.Text = currentManeuver.Name + Environment.NewLine + currentManeuver.Description;
                }
                else
                {
                    int lastSelectedIndex = maneuverOrderedSelection.ElementAt(maneuverOrderedSelection.Count - 1);
                    maneuverListBox.SelectedIndices.Remove(lastSelectedIndex);
                }

                OnManeuverChosen(null);
            }
        }

        protected virtual void OnManeuverChosen(EventArgs e)
        {
            EventHandler handler = ManeuverChosen;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
