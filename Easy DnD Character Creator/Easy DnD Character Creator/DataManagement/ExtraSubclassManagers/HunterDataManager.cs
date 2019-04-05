using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement.ExtraSubclassManagers
{
    public class HunterDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public HunterDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// gets the number of features available for a given subclass/level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public int hunterFeatureAmount(string subclass, int level)
        {
            int featureAmount = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT COUNT(hunterFeatures.name) FROM hunterFeatures  " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = hunterFeatures.subclassId " +
                                      "WHERE subclasses.name = @Subclass AND hunterFeatures.level BETWEEN 1 AND  @Level";
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
        /// gets a list of all hunter features and options for the given subclass/level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public List<ChoiceFeature> getHunterFeatures(string subclass, int level)
        {
            List<ChoiceFeature> featureList = new List<ChoiceFeature>();
            ChoiceFeature feature = new ChoiceFeature();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT hunterFeatures.name, hunterFeatures.description, hunterOptions.name, hunterOptions.description " +
                                      "FROM hunterFeatures " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = hunterFeatures.subclassId " +
                                      "INNER JOIN hunterOptions ON hunterOptions.hunterFeatureId=hunterFeatures.hunterFeatureId " +
                                      "WHERE subclasses.name = @Subclass AND hunterFeatures.level BETWEEN 1 and @Level";
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
                                feature = new ChoiceFeature(dbReader.GetString(0), dbReader.GetString(1));
                            }
                            feature.addOption(new ChoiceFeatureOption(dbReader.GetString(2), dbReader.GetString(3), false));
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
