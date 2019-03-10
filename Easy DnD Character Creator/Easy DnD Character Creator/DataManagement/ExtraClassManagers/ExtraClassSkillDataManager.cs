using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement.ExtraClassManagers
{
    public class ExtraClassSkillDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public ExtraClassSkillDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
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
