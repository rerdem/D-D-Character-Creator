using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public class LanguageDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public LanguageDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// gets a list of all available languages
        /// </summary>
        public List<string> getLanguages()
        {
            List<string> languages = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT name FROM languages";
                
                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            languages.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return languages;
        }

        /// <summary>
        /// gets a list of languages of the specified type
        /// </summary>
        /// <param name="type">language type (Standard, Exotic, Class)</param>
        public List<string> getLanguages(string type)
        {
            List<string> languages = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT name FROM languages " +
                                      "WHERE type=@Type";
                command.Parameters.AddWithValue("@Type", type);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            languages.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return languages;
        }

        /// <summary>
        /// gets a list of default languages known by the chosen race
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        public List<string> getDefaultRaceLanguages(string subrace)
        {
            List<string> languages = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT languages.name FROM languages " +
                                      "INNER JOIN languageRaceDefaults ON languageRaceDefaults.languageId = languages.id " +
                                      "INNER JOIN races ON languageRaceDefaults.raceId = races.raceid " +
                                      "WHERE races.subrace=@Subrace";
                command.Parameters.AddWithValue("@Subrace", subrace);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            languages.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return languages;
        }

        /// <summary>
        /// gets a list of default languages known by the chosen class
        /// </summary>
        /// <param name="className">chosen class</param>
        public List<string> getDefaultClassLanguages(string className)
        {
            List<string> languages = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT languages.name FROM languages " +
                                      "INNER JOIN languageClassDefaults ON languageClassDefaults.languageId = languages.id " +
                                      "INNER JOIN classes ON languageClassDefaults.classId = classes.classid " +
                                      "WHERE classes.name=@Class";
                command.Parameters.AddWithValue("@Class", className);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            languages.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return languages;
        }

        /// <summary>
        /// gets a list of default languages known by the chosen subclass
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        public List<string> getDefaultSubclassLanguages(string subclass)
        {
            List<string> languages = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT languages.name FROM languages " +
                                      "INNER JOIN languageSubclassDefaults ON languageSubclassDefaults.languageId = languages.id " +
                                      "INNER JOIN subclasses ON languageSubclassDefaults.subclassId = subclasses.subclassId " +
                                      "WHERE subclasses.name=@Subclass";
                command.Parameters.AddWithValue("@Subclass", subclass);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            languages.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return languages;
        }

        /// <summary>
        /// gets information about the speakers of the chosen language
        /// </summary>
        /// <param name="language">chosen language</param>
        public string getLanguageSpeakers(string language)
        {
            string languageSpeakers = "";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT speakers FROM languages " +
                                      "WHERE name=@Language";
                command.Parameters.AddWithValue("@Language", language);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            languageSpeakers = dbReader.GetString(0);
                        }
                    }
                }
            }

            return languageSpeakers;
        }

        /// <summary>
        /// gets information about the script of the chosen language
        /// </summary>
        /// <param name="language">chosen language</param>
        public string getLanguageScript(string language)
        {
            string languageScript = "";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT script FROM languages " +
                                      "WHERE name=@Language";
                command.Parameters.AddWithValue("@Language", language);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            languageScript = dbReader.GetString(0);
                        }
                    }
                }
            }

            return languageScript;
        }

        /// <summary>
        /// gets the number of default languages gained from race, class and subclass
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        public int getDefaultLanguageCount(string subrace, string className, string subclass)
        {
            int count = getDefaultRaceLanguages(subrace).Count;
            count += getDefaultClassLanguages(className).Count;
            count += getDefaultSubclassLanguages(subclass).Count;
            return count;
        }

        /// <summary>
        /// gets the number of extra languages gained through race, class, subclass and background chocies
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="background">chosen background</param>
        public int getExtraLanguageCount(string subrace, string subclass, string background)
        {
            int languageCount = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                //query for subrace and background languages
                command.CommandText = "SELECT sum(extraLanguages) FROM " +
                                      "(SELECT extraLanguages FROM extraRaceLanguages " +
                                      "INNER JOIN races ON extraRaceLanguages.raceId = races.raceid " +
                                      "WHERE races.subrace=@Subrace " +
                                      "UNION ALL " +
                                      "SELECT extraLanguages FROM extraBackgroundLanguages " +
                                      "INNER JOIN backgrounds ON extraBackgroundLanguages.backgroundId = backgrounds.backgroundId " +
                                      "WHERE backgrounds.name=@Background";
                //subclass languages
                if (subclass != "---")
                {
                    command.CommandText += " UNION ALL " +
                                           "SELECT extraLanguages FROM extraSubclassLanguages " +
                                           "INNER JOIN subclasses ON extraSubclassLanguages.subclassId = subclasses.subclassId " +
                                           "WHERE subclasses.name=@Subclass";
                    command.Parameters.AddWithValue("@Subclass", subclass);
                }
                command.CommandText += ")";
                command.Parameters.AddWithValue("@Subrace", subrace);
                command.Parameters.AddWithValue("@Background", background);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            languageCount = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return languageCount;
        }
    }
}
