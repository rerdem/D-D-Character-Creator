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

namespace Easy_DnD_Character_Creator.WizardComponents.SubComponents
{
    public partial class ExtraSkillControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private ToolTip toolTips;
        private List<CheckBox> skillBoxes;
        private List<CheckBox> choiceBoxes;
        private CheckBox extraSkillCheckBox;

        public ExtraSkillControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            toolTips = new ToolTip();
            skillBoxes = new List<CheckBox>();
            choiceBoxes = new List<CheckBox>();
            extraSkillCheckBox = new CheckBox();
            extraSkillCheckBox.Name = "extraSkillBox"

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
            return "";
        }

        public bool isValid()
        {
            return true;
        }

        public void populateForm()
        {
            //set groupbox text

            Visited = true;
        }

        public void saveContent()
        {
            
        }

        private void resetSkillBoxes()
        {
            choiceBoxes.Clear();
            List<string> knownSkills = new List<string>();
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
                extraSkillLabel.Text = $"Please choose {choiceAmount} additional skill(s):";
                extraSkillLayout.Visible = true;
            }
            else
            {
                extraSkillLayout.Visible = false;
            }
        }

        private void fillExtraSkillLayout()
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

            //add extraSkillBox
            extraSkillCheckBox.Text = "";
            extraSkillCheckBox.CheckedChanged += skillBoxes_CheckedChanged;
            string extraSkillToolTip = wm.DBManager.ExtraClassChoiceData.getExtraSkillTooltip(wm.Choices.Class);
            extraSkillToolTip = Regex.Replace(extraSkillToolTip, "([^ ]+(?: [^ ]+){3}) ", "$1" + Environment.NewLine);
            toolTips.SetToolTip(extraSkillCheckBox, extraSkillToolTip);
            skillBoxes.Add(extraSkillCheckBox);
            extraSkillCheckBox.Visible = false;

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
            refreshExtraSkillBox();

            OnSkillChosen(null);
        }
    }
}
