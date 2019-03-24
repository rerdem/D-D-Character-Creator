using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class BackgroundStoryChoice : IEquatable<BackgroundStoryChoice>
    {
        public string Name { get; }
        public string Description { get; }
        public List<string> Options { get; }
        public int selectedOption { get; private set; }

        public BackgroundStoryChoice()
        {
            Name = "";
            Description = "";
            Options = new List<string>();
            selectedOption = 0;
        }

        public BackgroundStoryChoice(string inputName, string inputDescription)
        {
            Name = inputName;
            Description = inputDescription;
            Options = new List<string>();
            selectedOption = 0;
        }

        public BackgroundStoryChoice(string inputName, string inputDescription, List<string> inputOptions)
        {
            Name = inputName;
            Description = inputDescription;
            Options = inputOptions;
            selectedOption = 0;
        }

        public void addOption(string newOption)
        {
            Options.Add(newOption);
        }

        public void removeOption(string deleteOption)
        {
            Options.Remove(deleteOption);
        }

        public void setSelectedOption(string optionToSelect)
        {
            selectedOption = Options.IndexOf(optionToSelect);
        }

        /// <summary>
        /// gets the currently selected option
        /// </summary>
        /// <returns>the currently selected option or an empty string, if the currently selected Option index is out of bounds</returns>
        public string getSelectedOption()
        {
            if ((selectedOption < 0) || (selectedOption > Options.Count))
            {
                return "";
            }
            else
            {
                return Options[selectedOption];
            }
        }

        public bool Equals(BackgroundStoryChoice other)
        {
            if (other == null)
            {
                return false;
            }

            return Name == other.Name;
        }

        public override bool Equals(Object other)
        {
            if (other == null)
            {
                return false;
            }

            BackgroundStoryChoice otherChoice = other as BackgroundStoryChoice;
            if (otherChoice == null)
            {
                return false;
            }

            return Equals(otherChoice);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
