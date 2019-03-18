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
    public partial class DraconicAncestryControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        List<DraconicAncestry> ancestryComboSource;

        public DraconicAncestryControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            ancestryComboSource = new List<DraconicAncestry>();

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
            if (ancestryComboBox.SelectedItem == null)
            {
                return "select a dragon ancestry";
            }

            return "";
        }

        public bool isValid()
        {
            return (ancestryComboBox.SelectedItem != null);
        }

        public void populateForm()
        {
            ancestryComboSource = wm.DBManager.ExtraSubclassChoiceData.DraconicAncestryData.getDraconicAncestries();

            ancestryComboBox.BeginUpdate();
            ancestryComboBox.DataSource = null;
            ancestryComboBox.DataSource = ancestryComboSource;
            ancestryComboBox.DisplayMember = "FullInfo";
            ancestryComboBox.EndUpdate();

            if (Visited)
            {
                if (ancestryComboBox.Items.Contains(wm.Choices.Ancestry))
                {
                    ancestryComboBox.SelectedIndex = ancestryComboBox.Items.IndexOf(wm.Choices.Ancestry);
                }
            }

            Visited = true;
        }

        public void saveContent()
        {
            if (ancestryComboBox.SelectedItem != null)
            {
                wm.Choices.Ancestry = (DraconicAncestry)ancestryComboBox.SelectedItem;
            }
            else
            {
                wm.Choices.Ancestry = new DraconicAncestry();
            }
        }
    }
}