using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement.ExtraSubclassManagers
{
    public class ExtraSubclassSpellDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public ExtraSubclassSpellDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// gets the amount of spells a given subclass may choose
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        public int extraSpellChoiceAmount(string subclass)
        {
            int amount = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT amount FROM extraSubclassCantrips " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = extraSubclassCantrips.subclassId " +
                                      "WHERE subclasses.name = @Subclass";
                command.Parameters.AddWithValue("@Subclass", subclass);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            amount = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return amount;
        }

        /// <summary>
        /// gets a list of the additional cantrips the given subclass may choose from
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        public List<Spell> getExtraSubclassCantripOptions(string subclass)
        {
            List<Spell> cantripList = new List<Spell>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT spells.name, spells.ritual, spells.level, spells.school, spells.castTime, spells.range, " +
                                      "spells.duration, spells.components, spells.materials, spells.description FROM spells " +
                                      "INNER JOIN books ON spells.book = books.bookid " +
                                      "WHERE spells.level = 0 " +
                                      "AND spells.classes LIKE \"%\" || " +
                                      "(SELECT classes.name FROM extraSubclassCantrips " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = extraSubclassCantrips.subclassId " +
                                      "INNER JOIN classes ON classes.classid = extraSubclassCantrips.classId " +
                                      "WHERE subclasses.name = @Subclass) || \"%\" " +
                                      "AND books.title IN(@UsedBooks)";
                command.Parameters.AddWithValue("@Subclass", subclass);
                SQLiteCommandExtensions.AddParametersWithValues(command, "@UsedBooks", UsedBooks);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            cantripList.Add(new Spell(dbReader.GetString(0), dbReader.GetBoolean(1), dbReader.GetInt32(2), dbReader.GetString(3), dbReader.GetString(4), dbReader.GetString(5), dbReader.GetString(6), dbReader.GetString(7), dbReader.GetString(8), dbReader.GetString(9), false));
                        }
                    }
                }
            }

            return cantripList;
        }
    }
}
