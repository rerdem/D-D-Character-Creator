using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class WarlockPact : IEquatable<WarlockPact>
    {
        public string Name { get; }
        public string Description { get; }
        public int SpellAmount { get; }

        public WarlockPact()
        {
            Name = "";
            Description = "";
            SpellAmount = 0;
        }

        public WarlockPact(string inputName, string inputDescription, int inputSpellAmount)
        {
            Name = inputName;
            Description = inputDescription;
            SpellAmount = inputSpellAmount;
        }

        public bool Equals(WarlockPact other)
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

            WarlockPact otherPact = other as WarlockPact;
            if (otherPact == null)
            {
                return false;
            }

            return Equals(otherPact);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
