using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement.SubManagers
{
    public class WarlockChoiceDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public WarlockChoiceDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// checks, if a given class may choose eldritch invocations at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool hasEldritchInvocations(string className, int level)
        {
            return ((className == "Warlock") && (level > 1));
        }

        /// <summary>
        /// gets the amounf of invocationsthat may be chosen at the given level
        /// </summary>
        /// <param name="level">current level</param>
        public int getEldritchInvocationAmount(int level)
        {
            int invocations = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT invocationsKnown FROM eldritchInvocationsKnown " +
                                      "WHERE level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            invocations = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return invocations;
        }

        //TO DO
        public int getEldritchInvocations(int level)
        {
            int invocations = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT invocationsKnown FROM eldritchInvocationsKnown " +
                                      "WHERE level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            invocations = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return invocations;
        }
    }
}
