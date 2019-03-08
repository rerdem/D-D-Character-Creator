using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Easy_DnD_Character_Creator.WizardComponents.SubComponents;

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public partial class ExtraClassChoiceControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private FightingStyleControl fightingStyleComponent;
        private FavoredEnemyTerrainControl favoredEnemyTerrainComponent;
        private ExtraClassSkillControl extraClassSkillsComponent;

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

            return missingElements;
        }

        public bool isValid()
        {
            bool isFightingStyleValid = true;
            bool isFavoredEnemyTerrainValid = true;
            bool isExtraClassSkillValid = true;

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

            return isFightingStyleValid &&
                   isFavoredEnemyTerrainValid &&
                   isExtraClassSkillValid;
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
