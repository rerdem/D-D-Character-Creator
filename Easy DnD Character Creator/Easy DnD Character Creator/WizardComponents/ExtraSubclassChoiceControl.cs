using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents;
using Easy_DnD_Character_Creator.DataTypes;

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public partial class ExtraSubclassChoiceControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private List<bool> subcomponentActivationList;
        private List<IWizardControl> subcomponentList;
        
        private ExtraSubclassSkillControl extraSubclassSkillComponent;
        private TotemControl totemComponent;
        private ExtraSubclassSpellControl extraSubclassSpellComponent;
        private ExtraToolProficiencyControl extraToolProficiencyComponent;
        private ManeuverControl maneuverComponent;
        private DraconicAncestryControl draconicAncestryComponent;
        private DisciplineControl disciplineComponent;
        private HunterControl hunterComponent;
        private CompanionControl companionComponent;
        private CircleTerrainControl circleTerrainComponent;

        public event EventHandler SubcontrolOptionChosen;

        public ExtraSubclassChoiceControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            subcomponentActivationList = new List<bool>();
            initializeSubcomponentList();
            refreshActivationList();
            

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
            string missingElements = "";

            for (int i = 0; i < subcomponentList.Count; i++)
            {
                if (subcomponentActivationList[i])
                {
                    if (!string.IsNullOrEmpty(missingElements))
                    {
                        missingElements += ", ";
                    }

                    missingElements = subcomponentList[i].getInvalidElements();
                }
            }
            
            return missingElements;
        }

        public bool isValid()
        {
            for (int i = 0; i < subcomponentList.Count; i++)
            {
                if (subcomponentActivationList[i])
                {
                    if (!subcomponentList[i].isValid())
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void populateForm()
        {
            refreshActivationList();

            //setup subcontrols
            subclassChoiceLayout.Controls.Clear();

            for (int i = 0; i < subcomponentList.Count; i++)
            {
                if (subcomponentActivationList[i])
                {
                    subclassChoiceLayout.Controls.Add((UserControl)subcomponentList[i]);
                }
                subcomponentList[i].populateForm();
            }
            
            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.ClassChoice.getSelectedSubclass().ExtraSkills.Clear();
            wm.Choices.ClassChoice.getSelectedSubclass().DoublesProficiency = false;
            wm.Choices.ClassChoice.getSelectedSubclass().TotemFeatures.Clear();
            wm.Choices.ClassChoice.getSelectedSubclass().ExtraSpells.Clear();
            wm.Choices.ClassChoice.getSelectedSubclass().ExtraToolProficiency = "";
            wm.Choices.ClassChoice.getSelectedSubclass().Maneuvers.Clear();
            wm.Choices.ClassChoice.getSelectedSubclass().Ancestry = new DraconicAncestry();
            wm.Choices.ClassChoice.getSelectedSubclass().ChosenDisciplines.Clear();
            wm.Choices.ClassChoice.getSelectedSubclass().MandatoryDisciplines.Clear();
            wm.Choices.ClassChoice.getSelectedSubclass().HunterFeatures.Clear();
            wm.Choices.ClassChoice.getSelectedSubclass().BeastCompanion = new Beast();

            for (int i = 0; i < subcomponentList.Count; i++)
            {
                if (subcomponentActivationList[i])
                {
                    subcomponentList[i].saveContent();
                }
            }
        }

        private void refreshActivationList()
        {
            subcomponentActivationList.Clear();
            subcomponentActivationList.Add(wm.Choices.ClassChoice.getSelectedSubclass().HasExtraSkills);
            subcomponentActivationList.Add(wm.Choices.ClassChoice.getSelectedSubclass().HasTotems);
            subcomponentActivationList.Add(wm.Choices.ClassChoice.getSelectedSubclass().HasExtraSpells);
            subcomponentActivationList.Add(wm.Choices.ClassChoice.getSelectedSubclass().HasExtraToolProficiencies);
            subcomponentActivationList.Add(wm.Choices.ClassChoice.getSelectedSubclass().HasManeuvers);
            subcomponentActivationList.Add(wm.Choices.ClassChoice.getSelectedSubclass().HasDraconicAncestry);
            subcomponentActivationList.Add(wm.Choices.ClassChoice.getSelectedSubclass().HasElementalDisciplines);
            subcomponentActivationList.Add(wm.Choices.ClassChoice.getSelectedSubclass().HasHunterChoices);
            subcomponentActivationList.Add(wm.Choices.ClassChoice.getSelectedSubclass().HasCompanion);
            subcomponentActivationList.Add(wm.Choices.ClassChoice.getSelectedSubclass().HasCircleTerrain);

            //fail safe, in case someone forgets to add the appropriate variable to the activation list
            //this should not need to exist, rethink link in future update
            while (subcomponentActivationList.Count < subcomponentList.Count)
            {
                subcomponentActivationList.Add(false);
            }
        }

        private void initializeSubcomponentList()
        {
            subcomponentList = new List<IWizardControl>();

            extraSubclassSkillComponent = new ExtraSubclassSkillControl(wm);
            extraSubclassSkillComponent.SkillChosen += new EventHandler(subcomponents_OptionChosen);
            subcomponentList.Add(extraSubclassSkillComponent);

            totemComponent = new TotemControl(wm);
            totemComponent.TotemOptionChosen += new EventHandler(subcomponents_OptionChosen);
            subcomponentList.Add(totemComponent);

            extraSubclassSpellComponent = new ExtraSubclassSpellControl(wm);
            extraSubclassSpellComponent.SpellChosen += new EventHandler(subcomponents_OptionChosen);
            subcomponentList.Add(extraSubclassSpellComponent);

            extraToolProficiencyComponent = new ExtraToolProficiencyControl(wm);
            subcomponentList.Add(extraToolProficiencyComponent);

            maneuverComponent = new ManeuverControl(wm);
            maneuverComponent.ManeuverChosen += new EventHandler(subcomponents_OptionChosen);
            subcomponentList.Add(maneuverComponent);

            draconicAncestryComponent = new DraconicAncestryControl(wm);
            subcomponentList.Add(draconicAncestryComponent);

            disciplineComponent = new DisciplineControl(wm);
            disciplineComponent.DisciplineChosen += new EventHandler(subcomponents_OptionChosen);
            subcomponentList.Add(disciplineComponent);

            hunterComponent = new HunterControl(wm);
            hunterComponent.HunterOptionChosen += new EventHandler(subcomponents_OptionChosen);
            subcomponentList.Add(hunterComponent);

            companionComponent = new CompanionControl(wm);
            subcomponentList.Add(companionComponent);

            circleTerrainComponent = new CircleTerrainControl(wm);
            subcomponentList.Add(circleTerrainComponent);
        }

        private void subcomponents_OptionChosen(object sender, EventArgs e)
        {
            OnSubcontrolOptionChosen(null);
        }

        protected virtual void OnSubcontrolOptionChosen(EventArgs e)
        {
            EventHandler handler = SubcontrolOptionChosen;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
