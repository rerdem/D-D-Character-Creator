using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class Maneuver : IEquatable<Maneuver>
    {
        public string Name { get; }
        public string Description { get; }

        public Maneuver()
        {
            Name = "";
            Description = "";
        }

        public Maneuver(string inputName, string inputDescription)
        {
            Name = inputName;
            Description = inputDescription;
        }

        public bool Equals(Maneuver other)
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

            Maneuver otherManeuver = other as Maneuver;
            if (otherManeuver == null)
            {
                return false;
            }

            return Equals(otherManeuver);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
