using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class Spell : IEquatable<Spell>
    {
        public string Name { get; }
        public bool Ritual { get; }
        public int Level { get; }
        public string School { get; }
        public string CastTime { get; }
        public string Range { get; }
        public string Duration { get; }
        public string Components { get; }
        public string Materials { get; }
        public string Description { get; }
        public bool NotDeselectable { get; }

        public Spell()
        {
            Name = "";
            Ritual = false;
            Level = 0;
            School = "";
            CastTime = "";
            Range = "";
            Duration = "";
            Components = "";
            Materials = "";
            Description = "";
            NotDeselectable = false;
        }

        public Spell(string inputName, bool inputRitual, int inputLevel, string inputSchool, string inputCastTime, string inputRange, string inputDuration, string inputComponents, string inputMaterials, string inputDescription, bool inputNotDeselectable)
        {
            Name = inputName;
            Ritual = inputRitual;
            Level = inputLevel;
            School = inputSchool;
            CastTime = inputCastTime;
            Range = inputRange;
            Duration = inputDuration;
            Components = inputComponents;
            Materials = inputMaterials;
            Description = inputDescription;
            NotDeselectable = inputNotDeselectable;
        }

        public bool Equals(Spell other)
        {
            if (other == null)
            {
                return false;
            }

            return Name == other.Name;
                   //Ritual == other.Ritual &&
                   //Level == other.Level &&
                   //School == other.School &&
                   //CastTime == other.CastTime &&
                   //Range == other.Range &&
                   //Duration == other.Duration &&
                   //Components == other.Components &&
                   //Materials == other.Materials &&
                   //Description == other.Description &&
                   //NotDeselectable == other.NotDeselectable;
        }

        public override bool Equals(Object other)
        {
            if (other == null)
            {
                return false;
            }

            Spell otherSpell = other as Spell;
            if (otherSpell == null)
            {
                return false;
            }

            return Equals(otherSpell);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}