using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator
{
    public class ChoiceManager
    {
        public int Preset { get; set; }
        public int Level { get; set; }
        public bool AdjustStartingMoney { get; set; }
        public string Race { get; set; }
        public string Subrace { get; set; }

        public ChoiceManager()
        {
            Preset = 0;
            Level = 1;
            AdjustStartingMoney = true;
            Race = "";
            Subrace = "";
        }
    }
}
