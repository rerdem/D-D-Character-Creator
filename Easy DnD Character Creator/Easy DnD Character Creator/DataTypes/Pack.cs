using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class Pack : EquipmentItem
    {
        //public string Name { get; }
        public string Content { get; }

        public Pack()
        {
            Name = "";
            Content = "";
        }

        public Pack(string inputName, string inputContent)
        {
            Name = inputName;
            Content = inputContent;
        }
    }
}
