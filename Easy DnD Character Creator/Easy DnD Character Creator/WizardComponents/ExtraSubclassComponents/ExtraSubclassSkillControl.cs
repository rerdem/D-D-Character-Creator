﻿using System;
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

namespace Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents
{
    public partial class ExtraSubclassSkillControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private bool doublesProficiency;
        List<Skill> skills;
        private int choiceAmount;
        private string lastSubclass;

        private ToolTip toolTips;
        private List<CheckBox> skillBoxes;
        private List<CheckBox> choiceBoxes;

        public event EventHandler SkillChosen;

        public ExtraSubclassSkillControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            doublesProficiency = false;
            skills = new List<Skill>();
            choiceAmount = 0;
            lastSubclass = "";

            toolTips = new ToolTip();
            skillBoxes = new List<CheckBox>();
            choiceBoxes = new List<CheckBox>();

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

            if (Visited && !hasSubclassChanged())
            {
                loadPreviousSelection();
            }

            lastSubclass = wm.Choices.ClassChoice.getSelectedSubclass().Name;
            Visited = true;
        }

        public void saveContent()
        {
            //save skills
            wm.Choices.ClassChoice.getSelectedSubclass().ExtraSkills.Clear();
            foreach (CheckBox box in choiceBoxes)
            {
                if (box.Checked)
                {
                    wm.Choices.ClassChoice.getSelectedSubclass().ExtraSkills.Add(box.Text);
                }
            }

            //save doubling proficiency
            wm.Choices.ClassChoice.getSelectedSubclass().DoublesProficiency = doublesProficiency;
        }

        private void loadPreviousSelection()
        {
            foreach (CheckBox box in choiceBoxes)
            {
                if (wm.Choices.ClassChoice.getSelectedSubclass().ExtraSkills.Contains(box.Text))
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
            if (hasSubclassChanged())
            {
                doublesProficiency = wm.DBManager.ExtraSubclassChoiceData.ExtraSubclassSkillData.SkillChoiceDoublesProficiency(wm.Choices.ClassChoice.getSelectedSubclass().Name, wm.Choices.Level);
                choiceAmount = wm.DBManager.ExtraSubclassChoiceData.ExtraSubclassSkillData.getSkillChoiceAmount(wm.Choices.ClassChoice.getSelectedSubclass().Name, wm.Choices.Level);
            }
            
            //get choosable skills
            List<string> choosableSkills = new List<string>();
            choosableSkills = new List<string>(skills.Count);
            choosableSkills.AddRange(skills.Select(skill => skill.Name).ToList());
            choosableSkills.RemoveAll(skill => wm.Choices.Skills.Exists(chosenSkill => skill == chosenSkill));
            choosableSkills.RemoveAll(skill => wm.Choices.ExtraSkills.Exists(chosenSkill => skill == chosenSkill));

            if (wm.DBManager.ExtraSubclassChoiceData.ExtraSubclassSkillData.hasSkillChoiceRestrictions(wm.Choices.ClassChoice.getSelectedSubclass().Name))
            {
                List<string> restrictions = wm.DBManager.ExtraSubclassChoiceData.ExtraSubclassSkillData.getSkillChoiceRestrictions(wm.Choices.ClassChoice.getSelectedSubclass().Name);
                choosableSkills.RemoveAll(skill => !restrictions.Exists(restriction => skill == restriction));
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
            //set box title
            if (doublesProficiency)
            {
                extraSkillBox.Text = $"{wm.Choices.ClassChoice.getSelectedSubclass().Name} Expertise";
            }
            else
            {
                extraSkillBox.Text = $"{wm.Choices.ClassChoice.getSelectedSubclass().Name} Bonus Skills";
            }

            //set label text
            if (doublesProficiency)
            {
                extraSkillLabel.Text = $"Please choose {choiceAmount} skill(s). Your proficiency bonus is doubled " +
                                       $"for any ability check you make that uses any of the chosen proficiencies.";
            }
            else
            {
                extraSkillLabel.Text = $"Please choose {choiceAmount} additional skill(s) below:";
            }
        }

        private bool hasSubclassChanged()
        {
            return (lastSubclass != wm.Choices.ClassChoice.getSelectedSubclass().Name);
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
