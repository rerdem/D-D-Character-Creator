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
        public ChoiceManager Choices { get; }
        public WizardState CurrentState { get; private set; }
        public bool FirstPage { get; private set; }
        public bool LastPage { get; private set; }

        public WizardManager()
        {
            DBManager = new DataManager();
            Choices = new ChoiceManager();
            CurrentState = WizardState.intro;
            FirstPage = true;
            LastPage = false;
        }

        public void advanceState()
        {
            switch (CurrentState)
            {
                case WizardState.race:
                    break;
                case WizardState.appearance:
                    break;
                case WizardState.classBackground:
                    break;
                case WizardState.stats:
                    break;
                case WizardState.languages:
                    break;
                case WizardState.skills:
                    break;
                case WizardState.equipment:
                    break;
                case WizardState.spells:
                    break;
                case WizardState.extraChoices:
                    break;
                case WizardState.story:
                    break;
                case WizardState.export:
                    break;
                default: //WizardState.intro
                    CurrentState = WizardState.race;
                    FirstPage = false;
                    LastPage = true;
                    break;
            }
        }
    }
}