using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public class SubclassDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public SubclassDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// gets a list of all subclass options for the chosen class
        /// </summary>
        /// <param name="className">class for which to get subclasses</param>
        /// <param name="level">player level</param>
        public List<Subclass> getSubclasses(string className, int level)
        {
            List<Subclass> subclassList = new List<Subclass>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT subclasses.name, subclasses.description FROM subclasses " +
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
                            subclassList.Add(new Subclass(dbReader.GetString(0), dbReader.GetString(1)));
                        }
                    }
                }
            }

            if (subclassList.Count == 0)
            {
                subclassList.Add(new Subclass("---", string.Empty));
            }

            return subclassList;
        }

        /// <summary>
        /// checks, if a given subclass chooses additional skills or skills to which more modifiers are applied
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public bool hasSubclassSkillChoice(string subclass, int level)
        {
            bool hasSubclassSkillCoice = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM extraSubclassSkillChoice " +
                                      "INNER JOIN subclasses ON subclasses.subclassId=extraSubclassSkillChoice.subclassId " +
                                      "WHERE subclasses.name=@Subclass AND extraSubclassSkillChoice.level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        hasSubclassSkillCoice = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasSubclassSkillCoice;
        }

        /// <summary>
        /// checks, if a given subclass has a totem choice at the given level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public bool hasTotemFeatures(string subclass, int level)
        {
            bool hasTotemFeatures = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT COUNT(barbarianTotemFeatures.name) FROM barbarianTotemFeatures " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = barbarianTotemFeatures.subclassId " +
                                      "WHERE subclasses.name = @Subclass AND barbarianTotemFeatures.level BETWEEN 1 and @Level";
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            hasTotemFeatures = (dbReader.GetInt32(0) > 0);
                        }
                    }
                }
            }

            return hasTotemFeatures;
        }

        /// <summary>
        /// checks, iof a given subclass chooses additional spells
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        public bool hasExtraSubclassSpellChoice(string subclass)
        {
            bool hasSpellChoice = false;

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
                            hasSpellChoice = (dbReader.GetInt32(0) > 0);
                        }
                    }
                }
            }

            return hasSpellChoice;
        }

        /// <summary>
        /// checks, if a given subclass must choose additional tool proficiencies at a given level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public bool hasSubclassToolProficiencyChoice(string subclass, int level)
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
        /// checks, if a given subclass may choose maneuvers at the given level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public bool hasManeuvers(string subclass, int level)
        {
            bool hasManeuvers = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT amount FROM subclassManeuvers " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = subclassManeuvers.subclassId " +
                                      "WHERE subclasses.name = @Subclass AND subclassManeuvers.level BETWEEN 1 AND @Level " +
                                      "ORDER BY subclassManeuvers.level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            hasManeuvers = (dbReader.GetInt32(0) > 0);
                        }
                    }
                }
            }

            return hasManeuvers;
        }

        /// <summary>
        /// checks, if a given subclass has draconic ancestry
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        public bool hasDraconicAncestry(string subclass)
        {
            bool hasAncestry = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT dragon FROM draconicAncestry " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = draconicAncestry.subclassId " +
                                      "WHERE subclasses.name = @Subclass";
                command.Parameters.AddWithValue("@Subclass", subclass);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        hasAncestry = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasAncestry;
        }

        /// <summary>
        /// checks, if the given subclass may choose elemental disciplines at the given level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public bool hasDisciplines(string subclass, int level)
        {
            bool hasDisciplines = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT disciplinesKnown FROM elementalDisciplinesKnown " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = elementalDisciplinesKnown.subclassId " +
                                      "WHERE subclasses.name = @Subclass AND elementalDisciplinesKnown.level BETWEEN 1 AND @Level " +
                                      "ORDER BY elementalDisciplinesKnown.level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            hasDisciplines = (dbReader.GetInt32(0) > 0);
                        }
                    }
                }
            }

            return hasDisciplines;
        }

        /// <summary>
        /// checks, if a given subclass has a hunter choice at the given level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public bool hasHunterFeatures(string subclass, int level)
        {
            bool hasHunterFeatures = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT COUNT(hunterFeatures.name) FROM hunterFeatures  " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = hunterFeatures.subclassId " +
                                      "WHERE subclasses.name = @Subclass AND hunterFeatures.level BETWEEN 1 AND  @Level";
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            hasHunterFeatures = (dbReader.GetInt32(0) > 0);
                        }
                    }
                }
            }

            return hasHunterFeatures;
        }

        /// <summary>
        /// checks, if a given subclass must choose aa beast companion at a given level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public bool hasCompanion(string subclass, int level)
        {
            bool hasCompanion = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT rangerCompanions.name FROM rangerCompanions " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = rangerCompanions.subclassId " +
                                      "WHERE subclasses.name = @Subclass AND rangerCompanions.level BETWEEN 1 AND @Level";
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        hasCompanion = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasCompanion;
        }

        /// <summary>
        /// checks, if a given subclass has a terrain choice at the given level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public bool hasCircleTerrain(string subclass, int level)
        {
            bool hasCircleTerrain = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT druidCircleTerrains.terrain, spells.name FROM druidCircleTerrains " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = druidCircleTerrains.subclassId " +
                                      "INNER JOIN druidCircleTerrainSpells ON druidCircleTerrainSpells.terrainId = druidCircleTerrains.terrainId " +
                                      "INNER JOIN spells ON spells.spellId = druidCircleTerrainSpells.spellId " +
                                      "WHERE subclasses.name = @Subclass AND druidCircleTerrainSpells.level BETWEEN 1 AND @Level";
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        hasCircleTerrain = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasCircleTerrain;
        }
    }
}
