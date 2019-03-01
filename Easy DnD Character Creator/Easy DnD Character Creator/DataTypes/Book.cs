using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class Book
    {
        public int Id { get;  }
        public string Title { get; }
        public string Shorthand { get; }
        public bool Active { get; }
        public bool Mandatory { get; }

        public Book(int inputId, string inputTitle, string inputShorthand, int inputActive, int inputMandatory)
        {
            Id = inputId;
            Title = inputTitle;
            Shorthand = inputShorthand;

            if (inputActive == 0)
            {
                Active = false;
            }
            else
            {
                Active = true;
            }

            if (inputMandatory == 0)
            {
                Mandatory = false;
            }
            else
            {
                Mandatory = true;
            }
        }
    }
}