using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class Language
    {
        public string Name { get; }
        public string Type { get; }
        public string Speakers { get; }
        public string Script { get; }

        public Language()
        {
            Name = "";
            Type = "";
            Speakers = "";
            Script = "";
        }

        public Language(string inputName, string inputType, string inputSpeakers, string inputScript)
        {
            Name = inputName;
            Type = inputType;
            Speakers = inputSpeakers;
            Script = inputScript;
        }
    }
}
