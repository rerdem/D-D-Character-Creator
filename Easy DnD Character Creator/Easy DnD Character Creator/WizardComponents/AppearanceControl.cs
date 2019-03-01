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
    public partial class AppearanceControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        public event EventHandler AppearanceChanged;

        public AppearanceControl(WizardManager inputWizardManager)
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

        public bool isValid()
        {
            bool valid = false;

            if ((!string.IsNullOrEmpty(eyeColorBox.Text)) && (!string.IsNullOrEmpty(skinColorBox.Text)) && (!string.IsNullOrEmpty(hairColorBox.Text)))
            {
                valid = true;
            }

            return valid;
        }

        public string getInvalidElements()
        {
            string output = "";

            if (string.IsNullOrEmpty(eyeColorBox.Text))
            {
                output += "eye color";
            }

            if (string.IsNullOrEmpty(skinColorBox.Text))
            {
                if (!string.IsNullOrEmpty(output))
                {
                    output += ", ";
                }

                output += "skin color";
            }

            if (string.IsNullOrEmpty(hairColorBox.Text))
            {
                if (!string.IsNullOrEmpty(output))
                {
                    output += ", ";
                }

                output += "hair color";
            }

            return output;
        }

        public void populateForm()
        {
            if (Visited)
            {
                eyeColorBox.Text = wm.Choices.EyeColor;
                skinColorBox.Text = wm.Choices.SkinColor;
                hairColorBox.Text = wm.Choices.HairColor;
            }

            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.EyeColor = eyeColorBox.Text;
            wm.Choices.SkinColor = skinColorBox.Text;
            wm.Choices.HairColor = hairColorBox.Text;
        }

        protected virtual void OnAppearanceChanged(EventArgs e)
        {
            EventHandler handler = AppearanceChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void eyeColorBox_TextChanged(object sender, EventArgs e)
        {
            OnAppearanceChanged(null);
        }

        private void skinColorBox_TextChanged(object sender, EventArgs e)
        {
            OnAppearanceChanged(null);
        }

        private void hairColorBox_TextChanged(object sender, EventArgs e)
        {
            OnAppearanceChanged(null);
        }       
    }
}
