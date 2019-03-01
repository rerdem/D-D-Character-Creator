using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public partial class ExtraRaceChoiceControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        public ExtraRaceChoiceControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            visited = false;
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
            return "select an option";
        }

        public bool isValid()
        {
            return (choiceList.SelectedItems.Count > 0);
        }

        public void populateForm()
        {
            //will need refactoring, when implementing other races with different choices
            introLabel.Text = wm.DBManager.getExtraRaceChoiceIntroText(wm.Choices.Subrace);

            choiceList.BeginUpdate();
            choiceList.Items.Clear();
            List<string> choices = wm.DBManager.getExtraRaceCantripChoiceOptions(wm.Choices.Subrace);
            choices.RemoveAll(option => wm.Choices.Spells.Contains(option));
            choiceList.Items.AddRange(choices.ToArray());
            choiceList.EndUpdate();

            if (choiceList.Items.Contains(wm.Choices.extraRaceChoice))
            {
                choiceList.SetSelected(choiceList.Items.IndexOf(wm.Choices.extraRaceChoice), true);
            }
            else
            {
                if (choiceList.Items.Count > 0)
                {
                    choiceList.SetSelected(0, true);
                }
            }

            visited = true;
        }

        public void saveContent()
        {
            if (choiceList.Items.Count > 0)
            {
                wm.Choices.extraRaceChoice = choiceList.SelectedItem.ToString();
            }
            else
            {
                wm.Choices.extraRaceChoice = "";
            }
        }

        private void choiceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //will need refactoring, when implementing other races with different choices
            if (wm.DBManager.hasExtraRaceCantripChoice(wm.Choices.Subrace))
            {
                descriptionLabel.Text = SpellFormatter.formatSpellDescription(wm.DBManager.SpellData.getSpell(choiceList.SelectedItem.ToString()));
            }
        }
    }
}
