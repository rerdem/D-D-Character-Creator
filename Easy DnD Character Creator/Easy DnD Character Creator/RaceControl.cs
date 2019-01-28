using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_DnD_Character_Creator
{
    public partial class RaceControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        public event EventHandler SubraceChanged;

        public RaceControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            visited = false;
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
            List<string> raceList = wm.DBManager.getRaces();

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
                raceListBox.SetSelected(0, true);
            }
        }

        private void fillSubraceListBox(string inputRace)
        {
            List<string> subraceList = wm.DBManager.getSubraces(inputRace);

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

        public void populateForm()
        {
            fillRaceListBox();
            if (!Visited)
            {
                Visited = true;
            }
        }

        public void saveContent()
        {
            wm.Choices.Race = raceListBox.SelectedItem.ToString();
            wm.Choices.Subrace = subraceListBox.SelectedItem.ToString();
        }

        private void raceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillSubraceListBox(raceListBox.SelectedItem.ToString());
        }

        private void subraceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            descriptionLabel.Text = wm.DBManager.getSubraceDescription(subraceListBox.SelectedItem.ToString());
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
