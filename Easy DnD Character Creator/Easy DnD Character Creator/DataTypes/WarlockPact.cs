using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class WarlockPact
    {
        public string Name { get; }
        public string Description { get; }
        public int SpellAmount { get; }

        public WarlockPact()
        {
            Name = "";
            Description = "";
            SpellAmount = 0;
        }

        public WarlockPact(string inputName, string inputDescription, int inputSpellAmount)
        {
            Name = inputName;
            Description = inputDescription;
            SpellAmount = inputSpellAmount;
        }
    }
}
