using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class TotemFeature : IEquatable<TotemFeature>
    {
        public string Name { get; }
        public string Description { get; }
        public List<TotemOption> Options { get; }

        public TotemFeature()
        {
            Name = "";
            Description = "";
            Options = new List<TotemOption>();
        }

        public TotemFeature(string inputName, string inputDescription)
        {
            Name = inputName;
            Description = inputDescription;
            Options = new List<TotemOption>();
        }

        public TotemFeature(string inputName, string inputDescription, List<TotemOption> inputOptions)
        {
            Name = inputName;
            Description = inputDescription;
            Options = inputOptions;
        }

        public void addOption(TotemOption newOption)
        {
            Options.Add(newOption);
        }

        public void removeOption(TotemOption oldOption)
        {
            Options.Remove(oldOption);
        }

        public void selectOption(TotemOption selectedOption)
        {
            foreach (TotemOption option in Options)
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

        public TotemOption getSelectedOption()
        {
            foreach (TotemOption option in Options)
            {
                if (option.Selected)
                {
                    return option;
                }
            }

            return new TotemOption();
        }

        public bool Equals(TotemFeature other)
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

            TotemFeature otherFeature = other as TotemFeature;
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
