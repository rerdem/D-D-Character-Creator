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

        private PersonalityControl traitComponent;
        private PersonalityControl idealComponent;
        private PersonalityControl bondComponent;
        private PersonalityControl flawComponent;
        private WildShapeControl wildShapeComponent;
        private BackgroundStoryControl backgroundStoryComponent;
        private BackstoryControl backstoryComponent;

        public event EventHandler SubcontrolOptionChosen;

        public StoryControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            traitComponent = new PersonalityControl(wm, PersonalityComponent.trait);
            idealComponent = new PersonalityControl(wm, PersonalityComponent.ideal);
            bondComponent = new PersonalityControl(wm, PersonalityComponent.bond);
            flawComponent = new PersonalityControl(wm, PersonalityComponent.flaw);
            wildShapeComponent = new WildShapeControl(wm);
            wildShapeComponent.WildShapeTerrainChosen += new EventHandler(wildShapeComponent_WildShapeTerrainChosen);
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

            if (wm.Choices.HasWildShape)
            {
                storyLayout.Controls.Add(wildShapeComponent);
                wildShapeComponent.populateForm();
            }

            if (wm.Choices.BackgroundChoice.HasStoryChoice)
            {
                storyLayout.Controls.Add(backgroundStoryComponent);
                backgroundStoryComponent.populateForm();
            }

            storyLayout.Controls.Add(backstoryComponent);
            backstoryComponent.populateForm();

            Visited = true;
        }

        public void saveContent()
        {
            traitComponent.saveContent();
            idealComponent.saveContent();
            bondComponent.saveContent();
            flawComponent.saveContent();
            backstoryComponent.saveContent();

            if (wm.Choices.HasWildShape)
            {
                wildShapeComponent.saveContent();
            }
            else
            {
                wm.Choices.TerrainChoice = new WildShapeTerrain();
            }

            if (wm.Choices.BackgroundChoice.HasStoryChoice)
            {
                backgroundStoryComponent.saveContent();
            }
            else
            {
                wm.Choices.BackgroundChoice.StoryChoice = new BackgroundStoryChoice();
            }
        }

        public bool isValid()
        {
            return traitComponent.isValid() &&
                   idealComponent.isValid() &&
                   bondComponent.isValid() &&
                   flawComponent.isValid() &&
                   wildShapeComponent.isValid() &&
                   backgroundStoryComponent.isValid() &&
                   backstoryComponent.isValid();
        }

        public string getInvalidElements()
        {
            string output = string.Join(", ", new string[] {
                            traitComponent.getInvalidElements(),
                            idealComponent.getInvalidElements(),
                            bondComponent.getInvalidElements(),
                            flawComponent.getInvalidElements(),
                            backstoryComponent.getInvalidElements()
                                }.Where(s => !string.IsNullOrEmpty(s)));

            if (wm.Choices.HasWildShape)
            {
                if (!string.IsNullOrEmpty(output))
                {
                    output += ", ";
                }
                output += wildShapeComponent.getInvalidElements();
            }

            if (wm.Choices.BackgroundChoice.HasStoryChoice)
            {
                if (!string.IsNullOrEmpty(output))
                {
                    output += ", ";
                }
                output += backgroundStoryComponent.getInvalidElements();
            }

            return output;
        }

        private void backgroundStoryComponent_BackgroundStoryChoiceChosen(object sender, EventArgs e)
        {
            OnSubcontrolOptionChosen(null);
        }

        private void wildShapeComponent_WildShapeTerrainChosen(object sender, EventArgs e)
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
