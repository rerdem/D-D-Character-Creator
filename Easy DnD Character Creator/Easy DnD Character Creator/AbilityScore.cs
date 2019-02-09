namespace Easy_DnD_Character_Creator
{
    public class AbilityScore
    {
        public int BaseValue { get; set; }
        public int SubraceAdd { get; set; }
        public int SubraceBonus { get; set; }

        public AbilityScore()
        {
            BaseValue = 0;
            SubraceAdd = 0;
            SubraceBonus = 0;
        }

        public AbilityScore(int inputBaseValue, int inputSubraceAdd, int inputSubraceBonus)
        {
            BaseValue = inputBaseValue;
            SubraceAdd = inputSubraceAdd;
            SubraceBonus = inputSubraceBonus;
        }

        public int getTotalValue()
        {
            return BaseValue + SubraceAdd + SubraceBonus;
        }
    }
}