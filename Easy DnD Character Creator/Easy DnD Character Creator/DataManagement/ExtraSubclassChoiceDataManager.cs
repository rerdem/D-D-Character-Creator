using Easy_DnD_Character_Creator.DataManagement.ExtraSubclassManagers;
using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public class ExtraSubclassChoiceDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public ExtraSubclassSkillDataManager ExtraSubclassSkillData { get; }
        public TotemDataManager TotemData { get; }
        public ExtraSubclassSpellDataManager ExtraSubclassSpellData { get; }
        public ExtraToolProficiencyDataManager ExtraToolProficiencyData { get; }
        public ManeuverDataManager ManeuverData { get; }
        public ElementalDisciplineDataManager ElementalDisciplineData { get; }
        public DraconicAncestryDataManager DraconicAncestryData { get; }
        public HunterDataManager HunterData { get; }
        public BeastCompanionDataManager BeastCompanionData { get; }
        public CircleTerrainDataManager CircleTerrainData { get; }

        public ExtraSubclassChoiceDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;

            ExtraSubclassSkillData = new ExtraSubclassSkillDataManager(ConnectionString, inputUsedBooks);
            TotemData = new TotemDataManager(ConnectionString, inputUsedBooks);
            ExtraSubclassSpellData = new ExtraSubclassSpellDataManager(ConnectionString, inputUsedBooks);
            ExtraToolProficiencyData = new ExtraToolProficiencyDataManager(ConnectionString, inputUsedBooks);
            ManeuverData = new ManeuverDataManager(ConnectionString, inputUsedBooks);
            ElementalDisciplineData = new ElementalDisciplineDataManager(ConnectionString, inputUsedBooks);
            DraconicAncestryData = new DraconicAncestryDataManager(ConnectionString, inputUsedBooks);
            HunterData = new HunterDataManager(ConnectionString, inputUsedBooks);
            BeastCompanionData = new BeastCompanionDataManager(ConnectionString, inputUsedBooks);
            CircleTerrainData = new CircleTerrainDataManager(ConnectionString, inputUsedBooks);
        }

        public void setUsedBooks(List<string> inputUsedBooks)
        {
            UsedBooks = inputUsedBooks;
            ExtraSubclassSkillData.UsedBooks = inputUsedBooks;
            TotemData.UsedBooks = inputUsedBooks;
            ExtraSubclassSpellData.UsedBooks = inputUsedBooks;
            ExtraToolProficiencyData.UsedBooks = inputUsedBooks;
            ManeuverData.UsedBooks = inputUsedBooks;
            ElementalDisciplineData.UsedBooks = inputUsedBooks;
            DraconicAncestryData.UsedBooks = inputUsedBooks;
            HunterData.UsedBooks = inputUsedBooks;
            BeastCompanionData.UsedBooks = inputUsedBooks;
            CircleTerrainData.UsedBooks = inputUsedBooks;
        }

        public bool hasExtraSubclassChoices(string subclass, int level)
        {
            return ExtraSubclassSkillData.hasSkillChoice(subclass, level) || TotemData.hasTotemFeatures(subclass, level) 
                || ExtraSubclassSpellData.hasExtraSpellChoice(subclass) || ExtraToolProficiencyData.hasToolProficiencyChoice(subclass, level)
                || ManeuverData.hasManeuvers(subclass, level) || DraconicAncestryData.hasDraconicAncestry(subclass)
                || ElementalDisciplineData.hasDisciplines(subclass, level) || HunterData.hasHunterFeatures(subclass, level)
                || BeastCompanionData.hasCompanion(subclass, level) || CircleTerrainData.hasCircleTerrain(subclass, level);
        }
    }
}
