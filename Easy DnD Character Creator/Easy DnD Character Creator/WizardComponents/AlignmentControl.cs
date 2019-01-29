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
    public partial class AlignmentControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        public AlignmentControl(WizardManager inputWizardManager)
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

        public void populateForm()
        {
            generalAlignmentLabel.Text = wm.DBManager.getGeneralAlignmentDescription();
            raceAlignmentLabel.Text = wm.DBManager.getSubraceAlignmentDescription(wm.Choices.Subrace);

            //fill alignment listBoxes
            lawBox.BeginUpdate();
            lawBox.Items.Clear();
            foreach (string entry in wm.DBManager.getLawAlignments())
            {
                lawBox.Items.Add(entry);
            }
            lawBox.EndUpdate();

            moralityBox.BeginUpdate();
            moralityBox.Items.Clear();
            foreach (string entry in wm.DBManager.getMoralityAlignments())
            {
                moralityBox.Items.Add(entry);
            }
            moralityBox.EndUpdate();
            
            //either load last selected entry or first entry for both listboxes
            if (lawBox.Items.Contains(wm.Choices.LawAlignment))
            {
                lawBox.SetSelected(lawBox.Items.IndexOf(wm.Choices.LawAlignment), true);
            }
            else
            {
                lawBox.SetSelected(0, true);
            }
            
            if (moralityBox.Items.Contains(wm.Choices.MoralityAlignment))
            {
                moralityBox.SetSelected(moralityBox.Items.IndexOf(wm.Choices.MoralityAlignment), true);
            }
            else
            {
                moralityBox.SetSelected(0, true);
            }
            

            if (!Visited)
            {
                Visited = true;
            }
        }

        public void updateRaceAlignmentDescription(string inputSubrace)
        {
            raceAlignmentLabel.Text = wm.DBManager.getSubraceAlignmentDescription(inputSubrace);
        }

        public void saveContent()
        {
            wm.Choices.LawAlignment = lawBox.SelectedItem.ToString();
            wm.Choices.MoralityAlignment = moralityBox.SelectedItem.ToString();
        }

        private void lawBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((lawBox.SelectedItem != null) && (moralityBox.SelectedItem != null))
            {
                chosenAlignmentLabel.Text = wm.DBManager.getAlignmentDescription(lawBox.SelectedItem.ToString(), moralityBox.SelectedItem.ToString());
                saveContent();
            }
        }

        private void moralityBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((lawBox.SelectedItem != null) && (moralityBox.SelectedItem != null))
            {
                chosenAlignmentLabel.Text = wm.DBManager.getAlignmentDescription(lawBox.SelectedItem.ToString(), moralityBox.SelectedItem.ToString());
                saveContent();
            }
        }

        public bool isValid()
        {
            return ((lawBox.SelectedItems.Count > 0) && (moralityBox.SelectedItems.Count > 0));
        }
    }
}
