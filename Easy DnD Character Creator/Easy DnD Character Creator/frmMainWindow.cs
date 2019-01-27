using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_DnD_Character_Creator
{
    public partial class frmMainWindow : Form
    {
        public WizardManager WM { get; }
        private IntroControl introPage;
        private RaceControl racePage;

        public frmMainWindow(WizardManager inputWizardManager)
        {
            WM = inputWizardManager;
            introPage = new IntroControl(WM);
            racePage = new RaceControl(WM);
            InitializeComponent();
            refreshWindow();
        }

        private void refreshWindow()
        {
            //fill content
            contentFlowPanel.Controls.Clear();
            switch (WM.CurrentState)
            {
                case WizardState.race:
                    contentFlowPanel.Controls.Add(racePage);
                    headerLabel.Text = "Race && Alignment";
                    descriptionLabel.Text = "Please select the race, subrace and alignment of your character.";
                    racePage.populateForm();
                    racePage.Visited = true;
                    break;
                case WizardState.appearance:
                    break;
                case WizardState.classBackground:
                    break;
                case WizardState.stats:
                    break;
                case WizardState.languages:
                    break;
                case WizardState.skills:
                    break;
                case WizardState.equipment:
                    break;
                case WizardState.spells:
                    break;
                case WizardState.extraChoices:
                    break;
                case WizardState.story:
                    break;
                case WizardState.export:
                    break;
                default: //WizardState.intro
                    contentFlowPanel.Controls.Add(introPage);
                    headerLabel.Text = "Introduction";
                    descriptionLabel.Text = "Please select the used books, creation preset and character level.";
                    introPage.populateForm();
                    introPage.Visited = true;
                    break;
            }

            //check if back or next buttons need to be disabled
            if (WM.FirstPage)
            {
                backButton.Enabled = false;
            }
            else
            {
                backButton.Enabled = true;
            }

            if (WM.LastPage)
            {
                nextButton.Enabled = false;
            }
            else
            {
                nextButton.Enabled = true;
            }
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            switch (WM.CurrentState)
            {
                case WizardState.race:
                    break;
                case WizardState.appearance:
                    break;
                case WizardState.classBackground:
                    break;
                case WizardState.stats:
                    break;
                case WizardState.languages:
                    break;
                case WizardState.skills:
                    break;
                case WizardState.equipment:
                    break;
                case WizardState.spells:
                    break;
                case WizardState.extraChoices:
                    break;
                case WizardState.story:
                    break;
                case WizardState.export:
                    break;
                default: //WizardState.intro
                    introPage.saveContent();
                    WM.advanceState();
                    break;
            }

            refreshWindow();

            //save current page values

            //advance status in WizardManager

            //if page has been visited, fill in information that did not reset

            //refresh panel

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            //save current page values

            //revert status in WizardManager

            //fill in previous page

            //refresh panel
        }
    }
}
