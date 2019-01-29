using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Easy_DnD_Character_Creator.WizardComponents;

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
        private AppearanceControl appearanceComponent;

        public frmMainWindow(WizardManager inputWizardManager)
        {
            WM = inputWizardManager;
            introComponent = new IntroControl(WM);
            raceComponent = new RaceControl(WM);
            alignmentComponent = new AlignmentControl(WM);
            raceComponent.SubraceChanged += new EventHandler(raceComponent_SubraceChanged);
            ageComponent = new AgeControl(WM);
            bodyComponent = new BodyControl(WM);
            appearanceComponent = new AppearanceControl(WM);
            appearanceComponent.AppearanceChanged += new EventHandler(appearanceComponent_AppearanceChanged);

            InitializeComponent();
            refreshWindow();
            refreshButtons();
        }

        private void refreshWindow()
        {
            //fill in header and description
            headerLabel.Text = WM.getCurrentPageHeader();
            descriptionLabel.Text = WM.getCurrentPageDescription();

            //fill content
            contentFlowPanel.Controls.Clear();
            switch (WM.CurrentState)
            {
                case WizardState.race:
                    contentFlowPanel.Controls.Add(raceComponent);
                    raceComponent.populateForm();

                    contentFlowPanel.Controls.Add(alignmentComponent);
                    alignmentComponent.populateForm();
                    break;
                case WizardState.appearance:
                    contentFlowPanel.Controls.Add(ageComponent);
                    ageComponent.populateForm();

                    contentFlowPanel.Controls.Add(bodyComponent);
                    bodyComponent.populateForm();

                    contentFlowPanel.Controls.Add(appearanceComponent);
                    appearanceComponent.populateForm();
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
                    contentFlowPanel.Controls.Add(introComponent);
                    introComponent.populateForm();
                    break;
            }
        }

        private void refreshButtons()
        {
            //check back button
            if (WM.FirstPage)
            {
                backButton.Enabled = false;
            }
            else
            {
                backButton.Enabled = true;
            }

            //check if last page reached or page invalid
            if (WM.LastPage)
            {
                nextButton.Enabled = false;
            }
            else
            {
                switch (WM.CurrentState)
                {
                    //case WizardState.race:
                    //    raceComponent.isValid();
                    //    alignmentComponent.isValid();
                    //    break;
                    case WizardState.appearance:
                        //ageComponent.isValid();
                        //bodyComponent.isValid();
                        nextButton.Enabled = appearanceComponent.isValid();
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
                        //introComponent.isValid();
                        nextButton.Enabled = true;
                        break;
                }
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
                    appearanceComponent.saveContent();
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

            //refresh panel and buttons
            refreshWindow();
            refreshButtons();
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

        void appearanceComponent_AppearanceChanged(object sender, EventArgs e)
        {
            AppearanceControl incoming = sender as AppearanceControl;
            if (incoming != null)
            {
                refreshButtons();
            }
        }
    }
}
