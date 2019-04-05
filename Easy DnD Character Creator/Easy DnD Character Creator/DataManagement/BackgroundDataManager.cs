using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public class BackgroundDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public BackgroundDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        public List<Background> getBackgrounds()
        {
            List<Background> backgroundList = new List<Background>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT name, description, equipment, gp, extraToolProficiencies, extraFeatureChoice FROM backgrounds " +
                                      "INNER JOIN backgroundEquipment ON backgroundEquipment.backgroundId = backgrounds.backgroundId " +
                                      "INNER JOIN books ON backgrounds.book=books.bookid " +
                                      "WHERE books.title IN (@UsedBooks)";
                SQLiteCommandExtensions.AddParametersWithValues(command, "@UsedBooks", UsedBooks);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            backgroundList.Add(new Background(dbReader.GetString(0), dbReader.GetString(1), dbReader.GetString(2), dbReader.GetInt32(3), dbReader.GetBoolean(4), dbReader.GetBoolean(5)));
                        }
                    }
                }
            }

            return backgroundList;
        }



        ///// <summary>
        ///// gets all available backgrounds
        ///// </summary>
        //public List<string> getBackgrounds()
        //{
        //    List<string> backgroundList = new List<string>();

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        connection.Open();
        //        command.CommandText = "SELECT name FROM backgrounds " +
        //                              "INNER JOIN books ON backgrounds.book=books.bookid " +
        //                              "WHERE books.title IN (@UsedBooks)";
        //        //command.Parameters.AddWithValue("@UsedBooks", TempUsedBooks);
        //        SQLiteCommandExtensions.AddParametersWithValues(command, "@UsedBooks", UsedBooks);

        //        using (SQLiteDataReader dbReader = command.ExecuteReader())
        //        {
        //            while (dbReader.Read())
        //            {
        //                if (!dbReader.IsDBNull(0))
        //                {
        //                    backgroundList.Add(dbReader.GetString(0));
        //                }
        //            }
        //        }
        //    }

        //    return backgroundList;
        //}

        ///// <summary>
        ///// gets the description of the chosen background
        ///// </summary>
        ///// <param name="background">chosen background</param>
        //public string getBackgroundDescription(string background)
        //{
        //    string description = "";

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        connection.Open();
        //        command.CommandText = "SELECT description FROM backgrounds " +
        //                              "INNER JOIN books ON backgrounds.book=books.bookid " +
        //                              "WHERE books.title IN (@UsedBooks) AND name=@Background";
        //        command.Parameters.AddWithValue("@Background", background);
        //        SQLiteCommandExtensions.AddParametersWithValues(command, "@UsedBooks", UsedBooks);

        //        using (SQLiteDataReader dbReader = command.ExecuteReader())
        //        {
        //            if (dbReader.Read())
        //            {
        //                if (!dbReader.IsDBNull(0))
        //                {
        //                    description = dbReader.GetString(0);
        //                }
        //            }
        //        }
        //    }

        //    return description;
        //}

        ///// <summary>
        ///// checks, if the chosen background has extra tool proficiency choices
        ///// </summary>
        ///// <param name="background">chosen background</param>
        //public bool backgroundHasExtraChoice(string background)
        //{
        //    bool hasChoice = false;

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        connection.Open();
        //        command.CommandText = "SELECT extraToolProficiencies FROM backgrounds " +
        //                              "WHERE name=@Background";
        //        command.Parameters.AddWithValue("@Background", background);
                
        //        using (SQLiteDataReader dbReader = command.ExecuteReader())
        //        {
        //            if (dbReader.Read())
        //            {
        //                if (!dbReader.IsDBNull(0))
        //                {
        //                    hasChoice = dbReader.GetBoolean(0);
        //                }
        //            }
        //        }
        //    }

        //    return hasChoice;
        //}

        /// <summary>
        /// gets a list of proficiency choices for the chosen background
        /// </summary>
        /// <param name="background">chosen background</param>
        public List<string> getExtraBackgroundProficiencies(string background)
        {
            List<string> proficiencyList = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT tools.name FROM tools " +
                                      "INNER JOIN extraBackgroundToolProficiencyChoices ON extraBackgroundToolProficiencyChoices.type=tools.type " +
                                      "INNER JOIN backgrounds ON extraBackgroundToolProficiencyChoices.backgroundId=backgrounds.backgroundId " +
                                      "WHERE backgrounds.name=@Background " +
                                      "ORDER BY tools.name";
                command.Parameters.AddWithValue("@Background", background);

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

        ///// <summary>
        ///// checks, if a given background has an extra story choice to make
        ///// </summary>
        ///// <param name="background">chosen background</param>
        //public bool hasBackgroundStoryChoice(string background)
        //{
        //    bool hasChoice = false;

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        connection.Open();
        //        command.CommandText = "SELECT extraFeatureChoice FROM backgrounds " +
        //                              "WHERE name=@Background";
        //        command.Parameters.AddWithValue("@Background", background);

        //        using (SQLiteDataReader dbReader = command.ExecuteReader())
        //        {
        //            if (dbReader.Read())
        //            {
        //                if (!dbReader.IsDBNull(0))
        //                {
        //                    hasChoice = dbReader.GetBoolean(0);
        //                }
        //            }
        //        }
        //    }

        //    return hasChoice;
        //}

        ///// <summary>
        ///// checks, if the chosen background has an extra feature choice attached to it
        ///// </summary>
        ///// <param name="background">the background to be checked</param>
        //public bool backgroundHasExtraFeatureChoice(string background)
        //{
        //    bool hasChoice = false;

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        connection.Open();
        //        command.CommandText = "SELECT extraFeatureChoice FROM backgrounds " +
        //                              "WHERE name=@Background";
        //        command.Parameters.AddWithValue("@Background", background);

        //        using (SQLiteDataReader dbReader = command.ExecuteReader())
        //        {
        //            if (dbReader.Read())
        //            {
        //                if (!dbReader.IsDBNull(0))
        //                {
        //                    hasChoice = dbReader.GetBoolean(0);
        //                }
        //            }
        //        }
        //    }

        //    return hasChoice;
        //}

        ///// <summary>
        ///// gets the title of the extra background feature choice to make
        ///// </summary>
        ///// <param name="background">chosen background</param>
        //public string getBackgroundFeatureChoiceTitle(string background)
        //{
        //    string title = "";

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        connection.Open();
        //        command.CommandText = "SELECT title FROM extraBackgroundChoices " +
        //                              "INNER JOIN backgrounds ON extraBackgroundChoices.backgroundId=backgrounds.backgroundId " +
        //                              "WHERE backgrounds.name=@Background";
        //        command.Parameters.AddWithValue("@Background", background);

        //        using (SQLiteDataReader dbReader = command.ExecuteReader())
        //        {
        //            if (dbReader.Read())
        //            {
        //                if (!dbReader.IsDBNull(0))
        //                {
        //                    title = dbReader.GetString(0);
        //                }
        //            }
        //        }
        //    }

        //    return title;
        //}

        ///// <summary>
        ///// gets a description of the extra background feature choice
        ///// </summary>
        ///// <param name="background">chosen background</param>
        //public string getBackgroundFeatureChoiceDescription(string background)
        //{
        //    string description = "";

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        connection.Open();
        //        command.CommandText = "SELECT extraBackgroundChoices.description FROM extraBackgroundChoices " +
        //                              "INNER JOIN backgrounds ON extraBackgroundChoices.backgroundId=backgrounds.backgroundId " +
        //                              "WHERE backgrounds.name=@Background";
        //        command.Parameters.AddWithValue("@Background", background);

        //        using (SQLiteDataReader dbReader = command.ExecuteReader())
        //        {
        //            if (dbReader.Read())
        //            {
        //                if (!dbReader.IsDBNull(0))
        //                {
        //                    description = dbReader.GetString(0);
        //                }
        //            }
        //        }
        //    }

        //    return description;
        //}

        ///// <summary>
        ///// gets a list of the options for the extra background feature choice
        ///// </summary>
        ///// <param name="background">chosen background</param>
        //public List<string> getBackgroundFeatureChoiceOptions(string background)
        //{
        //    List<string> backgroundChoicesList = new List<string>();

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        connection.Open();
        //        command.CommandText = "SELECT text FROM extraBackgroundChoiceOptions " +
        //                              "INNER JOIN extraBackgroundChoices ON extraBackgroundChoiceOptions.extraChoiceId=extraBackgroundChoices.extraChoiceId " +
        //                              "INNER JOIN backgrounds ON extraBackgroundChoices.backgroundId=backgrounds.backgroundId " +
        //                              "WHERE backgrounds.name=@Background " +
        //                              "ORDER BY extraBackgroundChoiceOptions.diceValue ASC";
        //        command.Parameters.AddWithValue("@Background", background);

        //        using (SQLiteDataReader dbReader = command.ExecuteReader())
        //        {
        //            while (dbReader.Read())
        //            {
        //                if (!dbReader.IsDBNull(0))
        //                {
        //                    backgroundChoicesList.Add(dbReader.GetString(0));
        //                }
        //            }
        //        }
        //    }

        //    return backgroundChoicesList;
        //}


        //################################################################


        ///// <summary>
        ///// gets equipment gained by the given background
        ///// </summary>
        ///// <param name="background">chosen background</param>
        //public string getBackgroundEquipment(string background)
        //{
        //    string equipment = "";

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        connection.Open();
        //        command.CommandText = "SELECT backgroundEquipment.equipment FROM backgroundEquipment " +
        //                              "INNER JOIN backgrounds ON backgroundEquipment.backgroundId = backgrounds.backgroundId " +
        //                              "WHERE backgrounds.name = @Background";
        //        command.Parameters.AddWithValue("@Background", background);

        //        using (SQLiteDataReader dbReader = command.ExecuteReader())
        //        {
        //            if (dbReader.Read())
        //            {
        //                if (!dbReader.IsDBNull(0))
        //                {
        //                    equipment = dbReader.GetString(0);
        //                }
        //            }
        //        }
        //    }

        //    return equipment;
        //}

        ///// <summary>
        ///// gets the amount of gold gained by a given background
        ///// </summary>
        ///// <param name="background">chosen background</param>
        //public int getBackgroundGold(string background)
        //{
        //    int gold = 0;

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        connection.Open();
        //        command.CommandText = "SELECT backgroundEquipment.gp FROM backgroundEquipment " +
        //                              "INNER JOIN backgrounds ON backgroundEquipment.backgroundId=backgrounds.backgroundId " +
        //                              "WHERE backgrounds.name = @Background";
        //        command.Parameters.AddWithValue("@Background", background);

        //        using (SQLiteDataReader dbReader = command.ExecuteReader())
        //        {
        //            if (dbReader.Read())
        //            {
        //                if (!dbReader.IsDBNull(0))
        //                {
        //                    gold = dbReader.GetInt32(0);
        //                }
        //            }
        //        }
        //    }

        //    return gold;
        //}


    }
}
