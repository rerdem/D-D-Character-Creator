using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class CircleTerrain
    {
        public string Name { get; }
        public List<Spell> Spells { get; }

        public CircleTerrain()
        {
            Name = "";
            Spells = new List<Spell>();
        }

        public CircleTerrain(string inputName)
        {
            Name = inputName;
            Spells = new List<Spell>();
        }

        public CircleTerrain(string inputName, List<Spell> inputSpells)
        {
            Name = inputName;
            Spells = inputSpells;
        }

        public void addSpell(Spell newSpell)
        {
            Spells.Add(newSpell);
        }

        public void removeSpell(Spell oldSpell)
        {
            Spells.Remove(oldSpell);
        }

        public bool Equals(CircleTerrain other)
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

            CircleTerrain otherTerrain = other as CircleTerrain;
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
