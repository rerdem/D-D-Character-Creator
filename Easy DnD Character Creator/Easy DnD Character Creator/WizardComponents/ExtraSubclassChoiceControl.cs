using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents;

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public partial class ExtraSubclassChoiceControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private ExtraSubclassSkillControl extraSubclassSkillComponent;
        
        public event EventHandler SubcontrolOptionChosen;

        public ExtraSubclassChoiceControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            extraSubclassSkillComponent = new ExtraSubclassSkillControl(wm);
            extraSubclassSkillComponent.SkillChosen += new EventHandler(extraSubclassSkillComponent_SkillChosen);

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
            string missingElements = "";

            if (subclassChoiceLayout.Controls.Contains(extraSubclassSkillComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = extraSubclassSkillComponent.getInvalidElements();
            }

            return missingElements;
        }

        public bool isValid()
        {
            bool isExtraSubclassSkillValid = true;
            
            if (subclassChoiceLayout.Controls.Contains(extraSubclassSkillComponent))
            {
                isExtraSubclassSkillValid = extraSubclassSkillComponent.isValid();
            }

            return isExtraSubclassSkillValid;
        }

        public void populateForm()
        {
            //setup subcontrols
            subclassChoiceLayout.Controls.Clear();
            //additional skills
            if (wm.DBManager.ExtraSubclassChoiceData.ExtraSubclassSkillData.hasSkillChoice(wm.Choices.Subclass, wm.Choices.Level))
            {
                subclassChoiceLayout.Controls.Add(extraSubclassSkillComponent);
            }
            extraSubclassSkillComponent.populateForm();

            Visited = true;
        }

        public void saveContent()
        {
            if (subclassChoiceLayout.Controls.Contains(extraSubclassSkillComponent))
            {
                extraSubclassSkillComponent.saveContent();
            }
            else
            {
                wm.Choices.SubclassSkills.Clear();
                wm.Choices.SubclassDoublesProficiency = false;
            }
        }

        private void extraSubclassSkillComponent_SkillChosen(object sender, EventArgs e)
        {
            OnSubcontrolOptionChosen(null);
        }

        protected virtual void OnSubcontrolOptionChosen(EventArgs e)
        {
            EventHandler handler = SubcontrolOptionChosen;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
