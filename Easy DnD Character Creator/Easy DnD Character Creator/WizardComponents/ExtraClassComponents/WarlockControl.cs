using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_DnD_Character_Creator.WizardComponents.ExtraClassComponents
{
    public partial class WarlockControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private WarlockPactControl warlockPactComponent;
        private WarlockInvocationControl warlockInvocationComponent;

        public event EventHandler SubcontrolOptionChosen;

        public WarlockControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            warlockPactComponent = new WarlockPactControl(wm);
            warlockPactComponent.PactChosen += new EventHandler(warlockPactComponent_OnPactChosen);
            warlockPactComponent.PactSpellChosen += new EventHandler(warlockPactComponent_OnPactSpellChosen);

            warlockInvocationComponent = new WarlockInvocationControl(wm);
            warlockInvocationComponent.InvocationChosen += new EventHandler(warlockPactComponent_OnInvocationChosen);
            warlockInvocationComponent.InvocationSpellChosen += new EventHandler(warlockPactComponent_OnInvocationSpellChosen);

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

            if (warlockLayout.Controls.Contains(warlockPactComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = warlockPactComponent.getInvalidElements();
            }

            if (warlockLayout.Controls.Contains(warlockInvocationComponent))
            {
                if (!string.IsNullOrEmpty(missingElements))
                {
                    missingElements += ", ";
                }

                missingElements = warlockInvocationComponent.getInvalidElements();
            }

            return missingElements;
        }

        public bool isValid()
        {
            bool isPactValid = true;
            bool isInvocationValid = true;

            if (warlockLayout.Controls.Contains(warlockPactComponent))
            {
                isPactValid = warlockPactComponent.isValid();
            }

            if (warlockLayout.Controls.Contains(warlockInvocationComponent))
            {
                isInvocationValid = warlockInvocationComponent.isValid();
            }

            return isPactValid && isInvocationValid;
        }

        public void populateForm()
        {
            //setup subcontrols
            warlockLayout.Controls.Clear();
            //warlock pacts
            if (wm.DBManager.ExtraClassChoiceData.WarlockChoiceData.hasWarlockPact(wm.Choices.Class, wm.Choices.Level))
            {
                warlockLayout.Controls.Add(warlockPactComponent);
            }
            warlockPactComponent.populateForm();

            //eldritch invocations
            if (wm.DBManager.ExtraClassChoiceData.WarlockChoiceData.hasEldritchInvocations(wm.Choices.Class, wm.Choices.Level))
            {
                warlockLayout.Controls.Add(warlockInvocationComponent);
            }
            warlockInvocationComponent.populateForm();


            Visited = true;
        }

        public void saveContent()
        {
            if (warlockLayout.Controls.Contains(warlockPactComponent))
            {
                warlockPactComponent.saveContent();
            }
            else
            {
                wm.Choices.WarlockPactChoice = new DataTypes.WarlockPact();
                wm.Choices.WarlockPactSpells.Clear();
            }

            if (warlockLayout.Controls.Contains(warlockInvocationComponent))
            {
                warlockInvocationComponent.saveContent();
            }
            else
            {
                wm.Choices.WarlockInvocations.Clear();
                wm.Choices.WarlockInvocationSpells.Clear();
            }
        }

        private void warlockPactComponent_OnInvocationSpellChosen(object sender, EventArgs e)
        {
            OnSubcontrolOptionChosen(null);
        }

        private void warlockPactComponent_OnInvocationChosen(object sender, EventArgs e)
        {
            OnSubcontrolOptionChosen(null);
        }

        private void warlockPactComponent_OnPactSpellChosen(object sender, EventArgs e)
        {
            warlockInvocationComponent.refreshInvocationList();

            OnSubcontrolOptionChosen(null);
        }

        private void warlockPactComponent_OnPactChosen(object sender, EventArgs e)
        {
            warlockInvocationComponent.refreshInvocationList();

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