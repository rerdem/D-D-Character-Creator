using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Easy_DnD_Character_Creator.DataTypes;

namespace Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents
{
    public partial class HunterControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private string lastSubclassLevel;

        private int hunterFeatureAmount;
        private List<ChoiceFeature> hunterFeatures;
        private List<HunterFeatureControl> featureControls;

        public event EventHandler HunterOptionChosen;

        public HunterControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            hunterFeatureAmount = 0;
            hunterFeatures = new List<ChoiceFeature>();
            featureControls = new List<HunterFeatureControl>();

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

            foreach (HunterFeatureControl control in featureControls)
            {
                if (!control.isValid())
                {
                    if (!string.IsNullOrEmpty(output))
                    {
                        output += ", ";
                    }
                    output += control.getInvalidElements();
                }
            }

            return output;
        }

        public bool isValid()
        {
            foreach (HunterFeatureControl control in featureControls)
            {
                if (!control.isValid())
                {
                    return false;
                }
            }

            return true;
        }

        public void populateForm()
        {
            if (!Visited || hasSubclassLevelChanged())
            {
                hunterFeatureAmount = wm.DBManager.ExtraSubclassChoiceData.HunterData.hunterFeatureAmount(wm.Choices.ClassChoice.getSelectedSubclass().Name, wm.Choices.Level);
                hunterFeatures = wm.DBManager.ExtraSubclassChoiceData.HunterData.getHunterFeatures(wm.Choices.ClassChoice.getSelectedSubclass().Name, wm.Choices.Level);
            }

            if (Visited && !hasSubclassLevelChanged())
            {
                //load content
                foreach (ChoiceFeature feature in hunterFeatures)
                {
                    if (wm.Choices.ClassChoice.getSelectedSubclass().HunterFeatures.Contains(feature))
                    {
                        feature.selectOption(wm.Choices.ClassChoice.getSelectedSubclass().HunterFeatures[wm.Choices.ClassChoice.getSelectedSubclass().HunterFeatures.IndexOf(feature)].getSelectedOption());
                    }
                }
            }
            refreshContent();

            lastSubclassLevel = wm.Choices.ClassChoice.getSelectedSubclass().Name + wm.Choices.Level.ToString();
            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.ClassChoice.getSelectedSubclass().HunterFeatures.Clear();
            foreach (HunterFeatureControl control in featureControls)
            {
                control.saveContent();
            }
        }

        private void refreshContent()
        {
            hunterLayout.Controls.Clear();
            featureControls.Clear();
            for (int i = 0; i < hunterFeatures.Count; i++)
            {
                HunterFeatureControl control = new HunterFeatureControl(wm, hunterFeatures[i]);
                control.HunterOptionChosen += new EventHandler(control_HunterOptionChosen);
                featureControls.Add(control);
                hunterLayout.Controls.Add(featureControls[i]);
                featureControls[i].populateForm();
            }
        }

        private bool hasSubclassLevelChanged()
        {
            return (lastSubclassLevel != (wm.Choices.ClassChoice.getSelectedSubclass().Name + wm.Choices.Level.ToString()));
        }

        private void control_HunterOptionChosen(object sender, EventArgs e)
        {
            OnHunterOptionChosen(null);
        }

        protected virtual void OnHunterOptionChosen(EventArgs e)
        {
            EventHandler handler = HunterOptionChosen;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
