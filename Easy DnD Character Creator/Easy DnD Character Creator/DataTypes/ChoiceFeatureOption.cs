using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class ChoiceFeatureOption : IEquatable<ChoiceFeatureOption>
    {
        public string Name { get; }
        public string Description { get; }
        public bool Selected { get; set; }

        public ChoiceFeatureOption()
        {
            Name = "";
            Description = "";
            Selected = false;
        }

        public ChoiceFeatureOption(string inputName, string inputDescription, bool inputSelected)
        {
            Name = inputName;
            Description = inputDescription;
            Selected = inputSelected;
        }

        public bool Equals(ChoiceFeatureOption other)
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

            ChoiceFeatureOption otherOption = other as ChoiceFeatureOption;
            if (otherOption == null)
            {
                return false;
            }

            return Equals(otherOption);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
