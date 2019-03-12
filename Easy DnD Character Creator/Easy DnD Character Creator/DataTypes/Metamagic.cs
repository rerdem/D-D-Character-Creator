using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class Metamagic : IEquatable<Metamagic>
    {
        public string Name { get; }
        public string Description { get; }

        public Metamagic()
        {
            Name = "";
            Description = "";
        }

        public Metamagic(string inputName, string inputDescription)
        {
            Name = inputName;
            Description = inputDescription;
        }

        public bool Equals(Metamagic other)
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

            Metamagic otherSpell = other as Metamagic;
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
