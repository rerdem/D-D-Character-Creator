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

namespace Easy_DnD_Character_Creator.WizardComponents.ExtraRaceComponents
{
    public partial class ExtraRaceSpellControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private List<Spell> spellSourceList;
        
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

        public ExtraRaceSpellControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            spellSourceList = new List<Spell>();

            InitializeComponent();
        }

        public void populateForm()
        {
            resetContent();

            //if there was a previous choice, load it
            if (Visited && (wm.Choices.RaceSpells.Count > 0))
            {
                for (int i = 0; i < spellListBox.Items.Count; i++)
                {
                    if (wm.Choices.RaceSpells.Contains(spellListBox.Items[i]))
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

        private void resetContent()
        {
            //fill choice list
            spellSourceList = wm.DBManager.ExtraRaceChoiceData.getExtraRaceCantripChoiceOptions(wm.Choices.RaceChoice.getSelectedSubrace().Name);
            foreach (Spell spell in wm.Choices.Spells)
            {
                spellSourceList.Remove(spell);
            }

            spellListBox.BeginUpdate();
            spellListBox.DataSource = null;
            spellListBox.DataSource = spellSourceList;
            spellListBox.DisplayMember = "Name";
            spellListBox.EndUpdate();
        }

        public void saveContent()
        {
            if (spellListBox.SelectedItems.Count > 0)
            {
                wm.Choices.RaceSpells.Clear();

                foreach (Spell spell in spellListBox.SelectedItems)
                {
                    wm.Choices.RaceSpells.Add(spell);
                }
            }
            else
            {
                wm.Choices.RaceSpells.Clear();
            }
        }

        public bool isValid()
        {
            return (spellListBox.SelectedItems.Count > 0);
        }

        public string getInvalidElements()
        {
            string output = "";

            if (spellListBox.SelectedItems.Count == 0)
            {
                output = "select a spell you know";
            }

            return output;
        }

        private void spellListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
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
