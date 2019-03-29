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
using Easy_DnD_Character_Creator.DataTypes;

namespace Easy_DnD_Character_Creator.WizardComponents.ExtraClassComponents
{
    public partial class ExtraClassSkillControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private bool doublesProficiency;
        List<Skill> skills;
        private int choiceAmount;
        private string lastClass;

        private ToolTip toolTips;
        private List<CheckBox> skillBoxes;
        private List<CheckBox> choiceBoxes;
        private CheckBox extraSkillCheckBox;

        public event EventHandler SkillChosen;

        public ExtraClassSkillControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            doublesProficiency = false;
            skills = new List<Skill>();
            choiceAmount = 0;
            lastClass = "";

            toolTips = new ToolTip();
            skillBoxes = new List<CheckBox>();
            choiceBoxes = new List<CheckBox>();
            extraSkillCheckBox = new CheckBox();
            extraSkillCheckBox.Name = "extraSkillCheckBox";

            InitializeComponent();

            fillExtraSkillLayout();
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

            int selectedBoxes = 0;
            foreach (CheckBox box in choiceBoxes)
            {
                if (box.Checked)
                {
                    selectedBoxes++;
                }
            }

            if (selectedBoxes < choiceAmount)
            {
                output = $"select {choiceAmount - selectedBoxes} more skill(s)";
            }

