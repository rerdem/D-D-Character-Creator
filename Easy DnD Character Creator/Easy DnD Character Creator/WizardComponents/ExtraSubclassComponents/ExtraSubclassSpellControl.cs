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
    public partial class ExtraSubclassSpellControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private int spellAmount;
        private List<Spell> spellSourceList;
        private List<int> spellOrderedSelection;

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

        public event EventHandler SpellChosen;

        public ExtraSubclassSpellControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            spellAmount = 0;
            spellSourceList = new List<Spell>();
            spellOrderedSelection = new List<int>();

            InitializeComponent();
        }

        public void populateForm()
        {
            spellAmount = wm.DBManager.ExtraSubclassChoiceData.ExtraSubclassSpellData.extraSpellChoiceAmount(wm.Choices.Subclass);

            //set intro text
            introLabel.Text = $"Please choose {spellAmount} spell(s) below:";

            //fill choice list
            spellSourceList = wm.DBManager.ExtraSubclassChoiceData.ExtraSubclassSpellData.getExtraSubclassCantripOptions(wm.Choices.Subclass).Except(wm.Choices.Spells).ToList();
            spellListBox.BeginUpdate();
            spellListBox.DataSource = null;
            spellListBox.DataSource = spellSourceList;
            spellListBox.DisplayMember = "Name";
            spellListBox.EndUpdate();

            //if there was a previous choice, load it
            if (wm.Choices.SubclassSpells.Count > 0)
            {
                for (int i = 0; i < spellListBox.Items.Count; i++)
                {
                    if (wm.Choices.SubclassSpells.Contains(spellListBox.Items[i]))
                    {
                        spellListBox.SetSelected(i, true);
                    }
                }
            }
            else
            {
                if (spellListBox.Items.Count > 0)
                {
                    spellListBox.SetSelected(0, true);
                }
            }

            Visited = true;
        }

        public void saveContent()
        {
            if (spellListBox.SelectedItems.Count > 0)
            {
                wm.Choices.SubclassSpells.Clear();

                foreach (Spell spell in spellListBox.SelectedItems)
                {
                    wm.Choices.SubclassSpells.Add(spell);
                }
            }
            else
            {
                wm.Choices.SubclassSpells.Clear();
            }
        }

        public bool isValid()
        {
            return (spellListBox.SelectedItems.Count == spellAmount);
        }

        public string getInvalidElements()
        {
            string output = "";

            if (spellListBox.SelectedItems.Count < spellAmount)
            {
                output = $"select {spellAmount - spellListBox.SelectedItems.Count} more spell(s)";
            }

            return output;
        }

        private void syncSpellSelectionOrder()
        {
            //add new selected items
            foreach (int index in spellListBox.SelectedIndices)
            {
                if (!spellOrderedSelection.Contains(index))
                {
                    spellOrderedSelection.Add(index);
                }
            }

            //remove deselected items
            spellOrderedSelection.RemoveAll(index => !spellListBox.SelectedIndices.Contains(index));
        }

        private void spellListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            syncSpellSelectionOrder();
            if (spellListBox.SelectedItems.Count > 0)
            {
                descriptionLabel.Text = SpellFormatter.formatSpellDescription((Spell)spellListBox.SelectedItem);

                OnSpellChosen(null);
            }
        }

        protected virtual void OnSpellChosen(EventArgs e)
        {
            EventHandler handler = SpellChosen;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
