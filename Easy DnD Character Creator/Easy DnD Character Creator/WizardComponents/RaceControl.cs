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
    public partial class RaceControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        public event EventHandler SubraceChanged;

        public RaceControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;
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

        private void fillRaceListBox()
        {
            List<string> raceList = wm.DBManager.RaceData.getRaces();

            raceListBox.BeginUpdate();
            raceListBox.Items.Clear();
            foreach (string race in raceList)
            {
                raceListBox.Items.Add(race);
            }
            raceListBox.EndUpdate();

            if (raceListBox.Items.Contains(wm.Choices.Race))
            {
                raceListBox.SetSelected(raceListBox.Items.IndexOf(wm.Choices.Race), true);
            }
            else
            {
                if (raceListBox.Items.Count > 0)
                {
                    raceListBox.SetSelected(0, true);
                }
            }
        }

        private void fillSubraceListBox(string inputRace)
        {
            List<string> subraceList = wm.DBManager.RaceData.getSubraces(inputRace);

            subraceListBox.BeginUpdate();
            subraceListBox.Items.Clear();
            foreach (string subrace in subraceList)
            {
                subraceListBox.Items.Add(subrace);
            }
            subraceListBox.EndUpdate();

            if (subraceListBox.Items.Contains(wm.Choices.Subrace))
            {
                subraceListBox.SetSelected(subraceListBox.Items.IndexOf(wm.Choices.Subrace), true);
            }
            else
            {
                subraceListBox.SetSelected(0, true);
            }
        }

        private void fillExtraChoiceListBox(string inputSubrace)
        {
            List<string> choiceList = wm.DBManager.RaceData.getExtraRaceProficiencies(subraceListBox.SelectedItem.ToString());

            extraChoiceBox.BeginUpdate();
            extraChoiceBox.Items.Clear();
            foreach (string entry in choiceList)
            {
                extraChoiceBox.Items.Add(entry);
            }
            extraChoiceBox.EndUpdate();

            if (extraChoiceBox.Items.Contains(wm.Choices.RaceProficiency))
            {
                extraChoiceBox.SetSelected(extraChoiceBox.Items.IndexOf(wm.Choices.RaceProficiency), true);
            }
            else
            {
                extraChoiceBox.SetSelected(0, true);
            }
        }

        public void populateForm()
        {
            fillRaceListBox();
            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.Race = raceListBox.SelectedItem.ToString();
            wm.Choices.Subrace = subraceListBox.SelectedItem.ToString();

            if (wm.DBManager.RaceData.subraceHasExtraChoice(subraceListBox.SelectedItem.ToString()))
            {
                wm.Choices.RaceProficiency = extraChoiceBox.SelectedItem.ToString();
            }
            else
            {
                wm.Choices.RaceProficiency = "";
            }

            wm.Choices.HasExtraRaceChoice = wm.DBManager.ExtraRaceChoiceData.hasExtraRaceChoices(subraceListBox.SelectedItem.ToString());
        }

        private void raceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillSubraceListBox(raceListBox.SelectedItem.ToString());
        }

        private void subraceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            descriptionLabel.Text = wm.DBManager.RaceData.getSubraceDescription(subraceListBox.SelectedItem.ToString());

            if (wm.DBManager.RaceData.subraceHasExtraChoice(subraceListBox.SelectedItem.ToString()))
            {
                descriptionLabel.MaximumSize = new Size(520, descriptionLabel.MaximumSize.Height);
                extraChoiceLayout.Visible = true;
                fillExtraChoiceListBox(subraceListBox.SelectedItem.ToString());
            }
            else
            {
                descriptionLabel.MaximumSize = new Size(650, descriptionLabel.MaximumSize.Height);
                extraChoiceLayout.Visible = false;
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

        public bool isValid()
        {
            if (subraceListBox.SelectedItems.Count > 0)
            {
                if (wm.DBManager.RaceData.subraceHasExtraChoice(subraceListBox.SelectedItem.ToString()))
                {
                    return ((subraceListBox.SelectedItems.Count > 0) && (extraChoiceBox.SelectedItems.Count > 0));
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

            if (subraceListBox.SelectedItems.Count == 0)
            {
                if (!string.IsNullOrEmpty(output))
                {
                    output += ", ";
                }
                output += "bonus race proficiency";
            }

            return output;
        }
    }
}
