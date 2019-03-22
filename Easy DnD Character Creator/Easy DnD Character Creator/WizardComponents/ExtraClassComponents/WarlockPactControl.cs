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
    public partial class WarlockPactControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private int lastLevel;

        private List<WarlockPact> pactSource;
        private List<Spell> pactSpellSource;
        private List<int> pactSpellsOrderedSelection;

        public event EventHandler PactChosen;
        public event EventHandler PactSpellChosen;

        public WarlockPactControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            lastLevel = 0;

            pactSource = new List<WarlockPact>();
            pactSpellSource = new List<Spell>();
            pactSpellsOrderedSelection = new List<int>();

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

            if (pactListBox.SelectedItems.Count > 0)
            {
                WarlockPact currentPact = (WarlockPact)pactListBox.SelectedItem;
                if (currentPact.SpellAmount > 0)
                {
                    if (pactSpellListBox.SelectedItems.Count < currentPact.SpellAmount)
                    {
                        output = $"select {currentPact.SpellAmount - pactSpellListBox.SelectedItems.Count} more pact spell(s)";
                    }
                }
            }
            else
            {
                output = "select a pact";
            }

            return output;
        }

        public bool isValid()
        {
            bool isValid = false;

            if (pactListBox.SelectedItems.Count > 0)
            {
                WarlockPact currentPact = (WarlockPact)pactListBox.SelectedItem;
                if (currentPact.SpellAmount > 0)
                {
                    if (pactSpellListBox.SelectedItems.Count == currentPact.SpellAmount)
                    {
                        isValid = true;
                    }
                }
                else
                {
                    isValid = true;
                }
            }

            return isValid;
        }

        public void populateForm()
        {
            refreshPactList();

            if (Visited && !hasLevelChanged())
            {
                loadPreviousSelections();
            }

            lastLevel = wm.Choices.Level;
            Visited = true;
        }

        public void saveContent()
        {
            //save chosen pact
            WarlockPact currentPact = (WarlockPact)pactListBox.SelectedItem;
            wm.Choices.WarlockPactChoice = currentPact;

            //save pact spells, if applicable
            wm.Choices.WarlockPactSpells.Clear();
            if (currentPact.SpellAmount > 0)
            {
                foreach (Spell spell in pactSpellListBox.SelectedItems)
                {
                    if (spell != null)
                    {
                        wm.Choices.WarlockPactSpells.Add(spell);
                    }
                }
            }
        }

        private void loadPreviousSelections()
        {
            if (wm.DBManager.ExtraClassChoiceData.WarlockChoiceData.hasWarlockPact(wm.Choices.Class, wm.Choices.Level))
            {
                //load pact choice
                if (pactListBox.Items.IndexOf(wm.Choices.WarlockPactChoice) >= 0)
                {
                    pactListBox.SetSelected(pactListBox.Items.IndexOf(wm.Choices.WarlockPactChoice), true);
                }

                //load pact spells
                foreach (Spell spell in wm.Choices.WarlockPactSpells)
                {
                    if (pactSpellListBox.Items.IndexOf(spell) >= 0)
                    {
                        pactSpellListBox.SetSelected(pactSpellListBox.Items.IndexOf(spell), true);
                    }
                }
            }
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

                refreshSpellList(currentPact);
            }
            else
            {
                pactLayout.Size = new Size(843, pactLayout.Size.Height);
                pactSpellLayout.Visible = false;
            }
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

        private void refreshSpellList(WarlockPact currentPact)
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

        private bool hasLevelChanged()
        {
            return (lastLevel != wm.Choices.Level);
        }

        private void syncSpellSelectionOrder()
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

        private void pactListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pactListBox.SelectedItems.Count > 0)
            {
                WarlockPact currentPact = (WarlockPact)pactListBox.SelectedItem;
                if (currentPact != null)
                {
                    pactDescriptionLabel.Text = currentPact.Description;

                    togglePactSpellSelection();

                    OnPactChosen(null);
                }
            }
        }

        private void pactSpellListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            syncSpellSelectionOrder();

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

                    saveContent();
                    OnPactSpellChosen(null);
                }
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
    }
}
