using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement.SubManagers
{
    public class FightingStyleDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public FightingStyleDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
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
                command.CommandText = "SELECT fightingStyles.name, fightingStyles.description from fightingStyleGain " +
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
    }
}
