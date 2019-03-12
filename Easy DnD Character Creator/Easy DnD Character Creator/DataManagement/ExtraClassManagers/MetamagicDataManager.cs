using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement.ExtraClassManagers
{
    public class MetamagicDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public MetamagicDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// checks, if a given class has access to metamagic at a given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool hasMetamagic(string className, int level)
        {
            bool hasMetamagic = (className == "Sorcerer");
            int metamagicAmount = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT metamagicAmount FROM sorcererFeatures " +
                                      "WHERE level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            metamagicAmount = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return hasMetamagic && (metamagicAmount > 0);
        }

        /// <summary>
        /// gets the amount of metamagic abilities a given class may choose at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public int getMetamagicAmount(string className, int level)
        {
            int metamagicAmount = 0;

            if (className == "Sorcerer")
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    connection.Open();
                    command.CommandText = "SELECT metamagicAmount FROM sorcererFeatures " +
                                          "WHERE level BETWEEN 1 AND @Level " +
                                          "ORDER BY level DESC LIMIT 1";
                    command.Parameters.AddWithValue("@Level", level.ToString());

                    using (SQLiteDataReader dbReader = command.ExecuteReader())
                    {
                        if (dbReader.Read())
                        {
                            if (!dbReader.IsDBNull(0))
                            {
                                metamagicAmount = dbReader.GetInt32(0);
                            }
                        }
                    }
                }
            }

            return metamagicAmount;
        }

        /// <summary>
        /// gets a list of all available metamagic options
        /// </summary>
        public List<Metamagic> getMetamagicOptions()
        {
            List<Metamagic> options = new List<Metamagic>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT name, description FROM metamagic";

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            options.Add(new Metamagic(dbReader.GetString(0), dbReader.GetString(1)));
                        }
                    }
                }
            }
            
            return options;
        }
    }
}
