using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class EldritchInvocation
    {
        public string Name { get; }
        public string Description { get; }
        public int LevelRestriction { get; }
        public Spell RequiredSpell { get; }
        public string PactRestriction { get; }
        public Spell GainedSpell { get; }
        public bool HasSpellChoice { get; }

        public EldritchInvocation()
        {
            Name = "";
            Description = "";
            LevelRestriction = 0;
            RequiredSpell = new Spell();
            PactRestriction = "";
            GainedSpell = new Spell();
            HasSpellChoice = false;
        }

        public EldritchInvocation(string inputName, string inputDescription, int inputLevelRestriction, Spell inputRequiredSpell, string inputPactRestriction, Spell inputGainedSpell, bool inputHasSpellChoice)
        {
            Name = inputName;
            Description = inputDescription;
            LevelRestriction = inputLevelRestriction;
            RequiredSpell = inputRequiredSpell;
            PactRestriction = inputPactRestriction;
            GainedSpell = inputGainedSpell;
            HasSpellChoice = inputHasSpellChoice;
        }
    }
}
