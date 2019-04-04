using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public class RaceDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public RaceDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// gets a list of all playable races
        /// </summary>
        public List<Race> getRaces()
        {
            List<Race> raceList = new List<Race>();
            Race currentRace = new Race();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT race, subrace, description, alignmentDescription, speed FROM races " +
                                      "INNER JOIN books ON races.book=books.bookid " +
                                      "WHERE books.title IN (@UsedBooks)";
                SQLiteCommandExtensions.AddParametersWithValues(command, "@UsedBooks", UsedBooks);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            if (dbReader.GetString(0) != currentRace.Name)
                            {
                                if (!string.IsNullOrEmpty(currentRace.Name))
                                {
                                    raceList.Add(currentRace);
                                }
                                currentRace = new Race(dbReader.GetString(0));
                            }
                            currentRace.addSubrace(new Subrace(dbReader.GetString(1), dbReader.GetString(2), dbReader.GetString(3), dbReader.GetInt32(4), subraceHasProficiencyChoice(dbReader.GetString(1))));
                        }
                    }

                    if (!raceList.Contains(currentRace) && !string.IsNullOrEmpty(currentRace.Name))
                    {
                        raceList.Add(currentRace);
                    }
                }
            }

            return raceList;
        }

        /// <summary>
        /// checks, if the chosen race has an extra choice attached to it
        /// </summary>
        /// <param name="subraceChoice">the race to be checked</param>
        public bool subraceHasProficiencyChoice(string subraceChoice)
        {
            bool hasChoice = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT extraToolProficiencies FROM races " +
                                      "WHERE subrace=@SubraceChoice";
                command.Parameters.AddWithValue("@SubraceChoice", subraceChoice);
                
                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            hasChoice = dbReader.GetBoolean(0);
                        }
                    }
                }
            }

            return hasChoice;
        }

        /// <summary>
        /// gets a list of proficiencies for the chosen subrace
        /// </summary>
        /// <param name="subraceChoice">chosen subrace</param>
        public List<string> getExtraRaceProficiencies(string subraceChoice)
        {
            List<string> proficiencyList = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT tools.name FROM tools " +
                                      "INNER JOIN extraRaceToolProficiencyChoices ON extraRaceToolProficiencyChoices.name=tools.name " +
                                      "INNER JOIN races ON extraRaceToolProficiencyChoices.raceId=races.raceid " +
                                      "WHERE races.subrace=@SubraceChoice";
                command.Parameters.AddWithValue("@SubraceChoice", subraceChoice);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            proficiencyList.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            if (proficiencyList.Count == 0)
            {
                proficiencyList.Add("---");
            }

            return proficiencyList;
        }

        /// <summary>
        /// gets description for the chosen alignment
        /// </summary>
        /// <param name="law">lawful, neutral or chaotic</param>
        /// <param name="morality">good, neutral or evil</param>
        public string getAlignmentDescription(string law, string morality)
        {
            string description = "";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT description FROM alignment " +
                                      "WHERE law=@Law AND morality=@Morality";
                command.Parameters.AddWithValue("@Law", law);
                command.Parameters.AddWithValue("@Morality", morality);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
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

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT law FROM alignment " +
                                      "WHERE law!=@Law " +
                                      "GROUP BY law";
                command.Parameters.AddWithValue("@Law", "-");

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            lawList.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

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

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT morality FROM alignment " +
                                      "WHERE morality!=@Morality " +
                                      "GROUP BY morality";
                command.Parameters.AddWithValue("@Morality", "-");

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            moralityList.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            if (moralityList.Count == 0)
            {
                moralityList.Add("---");
            }

            return moralityList;
        }

        /// <summary>
        /// gets the speed of a given subrace
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        public int getSpeed(string subrace)
        {
            int speed = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT speed FROM races " +
                                      "WHERE subrace=@Subrace";
                command.Parameters.AddWithValue("@Subrace", subrace);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            speed = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return speed;
        }
    }
}
