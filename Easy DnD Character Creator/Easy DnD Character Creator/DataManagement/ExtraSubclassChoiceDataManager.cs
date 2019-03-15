using Easy_DnD_Character_Creator.DataManagement.ExtraSubclassManagers;
using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public class ExtraSubclassChoiceDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public ExtraSubclassSkillDataManager ExtraSubclassSkillData { get; }
        public TotemDataManager TotemData { get; }
        public ExtraSubclassSpellDataManager ExtraSubclassSpellData { get; }

        public ExtraSubclassChoiceDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;

            ExtraSubclassSkillData = new ExtraSubclassSkillDataManager(ConnectionString, inputUsedBooks);
            TotemData = new TotemDataManager(ConnectionString, inputUsedBooks);
            ExtraSubclassSpellData = new ExtraSubclassSpellDataManager(ConnectionString, inputUsedBooks);
        }

        public void setUsedBooks(List<string> inputUsedBooks)
        {
            UsedBooks = inputUsedBooks;
            ExtraSubclassSkillData.UsedBooks = inputUsedBooks;
            TotemData.UsedBooks = inputUsedBooks;
            ExtraSubclassSpellData.UsedBooks = inputUsedBooks;
        }

        public bool hasExtraSubclassChoices(string subclass, int level)
        {
            return ExtraSubclassSkillData.hasSkillChoice(subclass, level) || TotemData.hasTotemFeatures(subclass, level) 
                || ExtraSubclassSpellData.hasExtraSpellChoice(subclass) || hasToolProficiencyChoice(subclass, level);
        }

        /// <summary>
        /// checks, if a given subclass must choose additional tool proficiencies at a given level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public bool hasToolProficiencyChoice(string subclass, int level)
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
        /// gets a list of tool proficiencies a given subclass can choose from at a given level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public List<string> getToolProficiencyChoices(string subclass, int level)
        {
            List<string> tools = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT tools.name FROM tools " +
                                      "WHERE tools.type IN " +
                                      "(SELECT extraSubclassToolProficiencyChoices.name FROM extraSubclassToolProficiencyChoices " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = extraSubclassToolProficiencyChoices.subclassId " +
                                      "WHERE subclasses.name = @Subclass AND extraSubclassToolProficiencyChoices.level BETWEEN 1 AND @Level)";
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            tools.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return tools;
        }







        /// <summary>
        /// gets a list of all possible dragon bloodlines
        /// </summary>
        public List<Dragon> getDragonBloodlines()
        {
            List<Dragon> bloodlines = new List<Dragon>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT dragon, damageType FROM dragonBloodline";

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            bloodlines.Add(new Dragon(dbReader.GetString(0), dbReader.GetString(1)));
                        }
                    }
                }
            }

            return bloodlines;
        }

        ///// <summary>
        ///// gets the damage type associated with the given dragon type
        ///// </summary>
        ///// <param name="dragon">color of dragon to get damage type for</param>
        //public string getDragonDamageType(string dragon)
        //{
        //    string damageType = "";

        //    DBConnection.Open();
        //    SQLiteDataReader dbReader;
        //    SQLiteCommand dbQuery;
        //    dbQuery = DBConnection.CreateCommand();
        //    dbQuery.CommandText = "SELECT damageType FROM dragonBloodline " +
        //                          "WHERE dragon=\"";
        //    dbQuery.CommandText += dragon;
        //    dbQuery.CommandText = "\"";

        //    dbReader = dbQuery.ExecuteReader();
        //    while (dbReader.Read())
        //    {
        //        if (!dbReader.IsDBNull(0))
        //        {
        //            damageType = dbReader.GetString(0);
        //        }
        //    }

        //    DBConnection.Close();

        //    return damageType;
        //}
    }
}
