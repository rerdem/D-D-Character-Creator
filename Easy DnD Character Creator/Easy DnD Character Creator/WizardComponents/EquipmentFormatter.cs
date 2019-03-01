using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public class EquipmentFormatter
    {
        public static string formatWeaponDescription(Weapon weapon)
        {
            //remove any parentheses with digits inbetween
            string description = Regex.Replace(weapon.Name, @"\s\([\d]+\)", "");
            description += " - ";
            description += weapon.Damage;
            description += " - ";
            description += weapon.DamageType;
            description += Environment.NewLine;
            description += weapon.Properties;

            return description;
        }

        public static string formatArmorDescription(Armor armor)
        {
            //remove any parentheses with digits inbetween
            string description = Regex.Replace(armor.Name, @"\s\([\d]+\)", "");
            description += " (";
            description += armor.Type;

            if (armor.StrengthRequirement > 0)
            {
                description += ", min. strength: ";
                description += armor.StrengthRequirement.ToString();
            }

            description += ")";
            description += Environment.NewLine;
            description += "AC: ";
            description += armor.AC.ToString();

            if (armor.AdditionalAC > 0)
            {
                description += " (+";
                description += armor.AdditionalAC.ToString();
                description += ")";
            }

            if (armor.AdditionalModifier != "-")
            {
                description += " +";
                description += armor.AdditionalModifier;

                if (armor.AdditionalModifierLimit > 0)
                {
                    description += " (max. ";
                    description += armor.AdditionalModifierLimit.ToString();
                    description += ")";
                }
            }

            if (armor.StealthDisadvantage)
            {
                description += " - stealth disadvantage";
            }

            return description;
        }
    }
}
