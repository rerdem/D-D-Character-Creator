using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents
{
    public partial class ExtraToolProficiencyControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        List<string> toolComboSource;

        public ExtraToolProficiencyControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            toolComboSource = new List<string>();

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
            if (toolComboBox.SelectedItem == null)
            {
                return "select a tool proficiency";
            }
            
            return "";
        }

        public bool isValid()
        {
            return (toolComboBox.SelectedItem != null);
        }

        public void populateForm()
        {
            toolComboSource = wm.DBManager.ExtraSubclassChoiceData.ExtraToolProficiencyData.getToolProficiencyChoices(wm.Choices.Subclass, wm.Choices.Level);

            toolComboBox.BeginUpdate();
            toolComboBox.DataSource = null;
            toolComboBox.DataSource = toolComboSource;
            toolComboBox.EndUpdate();

            if (Visited)
            {
                if (toolComboBox.Items.Contains(wm.Choices.SubclassToolProficiency))
                {
                    toolComboBox.SelectedIndex = toolComboBox.Items.IndexOf(wm.Choices.SubclassToolProficiency);
                }
            }

            Visited = true;
        }

        public void saveContent()
        {
            if (toolComboBox.SelectedItem != null)
            {
                wm.Choices.SubclassToolProficiency = toolComboBox.SelectedItem.ToString();
            }
            else
            {
                wm.Choices.SubclassToolProficiency = "";
            }
        }
    }
}
