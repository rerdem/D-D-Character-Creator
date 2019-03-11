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

namespace Easy_DnD_Character_Creator.WizardComponents.ExtraClassComponents
{
    public partial class WarlockInvocationControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private int invocationsKnown;
        private int invocationSpellsKnown;

        private List<EldritchInvocation> invocationSource;
        private List<int> invocationOrderedSelection;
        private List<Spell> invocationSpellSource;
        private List<int> invocationSpellsOrderedSelection;

        public event EventHandler InvocationChosen;
        public event EventHandler InvocationSpellChosen;

        public WarlockInvocationControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            invocationsKnown = 0;
            invocationSpellsKnown = 0;

            invocationSource = new List<EldritchInvocation>();
            invocationOrderedSelection = new List<int>();
            invocationSpellSource = new List<Spell>();
            invocationSpellsOrderedSelection = new List<int>();

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

            if (invocationListBox.SelectedItems.Count < invocationsKnown)
            {
                output += $"select {invocationsKnown - invocationListBox.SelectedItems.Count} more eldritch invocation(s)";
            }

            if (hasInvocationSelectionSpellChoice())
            {
                if (invocationSpellListBox.SelectedItems.Count < invocationSpellsKnown)
                {
                    if (!string.IsNullOrEmpty(output))
                    {
                        output += ", ";
                    }
                    output += $"select {invocationSpellsKnown - invocationSpellListBox.SelectedItems.Count} more invocation spell(s)";
                }
            }

