using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Easy_DnD_Character_Creator.WizardComponents.ExtraClassComponents;
using Easy_DnD_Character_Creator.DataTypes;

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public partial class ExtraClassChoiceControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private FightingStyleControl fightingStyleComponent;
        private FavoredEnemyTerrainControl favoredEnemyTerrainComponent;
        private ExtraClassSkillControl extraClassSkillsComponent;
        private WarlockControl warlockComponent;
        private MetamagicControl metamagicComponent;

        public event EventHandler SubcontrolOptionChosen;

        public ExtraClassChoiceControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            fightingStyleComponent = new FightingStyleControl(wm);
            fightingStyleComponent.FightingStyleChosen += new EventHandler(fightingStyleComponent_FightingStyleChosen);

            favoredEnemyTerrainComponent = new FavoredEnemyTerrainControl(wm);
            favoredEnemyTerrainComponent.FavoredEnemyChosen += new EventHandler(favoredEnemyTerrainComponent_FavoredEnemyTerrainChosen);
            favoredEnemyTerrainComponent.FavoredTerrainChosen += new EventHandler(favoredEnemyTerrainComponent_FavoredEnemyTerrainChosen);

            extraClassSkillsComponent = new ExtraClassSkillControl(wm);
            extraClassSkillsComponent.SkillChosen += new EventHandler(extraClassSkillsComponent_SkillChosen);

            warlockComponent = new WarlockControl(wm);
            warlockComponent.SubcontrolOptionChosen += new EventHandler(warlockComponent_SubcontrolOptionChosen);

            metamagicComponent = new MetamagicControl(wm);
            metamagicComponent.MetamagicChosen += new EventHandler(metamagicComponent_MetamagicChosen);

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

            if (classChoiceLayout.Controls.Contains(fightingStyleComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = fightingStyleComponent.getInvalidElements();
            }

            if (classChoiceLayout.Controls.Contains(favoredEnemyTerrainComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = favoredEnemyTerrainComponent.getInvalidElements();
            }

            if (classChoiceLayout.Controls.Contains(extraClassSkillsComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = extraClassSkillsComponent.getInvalidElements();
            }

            if (classChoiceLayout.Controls.Contains(warlockComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = warlockComponent.getInvalidElements();
            }

            if (classChoiceLayout.Controls.Contains(metamagicComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = metamagicComponent.getInvalidElements();
            }

            return missingElements;
        }

        public bool isValid()
        {
            bool isFightingStyleValid = true;
            bool isFavoredEnemyTerrainValid = true;
            bool isExtraClassSkillValid = true;
            bool isWarlockValid = true;
            bool isMetamagicValid = true;

            if (classChoiceLayout.Controls.Contains(fightingStyleComponent))
            {
                isFightingStyleValid = fightingStyleComponent.isValid();
            }

            if (classChoiceLayout.Controls.Contains(favoredEnemyTerrainComponent))
            {
                isFavoredEnemyTerrainValid = favoredEnemyTerrainComponent.isValid();
            }

            if (classChoiceLayout.Controls.Contains(extraClassSkillsComponent))
            {
                isExtraClassSkillValid = extraClassSkillsComponent.isValid();
            }

            if (classChoiceLayout.Controls.Contains(warlockComponent))
            {
                isWarlockValid = warlockComponent.isValid();
            }

            if (classChoiceLayout.Controls.Contains(metamagicComponent))
            {
                isMetamagicValid = metamagicComponent.isValid();
            }

            return isFightingStyleValid &&
                   isFavoredEnemyTerrainValid &&
                   isExtraClassSkillValid &&
                   isWarlockValid &&
                   isMetamagicValid;
        }

        public void populateForm()
        {
            //setup subcontrols
            classChoiceLayout.Controls.Clear();
            //fighting style
            if (wm.DBManager.ExtraClassChoiceData.FightingStyleData.hasFightingStyle(wm.Choices.Class, wm.Choices.Level))
            {
                classChoiceLayout.Controls.Add(fightingStyleComponent);
            }
            fightingStyleComponent.populateForm();

            //favored enemy and terrain
            if (wm.DBManager.ExtraClassChoiceData.FavoredEnemyTerrainData.hasFavoredEnemyTerrain(wm.Choices.Class, wm.Choices.Level))
            {
                classChoiceLayout.Controls.Add(favoredEnemyTerrainComponent);
            }
            favoredEnemyTerrainComponent.populateForm();

            //additional skills
            if (wm.DBManager.ExtraClassChoiceData.ExtraClassSkillData.hasSkillChoice(wm.Choices.Class, wm.Choices.Level))
            {
                classChoiceLayout.Controls.Add(extraClassSkillsComponent);
            }
            extraClassSkillsComponent.populateForm();

            //warlock choices
            if (wm.DBManager.ExtraClassChoiceData.WarlockChoiceData.hasWarlockChoices(wm.Choices.Class, wm.Choices.Level))
            {
                classChoiceLayout.Controls.Add(warlockComponent);
            }
            warlockComponent.populateForm();

            //metamagic
            if (wm.DBManager.ExtraClassChoiceData.MetamagicData.hasMetamagic(wm.Choices.Class, wm.Choices.Level))
            {
                classChoiceLayout.Controls.Add(metamagicComponent);
            }
            metamagicComponent.populateForm();

            Visited = true;
        }

        public void saveContent()
        {
            if (classChoiceLayout.Controls.Contains(fightingStyleComponent))
            {
                fightingStyleComponent.saveContent();
            }
            else
            {
                wm.Choices.ClassFightingStyles.Clear();
            }

            if (classChoiceLayout.Controls.Contains(favoredEnemyTerrainComponent))
            {
                favoredEnemyTerrainComponent.saveContent();
            }
            else
            {
                wm.Choices.FavoredEnemies = "";
                wm.Choices.FavoredTerrains = "";
            }

            if (classChoiceLayout.Controls.Contains(extraClassSkillsComponent))
            {
                extraClassSkillsComponent.saveContent();
            }
            else
            {
                wm.Choices.ClassSkills.Clear();
                wm.Choices.DoublesProficiency = false;
            }

            if (classChoiceLayout.Controls.Contains(warlockComponent))
            {
                warlockComponent.saveContent();
            }
            else
            {
                wm.Choices.WarlockPactChoice = new WarlockPact();
                wm.Choices.WarlockPactSpells.Clear();
                wm.Choices.WarlockInvocations.Clear();
                wm.Choices.WarlockInvocationSpells.Clear();
            }

            if (classChoiceLayout.Controls.Contains(metamagicComponent))
            {
                metamagicComponent.saveContent();
            }
            else
            {
                wm.Choices.SorcererMetamagic.Clear();
            }
        }

        private void fightingStyleComponent_FightingStyleChosen(object sender, EventArgs e)
        {
            OnSubcontrolOptionChosen(null);
        }

        private void favoredEnemyTerrainComponent_FavoredEnemyTerrainChosen(object sender, EventArgs e)
        {
            OnSubcontrolOptionChosen(null);
        }

        private void extraClassSkillsComponent_SkillChosen(object sender, EventArgs e)
        {
            OnSubcontrolOptionChosen(null);
        }

        private void warlockComponent_SubcontrolOptionChosen(object sender, EventArgs e)
        {
            OnSubcontrolOptionChosen(null);
        }

        private void metamagicComponent_MetamagicChosen(object sender, EventArgs e)
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
