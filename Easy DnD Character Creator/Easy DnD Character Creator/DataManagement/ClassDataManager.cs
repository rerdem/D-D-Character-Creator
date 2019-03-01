using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public class ClassDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public ClassDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// gets a list of all playable classes
        /// </summary>
        public List<string> getClasses()
        {
            List<string> classList = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT name FROM classes " +
                                      "INNER JOIN books ON classes.book=books.bookid " +
                                      "WHERE books.title IN (@UsedBooks)";
                SQLiteCommandExtensions.AddParametersWithValues(command, "@UsedBooks", UsedBooks);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            classList.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return classList;
        }

        /// <summary>
        /// gets a list of all subclass options for the chosen class
        /// </summary>
        /// <param name="className">class for which to get subclasses</param>
        /// <param name="level">player level</param>
        public List<string> getSubclasses(string className, int level)
        {
            List<string> subclassList = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT subclasses.name FROM subclasses " +
                                      "INNER JOIN books ON subclasses.book=books.bookid " +
                                      "INNER JOIN classes ON subclasses.parentclass=classes.classid " +
                                      "WHERE books.title IN (@UsedBooks) AND classes.name=@Class " +
                                      "AND classes.subclasslevel<=@Level";
                command.Parameters.AddWithValue("@Class", className);
                command.Parameters.AddWithValue("@Level", level.ToString());
                SQLiteCommandExtensions.AddParametersWithValues(command, "@UsedBooks", UsedBooks);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            subclassList.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            if (subclassList.Count == 0)
            {
                subclassList.Add("---");
            }

            return subclassList;
        }

        /// <summary>
        /// gets the description for the chosen class
        /// </summary>
        /// <param name="className">chosen class</param>
        public string getClassDescription(string className)
        {
            string description = "";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT description FROM classes " +
                                      "INNER JOIN books ON classes.book=books.bookid " +
                                      "WHERE books.title IN (@UsedBooks) AND name=@Class";
                command.Parameters.AddWithValue("@Class", className);
                SQLiteCommandExtensions.AddParametersWithValues(command, "@UsedBooks", UsedBooks);

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
        /// gets the description for the chosen subclass
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        public string getSubclassDescription(string subclass)
        {
            string description = "";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT description FROM subclasses " +
                                      "INNER JOIN books ON subclasses.book=books.bookid " +
                                      "WHERE books.title IN (@UsedBooks) AND name=@Subclass";
                command.Parameters.AddWithValue("@Subclass", subclass);
                SQLiteCommandExtensions.AddParametersWithValues(command, "@UsedBooks", UsedBooks);

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
        /// checks, if the chosen class has extra tool proficiencies
        /// </summary>
        /// <param name="className">chosen class</param>
        public bool classHasExtraChoice(string className)
        {
            bool hasChoice = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT extraToolProficiencies FROM classes " +
                                      "WHERE name=@Class";
                command.Parameters.AddWithValue("@Class", className);

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
        /// gets a list of the extra proficiencies the class can choose from
        /// </summary>
        /// <param name="className">chosen class</param>
        public List<string> getExtraClassProficiencies(string className)
        {
            List<string> proficiencyList = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT tools.name FROM tools " +
                                      "INNER JOIN extraClassToolProficiencyChoices ON extraClassToolProficiencyChoices.type=tools.type " +
                                      "INNER JOIN classes ON extraClassToolProficiencyChoices.classId=classes.classid " +
                                      "WHERE classes.name=@Class " +
                                      "ORDER BY tools.name";
                command.Parameters.AddWithValue("@Class", className);
                
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
        /// gets the amount of proficiencies the class is allowed to choose
        /// </summary>
        /// <param name="className">chosen class</param>
        public int getExtraClassProficiencyAmount(string className)
        {
            int amount = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT amount FROM extraClassToolProficiencyAmount " +
                                      "INNER JOIN classes ON extraClassToolProficiencyAmount.classId=classes.classid " +
                                      "WHERE classes.name=@Class";
                command.Parameters.AddWithValue("@Class", className);

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

    }
}
