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
    public partial class BackgroundStoryControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private string lastBackground;

        private BackgroundStoryChoice comboSource;

        public BackgroundStoryControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            lastBackground = "";

            comboSource = new BackgroundStoryChoice();

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
            if (backgroundComboBox.SelectedItem == null)
            {
                return "select a background story choice";
            }

            return "";
        }

        public bool isValid()
        {
            return (backgroundComboBox.SelectedItem != null);
        }

        public void populateForm()
        {
            if (!Visited || hasBackgroundChanged())
            {
                comboSource = wm.DBManager.StoryData.getBackgroundStoryChoice(wm.Choices.Background);
                backgroundComboBox.BeginUpdate();
                backgroundComboBox.DataSource = null;
                backgroundComboBox.DataSource = comboSource.Options;
                backgroundComboBox.EndUpdate();
            }

            if (Visited && !hasBackgroundChanged())
            {
                if (backgroundComboBox.Items.Contains(wm.Choices.BackgroundChoice.getSelectedOption()))
                {
                    backgroundComboBox.SelectedItem = wm.Choices.BackgroundChoice.getSelectedOption();
                }
            }

            Visited = true;
        }

        public void saveContent()
        {
            comboSource.setSelectedOption(backgroundComboBox.SelectedItem.ToString());
            wm.Choices.BackgroundChoice = comboSource;
        }

        private bool hasBackgroundChanged()
        {
            return (lastBackground != wm.Choices.Background);
        }
    }
}
