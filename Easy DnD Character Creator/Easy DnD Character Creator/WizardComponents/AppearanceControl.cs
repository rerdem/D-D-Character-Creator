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

        public bool isValid()
        {
            bool valid = true;

            if (string.IsNullOrEmpty(eyeColorBox.Text))
            {
                valid = false;
            }

            if (string.IsNullOrEmpty(skinColorBox.Text))
            {
                valid = false;
            }

            if (string.IsNullOrEmpty(hairColorBox.Text))
            {
                valid = false;
            }
            return valid;
        }

        public void populateForm()
        {
            eyeColorBox.Text = wm.Choices.EyeColor;
            skinColorBox.Text = wm.Choices.SkinColor;
            hairColorBox.Text = wm.Choices.HairColor;

            if (!visited)
            {
                visited = true;
            }
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
