using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using Easy_DnD_Character_Creator.DataManagement;
using Easy_DnD_Character_Creator.DataTypes;

namespace Easy_DnD_Character_Creator
{
    public class DataManager
    {
        private SQLiteConnection DBConnection { get; }
        private string ConnectionString { get; }
        //public string UsedBooks { get; set; }
        public List<string> UsedBooks { get; private set; }

        public RaceDataManager RaceData { get; }
        public AppearanceDataManager AppearanceData { get; }
        public ClassDataManager ClassData { get; }
        public BackgroundDataManager BackgroundData { get; }
        public AbilityDataManager AbilityData { get; }
        public LanguageDataManager LanguageData { get; }
        public SkillDataManager SkillData { get; }
        public EquipmentDataManager EquipmentData { get; }
        public SpellDataManager SpellData { get; }

        public DataManager()
        {
            ConnectionString = "Data Source=";
            ConnectionString += Directory.GetCurrentDirectory();
            ConnectionString += "\\data.sqlite;Version=3;Read Only=True;";

            DBConnection = new SQLiteConnection(ConnectionString);

            //UsedBooks = "\"Player's Handbook\"";
            UsedBooks = new List<string>();
            UsedBooks.Add("Player's Handbook");

            RaceData = new RaceDataManager(ConnectionString, UsedBooks);
            AppearanceData = new AppearanceDataManager(ConnectionString, UsedBooks);
            ClassData = new ClassDataManager(ConnectionString, UsedBooks);
            BackgroundData = new BackgroundDataManager(ConnectionString, UsedBooks);
            AbilityData = new AbilityDataManager(ConnectionString, UsedBooks);
            LanguageData = new LanguageDataManager(ConnectionString, UsedBooks);
            SkillData = new SkillDataManager(ConnectionString, UsedBooks);
            EquipmentData = new EquipmentDataManager(ConnectionString, UsedBooks);
            SpellData = new SpellDataManager(ConnectionString, UsedBooks);
        }

        public void setUsedBooks(List<string> inputUsedBooks)
        {
            UsedBooks = inputUsedBooks;
            RaceData.UsedBooks = inputUsedBooks;
            AppearanceData.UsedBooks = inputUsedBooks;
            ClassData.UsedBooks = inputUsedBooks;
            BackgroundData.UsedBooks = inputUsedBooks;
            AbilityData.UsedBooks = inputUsedBooks;
            LanguageData.UsedBooks = inputUsedBooks;
            SkillData.UsedBooks = inputUsedBooks;
            EquipmentData.UsedBooks = inputUsedBooks;
            SpellData.UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// returns a list of all active books
        /// </summary>
        public List<Book> getActiveBooks()
        {
            List<Book> activeBooks = new List<Book>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT * FROM books WHERE active = 1";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                activeBooks.Add(new Book(dbReader.GetInt32(0), dbReader.GetString(1), dbReader.GetString(2), dbReader.GetInt32(3), dbReader.GetInt32(4)));
            }
            DBConnection.Close();

            return activeBooks;
        }

        
        /// <summary>
        /// checks, if the chosen class may choose a fighting style at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool hasFightingStyle(string className, int level)
        {
            bool hasFightingStyle = false;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT * FROM fightingStyleGain " +
                                  "INNER JOIN classes ON fightingStyleGain.classId=classes.classid " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\" AND level BETWEEN 1 AND ";
            dbQuery.CommandText += level.ToString();

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    hasFightingStyle = true;
                }
            }

            DBConnection.Close();

