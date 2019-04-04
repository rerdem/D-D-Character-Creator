using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class Race : IEquatable<Race>
    {
        public string Name { get; }
        public List<Subrace> Subraces { get; }
        private int selectedIndex;

        public Race()
        {
            Name = "";
            Subraces = new List<Subrace>();
            selectedIndex = -1;
        }

        public Race(string inputName)
        {
            Name = inputName;
            Subraces = new List<Subrace>();
            selectedIndex = -1;
        }

        public void addSubrace(Subrace newSubrace)
        {
            Subraces.Add(newSubrace);
        }

        public void removeSubrace(Subrace subraceToRemove)
        {
            Subraces.Remove(subraceToRemove);
        }

        public void setSelectedSubrace(Subrace subraceToSelect)
        {
            selectedIndex = Subraces.IndexOf(subraceToSelect);
        }

        public Subrace getSelectedSubrace()
        {
            if ((selectedIndex >= 0) && (selectedIndex < Subraces.Count))
            {
                return Subraces[selectedIndex];
            }

            return new Subrace();
        }

        public bool Equals(Race other)
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

            Race otherRace = other as Race;
            if (otherRace == null)
            {
                return false;
            }

            return Equals(otherRace);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
