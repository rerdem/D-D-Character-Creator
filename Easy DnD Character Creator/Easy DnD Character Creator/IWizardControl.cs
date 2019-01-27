using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator
{
    interface IWizardControl
    {
        void populateForm();
        void saveContent();

        bool Visited { get; set; }
    }
}
