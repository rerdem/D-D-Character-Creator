using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public class StoryDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public StoryDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// gets a list of all available personality traits for a given background
        /// </summary>
        /// <param name="background">chosen background</param>
        public List<string> getTraits(string background)
        {
            List<string> traits = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT text FROM personalityTraits " +
                                      "INNER JOIN backgrounds ON personalityTraits.backgroundId=backgrounds.backgroundId " +
                                      "WHERE backgrounds.name=@Background " +
                                      "ORDER BY personalityTraits.d8Value ASC";
                command.Parameters.AddWithValue("@Background", background);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            traits.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return traits;
        }

        /// <summary>
        /// gets a list of all available ideals for a given background
        /// </summary>
        /// <param name="background">chosen background</param>
        public List<string> getIdeals(string background)
        {
            List<string> ideals = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT text FROM ideals " +
                                      "INNER JOIN backgrounds ON ideals.backgroundId=backgrounds.backgroundId " +
                                      "WHERE backgrounds.name=@Background " +
                                      "ORDER BY ideals.d6Value ASC";
                command.Parameters.AddWithValue("@Background", background);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            ideals.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return ideals;
        }

        /// <summary>
        /// gets a list of all available bonds for a given background
        /// </summary>
        /// <param name="background">chosen background</param>
        public List<string> getBonds(string background)
        {
            List<string> bonds = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT text FROM bonds " +
                                      "INNER JOIN backgrounds ON bonds.backgroundId=backgrounds.backgroundId " +
                                      "WHERE backgrounds.name=@Background " +
                                      "ORDER BY bonds.d6Value ASC";
                command.Parameters.AddWithValue("@Background", background);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            bonds.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return bonds;
        }

        /// <summary>
        /// gets a list of all available flaws for a given background
        /// </summary>
        /// <param name="background">chosen background</param>
        public List<string> getFlaws(string background)
        {
            List<string> flaws = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT text FROM flaws " +
                                      "INNER JOIN backgrounds ON flaws.backgroundId=backgrounds.backgroundId " +
                                      "WHERE backgrounds.name=@Background " +
                                      "ORDER BY flaws.d6Value ASC";
                command.Parameters.AddWithValue("@Background", background);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            flaws.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return flaws;
        }

        /// <summary>
        /// checks, if a given background has an extra story choice to make
        /// </summary>
        /// <param name="background">chosen background</param>
        public bool hasBackgroundStoryChoice(string background)
        {
            bool hasChoice = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT extraFeatureChoice FROM backgrounds " +
                                      "WHERE name=@Background";
                command.Parameters.AddWithValue("@Background", background);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
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
        /// gets the story choice for a given background
        /// </summary>
        /// <param name="background">chosen background</param>
        public BackgroundStoryChoice getBackgroundStoryChoice(string background)
        {
            BackgroundStoryChoice storyChoice = new BackgroundStoryChoice();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT extraBackgroundChoices.title, extraBackgroundChoices.description, extraBackgroundChoiceOptions.text " +
                                      "FROM extraBackgroundChoices " +
                                      "INNER JOIN backgrounds ON extraBackgroundChoices.backgroundId=backgrounds.backgroundId " +
                                      "INNER JOIN extraBackgroundChoiceOptions ON extraBackgroundChoiceOptions.extraChoiceId=extraBackgroundChoices.extraChoiceId " +
                                      "WHERE backgrounds.name=@Background " +
                                      "ORDER BY extraBackgroundChoiceOptions.diceValue ASC";
                command.Parameters.AddWithValue("@Background", background);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            if (string.IsNullOrEmpty(storyChoice.Name))
                            {
                                storyChoice = new BackgroundStoryChoice(dbReader.GetString(0), dbReader.GetString(1));
                            }
                            storyChoice.addOption(dbReader.GetString(2));
                        }
                    }
                }
            }

            return storyChoice;
        }
    }
}
