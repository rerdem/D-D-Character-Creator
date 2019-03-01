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
    public partial class AgeControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        public AgeControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;
            InitializeComponent();
        }

        public bool Visited {
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

            if (ageValue.Value < 0)
            {
                output += ageBox.Text;
            }

            return output;
        }

        public bool isValid()
        {
            return (ageValue.Value > 0);
        }

        public void populateForm()
        {
            ageValue.Value = wm.Choices.Age;

            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.Age = (int) ageValue.Value;
        }

        public void updateRaceAgeDescription(string inputSubrace)
        {
            ageLabel.Text = wm.DBManager.AppearanceData.getAgeDescription(inputSubrace);
        }
    }
}
