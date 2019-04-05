using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class Background : IEquatable<Background>
    {
        public string Name { get; }
        public string Description { get; }
        public string Equipment { get; }
        public int Gold { get; }
        public bool HasProficiencyChoice { get; }
        public string Proficiency { get; set; }
        public bool HasStoryChoice { get; }
        public BackgroundStoryChoice StoryChoice { get; set; }

        public Background()
        {
            Name = "";
            Description = "";
            Equipment = "";
            Gold = 0;
            HasProficiencyChoice = false;
            Proficiency = "";
            HasStoryChoice = false;
            StoryChoice = new BackgroundStoryChoice();

        }

        public Background(string inputName, string inputDescription, string inputEquipment, int inputGold, bool inputHasProficiencyChoice, bool inputHasStoryChoice)
        {
            Name = inputName;
            Description = inputDescription;
            Equipment = inputEquipment;
            Gold = inputGold;
            HasProficiencyChoice = inputHasProficiencyChoice;
            Proficiency = "";
            HasStoryChoice = inputHasStoryChoice;
            StoryChoice = new BackgroundStoryChoice();

        }

        public bool Equals(Background other)
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

            Background otherBackground = other as Background;
            if (otherBackground == null)
            {
                return false;
            }

            return Equals(otherBackground);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
