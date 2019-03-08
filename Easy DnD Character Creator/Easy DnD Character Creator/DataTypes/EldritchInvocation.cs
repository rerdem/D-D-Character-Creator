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
        public bool RequiresEldritchBlast { get; }
        public string PactRestriction { get; }
        public Spell gainedSpell { get; }

        public EldritchInvocation()
        {
            Name = "";
            Description = "";
            LevelRestriction = 0;
            RequiresEldritchBlast = false;
            PactRestriction = "";
            gainedSpell = new Spell();
        }

        public EldritchInvocation(string inputName, string inputDescription, int inputLevelRestriction, bool inputRequiresEldritchBlast, string inputPactRestriction, Spell inputGainedSpell)
        {
            Name = inputName;
            Description = inputDescription;
            LevelRestriction = inputLevelRestriction;
            RequiresEldritchBlast = inputRequiresEldritchBlast;
            PactRestriction = inputPactRestriction;
            gainedSpell = inputGainedSpell;
        }
    }
}
