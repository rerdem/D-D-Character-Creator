using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement.ExtraSubclassManagers
{
    public class TotemDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public TotemDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// checks, if a given subclass has a totem choice at the given level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        /// <returns></returns>
        public bool hasTotemFeatures(string subclass, int level)
        {
            return (totemFeatureAmount(subclass, level) > 0);
        }

        /// <summary>
        /// gets the number of features available for a given subclass/level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public int totemFeatureAmount(string subclass, int level)
        {
            int featureAmount = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT COUNT(barbarianTotemFeatures.name) FROM barbarianTotemFeatures " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = barbarianTotemFeatures.subclassId " +
                                      "WHERE subclasses.name = @Subclass AND barbarianTotemFeatures.level BETWEEN 1 and @Level";
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            featureAmount = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return featureAmount;
        }

        /// <summary>
        /// gets a list of all totem features and options for the given subclass/level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public List<TotemFeature> getTotemFeatures(string subclass, int level)
        {
            List<TotemFeature> featureList = new List<TotemFeature>();
            TotemFeature feature = new TotemFeature();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT barbarianTotemFeatures.name, barbarianTotemFeatures.description, barbarianTotemOptions.name, " +
                                      "barbarianTotemOptions.description FROM barbarianTotemFeatures " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = barbarianTotemFeatures.subclassId " +
                                      "INNER JOIN barbarianTotemOptions ON barbarianTotemOptions.totemFeatureId=barbarianTotemFeatures.totemFeatureId " +
                                      "WHERE subclasses.name = @Subclass AND barbarianTotemFeatures.level BETWEEN 1 and @Level";
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            if (feature.Name != dbReader.GetString(0))
                            {
                                if (!string.IsNullOrEmpty(feature.Name))
                                {
                                    featureList.Add(feature);
                                }
                                feature = new TotemFeature(dbReader.GetString(0), dbReader.GetString(1));
                            }
                            feature.addOption(new TotemOption(dbReader.GetString(2), dbReader.GetString(3), false));
                        }
                    }

                    if (!featureList.Contains(feature) && !string.IsNullOrEmpty(feature.Name))
                    {
                        featureList.Add(feature);
                    }
                }
            }

            return featureList;
        }


        
    }
}
