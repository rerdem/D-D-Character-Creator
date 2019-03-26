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

        public event EventHandler BackgroundStoryChoiceChosen;

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
            string output = "";

            if (backgroundComboBox.Items.Count > 0)
            {
                if (backgroundComboBox.SelectedItem == null)
                {
                    output = "select a background story choice";
                }
            }

            return output;
        }

        public bool isValid()
        {
            if (backgroundComboBox.Items.Count > 0)
            {
                return (backgroundComboBox.SelectedItem != null);
            }
            else
            {
                return true;
            }
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

                backgroundBox.Text = comboSource.Name;
                introLabel.Text = comboSource.Description;
            }

            if (Visited && !hasBackgroundChanged())
            {
                if (backgroundComboBox.Items.Contains(wm.Choices.BackgroundChoice.getSelectedOption()))
                {
                    backgroundComboBox.SelectedItem = wm.Choices.BackgroundChoice.getSelectedOption();
                }
            }

            lastBackground = wm.Choices.Background;
            Visited = true;
        }

        public void saveContent()
        {
            if ((wm.Choices.HasBackgroundStoryChoice) && (backgroundComboBox.SelectedItem != null))
            {
                comboSource.setSelectedOption(backgroundComboBox.SelectedItem.ToString());
                wm.Choices.BackgroundChoice = comboSource;
            }
            else
            {
                wm.Choices.BackgroundChoice = new BackgroundStoryChoice();
            }
        }

        private bool hasBackgroundChanged()
        {
            return (lastBackground != wm.Choices.Background);
        }

        private void backgroundComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnBackgroundStoryChoiceChosen(null);
        }

        protected virtual void OnBackgroundStoryChoiceChosen(EventArgs e)
        {
            EventHandler handler = BackgroundStoryChoiceChosen;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
