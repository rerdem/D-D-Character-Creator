using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public enum ProficiencyType { armor, weapon, tool }

    public class ExportDataManager
    {
        private string ConnectionString { get; }
        private string TemplatePath { get; }
        public List<string> UsedBooks { get; set; }

        public ExportDataManager(string inputConnectionString, string inputTemplatePath, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            TemplatePath = inputTemplatePath;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// gets the entire content of the html template file as a string
        /// </summary>
        public string getHTMLTemplate()
        {
            string template = "";

            if (File.Exists(TemplatePath))
            {
                template = File.ReadAllText(TemplatePath);
            }

            return template;
        }

        /// <summary>
        /// gets the minimum number of XP needed for a given level
        /// </summary>
        /// <param name="level">current level</param>
        public int getXpForLevel(int level)
        {
            int xpForLevel = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT minimumXp FROM xp " +
                                      "WHERE level = @Level";
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            xpForLevel = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return xpForLevel;
        }

        /// <summary>
        /// gets the proficiency bonus for a given level
        /// </summary>
        /// <param name="level">current level</param>
        public int getProficiencyBonus(int level)
        {
            int proficiencyBonus = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT bonus FROM proficiencyBonus " +
                                      "WHERE level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            proficiencyBonus = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return proficiencyBonus;
        }

        /// <summary>
        /// gets a list of all saving throws a given class is proficient in
        /// </summary>
        /// <param name="className">chosen class</param>
        public List<string> getSaveProficiencies(string className)
        {
            List<string> saveProficiencies = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT abilities.name FROM savingThrows " +
                                      "INNER JOIN classes ON savingThrows.classId=classes.classid " +
                                      "INNER JOIN abilities ON savingThrows.abilityId=abilities.abilityId " +
                                      "WHERE classes.name=@Class";
                command.Parameters.AddWithValue("@Class", className);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            saveProficiencies.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return saveProficiencies;
        }

        /// <summary>
        /// gets a list of all armor proficiencies gained by subrace, class, subclass and background
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="background">chosen background</param>
        public List<string> getArmorProficiencies(string subrace, string className, string subclass, string background)
        {
            return getProficienciesByType(ProficiencyType.armor, subrace, className, subclass, background);
        }

        /// <summary>
        /// gets a list of all weapon proficiencies gained by subrace, class, subclass and background
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="background">chosen background</param>
        public List<string> getWeaponProficiencies(string subrace, string className, string subclass, string background)
        {
            return getProficienciesByType(ProficiencyType.weapon, subrace, className, subclass, background);
        }

        /// <summary>
        /// gets a list of all tool proficiencies gained by subrace, class, subclass and background
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="background">chosen background</param>
        public List<string> getToolProficiencies(string subrace, string className, string subclass, string background)
        {
            return getProficienciesByType(ProficiencyType.tool, subrace, className, subclass, background);
        }

        /// <summary>
        /// gets a list of all proficiencies of a given type gained by subrace, class, subclass and background
        /// </summary>
        /// <param name="type">requested type</param>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="background">chosen background</param>
        private List<string> getProficienciesByType(ProficiencyType type, string subrace, string className, string subclass, string background)
        {
            List<string> proficiencies = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT proficiencies FROM extraRaceProficiencies " +
                                      "INNER JOIN races ON extraRaceProficiencies.raceId=races.raceid " +
                                      "WHERE races.subrace=@Subrace  AND extraRaceProficiencies.type=@Type " +
                                      "UNION " +
                                      "SELECT proficiencies FROM extraClassProficiencies " +
                                      "INNER JOIN classes ON extraClassProficiencies.classId=classes.classid " +
                                      "WHERE classes.name=@Class AND extraClassProficiencies.type=@Type " +
                                      "UNION " +
                                      "SELECT proficiencies FROM extraSubclassProficiencies " +
                                      "INNER JOIN subclasses ON extraSubclassProficiencies.subclassId=subclasses.subclassId " +
                                      "WHERE subclasses.name=@Subclass AND extraSubclassProficiencies.type=@Type " +
                                      "UNION " +
                                      "SELECT proficiencies FROM extraBackgroundProficiencies " +
                                      "INNER JOIN backgrounds ON extraBackgroundProficiencies.backgroundId=backgrounds.backgroundId " +
                                      "WHERE backgrounds.name=@Background  AND extraBackgroundProficiencies.type=@Type";
                command.Parameters.AddWithValue("@Type", type.ToString());
                command.Parameters.AddWithValue("@Subrace", subrace);
                command.Parameters.AddWithValue("@Class", className);
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Background", background);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            proficiencies.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return proficiencies;
        }

        /// <summary>
        /// gets the name of the ability used for unarmored AC for a given class
        /// </summary>
        /// <param name="className">chosen class</param>
        public List<string> getUnarmoredDefenseAbilities(string className)
        {
            List<string> abilities = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT abilities.name FROM unarmoredDefenseAbility " +
                                      "INNER JOIN classes ON classes.classid=unarmoredDefenseAbility.classId " +
                                      "INNER JOIN abilities ON abilities.abilityId=unarmoredDefenseAbility.abilityId " +
                                      "WHERE classes.name=@Class";
                command.Parameters.AddWithValue("@Class", className);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            abilities.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return abilities;
        }

        /// <summary>
        /// gets a list of features gained by subrace, class, subclass, background at a given level
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="background">chosen background</param>
        /// <param name="level">current level</param>
        public List<Feature> getFeatures(string subrace, string className, string subclass, string background, int level)
        {
            List<Feature> featureList = new List<Feature>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT features.name, features.description FROM raceFeatures " +
                                      "INNER JOIN features ON raceFeatures.featureId=features.featureId " +
                                      "INNER JOIN races ON raceFeatures.raceId=races.raceid " +
                                      "WHERE races.subrace=@Subrace  AND raceFeatures.level BETWEEN 1 AND @Level " +
                                      "UNION " +
                                      "SELECT features.name, features.description FROM classFeatures " +
                                      "INNER JOIN features ON classFeatures.featureId=features.featureId " +
                                      "INNER JOIN classes ON classFeatures.classId=classes.classid " +
                                      "WHERE classes.name=@Class AND classFeatures.level BETWEEN 1 AND @Level " +
                                      "UNION " +
                                      "SELECT features.name, features.description FROM subclassFeatures " +
                                      "INNER JOIN features ON subclassFeatures.featureId=features.featureId " +
                                      "INNER JOIN subclasses ON subclassFeatures.subclassId=subclasses.subclassid " +
                                      "WHERE subclasses.name=@Subclass AND subclassFeatures.level BETWEEN 1 AND @Level " +
                                      "UNION " +
                                      "SELECT features.name, features.description FROM backgroundFeatures " +
                                      "INNER JOIN features ON backgroundFeatures.featureId=features.featureId " +
                                      "INNER JOIN backgrounds ON backgroundFeatures.backgroundId=backgrounds.backgroundId " +
                                      "WHERE backgrounds.name=@Background AND backgroundFeatures.level BETWEEN 1 AND @Level";
                command.Parameters.AddWithValue("@Subrace", subrace);
                command.Parameters.AddWithValue("@Class", className);
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Background", background);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            featureList.Add(new Feature(dbReader.GetString(0), dbReader.GetString(1)));
                        }
                    }
                }
            }

            return featureList;
        }

        /// <summary>
        /// gets a feature by its ID in the database
        /// </summary>
        /// <param name="id">ID the feature has in the database</param>
        public Feature getFeatureById(int id)
        {
            Feature feature = new Feature();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT name, description FROM features " +
                                      "WHERE featureId=@ID";
                command.Parameters.AddWithValue("@ID", id.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            feature = new Feature(dbReader.GetString(0), dbReader.GetString(1));
                        }
                    }
                }
            }

            return feature;
        }

        /// <summary>
        /// gets the description of the breath weapon of the given subrace at the given level
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="level">current level</param>
        /// <returns>the description of the breath weapon or an empty string, if there is none</returns>
        public string getBreathWeapon(string subrace, int level)
        {
            string breathWeapon = "";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT dragonbornBreathWeapon.description FROM dragonbornBreathWeapon " +
                                      "INNER JOIN races ON dragonbornBreathWeapon.raceId=races.raceid " +
                                      "WHERE races.subrace=@Subrace AND level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Subrace", subrace);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            breathWeapon = dbReader.GetString(0);
                        }
                    }
                }
            }

            return breathWeapon;
        }

        /// <summary>
        /// gets the values for barbarian rage
        /// </summary>
        public BarbarianRage getBarbarianRage(int level)
        {
            BarbarianRage rage = new BarbarianRage();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT rages, damageBonus FROM barbarianRage  " +
                                      "WHERE level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            rage = new BarbarianRage(dbReader.GetString(0), dbReader.GetString(1));
                        }
                    }
                }
            }

            return rage;
        }

        /// <summary>
        /// gets the type of die a bard may use for inspiration at the given level
        /// </summary>
        /// <param name="level">current level</param>
        public string getBardicInspirationDie(int level)
        {
            string inspirationDie = "";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT dieType FROM bardicInspiration " +
                                      "WHERE level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            inspirationDie = dbReader.GetString(0);
                        }
                    }
                }
            }

            return inspirationDie;
        }


        /// <summary>
        /// gets the amount of Sorcery Points a Sorcerer has at a given level
        /// </summary>
        /// <param name="level">current level</param>
        public int getSorceryPoints(int level)
        {
            int sorceryPoints = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT sorceryPoints FROM sorcererFeatures " +
                                      "WHERE level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            sorceryPoints = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return sorceryPoints;
        }

        /// <summary>
        /// gets the type of die a monk rolls for martial arts damage
        /// </summary>
        /// <param name="level">current level</param>
        public string getMartialArtsDie(int level)
        {
            string martialArtsDie = "";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT martialArtsDamage FROM monkFeatures " +
                                      "WHERE level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            martialArtsDie = dbReader.GetString(0);
                        }
                    }
                }
            }

            return martialArtsDie;
        }

        /// <summary>
        /// gets the amount of Ki Points a Monk has at a given level
        /// </summary>
        /// <param name="level">current level</param>
        public int getKiPoints(int level)
        {
            int kiPoints = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT kiPoints FROM monkFeatures " +
                                      "WHERE level BETWEEN 1 AND @Level " +
                                      "ORDER BY level DESC LIMIT 1";
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            kiPoints = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return kiPoints;
        }
    }
}
