using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_DnD_Character_Creator.WizardComponents.StoryComponents
{
    public partial class BackstoryControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        public BackstoryControl(WizardManager inputWizardManager)
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

        public string getInvalidElements()
        {
            return "";
        }

        public bool isValid()
        {
            //writing a backstory is optional
            return true;
        }

        public void populateForm()
        {
            if (Visited)
            {
                storyTextBox.Text = wm.Choices.Backstory;
            }

            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.Backstory = storyTextBox.Text;
        }
    }
}
