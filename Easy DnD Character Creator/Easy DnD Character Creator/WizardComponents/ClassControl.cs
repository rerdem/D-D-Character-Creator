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
    public partial class ClassControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        public event EventHandler ClassChanged;
        public event EventHandler ClassChoiceChanged;

        private int ChoiceAmount { get; set; }

        public ClassControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;
            ChoiceAmount = 0;
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

            if ((classListBox.SelectedItems.Count == 0) || (subclassListBox.SelectedItems.Count == 0))
            {
                output += classBox.Text;
            }

            if (wm.DBManager.ClassData.classHasExtraChoice(classListBox.SelectedItem.ToString()))
            {
                if (extraChoiceBox.SelectedItems.Count != ChoiceAmount)
                {
                    if (!string.IsNullOrEmpty(output))
                    {
                        output += ", ";
                    }

                    output += "class tool proficiency amount";
                }
            }

            return output;
        }

        public bool isValid()
        {
            if (wm.DBManager.ClassData.classHasExtraChoice(classListBox.SelectedItem.ToString()))
            {
                return (classListBox.SelectedItems.Count > 0) 
                    && (subclassListBox.SelectedItems.Count > 0)
                    && (extraChoiceBox.SelectedItems.Count == ChoiceAmount);
            }
            return (classListBox.SelectedItems.Count > 0) && (subclassListBox.SelectedItems.Count > 0);
        }

        public void populateForm()
        {
            fillClassListBox();
            Visited = true;
        }

        public void saveContent()
        {
            //save class/subclass
            wm.Choices.Class = classListBox.SelectedItem.ToString();
            wm.Choices.Subclass = subclassListBox.SelectedItem.ToString();

            //save information about properties and additional choices to make
            wm.Choices.HasFightingStyle = wm.DBManager.ExtraClassChoiceData.FightingStyleData.hasFightingStyle(classListBox.SelectedItem.ToString(), wm.Choices.Level);
            wm.Choices.HasFavoredEnemy = wm.DBManager.ExtraClassChoiceData.FavoredEnemyTerrainData.hasFavoredEnemy(classListBox.SelectedItem.ToString(), wm.Choices.Level);
            wm.Choices.HasFavoredTerrain = wm.DBManager.ExtraClassChoiceData.FavoredEnemyTerrainData.hasFavoredTerrain(classListBox.SelectedItem.ToString(), wm.Choices.Level);
            wm.Choices.HasExtraClassSkills = wm.DBManager.ExtraClassChoiceData.ExtraClassSkillData.hasSkillChoice(classListBox.SelectedItem.ToString(), wm.Choices.Level);
            wm.Choices.HasWarlockChoices = wm.DBManager.ExtraClassChoiceData.WarlockChoiceData.hasWarlockChoices(classListBox.SelectedItem.ToString(), wm.Choices.Level);
            wm.Choices.HasMetamagic = wm.DBManager.ExtraClassChoiceData.MetamagicData.hasMetamagic(classListBox.SelectedItem.ToString(), wm.Choices.Level);
            wm.Choices.HasExtraClassChoice = wm.Choices.HasFightingStyle ||
                                             wm.Choices.HasFavoredEnemy ||
                                             wm.Choices.HasFavoredTerrain ||
                                             wm.Choices.HasExtraClassSkills ||
                                             wm.Choices.HasWarlockChoices ||
                                             wm.Choices.HasMetamagic;

            //wm.Choices.HasExtraClassChoice = wm.DBManager.ExtraClassChoiceData.hasExtraClassChoices(classListBox.SelectedItem.ToString(), wm.Choices.Level);
            //wm.Choices.HasExtraSubclassChoice = wm.DBManager.ExtraSubclassChoiceData.hasExtraSubclassChoices(subclassListBox.SelectedItem.ToString(), wm.Choices.Level);
            wm.Choices.HasSpellcasting = wm.DBManager.SpellData.hasSpellcasting(classListBox.SelectedItem.ToString(), subclassListBox.SelectedItem.ToString(), wm.Choices.Level);
            wm.Choices.ChoosesSpells = wm.DBManager.SpellData.hasSpellcasting(classListBox.SelectedItem.ToString(), subclassListBox.SelectedItem.ToString(), wm.Choices.Level);

            wm.Choices.HasExtraSubclassSkills = wm.DBManager.ExtraSubclassChoiceData.ExtraSubclassSkillData.hasSkillChoice(subclassListBox.SelectedItem.ToString(), wm.Choices.Level);
            wm.Choices.HasTotems = wm.DBManager.ExtraSubclassChoiceData.TotemData.hasTotemFeatures(subclassListBox.SelectedItem.ToString(), wm.Choices.Level);
            wm.Choices.HasExtraSubclassSpells = wm.DBManager.ExtraSubclassChoiceData.ExtraSubclassSpellData.hasExtraSpellChoice(subclassListBox.SelectedItem.ToString());
            wm.Choices.HasExtraToolProficiencies = wm.DBManager.ExtraSubclassChoiceData.ExtraToolProficiencyData.hasToolProficiencyChoice(subclassListBox.SelectedItem.ToString(), wm.Choices.Level);
            wm.Choices.HasManeuvers = wm.DBManager.ExtraSubclassChoiceData.ManeuverData.hasManeuvers(subclassListBox.SelectedItem.ToString(), wm.Choices.Level);
            wm.Choices.HasDraconicAncestry = wm.DBManager.ExtraSubclassChoiceData.DraconicAncestryData.hasDraconicAncestry(subclassListBox.SelectedItem.ToString());
            wm.Choices.HasElementalDisciplines = wm.DBManager.ExtraSubclassChoiceData.ElementalDisciplineData.hasDisciplines(subclassListBox.SelectedItem.ToString(), wm.Choices.Level);
            wm.Choices.HasHunterChoices = wm.DBManager.ExtraSubclassChoiceData.HunterData.hasHunterFeatures(subclassListBox.SelectedItem.ToString(), wm.Choices.Level);
            wm.Choices.HasCompanion = wm.DBManager.ExtraSubclassChoiceData.BeastCompanionData.hasCompanion(subclassListBox.SelectedItem.ToString(), wm.Choices.Level);
            wm.Choices.HasCircleTerrain = wm.DBManager.ExtraSubclassChoiceData.CircleTerrainData.hasCircleTerrain(subclassListBox.SelectedItem.ToString(), wm.Choices.Level);
            wm.Choices.HasExtraSubclassChoice = wm.Choices.HasExtraSubclassSkills ||
                                                wm.Choices.HasTotems ||
                                                wm.Choices.HasExtraSubclassSpells ||
                                                wm.Choices.HasExtraToolProficiencies ||
                                                wm.Choices.HasManeuvers ||
                                                wm.Choices.HasDraconicAncestry ||
                                                wm.Choices.HasElementalDisciplines ||
                                                wm.Choices.HasHunterChoices ||
                                                wm.Choices.HasCompanion ||
                                                wm.Choices.HasCircleTerrain;

            wm.Choices.HasWildShape = wm.DBManager.StoryData.hasWildShape(classListBox.SelectedItem.ToString());

            //save extra choices that did not require extre UserControls
            wm.Choices.ClassProficiencies.Clear();
            if (wm.DBManager.ClassData.classHasExtraChoice(classListBox.SelectedItem.ToString()))
            {
                foreach (object obj in extraChoiceBox.SelectedItems)
                {
                    if (!string.IsNullOrEmpty(obj.ToString()))
                    {
                        wm.Choices.ClassProficiencies.Add(obj.ToString());
                    }
                }
            }
        }

        private void fillClassListBox()
        {
            List<string> classList = wm.DBManager.ClassData.getClasses();

            classListBox.BeginUpdate();
            classListBox.Items.Clear();
            foreach (string className in classList)
            {
                classListBox.Items.Add(className);
            }
            classListBox.EndUpdate();

            if (classListBox.Items.Contains(wm.Choices.Class))
            {
                classListBox.SetSelected(classListBox.Items.IndexOf(wm.Choices.Class), true);
            }
            else
            {
                classListBox.SetSelected(0, true);
            }
        }

        private void fillSubclassListBox(string inputClass)
        {
            List<string> subclassList = wm.DBManager.ClassData.getSubclasses(inputClass, wm.Choices.Level);

            subclassListBox.BeginUpdate();
            subclassListBox.Items.Clear();
            foreach (string subrace in subclassList)
            {
                subclassListBox.Items.Add(subrace);
            }
            subclassListBox.EndUpdate();

            if (subclassListBox.Items.Contains(wm.Choices.Subclass))
            {
                subclassListBox.SetSelected(subclassListBox.Items.IndexOf(wm.Choices.Subclass), true);
            }
            else
            {
                subclassListBox.SetSelected(0, true);
            }
        }

        private void fillExtraChoiceBox(string classChoice)
        {
            List<string> choiceList = wm.DBManager.ClassData.getExtraClassProficiencies(classChoice);

            extraChoiceBox.BeginUpdate();
            extraChoiceBox.Items.Clear();
            foreach (string entry in choiceList)
            {
                extraChoiceBox.Items.Add(entry);
            }
            extraChoiceBox.EndUpdate();

            if (wm.Choices.ClassProficiencies.Count > 0)
            {
                for (int i = 0; i < extraChoiceBox.Items.Count; i++)
                {
                    if (wm.Choices.ClassProficiencies.Contains(extraChoiceBox.Items[i]))
                    {
                        extraChoiceBox.SetSelected(i, true);
                    }
                }
            }
        }

        private void toggleExtraChoiceBox()
        {
            if (wm.DBManager.ClassData.classHasExtraChoice(classListBox.SelectedItem.ToString()))
            {
                descriptionLabel.MaximumSize = new Size(520, descriptionLabel.MaximumSize.Height);
                extraChoiceLayout.Visible = true;

                ChoiceAmount = wm.DBManager.ClassData.getExtraClassProficiencyAmount(classListBox.SelectedItem.ToString());
                extraChoiceLabel.Text = extraChoiceLabel.Text.ToString().Replace(Regex.Match(extraChoiceLabel.Text, @"\d+").Value, ChoiceAmount.ToString());
                if (ChoiceAmount > 1)
                {
                    extraChoiceBox.SelectionMode = SelectionMode.MultiSimple;
                }
                else
                {
                    extraChoiceBox.SelectionMode = SelectionMode.One;
                }

                fillExtraChoiceBox(classListBox.SelectedItem.ToString());
            }
            else
            {
                descriptionLabel.MaximumSize = new Size(650, descriptionLabel.MaximumSize.Height);
                extraChoiceLayout.Visible = false;
                ChoiceAmount = 0;
            }
        }

        private void classListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillSubclassListBox(classListBox.SelectedItem.ToString());

            toggleExtraChoiceBox();

            OnClassChanged(null);
        }

        private void subclassListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            descriptionLabel.Text = wm.DBManager.ClassData.getClassDescription(classListBox.SelectedItem.ToString());
            descriptionLabel.Text += Environment.NewLine;
            descriptionLabel.Text += wm.DBManager.ClassData.getSubclassDescription(subclassListBox.SelectedItem.ToString());

            saveContent();
        }

        private void extraChoiceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (extraChoiceBox.SelectedItems.Count > ChoiceAmount)
            {
                extraChoiceBox.SelectedItems.Remove(extraChoiceBox.SelectedItems[ChoiceAmount]);
            }
            //saveContent();

            OnClassChoiceChanged(null);
        }

        protected virtual void OnClassChanged(EventArgs e)
        {
            EventHandler handler = ClassChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnClassChoiceChanged(EventArgs e)
        {
            EventHandler handler = ClassChoiceChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
