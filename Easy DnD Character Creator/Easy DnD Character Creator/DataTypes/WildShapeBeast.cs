using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class WildShapeBeast : IEquatable<WildShapeBeast>
    {
        public string Name { get; }
        public float CR { get; }
        public bool Fly { get; }
        public bool Swim { get; }

        public WildShapeBeast()
        {
            Name = "";
            CR = 0.0f;
            Fly = false;
            Swim = false;
        }

        public WildShapeBeast(string inputName, float inputCR, bool inputFly, bool inputSwim)
        {
            Name = inputName;
            CR = inputCR;
            Fly = inputFly;
            Swim = inputSwim;
        }

        public bool Equals(WildShapeBeast other)
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

            WildShapeBeast otherBeast = other as WildShapeBeast;
            if (otherBeast == null)
            {
                return false;
            }

            return Equals(otherBeast);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
