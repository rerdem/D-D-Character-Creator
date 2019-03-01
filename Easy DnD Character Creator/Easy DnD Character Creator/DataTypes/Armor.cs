using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class Armor : EquipmentItem
    {
        //public string Name { get; }
        public string Type { get; }
        public int AC { get; }
        public int AdditionalAC { get; }
        public string AdditionalModifier { get; }
        public int AdditionalModifierLimit { get; }
        public int StrengthRequirement { get; }
        public bool StealthDisadvantage { get; }

        public Armor()
        {
            Name = "";
            Type = "";
            AC = 0;
            AdditionalAC = 0;
            AdditionalModifier = "";
            AdditionalModifierLimit = 0;
            StrengthRequirement = 0;
            StealthDisadvantage = false;
        }

        public Armor(string inputName, string inputType, int inputAC, int inputAdditionalAC, string inputAdditionalModifier, int inputAdditionalModifierLimit, int inputStrengthRequirement, bool inputStealthDisadvantage)
        {
            Name = inputName;
            Type = inputType;
            AC = inputAC;
            AdditionalAC = inputAdditionalAC;
            AdditionalModifier = inputAdditionalModifier;
            AdditionalModifierLimit = inputAdditionalModifierLimit;
            StrengthRequirement = inputStrengthRequirement;
            StealthDisadvantage = inputStealthDisadvantage;
        }
    }
}