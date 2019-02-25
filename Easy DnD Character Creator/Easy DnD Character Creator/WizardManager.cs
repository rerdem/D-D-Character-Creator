﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator
{
    public enum WizardState { intro, race, appearance, classBackground, stats, languages, skillEquipment, spells, extraRaceChoices, extraClassChoices, extraSubclassChoices, story, export };

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
                    CurrentState = WizardState.classBackground;
                    break;
                case WizardState.classBackground:
                    CurrentState = WizardState.stats;
                    break;
                case WizardState.stats:
                    if (DBManager.getExtraLanguageCount(Choices.Subrace, Choices.Subclass, Choices.Background) > 0)
                    {
                        CurrentState = WizardState.languages;
                    }
                    else
                    {
                        CurrentState = WizardState.skillEquipment;
                    }
                    break;
                case WizardState.languages:
                    CurrentState = WizardState.skillEquipment;
                    break;
                case WizardState.skillEquipment:
                    if ((DBManager.hasSpellcasting(Choices.Class, Choices.Subclass, Choices.Level)) && (DBManager.choosesSpells(Choices.Class, Choices.Subclass, Choices.Level)))
                    {
                        CurrentState = WizardState.spells;
                    }
                    else
                    {
                        if (DBManager.hasExtraRaceChoices(Choices.Subrace))
                        {
                            CurrentState = WizardState.extraRaceChoices;
                        }
                        else if (DBManager.hasExtraClassChoices(Choices.Class, Choices.Level))
                        {
                            CurrentState = WizardState.extraClassChoices;
                        }
                        else if (DBManager.hasExtraSubclassChoices(Choices.Subclass, Choices.Level))
                        {
                            CurrentState = WizardState.extraSubclassChoices;
                        }
                        else
                        {
                            CurrentState = WizardState.story;
                        }
                    }
                    break;
                case WizardState.spells:
                    if (DBManager.hasExtraRaceChoices(Choices.Subrace))
                    {
                        CurrentState = WizardState.extraRaceChoices;
                    }
                    else if (DBManager.hasExtraClassChoices(Choices.Class, Choices.Level))
                    {
                        CurrentState = WizardState.extraClassChoices;
                    }
                    else if (DBManager.hasExtraSubclassChoices(Choices.Subclass, Choices.Level))
                    {
                        CurrentState = WizardState.extraSubclassChoices;
                    }
                    else
                    {
                        CurrentState = WizardState.story;
                    }
                    break;
                case WizardState.extraRaceChoices:
                    if (DBManager.hasExtraClassChoices(Choices.Class, Choices.Level))
                    {
                        CurrentState = WizardState.extraClassChoices;
                    }
                    else if (DBManager.hasExtraSubclassChoices(Choices.Subclass, Choices.Level))
                    {
                        CurrentState = WizardState.extraSubclassChoices;
                    }
                    else
                    {
                        CurrentState = WizardState.story;
                    }
                    break;
                case WizardState.extraClassChoices:
                    if (DBManager.hasExtraSubclassChoices(Choices.Subclass, Choices.Level))
                    {
                        CurrentState = WizardState.extraSubclassChoices;
                    }
                    else
                    {
                        CurrentState = WizardState.story;
                    }
                    break;
                case WizardState.extraSubclassChoices:
                    CurrentState = WizardState.story;
                    break;
                case WizardState.story:
                    CurrentState = WizardState.export;
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
                    CurrentState = WizardState.race;
                    break;
                case WizardState.classBackground:
                    CurrentState = WizardState.appearance;
                    break;
                case WizardState.stats:
                    CurrentState = WizardState.classBackground;
                    break;
                case WizardState.languages:
                    CurrentState = WizardState.stats;
                    break;
                case WizardState.skillEquipment:
                    if (DBManager.getExtraLanguageCount(Choices.Subrace, Choices.Subclass, Choices.Background) > 0)
                    {
                        CurrentState = WizardState.languages;
                    }
                    else
                    {
                        CurrentState = WizardState.stats;
                    }
                    break;
                case WizardState.spells:
                    CurrentState = WizardState.skillEquipment;
                    break;
                case WizardState.extraRaceChoices:
                    if ((DBManager.hasSpellcasting(Choices.Class, Choices.Subclass, Choices.Level)) && (DBManager.choosesSpells(Choices.Class, Choices.Subclass, Choices.Level)))
                    {
                        CurrentState = WizardState.spells;
                    }
                    else
                    {
                        CurrentState = WizardState.skillEquipment;
                    }
                    break;
                case WizardState.extraClassChoices:
                    if (DBManager.hasExtraRaceChoices(Choices.Subrace))
                    {
                        CurrentState = WizardState.extraRaceChoices;
                    }
                    else if ((DBManager.hasSpellcasting(Choices.Class, Choices.Subclass, Choices.Level)) && (DBManager.choosesSpells(Choices.Class, Choices.Subclass, Choices.Level)))
                    {
                        CurrentState = WizardState.spells;
                    }
                    else
                    {
                        CurrentState = WizardState.skillEquipment;
                    }
                    break;
                case WizardState.extraSubclassChoices:
                    if (DBManager.hasExtraClassChoices(Choices.Class, Choices.Level))
                    {
                        CurrentState = WizardState.extraClassChoices;
                    }
                    else if (DBManager.hasExtraRaceChoices(Choices.Subrace))
                    {
                        CurrentState = WizardState.extraRaceChoices;
                    }
                    else if ((DBManager.hasSpellcasting(Choices.Class, Choices.Subclass, Choices.Level)) && (DBManager.choosesSpells(Choices.Class, Choices.Subclass, Choices.Level)))
                    {
                        CurrentState = WizardState.spells;
                    }
                    else
                    {
                        CurrentState = WizardState.skillEquipment;
                    }
                    break;
                case WizardState.story:
                    if (DBManager.hasExtraSubclassChoices(Choices.Subclass, Choices.Level))
                    {
                        CurrentState = WizardState.extraSubclassChoices;
                    }
                    else if (DBManager.hasExtraClassChoices(Choices.Class, Choices.Level))
                    {
                        CurrentState = WizardState.extraClassChoices;
                    }
                    else if (DBManager.hasExtraRaceChoices(Choices.Subrace))
                    {
                        CurrentState = WizardState.extraRaceChoices;
                    }
                    else if ((DBManager.hasSpellcasting(Choices.Class, Choices.Subclass, Choices.Level)) && (DBManager.choosesSpells(Choices.Class, Choices.Subclass, Choices.Level)))
                    {
                        CurrentState = WizardState.spells;
                    }
                    else
                    {
                        CurrentState = WizardState.skillEquipment;
                    }
                    break;
                case WizardState.export:
                    CurrentState = WizardState.story;
                    break;
                default: //WizardState.intro
                    break;
            }
            setFirstOrLastPage();
        }

        /// <summary>
        /// generates a random number between min (incl.) and max (excl.)
        /// wrapper for random.Next to make sure random only gets seeded once
        /// </summary>
        /// <param name="minimum">lower inclusive threshhold</param>
        /// <param name="maximum">upper exclusive threshhold</param>
        /// <returns></returns>
        public int getRandomNumber(int minimum, int maximum)
        {
            return random.Next(minimum, maximum);
        }

        /// <summary>
        /// gets the headline of the current wizard page
        /// </summary>
        public string getCurrentPageHeader()
        {
            string headerOutput = "";
            switch (CurrentState)
            {
                case WizardState.race:
                    headerOutput = "Race && Alignment";
                    break;
                case WizardState.appearance:
                    headerOutput = "Physical Appearance";
                    break;
                case WizardState.classBackground:
                    headerOutput = "Class/Subclass && Background";
                    break;
                case WizardState.stats:
                    headerOutput = "Abilities";
                    break;
                case WizardState.languages:
                    headerOutput = "Languages";
                    break;
                case WizardState.skillEquipment:
                    headerOutput = "Skills && Equipment";
                    break;
                case WizardState.spells:
                    headerOutput = "Spells";
                    break;
                case WizardState.extraRaceChoices:
                    headerOutput = "Additional Race Choices";
                    break;
                case WizardState.extraClassChoices:
                    headerOutput = "Additional Class Choices";
                    break;
                case WizardState.extraSubclassChoices:
                    headerOutput = "Additional Subclass Choices";
                    break;
                case WizardState.story:
                    headerOutput = "Character Story";
                    break;
                case WizardState.export:
                    break;
                default: //WizardState.intro
                    headerOutput = "Introduction";
                    break;
            }

            return headerOutput;
        }

        /// <summary>
        /// gets the description of the current wizard page
        /// </summary>
        public string getCurrentPageDescription()
        {
            string descriptionOutput = "";

            switch (CurrentState)
            {
                case WizardState.race:
                    descriptionOutput = "Please select the race, subrace and alignment of your character.";
                    break;
                case WizardState.appearance:
                    descriptionOutput = "Please select the physical characteristics of your character.";
                    break;
                case WizardState.classBackground:
                    descriptionOutput = "Please select the Class/Subclass and Background for your character.";
                    break;
                case WizardState.stats:
                    descriptionOutput = "Please assign the ability values that define your character.";
                    break;
                case WizardState.languages:
                    descriptionOutput = "Please choose which languages your character can speak, read and write.";
                    break;
                case WizardState.skillEquipment:
                    descriptionOutput = "Please choose your starting equipment and which skills your character is proficient in.";
                    break;
                case WizardState.spells:
                    descriptionOutput = "Please choose your spells.";
                    break;
                case WizardState.extraRaceChoices:
                    descriptionOutput = "Please choose the options mandated by your choice of race.";
                    break;
                case WizardState.extraClassChoices:
                    descriptionOutput = "Please choose the options mandated by your choice of class.";
                    break;
                case WizardState.extraSubclassChoices:
                    descriptionOutput = "Please choose the options mandated by your choice of subclass.";
                    break;
                case WizardState.story:
                    descriptionOutput = "Please create your character's backstory.";
                    break;
                case WizardState.export:
                    break;
                default: //WizardState.intro
                    descriptionOutput = "Please select the used books, creation preset and character level.";
                    break;
            }

            return descriptionOutput;
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

            if (CurrentState == WizardState.export)
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