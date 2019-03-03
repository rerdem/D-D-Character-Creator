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

        public ExtraSubclassChoiceDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        public bool hasExtraSubclassChoices(string subclass, int level)
        {
            return true;
        }

        /// <summary>
        /// gets a list of all skills the given cleric subclass may choose from
        /// </summary>
        /// <param name="subclass">given subclass</param>
        public List<string> getClericSubclassSkills(string subclass)
        {
            List<string> clericSubclassSkills = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT skills.name FROM extraClericProficiencies " +
                                      "INNER JOIN subclasses ON extraClericProficiencies.subclassId=subclasses.subclassId " +
                                      "INNER JOIN skills ON extraClericProficiencies.skillId=skills.skillId " +
                                      "WHERE subclasses.name=@Subclass";
                command.Parameters.AddWithValue("@Subclass", subclass);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            clericSubclassSkills.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return clericSubclassSkills;
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
