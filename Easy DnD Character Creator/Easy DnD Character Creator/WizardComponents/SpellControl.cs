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
    public partial class SpellControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private string lastCharacterInfo; //subrace, class, subclass, level
        private int lastWisdom;

        private bool HasSubclassSpellSchoolLimitations;
        private int SubclassSpellSchoolLimitationExceptions;
        private List<string> SubclassSpellSchoolLimitations;

        private int CantripsKnown { get; set; }
        private int SpellsKnown { get; set; }

        private List<Spell> availableCantripsSource { get; set; }
        private List<Spell> chosenCantripsSource { get; set; }
        private List<Spell> availableSpellsSource { get; set; }
        private List<Spell> chosenSpellsSource { get; set; }

        public event EventHandler SpellChosen;

        public SpellControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            lastCharacterInfo = "";
            lastWisdom = 0;

            HasSubclassSpellSchoolLimitations = false;
            SubclassSpellSchoolLimitationExceptions = 0;
            SubclassSpellSchoolLimitations = new List<string>();

            CantripsKnown = 0;
            SpellsKnown = 0;

            availableCantripsSource = new List<Spell>();
            chosenCantripsSource = new List<Spell>();
            availableSpellsSource = new List<Spell>();
            chosenSpellsSource = new List<Spell>();

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

            if (chosenCantrips.Items.Count != CantripsKnown)
            {
                output += $"select {CantripsKnown - chosenCantrips.Items.Count} more cantrip(s)";
            }

            if (chosenSpells.Items.Count != SpellsKnown)
            {
                if (!string.IsNullOrEmpty(output))
                {
                    output += ", ";
                }
                output += $"select {SpellsKnown - chosenSpells.Items.Count} more spell(s)";
            }

            if (HasSubclassSpellSchoolLimitations)
            {
                int exceptionCounter = 0;

                foreach (Spell spell in chosenSpells.Items)
                {
                    if ((!spell.NotDeselectable)
                    && (!SubclassSpellSchoolLimitations.Contains(spell.School)))
                    {
                        exceptionCounter++;
                    }
                }

                if (SubclassSpellSchoolLimitationExceptions < exceptionCounter)
                {
                    if (!string.IsNullOrEmpty(output))
                    {
                        output += ", ";
                    }
                    output += $"select {exceptionCounter - SubclassSpellSchoolLimitationExceptions} fewer spell(s) outside of your spell school limitations";
                }
            }

            return output;
        }

        public bool isValid()
        {
            bool isValid = ((chosenCantrips.Items.Count == CantripsKnown) && (chosenSpells.Items.Count == SpellsKnown));

            if (HasSubclassSpellSchoolLimitations)
            {
                int exceptionCounter = 0;

                foreach (Spell spell in chosenSpells.Items)
                {
                    if ((!spell.NotDeselectable)
                    && (!SubclassSpellSchoolLimitations.Contains(spell.School)))
                    {
                        exceptionCounter++;
                    }
                }

                isValid = isValid && (SubclassSpellSchoolLimitationExceptions >= exceptionCounter);
            }

            return isValid;
        }

        public void populateForm()
        {
            resetSpells();

            if ((Visited) && (!hasCharacterInfoChanged()) && (!hasWisdomChanged()))
            {
                loadChosenSpells();
            }

            setCharacterInfo();
            setLastWisdom();

            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.Spells.Clear();

            //save cantrips
            foreach (Spell spell in chosenCantripsSource)
            {
                wm.Choices.Spells.Add(spell);
            }

            //save spells
            foreach (Spell spell in chosenSpellsSource)
            {
                wm.Choices.Spells.Add(spell);
            }
        }

        private void resetSpells()
        {
            //reset variables
            HasSubclassSpellSchoolLimitations = wm.DBManager.SpellData.hasSubclassSpellSchoolLimitations(wm.Choices.ClassChoice.getSelectedSubclass().Name);
            SubclassSpellSchoolLimitationExceptions = wm.DBManager.SpellData.getSubclassSpellSchoolLimitationExceptions(wm.Choices.ClassChoice.getSelectedSubclass().Name, wm.Choices.Level);
            SubclassSpellSchoolLimitations = wm.DBManager.SpellData.getSubclassSpellSchoolLimitations(wm.Choices.ClassChoice.getSelectedSubclass().Name);

            //set number of cantrips and spells known
            CantripsKnown = wm.DBManager.SpellData.getCantripsKnown(wm.Choices.ClassChoice.Name, wm.Choices.ClassChoice.getSelectedSubclass().Name, wm.Choices.Level);

            if (wm.DBManager.SpellData.areSpellsKnownStatic(wm.Choices.ClassChoice.Name, wm.Choices.ClassChoice.getSelectedSubclass().Name))
            {
                SpellsKnown = wm.DBManager.SpellData.getSpellsKnown(wm.Choices.ClassChoice.Name, wm.Choices.ClassChoice.getSelectedSubclass().Name, wm.Choices.Level);
            }
            else
            {
                //number of spells known for dynamic classes is WIS-Modifier + class level with a minimum of 1
                SpellsKnown = wm.Choices.Wisdom.getModifier();
                if (SpellsKnown < 1)
                {
                    SpellsKnown = 1;
                }
            }

            //set introLabel text
            introLabel.Text = $"You have chosen a class that has the ability to cast spells. Please choose {CantripsKnown} cantrips and {SpellsKnown} spells " +
                              $"with the help of the arrow buttons below. You may have also gained spells from your choice of race or class. These spells " +
                              $"cannot be unlearned and do not count against the limits.";

            //get possible spell school limitations and add to introLabel text
            if (HasSubclassSpellSchoolLimitations)
            {
                introLabel.Text += $" With the exception of {SubclassSpellSchoolLimitationExceptions} spell(s), you are limited to spells from these schools: {string.Join(", ", SubclassSpellSchoolLimitations.ToArray())}.";
            }

            //reset spell description labels
            cantripDescriptionLabel.Text = "No cantrips available.";
            spellDescriptionLabel.Text = "No spells available.";

            //clear and populate source lists
            chosenCantripsSource.Clear();
            chosenSpellsSource.Clear();

            //populate extra race and class spells
            if ((wm.DBManager.SpellData.hasExtraRaceSpells(wm.Choices.RaceChoice.getSelectedSubrace().Name, wm.Choices.Level)) || (wm.DBManager.SpellData.hasExtraSubclassSpells(wm.Choices.ClassChoice.getSelectedSubclass().Name, wm.Choices.Level)))
            {
                List<Spell> raceClassSpells = wm.DBManager.SpellData.getExtraRaceSpells(wm.Choices.RaceChoice.getSelectedSubrace().Name, wm.Choices.Level);
                raceClassSpells.AddRange(wm.DBManager.SpellData.getExtraSubclassSpells(wm.Choices.ClassChoice.getSelectedSubclass().Name, wm.Choices.Level));

                foreach (Spell spell in raceClassSpells)
                {
                    if (spell.Level == 0)
                    {
                        chosenCantripsSource.Add(spell);
                        CantripsKnown++; //to prevent interference with page validation
                    }
                    else
                    {
                        chosenSpellsSource.Add(spell);
                        SpellsKnown++; //to prevent interference with page validation
                    }
                }
            }
            
            //populate cantrips without extra race or class spells
            availableCantripsSource = wm.DBManager.SpellData.getCantripOptions(wm.Choices.ClassChoice.Name, wm.Choices.ClassChoice.getSelectedSubclass().Name).Except(chosenCantripsSource).ToList();

            //populate spells without extra race or class spells
            availableSpellsSource = wm.DBManager.SpellData.getSpellOptions(wm.Choices.ClassChoice.Name, wm.Choices.ClassChoice.getSelectedSubclass().Name, wm.Choices.Level).Except(chosenSpellsSource).ToList();

            //refresh list boxes from source lists
            refreshListBoxes();

            //set default selections
            if (availableCantrips.Items.Count > 0)
            {
                availableCantrips.SetSelected(0, true);
            }

            if (availableSpells.Items.Count > 0)
            {
                availableSpells.SetSelected(0, true);
            }

            //reset arrow buttons
            manageButtonClickability();
        }

        private void loadChosenSpells()
        {
            //load cantrips
            for (int i = availableCantripsSource.Count - 1; i >= 0; i--)
            {
                if (wm.Choices.Spells.Contains(availableCantripsSource[i]))
                {
                    chosenCantripsSource.Add(availableCantripsSource[i]);
                    availableCantripsSource.RemoveAt(i);
                }
            }

            //load spells
            for (int i = availableSpellsSource.Count - 1; i >= 0; i--)
            {
                if (wm.Choices.Spells.Contains(availableSpellsSource[i]))
                {
                    chosenSpellsSource.Add(availableSpellsSource[i]);
                    availableSpellsSource.RemoveAt(i);
                }
            }

            //refresh data sources
            refreshListBoxes();

            //reset arrow buttons
            manageButtonClickability();
        }

        private void manageButtonClickability()
        {
            if (chosenCantripsSource.Count < CantripsKnown)
            {
                cantripAddButton.Enabled = true;
            }
            else
            {
                cantripAddButton.Enabled = false;
            }

            if (chosenSpellsSource.Count < SpellsKnown)
            {
                spellAddButton.Enabled = true;
            }
            else
            {
                spellAddButton.Enabled = false;
            }
        }

        private void refreshListBoxes()
        {
            //begin updates
            availableCantrips.BeginUpdate();
            chosenCantrips.BeginUpdate();

            availableSpells.BeginUpdate();
            chosenSpells.BeginUpdate();

            //refresh data sources
            availableCantrips.DataSource = null;
            availableCantrips.DataSource = availableCantripsSource;
            availableCantrips.DisplayMember = "Name";
            chosenCantrips.DataSource = null;
            chosenCantrips.DataSource = chosenCantripsSource;
            chosenCantrips.DisplayMember = "Name";
            availableSpells.DataSource = null;
            availableSpells.DataSource = availableSpellsSource;
            availableSpells.DisplayMember = "Name";
            chosenSpells.DataSource = null;
            chosenSpells.DataSource = chosenSpellsSource;
            chosenSpells.DisplayMember = "Name";

            //end updates
            availableCantrips.EndUpdate();
            chosenCantrips.EndUpdate();

            availableSpells.EndUpdate();
            chosenSpells.EndUpdate();
        }

        private void setCharacterInfo()
        {
            lastCharacterInfo = wm.Choices.RaceChoice.getSelectedSubrace().Name + wm.Choices.ClassChoice.Name + wm.Choices.ClassChoice.getSelectedSubclass().Name + wm.Choices.Level.ToString();
        }

        private bool hasCharacterInfoChanged()
        {
            string currentCharacterInfo = wm.Choices.RaceChoice.getSelectedSubrace().Name + wm.Choices.ClassChoice.Name + wm.Choices.ClassChoice.getSelectedSubclass().Name + wm.Choices.Level.ToString();
            return (currentCharacterInfo != lastCharacterInfo);
        }

        private void setLastWisdom()
        {
            lastWisdom = wm.Choices.Wisdom.getTotalValue();
        }

        private bool hasWisdomChanged()
        {
            return (wm.Choices.Wisdom.getTotalValue() != lastWisdom);
        }

        private void cantripAddButton_Click(object sender, EventArgs e)
        {
            if (chosenCantripsSource.Count < CantripsKnown)
            {
                if (availableCantrips.SelectedItems.Count > 0)
                {
                    chosenCantripsSource.Add((Spell)availableCantrips.SelectedItem);
                    availableCantripsSource.Remove((Spell)availableCantrips.SelectedItem);
                    refreshListBoxes();
                    manageButtonClickability();

                    OnSpellChosen(null);
                }
            }
        }

        private void cantripRemoveButton_Click(object sender, EventArgs e)
        {
            if (chosenCantrips.SelectedItems.Count > 0)
            {
                Spell currentSelectedItem = (Spell)chosenCantrips.SelectedItem;
                if (!currentSelectedItem.NotDeselectable)
                {
                    availableCantripsSource.Add(currentSelectedItem);
                    chosenCantripsSource.Remove(currentSelectedItem);
                    refreshListBoxes();
                    manageButtonClickability();

                    OnSpellChosen(null);
                }
            }
        }

        private void spellAddButton_Click(object sender, EventArgs e)
        {
            if (chosenSpellsSource.Count < SpellsKnown)
            {
                if (availableSpells.SelectedItems.Count > 0)
                {
                    chosenSpellsSource.Add((Spell)availableSpells.SelectedItem);
                    availableSpellsSource.Remove((Spell)availableSpells.SelectedItem);
                    refreshListBoxes();
                    manageButtonClickability();

                    OnSpellChosen(null);
                }
            }
        }

        private void spellRemoveButton_Click(object sender, EventArgs e)
        {
            if (chosenSpells.SelectedItems.Count > 0)
            {
                Spell currentSelectedItem = (Spell)chosenSpells.SelectedItem;
                if (!currentSelectedItem.NotDeselectable)
                {
                    availableSpellsSource.Add(currentSelectedItem);
                    chosenSpellsSource.Remove(currentSelectedItem);
                    refreshListBoxes();
                    manageButtonClickability();

                    OnSpellChosen(null);
                }
            }
        }

        private void availableCantrips_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (availableCantrips.SelectedItems.Count > 0)
            {
                cantripDescriptionLabel.Text = SpellFormatter.formatSpellDescription((Spell)availableCantrips.SelectedItem);
            }
        }

        private void chosenCantrips_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chosenCantrips.SelectedItems.Count > 0)
            {
                cantripDescriptionLabel.Text = SpellFormatter.formatSpellDescription((Spell)chosenCantrips.SelectedItem);
            }
        }

        private void availableSpells_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (availableSpells.SelectedItems.Count > 0)
            {
                spellDescriptionLabel.Text = SpellFormatter.formatSpellDescription((Spell)availableSpells.SelectedItem);
            }
        }

        private void chosenSpells_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chosenSpells.SelectedItems.Count > 0)
            {
                spellDescriptionLabel.Text = SpellFormatter.formatSpellDescription((Spell)chosenSpells.SelectedItem);
            }
        }

        protected virtual void OnSpellChosen(EventArgs e)
        {
            EventHandler handler = SpellChosen;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
