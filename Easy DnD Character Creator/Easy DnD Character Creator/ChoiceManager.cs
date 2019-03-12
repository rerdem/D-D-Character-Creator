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
        public string Race { get; set; }
        public string Subrace { get; set; }
        public string RaceProficiency { get; set; }
        public bool HasExtraRaceChoice { get; set; }

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
        public bool HasExtraClassChoice { get; set; }
        public bool HasExtraSubclassChoice { get; set; }
        public bool HasSpellcasting { get; set; }
        public bool ChoosesSpells { get; set; }

        //BackgroundControl
        public string Background { get; set; }
        public string BackgroundProficiency { get; set; }

        //AbilityControl
        public AbilityScore Strength { get; set; }
        public AbilityScore Dexterity { get; set; }
        public AbilityScore Constitution { get; set; }
        public AbilityScore Intelligence { get; set; }
        public AbilityScore Wisdom { get; set; }
        public AbilityScore Charisma { get; set; }

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
        public List<Object> ExtraRaceChoices { get; set; }

        //ExtraClassChoiceControl
        public List<FightingStyle> ClassFightingStyles { get; set; }
        public string FavoredEnemies { get; set; }
        public string FavoredTerrains { get; set; }
        public List<string> ClassSkills { get; set; }
        public bool DoublesProficiency { get; set; }
        public WarlockPact WarlockPactChoice { get; set; }
        public List<Spell> WarlockPactSpells { get; set; }
        public List<EldritchInvocation> WarlockInvocations { get; set; }
        public List<Spell> WarlockInvocationSpells { get; set; }
        public List<string> WarlockInvocationSkills { get; set; }
        public List<Metamagic> SorcererMetamagic { get; set; }

        public ChoiceManager()
        {
            Preset = 0;
            Level = 1;
            AdjustStartingMoney = true;

            Race = "";
            Subrace = "";
            RaceProficiency = "";
            HasExtraRaceChoice = false;

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
            HasExtraClassChoice = false;
            HasExtraSubclassChoice = false;
            HasSpellcasting = false;
            ChoosesSpells = false;

            Background = "";
            BackgroundProficiency = "";

            Strength = new AbilityScore();
            Dexterity = new AbilityScore();
            Constitution = new AbilityScore();
            Intelligence = new AbilityScore();
            Wisdom = new AbilityScore();
            Charisma = new AbilityScore();

            Languages = new List<string>();

            Skills = new List<string>();
            ExtraSkills = new List<string>();

            Equipment1 = new List<EquipmentItem>();
            Equipment2 = new List<EquipmentItem>();
            Equipment3 = new List<EquipmentItem>();
            Equipment4 = new List<EquipmentItem>();
            ExtraEquipment = "";

            Spells = new List<Spell>();

            ExtraRaceChoices = new List<Object>();

            ClassFightingStyles = new List<FightingStyle>();
            FavoredEnemies = "";
            FavoredTerrains = "";
            ClassSkills = new List<string>();
            DoublesProficiency = false;
            WarlockPactChoice = new WarlockPact();
            WarlockPactSpells = new List<Spell>();
            WarlockInvocations = new List<EldritchInvocation>();
            WarlockInvocationSpells = new List<Spell>();
            WarlockInvocationSkills = new List<string>();
            SorcererMetamagic = new List<Metamagic>();
        }
    }
}
