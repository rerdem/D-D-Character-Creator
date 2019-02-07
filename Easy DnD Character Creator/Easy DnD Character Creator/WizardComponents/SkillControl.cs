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
    public partial class SkillControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        public SkillControl(WizardManager inputWizardManager)
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
            return "";
        }

        public bool isValid()
        {
            return true;
        }

        public void populateForm()
        {
            
        }

        public void saveContent()
        {
            
        }
    }
}
