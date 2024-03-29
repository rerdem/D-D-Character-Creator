﻿using Easy_DnD_Character_Creator.DataTypes;
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
        public List<Language> getLanguages()
        {
            List<Language> languages = new List<Language>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT name, type, speakers, script FROM languages";

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            languages.Add(new Language(dbReader.GetString(0), dbReader.GetString(1), dbReader.GetString(2), dbReader.GetString(3)));
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
        /// gets a list of default languages known by the chosen race, class and subclass
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        public List<string> getDefaultLanguages(string subrace, string className, string subclass)
        {
            List<string> defaultLanguages = getDefaultRaceLanguages(subrace);
            defaultLanguages.AddRange(getDefaultClassLanguages(className));
            defaultLanguages.AddRange(getDefaultSubclassLanguages(subclass));
            return defaultLanguages;
        }

        /// <summary>
        /// gets the number of default languages gained from subrace, class and subclass
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
        /// gets the number of extra languages gained through subrace, class, subclass and background choices
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

        /// <summary>
        /// checks, if the subrace, class, subclass and background may choose additional languages
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="background">chosen background</param>
        public bool hasExtraLanguages(string subrace, string subclass, string background)
        {
            return (getExtraLanguageCount(subrace, subclass, background) > 0);
        }
    }
}
