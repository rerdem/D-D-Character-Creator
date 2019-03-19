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
    public partial class CompanionControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        List<Beast> companionComboSource;

        public CompanionControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            companionComboSource = new List<Beast>();

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
            if (companionComboBox.SelectedItem == null)
            {
                return "select a beast companion";
            }

            return "";
        }

        public bool isValid()
        {
            return (companionComboBox.SelectedItem != null);
        }

        public void populateForm()
        {
            companionComboSource = wm.DBManager.ExtraSubclassChoiceData.BeastCompanionData.getBeastCompanions(wm.Choices.Subclass, wm.Choices.Level);

            companionComboBox.BeginUpdate();
            companionComboBox.DataSource = null;
            companionComboBox.DataSource = companionComboSource;
            companionComboBox.DisplayMember = "Name";
            companionComboBox.EndUpdate();

            if (Visited)
            {
                if (companionComboBox.Items.Contains(wm.Choices.BeastCompanion))
                {
                    companionComboBox.SelectedIndex = companionComboBox.Items.IndexOf(wm.Choices.BeastCompanion);
                }
            }

            Visited = true;
        }

        public void saveContent()
        {
            if (companionComboBox.SelectedItem != null)
            {
                wm.Choices.BeastCompanion = (Beast)companionComboBox.SelectedItem;
            }
            else
            {
                wm.Choices.BeastCompanion = new Beast();
            }
        }

        private void companionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (companionComboBox.SelectedItem != null)
            {
                Beast currentBeast = (Beast)companionComboBox.SelectedItem;
                bookLabel.Text = $"See {currentBeast.Book} page {currentBeast.Page} or ask your Dungeon Master for detailed statistics.";
            }
        }
    }
}
