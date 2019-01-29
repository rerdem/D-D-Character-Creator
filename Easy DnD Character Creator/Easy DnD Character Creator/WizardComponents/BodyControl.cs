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
            wm.Choices.HeightModifier = (int)heightModifier.Value;
            wm.Choices.WeightModifier = (int)weightModifier.Value;
            wm.Choices.Height = heightResultLabel.Text;
            wm.Choices.Weight = weightResultLabel.Text;
        }

        public void updateMinMax(string inputSubrace)
        {
            heightModifier.Minimum = wm.DBManager.getHeightModifierDiceCount(inputSubrace);
            heightModifier.Maximum = wm.DBManager.getHeightModifierDiceType(inputSubrace) * heightModifier.Minimum;
            
            weightModifier.Minimum = wm.DBManager.getWeightModifierDiceCount(inputSubrace);
            weightModifier.Maximum = wm.DBManager.getWeightModifierDiceType(inputSubrace) * weightModifier.Minimum;
        }

        private void refreshHeightWeight()
        {
            //calculate height in imperial
            int heightImperial = wm.DBManager.getBaseHeight(false, wm.Choices.Subrace);
            heightImperial += (int)heightModifier.Value;

            //calculate weight in imperial
            int weightImperial = wm.DBManager.getBaseWeight(false, wm.Choices.Subrace);
            int weightModifierImperial = (int)heightModifier.Value * (int)weightModifier.Value;
            weightImperial += weightModifierImperial;

            //convert to metric
            int heightMetric = (int)Math.Ceiling(heightImperial * 2.5);
            int weightMetric = (int)Math.Floor(weightImperial / 2.0);

            //construct and set height
            heightResultLabel.Text = Convert.ToString(heightMetric / 100);
            heightResultLabel.Text += ",";
            heightResultLabel.Text += Convert.ToString(heightMetric % 100);
            heightResultLabel.Text += "m (";
            heightResultLabel.Text += Convert.ToString(heightImperial / 12);
            heightResultLabel.Text += "\'";
            heightResultLabel.Text += Convert.ToString(heightImperial % 12);
            heightResultLabel.Text += "\")";

            //construct and set weight
            weightResultLabel.Text = Convert.ToString(weightMetric);
            weightResultLabel.Text += "kg (";
            weightResultLabel.Text += Convert.ToString(weightImperial);
            weightResultLabel.Text += "lbs.)";
        }

        private void heightModifier_ValueChanged(object sender, EventArgs e)
        {
            refreshHeightWeight();
        }

        private void weightModifier_ValueChanged(object sender, EventArgs e)
        {
            refreshHeightWeight();
        }

        private void randomizeButton_Click(object sender, EventArgs e)
        {
            heightModifier.Value = wm.getRandomNumber((int)heightModifier.Minimum, (int)heightModifier.Maximum + 1);
            weightModifier.Value = wm.getRandomNumber((int)weightModifier.Minimum, (int)weightModifier.Maximum + 1);
        }

        public bool isValid()
        {
            return ((heightModifier.Value > 0) && (weightModifier.Value > 0));
        }
    }
}
