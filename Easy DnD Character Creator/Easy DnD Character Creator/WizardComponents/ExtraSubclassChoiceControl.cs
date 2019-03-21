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
            wm.Choices.SubclassSkills.Clear();
            wm.Choices.SubclassDoublesProficiency = false;
            wm.Choices.TotemFeatures.Clear();
            wm.Choices.SubclassSpells.Clear();
            wm.Choices.SubclassToolProficiency = "";
            wm.Choices.Maneuvers.Clear();
            wm.Choices.Ancestry = new DraconicAncestry();
            wm.Choices.ChosenDisciplines.Clear();
            wm.Choices.MandatoryDisciplines.Clear();
            wm.Choices.HunterFeatures.Clear();
            wm.Choices.BeastCompanion = new Beast();

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
            subcomponentActivationList.Add(wm.Choices.HasExtraSubclassSkills);
            subcomponentActivationList.Add(wm.Choices.HasTotems);
            subcomponentActivationList.Add(wm.Choices.HasExtraSubclassSpells);
            subcomponentActivationList.Add(wm.Choices.HasExtraToolProficiencies);
            subcomponentActivationList.Add(wm.Choices.HasManeuvers);
            subcomponentActivationList.Add(wm.Choices.HasDraconicAncestry);
            subcomponentActivationList.Add(wm.Choices.HasElementalDisciplines);
            subcomponentActivationList.Add(wm.Choices.HasHunterChoices);
            subcomponentActivationList.Add(wm.Choices.HasCompanion);
            subcomponentActivationList.Add(wm.Choices.HasCircleTerrain);
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
