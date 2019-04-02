using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class WildShapeTerrain : IEquatable<WildShapeTerrain>
    {
        public string Name { get; }
        public List<WildShapeBeast> Beasts { get; }

        public WildShapeTerrain()
        {
            Name = "";
            Beasts = new List<WildShapeBeast>();
        }

        public WildShapeTerrain(string inputName)
        {
            Name = inputName;
            Beasts = new List<WildShapeBeast>();
        }

        public void addBeast(WildShapeBeast beast)
        {
            Beasts.Add(beast);
        }

        public void removeBeast(WildShapeBeast beast)
        {
            Beasts.Remove(beast);
        }

        public bool Equals(WildShapeTerrain other)
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

            WildShapeTerrain otherTerrain = other as WildShapeTerrain;
            if (otherTerrain == null)
            {
                return false;
            }

            return Equals(otherTerrain);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
