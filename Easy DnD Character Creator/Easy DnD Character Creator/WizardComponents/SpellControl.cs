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
    public partial class SpellControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private string lastCharacterInfo; //subrace, class, subclass, level
        private int lastWisdom;

        private int CantripsKnown { get; set; }
        private int SpellsKnown { get; set; }

        public event EventHandler SpellChosen;

        public SpellControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            visited = false;

            lastCharacterInfo = "";
            lastWisdom = 0;

            CantripsKnown = 0;
            SpellsKnown = 0;

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

            return output;
        }

        public bool isValid()
        {
            return ((chosenCantrips.Items.Count == CantripsKnown) && (chosenSpells.Items.Count == SpellsKnown));
        }

        public void populateForm()
        {
            resetSpells();

            if ((visited) && (!hasCharacterInfoChanged()) && (!hasWisdomChanged()))
            {
                loadChosenSpells();
            }

            setCharacterInfo();
            setLastWisdom();

            if (!visited)
            {
                visited = true;
            }
        }

        public void saveContent()
        {
            wm.Choices.Spells = "";

            //save cantrips
            foreach (var spell in chosenCantrips.Items)
            {
                if (!string.IsNullOrEmpty(wm.Choices.Spells))
                {
                    wm.Choices.Spells += ",";
                }
                wm.Choices.Spells += spell.ToString(); ;
            }

            //save spells
            foreach (var spell in chosenSpells.Items)
            {
                if (!string.IsNullOrEmpty(wm.Choices.Spells))
                {
                    wm.Choices.Spells += ",";
                }
                wm.Choices.Spells += spell.ToString(); ;
            }
        }

        private void resetSpells()
        {
            //set number of cantrips and spells known
            CantripsKnown = wm.DBManager.getCantripsKnown(wm.Choices.Class, wm.Choices.Subclass, wm.Choices.Level);

            if (wm.DBManager.areSpellsKnownStatic(wm.Choices.Class, wm.Choices.Subclass))
            {
                SpellsKnown = wm.DBManager.getSpellsKnown(wm.Choices.Class, wm.Choices.Subclass, wm.Choices.Level);
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

            //clear and populate lists
            //populate extra race and class spells
            chosenCantrips.BeginUpdate();
            chosenCantrips.Items.Clear();

            chosenSpells.BeginUpdate();
            chosenSpells.Items.Clear();

            if ((wm.DBManager.hasExtraRaceSpells(wm.Choices.Subrace, wm.Choices.Level)) || (wm.DBManager.hasExtraSubclassSpells(wm.Choices.Subclass, wm.Choices.Level)))
            {
                List<Spell> raceClassSpells = wm.DBManager.getExtraRaceSpells(wm.Choices.Subrace, wm.Choices.Level);
                raceClassSpells.AddRange(wm.DBManager.getExtraSubclassSpells(wm.Choices.Subclass, wm.Choices.Level));

                foreach (Spell spell in raceClassSpells)
                {
                    if (spell.Level == 0)
                    {
                        chosenCantrips.Items.Add(spell.Name);
                        CantripsKnown++; //to prevent interference with page validation
                    }
                    else
                    {
                        chosenSpells.Items.Add(spell.Name);
                        SpellsKnown++; //to prevent interference with page validation
                    }
                }
            }

            chosenCantrips.EndUpdate();
            chosenSpells.EndUpdate();

            //populate cantrips without extra race or class spells
            availableCantrips.BeginUpdate();
            availableCantrips.Items.Clear();

            List<string> cantrips = wm.DBManager.getCantripOptions(wm.Choices.Class, wm.Choices.Subclass);

            foreach (string spell in cantrips)
            {
                if (!chosenCantrips.Items.Contains(spell))
                {
                    availableCantrips.Items.Add(spell);
                }
            }

            availableCantrips.EndUpdate();

            if (availableCantrips.Items.Count > 0)
            {
                availableCantrips.SetSelected(0, true);
            }

            //populate spells without extra race or class spells
            availableSpells.BeginUpdate();
            availableSpells.Items.Clear();

            List<string> spells = wm.DBManager.getSpellOptions(wm.Choices.Class, wm.Choices.Subclass, wm.Choices.Level);

            foreach (string spell in spells)
            {
                if (!chosenSpells.Items.Contains(spell))
                {
                    availableSpells.Items.Add(spell);
                }
            }

            availableSpells.EndUpdate();

            if (availableSpells.Items.Count > 0)
            {
                availableSpells.SetSelected(0, true);
            }

            //reset arrow buttons
            manageButtonClickability();
        }

        private void loadChosenSpells()
        {
            //populate lists
            //begin updates
            availableCantrips.BeginUpdate();
            chosenCantrips.BeginUpdate();

            availableSpells.BeginUpdate();
            chosenSpells.BeginUpdate();

            //load cantrips
            for (int i = availableCantrips.Items.Count - 1; i >= 0; i--)
            {
                if (wm.Choices.Spells.Contains(availableCantrips.Items[i].ToString()))
                {
                    chosenCantrips.Items.Add(availableCantrips.Items[i].ToString());
                    availableCantrips.Items.RemoveAt(i);
                }
            }

            //load spells
            for (int i = availableSpells.Items.Count - 1; i >= 0; i--)
            {
                if (wm.Choices.Spells.Contains(availableSpells.Items[i].ToString()))
                {
                    chosenSpells.Items.Add(availableSpells.Items[i].ToString());
                    availableSpells.Items.RemoveAt(i);
                }
            }

            //end updates
            availableCantrips.EndUpdate();
            chosenCantrips.EndUpdate();

            availableSpells.EndUpdate();
            chosenSpells.EndUpdate();

            //reset arrow buttons
            manageButtonClickability();
        }

        private void manageButtonClickability()
        {
            if (chosenCantrips.Items.Count < CantripsKnown)
            {
                cantripAddButton.Enabled = true;
            }
            else
            {
                cantripAddButton.Enabled = false;
            }

            if (chosenSpells.Items.Count < SpellsKnown)
            {
                spellAddButton.Enabled = true;
            }
            else
            {
                spellAddButton.Enabled = false;
            }
        }

        private string formatSpellDescription(Spell spell)
        {
            //title
            string output = spell.Name;
            output += Environment.NewLine;

            //level, school, ritual
            output += "(Level ";
            output += spell.Level.ToString();
            output += " ";
            output += spell.School;

            if (spell.Ritual)
            {
                output += ", Ritual";
            }

            output += ")";
            output += Environment.NewLine;

            //casting time, range
            output += "Casting Time: ";
            output += spell.CastTime;
            output += " | Range: ";
            output += spell.Range;
            output += Environment.NewLine;

            //components, duration
            output += "Components: ";
            output += spell.Components;
            output += " | Duration: ";
            output += spell.Duration;
            output += Environment.NewLine;

            output += Environment.NewLine;

            //description
            output += spell.Description;

            return output;
        }

        private void setCharacterInfo()
        {
            lastCharacterInfo = wm.Choices.Subrace + wm.Choices.Class + wm.Choices.Subclass + wm.Choices.Level.ToString();
        }

        private bool hasCharacterInfoChanged()
        {
            string currentCharacterInfo = wm.Choices.Subrace + wm.Choices.Class + wm.Choices.Subclass + wm.Choices.Level.ToString();
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
            if (chosenCantrips.Items.Count < CantripsKnown)
            {
                if (availableCantrips.SelectedItems.Count > 0)
                {
                    chosenCantrips.Items.Add(availableCantrips.SelectedItem);
                    availableCantrips.Items.Remove(availableCantrips.SelectedItem);

                    OnSpellChosen(null);
                }
            }
            manageButtonClickability();
        }

        private void cantripRemoveButton_Click(object sender, EventArgs e)
        {
            if (chosenCantrips.SelectedItems.Count > 0)
            {
                if ((!wm.DBManager.isExtraRaceSpell(wm.Choices.Subrace, wm.Choices.Level, chosenCantrips.SelectedItem.ToString())) 
                    && (!wm.DBManager.isExtraSubclassSpell(wm.Choices.Subclass, wm.Choices.Level, chosenCantrips.SelectedItem.ToString())))
                {
                    availableCantrips.Items.Add(chosenCantrips.SelectedItem);
                    chosenCantrips.Items.Remove(chosenCantrips.SelectedItem);

                    OnSpellChosen(null);
                }
            }
            manageButtonClickability();
        }

        private void spellAddButton_Click(object sender, EventArgs e)
        {
            if (chosenSpells.Items.Count < SpellsKnown)
            {
                if (availableSpells.SelectedItems.Count > 0)
                {
                    chosenSpells.Items.Add(availableSpells.SelectedItem);
                    availableSpells.Items.Remove(availableSpells.SelectedItem);

                    OnSpellChosen(null);
                }
            }
            manageButtonClickability();
        }

        private void spellRemoveButton_Click(object sender, EventArgs e)
        {
            if (chosenSpells.SelectedItems.Count > 0)
            {
                if ((!wm.DBManager.isExtraRaceSpell(wm.Choices.Subrace, wm.Choices.Level, chosenSpells.SelectedItem.ToString()))
                    && (!wm.DBManager.isExtraSubclassSpell(wm.Choices.Subclass, wm.Choices.Level, chosenSpells.SelectedItem.ToString())))
                {
                    availableSpells.Items.Add(chosenSpells.SelectedItem);
                    chosenSpells.Items.Remove(chosenSpells.SelectedItem);

                    OnSpellChosen(null);
                }
            }
            manageButtonClickability();
        }

        private void availableCantrips_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (availableCantrips.SelectedItems.Count > 0)
            {
                cantripDescriptionLabel.Text = formatSpellDescription(wm.DBManager.getSpell(availableCantrips.SelectedItem.ToString()));
            }
        }

        private void chosenCantrips_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chosenCantrips.SelectedItems.Count > 0)
            {
                cantripDescriptionLabel.Text = formatSpellDescription(wm.DBManager.getSpell(chosenCantrips.SelectedItem.ToString()));
            }
        }

        private void availableSpells_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (availableSpells.SelectedItems.Count > 0)
            {
                spellDescriptionLabel.Text = formatSpellDescription(wm.DBManager.getSpell(availableSpells.SelectedItem.ToString()));
            }
        }

        private void chosenSpells_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chosenSpells.SelectedItems.Count > 0)
            {
                spellDescriptionLabel.Text = formatSpellDescription(wm.DBManager.getSpell(chosenSpells.SelectedItem.ToString()));
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
