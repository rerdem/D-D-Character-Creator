using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement.ExtraSubclassManagers
{
    public class BeastCompanionDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public BeastCompanionDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// gets a list of beasts the given subclass can choose as a companion at a given level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public List<Beast> getBeastCompanions(string subclass, int level)
        {
            List<Beast> beasts = new List<Beast>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT rangerCompanions.name, books.title, rangerCompanions.page FROM rangerCompanions " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = rangerCompanions.subclassId " +
                                      "INNER JOIN books ON books.bookid=rangerCompanions.bookId " +
                                      "WHERE subclasses.name = @Subclass AND rangerCompanions.level BETWEEN 1 AND @Level";
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            beasts.Add(new Beast(dbReader.GetString(0), dbReader.GetString(1), dbReader.GetInt32(2)));
                        }
                    }
                }
            }

            return beasts;
        }
    }
}