            return output;
        }

        public bool isValid()
        {
            int selectedBoxes = 0;
            foreach (CheckBox box in choiceBoxes)
            {
                if (box.Checked)
                {
                    selectedBoxes++;
                }
            }

            return (selectedBoxes == choiceAmount);
        }

        public void populateForm()
        {
            resetSkillBoxes();

            if ((Visited) && (!hasClassChanged()))
            {
                loadPreviousSelection();
            }

            lastClass = wm.Choices.Class;
            Visited = true;
        }

        public void saveContent()
        {
            //save skills
            wm.Choices.ClassSkills.Clear();
            foreach (CheckBox box in choiceBoxes)
            {
                if (box.Checked)
                {
                    wm.Choices.ClassSkills.Add(box.Text);
                }
            }

            //save doubling proficiency
            wm.Choices.ClassDoublesProficiency = doublesProficiency;
        }

        private void loadPreviousSelection()
        {
            foreach (CheckBox box in choiceBoxes)
            {
                if (wm.Choices.ClassSkills.Contains(box.Text))
                {
                    box.Checked = true;
                }
                else
                {
                    box.Checked = false;
                }
            }
        }

        private void resetSkillBoxes()
        {
            choiceBoxes.Clear();
            doublesProficiency = wm.DBManager.ExtraClassChoiceData.ExtraClassSkillData.SkillChoiceDoublesProficiency(wm.Choices.Class, wm.Choices.Level);
            choiceAmount = wm.DBManager.ExtraClassChoiceData.ExtraClassSkillData.getSkillChoiceAmount(wm.Choices.Class, wm.Choices.Level);

            //get choosable skills
            List<string> choosableSkills = new List<string>();
            if (wm.DBManager.ExtraClassChoiceData.ExtraClassSkillData.hasSkillChoiceRestrictions(wm.Choices.Class))
            {
                if (doublesProficiency)
                {
                    choosableSkills = new List<string>(wm.Choices.Skills.Count +
                                                                    wm.Choices.ExtraSkills.Count);
                    choosableSkills.AddRange(wm.Choices.Skills);
                    choosableSkills.AddRange(wm.Choices.ExtraSkills);
                }
                else
                {
                    choosableSkills = new List<string>(skills.Count);
                    choosableSkills.AddRange(skills.Select(skill => skill.Name).ToList());
                }

                List<string> restrictions = wm.DBManager.ExtraClassChoiceData.ExtraClassSkillData.getSkillChoiceRestrictions(wm.Choices.Class);
                choosableSkills.RemoveAll(skill => !restrictions.Exists(restriction => skill == restriction));
            }
            else
            {
                if (doublesProficiency)
                {
                    choosableSkills = new List<string>(wm.Choices.Skills.Count +
                                                                    wm.Choices.ExtraSkills.Count);
                    choosableSkills.AddRange(wm.Choices.Skills);
                    choosableSkills.AddRange(wm.Choices.ExtraSkills);
                }
            }

            //add additional box, if applicable
            if (wm.DBManager.ExtraClassChoiceData.ExtraClassSkillData.hasExtraSkillCheckbox(wm.Choices.Class))
            {
                string extraSkillCheckBoxName = wm.DBManager.ExtraClassChoiceData.ExtraClassSkillData.getExtraSkillCheckbox(wm.Choices.Class);
                choosableSkills.Add(extraSkillCheckBoxName);
                extraSkillCheckBox.Text = extraSkillCheckBoxName;
                //extraSkillBox.Visible = true;
            }
            else
            {
                extraSkillCheckBox.Text = "Class-specific skill";
                //extraSkillBox.Visible = false;
            }

            //enable choosable skill boxes and fill in choiceBoxes
            foreach (CheckBox box in skillBoxes)
            {
                box.Checked = false;

                if (choosableSkills.Contains(box.Text))
                {
                    box.Enabled = true;
                    choiceBoxes.Add(box);
                }
                else
                {
                    box.Enabled = false;
                }
            }

            //set box title
            if (doublesProficiency)
            {
                extraSkillBox.Text = $"{wm.Choices.Class} Expertise";
            }
            else
            {
                extraSkillBox.Text = $"{wm.Choices.Class} Bonus Skills";
            }

            //set label text
            if (doublesProficiency)
            {
                extraSkillLabel.Text = $"Please choose {choiceAmount} of your skill proficiencies. Your proficiency bonus " +
                                       $"is doubled for any ability check you make that uses any of the chosen proficiencies.";
            }
            else
            {
                extraSkillLabel.Text = $"Please choose {choiceAmount} additional skill(s) below:";
            }
        }

        private bool hasClassChanged()
        {
            return (lastClass != wm.Choices.Class);
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

            if (selectedBoxes >= choiceAmount)
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

        private void fillExtraSkillLayout()
        {
            skills = wm.DBManager.SkillData.getSkills();

            //create Checkboxes
            foreach (Skill skill in skills)
            {
                CheckBox box = new CheckBox();
                box.Name = skill.Name + "Box";
                box.Text = skill.Name;
                box.CheckedChanged += skillBoxes_CheckedChanged;
                string toolTipFormat = skill.Description;
                toolTipFormat = Regex.Replace(toolTipFormat, "([^ ]+(?: [^ ]+){3}) ", "$1" + Environment.NewLine);
                toolTips.SetToolTip(box, toolTipFormat);
                skillBoxes.Add(box);
            }

            //add extraSkillBox
            extraSkillCheckBox.Text = "";
            extraSkillCheckBox.CheckedChanged += skillBoxes_CheckedChanged;
            string extraSkillToolTip = wm.DBManager.ExtraClassChoiceData.ExtraClassSkillData.getExtraSkillTooltip(wm.Choices.Class);
            extraSkillToolTip = Regex.Replace(extraSkillToolTip, "([^ ]+(?: [^ ]+){3}) ", "$1" + Environment.NewLine);
            toolTips.SetToolTip(extraSkillCheckBox, extraSkillToolTip);
            skillBoxes.Add(extraSkillCheckBox);
            //extraSkillCheckBox.Visible = false;

            //place Checkboxes in table layout
            int boxcounter = 0;
            //for (int i = 0; i < 5; i++)
            for (int i = 0; i < extraSkillLayout.ColumnCount; i++)
            {
                //for (int j = 0; j < 4; j++)
                for (int j = 0; j < extraSkillLayout.RowCount; j++)
                {
                    extraSkillLayout.Controls.Add(skillBoxes.ElementAt(boxcounter), i, j);
                    boxcounter++;
                    if (boxcounter == skillBoxes.Count)
                    {
                        break;
                    }
                }
            }
        }

        private void skillBoxes_CheckedChanged(object sender, EventArgs e)
        {
            toggleChoiceBoxes();

            OnSkillChosen(null);
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
