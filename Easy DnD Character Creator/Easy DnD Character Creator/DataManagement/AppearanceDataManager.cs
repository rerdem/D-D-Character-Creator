using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public class AppearanceDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public AppearanceDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// gets the description of race specific ages
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        public string getAgeDescription(string subrace)
        {
            string description = "";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT ageDescription FROM races " +
                                      "WHERE subrace=@Subrace";
                command.Parameters.AddWithValue("@Subrace", subrace);
                
                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            description = dbReader.GetString(0);
                        }
                    }
                }
            }

            return description;
        }

        /// <summary>
        /// gets number of dice used for height modification
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        public int getHeightModifierDiceCount(string subrace)
        {
            int heightModifierDiceCount = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT heightModDiceCount FROM size " +
                                      "INNER JOIN races ON size.raceId=races.raceid " +
                                      "WHERE races.subrace=@Subrace";
                command.Parameters.AddWithValue("@Subrace", subrace);
                
                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            heightModifierDiceCount = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return heightModifierDiceCount;
        }

        /// <summary>
        /// gets type of dice used for height modification
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        public int getHeightModifierDiceType(string subrace)
        {
            int heightModifierDiceType = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT heightModDiceType FROM size " +
                                      "INNER JOIN races ON size.raceId=races.raceid " +
                                      "WHERE races.subrace=@Subrace";
                command.Parameters.AddWithValue("@Subrace", subrace);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            heightModifierDiceType = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return heightModifierDiceType;
        }

        /// <summary>
        /// gets number of dice used for weight modification
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        public int getWeightModifierDiceCount(string subrace)
        {
            int weightModifierDiceCount = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT weightModDiceCount FROM size " +
                                      "INNER JOIN races ON size.raceId=races.raceid " +
                                      "WHERE races.subrace=@Subrace";
                command.Parameters.AddWithValue("@Subrace", subrace);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            weightModifierDiceCount = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return weightModifierDiceCount;
        }

        /// <summary>
        /// gets type of dice used for weight modification
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        public int getWeightModifierDiceType(string subrace)
        {
            int weightModifierDiceType = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT weightModDiceType FROM size " +
                                      "INNER JOIN races ON size.raceId=races.raceid " +
                                      "WHERE races.subrace=@Subrace";
                command.Parameters.AddWithValue("@Subrace", subrace);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            weightModifierDiceType = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return weightModifierDiceType;
        }

        /// <summary>
        /// gets the base height in metric or imperial for the chosen race
        /// </summary>
        /// <param name="metric">true for metric, false for imperial</param>
        /// <param name="subrace">chosen subrace</param>
        public int getBaseHeight(bool metric, string subrace)
        {
            int baseHeight = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                if (metric)
                {
                    command.CommandText = "SELECT baseHeightCm FROM size " +
                                      "INNER JOIN races ON size.raceId=races.raceid " +
                                      "WHERE races.subrace=@Subrace";
                }
                else
                {
                    command.CommandText = "SELECT baseHeightIn FROM size " +
                                      "INNER JOIN races ON size.raceId=races.raceid " +
                                      "WHERE races.subrace=@Subrace";
                }
                command.Parameters.AddWithValue("@Subrace", subrace);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            baseHeight = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return baseHeight;
        }

        /// <summary>
        /// gets the base Weight in metric or imperial for the chosen race
        /// </summary>
        /// <param name="metric">true for metric, false for imperial</param>
        /// <param name="subrace">chosen subrace</param>
        public int getBaseWeight(bool metric, string subrace)
        {
            int baseWeight = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                if (metric)
                {
                    command.CommandText = "SELECT baseWeightKg FROM size " +
                                      "INNER JOIN races ON size.raceId=races.raceid " +
                                      "WHERE races.subrace=@Subrace";
                }
                else
                {
                    command.CommandText = "SELECT baseWeightLb FROM size " +
                                      "INNER JOIN races ON size.raceId=races.raceid " +
                                      "WHERE races.subrace=@Subrace";
                }
                command.Parameters.AddWithValue("@Subrace", subrace);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            baseWeight = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return baseWeight;
        }

    }
}
