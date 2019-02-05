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
        /// gets the number of extra languages gained through the chosen class
        /// </summary>
        /// <param name="className">chosen class</param>
        //public int getExtraClassLanguageCount(string className)
        //{
        //    int languageCount = 0;

        //    DBConnection.Open();
        //    SQLiteDataReader dbReader;
        //    SQLiteCommand dbQuery;
        //    dbQuery = DBConnection.CreateCommand();
        //    dbQuery.CommandText = "SELECT extraLanguages FROM extraClassLanguages " +
        //                          "INNER JOIN classes ON extraClassLanguages.classId=classes.classid " +
        //                          "WHERE classes.name=\"";
        //    dbQuery.CommandText += className;
        //    dbQuery.CommandText += "\"";

        //    dbReader = dbQuery.ExecuteReader();
        //    if (dbReader.Read())
        //    {
        //        languageCount = dbReader.GetInt32(0);
        //    }
        //    DBConnection.Close();

        //    return languageCount;
        //}

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
            //extra class languages
            //dbQuery.CommandText += "\" " +
            //                       "UNION ALL " +
            //                       "SELECT extraLanguages FROM extraClassLanguages " +
            //                       "INNER JOIN classes ON extraClassLanguages.classId = classes.classid " +
            //                       "WHERE classes.name=\"";
            //dbQuery.CommandText += className;
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
                languageCount = dbReader.GetInt32(0);
            }
            DBConnection.Close();

            return languageCount;
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