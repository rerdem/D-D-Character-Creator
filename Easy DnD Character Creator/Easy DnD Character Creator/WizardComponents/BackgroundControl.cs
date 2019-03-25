using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public partial class BackgroundControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        public BackgroundControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;
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

            if ((backgroundListBox.SelectedItem == null) || (backgroundListBox.SelectedItems.Count != 1))
            {
                output += backgroundBox.Text;
            }
            else
            {
                if (wm.DBManager.BackgroundData.backgroundHasExtraChoice(backgroundListBox.SelectedItem.ToString()))
                {
                    if (!string.IsNullOrEmpty(output))
                    {
                        output += ", ";
                    }
                    output += "extra background tool proficiency";
                }
            }

            return output;
        }

        public bool isValid()
        {
            if (backgroundListBox.SelectedItem != null)
            {
                if (wm.DBManager.BackgroundData.backgroundHasExtraChoice(backgroundListBox.SelectedItem.ToString()))
                {
                    return ((backgroundListBox.SelectedItems.Count == 1) && (extraProficiencyBox.SelectedItems.Count == 1));
                }

                return (backgroundListBox.SelectedItems.Count == 1);
            }

            return false;
        }

        public void populateForm()
        {
            fillBackgroundList();

            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.Background = backgroundListBox.SelectedItem.ToString();
            wm.Choices.HasBackgroundStoryChoice = wm.DBManager.StoryData.hasBackgroundStoryChoice(backgroundListBox.SelectedItem.ToString());

            if (wm.DBManager.BackgroundData.backgroundHasExtraChoice(backgroundListBox.SelectedItem.ToString()))
            {
                wm.Choices.BackgroundProficiency = extraProficiencyBox.SelectedItem.ToString();
            }
            else
            {
                wm.Choices.BackgroundProficiency = "";
            }
        }

        private void fillBackgroundList()
        {
            List<string> backgrounds = wm.DBManager.BackgroundData.getBackgrounds();

            backgroundListBox.BeginUpdate();
            backgroundListBox.Items.Clear();
            foreach (string entry in backgrounds)
            {
                backgroundListBox.Items.Add(entry);
            }
            backgroundListBox.EndUpdate();

            if (backgroundListBox.Items.Contains(wm.Choices.Background))
            {
                backgroundListBox.SetSelected(backgroundListBox.Items.IndexOf(wm.Choices.Background), true);
            }
            else
            {
                backgroundListBox.SetSelected(0, true);
            }
        }

        private void fillExtraChoiceListBox(string backgroundChoice)
        {
            List<string> choiceList = wm.DBManager.BackgroundData.getExtraBackgroundProficiencies(backgroundListBox.SelectedItem.ToString());

            extraProficiencyBox.BeginUpdate();
            extraProficiencyBox.Items.Clear();
            foreach (string entry in choiceList)
            {
                extraProficiencyBox.Items.Add(entry);
            }
            extraProficiencyBox.EndUpdate();

            if (extraProficiencyBox.Items.Contains(wm.Choices.BackgroundProficiency))
            {
                extraProficiencyBox.SetSelected(extraProficiencyBox.Items.IndexOf(wm.Choices.BackgroundProficiency), true);
            }
            else
            {
                extraProficiencyBox.SetSelected(0, true);
            }
        }

        private void backgroundListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            backgroundDescription.Text = wm.DBManager.BackgroundData.getBackgroundDescription(backgroundListBox.SelectedItem.ToString());
            
            if (wm.DBManager.BackgroundData.backgroundHasExtraChoice(backgroundListBox.SelectedItem.ToString()))
            {
                backgroundDescription.MaximumSize = new Size(520, backgroundDescription.MaximumSize.Height);
                extraProficiencyLayout.Visible = true;
                fillExtraChoiceListBox(backgroundListBox.SelectedItem.ToString());
            }
            else
            {
                backgroundDescription.MaximumSize = new Size(650, backgroundDescription.MaximumSize.Height);
                extraProficiencyLayout.Visible = false;
            }

            saveContent();
        }
    }
}
