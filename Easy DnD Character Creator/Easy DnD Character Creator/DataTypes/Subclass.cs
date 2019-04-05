using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class Subclass : IEquatable<Subclass>
    {
        public string Name { get; }
        public string Description { get; }
        public bool HasExtraSubclassChoice
        {
            get
            {
                return HasExtraSkills ||
                       HasTotems ||
                       HasExtraSpells ||
                       HasExtraToolProficiencies ||
                       HasManeuvers ||
                       HasDraconicAncestry ||
                       HasElementalDisciplines ||
                       HasHunterChoices ||
                       HasCompanion ||
                       HasCircleTerrain;
            }
        }

        public bool HasExtraSkills { get; set; }
        public bool HasTotems { get; set; }
        public bool HasExtraSpells { get; set; }
        public bool HasExtraToolProficiencies { get; set; }
        public bool HasManeuvers { get; set; }
        public bool HasDraconicAncestry { get; set; }
        public bool HasElementalDisciplines { get; set; }
        public bool HasHunterChoices { get; set; }
        public bool HasCompanion { get; set; }
        public bool HasCircleTerrain { get; set; }

        public List<string> ExtraSkills { get; set; }
        public bool DoublesProficiency { get; set; }
        public List<ChoiceFeature> TotemFeatures { get; set; }
        public List<Spell> ExtraSpells { get; set; }
        public string ExtraToolProficiency { get; set; }
        public List<Maneuver> Maneuvers { get; set; }
        public List<ElementalDiscipline> MandatoryDisciplines { get; set; }
        public List<ElementalDiscipline> ChosenDisciplines { get; set; }
        public DraconicAncestry Ancestry { get; set; }
        public List<ChoiceFeature> HunterFeatures { get; set; }
        public Beast BeastCompanion { get; set; }
        public CircleTerrain DruidCircleTerrain { get; set; }

        public Subclass()
        {
            Name = "";
            Description = "";

            HasExtraSkills = false;
            HasTotems = false;
            HasExtraSpells = false;
            HasExtraToolProficiencies = false;
            HasManeuvers = false;
            HasDraconicAncestry = false;
            HasElementalDisciplines = false;
            HasHunterChoices = false;
            HasCompanion = false;
            HasCircleTerrain = false;

            ExtraSkills = new List<string>();
            DoublesProficiency = false;
            TotemFeatures = new List<ChoiceFeature>();
            ExtraSpells = new List<Spell>();
            ExtraToolProficiency = "";
            Maneuvers = new List<Maneuver>();
            MandatoryDisciplines = new List<ElementalDiscipline>();
            ChosenDisciplines = new List<ElementalDiscipline>();
            Ancestry = new DraconicAncestry();
            HunterFeatures = new List<ChoiceFeature>();
            BeastCompanion = new Beast();
            DruidCircleTerrain = new CircleTerrain();
        }

        public Subclass(string inputName, string inputDescription)
        {
            Name = inputName;
            Description = inputDescription;

            HasExtraSkills = false;
            HasTotems = false;
            HasExtraSpells = false;
            HasExtraToolProficiencies = false;
            HasManeuvers = false;
            HasDraconicAncestry = false;
            HasElementalDisciplines = false;
            HasHunterChoices = false;
            HasCompanion = false;
            HasCircleTerrain = false;

            ExtraSkills = new List<string>();
            DoublesProficiency = false;
            TotemFeatures = new List<ChoiceFeature>();
            ExtraSpells = new List<Spell>();
            ExtraToolProficiency = "";
            Maneuvers = new List<Maneuver>();
            MandatoryDisciplines = new List<ElementalDiscipline>();
            ChosenDisciplines = new List<ElementalDiscipline>();
            Ancestry = new DraconicAncestry();
            HunterFeatures = new List<ChoiceFeature>();
            BeastCompanion = new Beast();
            DruidCircleTerrain = new CircleTerrain();
        }

        public bool Equals(Subclass other)
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

            Subclass otherSubclass = other as Subclass;
            if (otherSubclass == null)
            {
                return false;
            }

            return Equals(otherSubclass);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
