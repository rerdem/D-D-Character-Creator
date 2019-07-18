using Easy_DnD_Character_Creator.DataTypes;
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
        public Race RaceChoice { get; set; }
        
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
        public CharacterClass ClassChoice { get; set; }
        
        //BackgroundControl
        public Background BackgroundChoice { get; set; }

        //AbilityControl
        public List<AbilityScore> Abilities { get; set; }
        public AbilityScore Strength { get; set; }
        public AbilityScore Dexterity { get; set; }
        public AbilityScore Constitution { get; set; }
        public AbilityScore Intelligence { get; set; }
        public AbilityScore Wisdom { get; set; }
        public AbilityScore Charisma { get; set; }
        public int HP { get; set; }

        //LanguageControl
        public List<string> Languages { get; set; }

        //SkillControl
        public List<string> Skills { get; set; }
        public List<string> ExtraSkills { get; set; }

        //EquipmentControl
        public List<EquipmentItem> Equipment1 { get; set; }
        public List<EquipmentItem> Equipment2 { get; set; }
        public List<EquipmentItem> Equipment3 { get; set; }
        public List<EquipmentItem> Equipment4 { get; set; }
        public string ExtraEquipment { get; set; }

        //SpellControl
        public List<Spell> Spells { get; set; }

        //ExtraRaceChoiceControl
        public List<Spell> RaceSpells { get; set; }

        //ExtraClassChoiceControl
        
        //ExtraSubclassChoiceControl
        
        //NameControl
        public string CharacterName { get; set; }
        public string PlayerName { get; set; }
        public bool IsMale { get; set; }

        //StoryControl
        public string Trait { get; set; }
        public string Ideal { get; set; }
        public string Bond { get; set; }
        public string Flaw { get; set; }
        public bool CustomTrait { get; set; }
        public bool CustomIdeal { get; set; }
        public bool CustomBond { get; set; }
        public bool CustomFlaw { get; set; }
        public WildShapeTerrain TerrainChoice { get; set; }
        public string Backstory { get; set; }

        //ExportControl
        private List<string> allKnownSkills;
        public List<string> AllKnownSkills
        {
            get
            {
                allKnownSkills.Clear();
                allKnownSkills.AddRange(Skills);
                allKnownSkills.AddRange(ExtraSkills);
                allKnownSkills.AddRange(ClassChoice.ExtraSkills);
                allKnownSkills.AddRange(ClassChoice.getSelectedSubclass().ExtraSkills);
                allKnownSkills.AddRange(ClassChoice.WarlockInvocationSkills);
                return allKnownSkills;
            }
        }
        private List<EquipmentItem> chosenEquipment;
        public List<EquipmentItem> ChosenEquipment
        {
            get
            {
                chosenEquipment.Clear();
                //chosenEquipment = Equipment1;
                chosenEquipment.AddRange(Equipment1);
                chosenEquipment.AddRange(Equipment2);
                chosenEquipment.AddRange(Equipment3);
                chosenEquipment.AddRange(Equipment4);
                return chosenEquipment;
            }
        }

        public ChoiceManager()
        {
            Preset = 0;
            Level = 1;
            AdjustStartingMoney = true;

            RaceChoice = new Race();
            
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

            ClassChoice = new CharacterClass();
            
            BackgroundChoice = new Background();

            Strength = new AbilityScore("Strength");
            Dexterity = new AbilityScore("Dexterity");
            Constitution = new AbilityScore("Constitution");
            Intelligence = new AbilityScore("Intelligence");
            Wisdom = new AbilityScore("Wisdom");
            Charisma = new AbilityScore("Charisma");
            Abilities = new List<AbilityScore>(new AbilityScore[] { Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma });
            HP = 0;

            Languages = new List<string>();

            Skills = new List<string>();
            ExtraSkills = new List<string>();

            Equipment1 = new List<EquipmentItem>();
            Equipment2 = new List<EquipmentItem>();
            Equipment3 = new List<EquipmentItem>();
            Equipment4 = new List<EquipmentItem>();
            ExtraEquipment = "";

            Spells = new List<Spell>();

            RaceSpells = new List<Spell>();

            CharacterName = "";
            PlayerName = "";
            IsMale = false;

            Trait = "";
            Ideal = "";
            Bond = "";
            Flaw = "";
            CustomTrait = false;
            CustomIdeal = false;
            CustomBond = false;
            CustomFlaw = false;
            TerrainChoice = new WildShapeTerrain();
            Backstory = "";

            allKnownSkills = new List<string>();
            chosenEquipment = new List<EquipmentItem>();
        }
    }
}
