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

namespace Easy_DnD_Character_Creator.WizardComponents.StoryComponents
{
    public partial class WildShapeControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private string lastClass;

        private List<WildShapeTerrain> comboSource;

        public event EventHandler WildShapeTerrainChosen;

        public WildShapeControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            lastClass = "";

            comboSource = new List<WildShapeTerrain>();

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
            string output = "";

            if (terrainComboBox.Items.Count > 0)
            {
                if (terrainComboBox.SelectedItem == null)
                {
                    output = "select a terrain";
                }
            }

            return output;
        }

        public bool isValid()
        {
            if (terrainComboBox.Items.Count > 0)
            {
                return (terrainComboBox.SelectedItem != null);
            }
            else
            {
                return true;
            }
        }

        public void populateForm()
        {
            if (!Visited || hasClassChanged())
            {
                comboSource = wm.DBManager.StoryData.getWildShapeTerrains();
                terrainComboBox.BeginUpdate();
                terrainComboBox.DataSource = null;
                terrainComboBox.DataSource = comboSource;
                terrainComboBox.DisplayMember = "Name";
                terrainComboBox.EndUpdate();

                introLabel.Text = $"Please choose the terrain in which you became a {wm.Choices.ClassChoice.Name}:";
            }

            if (Visited && !hasClassChanged())
            {
                if (terrainComboBox.Items.Contains(wm.Choices.TerrainChoice))
                {
                    terrainComboBox.SelectedItem = wm.Choices.TerrainChoice;
                }
            }

            lastClass = wm.Choices.ClassChoice.Name;
            Visited = true;
        }

        public void saveContent()
        {
            if ((wm.Choices.ClassChoice.HasWildShape) && (terrainComboBox.SelectedItem != null))
            {
                wm.Choices.TerrainChoice = (WildShapeTerrain)terrainComboBox.SelectedItem;
            }
            else
            {
                wm.Choices.TerrainChoice = new WildShapeTerrain();
            }
        }

        private bool hasClassChanged()
        {
            return (lastClass != wm.Choices.ClassChoice.Name);
        }

        private void terrainComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnWildShapeTerrainChosen(null);
        }

        protected virtual void OnWildShapeTerrainChosen(EventArgs e)
        {
            EventHandler handler = WildShapeTerrainChosen;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
