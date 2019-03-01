using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class Weapon : EquipmentItem
    {
        //public string Name { get; }
        public string Damage { get; }
        public string DamageType { get; }
        public string Properties { get; }

        public Weapon()
        {
            Name = "";
            Damage = "";
            DamageType = "";
            Properties = "";
        }

        public Weapon(string inputName, string inputDamage, string inputDamageType, string inputProperties)
        {
            Name = inputName;
            Damage = inputDamage;
            DamageType = inputDamageType;
            Properties = inputProperties;
        }
    }
}
