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
        private IntroControl introComponent;
        private RaceControl raceComponent;
        private AlignmentControl alignmentComponent;

        public frmMainWindow(WizardManager inputWizardManager)
        {
            WM = inputWizardManager;
            introComponent = new IntroControl(WM);
            raceComponent = new RaceControl(WM);
            alignmentComponent = new AlignmentControl(WM);
            raceComponent.SubraceChanged += new EventHandler(raceComponent_SubraceChanged);

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
                    headerLabel.Text = "Race && Alignment";
                    descriptionLabel.Text = "Please select the race, subrace and alignment of your character.";

                    contentFlowPanel.Controls.Add(raceComponent);
                    raceComponent.populateForm();

                    contentFlowPanel.Controls.Add(alignmentComponent);
                    alignmentComponent.populateForm();
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
                    headerLabel.Text = "Introduction";
                    descriptionLabel.Text = "Please select the used books, creation preset and character level.";

                    contentFlowPanel.Controls.Add(introComponent);
                    introComponent.populateForm();
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
                    introComponent.saveContent();
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

        void raceComponent_SubraceChanged(object sender, EventArgs e)
        {
            RaceControl incoming = sender as RaceControl;
            if (incoming != null)
            {
                alignmentComponent.updateRaceAlignmentDescription(WM.DBManager.getSubraceAlignmentDescription(WM.Choices.Subrace));
            }
        }
    }
}
