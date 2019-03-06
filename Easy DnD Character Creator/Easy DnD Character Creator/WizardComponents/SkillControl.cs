using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public partial class SkillControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private string lastCharacterInfo; //subrace, class, subclass, background

        private ToolTip toolTips;
        private List<CheckBox> skillBoxes;
        private List<CheckBox> choiceBoxes;
        private int classSkillCount;

        public event EventHandler SkillChosen;

        public SkillControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            toolTips = new ToolTip();
            skillBoxes = new List<CheckBox>();
            choiceBoxes = new List<CheckBox>();
            classSkillCount = 0;

            InitializeComponent();
            tutorialLabel.Text = "The skills dictated by your choices of race and background have already been applied. " +
                                 "Based on your choice of class you have to choose a number of skills from the presented options. " +
                                 "Hovering over a skill will reveal a description of its application.";
            fillSkillLayout();
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

            //check, if enough skills were selected
            int selectedBoxes = 0;
            foreach (CheckBox box in choiceBoxes)
            {
                if (box.Checked)
                {
                    selectedBoxes++;
                }
            }

            if (selectedBoxes != classSkillCount)
            {
                int missingSkills = classSkillCount - selectedBoxes;
                output += $"select {missingSkills} more skill(s)";
            }

            if (wm.DBManager.SkillData.hasExtraSkillChoice(wm.Choices.Subrace))
            {
                if (extraSkillBox.SelectedItems.Count != wm.DBManager.SkillData.getExtraSkillChoiceAmount(wm.Choices.Subrace))
                {
                    if (!string.IsNullOrEmpty(output))
                    {
                        output += ", ";
                    }
                    int missingChoices = wm.DBManager.SkillData.getExtraSkillChoiceAmount(wm.Choices.Subrace) - extraSkillBox.SelectedItems.Count;
                    output += $"select {missingChoices} more skill(s) from the list on the right side";
                }
            }

            return output;
        }

        public bool isValid()
        {
            //check, if enough skills were selected
            int selectedBoxes = 0;
            foreach (CheckBox box in choiceBoxes)
            {
                if (box.Checked)
                {
                    selectedBoxes++;
                }
            }

            if (wm.DBManager.SkillData.hasExtraSkillChoice(wm.Choices.Subrace))
            {
                if (extraSkillBox.SelectedItems.Count != wm.DBManager.SkillData.getExtraSkillChoiceAmount(wm.Choices.Subrace))
                {
                    return false;
                }
            }

            return (selectedBoxes == classSkillCount);
        }

        public void populateForm()
        {
            if ((!Visited) || hasCharacterInfoChanged())
            {
                //reset to default
                resetSkillBoxes();
            }

            if ((Visited) && !hasCharacterInfoChanged())
            {
                //fill in checkboxes
                foreach (CheckBox box in skillBoxes)
                {
                    if (wm.Choices.Skills.Contains(box.Text))
                    {
                        box.Checked = true;
                    }
                    else
                    {
                        box.Checked = false;
                    }
                }
                toggleChoiceBoxes();

                // if applicable, fill in listbox
                if (wm.DBManager.SkillData.hasExtraSkillChoice(wm.Choices.Subrace))
                {
                    foreach (string skill in wm.Choices.ExtraSkills)
                    {
                        extraSkillBox.SetSelected(extraSkillBox.Items.IndexOf(skill), true);
                    }
                }
            }

            setCharacterInfo();

            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.Skills.Clear();
            wm.Choices.ExtraSkills.Clear();

            foreach (CheckBox box in skillBoxes)
            {
                if (box.Checked)
                {
                    wm.Choices.Skills.Add(box.Text);
                }
            }

            if (wm.DBManager.SkillData.hasExtraSkillChoice(wm.Choices.Subrace))
            {
                foreach (object obj in extraSkillBox.SelectedItems)
                {
                    wm.Choices.ExtraSkills.Add(obj.ToString());
                }
            }
        }

        private void refreshExtraSkillBox()
        {
            extraSkillBox.BeginUpdate();
            extraSkillBox.Items.Clear();
            foreach(CheckBox box in skillBoxes)
            {
                if (!box.Checked)
                {
                    extraSkillBox.Items.Add(box.Text);
                }
            }
            extraSkillBox.EndUpdate();
        }

        private void toggleChoiceBoxes()
        {
            int selectedBoxes = 0;
            foreach (CheckBox box in choiceBoxes)
            {
                if (box.Checked)
                {
                    selectedBoxes++;
                }
            }

            if (selectedBoxes >= classSkillCount)
            {
                foreach (CheckBox box in choiceBoxes)
                {
                    if (!box.Checked)
                    {
                        box.Enabled = false;
                    }
                }
            }
            else
            {
                foreach (CheckBox box in choiceBoxes)
                {
                    box.Enabled = true;
                }
            }
        }

        private void resetSkillBoxes()
        {
            choiceBoxes.Clear();
            classSkillCount = wm.DBManager.SkillData.getClassSkillCount(wm.Choices.Class);
            List<string> knownSkills = wm.DBManager.SkillData.getKnownSkills(wm.Choices.Subrace, wm.Choices.Background);
            List<string> skillOptions = wm.DBManager.SkillData.getClassSkillOptions(wm.Choices.Class);

            foreach (CheckBox box in skillBoxes)
            {
                box.Enabled = false;

                if (knownSkills.Contains(box.Text))
                {
                    box.Checked = true;
                }
                else
                {
                    box.Checked = false;
                }

                if ((skillOptions.Contains(box.Text)) && (!box.Checked))
                {
                    box.Enabled = true;
                    choiceBoxes.Add(box);
                }
            }

            if (wm.DBManager.SkillData.hasExtraSkillChoice(wm.Choices.Subrace))
            {
                int choiceAmount = wm.DBManager.SkillData.getExtraSkillChoiceAmount(wm.Choices.Subrace);
                extraSkillLabel.Text= $"Please choose {choiceAmount} additional skill(s):";
                extraSkillLayout.Visible = true;
            }
            else
            {
                extraSkillLayout.Visible = false;
            }
        }

        private void fillSkillLayout()
        {
            List<string> skills = wm.DBManager.SkillData.getSkills();
            
            //create Checkboxes
            foreach (string skill in skills)
            {
                CheckBox box = new CheckBox();
                box.Name = skill + "Box";
                box.Text = skill;
                box.CheckedChanged += skillBoxes_CheckedChanged;
                string toolTipFormat = wm.DBManager.SkillData.getSkillDescription(skill);
                toolTipFormat = Regex.Replace(toolTipFormat, "([^ ]+(?: [^ ]+){3}) ", "$1" + Environment.NewLine);
                toolTips.SetToolTip(box, toolTipFormat);
                skillBoxes.Add(box);
            }

            //place Checkboxes in table layout
            int boxcounter = 0;
            //for (int i = 0; i < 5; i++)
            for (int i = 0; i < skillLayout.ColumnCount; i++)
            {
                //for (int j = 0; j < 4; j++)
                for (int j = 0; j < skillLayout.RowCount; j++)
                {
                    skillLayout.Controls.Add(skillBoxes.ElementAt(boxcounter), i, j);
                    boxcounter++;
                    if (boxcounter==skillBoxes.Count)
                    {
                        break;
                    }
                }
            }
        }

        private void setCharacterInfo()
        {
            lastCharacterInfo = wm.Choices.Subrace + wm.Choices.Class + wm.Choices.Subclass + wm.Choices.Background;
        }

        private bool hasCharacterInfoChanged()
        {
            string currentCharacterInfo = wm.Choices.Subrace + wm.Choices.Class + wm.Choices.Subclass + wm.Choices.Background;
            return (currentCharacterInfo != lastCharacterInfo);
        }

        private void skillBoxes_CheckedChanged(object sender, EventArgs e)
        {
            toggleChoiceBoxes();
            refreshExtraSkillBox();

            OnSkillChosen(null);
        }

        private void extraSkillBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (extraSkillBox.SelectedItems.Count > wm.DBManager.SkillData.getExtraSkillChoiceAmount(wm.Choices.Subrace))
            {
                extraSkillBox.SelectedItems.Remove(extraSkillBox.SelectedItems[wm.DBManager.SkillData.getExtraSkillChoiceAmount(wm.Choices.Subrace)]);
            }

            OnSkillChosen(null);
        }
        
        private void extraSkillBox_MouseMove(object sender, MouseEventArgs e)
        {
            string tooltip = "";

            //get item under mouse pointer
            int itemIndex = extraSkillBox.IndexFromPoint(e.Location);
            if ((itemIndex >= 0) && (itemIndex < extraSkillBox.Items.Count))
            {
                tooltip = wm.DBManager.SkillData.getSkillDescription(extraSkillBox.Items[itemIndex].ToString());
                tooltip = Regex.Replace(tooltip, "([^ ]+(?: [^ ]+){3}) ", "$1" + Environment.NewLine);
            }

            toolTips.SetToolTip(extraSkillBox, tooltip);
        }

        protected virtual void OnSkillChosen(EventArgs e)
        {
            EventHandler handler = SkillChosen;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
