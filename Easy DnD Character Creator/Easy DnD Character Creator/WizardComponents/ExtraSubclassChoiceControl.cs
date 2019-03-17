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

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public partial class ExtraSubclassChoiceControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private ExtraSubclassSkillControl extraSubclassSkillComponent;
        private TotemControl totemComponent;
        private ExtraSubclassSpellControl extraSubclassSpellComponent;
        private ExtraToolProficiencyControl extraToolProficiencyComponent;
        private ManeuverControl maneuverComponent;

        public event EventHandler SubcontrolOptionChosen;

        public ExtraSubclassChoiceControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            extraSubclassSkillComponent = new ExtraSubclassSkillControl(wm);
            extraSubclassSkillComponent.SkillChosen += new EventHandler(extraSubclassSkillComponent_SkillChosen);

            totemComponent = new TotemControl(wm);
            totemComponent.TotemOptionChosen += new EventHandler(totemComponent_TotemOptionChosen);

            extraSubclassSpellComponent = new ExtraSubclassSpellControl(wm);
            extraSubclassSpellComponent.SpellChosen += new EventHandler(extraSubclassSpellComponent_SpellChosen);

            extraToolProficiencyComponent = new ExtraToolProficiencyControl(wm);

            maneuverComponent = new ManeuverControl(wm);
            maneuverComponent.ManeuverChosen += new EventHandler(maneuverComponent_ManeuverChosen);

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

            if (subclassChoiceLayout.Controls.Contains(extraSubclassSkillComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = extraSubclassSkillComponent.getInvalidElements();
            }

            if (subclassChoiceLayout.Controls.Contains(totemComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = totemComponent.getInvalidElements();
            }

            if (subclassChoiceLayout.Controls.Contains(extraSubclassSpellComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = extraSubclassSpellComponent.getInvalidElements();
            }

            if (subclassChoiceLayout.Controls.Contains(extraToolProficiencyComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = extraToolProficiencyComponent.getInvalidElements();
            }

            if (subclassChoiceLayout.Controls.Contains(maneuverComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = maneuverComponent.getInvalidElements();
            }

            return missingElements;
        }

        public bool isValid()
        {
            bool isExtraSubclassSkillValid = true;
            bool isTotemFeatureValid = true;
            bool isExtraSubclassSpellValid = true;
            bool isExtraToolProficiencyValid = true;
            bool isManeuverValid = true;

            if (subclassChoiceLayout.Controls.Contains(extraSubclassSkillComponent))
            {
                isExtraSubclassSkillValid = extraSubclassSkillComponent.isValid();
            }

            if (subclassChoiceLayout.Controls.Contains(totemComponent))
            {
                isTotemFeatureValid = totemComponent.isValid();
            }

            if (subclassChoiceLayout.Controls.Contains(extraSubclassSpellComponent))
            {
                isExtraSubclassSpellValid = extraSubclassSpellComponent.isValid();
            }

            if (subclassChoiceLayout.Controls.Contains(extraToolProficiencyComponent))
            {
                isExtraToolProficiencyValid = extraToolProficiencyComponent.isValid();
            }

            if (subclassChoiceLayout.Controls.Contains(maneuverComponent))
            {
                isManeuverValid = maneuverComponent.isValid();
            }

            return isExtraSubclassSkillValid &&
                   isTotemFeatureValid &&
                   isExtraSubclassSpellValid &&
                   isExtraToolProficiencyValid &&
                   isManeuverValid;
        }

        public void populateForm()
        {
            //setup subcontrols
            subclassChoiceLayout.Controls.Clear();
            //additional skills
            if (wm.DBManager.ExtraSubclassChoiceData.ExtraSubclassSkillData.hasSkillChoice(wm.Choices.Subclass, wm.Choices.Level))
            {
                subclassChoiceLayout.Controls.Add(extraSubclassSkillComponent);
            }
            extraSubclassSkillComponent.populateForm();

            //totems
            if (wm.DBManager.ExtraSubclassChoiceData.TotemData.hasTotemFeatures(wm.Choices.Subclass, wm.Choices.Level))
            {
                subclassChoiceLayout.Controls.Add(totemComponent);
            }
            totemComponent.populateForm();

            //additional spells
            if (wm.DBManager.ExtraSubclassChoiceData.ExtraSubclassSpellData.hasExtraSpellChoice(wm.Choices.Subclass))
            {
                subclassChoiceLayout.Controls.Add(extraSubclassSpellComponent);
            }
            extraSubclassSpellComponent.populateForm();

            //additional tool proficiencies
            if (wm.DBManager.ExtraSubclassChoiceData.ExtraToolProficiencyData.hasToolProficiencyChoice(wm.Choices.Subclass, wm.Choices.Level))
            {
                subclassChoiceLayout.Controls.Add(extraToolProficiencyComponent);
            }
            extraToolProficiencyComponent.populateForm();

            //maneuvers
            if (wm.DBManager.ExtraSubclassChoiceData.ManeuverData.hasManeuvers(wm.Choices.Subclass, wm.Choices.Level))
            {
                subclassChoiceLayout.Controls.Add(maneuverComponent);
            }
            maneuverComponent.populateForm();

            Visited = true;
        }

        public void saveContent()
        {
            //extra subclass skills
            if (subclassChoiceLayout.Controls.Contains(extraSubclassSkillComponent))
            {
                extraSubclassSkillComponent.saveContent();
            }
            else
            {
                wm.Choices.SubclassSkills.Clear();
                wm.Choices.SubclassDoublesProficiency = false;
            }

            //totem choices
            if (subclassChoiceLayout.Controls.Contains(totemComponent))
            {
                totemComponent.saveContent();
            }
            else
            {
                wm.Choices.TotemFeatures.Clear();
            }

            //extra subclass spells
            if (subclassChoiceLayout.Controls.Contains(extraSubclassSpellComponent))
            {
                extraSubclassSpellComponent.saveContent();
            }
            else
            {
                wm.Choices.SubclassSpells.Clear();
            }

            //extra subclass tool proficiencies
            if (subclassChoiceLayout.Controls.Contains(extraToolProficiencyComponent))
            {
                extraToolProficiencyComponent.saveContent();
            }
            else
            {
                wm.Choices.SubclassToolProficiency = "";
            }

            //maneuvers
            if (subclassChoiceLayout.Controls.Contains(maneuverComponent))
            {
                maneuverComponent.saveContent();
            }
            else
            {
                wm.Choices.Maneuvers.Clear();
            }
        }

        private void extraSubclassSkillComponent_SkillChosen(object sender, EventArgs e)
        {
            OnSubcontrolOptionChosen(null);
        }

        private void extraSubclassSpellComponent_SpellChosen(object sender, EventArgs e)
        {
            OnSubcontrolOptionChosen(null);
        }

        private void totemComponent_TotemOptionChosen(object sender, EventArgs e)
        {
            OnSubcontrolOptionChosen(null);
        }

        private void maneuverComponent_ManeuverChosen(object sender, EventArgs e)
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
