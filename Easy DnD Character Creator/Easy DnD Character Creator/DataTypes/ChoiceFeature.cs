using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class ChoiceFeature : IEquatable<ChoiceFeature>
    {
        public string Name { get; }
        public string Description { get; }
        public List<ChoiceFeatureOption> Options { get; }

        public ChoiceFeature()
        {
            Name = "";
            Description = "";
            Options = new List<ChoiceFeatureOption>();
        }

        public ChoiceFeature(string inputName, string inputDescription)
        {
            Name = inputName;
            Description = inputDescription;
            Options = new List<ChoiceFeatureOption>();
        }

        public ChoiceFeature(string inputName, string inputDescription, List<ChoiceFeatureOption> inputOptions)
        {
            Name = inputName;
            Description = inputDescription;
            Options = inputOptions;
        }

        public void addOption(ChoiceFeatureOption newOption)
        {
            Options.Add(newOption);
        }

        public void removeOption(ChoiceFeatureOption oldOption)
        {
            Options.Remove(oldOption);
        }

        public void selectOption(ChoiceFeatureOption selectedOption)
        {
            foreach (ChoiceFeatureOption option in Options)
            {
                if (option == selectedOption)
                {
                    option.Selected = true;
                }
                else
                {
                    option.Selected = false;
                }
            }
        }

        public ChoiceFeatureOption getSelectedOption()
        {
            foreach (ChoiceFeatureOption option in Options)
            {
                if (option.Selected)
                {
                    return option;
                }
            }

            return new ChoiceFeatureOption();
        }

        public bool Equals(ChoiceFeature other)
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

            ChoiceFeature otherFeature = other as ChoiceFeature;
            if (otherFeature == null)
            {
                return false;
            }

            return Equals(otherFeature);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
