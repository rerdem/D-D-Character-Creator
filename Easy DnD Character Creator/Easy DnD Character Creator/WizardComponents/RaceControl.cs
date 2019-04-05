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
    public partial class RaceControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private string lastUsedBooks;

        private List<Race> raceSourceList;
        private List<string> extraChoiceSource;

        public event EventHandler SubraceChanged;

        public RaceControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            lastUsedBooks = "";

            raceSourceList = new List<Race>();
            extraChoiceSource = new List<string>();

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

        public void populateForm()
        {
            if (!Visited || haveUsedBooksChanged())
            {
                populateRaceListBox();
            }

            if (Visited && !haveUsedBooksChanged())
            {
                loadPreviousSelection();
            }

            lastUsedBooks = wm.DBManager.getUsedBookString();
            Visited = true;
        }

        public void saveContent()
        {
            Race currentRace = raceListBox.SelectedItem as Race;
            Subrace currentSubrace = subraceListBox.SelectedItem as Subrace;

            if ((currentRace != null) && (currentSubrace != null))
            {
                if (currentSubrace.HasProficiencyChoice)
                {
                    currentSubrace.Proficiency = extraChoiceBox.SelectedItem.ToString();
                }
                else
                {
                    currentSubrace.Proficiency = "";
                }

                currentSubrace.HasExtraSpells = wm.DBManager.ExtraRaceChoiceData.hasExtraRaceCantripChoice(currentSubrace.Name);
                currentRace.setSelectedSubrace(currentSubrace);
                wm.Choices.RaceChoice = currentRace;
            }
        }

        public bool isValid()
        {
            if (subraceListBox.SelectedItems.Count > 0)
            {
                Subrace currentSubrace = subraceListBox.SelectedItem as Subrace;
                if (currentSubrace != null)
                {
                    if (currentSubrace.HasProficiencyChoice)
                    {
                        return ((subraceListBox.SelectedItems.Count > 0) && (extraChoiceBox.SelectedItems.Count > 0));
                    }
                }
            }

            return (subraceListBox.SelectedItems.Count > 0);
        }

        public string getInvalidElements()
        {
            string output = "";

            if (subraceListBox.SelectedItems.Count == 0)
            {
                output += raceBox.Text;
            }

            if (subraceListBox.SelectedItems.Count > 0)
            {
                Subrace currentSubrace = subraceListBox.SelectedItem as Subrace;
                if (currentSubrace != null)
                {
                    if (currentSubrace.HasProficiencyChoice && (extraChoiceBox.SelectedItems.Count == 0))
                    {
                        if (!string.IsNullOrEmpty(output))
                        {
                            output += ", ";
                        }
                        output += "bonus race proficiency";
                    }
                }
            }

            return output;
        }

        private void loadPreviousSelection()
        {
            Subrace savedSubrace = wm.Choices.RaceChoice.getSelectedSubrace();
            if (raceListBox.Items.Contains(wm.Choices.RaceChoice))
            {
                raceListBox.SetSelected(raceListBox.Items.IndexOf(wm.Choices.RaceChoice), true);
            }

            if (subraceListBox.Items.Contains(savedSubrace))
            {
                subraceListBox.SetSelected(subraceListBox.Items.IndexOf(savedSubrace), true);
            }
        }

        private void populateRaceListBox()
        {
            raceSourceList = wm.DBManager.RaceData.getRaces();

            raceListBox.BeginUpdate();
            raceListBox.DataSource = null;
            raceListBox.DataSource = raceSourceList;
            raceListBox.DisplayMember = "Name";
            raceListBox.EndUpdate();
        }

        private void populateSubraceListBox()
        {
            Race currentRace = raceListBox.SelectedItem as Race;
            if (currentRace != null)
            {
                subraceListBox.BeginUpdate();
                subraceListBox.DataSource = null;
                subraceListBox.DataSource = currentRace.Subraces;
                subraceListBox.DisplayMember = "Name";
                subraceListBox.EndUpdate();
            }
        }

        private void fillExtraChoiceListBox(string inputSubrace)
        {
            Subrace currentSubrace = subraceListBox.SelectedItem as Subrace;
            if (currentSubrace != null)
            {
                extraChoiceSource = wm.DBManager.RaceData.getExtraRaceProficiencies(currentSubrace.Name);

                extraChoiceBox.BeginUpdate();
                extraChoiceBox.DataSource = null;
                extraChoiceBox.DataSource = extraChoiceSource;
                extraChoiceBox.EndUpdate();

                if (extraChoiceBox.Items.Contains(wm.Choices.RaceChoice.getSelectedSubrace().Proficiency))
                {
                    extraChoiceBox.SetSelected(extraChoiceBox.Items.IndexOf(wm.Choices.RaceChoice.getSelectedSubrace().Proficiency), true);
                }
                //else
                //{
                //    extraChoiceBox.SetSelected(0, true);
                //}
            }
        }

        private bool haveUsedBooksChanged()
        {
            return (lastUsedBooks != wm.DBManager.getUsedBookString());
        }

        private void raceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateSubraceListBox();
        }

        private void subraceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Subrace currentSubrace = subraceListBox.SelectedItem as Subrace;
            if (currentSubrace != null)
            {
                descriptionLabel.Text = currentSubrace.Description;

                if (currentSubrace.HasProficiencyChoice)
                {
                    descriptionLabel.MaximumSize = new Size(520, descriptionLabel.MaximumSize.Height);
                    extraChoiceLayout.Visible = true;
                    fillExtraChoiceListBox(currentSubrace.Name);
                }
                else
                {
                    descriptionLabel.MaximumSize = new Size(650, descriptionLabel.MaximumSize.Height);
                    extraChoiceLayout.Visible = false;
                }
            }

            saveContent();

            OnSubraceChanged(null);
        }

        protected virtual void OnSubraceChanged(EventArgs e)
        {
            EventHandler handler = SubraceChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

    }
}
