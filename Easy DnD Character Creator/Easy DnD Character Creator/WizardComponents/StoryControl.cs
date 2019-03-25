using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Easy_DnD_Character_Creator.WizardComponents.StoryComponents;

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public enum PersonalityComponent { trait, ideal, bond, flaw };

    public partial class StoryControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private PersonalityControl traitComponent;
        private PersonalityControl idealComponent;
        private PersonalityControl bondComponent;
        private PersonalityControl flawComponent;
        private BackgroundStoryControl backgroundStoryComponent;
        private BackstoryControl backstoryComponent;

        public StoryControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            traitComponent = new PersonalityControl(wm, PersonalityComponent.trait);
            idealComponent = new PersonalityControl(wm, PersonalityComponent.ideal);
            bondComponent = new PersonalityControl(wm, PersonalityComponent.bond);
            flawComponent = new PersonalityControl(wm, PersonalityComponent.flaw);
            backgroundStoryComponent = new BackgroundStoryControl(wm);
            backstoryComponent = new BackstoryControl(wm);

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
            //fill in controls (check for extra bg story choice
            //check for bg story choice, if background has changed

            Visited = true;
        }

        public void saveContent()
        {
            
        }

        public bool isValid()
        {
            if (wm.Choices.HasBackgroundStoryChoice)
            {
                return traitComponent.isValid() &&
                   idealComponent.isValid() &&
                   bondComponent.isValid() &&
                   flawComponent.isValid() &&
                   backgroundStoryComponent.isValid() &&
                   backstoryComponent.isValid();
            }
            else
            {
                return traitComponent.isValid() &&
                   idealComponent.isValid() &&
                   bondComponent.isValid() &&
                   flawComponent.isValid() &&
                   backstoryComponent.isValid();
            }
        }

        public string getInvalidElements()
        {
            if (wm.Choices.HasBackgroundStoryChoice)
            {
                return string.Join(", ", new string[] {
                            traitComponent.getInvalidElements(),
                            idealComponent.getInvalidElements(),
                            bondComponent.getInvalidElements(),
                            flawComponent.getInvalidElements(),
                            backgroundStoryComponent.getInvalidElements(),
                            backstoryComponent.getInvalidElements()
                                }.Where(s => !string.IsNullOrEmpty(s)));
            }
            else
            {
                return string.Join(", ", new string[] {
                            traitComponent.getInvalidElements(),
                            idealComponent.getInvalidElements(),
                            bondComponent.getInvalidElements(),
                            flawComponent.getInvalidElements(),
                            backstoryComponent.getInvalidElements()
                                }.Where(s => !string.IsNullOrEmpty(s)));
            }
        }
    }
}
