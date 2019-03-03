using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class FightingStyle
    {
        public string Name { get; }
        public string Description { get; }

        public FightingStyle()
        {
            Name = "";
            Description = "";
        }

        public FightingStyle(string inputName, string inputDescription)
        {
            Name = inputName;
            Description = inputDescription;
        }
    }
}
