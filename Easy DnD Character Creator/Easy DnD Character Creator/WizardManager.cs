using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator
{
    public enum WizardState { intro, race, appearance, classBackground, stats, languages, skills, equipment, spells, extraChoices, story, export };

    public class WizardManager
    {
        public DataManager DBManager { get; }
        public WizardState CurrentState { get; private set; }

        public WizardManager()
        {
            DBManager = new DataManager();
            CurrentState = WizardState.intro;
        }

        private void createPages()
        {

        }
    }
}