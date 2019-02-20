using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Easy_DnD_Character_Creator
{
    public class DataManager
    {
        private SQLiteConnection DBConnection { get; }
        public string UsedBooks { get; set; }

        public DataManager()
        {
            string connectionString = "Data Source=";
            connectionString += Directory.GetCurrentDirectory();
            connectionString += "\\data.sqlite;Version=3;Read Only=True;";

            DBConnection = new SQLiteConnection(connectionString);

            UsedBooks = "\"Player's Handbook\"";
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
        /// gets a list of all playable races
        /// </summary>
        public List<string> getRaces()
        {
            List<string> raceList = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT race FROM races INNER JOIN books ON races.book=books.bookid " +
                                  "WHERE books.title IN (";
            dbQuery.CommandText += UsedBooks;
            dbQuery.CommandText += ") GROUP BY race";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                raceList.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return raceList;
        }

        /// <summary>
        /// gets a list of all subrace options for the chosen race
        /// </summary>
        /// <param name="raceChoice">race for which to get subraces</param>
        public List<string> getSubraces(string raceChoice)
        {
            List<string> subraceList = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT subrace FROM races INNER JOIN books ON races.book=books.bookid " +
                                  "WHERE books.title IN (";
            dbQuery.CommandText += UsedBooks;
            dbQuery.CommandText += ") AND race=\"";
            dbQuery.CommandText += raceChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                subraceList.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            if (subraceList.Count == 0)
            {
                subraceList.Add("---");
            }

            return subraceList;
        }

        /// <summary>
        /// gets the description for the chosen subrace
        /// </summary>
        /// <param name="subraceChoice">chosen subrace</param>
        public string  getSubraceDescription(string subraceChoice)
        {
            string description = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT description FROM races INNER JOIN books ON races.book=books.bookid " +
                                  "WHERE books.title IN (";
            dbQuery.CommandText += UsedBooks;
            dbQuery.CommandText += ") AND subrace=\"";
            dbQuery.CommandText += subraceChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                description = dbReader.GetString(0);
            }
            DBConnection.Close();

            return description;
        }

        /// <summary>
        /// checks, if the chosen race has an extra choice attached to it
        /// </summary>
        /// <param name="subraceChoice">the race to be checked</param>
        public bool subraceHasExtraChoice(string subraceChoice)
        {
            bool hasChoice = false;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT extraToolProficiencies FROM races WHERE subrace=\"";
            dbQuery.CommandText += subraceChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                hasChoice = dbReader.GetBoolean(0);
            }
            DBConnection.Close();

            return hasChoice;
        }

        /// <summary>
        /// gets a list of proficiencies for the chosen subrace
        /// </summary>
        /// <param name="subraceChoice">chosen subrace</param>
        public List<string> getExtraRaceProficiencies(string subraceChoice)
        {
            List<string> proficiencyList = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT tools.name FROM tools " +
                                  "INNER JOIN extraRaceToolProficiencyChoices ON extraRaceToolProficiencyChoices.name=tools.name " +
                                  "INNER JOIN races ON extraRaceToolProficiencyChoices.raceId=races.raceid " +
                                  "WHERE races.subrace=\"";
            dbQuery.CommandText += subraceChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                proficiencyList.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            if (proficiencyList.Count == 0)
            {
                proficiencyList.Add("---");
            }

            return proficiencyList;
        }

        /// <summary>
        /// gets the description for general alignment of chosen subrace
        /// </summary>
        /// <param name="subraceChoice">chosen subrace</param>
        public string getSubraceAlignmentDescription(string subraceChoice)
        {
            string description = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT alignmentDescription FROM races " +
                                  "WHERE subrace=\"";
            dbQuery.CommandText += subraceChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                description = dbReader.GetString(0);
            }
            DBConnection.Close();

            return description;
        }

        /// <summary>
        /// gets description for the chosen alignment
        /// </summary>
        /// <param name="law">lawful, neutral or chaotic</param>
        /// <param name="morality">good, neutral or evil</param>
        public string getAlignmentDescription(string law, string morality)
        {
            string description = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT description FROM alignment " +
                                  "WHERE law=\"";
            dbQuery.CommandText += law;
            dbQuery.CommandText += "\" AND morality=\"";
            dbQuery.CommandText += morality;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                description = dbReader.GetString(0);
            }
            DBConnection.Close();

            return description;
        }

        /// <summary>
        /// gets the general description about alignment
        /// </summary>
        public string getGeneralAlignmentDescription()
        {
            return getAlignmentDescription("-", "-");
        }

        /// <summary>
        /// gets a list of all alignments towards law
        /// </summary>
        /// <returns></returns>
        public List<string> getLawAlignments()
        {
            List<string> lawList = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT law FROM alignment WHERE law!=\"-\" GROUP BY law";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                lawList.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            if (lawList.Count == 0)
            {
                lawList.Add("---");
            }

            return lawList;
        }

        /// <summary>
        /// gets a list of all alignments towards morality
        /// </summary>
        /// <returns></returns>
        public List<string> getMoralityAlignments()
        {
            List<string> moralityList = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT morality FROM alignment WHERE morality!=\"-\" GROUP BY morality";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                moralityList.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            if (moralityList.Count == 0)
            {
                moralityList.Add("---");
            }

            return moralityList;
        }

        /// <summary>
        /// gets the description of race specific ages
        /// </summary>
        /// <param name="subraceChoice">chosen subrace</param>
        public string getAgeDescription(string subraceChoice)
        {
            string description = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT ageDescription FROM races " +
                                  "WHERE subrace=\"";
            dbQuery.CommandText += subraceChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                description = dbReader.GetString(0);
            }
            DBConnection.Close();

            return description;
        }

        /// <summary>
        /// gets number of dice used for height modification
        /// </summary>
        /// <param name="subraceChoice">chosen subrace</param>
        public int getHeightModifierDiceCount(string subraceChoice)
        {
            int heightModifierDiceCount = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT heightModDiceCount FROM size " +
                                  "INNER JOIN races ON size.raceId=races.raceid " +
                                  "WHERE races.subrace=\"";
            dbQuery.CommandText += subraceChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                heightModifierDiceCount = dbReader.GetInt32(0);
            }
            DBConnection.Close();

            return heightModifierDiceCount;
        }

        /// <summary>
        /// gets type of dice used for height modification
        /// </summary>
        /// <param name="subraceChoice">chosen subrace</param>
        public int getHeightModifierDiceType(string subraceChoice)
        {
            int heightModifierDiceType = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT heightModDiceType FROM size " +
                                  "INNER JOIN races ON size.raceId=races.raceid " +
                                  "WHERE races.subrace=\"";
            dbQuery.CommandText += subraceChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                heightModifierDiceType = dbReader.GetInt32(0);
            }
            DBConnection.Close();

            return heightModifierDiceType;
        }

        /// <summary>
        /// gets number of dice used for weight modification
        /// </summary>
        /// <param name="subraceChoice">chosen subrace</param>
        public int getWeightModifierDiceCount(string subraceChoice)
        {
            int weightModifierDiceCount = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT weightModDiceCount FROM size " +
                                  "INNER JOIN races ON size.raceId=races.raceid " +
                                  "WHERE races.subrace=\"";
            dbQuery.CommandText += subraceChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                weightModifierDiceCount = dbReader.GetInt32(0);
            }
            DBConnection.Close();

            return weightModifierDiceCount;
        }

        /// <summary>
        /// gets type of dice used for weight modification
        /// </summary>
        /// <param name="subraceChoice">chosen subrace</param>
        public int getWeightModifierDiceType(string subraceChoice)
        {
            int weightModifierDiceType = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT weightModDiceType FROM size " +
                                  "INNER JOIN races ON size.raceId=races.raceid " +
                                  "WHERE races.subrace=\"";
            dbQuery.CommandText += subraceChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                weightModifierDiceType = dbReader.GetInt32(0);
            }
            DBConnection.Close();

            return weightModifierDiceType;
        }

        /// <summary>
        /// gets the base height in metric or imperial for the chosen race
        /// </summary>
        /// <param name="metric">true for metric, false for imperial</param>
        /// <param name="subraceChoice">chosen subrace</param>
        public int getBaseHeight(bool metric, string subraceChoice)
        {
            int baseHeight = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT ";
            if (metric)
            {
                dbQuery.CommandText += "baseHeightCm";
            }
            else
            {
                dbQuery.CommandText += "baseHeightIn";
            }
            dbQuery.CommandText += " FROM size " +
                "INNER JOIN races ON size.raceId=races.raceid " +
                "WHERE races.subrace=\"";
            dbQuery.CommandText += subraceChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                baseHeight = dbReader.GetInt32(0);
            }
            DBConnection.Close();

            return baseHeight;
        }

        /// <summary>
        /// gets the base Weight in metric or imperial for the chosen race
        /// </summary>
        /// <param name="metric">true for metric, false for imperial</param>
        /// <param name="subraceChoice">chosen subrace</param>
        public int getBaseWeight(bool metric, string subraceChoice)
        {
            int baseWeight = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT ";
            if (metric)
            {
                dbQuery.CommandText += "baseWeightKg";
            }
            else
            {
                dbQuery.CommandText += "baseWeightLb";
            }
            dbQuery.CommandText += " FROM size " +
                "INNER JOIN races ON size.raceId=races.raceid " +
                "WHERE races.subrace=\"";
            dbQuery.CommandText += subraceChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                baseWeight = dbReader.GetInt32(0);
            }
            DBConnection.Close();

            return baseWeight;
        }

        /// <summary>
        /// gets a list of all playable classes
        /// </summary>
        public List<string> getClasses()
        {
            List<string> classList = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT name FROM classes INNER JOIN books ON classes.book=books.bookid " +
                                  "WHERE books.title IN (";
            dbQuery.CommandText += UsedBooks;
            dbQuery.CommandText += ")";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                classList.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return classList;
        }

        /// <summary>
        /// gets a list of all subclass options for the chosen class
        /// </summary>
        /// <param name="classChoice">class for which to get subclasses</param>
        /// <param name="level">player level</param>
        public List<string> getSubclasses(string classChoice, int level)
        {
            List<string> subclassList = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT subclasses.name FROM subclasses " + 
                                  "INNER JOIN books ON subclasses.book=books.bookid " + 
                                  "INNER JOIN classes ON subclasses.parentclass=classes.classid " + 
                                  "WHERE  books.title IN (";
            dbQuery.CommandText += UsedBooks;
            dbQuery.CommandText += ") AND classes.name=\"";
            dbQuery.CommandText += classChoice;
            dbQuery.CommandText += "\" AND classes.subclasslevel<=";
            dbQuery.CommandText += level.ToString();

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                subclassList.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            if (subclassList.Count == 0)
            {
                subclassList.Add("---");
            }

            return subclassList;
        }

        /// <summary>
        /// gets the description for the chosen class
        /// </summary>
        /// <param name="classChoice">chosen class</param>
        /// <returns></returns>
        public string getClassDescription(string classChoice)
        {
            string description = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT description FROM classes INNER JOIN books ON classes.book=books.bookid WHERE books.title IN (";
            dbQuery.CommandText += UsedBooks;
            dbQuery.CommandText += ") AND name=\"";
            dbQuery.CommandText += classChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                description = dbReader.GetString(0);
            }
            DBConnection.Close();

            return description;
        }

        /// <summary>
        /// gets the description for the chosen subclass
        /// </summary>
        /// <param name="subclassChoice">chosen subclass</param>
        /// <returns></returns>
        public string getSubclassDescription(string subclassChoice)
        {
            string description = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT description FROM subclasses INNER JOIN books ON subclasses.book=books.bookid WHERE books.title IN (";
            dbQuery.CommandText += UsedBooks;
            dbQuery.CommandText += ") AND name=\"";
            dbQuery.CommandText += subclassChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                description = dbReader.GetString(0);
            }
            DBConnection.Close();

            return description;
        }

        /// <summary>
        /// checks, if the chosen class has extra tool proficiencies
        /// </summary>
        /// <param name="classChoice">chosen class</param>
        public bool classHasExtraChoice(string classChoice)
        {
            bool hasChoice = false;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT extraToolProficiencies FROM classes WHERE name=\"";
            dbQuery.CommandText += classChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                hasChoice = dbReader.GetBoolean(0);
            }
            DBConnection.Close();

            return hasChoice;
        }

        /// <summary>
        /// gets a list of the extra proficiencies the class can choose from
        /// </summary>
        /// <param name="classChoice">chosen class</param>
        public List<string> getExtraClassProficiencies(string classChoice)
        {
            List<string> proficiencyList = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT tools.name FROM tools " +
                                  "INNER JOIN extraClassToolProficiencyChoices ON extraClassToolProficiencyChoices.type=tools.type " +
                                  "INNER JOIN classes ON extraClassToolProficiencyChoices.classId=classes.classid " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += classChoice;
            dbQuery.CommandText += "\" ORDER BY tools.name";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                proficiencyList.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            if (proficiencyList.Count == 0)
            {
                proficiencyList.Add("---");
            }

            return proficiencyList;
        }

        /// <summary>
        /// gets the amount of proficiencies the class is allowed to choose
        /// </summary>
        /// <param name="classChoice">chosen class</param>
        public int getExtraClassProficiencyAmount(string classChoice)
        {
            int amount = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT amount FROM extraClassToolProficiencyAmount " +
                                  "INNER JOIN classes ON extraClassToolProficiencyAmount.classId=classes.classid " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += classChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                amount = dbReader.GetInt32(0);
            }
            DBConnection.Close();

            return amount;
        }

        /// <summary>
        /// gets all available backgrounds
        /// </summary>
        public List<string> getBackgrounds()
        {
            List<string> backgroundList = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT name FROM backgrounds INNER JOIN books ON backgrounds.book=books.bookid WHERE books.title IN (";
            dbQuery.CommandText += UsedBooks;
            dbQuery.CommandText += ")";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                backgroundList.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return backgroundList;
        }

        /// <summary>
        /// gets the description of the chosen background
        /// </summary>
        /// <param name="backgroundChoice">chosen background</param>
        public string getBackgroundDescription(string backgroundChoice)
        {
            string description = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT description FROM backgrounds INNER JOIN books ON backgrounds.book=books.bookid WHERE books.title IN (";
            dbQuery.CommandText += UsedBooks;
            dbQuery.CommandText += ") AND name=\"";
            dbQuery.CommandText += backgroundChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                description = dbReader.GetString(0);
            }
            DBConnection.Close();

            return description;
        }

        /// <summary>
        /// checks, if the chosen background has extra tool proficiency choices
        /// </summary>
        /// <param name="backgroundChoice">chosen background</param>
        public bool backgroundHasExtraChoice(string backgroundChoice)
        {
            bool hasChoice = false;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT extraToolProficiencies FROM backgrounds WHERE name=\"";
            dbQuery.CommandText += backgroundChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                hasChoice = dbReader.GetBoolean(0);
            }
            DBConnection.Close();

            return hasChoice;
        }

        /// <summary>
        /// gets a list of proficiency choices for the chosen background
        /// </summary>
        /// <param name="backgroundChoice">chosen background</param>
        public List<string> getExtraBackgroundProficiencies(string backgroundChoice)
        {
            List<string> proficiencyList = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT tools.name FROM tools " +
                                  "INNER JOIN extraBackgroundToolProficiencyChoices ON extraBackgroundToolProficiencyChoices.type=tools.type " +
                                  "INNER JOIN backgrounds ON extraBackgroundToolProficiencyChoices.backgroundId=backgrounds.backgroundId " +
                                  "WHERE backgrounds.name=\"";
            dbQuery.CommandText += backgroundChoice;
            dbQuery.CommandText += "\" ORDER BY tools.name";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                proficiencyList.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            if (proficiencyList.Count == 0)
            {
                proficiencyList.Add("---");
            }

            return proficiencyList;
        }

        /// <summary>
        /// checks, if the chosen background has an extra feature choice attached to it
        /// </summary>
        /// <param name="backgroundChoice">the background to be checked</param>
        public bool backgroundHasExtraFeatureChoice(string backgroundChoice)
        {
            bool hasChoice = false;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT extraFeatureChoice FROM backgrounds WHERE name=\"";
            dbQuery.CommandText += backgroundChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                hasChoice = dbReader.GetBoolean(0);
            }
            DBConnection.Close();

            return hasChoice;
        }

        /// <summary>
        /// gets the title of the extra background feature choice to make
        /// </summary>
        /// <param name="backgroundChoice">chosen background</param>
        public string getBackgroundFeatureChoiceTitle(string backgroundChoice)
        {
            string title = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT title FROM extraBackgroundChoices " +
                                  "INNER JOIN backgrounds ON extraBackgroundChoices.backgroundId=backgrounds.backgroundId " +
                                  "WHERE backgrounds.name=\"";
            dbQuery.CommandText += backgroundChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                title = dbReader.GetString(0);
            }
            DBConnection.Close();

            return title;
        }

        /// <summary>
        /// gets a description of the extra background feature choice
        /// </summary>
        /// <param name="backgroundChoice">chosen background</param>
        public string getBackgroundFeatureChoiceDescription(string backgroundChoice)
        {
            string description = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT extraBackgroundChoices.description FROM extraBackgroundChoices " +
                                  "INNER JOIN backgrounds ON extraBackgroundChoices.backgroundId=backgrounds.backgroundId " +
                                  "WHERE backgrounds.name=\"";
            dbQuery.CommandText += backgroundChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                description = dbReader.GetString(0);
            }
            DBConnection.Close();

            return description;
        }

        /// <summary>
        /// gets a list of the options for the extra background feature choice
        /// </summary>
        /// <param name="backgroundChoice">chosen background</param>
        public List<string> getBackgroundFeatureChoiceOptions(string backgroundChoice)
        {
            List<string> backgroundChoicesList = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT text FROM extraBackgroundChoiceOptions " +
                                  "INNER JOIN extraBackgroundChoices ON extraBackgroundChoiceOptions.extraChoiceId=extraBackgroundChoices.extraChoiceId " +
                                  "INNER JOIN backgrounds ON extraBackgroundChoices.backgroundId=backgrounds.backgroundId " +
                                  "WHERE backgrounds.name=\"";
            dbQuery.CommandText += backgroundChoice;
            dbQuery.CommandText += "\" ORDER BY extraBackgroundChoiceOptions.diceValue ASC";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                backgroundChoicesList.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return backgroundChoicesList;
        }

        /// <summary>
        /// gets the recommended abilities for the chosen class
        /// </summary>
        /// <param name="classChoice">chosen class</param>
        public string getAbilityRecommendation(string classChoice)
        {
            string recommendation = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT recommendation FROM preferredAbilities " +
                                  "INNER JOIN classes ON preferredAbilities.classid=classes.classid " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += classChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                recommendation = dbReader.GetString(0);
            }
            DBConnection.Close();

            return recommendation;
        }

        /// <summary>
        /// gets the description of an ability
        /// </summary>
        /// <param name="abilityName">specified ability</param>
        public string getAbilityDescription(string abilityName)
        {
            string description = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT description FROM abilities " +
                                  "WHERE UPPER(name)=\"";
            dbQuery.CommandText += abilityName;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                description = dbReader.GetString(0);
            }
            DBConnection.Close();

            return description;
        }

        /// <summary>
        /// gets the value of the ability score bonus received from the chosen subrace
        /// </summary>
        /// <param name="subraceChoice">chosen subrace</param>
        /// <param name="abilityName">ability name</param>
        /// <returns></returns>
        public int getAbilityScoreBonus(string subraceChoice, string abilityName)
        {
            int bonus = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT ";
            dbQuery.CommandText += abilityName.ToLower();
            dbQuery.CommandText += " FROM raceAbilityScoreAdditions " +
                                   "INNER JOIN races ON raceAbilityScoreAdditions.raceid=races.raceid " +
                                   "WHERE races.subrace=\"";
            dbQuery.CommandText += subraceChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                bonus = dbReader.GetInt32(0);
            }
            DBConnection.Close();

            return bonus;
        }

        /// <summary>
        /// checks, if the character can choose ability increases because of their subrace
        /// </summary>
        /// <param name="subraceChoice">chosen subrace</param>
        public bool subraceHasAbilityChoice(string subraceChoice)
        {
            return (subraceChoice == "Half-Elf");
        }

        /// <summary>
        /// checks, how many abilities can be increased because of the subrace
        /// </summary>
        /// <param name="subraceChoice">chosen subrace</param>
        public int subraceAbilityChoiceAmount(string subraceChoice)
        {
            if (subraceChoice=="Half-Elf")
            {
                return 2;
            }

            return 0;
        }

        /// <summary>
        /// gets a list of the average ability scores
        /// </summary>
        public List<string> getAverageAbilityScores()
        {
            List<string> averageScores = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT * FROM averageAbilityScores";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                averageScores.Add(dbReader.GetInt32(0).ToString());
            }
            DBConnection.Close();

            return averageScores;
        }

        /// <summary>
        /// checks, if the subrace gains +1 hp per level
        /// </summary>
        /// <param name="subraceChoice">chosen subrace</param>
        public bool subraceHasBonusHP(string subraceChoice)
        {
            return (subraceChoice == "Hill Dwarf");
        }

        /// <summary>
        /// checks, if the subclass gains +1 hp per level
        /// </summary>
        /// <param name="subclassChoice">chosen subclass</param>
        public bool subclassHasBonusHP(string subclassChoice)
        {
            return (subclassChoice == "Draconic Bloodline");
        }

        /// <summary>
        /// gets the maximum result of the hit die of the chosen class
        /// </summary>
        /// <param name="classChoice">chosen class</param>
        public int getMaximumHitDieResult(string classChoice)
        {
            int result = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT maximumResult FROM hitDice " +
                                  "INNER JOIN classes ON hitDice.classId=classes.classid " +
                                  "WHERE name=\"";
            dbQuery.CommandText += classChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                result = dbReader.GetInt32(0);
            }
            DBConnection.Close();

            return result;
        }

        /// <summary>
        /// gets the average result of the hit die of the chosen class
        /// </summary>
        /// <param name="classChoice">chosen class</param>
        public int getAverageHitDieResult(string classChoice)
        {
            int result = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT averageResult FROM hitDice INNER JOIN classes ON hitDice.classId=classes.classid WHERE name=\"";
            dbQuery.CommandText += classChoice;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                result = dbReader.GetInt32(0);
            }
            DBConnection.Close();

            return result;
        }

        /// <summary>
        /// gets a list of all available languages
        /// </summary>
        public List<string> getLanguages()
        {
            List<string> languages = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT name FROM languages";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                languages.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return languages;
        }

        /// <summary>
        /// gets a list of languages of the specified type
        /// </summary>
        /// <param name="type">language type (Standard, Exotic, Class)</param>
        public List<string> getLanguages(string type)
        {
            List<string> languages = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT name FROM languages " +
                                  "WHERE type=\"";
            dbQuery.CommandText += type;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                languages.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return languages;
        }

        /// <summary>
        /// gets a list of default languages known by the chosen race
        /// </summary>
        /// <param name="subraceName">chosen subrace</param>
        public List<string> getDefaultRaceLanguages(string subraceName)
        {
            List<string> languages = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT languages.name FROM languages " +
                                  "INNER JOIN languageRaceDefaults ON languageRaceDefaults.languageId = languages.id " +
                                  "INNER JOIN races ON languageRaceDefaults.raceId = races.raceid " +
                                  "WHERE races.subrace=\"";
            dbQuery.CommandText += subraceName;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                languages.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return languages;
        }

        /// <summary>
        /// gets a list of default languages known by the chosen class
        /// </summary>
        /// <param name="className">chosen class</param>
        public List<string> getDefaultClassLanguages(string className)
        {
            List<string> languages = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT languages.name FROM languages " +
                                  "INNER JOIN languageClassDefaults ON languageClassDefaults.languageId = languages.id " +
                                  "INNER JOIN classes ON languageClassDefaults.classId = classes.classid " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                languages.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return languages;
        }

        /// <summary>
        /// gets a list of default languages known by the chosen subclass
        /// </summary>
        /// <param name="subclassName">chosen subclass</param>
        public List<string> getDefaultSubclassLanguages(string subclassName)
        {
            List<string> languages = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT languages.name FROM languages " +
                                  "INNER JOIN languageSubclassDefaults ON languageSubclassDefaults.languageId = languages.id " +
                                  "INNER JOIN subclasses ON languageSubclassDefaults.subclassId = subclasses.subclassId " +
                                  "WHERE subclasses.name=\"";
            dbQuery.CommandText += subclassName;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                languages.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return languages;
        }

        /// <summary>
        /// gets information about the speakers of the chosen language
        /// </summary>
        /// <param name="languageName">chosen language</param>
        public string getLanguageSpeakers(string languageName)
        {
            string languageSpeakers = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT speakers FROM languages " +
                                  "WHERE name=\"";
            dbQuery.CommandText += languageName;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                languageSpeakers = dbReader.GetString(0);
            }
            DBConnection.Close();

            return languageSpeakers;
        }

        /// <summary>
        /// gets information about the script of the chosen language
        /// </summary>
        /// <param name="languageName">chosen language</param>
        public string getLanguageScript(string languageName)
        {
            string languageScript = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT script FROM languages " +
                                  "WHERE name=\"";
            dbQuery.CommandText += languageName;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                languageScript = dbReader.GetString(0);
            }
            DBConnection.Close();

            return languageScript;
        }

        /// <summary>
        /// gets the number of default languages gained from race, class and subclass
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        public int getDefaultLanguageCount(string subrace, string className, string subclass)
        {
            int count = getDefaultRaceLanguages(subrace).Count;
            count += getDefaultClassLanguages(className).Count;
            count += getDefaultSubclassLanguages(subclass).Count;
            return count;
        }

        /// <summary>
        /// gets the number of extra languages gained through race, class, subclass and background chocies
        /// </summary>
        /// <param name="race">chosen race</param>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="background">chosen background</param>
        public int getExtraLanguageCount(string race, string subclass, string background)
        {
            int languageCount = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            //extra race languages
            dbQuery.CommandText = "SELECT sum(extraLanguages) FROM (" +
                                  "SELECT extraLanguages FROM extraRaceLanguages " +
                                  "INNER JOIN races ON extraRaceLanguages.raceId = races.raceid " +
                                  "WHERE races.subrace=\"";
            dbQuery.CommandText += race;
            //extra subclass languages
            if (subclass!= "---")
            {
                dbQuery.CommandText += "\" " +
                                   "UNION ALL " +
                                   "SELECT extraLanguages FROM extraSubclassLanguages " +
                                   "INNER JOIN subclasses ON extraSubclassLanguages.subclassId = subclasses.subclassId " +
                                   "WHERE subclasses.name=\"";
                dbQuery.CommandText += subclass;
            }
            //extra background languages
            dbQuery.CommandText += "\" " +
                                   "UNION ALL " +
                                   "SELECT extraLanguages FROM extraBackgroundLanguages " +
                                   "INNER JOIN backgrounds ON extraBackgroundLanguages.backgroundId = backgrounds.backgroundId " +
                                   "WHERE backgrounds.name=\"";
            dbQuery.CommandText += background;
            dbQuery.CommandText += "\")";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    languageCount = dbReader.GetInt32(0);
                }
            }
            DBConnection.Close();

            return languageCount;
        }

        /// <summary>
        /// gets a list of all available skills
        /// </summary>
        public List<string> getSkills()
        {
            List<string> skills = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT name FROM skills";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                skills.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return skills;
        }

        /// <summary>
        /// gets the description of the chosen skill
        /// </summary>
        /// <param name="skill">name of the chosen skill</param>
        public string getSkillDescription(string skill)
        {
            string description = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT description FROM skills " +
                                  "WHERE name=\"";
            dbQuery.CommandText += skill;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                description = dbReader.GetString(0);
            }
            DBConnection.Close();

            return description;
        }
        
        /// <summary>
        /// gets a list of known skills from subrace and background choices
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="background">chosen background</param>
        public List<string> getKnownSkills(string subrace, string background)
        {
            List<string> knownSkills = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT skills.name FROM skills " +
                                  "INNER JOIN backgroundSkills ON backgroundSkills.skillId=skills.skillId " +
                                  "INNER JOIN backgrounds ON backgroundSkills.backgroundId=backgrounds.backgroundId " +
                                  "WHERE backgrounds.name=\"";
            dbQuery.CommandText += background;
            dbQuery.CommandText += "\" " +
                                   "UNION " +
                                   "SELECT skills.name FROM skills " +
                                   "INNER JOIN raceSkills ON raceSkills.skillId=skills.skillId " +
                                   "INNER JOIN races ON raceSkills.raceId=races.raceid " +
                                   "WHERE races.subrace=\"";
            dbQuery.CommandText += subrace;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                knownSkills.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return knownSkills;
        }

        /// <summary>
        /// gets the list of skills the chosen class can choose from
        /// </summary>
        /// <param name="className">chosen class</param>
        public List<string> getClassSkillOptions(string className)
        {
            List<string> skillOptions = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT skills.name FROM skills " +
                                  "INNER JOIN classSkillChoiceOptions ON classSkillChoiceOptions.skillId=skills.skillId " +
                                  "INNER JOIN classes ON classSkillChoiceOptions.classId=classes.classid " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                skillOptions.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return skillOptions;
        }

        /// <summary>
        /// gets the number of skills the class can select
        /// </summary>
        /// <param name="className">chosen class</param>
        public int getClassSkillCount(string className)
        {
            int skillCount = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT extraSkills FROM classes " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    skillCount = dbReader.GetInt32(0);
                }
            }
            DBConnection.Close();

            return skillCount;
        }

        /// <summary>
        /// checks, if the chosen subrace gets to choose additional skills
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        public bool hasExtraSkillChoice(string subrace)
        {
            return (subrace == "Half-Elf");
        }

        /// <summary>
        /// gets the number of extra skills the chosen subrace can choose
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        public int getExtraSkillChoiceAmount(string subrace)
        {
            if (subrace == "Half-Elf")
            {
                return 2;
            }

            return 0;
        }

        /// <summary>
        /// gets the number of equipment choices the specified class must make
        /// </summary>
        /// <param name="className">chosen class</param>
        public int getEquipmentChoiceAmount(string className)
        {
            int choiceAmount = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT choices FROM equipmentChoices " +
                                  "INNER JOIN classes ON classes.classid = equipmentChoices.classId " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    choiceAmount = dbReader.GetInt32(0);
                }
            }
            DBConnection.Close();

            return choiceAmount;
        }

        /// <summary>
        /// gets the number of items the specified class can choose for their 2nd choice
        /// </summary>
        /// <param name="className">chosen class</param>
        public int getEquipment2ndSelectionAmount(string className)
        {
            int selectionAmount = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT choice2Amount FROM equipmentChoices " +
                                  "INNER JOIN classes ON classes.classid = equipmentChoices.classId " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    selectionAmount = dbReader.GetInt32(0);
                }
            }
            DBConnection.Close();

            return selectionAmount;
        }

        /// <summary>
        /// gets a list of available choices for the specified parameters
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="choice">which set of options should be returned</param>
        /// <param name="strength">character's strength</param>
        public List<string> getEquipmentChoices(string subrace, string className, string subclass, int choice, int strength)
        {
            List<string> outputList = new List<string>();

            if (choice <= getEquipmentChoiceAmount(className))
            {
                //Barbarian
                if (className == "Barbarian")
                {
                    if (choice == 1)
                    {
                        outputList.AddRange(getWeaponList("martial", "melee").ToArray());
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getWeaponList("simple", "*").ToArray());
                        outputList[outputList.FindIndex(index => index.Equals("handaxe"))] = "handaxe (2)";
                    }
                }

                //Bard
                if (className == "Bard")
                {
                    if (choice == 1)
                    {
                        outputList.AddRange(getWeaponList("rapier").ToArray());
                        outputList.AddRange(getWeaponList("longsword").ToArray());
                        outputList.AddRange(getWeaponList("simple", "*").ToArray());
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getPackChoices(className));
                    }

                    if (choice == 3)
                    {
                        outputList.AddRange(getToolList("musical instrument").ToArray());
                    }
                }

                //Cleric
                if (className == "Cleric")
                {
                    if (choice == 1)
                    {
                        outputList.AddRange(getWeaponList("mace").ToArray());

                        if ((subrace == "Hill Dwarf") || (subrace == "Mountain Dwarf")
                            || (subclass == "Tempest Domain") || (subclass == "War Domain"))
                        {
                            outputList.AddRange(getWeaponList("warhammer").ToArray());
                        }
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getArmorList("scale mail armor", "*").ToArray());
                        outputList.AddRange(getArmorList("leather armor", "*").ToArray());

                        if (strength > getArmorStrengthRequirement("chain mail armor"))
                        {
                            if ((subclass == "Life Domain") || (subclass == "Nature Domain")
                             || (subclass == "Tempest Domain") || (subclass == "War Domain"))
                            {
                                outputList.AddRange(getArmorList("chain mail armor", "*").ToArray());
                            }
                        }
                    }

                    if (choice == 3)
                    {
                        outputList.AddRange(getWeaponList("light crossbow").ToArray());
                        outputList.AddRange(getWeaponList("simple", "*").ToArray());
                    }

                    if (choice == 4)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }

                //Druid
                if (className == "Druid")
                {
                    if (choice == 1)
                    {
                        outputList.AddRange(getWeaponList("simple", "*").ToArray());
                        outputList.AddRange(getArmorList("shield", "*").ToArray());
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getWeaponList("scimitar").ToArray());
                        outputList.AddRange(getWeaponList("simple", "melee").ToArray());
                    }
                }

                //Fighter
                if (className == "Fighter")
                {
                    if (choice == 1)
                    {
                        if (strength > getArmorStrengthRequirement("chain mail armor"))
                        {
                            outputList.AddRange(getArmorList("chain mail armor", "*").ToArray());
                        }

                        List<string> multiItemList = getArmorList("leather armor", "*");
                        multiItemList.AddRange(getWeaponList("longbow").ToArray());
                        outputList.Add(string.Join(", ", multiItemList.ToArray()));

                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getWeaponList("martial", "*").ToArray());
                        outputList.AddRange(getArmorList("shield", "*").ToArray());
                    }

                    if (choice == 3)
                    {
                        outputList.AddRange(getWeaponList("light crossbow").ToArray());
                        outputList.AddRange(getWeaponList("handaxe").ToArray());
                        outputList[outputList.FindIndex(index => index.Equals("handaxe"))] = "handaxe (2)";
                    }

                    if (choice == 4)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }

                //Monk
                if (className == "Monk")
                {
                    if (choice == 1)
                    {
                        outputList.AddRange(getWeaponList("shortsword").ToArray());
                        outputList.AddRange(getWeaponList("simple", "*").ToArray());
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }

                //Paladin
                if (className == "Paladin")
                {
                    if (choice == 1)
                    {
                        outputList.AddRange(getWeaponList("javelin").ToArray());
                        outputList[outputList.FindIndex(index => index.Equals("javelin"))] = "javelin (5)";
                        outputList.AddRange(getWeaponList("simple", "melee").ToArray());
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getWeaponList("martial", "*").ToArray());
                        outputList.AddRange(getArmorList("shield", "*").ToArray());
                    }

                    if (choice == 3)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }

                //Ranger
                if (className == "Ranger")
                {
                    if (choice == 1)
                    {
                        outputList.AddRange(getArmorList("scale mail armor", "*").ToArray());
                        outputList.AddRange(getArmorList("leather armor", "*").ToArray());
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getWeaponList("shortsword").ToArray());
                        outputList.AddRange(getWeaponList("simple", "melee").ToArray());
                    }

                    if (choice == 3)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }

                //Rogue
                if (className == "Rogue")
                {
                    if (choice == 1)
                    {
                        outputList.AddRange(getWeaponList("rapier").ToArray());
                        outputList.AddRange(getWeaponList("shortsword").ToArray());
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getWeaponList("shortbow").ToArray());
                        outputList.AddRange(getWeaponList("shortsword").ToArray());
                    }

                    if (choice == 3)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }

                //Sorcerer
                if (className == "Sorcerer")
                {
                    if (choice == 1)
                    {
                        outputList.AddRange(getWeaponList("light crossbow").ToArray());
                        outputList.AddRange(getWeaponList("simple", "*").ToArray());
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getToolList("arcane tool").ToArray());
                    }

                    if (choice == 3)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }

                //Warlock
                if (className == "Warlock")
                {
                    if (choice == 1)
                    {
                        outputList.AddRange(getWeaponList("light crossbow").ToArray());
                        outputList.AddRange(getWeaponList("simple", "*").ToArray());
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getToolList("arcane tool").ToArray());
                    }

                    if (choice == 3)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }

                //Wizard
                if (className == "Wizard")
                {
                    if (choice == 1)
                    {
                        outputList.AddRange(getWeaponList("quarterstaff").ToArray());
                        outputList.AddRange(getWeaponList("dagger").ToArray());
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getToolList("arcane tool").ToArray());
                    }

                    if (choice == 3)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }
            }
            return outputList;            
        }

        /// <summary>
        /// gets additional default equipment for the specified class
        /// </summary>
        /// <param name="className">chosen class</param>
        public string getExtraEquipment(string className)
        {
            string extraEquip = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT equipment FROM extraEquipment " +
                                  "INNER JOIN classes ON extraEquipment.classId=classes.classid " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                extraEquip = dbReader.GetString(0);
            }
            DBConnection.Close();

            return extraEquip;
        }
        
        /// <summary>
        /// gets a list of weapons with the specified type and range
        /// </summary>
        /// <param name="type">type: simple, martial or wildcard *</param>
        /// <param name="range">range: melee, ranged or wildcard *</param>
        public List<string> getWeaponList(string type, string range)
        {
            //replace * with % for sqlite wildcard
            if (type == "*")
            {
                type = "%";
            }
            if (range == "*")
            {
                range = "%";
            }

            List<string> weapons = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT name FROM weapons " +
                                  "INNER JOIN books ON weapons.book = books.bookid " +
                                  "WHERE type LIKE \"";
            dbQuery.CommandText += type;
            dbQuery.CommandText += "\" AND range LIKE \"";
            dbQuery.CommandText += range;
            dbQuery.CommandText += "\" AND books.title IN (";
            dbQuery.CommandText += UsedBooks;
            dbQuery.CommandText += ")";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                weapons.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return weapons;
        }

        /// <summary>
        /// gets a list of weapons with the spedified name
        /// </summary>
        /// <param name="name">name</param>
        public List<string> getWeaponList(string name)
        {
            //replace * with % for sqlite wildcard
            if (name == "*")
            {
                name = "%";
            }

            List<string> weapons = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT name FROM weapons " +
                                  "INNER JOIN books ON weapons.book = books.bookid " +
                                  "WHERE name LIKE \"";
            dbQuery.CommandText += name;
            dbQuery.CommandText += "\" AND books.title IN (";
            dbQuery.CommandText += UsedBooks;
            dbQuery.CommandText += ")";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                weapons.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return weapons;
        }

        /// <summary>
        /// checks, if the specified item is a weapon
        /// </summary>
        /// <param name="name">item to check</param>
        public bool isWeapon(string name)
        {
            bool isWeapon = false;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT * FROM weapons " +
                                  "WHERE name=\"";
            dbQuery.CommandText += name;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                isWeapon = !dbReader.IsDBNull(0);
            }
            DBConnection.Close();

            return isWeapon;
        }

        /// <summary>
        /// gets the stats of the specified weapon
        /// </summary>
        /// <param name="name">chosen weapon</param>
        public string getWeaponStats(string name)
        {
            string stats = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT name, damage, damageType, properties FROM weapons " +
                                  "WHERE name=\"";
            dbQuery.CommandText += name;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    stats = dbReader.GetString(0);
                    stats += " - ";
                    stats += dbReader.GetString(1);
                    stats += " - ";
                    stats += dbReader.GetString(2);
                    stats += Environment.NewLine;
                    stats += dbReader.GetString(3);
                }
            }
            DBConnection.Close();

            return stats;
        }

        //public string getWeaponType(string name)
        //{
        //    string type = "";

        //    DBConnection.Open();
        //    SQLiteDataReader dbReader;
        //    SQLiteCommand dbQuery;
        //    dbQuery = DBConnection.CreateCommand();
        //    dbQuery.CommandText = "SELECT type FROM weapons " +
        //                          "WHERE name=\"";
        //    dbQuery.CommandText += name;
        //    dbQuery.CommandText += "\"";

        //    dbReader = dbQuery.ExecuteReader();
        //    if (dbReader.Read())
        //    {
        //        if (!dbReader.IsDBNull(0))
        //        {
        //            type = dbReader.GetString(0);
        //        }
        //    }
        //    DBConnection.Close();

        //    return type;
        //}

        //public string getWeaponDamage(string name)
        //{
        //    string damage = "";

        //    DBConnection.Open();
        //    SQLiteDataReader dbReader;
        //    SQLiteCommand dbQuery;
        //    dbQuery = DBConnection.CreateCommand();
        //    dbQuery.CommandText = "SELECT damage FROM weapons " +
        //                          "WHERE name=\"";
        //    dbQuery.CommandText += name;
        //    dbQuery.CommandText += "\"";

        //    dbReader = dbQuery.ExecuteReader();
        //    if (dbReader.Read())
        //    {
        //        if (!dbReader.IsDBNull(0))
        //        {
        //            damage = dbReader.GetString(0);
        //        }
        //    }
        //    DBConnection.Close();

        //    return damage;
        //}

        //public string getWeaponDamageType(string name)
        //{
        //    string damageType = "";

        //    DBConnection.Open();
        //    SQLiteDataReader dbReader;
        //    SQLiteCommand dbQuery;
        //    dbQuery = DBConnection.CreateCommand();
        //    dbQuery.CommandText = "SELECT damageType FROM weapons " +
        //                          "WHERE name=\"";
        //    dbQuery.CommandText += name;
        //    dbQuery.CommandText += "\"";

        //    dbReader = dbQuery.ExecuteReader();
        //    if (dbReader.Read())
        //    {
        //        if (!dbReader.IsDBNull(0))
        //        {
        //            damageType = dbReader.GetString(0);
        //        }
        //    }
        //    DBConnection.Close();

        //    return damageType;
        //}

        //public string getWeaponProperties(string name)
        //{
        //    string properties = "";

        //    DBConnection.Open();
        //    SQLiteDataReader dbReader;
        //    SQLiteCommand dbQuery;
        //    dbQuery = DBConnection.CreateCommand();
        //    dbQuery.CommandText = "SELECT properties FROM weapons " +
        //                          "WHERE name=\"";
        //    dbQuery.CommandText += name;
        //    dbQuery.CommandText += "\"";

        //    dbReader = dbQuery.ExecuteReader();
        //    if (dbReader.Read())
        //    {
        //        if (!dbReader.IsDBNull(0))
        //        {
        //            properties = dbReader.GetString(0);
        //        }
        //    }
        //    DBConnection.Close();

        //    return properties;
        //}

        /// <summary>
        /// gets a list of armors with the specified name or of of the specified type
        /// </summary>
        /// <param name="name">chosen name or wildcard *</param>
        /// <param name="type">chosen type or wildcard *</param>
        public List<string> getArmorList(string name, string type)
        {
            //replace * with % for sqlite wildcard
            if (name == "*")
            {
                name = "%";
            }
            if (type == "*")
            {
                type = "%";
            }

            List<string> armor = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT name FROM armor " +
                                  "INNER JOIN books ON armor.book = books.bookid " +
                                  "WHERE name LIKE \"";
            dbQuery.CommandText += name;
            dbQuery.CommandText += "\" AND type LIKE \"";
            dbQuery.CommandText += type;
            dbQuery.CommandText += "\" AND books.title IN (";
            dbQuery.CommandText += UsedBooks;
            dbQuery.CommandText += ")";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                armor.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return armor;
        }

        /// <summary>
        /// checks, if the specified item is armor
        /// </summary>
        /// <param name="name">item to check</param>
        public bool isArmor(string name)
        {
            bool isArmor = false;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT * FROM armor " +
                                  "WHERE name=\"";
            dbQuery.CommandText += name;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                isArmor = !dbReader.IsDBNull(0);
            }
            DBConnection.Close();

            return isArmor;
        }

        /// <summary>
        /// gets the stats of the specified armor
        /// </summary>
        /// <param name="name">chosen armor</param>
        public string getArmorStats(string name)
        {
            string stats = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT * FROM armor " +
                                  "WHERE name=\"";
            dbQuery.CommandText += name;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    stats = dbReader.GetString(0);
                    stats += " (";
                    stats += dbReader.GetString(1);

                    if (dbReader.GetInt32(7) > 0)
                    {
                        stats += ", min. strength: ";
                        stats += dbReader.GetInt32(7).ToString();
                    }

                    stats += ")";
                    stats += Environment.NewLine;
                    stats += "AC: ";
                    stats += dbReader.GetInt32(3).ToString();

                    if (dbReader.GetInt32(4) > 0)
                    {
                        stats += " (+";
                        stats += dbReader.GetInt32(4).ToString();
                        stats += ")";
                    }

                    if (dbReader.GetString(5) != "-")
                    {
                        stats += " +";
                        stats += dbReader.GetString(5);

                        if (dbReader.GetInt32(6) > 0)
                        {
                            stats += " (max. ";
                            stats += dbReader.GetInt32(6).ToString();
                            stats += ")";
                        }
                    }

                    if (dbReader.GetString(8) != "-")
                    {
                        stats += " - stealth ";
                        stats += dbReader.GetString(8);
                    }
                }
            }
            DBConnection.Close();

            return stats;
        }
        
        /// <summary>
        /// gets the strength requirement for the specified armore
        /// </summary>
        /// <param name="armorName">chosen armor</param>
        /// <returns>the strength requirement or 99 when armor not found in database</returns>
        public int getArmorStrengthRequirement(string armorName)
        {
            //high value to prevent usage of armor that is not in database
            int strengthReq = 99;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT strength FROM armor " +
                                  "WHERE name=\"";
            dbQuery.CommandText += armorName;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    strengthReq = dbReader.GetInt32(0);
                }
            }
            DBConnection.Close();

            return strengthReq;
        }

        /// <summary>
        /// gets a list of tools of the specified type
        /// </summary>
        /// <param name="type">type of tools to return</param>
        public List<string> getToolList(string type)
        {
            List<string> tools = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT name FROM tools " +
                                  "WHERE type=\"";
            dbQuery.CommandText += type;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                tools.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return tools;
        }

        /// <summary>
        /// gets a list of packs the specified class can choose from
        /// </summary>
        /// <param name="className">chosen class</param>
        public List<string> getPackChoices(string className)
        {
            List<string> packs = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT equipmentPacks.name FROM equipmentPacks " +
                                  "INNER JOIN equipmentPackChoices ON equipmentPacks.packId=equipmentPackChoices.packId " +
                                  "INNER JOIN classes ON equipmentPackChoices.classId=classes.classid " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                packs.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return packs;
        }

        /// <summary>
        /// gets the content of the specified equipment pack
        /// </summary>
        /// <param name="packName">chosen equipment pack</param>
        public string getPackContent(string packName)
        {
            string content = "";

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT content FROM equipmentPacks " +
                                  "WHERE name=\"";
            dbQuery.CommandText += packName;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    content = dbReader.GetString(0);
                }
            }
            DBConnection.Close();

            return content;
        }

        /// <summary>
        /// checks, if a given item is an equipment pack
        /// </summary>
        /// <param name="packName">item to check</param>
        public bool isPack(string packName)
        {
            bool isPack = false;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT * FROM equipmentPacks " +
                                  "WHERE name=\"";
            dbQuery.CommandText += packName;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                isPack = !dbReader.IsDBNull(0);
            }
            DBConnection.Close();

            return isPack;
        }

        /// <summary>
        /// gets the stats of the specified weapon, armor or equipment pack
        /// </summary>
        /// <param name="name">chosen equipment</param>
        public string getEquipmentStats(string name)
        {
            if (isWeapon(name))
            {
                return getWeaponStats(name);
            }

            if (isArmor(name))
            {
                return getArmorStats(name);
            }

            if (isPack(name))
            {
                return getPackContent(name);
            }

            return "";
        }

        public bool hasSpellcasting(string className, string subclass, int level)
        {
            bool classHasSpellcasting = false;
            bool subclassHasSpellcasting = false;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT hasSpellcasting FROM classes " +
                                  "WHERE name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\"  AND spellcastingStartLevel BETWEEN 1 AND ";
            dbQuery.CommandText += level.ToString();

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                classHasSpellcasting = dbReader.GetBoolean(0);
            }

            if (subclass != "---")
            {
                dbQuery.CommandText = "SELECT hasSpellcasting FROM subclasses " +
                                      "WHERE name=\"";
                dbQuery.CommandText += subclass;
                dbQuery.CommandText += "\"";

                dbReader = dbQuery.ExecuteReader();
                if (dbReader.Read())
                {
                    subclassHasSpellcasting = dbReader.GetBoolean(0);
                }
            }

            DBConnection.Close();

            return classHasSpellcasting || subclassHasSpellcasting;
        }

        public int getCantripsKnown(string className, string subclass, int level)
        {
            int cantripsKnown = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT cantripsKnown FROM cantripsKnown " +
                                  "INNER JOIN classes ON cantripsKnown.classId = classes.classid " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\" AND cantripsKnown.level BETWEEN 1 AND ";
            dbQuery.CommandText += level.ToString();
            dbQuery.CommandText += " ORDER BY cantripsKnown.level DESC LIMIT 1";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    cantripsKnown += dbReader.GetInt32(0);
                }
            }

            if (subclass != "---")
            {
                dbQuery.CommandText = "SELECT cantripsKnown FROM cantripsKnownSubclass " +
                                      "INNER JOIN subclasses ON cantripsKnownSubclass.subclassId = subclasses.subclassId " +
                                      "WHERE subclasses.name=\"";
                dbQuery.CommandText += subclass;
                dbQuery.CommandText += "\" AND cantripsKnownSubclass.level BETWEEN 1 AND ";
                dbQuery.CommandText += level.ToString();
                dbQuery.CommandText += " ORDER BY cantripsKnownSubclass.level DESC LIMIT 1";

                dbReader = dbQuery.ExecuteReader();
                if (dbReader.Read())
                {
                    if (!dbReader.IsDBNull(0))
                    {
                        cantripsKnown += dbReader.GetInt32(0);
                    }
                }
            }

            DBConnection.Close();

            return cantripsKnown;
        }

        public bool areSpellsKnownStatic(string className, string subclass)
        {
            bool classSpellsKnownStatic = false;
            bool subclassSpellsKnownStatic = false;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT MAX(static) FROM spellsKnown " +
                                  "INNER JOIN classes ON spellsKnown.classId = classes.classid " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                classSpellsKnownStatic = dbReader.GetBoolean(0);
            }

            if (subclass != "---")
            {
                dbQuery.CommandText = "SELECT MAX(static) FROM spellsKnownSubclass " +
                                      "INNER JOIN subclasses ON spellsKnownSubclass.subclassId = subclasses.subclassId " +
                                      "WHERE subclasses.name=\"";
                dbQuery.CommandText += subclass;
                dbQuery.CommandText += "\"";

                dbReader = dbQuery.ExecuteReader();
                if (dbReader.Read())
                {
                    if (!dbReader.IsDBNull(0))
                    {
                        subclassSpellsKnownStatic = dbReader.GetBoolean(0);
                    }
                }
            }

            DBConnection.Close();

            return classSpellsKnownStatic || subclassSpellsKnownStatic;
        }

        public int getSpellsKnown(string className, string subclass, int level)
        {
            int spellsKnown = 0;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT spellsKnown FROM spellsKnown " +
                                  "INNER JOIN classes ON spellsKnown.classId = classes.classid " +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\" AND spellsKnown.level BETWEEN 1 AND ";
            dbQuery.CommandText += level.ToString();
            dbQuery.CommandText += " ORDER BY spellsKnown.level DESC LIMIT 1";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    spellsKnown += dbReader.GetInt32(0);
                }
            }

            if (subclass != "---")
            {
                dbQuery.CommandText = "SELECT spellsKnown FROM spellsKnownSubclass " +
                                      "INNER JOIN subclasses ON spellsKnownSubclass.subclassId = subclasses.subclassId " +
                                      "WHERE subclasses.name=\"";
                dbQuery.CommandText += subclass;
                dbQuery.CommandText += "\"AND spellsKnownSubclass.level BETWEEN 1 AND ";
                dbQuery.CommandText += level.ToString();
                dbQuery.CommandText += " ORDER BY spellsKnownSubclass.level DESC LIMIT 1";

                dbReader = dbQuery.ExecuteReader();
                if (dbReader.Read())
                {
                    if (!dbReader.IsDBNull(0))
                    {
                        spellsKnown += dbReader.GetInt32(0);
                    }
                }
            }

            DBConnection.Close();

            return spellsKnown;
        }

        public List<string> getCantripOptions(string className, string subclass)
        {
            List<string> cantripList = new List<string>();

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT name FROM spells " +
                                  "INNER JOIN books ON spells.book=books.bookid " +
                                  "WHERE spells.level=0 " +
                                  "AND spells.classes LIKE \"%";
            if ((subclass == "Arcane Trickster") || (subclass == "Eldritch Knight"))
            {
                dbQuery.CommandText += subclass;
            }
            else
            {
                dbQuery.CommandText += className;
            }
            dbQuery.CommandText += "%\" AND books.title IN (";
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

        public List<string> getSpellOptions(string className, string subclass, int level)
        {
            List<string> spellList = new List<string>();
            
            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT name FROM spells " +
                                  "INNER JOIN books ON spells.book=books.bookid " +
                                  "WHERE spells.level = " +
                                  "(SELECT maxSpellLevel FROM maxSpellLevel " +
                                  "INNER JOIN classes ON maxSpellLevel.classId = classes.classid" +
                                  "WHERE classes.name=\"";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "\" AND maxSpellLevel.level BETWEEN 1 AND ";
            dbQuery.CommandText += level.ToString();
            dbQuery.CommandText += " UNION" +
                                   "SELECT maxSpellLevel FROM maxSpellLevelSubclass " +
                                   "INNER JOIN subclasses ON maxSpellLevelSubclass.subclassId = subclasses.subclassId " +
                                   "WHERE subclasses.name=\"";
            dbQuery.CommandText += subclass;
            dbQuery.CommandText += "\" AND maxSpellLevelSubclass.level BETWEEN 1 AND ";
            dbQuery.CommandText += level.ToString();
            dbQuery.CommandText += " ORDER BY maxSpellLevel DESC LIMIT 1)" +
                                   "AND(spells.classes LIKE \"%";
            dbQuery.CommandText += className;
            dbQuery.CommandText += "%\" OR spells.classes LIKE \"%";
            dbQuery.CommandText += subclass;
            dbQuery.CommandText += "%\") AND books.title IN (\"";
            dbQuery.CommandText += UsedBooks;
            dbQuery.CommandText += "\")";

            dbReader = dbQuery.ExecuteReader();
            while (dbReader.Read())
            {
                spellList.Add(dbReader.GetString(0));
            }
            DBConnection.Close();

            return spellList;
        }

        public Spell getSpell(string spellName)
        {
            Spell outputSpell;

            DBConnection.Open();
            SQLiteDataReader dbReader;
            SQLiteCommand dbQuery;
            dbQuery = DBConnection.CreateCommand();
            dbQuery.CommandText = "SELECT * FROM spells " +
                                  "WHERE name = \"";
            dbQuery.CommandText += spellName;
            dbQuery.CommandText += "\"";

            dbReader = dbQuery.ExecuteReader();
            if (dbReader.Read())
            {
                if (!dbReader.IsDBNull(0))
                {
                    outputSpell = new Spell(dbReader.GetString(1), dbReader.GetBoolean(2), dbReader.GetInt32(3), dbReader.GetString(4), dbReader.GetString(5), dbReader.GetString(6), dbReader.GetString(7), dbReader.GetString(8), dbReader.GetString(9), dbReader.GetString(10));
                }
            }

            DBConnection.Close();

            outputSpell = new Spell();
            return outputSpell;
        }
















        

        bool DataManager::hasExtraRaceSpells(QString subrace, int level)
        {
            QSqlQuery query(db);
            QString querystring = "SELECT * FROM extraRaceSpells "


                          "INNER JOIN races ON extraRaceSpells.raceId=races.raceid "


                          "WHERE races.subrace=\"";
            querystring += subrace;
            querystring += "\" AND extraRaceSpells.level BETWEEN 1 AND ";
            querystring += QString::number(level);

            if (query.exec(querystring))
            {
                if (query.next())
                {
                    return true;
                }
            }

            return false;
        }

        bool DataManager::hasExtraSubclassSpells(QString subclassName, int level)
        {
            QSqlQuery query(db);
            QString querystring = "SELECT * FROM extraSubclassSpells "


                          "INNER JOIN subclasses ON extraSubclassSpells.subclassId=subclasses.subclassId "


                          "WHERE subclasses.name=\"";
            querystring += subclassName;
            querystring += "\" AND extraSubclassSpells.level BETWEEN 1 AND ";
            querystring += QString::number(level);

            if (query.exec(querystring))
            {
                if (query.next())
                {
                    return true;
                }
            }

            return false;
        }

        QVector<Spell>* DataManager::getExtraRaceSpells(QString subrace, int level)
        {
            QVector<Spell>* extraRaceSpells = new QVector<Spell>();

            QSqlQuery query(db);
            QString querystring = "SELECT spells.* FROM extraRaceSpells "


                          "INNER JOIN races ON extraRaceSpells.raceId=races.raceid "


                          "INNER JOIN spells ON extraRaceSpells.spellId=spells.spellId "


                          "WHERE races.subrace=\"";
            querystring += subrace;
            querystring += "\" AND extraRaceSpells.level BETWEEN 1 AND ";
            querystring += QString::number(level);

            if (query.exec(querystring))
            {
                while (query.next())
                {
                    extraRaceSpells->append(*new Spell(query.value(1).toString(), query.value(2).toBool(), query.value(3).toInt(), query.value(4).toString(), query.value(5).toString(), query.value(6).toString(), query.value(7).toString(), query.value(8).toString(), query.value(9).toString(), query.value(10).toString()));
                }
            }

            return extraRaceSpells;
        }

        QVector<Spell>* DataManager::getExtraSubclassSpells(QString subclassName, int level)
        {
            QVector<Spell>* extraSubclassSpells = new QVector<Spell>();

            QSqlQuery query(db);
            QString querystring = "SELECT spells.* FROM extraSubclassSpells "


                          "INNER JOIN subclasses ON extraSubclassSpells.subclassId=subclasses.subclassId "


                          "INNER JOIN spells ON extraSubclassSpells.spellId=spells.spellId "


                          "WHERE subclasses.name=\"";
            querystring += subclassName;
            querystring += "\" AND extraSubclassSpells.level BETWEEN 1 AND ";
            querystring += QString::number(level);

            if (query.exec(querystring))
            {
                while (query.next())
                {
                    extraSubclassSpells->append(*new Spell(query.value(1).toString(), query.value(2).toBool(), query.value(3).toInt(), query.value(4).toString(), query.value(5).toString(), query.value(6).toString(), query.value(7).toString(), query.value(8).toString(), query.value(9).toString(), query.value(10).toString()));
                }
            }

            return extraSubclassSpells;
        }

        bool DataManager::isExtraRaceSpell(QString subrace, int level, QString spell)
        {
            QSqlQuery query(db);
            QString querystring = "SELECT spells.name FROM extraRaceSpells "


                          "INNER JOIN races ON extraRaceSpells.raceId=races.raceid "


                          "INNER JOIN spells ON extraRaceSpells.spellId=spells.spellId "


                          "WHERE races.subrace=\"";
            querystring += subrace;
            querystring += "\" AND extraRaceSpells.level BETWEEN 1 AND ";
            querystring += QString::number(level);
            querystring += " AND spells.name=\"";
            querystring += spell;
            querystring += "\"";

            if (query.exec(querystring))
            {
                if (query.next())
                {
                    return true;
                }
            }

            return false;

        }

        bool DataManager::isExtraSubclassSpell(QString subclassName, int level, QString spell)
        {
            QSqlQuery query(db);
            QString querystring = "SELECT spells.* FROM extraSubclassSpells "


                          "INNER JOIN subclasses ON extraSubclassSpells.subclassId=subclasses.subclassId "


                          "INNER JOIN spells ON extraSubclassSpells.spellId=spells.spellId "


                          "WHERE subclasses.name=\"";
            querystring += subclassName;
            querystring += "\" AND extraSubclassSpells.level BETWEEN 1 AND ";
            querystring += QString::number(level);
            querystring += " AND spells.name=\"";
            querystring += spell;
            querystring += "\"";

            if (query.exec(querystring))
            {
                if (query.next())
                {
                    return true;
                }
            }

            return false;

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