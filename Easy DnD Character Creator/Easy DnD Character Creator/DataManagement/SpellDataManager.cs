﻿using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public class SpellDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public SpellDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// checks whether a given class/subclass has the ability to cast spells
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public bool hasSpellcasting(string className, string subclass, int level)
        {
            bool hasSpellcasting = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT SUM(hasSpellcasting) FROM " +
                                      "(SELECT hasSpellcasting FROM classes " +
                                      "WHERE name=@Class AND spellcastingStartLevel BETWEEN 1 AND @Level " +
                                      "UNION ALL " +
                                      "SELECT hasSpellcasting FROM subclasses " +
                                      "WHERE name=@Subclass)";
                command.Parameters.AddWithValue("@Class", className);
                command.Parameters.AddWithValue("@Level", level.ToString());
                command.Parameters.AddWithValue("@Subclass", subclass);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            hasSpellcasting = (dbReader.GetInt32(0) > 0);
                        }
                    }
                }
            }

            return hasSpellcasting;
        }

        /// <summary>
        /// checks whether a given class/subclass has spellcasting and chooses spells to learn
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public bool choosesSpells(string className, string subclass, int level)
        {
            return (hasSpellcasting(className, subclass, level) && (className != "Paladin"));
        }

        /// <summary>
        /// gets the amount of cantrips the character can know at the current level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public int getCantripsKnown(string className, string subclass, int level)
        {
            int cantripsKnown = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT cantripsKnown FROM cantripsKnown " +
                                          "INNER JOIN classes ON cantripsKnown.classId = classes.classid " +
                                          "WHERE classes.name=@Class AND cantripsKnown.level BETWEEN 1 AND @Level " +
                                          "ORDER BY cantripsKnown.level DESC LIMIT 1";
                    command.Parameters.AddWithValue("@Class", className);
                    command.Parameters.AddWithValue("@Level", level.ToString());

                    using (SQLiteDataReader dbReader = command.ExecuteReader())
                    {
                        if (dbReader.Read())
                        {
                            if (!dbReader.IsDBNull(0))
                            {
                                cantripsKnown += dbReader.GetInt32(0);
                            }
                        }
                    }
                }
                if (subclass != "---")
                { 
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "SELECT cantripsKnown FROM cantripsKnownSubclass " +
                                              "INNER JOIN subclasses ON cantripsKnownSubclass.subclassId = subclasses.subclassId " +
                                              "WHERE subclasses.name=@Subclass AND cantripsKnownSubclass.level BETWEEN 1 AND @Level " +
                                              "ORDER BY cantripsKnownSubclass.level DESC LIMIT 1";
                        command.Parameters.AddWithValue("@Subclass", subclass);
                        command.Parameters.AddWithValue("@Level", level.ToString());

                        using (SQLiteDataReader dbReader = command.ExecuteReader())
                        {
                            if (dbReader.Read())
                            {
                                if (!dbReader.IsDBNull(0))
                                {
                                    cantripsKnown += dbReader.GetInt32(0);
                                }
                            }
                        }
                    }
                }
            }

            return cantripsKnown;
        }

        /// <summary>
        /// checks, of the number of known spells is static with level or dependent on other modifiers
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        public bool areSpellsKnownStatic(string className, string subclass)
        {
            bool classSpellsKnownStatic = false;
            bool subclassSpellsKnownStatic = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT MAX(static) FROM spellsKnown " +
                                          "INNER JOIN classes ON spellsKnown.classId = classes.classid " +
                                          "WHERE classes.name=@Class";
                    command.Parameters.AddWithValue("@Class", className);

                    using (SQLiteDataReader dbReader = command.ExecuteReader())
                    {
                        if (dbReader.Read())
                        {
                            if (!dbReader.IsDBNull(0))
                            {
                                classSpellsKnownStatic = dbReader.GetBoolean(0);
                            }
                        }
                    }
                }
                if (subclass != "---")
                {
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "SELECT MAX(static) FROM spellsKnownSubclass " +
                                              "INNER JOIN subclasses ON spellsKnownSubclass.subclassId = subclasses.subclassId " +
                                              "WHERE subclasses.name=@Subclass";
                        command.Parameters.AddWithValue("@Subclass", subclass);

                        using (SQLiteDataReader dbReader = command.ExecuteReader())
                        {
                            if (dbReader.Read())
                            {
                                if (!dbReader.IsDBNull(0))
                                {
                                    subclassSpellsKnownStatic = dbReader.GetBoolean(0);
                                }
                            }
                        }
                    }
                }
            }

            return classSpellsKnownStatic || subclassSpellsKnownStatic;
        }

        /// <summary>
        /// gets the amount of spells the character can know at the current level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public int getSpellsKnown(string className, string subclass, int level)
        {
            int spellsKnown = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT spellsKnown FROM spellsKnown " +
                                          "INNER JOIN classes ON spellsKnown.classId = classes.classid " +
                                          "WHERE classes.name=@Class AND spellsKnown.level BETWEEN 1 AND @Level " +
                                          "ORDER BY spellsKnown.level DESC LIMIT 1";
                    command.Parameters.AddWithValue("@Class", className);
                    command.Parameters.AddWithValue("@Level", level.ToString());

                    using (SQLiteDataReader dbReader = command.ExecuteReader())
                    {
                        if (dbReader.Read())
                        {
                            if (!dbReader.IsDBNull(0))
                            {
                                spellsKnown += dbReader.GetInt32(0);
                            }
                        }
                    }
                }
                if (subclass != "---")
                {
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "SELECT spellsKnown FROM spellsKnownSubclass " +
                                              "INNER JOIN subclasses ON spellsKnownSubclass.subclassId = subclasses.subclassId " +
                                              "WHERE subclasses.name=@Subclass AND spellsKnownSubclass.level BETWEEN 1 AND @Level " +
                                              "ORDER BY spellsKnownSubclass.level DESC LIMIT 1";
                        command.Parameters.AddWithValue("@Subclass", subclass);
                        command.Parameters.AddWithValue("@Level", level.ToString());

                        using (SQLiteDataReader dbReader = command.ExecuteReader())
                        {
                            if (dbReader.Read())
                            {
                                if (!dbReader.IsDBNull(0))
                                {
                                    spellsKnown += dbReader.GetInt32(0);
                                }
                            }
                        }
                    }
                }
            }

            return spellsKnown;
        }

        /// <summary>
        /// gets a list of cantrips the character can choose from
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        public List<Spell> getCantripOptions(string className, string subclass)
        {
            List<Spell> cantripList = new List<Spell>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM spells " +
                                      "INNER JOIN books ON books.bookid=spells.book " +
                                      "WHERE spells.level=0 " +
                                      "AND spells.classes LIKE @Class " +
                                      "AND books.title IN (@UsedBooks)";
                if ((subclass == "Arcane Trickster") || (subclass == "Eldritch Knight"))
                {
                    command.Parameters.AddWithValue("@Class", "%" + subclass + "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Class", "%" + className + "%");
                }
                SQLiteCommandExtensions.AddParametersWithValues(command, "@UsedBooks", UsedBooks);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            cantripList.Add(new Spell(dbReader.GetString(1), dbReader.GetBoolean(2), dbReader.GetInt32(3), dbReader.GetString(4), dbReader.GetString(5), dbReader.GetString(6), dbReader.GetString(7), dbReader.GetString(8), dbReader.GetString(9), dbReader.GetString(10), false));
                        }
                    }
                }
            }

            return cantripList;
        }

        /// <summary>
        /// gets a list of spells the character can choose from
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public List<Spell> getSpellOptions(string className, string subclass, int level)
        {
            List<Spell> spellList = new List<Spell>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM spells " +
                                      "INNER JOIN books ON books.bookid=spells.book " +
                                      "WHERE spells.level = " +
                                      "(SELECT maxSpellLevel FROM maxSpellLevel " +
                                      "INNER JOIN classes ON maxSpellLevel.classId = classes.classid " +
                                      "WHERE classes.name=@Class AND maxSpellLevel.level BETWEEN 1 AND @Level " +
                                      "UNION " +
                                      "SELECT maxSpellLevel FROM maxSpellLevelSubclass " +
                                      "INNER JOIN subclasses ON maxSpellLevelSubclass.subclassId = subclasses.subclassId " +
                                      "WHERE subclasses.name=@Subclass AND maxSpellLevelSubclass.level BETWEEN 1 AND @Level " +
                                      "ORDER BY maxSpellLevel DESC LIMIT 1) " +
                                      "AND(spells.classes LIKE @LikeClass " +
                                      "OR spells.classes LIKE @LikeSubclass) " +
                                      "AND books.title IN (@UsedBooks)";
                command.Parameters.AddWithValue("@Class", className);
                command.Parameters.AddWithValue("@LikeClass", "%" + className + "%");
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@LikeSubclass", "%" + subclass + "%");
                command.Parameters.AddWithValue("@Level", level.ToString());
                SQLiteCommandExtensions.AddParametersWithValues(command, "@UsedBooks", UsedBooks);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            spellList.Add(new Spell(dbReader.GetString(1), dbReader.GetBoolean(2), dbReader.GetInt32(3), dbReader.GetString(4), dbReader.GetString(5), dbReader.GetString(6), dbReader.GetString(7), dbReader.GetString(8), dbReader.GetString(9), dbReader.GetString(10), false));
                        }
                    }
                }
            }

            return spellList;
        }

        /// <summary>
        /// gets all information for a spell given its name
        /// </summary>
        /// <param name="spellName">name of the spell to look up</param>
        public Spell getSpell(string spellName)
        {
            Spell outputSpell = new Spell(); ;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM spells " +
                                      "WHERE name=@Spell";
                command.Parameters.AddWithValue("@Spell", spellName);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            outputSpell = new Spell(dbReader.GetString(1), dbReader.GetBoolean(2), dbReader.GetInt32(3), dbReader.GetString(4), dbReader.GetString(5), dbReader.GetString(6), dbReader.GetString(7), dbReader.GetString(8), dbReader.GetString(9), dbReader.GetString(10), false);
                        }
                    }
                }
            }

            return outputSpell;
        }

        /// <summary>
        /// checks, if the selected subrace gains extra spells
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="level">current level</param>
        public bool hasExtraRaceSpells(string subrace, int level)
        {
            bool hasExtraRaceSpells = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM extraRaceSpells " +
                                      "INNER JOIN races ON extraRaceSpells.raceId=races.raceid " +
                                      "WHERE races.subrace=@Subrace AND extraRaceSpells.level BETWEEN 1 AND @Level";
                command.Parameters.AddWithValue("@Subrace", subrace);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        hasExtraRaceSpells = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasExtraRaceSpells;
        }

        /// <summary>
        /// checks, if the selected subrace gains extra spells
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public bool hasExtraSubclassSpells(string subclass, int level)
        {
            bool hasExtraSubclassSpells = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM extraSubclassSpells " +
                                      "INNER JOIN subclasses ON extraSubclassSpells.subclassId=subclasses.subclassId " +
                                      "WHERE subclasses.name=@Subclass AND extraSubclassSpells.level BETWEEN 1 AND @Level";
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        hasExtraSubclassSpells = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasExtraSubclassSpells;
        }

        /// <summary>
        /// gets a list of spells that are gained through the chosen subrace
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="level">current level</param>
        public List<Spell> getExtraRaceSpells(string subrace, int level)
        {
            List<Spell> extraRaceSpells = new List<Spell>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT spells.name, spells.ritual, spells.level, spells.school, spells.castTime, spells.range, spells.duration, " +
                                      "spells.components, spells.materials, spells.description, extraRaceSpells.maxSpellLevel, abilities.name " +
                                      "FROM extraRaceSpells " +
                                      "INNER JOIN races ON extraRaceSpells.raceId=races.raceid " +
                                      "INNER JOIN spells ON extraRaceSpells.spellId=spells.spellId " +
                                      "INNER JOIN abilities ON extraRaceSpells.spellcastingAbility=abilities.abilityId " +
                                      "WHERE races.subrace=@Subrace AND extraRaceSpells.level BETWEEN 1 AND @Level";
                command.Parameters.AddWithValue("@Subrace", subrace);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            extraRaceSpells.Add(new ExtraRaceSpell(dbReader.GetString(0), dbReader.GetBoolean(1), dbReader.GetInt32(2), dbReader.GetString(3), dbReader.GetString(4), dbReader.GetString(5), dbReader.GetString(6), dbReader.GetString(7), dbReader.GetString(8), dbReader.GetString(9), true, dbReader.GetInt32(10), dbReader.GetString(11)));
                        }
                    }
                }
            }

            return extraRaceSpells;
        }

        /// <summary>
        /// gets a list of spells that are gained through the chosen subclass
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public List<Spell> getExtraSubclassSpells(string subclass, int level)
        {
            List<Spell> extraSubclassSpells = new List<Spell>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT spells.* FROM extraSubclassSpells " +
                                      "INNER JOIN subclasses ON extraSubclassSpells.subclassId=subclasses.subclassId " +
                                      "INNER JOIN spells ON extraSubclassSpells.spellId=spells.spellId " +
                                      "WHERE subclasses.name=@Subclass AND extraSubclassSpells.level BETWEEN 1 AND @Level";
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            extraSubclassSpells.Add(new Spell(dbReader.GetString(1), dbReader.GetBoolean(2), dbReader.GetInt32(3), dbReader.GetString(4), dbReader.GetString(5), dbReader.GetString(6), dbReader.GetString(7), dbReader.GetString(8), dbReader.GetString(9), dbReader.GetString(10), true));
                        }
                    }
                }
            }

            return extraSubclassSpells;
        }

        ///// <summary>
        ///// checks, if a given spell was gained through choice of subrace
        ///// </summary>
        ///// <param name="subrace">chosen subrace</param>
        ///// <param name="level">current level</param>
        ///// <param name="spellName">name of the spell to checl</param>
        //public bool isExtraRaceSpell(string subrace, int level, string spellName)
        //{
        //    bool isExtraRaceSpell = false;

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        connection.Open();
        //        command.CommandText = "SELECT spells.name FROM extraRaceSpells " +
        //                              "INNER JOIN races ON extraRaceSpells.raceId=races.raceid " +
        //                              "INNER JOIN spells ON extraRaceSpells.spellId=spells.spellId " +
        //                              "WHERE races.subrace=@Subrace AND extraRaceSpells.level BETWEEN 1 AND @Level " +
        //                              "AND spells.name=@Spell";
        //        command.Parameters.AddWithValue("@Subrace", subrace);
        //        command.Parameters.AddWithValue("@Level", level.ToString());
        //        command.Parameters.AddWithValue("@Spell", spellName);

        //        using (SQLiteDataReader dbReader = command.ExecuteReader())
        //        {
        //            if (dbReader.Read())
        //            {
        //                isExtraRaceSpell = !dbReader.IsDBNull(0);
        //            }
        //        }
        //    }

        //    return isExtraRaceSpell;
        //}

        ///// <summary>
        ///// checks, if a given spell was gained through the choice of subclass
        ///// </summary>
        ///// <param name="subclass">given subclass</param>
        ///// <param name="level">current level</param>
        ///// <param name="spellName">name of spell to check</param>
        //public bool isExtraSubclassSpell(string subclass, int level, string spellName)
        //{
        //    bool isExtraSubclassSpell = false;

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        connection.Open();
        //        command.CommandText = "SELECT spells.* FROM extraSubclassSpells " +
        //                              "INNER JOIN subclasses ON extraSubclassSpells.subclassId=subclasses.subclassId " +
        //                              "INNER JOIN spells ON extraSubclassSpells.spellId=spells.spellId " +
        //                              "WHERE subclasses.name=@Subclass AND extraSubclassSpells.level BETWEEN 1 AND @Level " +
        //                              "AND spells.name=@Spell";
        //        command.Parameters.AddWithValue("@Subclass", subclass);
        //        command.Parameters.AddWithValue("@Level", level.ToString());
        //        command.Parameters.AddWithValue("@Spell", spellName);

        //        using (SQLiteDataReader dbReader = command.ExecuteReader())
        //        {
        //            if (dbReader.Read())
        //            {
        //                isExtraSubclassSpell = !dbReader.IsDBNull(0);
        //            }
        //        }
        //    }

        //    return isExtraSubclassSpell;
        //}

        /// <summary>
        /// checks, if the given subclass has limitations on which spell school to choose from
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        public bool hasSubclassSpellSchoolLimitations(string subclass)
        {
            bool hasSubclassSpellSchoolLimitations = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT school FROM subclassSpellSchoolLimitations " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = subclassSpellSchoolLimitations.subclassId " +
                                      "WHERE subclasses.name=@Subclass";
                command.Parameters.AddWithValue("@Subclass", subclass);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        hasSubclassSpellSchoolLimitations = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasSubclassSpellSchoolLimitations;
        }

        /// <summary>
        /// gets a list of spell schools the given subclass is restricted to
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        public List<string> getSubclassSpellSchoolLimitations(string subclass)
        {
            List<string> subclassSpellSchoolLimitations = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT school FROM subclassSpellSchoolLimitations " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = subclassSpellSchoolLimitations.subclassId " +
                                      "WHERE subclasses.name=@Subclass";
                command.Parameters.AddWithValue("@Subclass", subclass);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            subclassSpellSchoolLimitations.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return subclassSpellSchoolLimitations;
        }

        /// <summary>
        /// gets the number of exceptions from the spell school limitations dependend on level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public int getSubclassSpellSchoolLimitationExceptions(string subclass, int level)
        {
            int exceptions = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM subclassSpellSchoolLimitationExceptions " +
                                      "INNER JOIN subclasses ON subclasses.subclassId=subclassSpellSchoolLimitationExceptions.subclassId " +
                                      "WHERE subclasses.name=@Subclass AND subclassSpellSchoolLimitationExceptions.level BETWEEN 1 AND @Level";
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            exceptions++;
                        }
                    }
                }
            }

            return exceptions;
        }

        /// <summary>
        /// gets the spellcasting ability for a given subrace
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <returns>the spellcasting ability of a subrace or an empty string, if there is none</returns>
        public string getSpellcastingAbility(string subrace)
        {
            string ability = "";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT abilities.name FROM raceSpellcastingAbility " +
                                      "INNER JOIN races ON raceSpellcastingAbility.raceId=races.raceid " +
                                      "INNER JOIN abilities ON raceSpellcastingAbility.abilityId=abilities.abilityId " +
                                      "WHERE races.subrace=@Subrace";
                command.Parameters.AddWithValue("@Subrace", subrace);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            ability = dbReader.GetString(0);
                        }
                    }
                }
            }

            return ability;
        }

        /// <summary>
        /// gets the spellcasting ability for a given class/subclass
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        /// <returns>the spellcasting ability of a class/subclass or an empty string, if there is none</returns>
        public string getSpellcastingAbility(string className, string subclass)
        {
            string ability = "";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT abilities.name FROM classSpellcastingAbility " +
                                      "INNER JOIN classes ON classSpellcastingAbility.classId=classes.classid " +
                                      "INNER JOIN abilities ON classSpellcastingAbility.abilityId=abilities.abilityId " +
                                      "WHERE classes.name=@Class " +
                                      "UNION " +
                                      "SELECT abilities.name FROM subclassSpellcastingAbility " +
                                      "INNER JOIN subclasses ON subclassSpellcastingAbility.subclassId=subclasses.subclassId " +
                                      "INNER JOIN abilities ON subclassSpellcastingAbility.abilityId=abilities.abilityId " +
                                      "WHERE subclasses.name=@Subclass";
                command.Parameters.AddWithValue("@Class", className);
                command.Parameters.AddWithValue("@Subclass", subclass);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            ability = dbReader.GetString(0);
                        }
                    }
                }
            }

            return ability;
        }

        /// <summary>
        /// gets the list of bonus spells a subclass gains
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        public List<BonusSpell> getSubclasBonusSpells(string subclass)
        {
            List<BonusSpell> bonusSpells = new List<BonusSpell>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT spells.name, spells.ritual, spells.level, spells.school, spells.castTime, " +
                                      "spells.range, spells.duration, spells.components, spells.materials, spells.description, subclassBonusSpells.onlyRitual " +
                                      "FROM subclassBonusSpells " +
                                      "INNER JOIN subclasses ON subclassBonusSpells.subclassId = subclasses.subclassId " +
                                      "INNER JOIN spells ON subclassBonusSpells.spellId = spells.spellId " +
                                      "WHERE subclasses.name = @Subclass";
                command.Parameters.AddWithValue("@Subclass", subclass);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            bonusSpells.Add(new BonusSpell(dbReader.GetString(0), dbReader.GetBoolean(1), dbReader.GetInt32(2), dbReader.GetString(3), dbReader.GetString(4), dbReader.GetString(5), dbReader.GetString(6), dbReader.GetString(7), dbReader.GetString(8), dbReader.GetString(9), true, dbReader.GetBoolean(10)));
                        }
                    }
                }
            }

            return bonusSpells;
        }

        /// <summary>
        /// gets the number of spell slots of a given spell level for a class at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="characterLevel">current level</param>
        /// <param name="spellLevel">spell level</param>
        /// <returns></returns>
        private int getClassSpellSlots(string className, int characterLevel, int spellLevel)
        {
            int slots = 0;

            if ((spellLevel > 0) && (spellLevel < 10))
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    connection.Open();
                    command.CommandText = $"SELECT level{spellLevel} FROM classSpellSlots " +
                                          "INNER JOIN classes ON classSpellSlots.classId=classes.classid " +
                                          "WHERE classes.name==@Class AND level=@Level";
                    command.Parameters.AddWithValue("@Class", className);
                    command.Parameters.AddWithValue("@Level", characterLevel.ToString());

                    using (SQLiteDataReader dbReader = command.ExecuteReader())
                    {
                        if (dbReader.Read())
                        {
                            if (!dbReader.IsDBNull(0))
                            {
                                slots = dbReader.GetInt32(0);
                            }
                        }
                    }
                }
            }

            return slots;
        }

        /// <summary>
        /// gets the number of spell slots of a given spell level for a subclass
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="characterLevel">current level</param>
        /// <param name="spellLevel">spell level</param>
        /// <returns></returns>
        private int getSubclassSpellSlots(string subclass, int characterLevel, int spellLevel)
        {
            int slots = 0;

            if ((spellLevel > 0) && (spellLevel < 10))
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    connection.Open();
                    command.CommandText = $"SELECT level{spellLevel} FROM subclassSpellSlots " +
                                          "INNER JOIN subclasses ON subclassSpellSlots.subclassId=subclasses.subclassId " +
                                          "WHERE subclasses.name==@Subclass AND level=@Level";
                    command.Parameters.AddWithValue("@Subclass", subclass);
                    command.Parameters.AddWithValue("@Level", characterLevel.ToString());

                    using (SQLiteDataReader dbReader = command.ExecuteReader())
                    {
                        if (dbReader.Read())
                        {
                            if (!dbReader.IsDBNull(0))
                            {
                                slots = dbReader.GetInt32(0);
                            }
                        }
                    }
                }
            }

            return slots;
        }

        /// <summary>
        /// gets the number of spell slots of a given spell level for a class/subclass
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="characterLevel">current level</param>
        /// <param name="spellLevel">spell level</param>
        /// <returns></returns>
        public int getSpellSlots(string className, string subclass, int characterLevel, int spellLevel)
        {
            return Math.Max(getClassSpellSlots(className, characterLevel, spellLevel), getSubclassSpellSlots(subclass, characterLevel, spellLevel));
        }



    }
}
