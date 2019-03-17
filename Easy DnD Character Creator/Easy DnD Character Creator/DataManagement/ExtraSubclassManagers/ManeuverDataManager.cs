using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement.ExtraSubclassManagers
{
    public class ManeuverDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public ManeuverDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// checks, if a given subclass may choose maneuvers at the given level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public bool hasManeuvers(string subclass, int level)
        {
            return (getManeuverAmount(subclass, level) > 0);
        }

        /// <summary>
        /// gets the amount of maneuvers a given subclass may choose at the given level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public int getManeuverAmount(string subclass, int level)
        {
            int amount = 0;

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
                            amount = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return amount;
        }

        /// <summary>
        /// gets a list of available maneuvers
        /// </summary>
        public List<Maneuver> getManeuvers()
        {
            List<Maneuver> maneuvers = new List<Maneuver>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT name, description FROM maneuvers";

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            maneuvers.Add(new Maneuver(dbReader.GetString(0), dbReader.GetString(1)));
                        }
                    }
                }
            }

            return maneuvers;
        }
    }
}
