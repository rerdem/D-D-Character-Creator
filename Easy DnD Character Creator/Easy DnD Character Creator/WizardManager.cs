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
        private Random random;

        public WizardManager()
        {
            DBManager = new DataManager();
            Choices = new ChoiceManager();
            CurrentState = WizardState.intro;
            FirstPage = true;
            LastPage = false;
            random = new Random();
        }

        public void advanceState()
        {
            switch (CurrentState)
            {
                case WizardState.race:
                    CurrentState = WizardState.appearance;
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
                    break;
            }
            setFirstOrLastPage();
        }

        public void revertState()
        {
            switch (CurrentState)
            {
                case WizardState.race:
                    CurrentState = WizardState.intro;
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
                    break;
            }
            setFirstOrLastPage();
        }

        public int getRandomNumber(int minimum, int maximum)
        {
            return random.Next(minimum, maximum);
        }

        /// <summary>
        /// determines, if current state is first or last and sets FirstPage and LastPage accordingly
        /// </summary>
        private void setFirstOrLastPage()
        {
            if (CurrentState == WizardState.intro)
            {
                FirstPage = true;
            }
            else
            {
                FirstPage = false;
            }

            if (CurrentState == WizardState.appearance)
            {
                LastPage = true;
            }
            else
            {
                LastPage = false;
            }
        }
    }
}