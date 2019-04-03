using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class EldritchInvocation : IEquatable<EldritchInvocation>
    {
        public string Name { get; }
        public string Description { get; }
        public int LevelRestriction { get; }
        public Spell RequiredSpell { get; }
        public string PactRestriction { get; }
        public Spell GainedSpell { get; }
        public bool HasSpellChoice { get; }
        public bool HasSkillGain { get; }

        public EldritchInvocation()
        {
            Name = "";
            Description = "";
            LevelRestriction = 0;
            RequiredSpell = new Spell();
            PactRestriction = "";
            GainedSpell = new Spell();
            HasSpellChoice = false;
            HasSkillGain = false;
        }

        public EldritchInvocation(string inputName, string inputDescription, int inputLevelRestriction, Spell inputRequiredSpell, string inputPactRestriction, Spell inputGainedSpell, bool inputHasSpellChoice, bool inputHasSkillGain)
        {
            Name = inputName;
            Description = inputDescription;
            LevelRestriction = inputLevelRestriction;
            RequiredSpell = inputRequiredSpell;
            PactRestriction = inputPactRestriction;
            GainedSpell = inputGainedSpell;
            HasSpellChoice = inputHasSpellChoice;
            HasSkillGain = inputHasSkillGain;
        }

        public bool Equals(EldritchInvocation other)
        {
            if (other == null)
            {
                return false;
            }

            return Name == other.Name;
        }

        public override bool Equals(Object other)
        {
            if (other == null)
            {
                return false;
            }

            EldritchInvocation otherInvocation = other as EldritchInvocation;
            if (otherInvocation == null)
            {
                return false;
            }

            return Equals(otherInvocation);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
