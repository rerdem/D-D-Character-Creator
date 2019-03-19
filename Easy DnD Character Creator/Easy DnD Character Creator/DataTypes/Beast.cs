using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class Beast : IEquatable<Beast>
    {
        public string Name { get; }
        public string Book { get; }
        public int Page { get; }

        public Beast()
        {
            Name = "";
            Book = "";
            Page = 0;
        }

        public Beast(string inputName, string inputBook, int inputPage)
        {
            Name = inputName;
            Book = inputBook;
            Page = inputPage;
        }

        public bool Equals(Beast other)
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

            Beast otherBeast = other as Beast;
            if (otherBeast == null)
            {
                return false;
            }

            return Equals(otherBeast);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
