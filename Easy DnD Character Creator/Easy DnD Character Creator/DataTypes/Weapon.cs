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
        public string Type { get; }
        public string Range { get; }
        public string Damage { get; }
        public string DamageType { get; }
        public string Properties { get; }

        public bool IsMelee
        {
            get
            {
                return Range == "melee";
            }
        }
        public bool IsFinesse
        {
            get
            {
                return Properties.Contains("finesse");
            }
        }

        public Weapon()
        {
            Name = "";
            Type = "";
            Range = "";
            Damage = "";
            DamageType = "";
            Properties = "";
        }

        public Weapon(string inputName, string inputType, string inputRange, string inputDamage, string inputDamageType, string inputProperties)
        {
            Name = inputName;
            Type = inputType;
            Range = inputRange;
            Damage = inputDamage;
            DamageType = inputDamageType;
            Properties = inputProperties;
        }
    }
}
