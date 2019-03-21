using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class DraconicAncestry : IEquatable<DraconicAncestry>
    {
        public string Color { get; }
        public string DamageType { get; }
        public string FullInfo { get { return Color + " (" + DamageType + ")"; } }

        public DraconicAncestry()
        {
            Color = "";
            DamageType = "";
        }

        public DraconicAncestry(string inputColor, string inputDamageType)
        {
            Color = inputColor;
            DamageType = inputDamageType;
        }

        public bool Equals(DraconicAncestry other)
        {
            if (other == null)
            {
                return false;
            }

            return FullInfo == other.FullInfo;
        }

        public override bool Equals(Object other)
        {
            if (other == null)
            {
                return false;
            }

            DraconicAncestry otherAncestry = other as DraconicAncestry;
            if (otherAncestry == null)
            {
                return false;
            }

            return Equals(otherAncestry);
        }

        public override int GetHashCode()
        {
            return this.FullInfo.GetHashCode();
        }
    }
}
