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
    public partial class BodyControl : UserControl, IWizardControl
    {
        private WizardManager wm;

        private bool visited;

        public BodyControl(WizardManager inputWizardManager)
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
            //fill in height
            if ((wm.Choices.HeightModifier >= heightModifier.Minimum) && (wm.Choices.HeightModifier <= heightModifier.Maximum))
            {
                heightModifier.Value = wm.Choices.HeightModifier;
            }
            else
            {
                heightModifier.Value = heightModifier.Minimum;
            }

            //fill in weight
            if ((wm.Choices.WeightModifier >= weightModifier.Minimum) && (wm.Choices.WeightModifier <= weightModifier.Maximum))
            {
                weightModifier.Value = wm.Choices.WeightModifier;
            }
            else
            {
                weightModifier.Value = weightModifier.Minimum;
            }

            if (!visited)
            {
                visited = true;
            }
        }

        public void saveContent()
        {
            throw new NotImplementedException();
        }

        public void updateMinMax(string inputSubrace)
        {
            heightModifier.Minimum = wm.DBManager.getHeightModifierDiceCount(inputSubrace);
            heightModifier.Maximum = wm.DBManager.getHeightModifierDiceType(inputSubrace) * heightModifier.Minimum;
            
            weightModifier.Minimum = wm.DBManager.getWeightModifierDiceCount(inputSubrace);
            weightModifier.Maximum = wm.DBManager.getWeightModifierDiceType(inputSubrace) * weightModifier.Minimum;
        }
    }
}
