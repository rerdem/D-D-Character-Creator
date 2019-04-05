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
    public partial class TotemFeatureControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private ChoiceFeature feature;

        public event EventHandler TotemOptionChosen;

        public TotemFeatureControl(WizardManager inputWizardManager, ChoiceFeature inputFeature)
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

            if (totemList.SelectedItems.Count <= 0)
            {
                output = "select a totem for " + feature.Name;
            }

            return output;
        }

        public bool isValid()
        {
            return (totemList.SelectedItems.Count > 0);
        }

        public void populateForm()
        {
            totemFeatureBox.Text = feature.Name;
            featureIntroLabel.Text = feature.Description;

            totemList.BeginUpdate();
            totemList.DataSource = null;
            totemList.DataSource = feature.Options;
            totemList.DisplayMember = "Name";
            totemList.EndUpdate();

            totemList.SetSelected(getCurrentlySelectedOptionIndex(), true);

            Visited = true;
        }

        public void saveContent()
        {
            if (totemList.SelectedItems.Count > 0)
            {
                ChoiceFeatureOption currentOption = (ChoiceFeatureOption)totemList.SelectedItem;
                feature.selectOption(currentOption);
            }

            wm.Choices.ClassChoice.getSelectedSubclass().TotemFeatures.Remove(feature);
            wm.Choices.ClassChoice.getSelectedSubclass().TotemFeatures.Add(feature);
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

        private void featureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (totemList.SelectedItems.Count > 0)
            {
                ChoiceFeatureOption currentOption = (ChoiceFeatureOption)totemList.SelectedItem;
                totemDescriptionLabel.Text = currentOption.Description;
            }

            OnTotemOptionChosen(null);
        }

        protected virtual void OnTotemOptionChosen(EventArgs e)
        {
            EventHandler handler = TotemOptionChosen;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
