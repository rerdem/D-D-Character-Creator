using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement.ExtraSubclassManagers
{
    public class ExtraToolProficiencyDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public ExtraToolProficiencyDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// checks, if a given subclass must choose additional tool proficiencies at a given level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public bool hasToolProficiencyChoice(string subclass, int level)
        {
            bool hasChoice = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT extraSubclassToolProficiencyChoices.name FROM extraSubclassToolProficiencyChoices " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = extraSubclassToolProficiencyChoices.subclassId " +
                                      "WHERE subclasses.name = @Subclass AND extraSubclassToolProficiencyChoices.level BETWEEN 1 AND @Level";
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        hasChoice = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasChoice;
        }

        /// <summary>
        /// gets a list of tool proficiencies a given subclass can choose from at a given level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public List<string> getToolProficiencyChoices(string subclass, int level)
        {
            List<string> tools = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT tools.name FROM tools " +
                                      "WHERE tools.type IN " +
                                      "(SELECT extraSubclassToolProficiencyChoices.name FROM extraSubclassToolProficiencyChoices " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = extraSubclassToolProficiencyChoices.subclassId " +
                                      "WHERE subclasses.name = @Subclass AND extraSubclassToolProficiencyChoices.level BETWEEN 1 AND @Level)";
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            tools.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return tools;
        }
    }
}
