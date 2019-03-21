using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Easy_DnD_Character_Creator.DataTypes;

namespace Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents
{
    public partial class CircleTerrainControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        List<CircleTerrain> circleTerrainComboSource;

        public CircleTerrainControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            circleTerrainComboSource = new List<CircleTerrain>();

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
            if (circleTerrainComboBox.SelectedItem == null)
            {
                return "select a circle terrain";
            }

            return "";
        }

        public bool isValid()
        {
            return (circleTerrainComboBox.SelectedItem != null);
        }

        public void populateForm()
        {
            circleTerrainComboSource = wm.DBManager.ExtraSubclassChoiceData.CircleTerrainData.getCircleTerrains(wm.Choices.Subclass, wm.Choices.Level);

            circleTerrainComboBox.BeginUpdate();
            circleTerrainComboBox.DataSource = null;
            circleTerrainComboBox.DataSource = circleTerrainComboSource;
            circleTerrainComboBox.DisplayMember = "Name";
            circleTerrainComboBox.EndUpdate();

            if (Visited)
            {
                if (circleTerrainComboBox.Items.Contains(wm.Choices.DruidCircleTerrain))
                {
                    circleTerrainComboBox.SelectedItem = wm.Choices.DruidCircleTerrain;
                }
            }

            Visited = true;
        }

        public void saveContent()
        {
            if (circleTerrainComboBox.SelectedItem != null)
            {
                wm.Choices.DruidCircleTerrain = (CircleTerrain)circleTerrainComboBox.SelectedItem;
            }
            else
            {
                wm.Choices.DruidCircleTerrain = new CircleTerrain();
            }
        }
    }
}
