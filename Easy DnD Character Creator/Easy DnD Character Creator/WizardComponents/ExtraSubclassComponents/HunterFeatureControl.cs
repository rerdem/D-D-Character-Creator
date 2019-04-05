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
    public partial class HunterFeatureControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private ChoiceFeature feature;

        public event EventHandler HunterOptionChosen;

        public HunterFeatureControl(WizardManager inputWizardManager, ChoiceFeature inputFeature)
        {
            wm = inputWizardManager;
            Visited = false;

            feature = inputFeature;

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

            if (optionList.SelectedItems.Count <= 0)
            {
                output = "select an option for " + feature.Name;
            }

            return output;
        }

        public bool isValid()
        {
            return (optionList.SelectedItems.Count > 0);
        }

        public void populateForm()
        {
            hunterFeatureBox.Text = feature.Name;
            featureIntroLabel.Text = feature.Description;

            optionList.BeginUpdate();
            optionList.DataSource = null;
            optionList.DataSource = feature.Options;
            optionList.DisplayMember = "Name";
            optionList.EndUpdate();

            optionList.SetSelected(getCurrentlySelectedOptionIndex(), true);

            Visited = true;
        }

        public void saveContent()
        {
            if (optionList.SelectedItems.Count > 0)
            {
                ChoiceFeatureOption currentOption = (ChoiceFeatureOption)optionList.SelectedItem;
                feature.selectOption(currentOption);
            }

            wm.Choices.ClassChoice.getSelectedSubclass().HunterFeatures.Remove(feature);
            wm.Choices.ClassChoice.getSelectedSubclass().HunterFeatures.Add(feature);
        }

        private int getCurrentlySelectedOptionIndex()
        {
            for (int i = 0; i < feature.Options.Count; i++)
            {
                if (feature.Options[i].Selected)
                {
                    return i;
                }
            }

            return 0;
        }

        private void optionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optionList.SelectedItems.Count > 0)
            {
                ChoiceFeatureOption currentOption = (ChoiceFeatureOption)optionList.SelectedItem;
                optionDescriptionLabel.Text = currentOption.Description;
            }

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
