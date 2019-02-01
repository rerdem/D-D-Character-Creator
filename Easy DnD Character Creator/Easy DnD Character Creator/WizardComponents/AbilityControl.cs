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
    public partial class AbilityControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        public AbilityControl(WizardManager inputWizardManager)
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

        private void abilityScoreHoldingLayout_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void abilityScoreHoldingLayout_DragDrop(object sender, DragEventArgs e)
        {
            ((Label)e.Data.GetData(typeof(Label))).Parent = (Panel)sender;
        }

        private void dropzone_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dropzone_DragDrop(object sender, DragEventArgs e)
        {
            ((Label)e.Data.GetData(typeof(Label))).Parent = (Panel)sender;
        }

        private void abilityScore1_MouseDown(object sender, MouseEventArgs e)
        {
            abilityScore1.DoDragDrop(abilityScore1, DragDropEffects.Move);
        }

        private void abilityScore2_MouseDown(object sender, MouseEventArgs e)
        {
            abilityScore2.DoDragDrop(abilityScore2, DragDropEffects.Move);
        }

        private void abilityScore3_MouseDown(object sender, MouseEventArgs e)
        {
            abilityScore3.DoDragDrop(abilityScore3, DragDropEffects.Move);
        }

        private void abilityScore4_MouseDown(object sender, MouseEventArgs e)
        {
            abilityScore4.DoDragDrop(abilityScore4, DragDropEffects.Move);
        }

        private void abilityScore5_MouseDown(object sender, MouseEventArgs e)
        {
            abilityScore5.DoDragDrop(abilityScore5, DragDropEffects.Move);
        }

        private void abilityScore6_MouseDown(object sender, MouseEventArgs e)
        {
            abilityScore6.DoDragDrop(abilityScore6, DragDropEffects.Move);
        }
    }
}
