using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator
{
    public class Spell
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
        }

        public Spell(string inputName, bool inputRitual, int inputLevel, string inputSchool, string inputCastTime, string inputRange, string inputDuration, string inputComponents, string inputMaterials, string inputDescription)
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
        }
    }
}
