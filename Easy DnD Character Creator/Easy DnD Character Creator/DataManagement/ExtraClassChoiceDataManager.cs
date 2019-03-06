using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public class ExtraClassChoiceDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public ExtraClassChoiceDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        public bool hasExtraClassChoices(string className, int level)
        {
            return hasFightingStyle(className, level) || hasFavoredEnemy(className, level) || hasFavoredTerrain(className, level);
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
        /// gets a list of fighting styles the class can choose from at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public List<FightingStyle> getFightingStyles(string className, int level)
        {
            List<FightingStyle> fightingStyles = new List<FightingStyle>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT fightingStyles.name from fightingStyleGain " +
                                      "INNER JOIN classes ON fightingStyleGain.classId=classes.classid " +
                                      "INNER JOIN fightingStyles ON fightingStyleGain.styleId=fightingStyles.styleId " +
                                      "WHERE classes.name=@Class AND level BETWEEN 1 AND @Level";
                command.Parameters.AddWithValue("@Class", className);
                command.Parameters.AddWithValue("@Level", level);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            fightingStyles.Add(new FightingStyle(dbReader.GetString(0), dbReader.GetString(1)));
                        }
                    }
                }
            }

            return fightingStyles;
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
        /// gets the amount of favored enemies a class may choose at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public int getFavoredEnemyAmount(string className, int level)
        {
            int favoredEnemyAmount = 0;

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
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            favoredEnemyAmount = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return favoredEnemyAmount;
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
        /// gets the amount of favored terrains a class may choose at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public int getFavoredTerrainAmount(string className, int level)
        {
            int favoredTerrainAmount = 0;

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
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            favoredTerrainAmount = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return favoredTerrainAmount;
        }

        /// <summary>
        /// gets a list of all possible favored enemy types
        /// </summary>
        public List<string> getFavoredEnemyTypes()
        {
            List<string> favoredEnemies = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT name FROM favoredEnemyTypes";

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            favoredEnemies.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return favoredEnemies;
        }

        /// <summary>
        /// gets a list of all possible favored terrain types
        /// </summary>
        /// <returns></returns>
        public List<string> getFavoredTerrainTypes()
        {
            List<string> favoredTerrain = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT name FROM favoredTerrainTypes";

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            favoredTerrain.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return favoredTerrain;
        }

        /// <summary>
        /// checks, if the given class has more than the standard set of skills
        /// </summary>
        /// <param name="className">chosen class</param>
        public bool hasExtraSkillCheckbox(string className)
        {
            return (className == "Rogue");
        }

        /// <summary>
        /// gets the name of the additional skill outside of the standard skill set
        /// </summary>
        /// <param name="className">chosen class</param>
        public string getExtraSkillCheckbox(string className)
        {
            if (className == "Rogue")
            {
                return "Thieves' Tools";
            }

            return "";
        }

        /// <summary>
        /// gets the tooltip of the additional skill outside of the standard skill set
        /// </summary>
        /// <param name="className">chosen class</param>
        public string getExtraSkillTooltip(string className)
        {
            if (className == "Rogue")
            {
                return "Your skill with thieves' tools lets you disarm traps and pick locks.";
            }

            return "";
        }

        /// <summary>
        /// checks, if a given class chooses additional skills or skills to which more modifiers are applied
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool hasSkillChoice(string className, int level)
        {
            bool hasSkillCoice = false;

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
                        hasSkillCoice = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasSkillCoice;
        }

        /// <summary>
        /// checks, if the extra skill choice doubles proficiency or gains new proficiencies
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool SkillChoiceDoublesProficiency(string className, int level)
        {
            bool doublesProficiency = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT doublesProficiency FROM extraClassSkillChoice " +
                                      "INNER JOIN classes ON classes.classid=extraClassSkillChoice.classId " +
                                      "WHERE classes.name=@Class AND extraClassSkillChoice.level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Class", className);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            doublesProficiency = dbReader.GetBoolean(0);
                        }
                    }
                }
            }

            return doublesProficiency;
        }

        /// <summary>
        /// gets the amount of additional skills a class can choose
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public int getSkillChoiceAmount(string className, int level)
        {
            int choiceAmount = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT amount FROM extraClassSkillChoice " +
                                      "INNER JOIN classes ON classes.classid=extraClassSkillChoice.classId " +
                                      "WHERE classes.name=@Class AND extraClassSkillChoice.level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Class", className);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            choiceAmount = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return choiceAmount;
        }

        /// <summary>
        /// checks, if there are any restrictions on the skills that may be chosen
        /// </summary>
        /// <param name="className">chosen class</param>
        public bool hasSkillChoiceRestrictions(string className)
        {
            bool hasSkillCoiceRestrictions = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT skills.name FROM extraClassSkillChoiceRestrictions " +
                                      "INNER JOIN classes ON classes.classid=extraClassSkillChoiceRestrictions.classId " +
                                      "INNER JOIN skills ON skills.skillId=extraClassSkillChoiceRestrictions.skillId  " +
                                      "WHERE classes.name=@Class";
                command.Parameters.AddWithValue("@Class", className);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        hasSkillCoiceRestrictions = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasSkillCoiceRestrictions;
        }

        /// <summary>
        /// gets a list of skills from which the additional choices must be selected
        /// </summary>
        /// <param name="className">chosen class</param>
        public List<string> getSkillChoiceRestrictions(string className)
        {
            List<string> skillCoiceRestrictions = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT skills.name FROM extraClassSkillChoiceRestrictions " +
                                      "INNER JOIN classes ON classes.classid=extraClassSkillChoiceRestrictions.classId " +
                                      "INNER JOIN skills ON skills.skillId=extraClassSkillChoiceRestrictions.skillId  " +
                                      "WHERE classes.name=@Class";
                command.Parameters.AddWithValue("@Class", className);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            skillCoiceRestrictions.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return skillCoiceRestrictions;
        }

    }
}
