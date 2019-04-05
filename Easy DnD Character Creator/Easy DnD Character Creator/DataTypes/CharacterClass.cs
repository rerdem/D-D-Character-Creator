using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class CharacterClass : IEquatable<CharacterClass>
    {
        public string Name { get; }
        public string Description { get; }
        public string HitDie { get; }
        public List<Subclass> Subclasses { get; set; }
        public bool HasProficiencyChoice { get; }
        public int ProficiencyChoiceAmount { get; }
        public List<string> Proficiencies { get; set; }

        public bool HasExtraClassChoice
        {
            get
            {
                return HasFightingStyle ||
                       HasFavoredEnemy ||
                       HasFavoredTerrain ||
                       HasExtraSkills ||
                       HasWarlockChoices ||
                       HasMetamagic;
            }
        }
        public bool HasSpellcasting { get; set; }
        public bool ChoosesSpells { get; set; }

        public bool HasFightingStyle { get; set; }
        public bool HasFavoredEnemy { get; set; }
        public bool HasFavoredTerrain { get; set; }
        public bool HasFavoredEnemyTerrain
        {
            get
            {
                return HasFavoredEnemy || HasFavoredTerrain;
            }
        }
        public bool HasExtraSkills { get; set; }
        public bool HasWarlockPact { get; set; }
        public bool HasEldritchInvocations { get; set; }
        public bool HasWarlockChoices
        {
            get
            {
                return HasWarlockPact || HasEldritchInvocations;
            }
        }
        public bool HasMetamagic { get; set; }
        public bool HasWildShape { get; set; }

        public List<FightingStyle> FightingStyles { get; set; }
        public string FavoredEnemies { get; set; }
        public string FavoredTerrains { get; set; }
        public List<string> ExtraSkills { get; set; }
        public bool DoublesProficiency { get; set; }
        public WarlockPact WarlockPactChoice { get; set; }
        public List<Spell> WarlockPactSpells { get; set; }
        public List<EldritchInvocation> WarlockInvocations { get; set; }
        public List<Spell> WarlockInvocationSpells { get; set; }
        public List<string> WarlockInvocationSkills { get; set; }
        public List<Metamagic> SorcererMetamagic { get; set; }

        private int selectedIndex;

        public CharacterClass()
        {
            Name = "";
            Description = "";
            HitDie = "";
            Subclasses = new List<Subclass>();
            HasProficiencyChoice = false;
            ProficiencyChoiceAmount = 0;
            Proficiencies = new List<string>();

            HasSpellcasting = false;
            ChoosesSpells = false;

            HasFightingStyle = false;
            HasFavoredEnemy = false;
            HasFavoredTerrain = false;
            HasExtraSkills = false;
            HasWarlockPact = false;
            HasEldritchInvocations = false;
            HasMetamagic = false;
            HasWildShape = false;

            FightingStyles = new List<FightingStyle>();
            FavoredEnemies = "";
            FavoredTerrains = "";
            ExtraSkills = new List<string>();
            DoublesProficiency = false;
            WarlockPactChoice = new WarlockPact();
            WarlockPactSpells = new List<Spell>();
            WarlockInvocations = new List<EldritchInvocation>();
            WarlockInvocationSpells = new List<Spell>();
            WarlockInvocationSkills = new List<string>();
            SorcererMetamagic = new List<Metamagic>();

            selectedIndex = 0;
        }

        public CharacterClass(string inputName, string inputDescription, string inputHitDie, bool inputHasProficiencyChoice, int inputProficiencyChoiceAmount)
        {
            Name = inputName;
            Description = inputDescription;
            HitDie = inputHitDie;
            Subclasses = new List<Subclass>();
            HasProficiencyChoice = inputHasProficiencyChoice;
            ProficiencyChoiceAmount = inputProficiencyChoiceAmount;
            Proficiencies = new List<string>();

            HasSpellcasting = false;
            ChoosesSpells = false;

            HasFightingStyle = false;
            HasFavoredEnemy = false;
            HasFavoredTerrain = false;
            HasExtraSkills = false;
            HasWarlockPact = false;
            HasEldritchInvocations = false;
            HasMetamagic = false;
            HasWildShape = false;

            FightingStyles = new List<FightingStyle>();
            FavoredEnemies = "";
            FavoredTerrains = "";
            ExtraSkills = new List<string>();
            DoublesProficiency = false;
            WarlockPactChoice = new WarlockPact();
            WarlockPactSpells = new List<Spell>();
            WarlockInvocations = new List<EldritchInvocation>();
            WarlockInvocationSpells = new List<Spell>();
            WarlockInvocationSkills = new List<string>();
            SorcererMetamagic = new List<Metamagic>();

            selectedIndex = 0;
        }

        public void setSelectedSubclass(Subclass subclassToSelect)
        {
            selectedIndex = Subclasses.IndexOf(subclassToSelect);
        }

        public Subclass getSelectedSubclass()
        {
            if ((selectedIndex >= 0) && (selectedIndex < Subclasses.Count))
            {
                return Subclasses[selectedIndex];
            }

            return new Subclass();
        }

        public bool Equals(CharacterClass other)
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

            CharacterClass otherClass = other as CharacterClass;
            if (otherClass == null)
            {
                return false;
            }

            return Equals(otherClass);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
