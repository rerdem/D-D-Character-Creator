using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class Tool : EquipmentItem
    {
        public Tool()
        {
            Name = "";
        }

        public Tool(string inputName)
        {
            Name = inputName;
        }
    }
}
