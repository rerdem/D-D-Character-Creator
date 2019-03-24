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
    public enum PersonalityComponent { trait, ideal, bond, flaw };

    public partial class StoryControl : UserControl
    {
        public StoryControl()
        {
            InitializeComponent();
        }
    }
}
