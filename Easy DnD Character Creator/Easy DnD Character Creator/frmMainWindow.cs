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
        private BackgroundControl backgroundComponent;
        private AbilityControl abilityComponent;
        private LanguageControl languageComponent;
        private SkillControl skillComponent;
        private EquipmentControl equipmentComponent;
        private SpellControl spellComponent;
        private ExtraRaceChoiceControl extraRaceChoiceComponent;
        private ExtraClassChoiceControl extraClassChoiceComponent;
        private ExtraSubclassChoiceControl extraSubclassChoiceComponent;
        private NameControl nameComponent;
        private StoryControl storyComponent;
        private ExportControl exportComponent;

        public frmMainWindow(WizardManager inputWizardManager)
        {
            WM = inputWizardManager;

            initializeUserControls();

            InitializeComponent();
            refreshWindow();
        }

        private void initializeUserControls()
        {
            introComponent = new IntroControl(WM);
            raceComponent = new RaceControl(WM);
            alignmentComponent = new AlignmentControl(WM);
            raceComponent.SubraceChanged += new EventHandler(raceComponent_SubraceChanged);
            ageComponent = new AgeControl(WM);
            bodyComponent = new BodyControl(WM);
            appearanceComponent = new AppearanceControl(WM);
            appearanceComponent.AppearanceChanged += new EventHandler(appearanceComponent_AppearanceChanged);
            classComponent = new ClassControl(WM);
            classComponent.ClassChanged += new EventHandler(classComponent_ClassChanged);
            classComponent.ClassChoiceChanged += new EventHandler(classComponent_ClassChoiceChanged);
            backgroundComponent = new BackgroundControl(WM);
            abilityComponent = new AbilityControl(WM);
            abilityComponent.AbilityAssigned += new EventHandler(abilityComponent_AbilityAssigned);
            abilityComponent.AbilityBonusAssigned += new EventHandler(abilityComponent_AbilityBonusAssigned);
            languageComponent = new LanguageControl(WM);
            languageComponent.LanguageSelectionChanged += new EventHandler(languageComponent_LanguageSelectionChanged);
            skillComponent = new SkillControl(WM);
            skillComponent.SkillChosen += new EventHandler(skillComponent_SkillChosen);
            equipmentComponent = new EquipmentControl(WM);
            equipmentComponent.EquipmentSelectionChanged += new EventHandler(equipmentComponent_EquipmentSelectionChanged);
            spellComponent = new SpellControl(WM);
            spellComponent.SpellChosen += new EventHandler(spellComponent_SpellChosen);
            extraRaceChoiceComponent = new ExtraRaceChoiceControl(WM);
            extraRaceChoiceComponent.ExtraRaceChoiceChanged += new EventHandler(extraRaceChoiceComponent_ExtraRaceChoiceChanged);
            extraClassChoiceComponent = new ExtraClassChoiceControl(WM);
            extraClassChoiceComponent.SubcontrolOptionChosen += new EventHandler(extraClassChoiceComponent_SubcontrolOptionChosen);
            extraSubclassChoiceComponent = new ExtraSubclassChoiceControl(WM);
            extraSubclassChoiceComponent.SubcontrolOptionChosen += new EventHandler(extraSubclassChoiceComponent_SubcontrolOptionChosen);
            nameComponent = new NameControl(WM);
            nameComponent.NameChanged += new EventHandler(nameComponent_NameChanged);
            storyComponent = new StoryControl(WM);
            storyComponent.SubcontrolOptionChosen += new EventHandler(storyComponent_SubcontrolOptionChosen);
            exportComponent = new ExportControl(WM);
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

                    contentFlowPanel.Controls.Add(backgroundComponent);
                    backgroundComponent.populateForm();
                    break;
                case WizardState.stats:
                    contentFlowPanel.Controls.Add(abilityComponent);
                    abilityComponent.populateForm();
                    break;
                case WizardState.languages:
                    contentFlowPanel.Controls.Add(languageComponent);
                    languageComponent.populateForm();
                    break;
                case WizardState.skillEquipment:
                    contentFlowPanel.Controls.Add(skillComponent);
                    skillComponent.populateForm();

                    contentFlowPanel.Controls.Add(equipmentComponent);
                    equipmentComponent.populateForm();
                    break;
                case WizardState.spells:
                    contentFlowPanel.Controls.Add(spellComponent);
                    spellComponent.populateForm();
                    break;
                case WizardState.extraRaceChoices:
                    contentFlowPanel.Controls.Add(extraRaceChoiceComponent);
                    extraRaceChoiceComponent.populateForm();
                    break;
                case WizardState.extraClassChoices:
                    contentFlowPanel.Controls.Add(extraClassChoiceComponent);
                    extraClassChoiceComponent.populateForm();
                    break;
                case WizardState.extraSubclassChoices:
                    contentFlowPanel.Controls.Add(extraSubclassChoiceComponent);
                    extraSubclassChoiceComponent.populateForm();
                    break;
                case WizardState.story:
                    contentFlowPanel.Controls.Add(nameComponent);
                    nameComponent.populateForm();
                    contentFlowPanel.Controls.Add(storyComponent);
                    storyComponent.populateForm();
                    break;
                case WizardState.export:
                    contentFlowPanel.Controls.Add(exportComponent);
                    exportComponent.populateForm();
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
                            backgroundComponent.getInvalidElements()
                                }.Where(s => !string.IsNullOrEmpty(s)));
                        break;
                    case WizardState.stats:
                        missingElements = abilityComponent.getInvalidElements();
                        break;
                    case WizardState.languages:
                        missingElements = languageComponent.getInvalidElements();
                        break;
                    case WizardState.skillEquipment:
                        missingElements = string.Join(", ", new string[] {
                            skillComponent.getInvalidElements(),
                            equipmentComponent.getInvalidElements()
                                }.Where(s => !string.IsNullOrEmpty(s)));
                        break;
                    case WizardState.spells:
                        missingElements = spellComponent.getInvalidElements();
                        break;
                    case WizardState.extraRaceChoices:
                        missingElements = extraRaceChoiceComponent.getInvalidElements();
                        break;
                    case WizardState.extraClassChoices:
                        missingElements = extraClassChoiceComponent.getInvalidElements();
                        break;
                    case WizardState.extraSubclassChoices:
                        missingElements = extraSubclassChoiceComponent.getInvalidElements();
                        break;
                    case WizardState.story:
                        missingElements = string.Join(", ", new string[] {
                            nameComponent.getInvalidElements(),
                            storyComponent.getInvalidElements()
                                }.Where(s => !string.IsNullOrEmpty(s)));
                        break;
                    case WizardState.export:
                        missingElements = exportComponent.getInvalidElements();
                        break;
                    default: //WizardState.intro
                        missingElements = introComponent.getInvalidElements();
                        break;
                }

                missingElementsLabel.Text += missingElements;
            }
            else
            {
                missingElementsLabel.Text = "Page is filled out correctly.";
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
                    isValid = classComponent.isValid() && backgroundComponent.isValid();
                    break;
                case WizardState.stats:
                    isValid = abilityComponent.isValid();
                    break;
                case WizardState.languages:
                    isValid = languageComponent.isValid();
                    break;
                case WizardState.skillEquipment:
                    isValid = skillComponent.isValid() && equipmentComponent.isValid();
                    break;
                case WizardState.spells:
                    isValid = spellComponent.isValid();
                    break;
                case WizardState.extraRaceChoices:
                    isValid = extraRaceChoiceComponent.isValid();
                    break;
                case WizardState.extraClassChoices:
                    isValid = extraClassChoiceComponent.isValid();
                    break;
                case WizardState.extraSubclassChoices:
                    isValid = extraSubclassChoiceComponent.isValid();
                    break;
                case WizardState.story:
                    isValid = nameComponent.isValid() && storyComponent.isValid();
                    break;
                case WizardState.export:
                    isValid = exportComponent.isValid();
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
            saveCurrentPage();
            
            //advance status in WizardManager
            WM.advanceState();

            //refresh panel and buttons
            refreshWindow();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            //save current page values
            saveCurrentPage();

            //revert status in WizardManager
            WM.revertState();

            //refresh panel and buttons
            refreshWindow();
        }

        private void saveCurrentPage()
        {
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
                    backgroundComponent.saveContent();
                    break;
                case WizardState.stats:
                    abilityComponent.saveContent();
                    break;
                case WizardState.languages:
                    languageComponent.saveContent();
                    break;
                case WizardState.skillEquipment:
                    skillComponent.saveContent();
                    equipmentComponent.saveContent();
                    break;
                case WizardState.spells:
                    spellComponent.saveContent();
                    break;
                case WizardState.extraRaceChoices:
                    extraRaceChoiceComponent.saveContent();
                    break;
                case WizardState.extraClassChoices:
                    extraClassChoiceComponent.saveContent();
                    break;
                case WizardState.extraSubclassChoices:
                    extraSubclassChoiceComponent.saveContent();
                    break;
                case WizardState.story:
                    nameComponent.saveContent();
                    storyComponent.saveContent();
                    break;
                case WizardState.export:
                    exportComponent.saveContent();
                    break;
                default: //WizardState.intro
                    introComponent.saveContent();
                    break;
            }
        }

        void raceComponent_SubraceChanged(object sender, EventArgs e)
        {
            RaceControl incoming = sender as RaceControl;
            if (incoming != null)
            {
                alignmentComponent.updateRaceAlignmentDescription(WM.Choices.RaceChoice.getSelectedSubrace());
                ageComponent.updateRaceAgeDescription(WM.Choices.RaceChoice.getSelectedSubrace().Name);
                bodyComponent.updateMinMax(WM.Choices.RaceChoice.getSelectedSubrace().Name);
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

        void classComponent_ClassChanged(object sender, EventArgs e)
        {
            ClassControl incoming = sender as ClassControl;
            if (incoming != null)
            {
                refreshButtons();
                refreshStatusText();
            }
        }

        void classComponent_ClassChoiceChanged(object sender, EventArgs e)
        {
            ClassControl incoming = sender as ClassControl;
            if (incoming != null)
            {
                refreshButtons();
                refreshStatusText();
            }
        }

        void abilityComponent_AbilityAssigned(object sender, EventArgs e)
        {
            AbilityControl incoming = sender as AbilityControl;
            if (incoming != null)
            {
                refreshButtons();
                refreshStatusText();
            }
        }

        void abilityComponent_AbilityBonusAssigned(object sender, EventArgs e)
        {
            AbilityControl incoming = sender as AbilityControl;
            if (incoming != null)
            {
                refreshButtons();
                refreshStatusText();
            }
        }

        void languageComponent_LanguageSelectionChanged(object sender, EventArgs e)
        {
            LanguageControl incoming = sender as LanguageControl;
            if (incoming != null)
            {
                refreshButtons();
                refreshStatusText();
            }
        }

        void skillComponent_SkillChosen(object sender, EventArgs e)
        {
            SkillControl incoming = sender as SkillControl;
            if (incoming != null)
            {
                refreshButtons();
                refreshStatusText();
            }
        }

        void equipmentComponent_EquipmentSelectionChanged(object sender, EventArgs e)
        {
            EquipmentControl incoming = sender as EquipmentControl;
            if (incoming != null)
            {
                refreshButtons();
                refreshStatusText();
            }
        }

        private void spellComponent_SpellChosen(object sender, EventArgs e)
        {
            SpellControl incoming = sender as SpellControl;
            if (incoming != null)
            {
                refreshButtons();
                refreshStatusText();
            }
        }

        private void extraRaceChoiceComponent_ExtraRaceChoiceChanged(object sender, EventArgs e)
        {
            ExtraRaceChoiceControl incoming = sender as ExtraRaceChoiceControl;
            if (incoming != null)
            {
                refreshButtons();
                refreshStatusText();
            }
        }

        private void extraClassChoiceComponent_SubcontrolOptionChosen(object sender, EventArgs e)
        {
            ExtraClassChoiceControl incoming = sender as ExtraClassChoiceControl;
            if (incoming != null)
            {
                refreshButtons();
                refreshStatusText();
            }
        }

        private void extraSubclassChoiceComponent_SubcontrolOptionChosen(object sender, EventArgs e)
        {
            ExtraSubclassChoiceControl incoming = sender as ExtraSubclassChoiceControl;
            if (incoming != null)
            {
                refreshButtons();
                refreshStatusText();
            }
        }

        private void nameComponent_NameChanged(object sender, EventArgs e)
        {
            NameControl incoming = sender as NameControl;
            if (incoming != null)
            {
                refreshButtons();
                refreshStatusText();
            }
        }

        private void storyComponent_SubcontrolOptionChosen(object sender, EventArgs e)
        {
            StoryControl incoming = sender as StoryControl;
            if (incoming != null)
            {
                refreshButtons();
                refreshStatusText();
            }
        }
    }
}
