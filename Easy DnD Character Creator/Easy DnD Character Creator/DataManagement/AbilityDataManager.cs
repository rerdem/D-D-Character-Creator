using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public class AbilityDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public AbilityDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// gets the recommended abilities for the chosen class
        /// </summary>
        /// <param name="className">chosen class</param>
        public string getAbilityRecommendation(string className)
        {
            string recommendation = "";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT recommendation FROM preferredAbilities " +
                                      "INNER JOIN classes ON preferredAbilities.classid=classes.classid " +
                                      "WHERE classes.name=@Class";
                command.Parameters.AddWithValue("@Class", className);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            recommendation = dbReader.GetString(0);
                        }
                    }
                }
            }

            return recommendation;
        }

        /// <summary>
        /// gets the description of an ability
        /// </summary>
        /// <param name="ability">specified ability</param>
        public string getAbilityDescription(string ability)
        {
            string description = "";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT description FROM abilities " +
                                      "WHERE UPPER(name)=@Ability";
                command.Parameters.AddWithValue("@Ability", ability);

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
        /// gets the value of the ability score bonus received from the chosen subrace
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="ability">ability name</param>
        public int getAbilityScoreBonus(string subrace, string ability)
        {
            int bonus = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = $"SELECT {ability.ToLower()} FROM raceAbilityScoreAdditions " +
                                      $"INNER JOIN races ON raceAbilityScoreAdditions.raceid=races.raceid " +
                                      $"WHERE races.subrace=@Subrace";
                command.Parameters.AddWithValue("@Subrace", subrace);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            bonus = dbReader.GetInt32(0);
                        }
                    }
                }
            }

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
            if (subraceChoice == "Half-Elf")
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

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM averageAbilityScores";

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            averageScores.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

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
        /// <param name="className">chosen class</param>
        public int getMaximumHitDieResult(string className)
        {
            int result = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT maximumResult FROM hitDice " +
                                      "INNER JOIN classes ON hitDice.classId=classes.classid " +
                                      "WHERE name=@Class";
                command.Parameters.AddWithValue("@Class", className);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            result = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// gets the average result of the hit die of the chosen class
        /// </summary>
        /// <param name="className">chosen class</param>
        public int getAverageHitDieResult(string className)
        {
            int result = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT averageResult FROM hitDice " +
                                      "INNER JOIN classes ON hitDice.classId=classes.classid " +
                                      "WHERE name=@Class";
                command.Parameters.AddWithValue("@Class", className);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            result = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return result;
        }

    }
}
