using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class Skill : IEquatable<Skill>
    {
        public string Name { get; }
        public string Description { get; }
        public string Ability { get; }

        public Skill()
        {
            Name = "";
            Description = "";
            Ability = "";
        }

        public Skill(string inputName, string inputDescription, string inputAbility)
        {
            Name = inputName;
            Description = inputDescription;
            Ability = inputAbility;
        }

        public bool Equals(Skill other)
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

            Skill otherSkill = other as Skill;
            if (otherSkill == null)
            {
                return false;
            }

            return Equals(otherSkill);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
