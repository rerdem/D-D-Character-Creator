using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public class SkillDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public SkillDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// gets a list of all available skills
        /// </summary>
        public List<string> getSkills()
        {
            List<string> skills = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT name FROM skills";
                
                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            skills.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return skills;
        }

        /// <summary>
        /// gets the description of the chosen skill
        /// </summary>
        /// <param name="skill">name of the chosen skill</param>
        public string getSkillDescription(string skill)
        {
            string description = "";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT description FROM skills " +
                                      "WHERE name=@Skill";
                command.Parameters.AddWithValue("@Skill", skill);

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
        /// gets a list of known skills from subrace and background choices
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="background">chosen background</param>
        public List<string> getKnownSkills(string subrace, string background)
        {
            List<string> knownSkills = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT skills.name FROM skills " +
                                      "INNER JOIN backgroundSkills ON backgroundSkills.skillId=skills.skillId " +
                                      "INNER JOIN backgrounds ON backgroundSkills.backgroundId=backgrounds.backgroundId " +
                                      "WHERE backgrounds.name=@Background " +
                                      "UNION " +
                                      "SELECT skills.name FROM skills " +
                                      "INNER JOIN raceSkills ON raceSkills.skillId=skills.skillId " +
                                      "INNER JOIN races ON raceSkills.raceId=races.raceid " +
                                      "WHERE races.subrace=@Subrace";
                command.Parameters.AddWithValue("@Background", background);
                command.Parameters.AddWithValue("@Subrace", subrace);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            knownSkills.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return knownSkills;
        }

        /// <summary>
        /// gets the list of skills the chosen class can choose from
        /// </summary>
        /// <param name="className">chosen class</param>
        public List<string> getClassSkillOptions(string className)
        {
            List<string> skillOptions = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT skills.name FROM skills " +
                                      "INNER JOIN classSkillChoiceOptions ON classSkillChoiceOptions.skillId=skills.skillId " +
                                      "INNER JOIN classes ON classSkillChoiceOptions.classId=classes.classid " +
                                      "WHERE classes.name=@Class";
                command.Parameters.AddWithValue("@Class", className);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            skillOptions.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return skillOptions;
        }

        /// <summary>
        /// gets the number of skills the class can select
        /// </summary>
        /// <param name="className">chosen class</param>
        public int getClassSkillCount(string className)
        {
            int skillCount = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT extraSkills FROM classes " +
                                      "WHERE classes.name=@Class";
                command.Parameters.AddWithValue("@Class", className);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            skillCount = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return skillCount;
        }

        /// <summary>
        /// checks, if the chosen subrace gets to choose additional skills
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        public bool hasExtraSkillChoice(string subrace)
        {
            return (subrace == "Half-Elf");
        }

        /// <summary>
        /// gets the number of extra skills the chosen subrace can choose
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        public int getExtraSkillChoiceAmount(string subrace)
        {
            if (subrace == "Half-Elf")
            {
                return 2;
            }

            return 0;
        }

    }
}
