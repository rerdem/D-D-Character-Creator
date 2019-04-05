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

        /// <summary>
        /// gets all available backgrounds
        /// </summary>
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

    }
}
