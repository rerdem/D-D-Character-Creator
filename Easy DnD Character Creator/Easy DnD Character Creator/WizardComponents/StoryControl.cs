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
using Easy_DnD_Character_Creator.DataTypes;

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public enum PersonalityComponent { trait, ideal, bond, flaw };

    public partial class StoryControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private string lastBackground;

        private PersonalityControl traitComponent;
        private PersonalityControl idealComponent;
        private PersonalityControl bondComponent;
        private PersonalityControl flawComponent;
        private BackgroundStoryControl backgroundStoryComponent;
        private BackstoryControl backstoryComponent;

        public event EventHandler SubcontrolOptionChosen;

        public StoryControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            lastBackground = "";

            traitComponent = new PersonalityControl(wm, PersonalityComponent.trait);
            idealComponent = new PersonalityControl(wm, PersonalityComponent.ideal);
            bondComponent = new PersonalityControl(wm, PersonalityComponent.bond);
            flawComponent = new PersonalityControl(wm, PersonalityComponent.flaw);
            backgroundStoryComponent = new BackgroundStoryControl(wm);
            backgroundStoryComponent.BackgroundStoryChoiceChosen += new EventHandler(backgroundStoryComponent_BackgroundStoryChoiceChosen);
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
            storyLayout.Controls.Clear();

            storyLayout.Controls.Add(traitComponent);
            traitComponent.populateForm();

            storyLayout.Controls.Add(idealComponent);
            idealComponent.populateForm();

            storyLayout.Controls.Add(bondComponent);
            bondComponent.populateForm();

            storyLayout.Controls.Add(flawComponent);
            flawComponent.populateForm();

            if (wm.Choices.HasBackgroundStoryChoice)
            {
                storyLayout.Controls.Add(backgroundStoryComponent);
                backgroundStoryComponent.populateForm();
            }

            storyLayout.Controls.Add(backstoryComponent);
            backstoryComponent.populateForm();

            lastBackground = wm.Choices.Background;
            Visited = true;
        }

        public void saveContent()
        {
            traitComponent.saveContent();
            idealComponent.saveContent();
            bondComponent.saveContent();
            flawComponent.saveContent();
            backstoryComponent.saveContent();

            if (wm.Choices.HasBackgroundStoryChoice)
            {
                backgroundStoryComponent.saveContent();
            }
            else
            {
                wm.Choices.BackgroundChoice = new BackgroundStoryChoice();
            }
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

        private bool hasBackgroundChanged()
        {
            return (lastBackground != wm.Choices.Background);
        }

        private void backgroundStoryComponent_BackgroundStoryChoiceChosen(object sender, EventArgs e)
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
