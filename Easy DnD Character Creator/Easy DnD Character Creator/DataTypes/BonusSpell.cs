using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class BonusSpell : Spell
    {
        public bool OnlyAsRitual { get; }

        public BonusSpell() : base()
        {
            OnlyAsRitual = false;
        }

        public BonusSpell(string inputName, bool inputRitual, int inputLevel, string inputSchool, string inputCastTime, string inputRange, string inputDuration, string inputComponents, string inputMaterials, string inputDescription, bool inputNotDeselectable, bool inputOnlyAsRitual)
            : base(inputName, inputRitual, inputLevel, inputSchool, inputCastTime, inputRange, inputDuration, inputComponents, inputMaterials, inputDescription, inputNotDeselectable)
        {
            OnlyAsRitual = inputOnlyAsRitual;
        }
    }
}
