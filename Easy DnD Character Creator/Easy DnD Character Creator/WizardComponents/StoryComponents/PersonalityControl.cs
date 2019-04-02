using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_DnD_Character_Creator.WizardComponents.StoryComponents
{
    public partial class PersonalityControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private string lastBackground;

        private PersonalityComponent currentComponent;
        private List<string> choices;
        private int currentlyDisplayedIndex;

        public PersonalityControl(WizardManager inputWizardManager, PersonalityComponent inputComponent)
        {
            wm = inputWizardManager;
            Visited = false;

            lastBackground = "";

            currentComponent = inputComponent;
            choices = new List<string>();
            currentlyDisplayedIndex = 0;

            InitializeComponent();
            initializeContent();
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
            return "";
        }

        public bool isValid()
        {
            //selection of traits, ideals, bonds and flaws is optional
            return true;
        }

        public void populateForm()
        {
            resetContent();

            if (Visited && !hasBackgroundChanged())
            {
                loadPreviousSelection();
            }

            lastBackground = wm.Choices.Background;
            Visited = true;
        }

        public void saveContent()
        {
            switch (currentComponent)
            {
                case PersonalityComponent.ideal:
                    wm.Choices.Ideal = choiceTextBox.Text;
                    wm.Choices.CustomIdeal= customCheckBox.Checked;
                    break;
                case PersonalityComponent.bond:
                    wm.Choices.Bond = choiceTextBox.Text;
                    wm.Choices.CustomBond = customCheckBox.Checked;
                    break;
                case PersonalityComponent.flaw:
                    wm.Choices.Flaw = choiceTextBox.Text;
                    wm.Choices.CustomFlaw = customCheckBox.Checked;
                    break;
                default://trait
                    wm.Choices.Trait = choiceTextBox.Text;
                    wm.Choices.CustomTrait = customCheckBox.Checked;
                    break;
            }
        }

        private void resetContent()
        {
            switch (currentComponent)
            {
                case PersonalityComponent.ideal:
                    choices = wm.DBManager.StoryData.getIdeals(wm.Choices.Background);
                    break;
                case PersonalityComponent.bond:
                    choices = wm.DBManager.StoryData.getBonds(wm.Choices.Background);
                    break;
                case PersonalityComponent.flaw:
                    choices = wm.DBManager.StoryData.getFlaws(wm.Choices.Background);
                    break;
                default://trait
                    choices = wm.DBManager.StoryData.getTraits(wm.Choices.Background);
                    break;
            }
            customCheckBox.Checked = false;
            currentlyDisplayedIndex = wm.getRandomNumber(0, choices.Count);
            refreshTextBoxContent();
        }

        private void loadPreviousSelection()
        {
            switch (currentComponent)
            {
                case PersonalityComponent.ideal:
                    if (wm.Choices.CustomIdeal)
                    {
                        customCheckBox.Checked = true;
                        choiceTextBox.Text = wm.Choices.Ideal;
                    }
                    else
                    {
                        currentlyDisplayedIndex = choices.IndexOf(wm.Choices.Ideal);
                        refreshTextBoxContent();
                    }
                    break;
                case PersonalityComponent.bond:
                    if (wm.Choices.CustomBond)
                    {
                        customCheckBox.Checked = true;
                        choiceTextBox.Text = wm.Choices.Bond;
                    }
                    else
                    {
                        currentlyDisplayedIndex = choices.IndexOf(wm.Choices.Bond);
                        refreshTextBoxContent();
                    }
                    break;
                case PersonalityComponent.flaw:
                    if (wm.Choices.CustomFlaw)
                    {
                        customCheckBox.Checked = true;
                        choiceTextBox.Text = wm.Choices.Flaw;
                    }
                    else
                    {
                        currentlyDisplayedIndex = choices.IndexOf(wm.Choices.Flaw);
                        refreshTextBoxContent();
                    }
                    break;
                default://trait
                    if (wm.Choices.CustomTrait)
                    {
                        customCheckBox.Checked = true;
                        choiceTextBox.Text = wm.Choices.Trait;
                    }
                    else
                    {
                        currentlyDisplayedIndex = choices.IndexOf(wm.Choices.Trait);
                        refreshTextBoxContent();
                    }
                    break;
            }
        }

        private void refreshTextBoxContent()
        {
            choiceTextBox.Text = choices[currentlyDisplayedIndex];
        }

        private bool hasBackgroundChanged()
        {
            return (lastBackground != wm.Choices.Background);
        }

        private void initializeContent()
        {
            upButton.Text = char.ConvertFromUtf32(0x25B2);
            downButton.Text = char.ConvertFromUtf32(0x25BC); ;

            switch (currentComponent)
            {
                case PersonalityComponent.ideal:
                    groupBox.Text = "Ideals";
                    break;
                case PersonalityComponent.bond:
                    groupBox.Text = "Bonds";
                    break;
                case PersonalityComponent.flaw:
                    groupBox.Text = "Flaws";
                    break;
                default://trait
                    groupBox.Text = "Personality Traits";
                    break;
            }
        }

        private void customCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (customCheckBox.Checked)
            {
                upButton.Enabled = false;
                downButton.Enabled = false;
                choiceTextBox.Enabled = true;
            }
            else
            {
                upButton.Enabled = true;
                downButton.Enabled = true;
                choiceTextBox.Enabled = false;
                refreshTextBoxContent();
            }
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            currentlyDisplayedIndex++;
            if (currentlyDisplayedIndex >= choices.Count)
            {
                currentlyDisplayedIndex = 0;
            }
            refreshTextBoxContent();
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            currentlyDisplayedIndex--;
            if (currentlyDisplayedIndex < 0)
            {
                currentlyDisplayedIndex = choices.Count - 1;
            }
            refreshTextBoxContent();
        }

        private void randomButton_Click(object sender, EventArgs e)
        {
            customCheckBox.Checked = false;
            currentlyDisplayedIndex = wm.getRandomNumber(0, choices.Count);
            refreshTextBoxContent();
        }
    }
}
