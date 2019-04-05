using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class Subrace : IEquatable<Subrace>
    {
        public string Name { get; }
        public string Description { get; }
        public string AlignmentDescription { get; }
        public int Speed { get; }
        public bool HasProficiencyChoice { get; }
        public string Proficiency { get; set; }
        public bool HasExtraRaceChoice
        {
            get
            {
                return HasExtraSpells;
            }
        }
        public bool HasExtraSpells { get; set; }

        public Subrace()
        {
            Name = "";
            Description = "";
            AlignmentDescription = "";
            Speed = 0;
            HasProficiencyChoice = false;
            Proficiency = "";
            HasExtraSpells = false;
        }

        public Subrace(string inputName, string inputDescription, string inputAlignmentDescription, int inputSpeed, bool inputProficiencyChoice)
        {
            Name = inputName;
            Description = inputDescription;
            AlignmentDescription = inputAlignmentDescription;
            Speed = inputSpeed;
            HasProficiencyChoice = inputProficiencyChoice;
            Proficiency = "";
            HasExtraSpells = false;
        }

        public bool Equals(Subrace other)
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

            Subrace otherSubrace = other as Subrace;
            if (otherSubrace == null)
            {
                return false;
            }

            return Equals(otherSubrace);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
