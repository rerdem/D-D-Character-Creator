using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class ExtraRaceSpell : Spell
    {
        public int MaxSpellLevel { get; }
        public string SpellcastingAbility { get; }

        public ExtraRaceSpell() : base()
        {
            MaxSpellLevel = 0;
            SpellcastingAbility = "";
        }

        public ExtraRaceSpell(string inputName, bool inputRitual, int inputLevel, string inputSchool, string inputCastTime, string inputRange, string inputDuration, string inputComponents, string inputMaterials, string inputDescription, bool inputNotDeselectable, int inputMaxSpellLevel, string inputSpellcastingAbility)
            : base(inputName, inputRitual, inputLevel, inputSchool, inputCastTime, inputRange, inputDuration, inputComponents, inputMaterials, inputDescription, inputNotDeselectable)
        {
            MaxSpellLevel = inputMaxSpellLevel;
            SpellcastingAbility = inputSpellcastingAbility;
        }
    }
}
