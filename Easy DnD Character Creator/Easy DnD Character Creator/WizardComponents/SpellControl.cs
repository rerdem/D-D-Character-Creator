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

        private int CantripsKnown { get; set; }
        private int SpellsKnown { get; set; }

        public SpellControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            visited = false;

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
            return "";
        }

        public bool isValid()
        {
            return ((chosenCantrips.Items.Count == CantripsKnown) && (chosenSpells.Items.Count == SpellsKnown));
        }

        public void populateForm()
        {
            resetSpells();


            if (!visited)
            {
                visited = true;
            }
        }

        public void saveContent()
        {
            
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

        private void manageButtonClickability()
        {

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

        private void cantripAddButton_Click(object sender, EventArgs e)
        {

        }

        private void cantripRemoveButton_Click(object sender, EventArgs e)
        {

        }

        private void spellAddButton_Click(object sender, EventArgs e)
        {

        }

        private void spellRemoveButton_Click(object sender, EventArgs e)
        {

        }

        private void availableCantrips_SelectedIndexChanged(object sender, EventArgs e)
        {
            cantripDescriptionLabel.Text = formatSpellDescription(wm.DBManager.getSpell(availableCantrips.SelectedItem.ToString()));
        }

        private void chosenCantrips_SelectedIndexChanged(object sender, EventArgs e)
        {
            cantripDescriptionLabel.Text = formatSpellDescription(wm.DBManager.getSpell(chosenCantrips.SelectedItem.ToString()));
        }

        private void availableSpells_SelectedIndexChanged(object sender, EventArgs e)
        {
            spellDescriptionLabel.Text = formatSpellDescription(wm.DBManager.getSpell(availableSpells.SelectedItem.ToString()));
        }

        private void chosenSpells_SelectedIndexChanged(object sender, EventArgs e)
        {
            spellDescriptionLabel.Text = formatSpellDescription(wm.DBManager.getSpell(chosenSpells.SelectedItem.ToString()));
        }
    }
}
