using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class Dragon
    {
        public string Color { get; }
        public string DamageType { get; }

        public Dragon()
        {
            Color = "";
            DamageType = "";
        }

        public Dragon(string inputColor, string inputDamageType)
        {
            Color = inputColor;
            DamageType = inputDamageType;
        }
    }
}
