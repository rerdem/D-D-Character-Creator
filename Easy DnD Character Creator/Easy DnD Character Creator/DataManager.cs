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