            return output;
        }

        public bool isValid()
        {
            bool isValid = false;

            if (hasInvocationSelectionSpellChoice())
            {
                isValid = ((invocationListBox.SelectedItems.Count == invocationsKnown) && (invocationSpellListBox.SelectedItems.Count == invocationSpellsKnown));
            }
            else
            {
                isValid = (invocationListBox.SelectedItems.Count == invocationsKnown);
            }

            return isValid;
        }

        public void populateForm()
        {
            invocationsKnown = wm.DBManager.ExtraClassChoiceData.WarlockChoiceData.getEldritchInvocationAmount(wm.Choices.Level);
            if (invocationsKnown > 1)
            {
                invocationListBox.SelectionMode = SelectionMode.MultiSimple;
            }
            else
            {
                invocationListBox.SelectionMode = SelectionMode.One;
            }

            invocationSpellsKnown = 0;
            refreshInvocationList();

            if (Visited)
            {
                loadPreviousSelections();
            }

            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.WarlockInvocations.Clear();
            wm.Choices.WarlockInvocationSkills.Clear();
            wm.Choices.WarlockInvocationSpells.Clear();

            //save chosen invocations and possibly gained skills
            foreach (EldritchInvocation invocation in invocationListBox.SelectedItems)
            {
                if (invocation != null)
                {
                    wm.Choices.WarlockInvocations.Add(invocation);

                    if (invocation.HasSkillGain)
                    {
                        wm.Choices.WarlockInvocationSkills.AddRange(wm.DBManager.ExtraClassChoiceData.WarlockChoiceData.getInvocationGainedSkills(invocation));
                    }
                }
            }

            //save invocation spells, if applicable
            if (hasInvocationSelectionSpellChoice())
            {
                foreach (Spell spell in invocationSpellListBox.SelectedItems)
                {
                    if (spell != null)
                    {
                        wm.Choices.WarlockInvocationSpells.Add(spell);
                    }
                }
            }
        }

        private void loadPreviousSelections()
        {
            //load invocation choices
            foreach (EldritchInvocation invocation in wm.Choices.WarlockInvocations)
            {
                if (invocationListBox.Items.IndexOf(invocation) >= 0)
                {
                    invocationListBox.SetSelected(invocationListBox.Items.IndexOf(invocation), true);
                }
            }
            
            //load invocation spells
            foreach (Spell spell in wm.Choices.WarlockInvocationSpells)
            {
                if (invocationSpellListBox.Items.IndexOf(spell) >= 0)
                {
                    invocationSpellListBox.SetSelected(invocationSpellListBox.Items.IndexOf(spell), true);
                }
            }
        }

        public void refreshInvocationList()
        {
            //compile list of all known spells
            List<Spell> knownSpells = new List<Spell>();
            knownSpells.AddRange(wm.Choices.Spells);
            foreach (object item in wm.Choices.ExtraRaceChoices)
            {
                Spell spell = item as Spell;

                if (spell != null)
                {
                    knownSpells.Add(spell);
                }
            }
            if (wm.DBManager.ExtraClassChoiceData.WarlockChoiceData.hasWarlockPact(wm.Choices.Class, wm.Choices.Level))
            {
                knownSpells.AddRange(wm.Choices.WarlockPactSpells);
            }

            //refresh list
            invocationSource = wm.DBManager.ExtraClassChoiceData.WarlockChoiceData.getEldritchInvocations(knownSpells, wm.Choices.WarlockPactChoice, wm.Choices.Level);

            invocationListBox.BeginUpdate();
            invocationListBox.DataSource = null;
            invocationListBox.DataSource = invocationSource;
            invocationListBox.DisplayMember = "Name";
            invocationListBox.EndUpdate();
        }

        private void toggleInvocationSpellSelection()
        {
            bool activateSpellSelection = false;
            int spellChoiceInvocationIndex = 0;
            foreach (EldritchInvocation invocation in invocationListBox.SelectedItems)
            {
                if (invocation != null)
                {
                    if (invocation.HasSpellChoice)
                    {
                        activateSpellSelection = true;
                        spellChoiceInvocationIndex = invocationListBox.Items.IndexOf(invocation);
                        break;
                    }
                }
            }

            if (activateSpellSelection)
            {
                EldritchInvocation spellChoiceInvocation = (EldritchInvocation)invocationListBox.Items[spellChoiceInvocationIndex];
                invocationSpellsKnown = wm.DBManager.ExtraClassChoiceData.WarlockChoiceData.invocationSpellChoiceAmount(spellChoiceInvocation);

                invocationLayout.Size = new Size(425, invocationLayout.Size.Height);
                invocationSpellLayout.Visible = true;

                invocationSpellIntroLabel.Text = $"Choose {invocationSpellsKnown} spell(s):";
                if (invocationSpellsKnown > 1)
                {
                    invocationSpellListBox.SelectionMode = SelectionMode.MultiSimple;
                }
                else
                {
                    invocationSpellListBox.SelectionMode = SelectionMode.One;
                }

                refreshSpellList(spellChoiceInvocation);
            }
            else
            {
                invocationLayout.Size = new Size(843, invocationLayout.Size.Height);
                invocationSpellLayout.Visible = false;
            }
        }

        private void refreshSpellList(EldritchInvocation selectedInvocation)
        {
            invocationSpellSource = wm.DBManager.ExtraClassChoiceData.WarlockChoiceData.getInvocationSpellOptions(selectedInvocation);

            //remove already chosen spells
            foreach (Spell spell in wm.Choices.Spells)
            {
                invocationSpellSource.Remove(spell);
            }

            foreach (Spell spell in wm.Choices.ExtraRaceChoices)
            {
                if (spell != null)
                {
                    invocationSpellSource.Remove(spell);
                }
            }

            invocationSpellListBox.BeginUpdate();
            invocationSpellListBox.DataSource = null;
            invocationSpellListBox.DataSource = invocationSpellSource;
            invocationSpellListBox.DisplayMember = "Name";
            invocationSpellListBox.EndUpdate();
        }

        private bool hasInvocationSelectionSpellChoice()
        {
            bool hasSpellChoice = false;

            foreach (EldritchInvocation invocation in invocationListBox.SelectedItems)
            {
                if (invocation != null)
                {
                    if (invocation.HasSpellChoice)
                    {
                        hasSpellChoice = true;
                        break;
                    }
                }
            }

            return hasSpellChoice;
        }

        private bool hasInvocationSelectionSkillGain()
        {
            bool hasSkillGain = false;

            foreach (EldritchInvocation invocation in invocationListBox.SelectedItems)
            {
                if (invocation != null)
                {
                    if (invocation.HasSkillGain)
                    {
                        hasSkillGain = true;
                        break;
                    }
                }
            }

            return hasSkillGain;
        }

        private void syncInvocationSelectionOrder()
        {
            //add new selected items
            foreach (int index in invocationListBox.SelectedIndices)
            {
                if (!invocationOrderedSelection.Contains(index))
                {
                    invocationOrderedSelection.Add(index);
                }
            }

            //remove deselected items
            invocationOrderedSelection.RemoveAll(index => !invocationListBox.SelectedIndices.Contains(index));
        }

        private void syncInvocationSpellSelectionOrder()
        {
            //add new selected items
            foreach (int index in invocationSpellListBox.SelectedIndices)
            {
                if (!invocationSpellsOrderedSelection.Contains(index))
                {
                    invocationSpellsOrderedSelection.Add(index);
                }
            }

            //remove deselected items
            invocationSpellsOrderedSelection.RemoveAll(index => !invocationSpellListBox.SelectedIndices.Contains(index));
        }

        private void invocationListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            syncInvocationSelectionOrder();
            if (invocationListBox.SelectedItems.Count > 0)
            {
                if (invocationListBox.SelectedIndices.Count <= invocationsKnown)
                {
                    int lastSelectedIndex = invocationOrderedSelection.ElementAt(invocationOrderedSelection.Count - 1);
                    EldritchInvocation currentInvocation = (EldritchInvocation)invocationListBox.Items[lastSelectedIndex];
                    if (currentInvocation != null)
                    {
                        invocationDescriptionLabel.Text = currentInvocation.Description;

                        toggleInvocationSpellSelection();
                    }
                }
                else
                {
                    int lastSelectedIndex = invocationOrderedSelection.ElementAt(invocationOrderedSelection.Count - 1);
                    invocationListBox.SelectedIndices.Remove(lastSelectedIndex);
                }

                OnInvocationChosen(null);
            }
        }

        private void invocationSpellListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            syncInvocationSpellSelectionOrder();
            if (invocationSpellListBox.SelectedIndices.Count > 0)
            {
                if (invocationSpellListBox.SelectedIndices.Count <= invocationSpellsKnown)
                {
                    int lastSelectedIndex = invocationSpellsOrderedSelection.ElementAt(invocationSpellsOrderedSelection.Count - 1);
                    Spell currentSpell = (Spell)invocationSpellListBox.Items[lastSelectedIndex];
                    if (currentSpell != null)
                    {
                        invocationSpellDescriptionLabel.Text = SpellFormatter.formatSpellDescription(currentSpell);
                    }
                }
                else
                {
                    int lastSelectedIndex = invocationSpellsOrderedSelection.ElementAt(invocationSpellsOrderedSelection.Count - 1);
                    invocationSpellListBox.SelectedIndices.Remove(lastSelectedIndex);
                }

                OnInvocationSpellChosen(null);
            }
        }

        protected virtual void OnInvocationChosen(EventArgs e)
        {
            EventHandler handler = InvocationChosen;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnInvocationSpellChosen(EventArgs e)
        {
            EventHandler handler = InvocationSpellChosen;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}