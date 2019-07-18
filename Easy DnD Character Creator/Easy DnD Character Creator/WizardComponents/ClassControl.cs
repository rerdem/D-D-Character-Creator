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

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public partial class ClassControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private List<CharacterClass> classSourceList;
        private List<int> extraChoiceOrderedSelection;

        private string lastUsedBooks;
        private int lastLevel;

        public event EventHandler ClassChanged;
        public event EventHandler ClassChoiceChanged;

        private int ChoiceAmount { get; set; }

        public ClassControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            classSourceList = new List<CharacterClass>();
            extraChoiceOrderedSelection = new List<int>();

            lastUsedBooks = "";

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

            CharacterClass currentClass = classListBox.SelectedItem as CharacterClass;
            if (currentClass != null)
            {
                if (currentClass.HasProficiencyChoice)
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
            }

            return output;
        }

        public bool isValid()
        {
            CharacterClass currentClass = classListBox.SelectedItem as CharacterClass;
            if (currentClass != null)
            {
                if (currentClass.HasProficiencyChoice)
                {
                    return (classListBox.SelectedItems.Count > 0)
                        && (subclassListBox.SelectedItems.Count > 0)
                        && (extraChoiceBox.SelectedItems.Count == ChoiceAmount);
                }
                return (classListBox.SelectedItems.Count > 0) && (subclassListBox.SelectedItems.Count > 0);
            }

            return false;
        }

        public void populateForm()
        {
            if (!Visited || haveUsedBooksChanged() || hasLevelChanged())
            {
                fillClassListBox();
            }

            if (Visited && !haveUsedBooksChanged() && !hasLevelChanged())
            {
                loadPreviousSelections();
            }

            lastUsedBooks = string.Join(", ", wm.DBManager.UsedBooks);
            lastLevel = wm.Choices.Level;
            Visited = true;
        }

        public void saveContent()
        {
            CharacterClass currentClass = classListBox.SelectedItem as CharacterClass;
            Subclass currentSubclass = subclassListBox.SelectedItem as Subclass;
            if ((currentClass != null) && (currentSubclass != null))
            {
                currentSubclass = setSubclassFlags(currentSubclass);
                currentClass.setSelectedSubclass(currentSubclass);

                currentClass = setClassFlags(currentClass);
                
                //save extra choices that did not require extra UserControls
                currentClass.Proficiencies.Clear();
                if (currentClass.HasProficiencyChoice)
                {
                    foreach (string item in extraChoiceBox.SelectedItems)
                    {
                        currentClass.Proficiencies.Add(item);
                    }
                }

                wm.Choices.ClassChoice = currentClass;
            }
        }

        private CharacterClass setClassFlags(CharacterClass selectedClass)
        {
            selectedClass.HasSpellcasting = wm.DBManager.SpellData.hasSpellcasting(selectedClass.Name, selectedClass.getSelectedSubclass().Name, wm.Choices.Level);
            selectedClass.ChoosesSpells = wm.DBManager.SpellData.choosesSpells(selectedClass.Name, selectedClass.getSelectedSubclass().Name, wm.Choices.Level);

            selectedClass.HasFightingStyle = wm.DBManager.ClassData.hasFightingStyle(selectedClass.Name, wm.Choices.Level);
            selectedClass.HasFavoredEnemy = wm.DBManager.ClassData.hasFavoredEnemy(selectedClass.Name, wm.Choices.Level);
            selectedClass.HasFavoredTerrain = wm.DBManager.ClassData.hasFavoredEnemy(selectedClass.Name, wm.Choices.Level);
            selectedClass.HasExtraSkills = wm.DBManager.ClassData.hasClassSkillChoice(selectedClass.Name, wm.Choices.Level);
            selectedClass.HasWarlockPact = wm.DBManager.ClassData.hasWarlockPact(selectedClass.Name, wm.Choices.Level);
            selectedClass.HasEldritchInvocations = wm.DBManager.ClassData.hasEldritchInvocations(selectedClass.Name, wm.Choices.Level);
            selectedClass.HasMetamagic = wm.DBManager.ClassData.hasMetamagic(selectedClass.Name, wm.Choices.Level);
            selectedClass.HasWildShape = wm.DBManager.ClassData.hasWildShape(selectedClass.Name);

            return selectedClass;
        }

        private Subclass setSubclassFlags(Subclass selectedSubclass)
        {
            selectedSubclass.HasExtraSkills = wm.DBManager.ClassData.SubclassData.hasSubclassSkillChoice(selectedSubclass.Name, wm.Choices.Level);
            selectedSubclass.HasTotems = wm.DBManager.ClassData.SubclassData.hasTotemFeatures(selectedSubclass.Name, wm.Choices.Level);
            selectedSubclass.HasExtraSpells = wm.DBManager.ClassData.SubclassData.hasExtraSubclassSpellChoice(selectedSubclass.Name);
            selectedSubclass.HasExtraToolProficiencies = wm.DBManager.ClassData.SubclassData.hasSubclassToolProficiencyChoice(selectedSubclass.Name, wm.Choices.Level);
            selectedSubclass.HasManeuvers = wm.DBManager.ClassData.SubclassData.hasManeuvers(selectedSubclass.Name, wm.Choices.Level);
            selectedSubclass.HasDraconicAncestry = wm.DBManager.ClassData.SubclassData.hasDraconicAncestry(selectedSubclass.Name);
            selectedSubclass.HasElementalDisciplines = wm.DBManager.ClassData.SubclassData.hasDisciplines(selectedSubclass.Name, wm.Choices.Level);
            selectedSubclass.HasHunterChoices = wm.DBManager.ClassData.SubclassData.hasHunterFeatures(selectedSubclass.Name, wm.Choices.Level);
            selectedSubclass.HasCompanion = wm.DBManager.ClassData.SubclassData.hasCompanion(selectedSubclass.Name, wm.Choices.Level);
            selectedSubclass.HasCircleTerrain = wm.DBManager.ClassData.SubclassData.hasCircleTerrain(selectedSubclass.Name, wm.Choices.Level);

            return selectedSubclass;
        }

        private bool haveUsedBooksChanged()
        {
            return (lastUsedBooks != string.Join(", ", wm.DBManager.UsedBooks));
        }

        private bool hasLevelChanged()
        {
            return (lastLevel != wm.Choices.Level);
        }

        private void loadPreviousSelections()
        {
            //class choice
            if (classListBox.Items.Contains(wm.Choices.ClassChoice))
            {
                classListBox.SetSelected(classListBox.Items.IndexOf(wm.Choices.ClassChoice), true);
            }

            //subclass choice
            if (subclassListBox.Items.Contains(wm.Choices.ClassChoice.getSelectedSubclass()))
            {
                subclassListBox.SetSelected(subclassListBox.Items.IndexOf(wm.Choices.ClassChoice.getSelectedSubclass()), true);
            }

            //extra proficiencies
            if (wm.Choices.ClassChoice.Proficiencies.Count > 0)
            {
                for (int i = 0; i < extraChoiceBox.Items.Count; i++)
                {
                    if (wm.Choices.ClassChoice.Proficiencies.Contains(extraChoiceBox.Items[i]))
                    {
                        extraChoiceBox.SetSelected(i, true);
                    }
                }
            }
        }

        private void fillClassListBox()
        {
            classSourceList = wm.DBManager.ClassData.getClasses(wm.Choices.Level);

            classListBox.BeginUpdate();
            classListBox.DataSource = null;
            classListBox.DataSource = classSourceList;
            classListBox.DisplayMember = "Name";
            classListBox.EndUpdate();
        }

        private void fillSubclassListBox()
        {
            CharacterClass currentClass = classListBox.SelectedItem as CharacterClass;
            if (currentClass != null)
            {
                subclassListBox.BeginUpdate();
                subclassListBox.DataSource = null;
                subclassListBox.DataSource = currentClass.Subclasses;
                subclassListBox.DisplayMember = "Name";
                subclassListBox.EndUpdate();
            }
        }

        private void fillExtraChoiceBox()
        {
            CharacterClass currentClass = classListBox.SelectedItem as CharacterClass;
            if (currentClass != null)
            {
                List<string> choiceList = wm.DBManager.ClassData.getExtraClassProficiencies(currentClass.Name);
                choiceList.Remove(wm.Choices.RaceChoice.getSelectedSubrace().Proficiency);

                extraChoiceBox.BeginUpdate();
                extraChoiceBox.DataSource = null;
                extraChoiceBox.DataSource = choiceList;
                extraChoiceBox.EndUpdate();
            }
        }

        private void toggleExtraChoiceBox()
        {
            CharacterClass currentClass = classListBox.SelectedItem as CharacterClass;
            if (currentClass != null)
            {
                if (currentClass.HasProficiencyChoice)
                {
                    descriptionLabel.MaximumSize = new Size(520, descriptionLabel.MaximumSize.Height);
                    extraChoiceLayout.Visible = true;

                    ChoiceAmount = currentClass.ProficiencyChoiceAmount;
                    extraChoiceLabel.Text = extraChoiceLabel.Text.ToString().Replace(Regex.Match(extraChoiceLabel.Text, @"\d+").Value, ChoiceAmount.ToString());
                    if (ChoiceAmount > 1)
                    {
                        extraChoiceBox.SelectionMode = SelectionMode.MultiSimple;
                    }
                    else
                    {
                        extraChoiceBox.SelectionMode = SelectionMode.One;
                    }

                    fillExtraChoiceBox();
                }
                else
                {
                    descriptionLabel.MaximumSize = new Size(650, descriptionLabel.MaximumSize.Height);
                    extraChoiceLayout.Visible = false;
                    ChoiceAmount = 0;
                }
            }
        }

        private void syncExtraChoiceSelectionOrder()
        {
            //add new selected items
            foreach (int index in extraChoiceBox.SelectedIndices)
            {
                if (!extraChoiceOrderedSelection.Contains(index))
                {
                    extraChoiceOrderedSelection.Add(index);
                }
            }

            //remove deselected items
            extraChoiceOrderedSelection.RemoveAll(index => !extraChoiceBox.SelectedIndices.Contains(index));
        }

        private void classListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillSubclassListBox();

            toggleExtraChoiceBox();

            OnClassChanged(null);
        }

        private void subclassListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CharacterClass currentClass = classListBox.SelectedItem as CharacterClass;
            Subclass currentSubclass = subclassListBox.SelectedItem as Subclass;
            if ((currentClass != null) && (currentSubclass != null))
            {
                currentClass.setSelectedSubclass(currentSubclass);

                descriptionLabel.Text = currentClass.Description;
                descriptionLabel.Text += Environment.NewLine;
                descriptionLabel.Text += currentSubclass.Description;

                //saveContent();
            }
        }

        private void extraChoiceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            syncExtraChoiceSelectionOrder();
            if (extraChoiceBox.SelectedItems.Count > 0)
            {
                if (extraChoiceBox.SelectedItems.Count > ChoiceAmount)
                {
                    int lastSelectedIndex = extraChoiceOrderedSelection.ElementAt(extraChoiceOrderedSelection.Count - 1);
                    extraChoiceBox.SelectedIndices.Remove(lastSelectedIndex);
                }

                //saveContent(); was already commented out

                OnClassChoiceChanged(null);
            }
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
