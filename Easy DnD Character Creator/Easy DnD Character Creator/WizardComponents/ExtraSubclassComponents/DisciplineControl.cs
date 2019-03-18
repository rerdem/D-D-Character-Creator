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

namespace Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents
{
    public partial class DisciplineControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private int disciplinesKnown;
        private List<ElementalDiscipline> disciplineSource;
        private List<int> disciplineOrderedSelection;

        public event EventHandler DisciplineChosen;

        public DisciplineControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            disciplinesKnown = 0;
            disciplineSource = new List<ElementalDiscipline>();
            disciplineOrderedSelection = new List<int>();

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

            if (disciplineListBox.SelectedItems.Count < disciplinesKnown)
            {
                output += $"select {disciplinesKnown - disciplineListBox.SelectedItems.Count} more elemental discipline(s)";
            }

            return output;
        }

        public bool isValid()
        {
            return (disciplineListBox.SelectedItems.Count == disciplinesKnown);
        }

        public void populateForm()
        {
            resetContent();

            if (Visited)
            {
                loadPreviousSelections();
            }

            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.ChosenDisciplines.Clear();
            wm.Choices.MandatoryDisciplines.Clear();

            foreach (ElementalDiscipline discipline in disciplineListBox.SelectedItems)
            {
                if (discipline != null)
                {
                    wm.Choices.ChosenDisciplines.Add(discipline);
                }
            }

            wm.Choices.MandatoryDisciplines = wm.DBManager.ExtraSubclassChoiceData.ElementalDisciplineData.getMandatoryDisciplines(wm.Choices.Level);
        }

        private void loadPreviousSelections()
        {
            //load discipline choices
            foreach (ElementalDiscipline discipline in wm.Choices.ChosenDisciplines)
            {
                if (disciplineListBox.Items.IndexOf(discipline) >= 0)
                {
                    disciplineListBox.SetSelected(disciplineListBox.Items.IndexOf(discipline), true);
                }
            }
        }

        private void resetContent()
        {
            disciplinesKnown = wm.DBManager.ExtraSubclassChoiceData.ElementalDisciplineData.getDisciplineAmount(wm.Choices.Subclass, wm.Choices.Level);

            introLabel.Text = $"Please choose {disciplinesKnown} elemental discipline(s) below. Some options are mandatory and cannot be deselected.";

            disciplineSource = wm.DBManager.ExtraSubclassChoiceData.ElementalDisciplineData.getDisciplineOptions(wm.Choices.Level);

            disciplineListBox.BeginUpdate();
            disciplineListBox.DataSource = null;
            disciplineListBox.DataSource = disciplineSource;
            disciplineListBox.DisplayMember = "Name";
            disciplineListBox.EndUpdate();

            if (disciplinesKnown > 1)
            {
                disciplineListBox.SelectionMode = SelectionMode.MultiSimple;
            }
            else
            {
                disciplineListBox.SelectionMode = SelectionMode.One;
            }
        }

        private void syncDisciplineSelectionOrder()
        {
            //add new selected items
            foreach (int index in disciplineListBox.SelectedIndices)
            {
                if (!disciplineOrderedSelection.Contains(index))
                {
                    disciplineOrderedSelection.Add(index);
                }
            }

            //remove deselected items
            disciplineOrderedSelection.RemoveAll(index => !disciplineListBox.SelectedIndices.Contains(index));
        }

        private void disciplineListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            syncDisciplineSelectionOrder();
            if (disciplineListBox.SelectedItems.Count > 0)
            {
                if (disciplineListBox.SelectedIndices.Count <= disciplinesKnown)
                {
                    int lastSelectedIndex = disciplineOrderedSelection.ElementAt(disciplineOrderedSelection.Count - 1);
                    ElementalDiscipline currentDiscipline = (ElementalDiscipline)disciplineListBox.Items[lastSelectedIndex];
                    if (currentDiscipline != null)
                    {
                        descriptionLabel.Text = currentDiscipline.Description;
                    }
                }
                else
                {
                    int lastSelectedIndex = disciplineOrderedSelection.ElementAt(disciplineOrderedSelection.Count - 1);
                    disciplineListBox.SelectedIndices.Remove(lastSelectedIndex);
                }

                OnDisciplineChosen(null);
            }
        }

        protected virtual void OnDisciplineChosen(EventArgs e)
        {
            EventHandler handler = DisciplineChosen;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}