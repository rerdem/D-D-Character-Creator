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
        public List<string> ClassProficiencies { get; set; }
        public bool HasExtraClassChoice { get; set; }
        public bool HasExtraSubclassChoice { get; set; }
        public bool HasSpellcasting { get; set; }
        public bool ChoosesSpells { get; set; }

        public bool HasFightingStyle { get; set; }
        public bool HasFavoredEnemy { get; set; }
        public bool HasFavoredTerrain { get; set; }
        public bool HasExtraClassSkills { get; set; }
        public bool HasWarlockPact { get; set; }
        public bool HasEldritchInvocations { get; set; }
        public bool HasWarlockChoices { get; set; }
        public bool HasMetamagic { get; set; }

        public bool HasExtraSubclassSkills { get; set; }
        public bool HasTotems { get; set; }
        public bool HasExtraSubclassSpells { get; set; }
        public bool HasExtraToolProficiencies { get; set; }
        public bool HasManeuvers { get; set; }
        public bool HasDraconicAncestry { get; set; }
        public bool HasElementalDisciplines { get; set; }
        public bool HasHunterChoices { get; set; }
        public bool HasCompanion { get; set; }
        public bool HasCircleTerrain { get; set; }

        public bool HasWildShape { get; set; }

        //BackgroundControl
        public string Background { get; set; }
        public bool HasBackgroundProficiencyChoice { get; set; }
        public string BackgroundProficiency { get; set; }
        public bool HasBackgroundStoryChoice { get; set; }

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
        public List<FightingStyle> ClassFightingStyles { get; set; }
        public string FavoredEnemies { get; set; }
        public string FavoredTerrains { get; set; }
        public List<string> ClassSkills { get; set; }
        public bool ClassDoublesProficiency { get; set; }
        public WarlockPact WarlockPactChoice { get; set; }
        public List<Spell> WarlockPactSpells { get; set; }
        public List<EldritchInvocation> WarlockInvocations { get; set; }
        public List<Spell> WarlockInvocationSpells { get; set; }
        public List<string> WarlockInvocationSkills { get; set; }
        public List<Metamagic> SorcererMetamagic { get; set; }

        //ExtraSubclassChoiceControl
        public List<string> SubclassSkills { get; set; }
        public bool SubclassDoublesProficiency { get; set; }
        public List<ChoiceFeature> TotemFeatures { get; set; }
        public List<Spell> SubclassSpells { get; set; }
        public string SubclassToolProficiency { get; set; }
        public List<Maneuver> Maneuvers { get; set; }
        public List<ElementalDiscipline> MandatoryDisciplines { get; set; }
        public List<ElementalDiscipline> ChosenDisciplines { get; set; }
        public DraconicAncestry Ancestry { get; set; }
        public List<ChoiceFeature> HunterFeatures { get; set; }
        public Beast BeastCompanion { get; set; }
        public CircleTerrain DruidCircleTerrain { get; set; }

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
        public BackgroundStoryChoice BackgroundChoice { get; set; }
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
                allKnownSkills.AddRange(ClassSkills);
                allKnownSkills.AddRange(SubclassSkills);
                allKnownSkills.AddRange(WarlockInvocationSkills);
                return allKnownSkills;
            }
        }
        private List<EquipmentItem> chosenEquipment;
        public List<EquipmentItem> ChosenEquipment
        {
            get
            {
                chosenEquipment.Clear();
                chosenEquipment = Equipment1;
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
            ClassProficiencies = new List<string>();
            HasExtraClassChoice = false;
            HasExtraSubclassChoice = false;
            HasSpellcasting = false;
            ChoosesSpells = false;

            HasFightingStyle = false;
            HasFavoredEnemy = false;
            HasFavoredTerrain = false;
            HasExtraClassSkills = false;
            HasWarlockPact = false;
            HasEldritchInvocations = false;
            HasWarlockChoices = false;
            HasMetamagic = false;

            HasExtraSubclassSkills = false;
            HasTotems = false;
            HasExtraSubclassSpells = false;
            HasExtraToolProficiencies = false;
            HasManeuvers = false;
            HasDraconicAncestry = false;
            HasElementalDisciplines = false;
            HasHunterChoices = false;
            HasCompanion = false;
            HasCircleTerrain = false;

            Background = "";
            HasBackgroundProficiencyChoice = false;
            BackgroundProficiency = "";
            HasBackgroundStoryChoice = false;

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

            ClassFightingStyles = new List<FightingStyle>();
            FavoredEnemies = "";
            FavoredTerrains = "";
            ClassSkills = new List<string>();
            ClassDoublesProficiency = false;
            WarlockPactChoice = new WarlockPact();
            WarlockPactSpells = new List<Spell>();
            WarlockInvocations = new List<EldritchInvocation>();
            WarlockInvocationSpells = new List<Spell>();
            WarlockInvocationSkills = new List<string>();
            SorcererMetamagic = new List<Metamagic>();

            SubclassSkills = new List<string>();
            SubclassDoublesProficiency = false;
            TotemFeatures = new List<ChoiceFeature>();
            SubclassSpells = new List<Spell>();
            SubclassToolProficiency = "";
            Maneuvers = new List<Maneuver>();
            MandatoryDisciplines = new List<ElementalDiscipline>();
            ChosenDisciplines = new List<ElementalDiscipline>();
            Ancestry = new DraconicAncestry();
            HunterFeatures = new List<ChoiceFeature>();
            BeastCompanion = new Beast();
            DruidCircleTerrain = new CircleTerrain();

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
            BackgroundChoice = new BackgroundStoryChoice();
            Backstory = "";

            allKnownSkills = new List<string>();
            chosenEquipment = new List<EquipmentItem>();
        }
    }
}
