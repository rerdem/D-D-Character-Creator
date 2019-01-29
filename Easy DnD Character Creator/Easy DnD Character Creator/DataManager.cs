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