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

namespace Easy_DnD_Character_Creator.WizardComponents
{
    //this entire UserControl will need refactoring, if there is another race that has additional choices
    public partial class ExtraRaceChoiceControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private List<Spell> spellOptions;

        public event EventHandler ExtraRaceChoiceChanged;

        public ExtraRaceChoiceControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            spellOptions = new List<Spell>();

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

            if (choiceList.SelectedItems.Count <= 0)
            {
                output = "select an option";
            }

            return output;
        }

        public bool isValid()
        {
            return (choiceList.SelectedItems.Count > 0);
        }

        public void populateForm()
        {
            //set intro text
            introLabel.Text = wm.DBManager.ExtraRaceChoiceData.getExtraRaceChoiceIntroText(wm.Choices.Subrace);

            //fill choice lsit
            choiceList.BeginUpdate();
            spellOptions = wm.DBManager.ExtraRaceChoiceData.getExtraRaceCantripChoiceOptions(wm.Choices.Subrace).Except(wm.Choices.Spells).ToList();
            choiceList.DataSource = null;
            choiceList.DataSource = spellOptions;
            choiceList.DisplayMember = "Name";
            choiceList.EndUpdate();

            //if there was a previous choice, load it
            if (wm.Choices.extraRaceChoices.Count > 0)
            {
                List<Spell> choiceToLoad = wm.Choices.extraRaceChoices.OfType<Spell>().ToList();
                for (int i = 0; i < choiceList.Items.Count; i++)
                {
                    if (choiceToLoad.Contains(choiceList.Items[i]))
                    {
                        choiceList.SetSelected(i, true);
                    }
                }
            }
            else
            {
                if (choiceList.Items.Count > 0)
                {
                    choiceList.SetSelected(0, true);
                }
            }

            Visited = true;
        }

        public void saveContent()
        {
            if (choiceList.SelectedItems.Count > 0)
            {
                wm.Choices.extraRaceChoices.Clear();

                //needs refactor, once more than High Elves have additional choices
                foreach (Spell spell in choiceList.SelectedItems)
                {
                    if (spell != null)
                    {
                        wm.Choices.extraRaceChoices.Add(spell);
                    }
                }
            }
            else
            {
                wm.Choices.extraRaceChoices.Clear();
            }
        }

        private void choiceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (choiceList.SelectedItems.Count > 0)
            {
                //will need refactoring, when implementing other races with different choices
                if ((Spell)choiceList.SelectedItem != null)
                {
                    descriptionLabel.Text = SpellFormatter.formatSpellDescription((Spell)choiceList.SelectedItem);

                    OnExtraRaceChoiceChanged(null);
                }
            }
        }

        protected virtual void OnExtraRaceChoiceChanged(EventArgs e)
        {
            EventHandler handler = ExtraRaceChoiceChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
