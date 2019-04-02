using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class Feature
    {
        public string Name { get; }
        public string Description { get; set; }

        public Feature()
        {
            Name = "";
            Description = "";
        }

        public Feature(string inputName, string inputDescription)
        {
            Name = inputName;
            Description = inputDescription;
        }
    }
}
