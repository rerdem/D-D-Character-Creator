using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class FightingStyle : IEquatable<FightingStyle>
    {
        public string Name { get; }
        public string Description { get; }

        public FightingStyle()
        {
            Name = "";
            Description = "";
        }

        public FightingStyle(string inputName, string inputDescription)
        {
            Name = inputName;
            Description = inputDescription;
        }

        public bool Equals(FightingStyle other)
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

            FightingStyle otherSpell = other as FightingStyle;
            if (otherSpell == null)
            {
                return false;
            }

            return Equals(otherSpell);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
