﻿using System;
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

            extraSubclassSkillComponent = new ExtraSubclassSkillControl(wm);
            extraSubclassSkillComponent.SkillChosen += new EventHandler(subcomponents_OptionChosen);

            totemComponent = new TotemControl(wm);
            totemComponent.TotemOptionChosen += new EventHandler(subcomponents_OptionChosen);

            extraSubclassSpellComponent = new ExtraSubclassSpellControl(wm);
            extraSubclassSpellComponent.SpellChosen += new EventHandler(subcomponents_OptionChosen);

            extraToolProficiencyComponent = new ExtraToolProficiencyControl(wm);

            maneuverComponent = new ManeuverControl(wm);
            maneuverComponent.ManeuverChosen += new EventHandler(subcomponents_OptionChosen);

            draconicAncestryComponent = new DraconicAncestryControl(wm);

            disciplineComponent = new DisciplineControl(wm);
            disciplineComponent.DisciplineChosen += new EventHandler(subcomponents_OptionChosen);

            hunterComponent = new HunterControl(wm);
            hunterComponent.HunterOptionChosen += new EventHandler(subcomponents_OptionChosen);

            companionComponent = new CompanionControl(wm);

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

            //extra skills
            if (subclassChoiceLayout.Controls.Contains(extraSubclassSkillComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = extraSubclassSkillComponent.getInvalidElements();
            }

            //totem
            if (subclassChoiceLayout.Controls.Contains(totemComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = totemComponent.getInvalidElements();
            }

            //extra spells
            if (subclassChoiceLayout.Controls.Contains(extraSubclassSpellComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = extraSubclassSpellComponent.getInvalidElements();
            }

            //extra tool proficiencies
            if (subclassChoiceLayout.Controls.Contains(extraToolProficiencyComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = extraToolProficiencyComponent.getInvalidElements();
            }

            //maneuvers
            if (subclassChoiceLayout.Controls.Contains(maneuverComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = maneuverComponent.getInvalidElements();
            }

            //draconic ancestry
            if (subclassChoiceLayout.Controls.Contains(draconicAncestryComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = draconicAncestryComponent.getInvalidElements();
            }

            //elemental disciplines
            if (subclassChoiceLayout.Controls.Contains(disciplineComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = disciplineComponent.getInvalidElements();
            }

            //hunter features
            if (subclassChoiceLayout.Controls.Contains(hunterComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = hunterComponent.getInvalidElements();
            }

            //beast companion
            if (subclassChoiceLayout.Controls.Contains(companionComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = companionComponent.getInvalidElements();
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
            bool isAncestryValid = true;
            bool isDisciplineValid = true;
            bool isHunterFeatureValid = true;
            bool isCompanionValid = true;

            //extra skills
            if (subclassChoiceLayout.Controls.Contains(extraSubclassSkillComponent))
            {
                isExtraSubclassSkillValid = extraSubclassSkillComponent.isValid();
            }

            //totems
            if (subclassChoiceLayout.Controls.Contains(totemComponent))
            {
                isTotemFeatureValid = totemComponent.isValid();
            }

            //extra spells
            if (subclassChoiceLayout.Controls.Contains(extraSubclassSpellComponent))
            {
                isExtraSubclassSpellValid = extraSubclassSpellComponent.isValid();
            }

            //extra tool proficiencies
            if (subclassChoiceLayout.Controls.Contains(extraToolProficiencyComponent))
            {
                isExtraToolProficiencyValid = extraToolProficiencyComponent.isValid();
            }

            //maneuvers
            if (subclassChoiceLayout.Controls.Contains(maneuverComponent))
            {
                isManeuverValid = maneuverComponent.isValid();
            }

            //draconic ancestry
            if (subclassChoiceLayout.Controls.Contains(draconicAncestryComponent))
            {
                isAncestryValid = draconicAncestryComponent.isValid();
            }

            //elemental disciplines
            if (subclassChoiceLayout.Controls.Contains(disciplineComponent))
            {
                isDisciplineValid = disciplineComponent.isValid();
            }

            //hunter feature
            if (subclassChoiceLayout.Controls.Contains(hunterComponent))
            {
                isHunterFeatureValid = hunterComponent.isValid();
            }

            //beast companion
            if (subclassChoiceLayout.Controls.Contains(companionComponent))
            {
                isCompanionValid = companionComponent.isValid();
            }
            
            return isExtraSubclassSkillValid &&
                   isTotemFeatureValid &&
                   isExtraSubclassSpellValid &&
                   isExtraToolProficiencyValid &&
                   isManeuverValid &&
                   isAncestryValid &&
                   isDisciplineValid &&
                   isHunterFeatureValid &&
                   isCompanionValid;
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

            //draconic ancestry
            if (wm.DBManager.ExtraSubclassChoiceData.DraconicAncestryData.hasDraconicAncestry(wm.Choices.Subclass))
            {
                subclassChoiceLayout.Controls.Add(draconicAncestryComponent);
            }
            draconicAncestryComponent.populateForm();

            //elemental disciplines
            if (wm.DBManager.ExtraSubclassChoiceData.ElementalDisciplineData.hasDisciplines(wm.Choices.Subclass, wm.Choices.Level))
            {
                subclassChoiceLayout.Controls.Add(disciplineComponent);
            }
            disciplineComponent.populateForm();

            //hunter features
            if (wm.DBManager.ExtraSubclassChoiceData.HunterData.hasHunterFeatures(wm.Choices.Subclass, wm.Choices.Level))
            {
                subclassChoiceLayout.Controls.Add(hunterComponent);
            }
            hunterComponent.populateForm();

            //beast companion
            if (wm.DBManager.ExtraSubclassChoiceData.BeastCompanionData.hasCompanion(wm.Choices.Subclass, wm.Choices.Level))
            {
                subclassChoiceLayout.Controls.Add(companionComponent);
            }
            companionComponent.populateForm();
            
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

            //draconic ancestry
            if (subclassChoiceLayout.Controls.Contains(draconicAncestryComponent))
            {
                draconicAncestryComponent.saveContent();
            }
            else
            {
                wm.Choices.Ancestry = new DraconicAncestry();
            }

            //elemental disciplines
            if (subclassChoiceLayout.Controls.Contains(disciplineComponent))
            {
                disciplineComponent.saveContent();
            }
            else
            {
                wm.Choices.ChosenDisciplines.Clear();
                wm.Choices.MandatoryDisciplines.Clear();
            }

            //hunter features
            if (subclassChoiceLayout.Controls.Contains(hunterComponent))
            {
                hunterComponent.saveContent();
            }
            else
            {
                wm.Choices.HunterFeatures.Clear();
            }

            //beast companion
            if (subclassChoiceLayout.Controls.Contains(companionComponent))
            {
                companionComponent.saveContent();
            }
            else
            {
                wm.Choices.BeastCompanion = new Beast();
            }
            
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
