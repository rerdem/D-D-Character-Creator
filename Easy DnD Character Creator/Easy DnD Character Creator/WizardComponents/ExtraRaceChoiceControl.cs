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
using Easy_DnD_Character_Creator.WizardComponents.ExtraRaceComponents;

namespace Easy_DnD_Character_Creator.WizardComponents
{
    //this entire UserControl will need refactoring, if there is another race that has additional choices
    public partial class ExtraRaceChoiceControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private List<bool> subcomponentActivationList;
        private List<IWizardControl> subcomponentList;

        private ExtraRaceSpellControl extraRaceSpellComponent;

        public event EventHandler ExtraRaceChoiceChanged;

        public ExtraRaceChoiceControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            subcomponentActivationList = new List<bool>();
            initializeSubcomponentList();
            refreshActivationList();


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

            for (int i = 0; i < subcomponentList.Count; i++)
            {
                if (subcomponentActivationList[i])
                {
                    if (!string.IsNullOrEmpty(missingElements))
                    {
                        missingElements += ", ";
                    }

                    missingElements = subcomponentList[i].getInvalidElements();
                }
            }

            return missingElements;
        }

        public bool isValid()
        {
            for (int i = 0; i < subcomponentList.Count; i++)
            {
                if (subcomponentActivationList[i])
                {
                    if (!subcomponentList[i].isValid())
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void populateForm()
        {
            refreshActivationList();

            //setup subcontrols
            raceChoiceLayout.Controls.Clear();

            for (int i = 0; i < subcomponentList.Count; i++)
            {
                if (subcomponentActivationList[i])
                {
                    raceChoiceLayout.Controls.Add((UserControl)subcomponentList[i]);
                }
                subcomponentList[i].populateForm();
            }

            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.RaceSpells.Clear();

            for (int i = 0; i < subcomponentList.Count; i++)
            {
                if (subcomponentActivationList[i])
                {
                    subcomponentList[i].saveContent();
                }
            }
        }

        private void refreshActivationList()
        {
            subcomponentActivationList.Clear();
            subcomponentActivationList.Add(wm.Choices.RaceChoice.getSelectedSubrace().HasExtraSpells);

            //fail safe, in case someone forgets to add the appropriate variable to the activation list
            //this should not need to exist, rethink link in future update
            while (subcomponentActivationList.Count < subcomponentList.Count)
            {
                subcomponentActivationList.Add(false);
            }
        }

        private void initializeSubcomponentList()
        {
            subcomponentList = new List<IWizardControl>();

            extraRaceSpellComponent = new ExtraRaceSpellControl(wm);
            extraRaceSpellComponent.SpellChosen += new EventHandler(subcomponents_OptionChosen);
            subcomponentList.Add(extraRaceSpellComponent);
        }

        private void subcomponents_OptionChosen(object sender, EventArgs e)
        {
            OnExtraRaceChoiceChanged(null);
        }

        protected virtual void OnExtraRaceChoiceChanged(EventArgs e)
        {
            EventHandler handler = ExtraRaceChoiceChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
