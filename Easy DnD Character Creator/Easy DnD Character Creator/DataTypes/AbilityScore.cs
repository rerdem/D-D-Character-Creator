using System;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class AbilityScore
    {
        public string Name { get; }
        public int BaseValue { get; set; }
        public int SubraceAdd { get; set; }
        public int SubraceBonus { get; set; }

        public AbilityScore(string inputName)
        {
            Name = inputName;
            BaseValue = 0;
            SubraceAdd = 0;
            SubraceBonus = 0;
        }

        //public AbilityScore(int inputBaseValue, int inputSubraceAdd, int inputSubraceBonus)
        //{
        //    BaseValue = inputBaseValue;
        //    SubraceAdd = inputSubraceAdd;
        //    SubraceBonus = inputSubraceBonus;
        //}

        public int getTotalValue()
        {
            return BaseValue + SubraceAdd + SubraceBonus;
        }

        public int getModifier()
        {
            return (int)Math.Floor((getTotalValue() - 10.0) / 2.0);
        }

        public string getShortName()
        {
            return Name.Substring(0, 3).ToLower();
        }
    }
}