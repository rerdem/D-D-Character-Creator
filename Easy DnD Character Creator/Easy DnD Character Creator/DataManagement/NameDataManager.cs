using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public class NameDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public NameDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// checks, if members of a given subrace usually have last names
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        public bool hasLastName(string subrace)
        {
            bool hasLastName = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT names.name FROM nameRaceConnection " +
                                      "INNER JOIN races ON nameRaceConnection.raceId=races.raceid " +
                                      "INNER JOIN names ON nameRaceConnection.nameId=names.nameId " +
                                      "WHERE races.subrace=@Subrace AND names.isFirstName=0";
                command.Parameters.AddWithValue("@Subrace", subrace);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        hasLastName = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasLastName;
        }

        /// <summary>
        /// gets all available first names for a given subrace and sex
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="isMale">is the character male</param>
        public List<string> getFirstNames(string subrace, bool isMale)
        {
            List<string> firstNames = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT names.name FROM nameRaceConnection " +
                                      "INNER JOIN races ON nameRaceConnection.raceId=races.raceid " +
                                      "INNER JOIN names ON nameRaceConnection.nameId=names.nameId " +
                                      "WHERE races.subrace=@Subrace AND names.isFirstName=1 " +
                                      "AND (sex = \"-\" OR sex = @Sex)";
                command.Parameters.AddWithValue("@Subrace", subrace);

                if (isMale)
                {
                    command.Parameters.AddWithValue("@Sex", "m");
                }
                else
                {
                    command.Parameters.AddWithValue("@Sex", "f");
                }

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            firstNames.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return firstNames;
        }

        /// <summary>
        /// gets all available last names for a given subrace
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        public List<string> getLastNames(string subrace)
        {
            List<string> lastNames = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT names.name FROM nameRaceConnection " +
                                      "INNER JOIN races ON nameRaceConnection.raceId=races.raceid " +
                                      "INNER JOIN names ON nameRaceConnection.nameId=names.nameId " +
                                      "WHERE races.subrace=@Subrace AND names.isFirstName=0";
                command.Parameters.AddWithValue("@Subrace", subrace);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            lastNames.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return lastNames;
        }
    }
}
