using Easy_DnD_Character_Creator.DataTypes;
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

        public SubclassDataManager SubclassData { get; }

        public ClassDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;

            SubclassData = new SubclassDataManager(inputConnectionString, inputUsedBooks);
        }

        public void setUsedBooks(List<string> inputUsedBooks)
        {
            UsedBooks = inputUsedBooks;
            SubclassData.UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// gets a list of all playable classes
        /// </summary>
        public List<CharacterClass> getClasses(int level)
        {
            List<CharacterClass> classList = new List<CharacterClass>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT name, description, dieType, extraToolProficiencies FROM classes " +
                                      "INNER JOIN hitDice ON hitDice.classId=classes.classid " +
                                      "INNER JOIN books ON classes.book=books.bookid " +
                                      "WHERE books.title IN (@UsedBooks)";
                SQLiteCommandExtensions.AddParametersWithValues(command, "@UsedBooks", UsedBooks);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            CharacterClass newClass = new CharacterClass(dbReader.GetString(0), dbReader.GetString(1), dbReader.GetString(2), dbReader.GetBoolean(3), getProficiencyChoiceAmount(dbReader.GetString(0)));

                            //add subclasses
                            newClass.Subclasses = SubclassData.getSubclasses(newClass.Name, level);

                            //set bools
                            newClass.HasFightingStyle = hasFightingStyle(newClass.Name, level);
                            newClass.HasFavoredEnemy = hasFavoredEnemy(newClass.Name, level);
                            newClass.HasFavoredTerrain = hasFavoredEnemy(newClass.Name, level);
                            newClass.HasExtraSkills = hasClassSkillChoice(newClass.Name, level);
                            newClass.HasWarlockPact = hasWarlockPact(newClass.Name, level);
                            newClass.HasEldritchInvocations = hasEldritchInvocations(newClass.Name, level);
                            newClass.HasMetamagic = hasMetamagic(newClass.Name, level);
                            newClass.HasWildShape = hasWildShape(newClass.Name);

                            classList.Add(newClass);
                        }
                    }
                }
            }

            return classList;
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
        public int getProficiencyChoiceAmount(string className)
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

        /// <summary>
        /// checks, if the chosen class may choose a fighting style at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool hasFightingStyle(string className, int level)
        {
            bool hasFightingStyle = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM fightingStyleGain " +
                                      "INNER JOIN classes ON fightingStyleGain.classId=classes.classid " +
                                      "WHERE classes.name=@Class AND level BETWEEN 1 AND @Level";
                command.Parameters.AddWithValue("@Class", className);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        hasFightingStyle = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasFightingStyle;
        }

        /// <summary>
        /// checks, if a given class may choose a favored enemy at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool hasFavoredEnemy(string className, int level)
        {
            bool hasFavoredEnemy = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT amount FROM favoredEnemyAmount " +
                                      "INNER JOIN classes ON classes.classid=favoredEnemyAmount.classId " +
                                      "WHERE classes.name=@Class AND favoredEnemyAmount.level BETWEEN 1 AND @Level " +
                                      "ORDER BY amount DESC LIMIT 1";
                command.Parameters.AddWithValue("@Class", className);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        hasFavoredEnemy = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasFavoredEnemy;
        }

        /// <summary>
        /// checks, if a given class may choose a favored terrain at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool hasFavoredTerrain(string className, int level)
        {
            bool hasFavoredTerrain = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT amount FROM favoredTerrainAmount " +
                                      "INNER JOIN classes ON classes.classid=favoredTerrainAmount.classId " +
                                      "WHERE classes.name=@Class AND favoredTerrainAmount.level BETWEEN 1 AND @Level " +
                                      "ORDER BY amount DESC LIMIT 1";
                command.Parameters.AddWithValue("@Class", className);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        hasFavoredTerrain = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasFavoredTerrain;
        }

        /// <summary>
        /// checks, if a given class chooses additional skills or skills to which more modifiers are applied
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool hasClassSkillChoice(string className, int level)
        {
            bool hasClassSkillCoice = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM extraClassSkillChoice " +
                                      "INNER JOIN classes ON classes.classid=extraClassSkillChoice.classId " +
                                      "WHERE classes.name=@Class AND extraClassSkillChoice.level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Class", className);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        hasClassSkillCoice = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasClassSkillCoice;
        }

        /// <summary>
        /// checks, if a given class may choose a warlock pact at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool hasWarlockPact(string className, int level)
        {
            bool hasWarlockPact = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT hasWarlockPact FROM warlockFeatures " +
                                      "INNER JOIN classes ON classes.classid=warlockFeatures.classId " +
                                      "WHERE classes.name=@Class AND level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Class", className);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            hasWarlockPact = dbReader.GetBoolean(0);
                        }
                    }
                }
            }

            return hasWarlockPact;
        }

        /// <summary>
        /// checks, if a given class may choose eldritch invocations at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool hasEldritchInvocations(string className, int level)
        {
            bool hasEldritchInvocations = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT hasEldritchInvocations FROM warlockFeatures " +
                                      "INNER JOIN classes ON classes.classid=warlockFeatures.classId " +
                                      "WHERE classes.name=@Class AND level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Class", className);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            hasEldritchInvocations = dbReader.GetBoolean(0);
                        }
                    }
                }
            }

            return hasEldritchInvocations;
        }

        /// <summary>
        /// checks, if a given class has access to metamagic at a given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool hasMetamagic(string className, int level)
        {
            bool hasMetamagic = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT metamagicAmount FROM sorcererFeatures " +
                                      "INNER JOIN classes ON classes.classid=sorcererFeatures.classId " +
                                      "WHERE classes.name=@Class AND level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Class", className);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            hasMetamagic = (dbReader.GetInt32(0) > 0);
                        }
                    }
                }
            }

            return hasMetamagic;
        }

        /// <summary>
        /// checks, if a given class has the ability to wild shape
        /// </summary>
        /// <param name="className">chosen class</param>
        public bool hasWildShape(string className)
        {
            bool hasWildShape = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT terrain FROM wildShapeTerrains " +
                                      "INNER JOIN classes ON classes.classid=wildShapeTerrains.classId " +
                                      "WHERE classes.name=@Class";
                command.Parameters.AddWithValue("@Class", className);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        hasWildShape = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasWildShape;
        }

    }
}
