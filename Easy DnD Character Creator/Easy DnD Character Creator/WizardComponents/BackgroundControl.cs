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
                Background currentBackground = backgroundListBox.SelectedItem as Background;
                if (currentBackground != null)
                {
                    if (currentBackground.HasProficiencyChoice)
                    {
                        if (!string.IsNullOrEmpty(output))
                        {
                            output += ", ";
                        }
                        output += "extra background tool proficiency";
                    }
                }
            }

            return output;
        }

        public bool isValid()
        {
            if (backgroundListBox.SelectedItem != null)
            {
                Background currentBackground = backgroundListBox.SelectedItem as Background;
                if (currentBackground != null)
                {
                    if (currentBackground.HasProficiencyChoice)
                    {
                        return ((backgroundListBox.SelectedItems.Count == 1) && (extraProficiencyBox.SelectedItems.Count == 1));
                    }
                }

                return (backgroundListBox.SelectedItems.Count == 1);
            }

            return false;
        }

        public void populateForm()
        {
            fillBackgroundList();

            if (Visited)
            {
                loadPreviousSelections();
            }

            Visited = true;
        }

        public void saveContent()
        {
            Background currentBackground = backgroundListBox.SelectedItem as Background;
            if (currentBackground != null)
            {
                if (currentBackground.HasProficiencyChoice)
                {
                    currentBackground.Proficiency = extraProficiencyBox.SelectedItem.ToString();
                }
                else
                {
                    currentBackground.Proficiency = "";
                }
                wm.Choices.BackgroundChoice = currentBackground;
            }
        }

        private void fillBackgroundList()
        {
            List<Background> backgrounds = wm.DBManager.BackgroundData.getBackgrounds();

            backgroundListBox.BeginUpdate();
            backgroundListBox.DataSource = null;
            backgroundListBox.DataSource = backgrounds;
            backgroundListBox.DisplayMember = "Name";
            backgroundListBox.EndUpdate();
        }

        private void fillExtraChoiceListBox(string backgroundChoice)
        {
            List<string> choiceList = wm.DBManager.BackgroundData.getExtraBackgroundProficiencies(backgroundChoice);
            choiceList.Remove(wm.Choices.RaceChoice.getSelectedSubrace().Proficiency);

            extraProficiencyBox.BeginUpdate();
            extraProficiencyBox.DataSource = null;
            extraProficiencyBox.DataSource = choiceList;
            extraProficiencyBox.EndUpdate();
        }

        private void loadPreviousSelections()
        {
            //background
            if (backgroundListBox.Items.Contains(wm.Choices.BackgroundChoice))
            {
                backgroundListBox.SetSelected(backgroundListBox.Items.IndexOf(wm.Choices.BackgroundChoice), true);
            }

            //background tool proficiency
            if (extraProficiencyBox.Items.Contains(wm.Choices.BackgroundChoice.Proficiency))
            {
                extraProficiencyBox.SetSelected(extraProficiencyBox.Items.IndexOf(wm.Choices.BackgroundChoice.Proficiency), true);
            }
        }

        private void backgroundListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Background currentBackground = backgroundListBox.SelectedItem as Background;
            if (currentBackground != null)
            {
                backgroundDescription.Text = currentBackground.Description;

                if (currentBackground.HasProficiencyChoice)
                {
                    backgroundDescription.MaximumSize = new Size(520, backgroundDescription.MaximumSize.Height);
                    extraProficiencyLayout.Visible = true;
                    fillExtraChoiceListBox(currentBackground.Name);
                }
                else
                {
                    backgroundDescription.MaximumSize = new Size(650, backgroundDescription.MaximumSize.Height);
                    extraProficiencyLayout.Visible = false;
                }
            }
        }
    }
}
