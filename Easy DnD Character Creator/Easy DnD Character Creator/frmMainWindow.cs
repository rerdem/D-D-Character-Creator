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
        private ClassControl classComponent;

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
            classComponent = new ClassControl(WM);


            InitializeComponent();
            refreshWindow();
        }

        private void refreshWindow()
        {
            refreshContentPanel();
            refreshButtons();
            refreshStatusText();
        }

        private void refreshContentPanel()
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
                    contentFlowPanel.Controls.Add(classComponent);
                    classComponent.populateForm();
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
                nextButton.Enabled = isCurrentPageValid();
            }
        }

        private void refreshStatusText()
        {
            if (!isCurrentPageValid())
            {
                missingElementsLabel.Text = "The following properties need to be filled out to continue: ";
                string missingElements = "";

                switch (WM.CurrentState)
                {
                    case WizardState.race:
                        missingElements = string.Join(", ", new string[] {
                            raceComponent.getInvalidElements(), 
                            alignmentComponent.getInvalidElements()
                                }.Where(s => !string.IsNullOrEmpty(s)));
                        break;
                    case WizardState.appearance:
                        missingElements = string.Join(", ", new string[] {
                            ageComponent.getInvalidElements(),
                            bodyComponent.getInvalidElements(),
                            appearanceComponent.getInvalidElements()
                                }.Where(s => !string.IsNullOrEmpty(s)));
                        break;
                    case WizardState.classBackground:
                        missingElements = string.Join(", ", new string[] {
                            classComponent.getInvalidElements(),
                            //backgroundComponent.getInvalidElements()
                                }.Where(s => !string.IsNullOrEmpty(s)));
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
                        missingElements = introComponent.getInvalidElements();
                        break;
                }

                missingElementsLabel.Text += missingElements;
            }
            else
            {
                missingElementsLabel.Text = "Page is correctly filled out.";
            }
        }

        private bool isCurrentPageValid()
        {
            bool isValid = false;

            switch (WM.CurrentState)
            {
                case WizardState.race:
                    isValid = raceComponent.isValid() && alignmentComponent.isValid();
                    break;
                case WizardState.appearance:
                    isValid = ageComponent.isValid() && bodyComponent.isValid() && appearanceComponent.isValid();
                    break;
                case WizardState.classBackground:
                    isValid = classComponent.isValid();
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
                    isValid = introComponent.isValid();
                    break;
            }

            return isValid;
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
                    classComponent.saveContent();
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
                refreshStatusText();
            }
        }
    }
}
