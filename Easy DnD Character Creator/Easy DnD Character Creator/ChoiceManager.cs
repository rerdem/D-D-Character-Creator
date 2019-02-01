using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator
{
    public class ChoiceManager
    {
        //IntroControl
        public int Preset { get; set; }
        public int Level { get; set; }
        public bool AdjustStartingMoney { get; set; }

        //RaceControl
        public string Race { get; set; }
        public string Subrace { get; set; }
        public string RaceProficiency { get; set; }

        //AlignmentControl
        public string LawAlignment { get; set; }
        public string MoralityAlignment { get; set; }

        //AgeControl
        public int Age { get; set; }

        //BodyControl
        public int HeightModifier { get; set; }
        public int WeightModifier { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }

        //AppearanceControl
        public string EyeColor { get; set; }
        public string SkinColor { get; set; }
        public string HairColor { get; set; }

        //ClassControl
        public string Class { get; set; }
        public string Subclass { get; set; }
        public string ClassProficiency { get; set; }

        //BackgroundControl
        public string Background { get; set; }
        public string BackgroundChoice { get; set; }

        public ChoiceManager()
        {
            Preset = 0;
            Level = 1;
            AdjustStartingMoney = true;

            Race = "";
            Subrace = "";
            RaceProficiency = "";

            LawAlignment = "";
            MoralityAlignment = "";

            Age = 1;

            HeightModifier = 0;
            WeightModifier = 0;
            Height = "";
            Weight = "";

            EyeColor = "";
            SkinColor = "";
            HairColor = "";

            Class = "";
            Subclass = "";
            ClassProficiency = "";

            Background = "";
            BackgroundChoice = "";
        }
    }
}
