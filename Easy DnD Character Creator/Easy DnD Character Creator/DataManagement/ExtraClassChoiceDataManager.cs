using Easy_DnD_Character_Creator.DataManagement.ExtraClassManagers;
using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public class ExtraClassChoiceDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks;

        public FightingStyleDataManager FightingStyleData { get; }
        public FavoredEnemyTerrainDataManager FavoredEnemyTerrainData { get; }
        public ExtraClassSkillDataManager ExtraClassSkillData { get; }
        public WarlockChoiceDataManager WarlockChoiceData { get; }

        public ExtraClassChoiceDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;

            FightingStyleData = new FightingStyleDataManager(ConnectionString, inputUsedBooks);
            FavoredEnemyTerrainData = new FavoredEnemyTerrainDataManager(ConnectionString, inputUsedBooks);
            ExtraClassSkillData = new ExtraClassSkillDataManager(ConnectionString, inputUsedBooks);
            WarlockChoiceData = new WarlockChoiceDataManager(ConnectionString, inputUsedBooks);
        }

        public void setUsedBooks(List<string> inputUsedBooks)
        {
            UsedBooks = inputUsedBooks;
            FightingStyleData.UsedBooks = inputUsedBooks;
            FavoredEnemyTerrainData.UsedBooks = inputUsedBooks;
            ExtraClassSkillData.UsedBooks = inputUsedBooks;
            WarlockChoiceData.UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// checks, if a given class has any additional choices to make
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool hasExtraClassChoices(string className, int level)
        {
            return FightingStyleData.hasFightingStyle(className, level) || FavoredEnemyTerrainData.hasFavoredEnemy(className, level)
                || FavoredEnemyTerrainData.hasFavoredTerrain(className, level) || ExtraClassSkillData.hasSkillChoice(className, level)
                || WarlockChoiceData.hasWarlockChoices(className, level);
        }

    }
}
