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
    public partial class WarlockControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private int lastLevel;

        private int invocationsKnown;
        private int invocationSpellsKnown;

        private List<WarlockPact> pactSource;
        private List<Spell> pactSpellSource;
        private List<int> pactSpellsOrderedSelection;

        private List<EldritchInvocation> invocationSource;
        private List<int> invocationOrderedSelection;
        private List<Spell> invocationSpellSource;
        private List<int> invocationSpellsOrderedSelection;

        public event EventHandler PactChosen;
        public event EventHandler PactSpellChosen;
        public event EventHandler InvocationChosen;
        public event EventHandler InvocationSpellChosen;

        public WarlockControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            lastLevel = 0;

            invocationsKnown = 0;
            invocationSpellsKnown = 0;

            pactSource = new List<WarlockPact>();
            pactSpellSource = new List<Spell>();
            pactSpellsOrderedSelection = new List<int>();

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
            string missingElements = "";

            //warlock pact
            if (wm.Choices.ClassChoice.HasWarlockPact)
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                if (pactListBox.SelectedItems.Count > 0)
                {
                    WarlockPact currentPact = (WarlockPact)pactListBox.SelectedItem;
                    if (currentPact.SpellAmount > 0)
                    {
                        if (pactSpellListBox.SelectedItems.Count < currentPact.SpellAmount)
                        {
                            missingElements = $"select {currentPact.SpellAmount - pactSpellListBox.SelectedItems.Count} more pact spell(s)";
                        }
                    }
                }
                else
                {
                    missingElements = "select a pact";
                }
            }
            
            //warlock invocations
            if (wm.Choices.ClassChoice.HasEldritchInvocations)
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                if (invocationListBox.SelectedItems.Count < invocationsKnown)
                {
                    missingElements += $"select {invocationsKnown - invocationListBox.SelectedItems.Count} more eldritch invocation(s)";
                }

                if (hasInvocationSelectionSpellChoice())
                {
                    if (invocationSpellListBox.SelectedItems.Count < invocationSpellsKnown)
                    {
                        if (!string.IsNullOrEmpty(missingElements))
                        {
                            missingElements += ", ";
                        }
                        missingElements += $"select {invocationSpellsKnown - invocationSpellListBox.SelectedItems.Count} more invocation spell(s)";
                    }
                }

            }

            return missingElements;
        }

        public bool isValid()
        {
            bool isPactValid = false;
            bool isInvocationValid = false;

            //warlock pact
            if (wm.Choices.ClassChoice.HasWarlockPact)
            {
                if (pactListBox.SelectedItems.Count > 0)
                {
                    WarlockPact currentPact = (WarlockPact)pactListBox.SelectedItem;
                    if (currentPact.SpellAmount > 0)
                    {
                        if (pactSpellListBox.SelectedItems.Count == currentPact.SpellAmount)
                        {
                            isPactValid = true;
                        }
                    }
                    else
                    {
                        isPactValid = true;
                    }
                }
            }
            else
            {
                isPactValid = true;
            }

            //warlock invocations
            if (wm.Choices.ClassChoice.HasEldritchInvocations)
            {
                if (hasInvocationSelectionSpellChoice())
                {
                    isInvocationValid = ((invocationListBox.SelectedItems.Count == invocationsKnown) && (invocationSpellListBox.SelectedItems.Count == invocationSpellsKnown));
                }
                else
                {
                    isInvocationValid = (invocationListBox.SelectedItems.Count == invocationsKnown);
                }
            }
            else
            {
                isInvocationValid = true;
            }

            return isPactValid && isInvocationValid;
        }

        public void populateForm()
        {
            warlockLayout.Controls.Clear();
            
            //add and populate warlock pacts
            if (wm.Choices.ClassChoice.HasWarlockPact)
            {
                warlockLayout.Controls.Add(pactBox);
                refreshPactList();
                if (Visited && !hasLevelChanged())
                {
                    loadPreviousPactSelections();
                }
            }
            
            //add and populate eldritch invocations
            if (wm.Choices.ClassChoice.HasEldritchInvocations)
            {
                warlockLayout.Controls.Add(invocationBox);
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
                    loadPreviousInvocationSelections();
                }
            }

            lastLevel = wm.Choices.Level;
            Visited = true;
        }

        public void saveContent()
        {
            if (wm.Choices.ClassChoice.HasWarlockPact)
            {
                //save chosen pact
                WarlockPact currentPact = (WarlockPact)pactListBox.SelectedItem;
                wm.Choices.ClassChoice.WarlockPactChoice = currentPact;

                //save pact spells, if applicable
                wm.Choices.ClassChoice.WarlockPactSpells.Clear();
                if (currentPact.SpellAmount > 0)
                {
                    foreach (Spell spell in pactSpellListBox.SelectedItems)
                    {
                        if (spell != null)
                        {
                            wm.Choices.ClassChoice.WarlockPactSpells.Add(spell);
                        }
                    }
                }
            }
            else
            {
                wm.Choices.ClassChoice.WarlockPactChoice = new WarlockPact();
                wm.Choices.ClassChoice.WarlockPactSpells.Clear();
            }

            if (wm.Choices.ClassChoice.HasEldritchInvocations)
            {
                wm.Choices.ClassChoice.WarlockInvocations.Clear();
                wm.Choices.ClassChoice.WarlockInvocationSkills.Clear();
                wm.Choices.ClassChoice.WarlockInvocationSpells.Clear();

                //save chosen invocations and possibly gained skills
                foreach (EldritchInvocation invocation in invocationListBox.SelectedItems)
                {
                    if (invocation != null)
                    {
                        wm.Choices.ClassChoice.WarlockInvocations.Add(invocation);

                        if (invocation.HasSkillGain)
                        {
                            wm.Choices.ClassChoice.WarlockInvocationSkills.AddRange(wm.DBManager.ExtraClassChoiceData.WarlockChoiceData.getInvocationGainedSkills(invocation));
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
                            wm.Choices.ClassChoice.WarlockInvocationSpells.Add(spell);
                        }
                    }
                }
            }
            else
            {
                wm.Choices.ClassChoice.WarlockInvocations.Clear();
                wm.Choices.ClassChoice.WarlockInvocationSpells.Clear();
                wm.Choices.ClassChoice.WarlockInvocationSkills.Clear();
            }
        }

        private void loadPreviousPactSelections()
        {
            if (wm.Choices.ClassChoice.HasWarlockPact)
            {
                //load pact choice
                if (pactListBox.Items.IndexOf(wm.Choices.ClassChoice.WarlockPactChoice) >= 0)
                {
                    pactListBox.SetSelected(pactListBox.Items.IndexOf(wm.Choices.ClassChoice.WarlockPactChoice), true);
                }

                //load pact spells
                foreach (Spell spell in wm.Choices.ClassChoice.WarlockPactSpells)
                {
                    if (pactSpellListBox.Items.IndexOf(spell) >= 0)
                    {
                        pactSpellListBox.SetSelected(pactSpellListBox.Items.IndexOf(spell), true);
                    }
                }
            }
        }

        private void loadPreviousInvocationSelections()
        {
            //load invocation choices
            foreach (EldritchInvocation invocation in wm.Choices.ClassChoice.WarlockInvocations)
            {
                if (invocationListBox.Items.IndexOf(invocation) >= 0)
                {
                    invocationListBox.SetSelected(invocationListBox.Items.IndexOf(invocation), true);
                }
            }

            //load invocation spells
            foreach (Spell spell in wm.Choices.ClassChoice.WarlockInvocationSpells)
            {
                if (invocationSpellListBox.Items.IndexOf(spell) >= 0)
                {
                    invocationSpellListBox.SetSelected(invocationSpellListBox.Items.IndexOf(spell), true);
                }
            }
        }

        private bool hasLevelChanged()
        {
            return (lastLevel != wm.Choices.Level);
        }

        private void refreshPactList()
        {
            pactSource = wm.DBManager.ExtraClassChoiceData.WarlockChoiceData.getWarlockPacts();

            pactListBox.BeginUpdate();
            pactListBox.DataSource = null;
            pactListBox.DataSource = pactSource;
            pactListBox.DisplayMember = "Name";
            pactListBox.EndUpdate();
        }

        private void togglePactSpellSelection()
        {
            WarlockPact currentPact = (WarlockPact)pactListBox.SelectedItem;
            if (currentPact.SpellAmount > 0)
            {
                pactLayout.Size = new Size(425, pactLayout.Size.Height);
                pactSpellLayout.Visible = true;

                pactSpellIntroLabel.Text = $"Choose {currentPact.SpellAmount} spell(s):";
                if (currentPact.SpellAmount > 1)
                {
                    pactSpellListBox.SelectionMode = SelectionMode.MultiSimple;
                }
                else
                {
                    pactSpellListBox.SelectionMode = SelectionMode.One;
                }

                refreshPactSpellList(currentPact);
            }
            else
            {
                pactLayout.Size = new Size(843, pactLayout.Size.Height);
                pactSpellLayout.Visible = false;
            }
        }

        public void refreshInvocationList()
        {
            //compile list of all known spells
            List<Spell> knownSpells = new List<Spell>();
            knownSpells.AddRange(wm.Choices.Spells);
            foreach (Spell spell in wm.Choices.RaceSpells)
            {
                if (!string.IsNullOrEmpty(spell.Name))
                {
                    knownSpells.Add(spell);
                }
            }
            if (wm.Choices.ClassChoice.HasWarlockPact)
            {
                foreach (Spell spell in pactSpellListBox.SelectedItems)
                {
                    if (spell != null)
                    {
                        knownSpells.Add(spell);
                    }
                }
            }

            //refresh list
            WarlockPact currentPact = (WarlockPact)pactListBox.SelectedItem;
            if ((currentPact != null) && (wm.Choices.ClassChoice.HasWarlockPact))
            {
                invocationSource = wm.DBManager.ExtraClassChoiceData.WarlockChoiceData.getEldritchInvocations(knownSpells, currentPact, wm.Choices.Level);
            }
            else
            {
                invocationSource = wm.DBManager.ExtraClassChoiceData.WarlockChoiceData.getEldritchInvocations(knownSpells, new WarlockPact(), wm.Choices.Level);
            }

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

                refreshInvocationSpellList(spellChoiceInvocation);
            }
            else
            {
                invocationLayout.Size = new Size(843, invocationLayout.Size.Height);
                invocationSpellLayout.Visible = false;
            }
        }

        private void refreshPactSpellList(WarlockPact currentPact)
        {
            pactSpellSource = wm.DBManager.ExtraClassChoiceData.WarlockChoiceData.getPactSpells(currentPact);

            //remove already chosen spells
            foreach (Spell spell in wm.Choices.Spells)
            {
                pactSpellSource.Remove(spell);
            }

            foreach (Spell spell in wm.Choices.RaceSpells)
            {
                if (!string.IsNullOrEmpty(spell.Name))
                {
                    pactSpellSource.Remove(spell);
                }
            }

            pactSpellListBox.BeginUpdate();
            pactSpellListBox.DataSource = null;
            pactSpellListBox.DataSource = pactSpellSource;
            pactSpellListBox.DisplayMember = "Name";
            pactSpellListBox.EndUpdate();
        }

        private void refreshInvocationSpellList(EldritchInvocation selectedInvocation)
        {
            invocationSpellSource = wm.DBManager.ExtraClassChoiceData.WarlockChoiceData.getInvocationSpellOptions(selectedInvocation);

            //remove already chosen spells
            foreach (Spell spell in wm.Choices.Spells)
            {
                invocationSpellSource.Remove(spell);
            }

            foreach (Spell spell in wm.Choices.RaceSpells)
            {
                if (!string.IsNullOrEmpty(spell.Name))
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

        private void syncPactSpellSelectionOrder()
        {
            //add new selected items
            foreach (int index in pactSpellListBox.SelectedIndices)
            {
                if (!pactSpellsOrderedSelection.Contains(index))
                {
                    pactSpellsOrderedSelection.Add(index);
                }
            }

            //remove deselected items
            pactSpellsOrderedSelection.RemoveAll(index => !pactSpellListBox.SelectedIndices.Contains(index));
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

        private void pactListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pactListBox.SelectedItems.Count > 0)
            {
                WarlockPact currentPact = (WarlockPact)pactListBox.SelectedItem;
                if (currentPact != null)
                {
                    pactDescriptionLabel.Text = currentPact.Description.Replace("<br>", Environment.NewLine);

                    togglePactSpellSelection();

                    refreshInvocationList();

                    OnPactChosen(null);
                }
            }
        }

        private void pactSpellListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            syncPactSpellSelectionOrder();

            WarlockPact currentPact = (WarlockPact)pactListBox.SelectedItem;
            if (currentPact != null)
            {
                if (pactSpellListBox.SelectedIndices.Count > 0)
                {
                    if (pactSpellListBox.SelectedIndices.Count <= currentPact.SpellAmount)
                    {
                        int lastSelectedIndex = pactSpellsOrderedSelection.ElementAt(pactSpellsOrderedSelection.Count - 1);
                        Spell currentSpell = (Spell)pactSpellListBox.Items[lastSelectedIndex];
                        if (currentSpell != null)
                        {
                            pactSpellDescriptionLabel.Text = SpellFormatter.formatSpellDescription(currentSpell);
                        }
                    }
                    else
                    {
                        int lastSelectedIndex = pactSpellsOrderedSelection.ElementAt(pactSpellsOrderedSelection.Count - 1);
                        pactSpellListBox.SelectedIndices.Remove(lastSelectedIndex);
                    }

                    refreshInvocationList();
                    OnPactSpellChosen(null);
                }
            }
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

        protected virtual void OnPactChosen(EventArgs e)
        {
            EventHandler handler = PactChosen;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnPactSpellChosen(EventArgs e)
        {
            EventHandler handler = PactSpellChosen;
            if (handler != null)
            {
                handler(this, e);
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