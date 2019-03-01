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
    public partial class AbilityControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private ToolTip toolTips;

        private CheckBox[] bonusCheckBoxes;
        private Label[] abilityScores;
        private FlowLayoutPanel[] dropzones;

        private int LastPreset { get; set; }
        private int LastLevel { get; set; }
        private int SubraceBonusCounter { get; set; }

        public event EventHandler AbilityAssigned;
        public event EventHandler AbilityBonusAssigned;

        public AbilityControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            visited = false;

            toolTips = new ToolTip();
            
            LastPreset = 0;
            LastLevel = 0;
            SubraceBonusCounter = 0;

            InitializeComponent();
            initializeHoldingArrays();
            initializeToolTips();
            tutorialLabel.Text = "You can assign each value to an ability via drag & drop. " +
                "Each value can only be assigned once. Hovering above each ability name will " +
                "show you a short description of it. Below each ability are any score increases " +
                "granted by your choice of race. At the bottom you can see a preview of what your " +
                "final score (and modifier that is added to your rolls) will be. Additionally, you " +
                "will see the health value of your character, which is dependent on your character's " +
                "level and constitution.";
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
            //check, of all values have been assigned
            foreach (Control control in abilityScoreHoldingLayout.Controls)
            {
                if (control is Label)
                {
                    output += "not all ability scores assigned";
                    break;
                }
            }

            if (wm.DBManager.AbilityData.subraceHasAbilityChoice(wm.Choices.Subrace))
            {
                if (SubraceBonusCounter != wm.DBManager.AbilityData.subraceAbilityChoiceAmount(wm.Choices.Subrace))
                {
                    if (!string.IsNullOrEmpty(output))
                    {
                        output += ", ";
                    }

                    output += "not all subrace increases chosen";
                }

            }

            return output;
        }

        public bool isValid()
        {
            //check, if all values have been assigned
            foreach (Control control in abilityScoreHoldingLayout.Controls)
            {
                if (control is Label)
                {
                    return false;
                }
            }

            //check, if subrace bonus choices have been chosen
            if (wm.DBManager.AbilityData.subraceHasAbilityChoice(wm.Choices.Subrace))
            {
                return (SubraceBonusCounter == wm.DBManager.AbilityData.subraceAbilityChoiceAmount(wm.Choices.Subrace));
            }

            return true;
        }

        public void populateForm()
        {
            //roll values, reroll only when preset changed
            if ((!visited) || hasPresetChanged())
            {
                if (wm.Choices.Preset == 1)
                {
                    List<string> scoreList = wm.DBManager.AbilityData.getAverageAbilityScores();
                    for (int i = 0; i < abilityScores.Length; i++)
                    {
                        abilityScores[i].Text = scoreList.ElementAt(i).ToString();
                    }
                }
                else
                {
                    foreach (Label choice in abilityScores)
                    {
                        choice.Text = rollAbility().ToString();
                    }
                }

                LastPreset = wm.Choices.Preset;
            }

            //reset current assignments
            resetAbilityAssignments();

            //import saved ability scores
            importSavedAbilityScores();

            //fill ability recommendation
            recommendationLabel.Text = wm.DBManager.AbilityData.getAbilityRecommendation(wm.Choices.Class);

            //populate ability score bonus from subrace
            strBonusLabel.Text = "+" + wm.DBManager.AbilityData.getAbilityScoreBonus(wm.Choices.Subrace, "Strength");
            dexBonusLabel.Text = "+" + wm.DBManager.AbilityData.getAbilityScoreBonus(wm.Choices.Subrace, "Dexterity");
            conBonusLabel.Text = "+" + wm.DBManager.AbilityData.getAbilityScoreBonus(wm.Choices.Subrace, "Constitution");
            intBonusLabel.Text = "+" + wm.DBManager.AbilityData.getAbilityScoreBonus(wm.Choices.Subrace, "Intelligence");
            wisBonusLabel.Text = "+" + wm.DBManager.AbilityData.getAbilityScoreBonus(wm.Choices.Subrace, "Wisdom");
            chaBonusLabel.Text = "+" + wm.DBManager.AbilityData.getAbilityScoreBonus(wm.Choices.Subrace, "Charisma");

            //populate ability choices from subrace
            toggleScoreBonusChoices();
            importSavedBonusChoices();

            //toggle reroll button
            if (wm.Choices.Preset == 0)
            {
                rerollButton.Visible = true;
            }
            else
            {
                rerollButton.Visible = false;
            }

            Visited = true;
        }

        private void importSavedBonusChoices()
        {
            SubraceBonusCounter = 0;

            if (wm.DBManager.AbilityData.subraceHasAbilityChoice(wm.Choices.Subrace))
            {
                //strength
                if (wm.Choices.Strength.SubraceBonus > 0)
                {
                    strBonusCheck.Checked = true;
                }

                //dexterity
                if (wm.Choices.Dexterity.SubraceBonus > 0)
                {
                    dexBonusCheck.Checked = true;
                }

                //constitution
                if (wm.Choices.Constitution.SubraceBonus > 0)
                {
                    conBonusCheck.Checked = true;
                }

                //intelligence
                if (wm.Choices.Intelligence.SubraceBonus > 0)
                {
                    intBonusCheck.Checked = true;
                }

                //wisdom
                if (wm.Choices.Wisdom.SubraceBonus > 0)
                {
                    wisBonusCheck.Checked = true;
                }

                //charisma
                if (wm.Choices.Charisma.SubraceBonus > 0)
                {
                    chaBonusCheck.Checked = true;
                }
            }
        }

        private void importSavedAbilityScores()
        {
            //strength
            if (wm.Choices.Strength.BaseValue > 0)
            {
                foreach (Label score in abilityScores)
                {
                    if ((score.Text == wm.Choices.Strength.BaseValue.ToString()) && (score.Parent == abilityScoreHoldingLayout))
                    {
                        score.Parent = strDropzone;
                        break;
                    }
                }
            }

            //dexterity
            if (wm.Choices.Dexterity.BaseValue > 0)
            {
                foreach (Label score in abilityScores)
                {
                    if ((score.Text == wm.Choices.Dexterity.BaseValue.ToString()) && (score.Parent == abilityScoreHoldingLayout))
                    {
                        score.Parent = dexDropzone;
                        break;
                    }
                }
            }

            //constitution
            if (wm.Choices.Constitution.BaseValue > 0)
            {
                foreach (Label score in abilityScores)
                {
                    if ((score.Text == wm.Choices.Constitution.BaseValue.ToString()) && (score.Parent == abilityScoreHoldingLayout))
                    {
                        score.Parent = conDropzone;
                        if (hasLevelChanged())
                        {
                            calculateHealth();
                            LastLevel = wm.Choices.Level;
                        }
                        break;
                    }
                }
            }

            //intelligence
            if (wm.Choices.Intelligence.BaseValue > 0)
            {
                foreach (Label score in abilityScores)
                {
                    if ((score.Text == wm.Choices.Intelligence.BaseValue.ToString()) && (score.Parent == abilityScoreHoldingLayout))
                    {
                        score.Parent = intDropzone;
                        break;
                    }
                }
            }

            //wisdom
            if (wm.Choices.Wisdom.BaseValue > 0)
            {
                foreach (Label score in abilityScores)
                {
                    if ((score.Text == wm.Choices.Wisdom.BaseValue.ToString()) && (score.Parent == abilityScoreHoldingLayout))
                    {
                        score.Parent = wisDropzone;
                        break;
                    }
                }
            }

            //charisma
            if (wm.Choices.Charisma.BaseValue > 0)
            {
                foreach (Label score in abilityScores)
                {
                    if ((score.Text == wm.Choices.Charisma.BaseValue.ToString()) && (score.Parent == abilityScoreHoldingLayout))
                    {
                        score.Parent = chaDropzone;
                        break;
                    }
                }
            }
        }

        public void saveContent()
        {
            //value of each ability dropzone
            //reset old values
            wm.Choices.Strength.BaseValue = 0;
            wm.Choices.Dexterity.BaseValue = 0;
            wm.Choices.Constitution.BaseValue = 0;
            wm.Choices.Intelligence.BaseValue = 0;
            wm.Choices.Wisdom.BaseValue = 0;
            wm.Choices.Charisma.BaseValue = 0;

            //strength
            foreach (Control control in strDropzone.Controls)
            {
                if (control is Label)
                {
                    wm.Choices.Strength.BaseValue = int.Parse(control.Text);
                }
            }

            //dexterity
            foreach (Control control in dexDropzone.Controls)
            {
                if (control is Label)
                {
                    wm.Choices.Dexterity.BaseValue = int.Parse(control.Text);
                }
            }

            //constitution
            foreach (Control control in conDropzone.Controls)
            {
                if (control is Label)
                {
                    wm.Choices.Constitution.BaseValue = int.Parse(control.Text);
                }
            }

            //intelligence
            foreach (Control control in intDropzone.Controls)
            {
                if (control is Label)
                {
                    wm.Choices.Intelligence.BaseValue = int.Parse(control.Text);
                }
            }

            //wisdom
            foreach (Control control in wisDropzone.Controls)
            {
                if (control is Label)
                {
                    wm.Choices.Wisdom.BaseValue = int.Parse(control.Text);
                }
            }

            //charisma
            foreach (Control control in chaDropzone.Controls)
            {
                if (control is Label)
                {
                    wm.Choices.Charisma.BaseValue = int.Parse(control.Text);
                }
            }

            //subrace additions
            wm.Choices.Strength.SubraceAdd = int.Parse(strBonusLabel.Text.Substring(1));
            wm.Choices.Dexterity.SubraceAdd = int.Parse(dexBonusLabel.Text.Substring(1));
            wm.Choices.Constitution.SubraceAdd = int.Parse(conBonusLabel.Text.Substring(1));
            wm.Choices.Intelligence.SubraceAdd = int.Parse(intBonusLabel.Text.Substring(1));
            wm.Choices.Wisdom.SubraceAdd = int.Parse(wisBonusLabel.Text.Substring(1));
            wm.Choices.Charisma.SubraceAdd = int.Parse(chaBonusLabel.Text.Substring(1));

            //subrace choices
            if (wm.DBManager.AbilityData.subraceHasAbilityChoice(wm.Choices.Subrace))
            {
                //strength
                if (strBonusCheck.Checked)
                {
                    wm.Choices.Strength.SubraceBonus = 1;
                }
                else
                {
                    wm.Choices.Strength.SubraceBonus = 0;
                }

                //dexterity
                if (dexBonusCheck.Checked)
                {
                    wm.Choices.Dexterity.SubraceBonus = 1;
                }
                else
                {
                    wm.Choices.Dexterity.SubraceBonus = 0;
                }

                //constitution
                if (conBonusCheck.Checked)
                {
                    wm.Choices.Constitution.SubraceBonus = 1;
                }
                else
                {
                    wm.Choices.Constitution.SubraceBonus = 0;
                }

                //intelligence
                if (intBonusCheck.Checked)
                {
                    wm.Choices.Intelligence.SubraceBonus = 1;
                }
                else
                {
                    wm.Choices.Intelligence.SubraceBonus = 0;
                }

                //wisdom
                if (wisBonusCheck.Checked)
                {
                    wm.Choices.Wisdom.SubraceBonus = 1;
                }
                else
                {
                    wm.Choices.Wisdom.SubraceBonus = 0;
                }

                //charisma
                if (chaBonusCheck.Checked)
                {
                    wm.Choices.Charisma.SubraceBonus = 1;
                }
                else
                {
                    wm.Choices.Charisma.SubraceBonus = 0;
                }
            }
            else
            {
                wm.Choices.Strength.SubraceBonus = 0;
                wm.Choices.Dexterity.SubraceBonus = 0;
                wm.Choices.Constitution.SubraceBonus = 0;
                wm.Choices.Intelligence.SubraceBonus = 0;
                wm.Choices.Wisdom.SubraceBonus = 0;
                wm.Choices.Charisma.SubraceBonus = 0;
            }

        }

        public void refreshResults()
        {
            //strength
            int strength = 0;

            foreach (Control control in strDropzone.Controls)
            {
                if (control is Label)
                {
                    strength = int.Parse(control.Text);
                    break;
                }
            }
            if (strength > 0)
            {
                strength += int.Parse(strBonusLabel.Text.Substring(1));

                if ((wm.DBManager.AbilityData.subraceHasAbilityChoice(wm.Choices.Subrace)) && (strBonusCheck.Checked))
                {
                    strength++;
                }

                int strModifier = calculateAbilityModifier(strength);
                if (strModifier >= 0)
                {
                    strResultLabel.Text = strength.ToString() + " (+" + strModifier.ToString() + ")";
                }
                else
                {
                    strResultLabel.Text = strength.ToString() + " (" + strModifier.ToString() + ")";
                }
            }
            else
            {
                strResultLabel.Text = "0 (+0)";
            }
            

            //dexterity
            int dexterity = 0;

            foreach (Control control in dexDropzone.Controls)
            {
                if (control is Label)
                {
                    dexterity = int.Parse(control.Text);
                    break;
                }
            }

            if (dexterity > 0)
            {
                dexterity += int.Parse(dexBonusLabel.Text.Substring(1));

                if ((wm.DBManager.AbilityData.subraceHasAbilityChoice(wm.Choices.Subrace)) && (dexBonusCheck.Checked))
                {
                    dexterity++;
                }

                int dexModifier = calculateAbilityModifier(dexterity);
                if (dexModifier >= 0)
                {
                    dexResultLabel.Text = dexterity.ToString() + " (+" + dexModifier.ToString() + ")";
                }
                else
                {
                    dexResultLabel.Text = dexterity.ToString() + " (" + dexModifier.ToString() + ")";
                }
            }
            else
            {
                dexResultLabel.Text = "0 (+0)";
            }


            //constitution
            int constitution = 0;

            foreach (Control control in conDropzone.Controls)
            {
                if (control is Label)
                {
                    constitution = int.Parse(control.Text);
                    break;
                }
            }

            if (constitution > 0)
            {
                constitution += int.Parse(conBonusLabel.Text.Substring(1));

                if ((wm.DBManager.AbilityData.subraceHasAbilityChoice(wm.Choices.Subrace)) && (conBonusCheck.Checked))
                {
                    constitution++;
                }

                int conModifier = calculateAbilityModifier(constitution);
                if (conModifier >= 0)
                {
                    conResultLabel.Text = constitution.ToString() + " (+" + conModifier.ToString() + ")";
                }
                else
                {
                    conResultLabel.Text = constitution.ToString() + " (" + conModifier.ToString() + ")";
                }
            }
            else
            {
                conResultLabel.Text = "0 (+0)";
            }
            

            //intelligence
            int intelligence = 0;

            foreach (Control control in intDropzone.Controls)
            {
                if (control is Label)
                {
                    intelligence = int.Parse(control.Text);
                    break;
                }
            }

            if (intelligence > 0)
            {
                intelligence += int.Parse(intBonusLabel.Text.Substring(1));

                if ((wm.DBManager.AbilityData.subraceHasAbilityChoice(wm.Choices.Subrace)) && (intBonusCheck.Checked))
                {
                    intelligence++;
                }

                int intModifier = calculateAbilityModifier(intelligence);
                if (intModifier >= 0)
                {
                    intResultLabel.Text = intelligence.ToString() + " (+" + intModifier.ToString() + ")";
                }
                else
                {
                    intResultLabel.Text = intelligence.ToString() + " (" + intModifier.ToString() + ")";
                }
            }
            else
            {
                intResultLabel.Text = "0 +(0)";
            }


            //wisdom
            int wisdom = 0;

            foreach (Control control in wisDropzone.Controls)
            {
                if (control is Label)
                {
                    wisdom = int.Parse(control.Text);
                    break;
                }
            }

            if (wisdom > 0)
            {
                wisdom += int.Parse(wisBonusLabel.Text.Substring(1));

                if ((wm.DBManager.AbilityData.subraceHasAbilityChoice(wm.Choices.Subrace)) && (wisBonusCheck.Checked))
                {
                    wisdom++;
                }

                int wisModifier = calculateAbilityModifier(wisdom);
                if (wisModifier >= 0)
                {
                    wisResultLabel.Text = wisdom.ToString() + " (+" + wisModifier.ToString() + ")";
                }
                else
                {
                    wisResultLabel.Text = wisdom.ToString() + " (" + wisModifier.ToString() + ")";
                }
            }
            else
            {
                wisResultLabel.Text = "0 (+0)";
            }


            //charisma
            int charisma = 0;

            foreach (Control control in chaDropzone.Controls)
            {
                if (control is Label)
                {
                    charisma = int.Parse(control.Text);
                    break;
                }
            }

            if (charisma > 0)
            {
                charisma += int.Parse(chaBonusLabel.Text.Substring(1));

                if ((wm.DBManager.AbilityData.subraceHasAbilityChoice(wm.Choices.Subrace)) && (chaBonusCheck.Checked))
                {
                    charisma++;
                }

                int chaModifier = calculateAbilityModifier(charisma);
                if (chaModifier >= 0)
                {
                    chaResultLabel.Text = charisma.ToString() + " (+" + chaModifier.ToString() + ")";
                }
                else
                {
                    chaResultLabel.Text = charisma.ToString() + " (" + chaModifier.ToString() + ")";
                }
            }
            else
            {
                chaResultLabel.Text = "0 (+0)";
            }
        }

        public void calculateHealth()
        {
            int health = 0;
            int constitution = 0;

            foreach (Control control in conDropzone.Controls)
            {
                if (control is Label)
                {
                    constitution = int.Parse(control.Text);
                    break;
                }
            }

            if (constitution > 0)
            {
                constitution += int.Parse(conBonusLabel.Text.Substring(1));

                if ((wm.DBManager.AbilityData.subraceHasAbilityChoice(wm.Choices.Subrace)) && (conBonusCheck.Checked))
                {
                    constitution++;
                }

                int conModifier = calculateAbilityModifier(constitution);

                //hp at level 1
                health = wm.DBManager.AbilityData.getMaximumHitDieResult(wm.Choices.Class) + conModifier;

                //separate check for subrace and subclass because bonus stacks
                if (wm.DBManager.AbilityData.subraceHasBonusHP(wm.Choices.Subrace))
                {
                    health++;
                }

                if (wm.DBManager.AbilityData.subclassHasBonusHP(wm.Choices.Subclass))
                {
                    health++;
                }

                //each level above 1
                //average: average die result + constitution modifier per level
                //lenient: die roll + constitution modifier per level, die roll can't be below average
                //purist: die roll + constitution modifier per level
                for (int i = wm.Choices.Level - 1; i > 0; i--)
                {
                    if (wm.Choices.Preset == 1)
                    {
                        health += wm.DBManager.AbilityData.getAverageHitDieResult(wm.Choices.Class);
                    }
                    else
                    {
                        int dieRoll = wm.getRandomNumber(1, wm.DBManager.AbilityData.getMaximumHitDieResult(wm.Choices.Class) + 1);
                        if ((wm.Choices.Preset == 0) && (dieRoll < wm.DBManager.AbilityData.getAverageHitDieResult(wm.Choices.Class)))
                        {
                            dieRoll = wm.DBManager.AbilityData.getAverageHitDieResult(wm.Choices.Class);
                        }
                        health += dieRoll;
                    }

                    health += conModifier;

                    //separate check for subrace and subclass because bonus stacks
                    if (wm.DBManager.AbilityData.subraceHasBonusHP(wm.Choices.Subrace))
                    {
                        health++;
                    }

                    if (wm.DBManager.AbilityData.subclassHasBonusHP(wm.Choices.Subclass))
                    {
                        health++;
                    }
                }

                healthBox.Text = health.ToString();
            }
            else
            {
                healthBox.Text = health.ToString();
            }
        }

        /// <summary>
        /// rolls an ability score rolling 4d6 and discarding the lowest result
        /// </summary>
        private int rollAbility()
        {
            int ability = 0;

            //roll 4d6 and discard the lowest
            int smallest = wm.getRandomNumber(1, 7);
            for (int i = 0; i < 3; i++)
            {
                int newest = wm.getRandomNumber(1, 7);
                if (newest > smallest)
                {
                    ability += newest;
                }
                else
                {
                    ability += smallest;
                    smallest = newest;
                }
            }

            return ability;
        }

        private void rerollLowAbilityScores()
        {
            foreach (Label score in abilityScores)
            {
                if (int.Parse(score.Text) < 8)
                {
                    int roll = 0;
                    while (roll < 8)
                    {
                        roll = rollAbility();
                    }
                    score.Text = roll.ToString();
                }
            }
        }

        private int calculateAbilityModifier(int abilityScore)
        {
            return (int)Math.Floor((abilityScore - 10.0) / 2.0);
        }

        private bool hasPresetChanged()
        {
            return (wm.Choices.Preset != LastPreset);
        }

        private bool hasLevelChanged()
        {
            return (wm.Choices.Level != LastLevel);
        }

        private void toggleScoreBonusChoices()
        {
            if (wm.DBManager.AbilityData.subraceHasAbilityChoice(wm.Choices.Subrace))
            {
                raceAbilityBonusSeparatorLabel.Visible = true;
                raceAbilityBonusLabel.Visible = true;

                foreach (CheckBox box in bonusCheckBoxes)
                {
                    box.Visible = true;
                }
            }
            else
            {
                raceAbilityBonusSeparatorLabel.Visible = false;
                raceAbilityBonusLabel.Visible = false;

                foreach (CheckBox box in bonusCheckBoxes)
                {
                    box.Visible = false;
                }
            }
        }

        private void toggleScoreBonusSelectability()
        {
            if (SubraceBonusCounter == wm.DBManager.AbilityData.subraceAbilityChoiceAmount(wm.Choices.Subrace))
            {
                foreach (CheckBox box in bonusCheckBoxes)
                {
                    if (!box.Checked)
                    {
                        box.Enabled = false;
                    }
                }
            }
            else
            {
                foreach (CheckBox box in bonusCheckBoxes)
                {
                    box.Enabled = true;
                }
            }
        }

        private void initializeToolTips()
        {
            //get ability score description and replace every 4th space with a new line
            string toolTipFormat = wm.DBManager.AbilityData.getAbilityDescription("Strength");
            toolTipFormat = Regex.Replace(toolTipFormat, "([^ ]+(?: [^ ]+){3}) ", "$1" + Environment.NewLine);
            toolTips.SetToolTip(strLabel, toolTipFormat);

            toolTipFormat = wm.DBManager.AbilityData.getAbilityDescription("Dexterity");
            toolTipFormat = Regex.Replace(toolTipFormat, "([^ ]+(?: [^ ]+){3}) ", "$1" + Environment.NewLine);
            toolTips.SetToolTip(dexLabel, toolTipFormat);

            toolTipFormat = wm.DBManager.AbilityData.getAbilityDescription("Constitution");
            toolTipFormat = Regex.Replace(toolTipFormat, "([^ ]+(?: [^ ]+){3}) ", "$1" + Environment.NewLine);
            toolTips.SetToolTip(conLabel, toolTipFormat);

            toolTipFormat = wm.DBManager.AbilityData.getAbilityDescription("Intelligence");
            toolTipFormat = Regex.Replace(toolTipFormat, "([^ ]+(?: [^ ]+){3}) ", "$1" + Environment.NewLine);
            toolTips.SetToolTip(intLabel, toolTipFormat);

            toolTipFormat = wm.DBManager.AbilityData.getAbilityDescription("Wisdom");
            toolTipFormat = Regex.Replace(toolTipFormat, "([^ ]+(?: [^ ]+){3}) ", "$1" + Environment.NewLine);
            toolTips.SetToolTip(wisLabel, toolTipFormat);

            toolTipFormat = wm.DBManager.AbilityData.getAbilityDescription("Charisma");
            toolTipFormat = Regex.Replace(toolTipFormat, "([^ ]+(?: [^ ]+){3}) ", "$1" + Environment.NewLine);
            toolTips.SetToolTip(chaLabel, toolTipFormat);
        }

        private void initializeHoldingArrays()
        {
            bonusCheckBoxes = new CheckBox[6];
            bonusCheckBoxes[0] = strBonusCheck;
            bonusCheckBoxes[1] = dexBonusCheck;
            bonusCheckBoxes[2] = conBonusCheck;
            bonusCheckBoxes[3] = intBonusCheck;
            bonusCheckBoxes[4] = wisBonusCheck;
            bonusCheckBoxes[5] = chaBonusCheck;

            abilityScores = new Label[6];
            abilityScores[0] = abilityScore1;
            abilityScores[1] = abilityScore2;
            abilityScores[2] = abilityScore3;
            abilityScores[3] = abilityScore4;
            abilityScores[4] = abilityScore5;
            abilityScores[5] = abilityScore6;

            dropzones = new FlowLayoutPanel[6];
            dropzones[0] = strDropzone;
            dropzones[1] = dexDropzone;
            dropzones[2] = conDropzone;
            dropzones[3] = intDropzone;
            dropzones[4] = wisDropzone;
            dropzones[5] = chaDropzone;
        }

        private void resetAbilityAssignments()
        {
            foreach (Label ability in abilityScores)
            {
                ability.Parent = abilityScoreHoldingLayout;
            }

            foreach (CheckBox abilityBonus in bonusCheckBoxes)
            {
                abilityBonus.Checked = false;
            }
        }

        private void abilityScoreHoldingLayout_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void abilityScoreHoldingLayout_DragDrop(object sender, DragEventArgs e)
        {
            ((Label)e.Data.GetData(typeof(Label))).Parent = (Panel)sender;

            refreshResults();

            OnAbilityAssigned(null);
        }

        private void dropzone_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dropzone_DragDrop(object sender, DragEventArgs e)
        {
            bool isEmpty = true;

            Panel dropPanel = (Panel)sender;

            foreach (Control control in dropPanel.Controls)
            {
                if (control is Label)
                {
                    isEmpty = false;
                }
            }

            if (isEmpty)
            {
                ((Label)e.Data.GetData(typeof(Label))).Parent = dropPanel;

                refreshResults();

                if (dropPanel.Name == "conDropzone")
                {
                    calculateHealth();
                }

                OnAbilityAssigned(null);
            }
        }

        private void abilityScore1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                abilityScore1.DoDragDrop(abilityScore1, DragDropEffects.Move);
            }
        }

        private void abilityScore2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                abilityScore2.DoDragDrop(abilityScore2, DragDropEffects.Move);
            }
        }

        private void abilityScore3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                abilityScore3.DoDragDrop(abilityScore3, DragDropEffects.Move);
            }
        }

        private void abilityScore4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                abilityScore4.DoDragDrop(abilityScore4, DragDropEffects.Move);
            }
        }

        private void abilityScore5_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                abilityScore5.DoDragDrop(abilityScore5, DragDropEffects.Move);
            }
        }

        private void abilityScore6_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                abilityScore6.DoDragDrop(abilityScore6, DragDropEffects.Move);
            }
        }

        protected virtual void OnAbilityAssigned(EventArgs e)
        {
            EventHandler handler = AbilityAssigned;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnAbilityBonusAssigned(EventArgs e)
        {
            EventHandler handler = AbilityBonusAssigned;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void strBonusCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (strBonusCheck.Checked)
            {
                SubraceBonusCounter++;
            }
            else
            {
                SubraceBonusCounter--;
            }

            toggleScoreBonusSelectability();

            refreshResults();

            OnAbilityBonusAssigned(null);
        }

        private void dexBonusCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (dexBonusCheck.Checked)
            {
                SubraceBonusCounter++;
            }
            else
            {
                SubraceBonusCounter--;
            }

            toggleScoreBonusSelectability();

            refreshResults();

            OnAbilityBonusAssigned(null);
        }

        private void conBonusCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (conBonusCheck.Checked)
            {
                SubraceBonusCounter++;
            }
            else
            {
                SubraceBonusCounter--;
            }

            toggleScoreBonusSelectability();

            refreshResults();
            calculateHealth();

            OnAbilityBonusAssigned(null);
        }

        private void intBonusCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (intBonusCheck.Checked)
            {
                SubraceBonusCounter++;
            }
            else
            {
                SubraceBonusCounter--;
            }

            toggleScoreBonusSelectability();

            refreshResults();

            OnAbilityBonusAssigned(null);
        }

        private void wisBonusCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (wisBonusCheck.Checked)
            {
                SubraceBonusCounter++;
            }
            else
            {
                SubraceBonusCounter--;
            }

            toggleScoreBonusSelectability();

            refreshResults();

            OnAbilityBonusAssigned(null);
        }

        private void chaBonusCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (chaBonusCheck.Checked)
            {
                SubraceBonusCounter++;
            }
            else
            {
                SubraceBonusCounter--;
            }

            toggleScoreBonusSelectability();

            refreshResults();

            OnAbilityBonusAssigned(null);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            resetAbilityAssignments();
        }

        private void rerollButton_Click(object sender, EventArgs e)
        {
            rerollLowAbilityScores();
            refreshResults();
            calculateHealth();
        }

        private void abilityScore1_DoubleClick(object sender, EventArgs e)
        {
            if (abilityScore1.Parent != abilityScoreHoldingLayout)
            {
                abilityScore1.Parent = abilityScoreHoldingLayout;
            }
            else
            {
                foreach (FlowLayoutPanel dropzone in dropzones)
                {
                    bool isEmpty = true;

                    foreach (Control control in dropzone.Controls)
                    {
                        if (control is Label)
                        {
                            isEmpty = false;
                            break;
                        }
                    }

                    if (isEmpty)
                    {
                        abilityScore1.Parent = dropzone;

                        refreshResults();

                        if (dropzone.Name == "conDropzone")
                        {
                            calculateHealth();
                        }

                        OnAbilityAssigned(null);
                        break;
                    }
                }
            }
        }

        private void abilityScore2_DoubleClick(object sender, EventArgs e)
        {
            if (abilityScore2.Parent != abilityScoreHoldingLayout)
            {
                abilityScore2.Parent = abilityScoreHoldingLayout;
            }
            else
            {
                foreach (FlowLayoutPanel dropzone in dropzones)
                {
                    bool isEmpty = true;

                    foreach (Control control in dropzone.Controls)
                    {
                        if (control is Label)
                        {
                            isEmpty = false;
                            break;
                        }
                    }

                    if (isEmpty)
                    {
                        abilityScore2.Parent = dropzone;

                        refreshResults();

                        if (dropzone.Name == "conDropzone")
                        {
                            calculateHealth();
                        }

                        OnAbilityAssigned(null);
                        break;
                    }
                }
            }
        }

        private void abilityScore3_DoubleClick(object sender, EventArgs e)
        {
            if (abilityScore3.Parent != abilityScoreHoldingLayout)
            {
                abilityScore3.Parent = abilityScoreHoldingLayout;
            }
            else
            {
                foreach (FlowLayoutPanel dropzone in dropzones)
                {
                    bool isEmpty = true;

                    foreach (Control control in dropzone.Controls)
                    {
                        if (control is Label)
                        {
                            isEmpty = false;
                            break;
                        }
                    }

                    if (isEmpty)
                    {
                        abilityScore3.Parent = dropzone;

                        refreshResults();

                        if (dropzone.Name == "conDropzone")
                        {
                            calculateHealth();
                        }

                        OnAbilityAssigned(null);
                        break;
                    }
                }
            }
        }

        private void abilityScore4_DoubleClick(object sender, EventArgs e)
        {
            if (abilityScore4.Parent != abilityScoreHoldingLayout)
            {
                abilityScore4.Parent = abilityScoreHoldingLayout;
            }
            else
            {
                foreach (FlowLayoutPanel dropzone in dropzones)
                {
                    bool isEmpty = true;

                    foreach (Control control in dropzone.Controls)
                    {
                        if (control is Label)
                        {
                            isEmpty = false;
                            break;
                        }
                    }

                    if (isEmpty)
                    {
                        abilityScore4.Parent = dropzone;

                        refreshResults();

                        if (dropzone.Name == "conDropzone")
                        {
                            calculateHealth();
                        }

                        OnAbilityAssigned(null);
                        break;
                    }
                }
            }
        }

        private void abilityScore5_DoubleClick(object sender, EventArgs e)
        {
            if (abilityScore5.Parent != abilityScoreHoldingLayout)
            {
                abilityScore5.Parent = abilityScoreHoldingLayout;
            }
            else
            {
                foreach (FlowLayoutPanel dropzone in dropzones)
                {
                    bool isEmpty = true;

                    foreach (Control control in dropzone.Controls)
                    {
                        if (control is Label)
                        {
                            isEmpty = false;
                            break;
                        }
                    }

                    if (isEmpty)
                    {
                        abilityScore5.Parent = dropzone;

                        refreshResults();

                        if (dropzone.Name == "conDropzone")
                        {
                            calculateHealth();
                        }

                        OnAbilityAssigned(null);
                        break;
                    }
                }
            }
        }

        private void abilityScore6_DoubleClick(object sender, EventArgs e)
        {
            if (abilityScore6.Parent != abilityScoreHoldingLayout)
            {
                abilityScore6.Parent = abilityScoreHoldingLayout;
            }
            else
            {
                foreach (FlowLayoutPanel dropzone in dropzones)
                {
                    bool isEmpty = true;

                    foreach (Control control in dropzone.Controls)
                    {
                        if (control is Label)
                        {
                            isEmpty = false;
                            break;
                        }
                    }

                    if (isEmpty)
                    {
                        abilityScore6.Parent = dropzone;

                        refreshResults();

                        if (dropzone.Name == "conDropzone")
                        {
                            calculateHealth();
                        }

                        OnAbilityAssigned(null);
                        break;
                    }
                }
            }
        }
    }
}
