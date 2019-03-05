using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_DnD_Character_Creator.WizardComponents.SubComponents
{
    public partial class FavoredEnemyTerrainControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private int lastLevel;

        private int favoredEnemyAmount;
        private List<string> availableEnemies;
        private List<int> enemyOrder;

        private int favoredTerrainAmount;
        private List<string> availableTerrain;
        private List<int> terrainOrder;

        public FavoredEnemyTerrainControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            lastLevel = 0;

            favoredEnemyAmount = 0;
            availableEnemies = new List<string>();
            enemyOrder = new List<int>();

            favoredTerrainAmount = 0;
            availableTerrain = new List<string>();
            terrainOrder = new List<int>();

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

            if (enemyBox.SelectedItems.Count != favoredEnemyAmount)
            {
                output = $"select {favoredEnemyAmount - enemyBox.SelectedItems.Count} more enemy type(s)";
            }

            if (terrainBox.SelectedItems.Count != favoredTerrainAmount)
            {
                if (!string.IsNullOrEmpty(output))
                {
                    output += ", ";
                }
                output += $"select {favoredTerrainAmount - terrainBox.SelectedItems.Count} more terrain type(s)";
            }

            return output;
        }

        public bool isValid()
        {
            return ((enemyBox.SelectedItems.Count == favoredEnemyAmount) && (terrainBox.SelectedItems.Count == favoredTerrainAmount));
        }

        public void populateForm()
        {
            resetPage();

            if (Visited && !hasLevelChanged())
            {
                loadChoices();
            }

            lastLevel = wm.Choices.Level;
            Visited = true;
        }
        
        public void saveContent()
        {
            wm.Choices.FavoredEnemies = string.Join(", ", enemyBox.Items
                                                                  .OfType<object>()
                                                                  .Select(item => item.ToString())
                                                                  .ToArray());
            wm.Choices.FavoredTerrains = string.Join(", ", terrainBox.Items
                                                                     .OfType<object>()
                                                                     .Select(item => item.ToString())
                                                                     .ToArray());
        }

        private void resetPage()
        {
            favoredEnemyAmount = wm.DBManager.ExtraClassChoiceData.getFavoredEnemyAmount(wm.Choices.Class, wm.Choices.Level);
            favoredTerrainAmount = wm.DBManager.ExtraClassChoiceData.getFavoredTerrainAmount(wm.Choices.Class, wm.Choices.Level);

            if (!Visited || hasLevelChanged())
            {
                favoredLabel.Text = $"You have significant experience studying, tracking, hunting, and even talking to {favoredEnemyAmount} type(s) of enemies. " +
                                   $"You are also particularly familiar with {favoredTerrainAmount} type(s) of natural environment and are adept at traveling and surviving in such regions. " +
                                   $"Choose each below.";
            }

            availableEnemies = wm.DBManager.ExtraClassChoiceData.getFavoredEnemyTypes();
            availableTerrain = wm.DBManager.ExtraClassChoiceData.getFavoredTerrainTypes();

            enemyBox.BeginUpdate();
            enemyBox.DataSource = null;
            enemyBox.DataSource = availableEnemies;
            enemyBox.EndUpdate();
            enemyOrder.Clear();

            terrainBox.BeginUpdate();
            terrainBox.DataSource = null;
            terrainBox.DataSource = availableTerrain;
            terrainBox.EndUpdate();
            terrainOrder.Clear();
        }

        private void loadChoices()
        {
            //load enemy
            for (int i = 0; i < enemyBox.Items.Count; i++)
            {
                if (wm.Choices.FavoredEnemies.Contains(enemyBox.Items[i].ToString()))
                {
                    enemyBox.SetSelected(i, true);
                }
                else
                {
                    enemyBox.SetSelected(i, false);
                }
            }

            //load terrain
            for (int i = 0; i < terrainBox.Items.Count; i++)
            {
                if (wm.Choices.FavoredTerrains.Contains(terrainBox.Items[i].ToString()))
                {
                    terrainBox.SetSelected(i, true);
                }
                else
                {
                    terrainBox.SetSelected(i, false);
                }
            }
        }

        private bool hasLevelChanged()
        {
            return (lastLevel != wm.Choices.Level);
        }

        private void syncEnemySelection()
        {
            //add new selected items
            foreach (int index in enemyBox.SelectedIndices)
            {
                if (!enemyOrder.Contains(index))
                {
                    enemyOrder.Add(index);
                }
            }

            //remove deselected items
            enemyOrder.RemoveAll(index => !enemyBox.SelectedIndices.Contains(index));
        }

        private void syncTerrainSelection()
        {
            //add new selected items
            foreach (int index in terrainBox.SelectedIndices)
            {
                if (!terrainOrder.Contains(index))
                {
                    terrainOrder.Add(index);
                }
            }

            //remove deselected items
            terrainOrder.RemoveAll(index => !terrainBox.SelectedIndices.Contains(index));
        }

        private void enemyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            syncEnemySelection();
            if (enemyBox.SelectedIndices.Count > 0)
            {
                if (enemyBox.SelectedIndices.Count <= favoredEnemyAmount)
                {
                    int lastSelectedIndex = enemyOrder.ElementAt(enemyOrder.Count - 1);
                }
                else
                {
                    int lastSelectedIndex = enemyOrder.ElementAt(enemyOrder.Count - 1);
                    enemyBox.SelectedIndices.Remove(lastSelectedIndex);
                }
            }
        }

        private void terrainBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            syncTerrainSelection();
            if (terrainBox.SelectedIndices.Count > 0)
            {
                if (terrainBox.SelectedIndices.Count <= favoredTerrainAmount)
                {
                    int lastSelectedIndex = terrainOrder.ElementAt(terrainOrder.Count - 1);
                }
                else
                {
                    int lastSelectedIndex = terrainOrder.ElementAt(terrainOrder.Count - 1);
                    terrainBox.SelectedIndices.Remove(lastSelectedIndex);
                }
            }
        }
    }
}
