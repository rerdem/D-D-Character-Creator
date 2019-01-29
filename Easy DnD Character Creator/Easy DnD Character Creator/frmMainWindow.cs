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
        private AgeControl ageComponent;
        private BodyControl bodyComponent;

        public frmMainWindow(WizardManager inputWizardManager)
        {
            WM = inputWizardManager;
            introComponent = new IntroControl(WM);
            raceComponent = new RaceControl(WM);
            alignmentComponent = new AlignmentControl(WM);
            raceComponent.SubraceChanged += new EventHandler(raceComponent_SubraceChanged);
            ageComponent = new AgeControl(WM);
            bodyComponent = new BodyControl(WM);

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
                    headerLabel.Text = "Physical Appearance";
                    descriptionLabel.Text = "Please select the physical characteristics of your character.";

                    contentFlowPanel.Controls.Add(ageComponent);
                    ageComponent.populateForm();

                    contentFlowPanel.Controls.Add(bodyComponent);
                    bodyComponent.populateForm();
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
            //save current page values
            switch (WM.CurrentState)
            {
                case WizardState.race:
                    raceComponent.saveContent();
                    alignmentComponent.saveContent();
                    break;
                case WizardState.appearance:
                    ageComponent.saveContent();
                    bodyComponent.saveContent();
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
                    break;
            }
            
            //advance status in WizardManager
            WM.advanceState();

            //refresh panel
            refreshWindow();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            //save current page values

            //revert status in WizardManager

            //refresh panel
        }

        void raceComponent_SubraceChanged(object sender, EventArgs e)
        {
            RaceControl incoming = sender as RaceControl;
            if (incoming != null)
            {
                alignmentComponent.updateRaceAlignmentDescription(WM.Choices.Subrace);
                ageComponent.updateRaceAgeDescription(WM.Choices.Subrace);
                bodyComponent.updateMinMax(WM.Choices.Subrace);
            }
        }
    }
}
