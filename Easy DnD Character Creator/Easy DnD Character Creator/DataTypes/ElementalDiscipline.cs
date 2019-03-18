using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class ElementalDiscipline : IEquatable<ElementalDiscipline>
    {
        public string Name { get; }
        public string Description { get; }
        public Spell GainedSpell { get; }

        public ElementalDiscipline()
        {
            Name = "";
            Description = "";
            GainedSpell = new Spell();
        }

        public ElementalDiscipline(string inputName, string inputDescription, Spell inputSpell)
        {
            Name = inputName;
            Description = inputDescription;
            GainedSpell = inputSpell;
        }

        public bool Equals(ElementalDiscipline other)
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

            ElementalDiscipline otherDiscipline = other as ElementalDiscipline;
            if (otherDiscipline == null)
            {
                return false;
            }

            return Equals(otherDiscipline);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
