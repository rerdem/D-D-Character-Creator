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

        private FightingStyleControl fightingStyle;
        private FavoredEnemyTerrainControl favoredEnemyTerrain;

        public ExtraClassChoiceControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            fightingStyle = new FightingStyleControl(wm);
            favoredEnemyTerrain = new FavoredEnemyTerrainControl(wm);

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
            return "";
        }

        public bool isValid()
        {
            return true;
        }

        public void populateForm()
        {
            Visited = true;
        }

        public void saveContent()
        {
            
        }
    }
}
