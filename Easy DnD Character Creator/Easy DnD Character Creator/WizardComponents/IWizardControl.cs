using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.WizardComponents
{
    interface IWizardControl
    {
        bool Visited { get; set; }

        void populateForm();
        void saveContent();
        bool isValid();
        string getInvalidElements();
    }
}
