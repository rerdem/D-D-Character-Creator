using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class BarbarianRage
    {
        public string RageAmount { get; }
        public string DamageBonus { get; }

        public BarbarianRage()
        {
            RageAmount = "";
            DamageBonus = "";
        }

        public BarbarianRage(string inputRageAmount, string inputDamageBonus)
        {
            RageAmount = inputRageAmount;
            DamageBonus = inputDamageBonus;
        }
    }
}
