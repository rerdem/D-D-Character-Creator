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
    public partial class BackgroundControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        public BackgroundControl(WizardManager inputWizardManager)
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

        public string getInvalidElements()
        {
            string output = "";

            if (backgroundListBox.SelectedItems.Count != 1)
            {
                output += backgroundBox.Text;
            }

            return output;
        }

        public bool isValid()
        {
            return (backgroundListBox.SelectedItems.Count == 1);
        }

        public void populateForm()
        {
            fillBackgroundList();

            if (!Visited)
            {
                Visited = true;
            }
        }

        public void saveContent()
        {
            wm.Choices.Background = backgroundListBox.SelectedItem.ToString();
        }

        private void fillBackgroundList()
        {
            List<string> backgrounds = wm.DBManager.getBackgrounds();

            backgroundListBox.BeginUpdate();
            backgroundListBox.Items.Clear();
            foreach (string entry in backgrounds)
            {
                backgroundListBox.Items.Add(entry);
            }
            backgroundListBox.EndUpdate();

            if (backgroundListBox.Items.Contains(wm.Choices.Background))
            {
                backgroundListBox.SetSelected(backgroundListBox.Items.IndexOf(wm.Choices.Background), true);
            }
            else
            {
                backgroundListBox.SetSelected(0, true);
            }
        }

        private void backgroundListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            backgroundDescription.Text = wm.DBManager.getBackgroundDescription(backgroundListBox.SelectedItem.ToString());
            saveContent();
        }
    }
}
