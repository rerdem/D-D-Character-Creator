using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class DraconicAncestry
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
    }
}
