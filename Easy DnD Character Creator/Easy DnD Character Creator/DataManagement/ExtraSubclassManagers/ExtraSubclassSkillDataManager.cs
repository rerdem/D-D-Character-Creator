using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement.ExtraSubclassManagers
{
    public class ExtraSubclassSkillDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public ExtraSubclassSkillDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// checks, if the extra skill choice doubles proficiency or gains new proficiencies
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public bool SkillChoiceDoublesProficiency(string subclass, int level)
        {
            bool doublesProficiency = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT doublesProficiency FROM extraSubclassSkillChoice " +
                                      "INNER JOIN subclasses ON subclasses.subclassId=extraSubclassSkillChoice.subclassId " +
                                      "WHERE subclasses.name=@Subclass AND extraSubclassSkillChoice.level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Subclass", subclass);
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
        /// gets the amount of additional skills a subclass can choose
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public int getSkillChoiceAmount(string subclass, int level)
        {
            int choiceAmount = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT amount FROM extraSubclassSkillChoice " +
                                      "INNER JOIN subclasses ON subclasses.subclassId=extraSubclassSkillChoice.subclassId " +
                                      "WHERE subclasses.name=@Subclass AND extraSubclassSkillChoice.level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Subclass", subclass);
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
        /// <param name="subclass">chosen subclass</param>
        public bool hasSkillChoiceRestrictions(string subclass)
        {
            bool hasSkillCoiceRestrictions = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT skills.name FROM extraSubclassSkillChoiceRestrictions " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = extraSubclassSkillChoiceRestrictions.subclassId " +
                                      "INNER JOIN skills ON skills.skillId = extraSubclassSkillChoiceRestrictions.skillId " +
                                      "WHERE subclasses.name = @Subclass";
                command.Parameters.AddWithValue("@Subclass", subclass);

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
        /// <param name="subclass">chosen subclass</param>
        public List<string> getSkillChoiceRestrictions(string subclass)
        {
            List<string> skillCoiceRestrictions = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT skills.name FROM extraSubclassSkillChoiceRestrictions " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = extraSubclassSkillChoiceRestrictions.subclassId " +
                                      "INNER JOIN skills ON skills.skillId = extraSubclassSkillChoiceRestrictions.skillId " +
                                      "WHERE subclasses.name = @Subclass";
                command.Parameters.AddWithValue("@Subclass", subclass);

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
