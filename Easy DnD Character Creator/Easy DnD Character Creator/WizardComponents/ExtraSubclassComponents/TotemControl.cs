﻿using System;
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
    public partial class TotemControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private string lastSubclassLevel;

        private int totemFeatureAmount;
        private List<ChoiceFeature> totemFeatures;
        private List<TotemFeatureControl> featureControls;

        public event EventHandler TotemOptionChosen;

        public TotemControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            totemFeatureAmount = 0;
            totemFeatures = new List<ChoiceFeature>();
            featureControls = new List<TotemFeatureControl>();

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

            foreach (TotemFeatureControl control in featureControls)
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
            foreach (TotemFeatureControl control in featureControls)
            {
                if(!control.isValid())
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
                totemFeatureAmount = wm.DBManager.ExtraSubclassChoiceData.TotemData.totemFeatureAmount(wm.Choices.ClassChoice.getSelectedSubclass().Name, wm.Choices.Level);
                totemFeatures = wm.DBManager.ExtraSubclassChoiceData.TotemData.getTotemFeatures(wm.Choices.ClassChoice.getSelectedSubclass().Name, wm.Choices.Level);
            }

            if (Visited && !hasSubclassLevelChanged())
            {
                //load content
                foreach (ChoiceFeature feature in totemFeatures)
                {
                    if (wm.Choices.ClassChoice.getSelectedSubclass().TotemFeatures.Contains(feature))
                    {
                        feature.selectOption(wm.Choices.ClassChoice.getSelectedSubclass().TotemFeatures[wm.Choices.ClassChoice.getSelectedSubclass().TotemFeatures.IndexOf(feature)].getSelectedOption());
                    }
                }
            }
            refreshContent();

            lastSubclassLevel = wm.Choices.ClassChoice.getSelectedSubclass().Name + wm.Choices.Level.ToString();
            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.ClassChoice.getSelectedSubclass().TotemFeatures.Clear();
            foreach (TotemFeatureControl control in featureControls)
            {
                control.saveContent();
            }
        }

        private void refreshContent()
        {
            totemLayout.Controls.Clear();
            featureControls.Clear();
            for (int i = 0; i < totemFeatures.Count; i++)
            {
                TotemFeatureControl control = new TotemFeatureControl(wm, totemFeatures[i]);
                control.TotemOptionChosen += new EventHandler(control_TotemOptionChosen);
                featureControls.Add(control);
                totemLayout.Controls.Add(featureControls[i]);
                featureControls[i].populateForm();
            }
    }

        private bool hasSubclassLevelChanged()
        {
            return (lastSubclassLevel != (wm.Choices.ClassChoice.getSelectedSubclass().Name + wm.Choices.Level.ToString()));
        }

        private void control_TotemOptionChosen(object sender, EventArgs e)
        {
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