            return hasFightingStyle;
        }

        /// <summary>
        /// gets a list of fighting styles the class can choose from at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public List<string> getFightingStyles(string className, int level)
        {
            List<string> fightingStyles = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT fightingStyles.name from fightingStyleGain " +
                                  "INNER JOIN classes ON fightingStyleGain.classId=classes.classid " +
                                  "INNER JOIN fightingStyles ON fightingStyleGain.styleId=fightingStyles.styleId " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\" AND level BETWEEN 1 AND ";
            dbQuery.CommandText += level.ToString();

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    fightingStyles.Add(dbReader.GetString(0));
                }
            }

            DBConnection.Close();

            return fightingStyles;
        }

        /// <summary>
        /// gets the description of the given fighting style
        /// </summary>
        /// <param name="fightingStyle">name of fighting style</param>
        public string getFightingStyleDescription(string fightingStyle)
        {
            string description = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT description FROM fightingStyles " +
                                  "WHERE name=\"";
            dbQuery.CommandText += fightingStyle;
            dbQuery.CommandText = "\"";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    description = dbReader.GetString(0);
                }
            }

            DBConnection.Close();

            return description;
        }

        /// <summary>
        /// checks, if a given class may choose a favored enemy at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool hasFavoredEnemy(string className, int level)
        {
            bool hasFavoredEnemy = false;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT amount FROM favoredEnemyAmount " +
                                  "INNER JOIN classes ON classes.classid=favoredEnemyAmount.classId " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\" AND favoredEnemyAmount.level BETWEEN 1 AND ";
            dbQuery.CommandText += level.ToString();
            dbQuery.CommandText += "ORDER BY amount DESC LIMIT 1";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    hasFavoredEnemy = true;
                }
            }

            DBConnection.Close();

            return hasFavoredEnemy;
        }

        /// <summary>
        /// gets the amount of favored enemies a class may choose at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public int getFavoredEnemyAmount(string className, int level)
        {
            int favoredEnemyAmount = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT amount FROM favoredEnemyAmount " +
                                  "INNER JOIN classes ON classes.classid=favoredEnemyAmount.classId " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\" AND favoredEnemyAmount.level BETWEEN 1 AND ";
            dbQuery.CommandText += level.ToString();
            dbQuery.CommandText += "ORDER BY amount DESC LIMIT 1";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    favoredEnemyAmount = dbReader.GetInt32(0);
                }
            }

            DBConnection.Close();

            return favoredEnemyAmount;
        }

        /// <summary>
        /// checks, if a given class may choose a favored terrain at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool hasFavoredTerrain(string className, int level)
        {
            bool hasFavoredTerrain = false;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT amount FROM favoredTerrainAmount " +
                                  "INNER JOIN classes ON classes.classid=favoredTerrainAmount.classId " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\" AND favoredTerrainAmount.level BETWEEN 1 AND ";
            dbQuery.CommandText += level.ToString();
            dbQuery.CommandText += "ORDER BY amount DESC LIMIT 1";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    hasFavoredTerrain = true;
                }
            }

            DBConnection.Close();

            return hasFavoredTerrain;
        }
        
        /// <summary>
        /// gets the amount of favored terrains a class may choose at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public int getFavoredTerrainAmount(string className, int level)
        {
            int favoredTerrainAmount = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT amount FROM favoredTerrainAmount " +
                                  "INNER JOIN classes ON classes.classid=favoredTerrainAmount.classId " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\" AND favoredTerrainAmount.level BETWEEN 1 AND ";
            dbQuery.CommandText += level.ToString();
            dbQuery.CommandText += "ORDER BY amount DESC LIMIT 1";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    favoredTerrainAmount = dbReader.GetInt32(0);
                }
            }

            DBConnection.Close();

            return favoredTerrainAmount;
        }

        /// <summary>
        /// gets a list of all possible favored enemy types
        /// </summary>
        public List<string> getFavoredEnemyTypes()
        {
            List<string> favoredEnemies = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT name FROM favoredEnemyTypes";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    favoredEnemies.Add(dbReader.GetString(0));
                }
            }

            DBConnection.Close();

            return favoredEnemies;
        }

        /// <summary>
        /// gets a list of all possible favored terrain types
        /// </summary>
        /// <returns></returns>
        public List<string> getFavoredTerrainTypes()
        {
            List<string> favoredTerrain = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT name FROM favoredTerrainTypes";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    favoredTerrain.Add(dbReader.GetString(0));
                }
            }

            DBConnection.Close();

            return favoredTerrain;
        }

        /// <summary>
        /// gets a list of all skills the given cleric subclass may choose from
        /// </summary>
        /// <param name="subclass">given subclass</param>
        public List<string> getClericSubclassSkills(string subclass)
        {
            List<string> clericSubclassSkills = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT skills.name FROM extraClericProficiencies " +
                                  "INNER JOIN subclasses ON extraClericProficiencies.subclassId=subclasses.subclassId " +
                                  "INNER JOIN skills ON extraClericProficiencies.skillId=skills.skillId " +
                                  "WHERE subclasses.name=\"";
            dbQuery.CommandText += subclass;
            dbQuery.CommandText = "\"";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    clericSubclassSkills.Add(dbReader.GetString(0));
                }
            }

            DBConnection.Close();

            return clericSubclassSkills;
        }

        /// <summary>
        /// gets a list of all possible dragon bloodlines
        /// </summary>
        public List<string> getDragonBloodlines()
        {
            List<string> bloodlines = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT dragon FROM dragonBloodline";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    bloodlines.Add(dbReader.GetString(0));
                }
            }

            DBConnection.Close();

            return bloodlines;
        }

        /// <summary>
        /// gets the damage type associated with the given dragon type
        /// </summary>
        /// <param name="dragon">color of dragon to get damage type for</param>
        public string getDragonDamageType(string dragon)
        {
            string damageType = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT damageType FROM dragonBloodline " +
                                  "WHERE dragon=\"";
            dbQuery.CommandText += dragon;
            dbQuery.CommandText = "\"";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    damageType = dbReader.GetString(0);
                }
            }

            DBConnection.Close();

            return damageType;
        }

        /// <summary>
        /// checks, if a given subrace may choose additional cantrips
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        public bool hasExtraRaceCantripChoice(string subrace)
        {
            bool hasExtraCantripChoice = false;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT * FROM extraRaceCantrips " +
                                  "INNER JOIN races ON races.raceid = extraRaceCantrips.raceId " +
                                  "WHERE races.subrace=\"";
            dbQuery.CommandText += subrace;
            dbQuery.CommandText += "\"";
            
            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    hasExtraCantripChoice = true;
                }
            }

            DBConnection.Close();

            return hasExtraCantripChoice;
        }

        /// <summary>
        /// gets a list of the additional cantrips the given subrace may choose from
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        public List<string> getExtraRaceCantripChoiceOptions(string subrace)
        {
            List<string> cantripList = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT name FROM spells " +
                                  "INNER JOIN books ON spells.book = books.bookid " +
                                  "WHERE spells.level = 0 " +
                                  "AND spells.classes LIKE \"%\" || " +
                                  "(SELECT classes.name FROM extraRaceCantrips " +
                                  "INNER JOIN races ON races.raceid = extraRaceCantrips.raceId " +
                                  "INNER JOIN classes ON classes.classid = extraRaceCantrips.classId " +
                                  "WHERE races.subrace = \"";
            dbQuery.CommandText += subrace;
            dbQuery.CommandText += "\") || \"%\" AND books.title IN(";
            dbQuery.CommandText += UsedBooks;
            dbQuery.CommandText += ")";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                cantripList.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return cantripList;
        }

        public string getExtraRaceChoiceIntroText(string subrace)
        {
            if (subrace == "High Elf")
            {
                return "As a High Elf, please choose a cantrip you know:";
            }
            
            return "";
        }
        
        
        
        /// <summary>
        /// checks, if a given subrace has any additional choices to make
        /// </summary>
        /// <param name="subrace">given subrace</param>
        public bool hasExtraRaceChoices(string subrace)
        {
            //currently the only possible race choice is an extra cantrip for High Elves
            return hasExtraRaceCantripChoice(subrace);
        }

        public bool hasExtraClassChoices(string className, int level)
        {
            return hasFavoredEnemy(className, level) || hasFavoredTerrain(className, level);
        }

        public bool hasExtraSubclassChoices(string subclass, int level)
        {
            return true;
        }

        public bool hasExtraChoices(string subrace, string className, string subclass, int level)
        {
            return hasExtraRaceChoices(subrace) || hasExtraClassChoices(className, level) || hasExtraSubclassChoices(subclass, level);
        }

        //private void ReadData()
        //{
        //    DBConnection.Open();
        //    SQLiteDataReader sqlite_datareader;
        //    SQLiteCommand sqlite_cmd;
        //    sqlite_cmd = DBConnection.CreateCommand();
        //    sqlite_cmd.CommandText = "SELECT title FROM books WHERE active = 1";

        //    sqlite_datareader = sqlite_cmd.ExecuteReader();
        //    while (sqlite_datareader.Read())
        //    {
        //        string myreader = sqlite_datareader.GetString(0);
        //        Console.WriteLine(myreader);
        //    }
        //    DBConnection.Close();
        //}
    }
}