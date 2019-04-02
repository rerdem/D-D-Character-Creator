using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public class ExtraRaceChoiceDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public ExtraRaceChoiceDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// checks, if a given subrace may choose additional cantrips
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        public bool hasExtraRaceCantripChoice(string subrace)
        {
            bool hasExtraCantripChoice = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM extraRaceCantrips " +
                                      "INNER JOIN races ON races.raceid = extraRaceCantrips.raceId " +
                                      "WHERE races.subrace=@Subrace";
                command.Parameters.AddWithValue("@Subrace", subrace);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        hasExtraCantripChoice = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasExtraCantripChoice;
        }

        /// <summary>
        /// gets a list of the additional cantrips the given subrace may choose from
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        public List<Spell> getExtraRaceCantripChoiceOptions(string subrace)
        {
            List<Spell> cantripList = new List<Spell>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT spells.name, spells.ritual, spells.level, spells.school, spells.castTime, spells.range, spells.duration, " +
                                      "spells.components, spells.materials, spells.description, (SELECT abilities.name FROM extraRaceCantrips " +
                                      "INNER JOIN races ON races.raceid = extraRaceCantrips.raceId INNER JOIN classes ON classes.classid = extraRaceCantrips.classId " +
                                      "INNER JOIN abilities ON abilities.abilityId = extraRaceCantrips.spellcastingAbility " +
                                      "WHERE races.subrace = @Subrace) as spellcastingAbility " +
                                      "FROM spells " +
                                      "INNER JOIN books ON spells.book = books.bookid " +
                                      "WHERE spells.level = 0 " +
                                      "AND spells.classes LIKE \"%\" || " +
                                      "(SELECT classes.name FROM extraRaceCantrips " +
                                      "INNER JOIN races ON races.raceid = extraRaceCantrips.raceId " +
                                      "INNER JOIN classes ON classes.classid = extraRaceCantrips.classId " +
                                      "WHERE races.subrace=@Subrace) || \"%\" " +
                                      "AND books.title IN(@UsedBooks)";
                command.Parameters.AddWithValue("@Subrace", subrace);
                SQLiteCommandExtensions.AddParametersWithValues(command, "@UsedBooks", UsedBooks);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            cantripList.Add(new ExtraRaceSpell(dbReader.GetString(0), 
                                                               dbReader.GetBoolean(1), 
                                                               dbReader.GetInt32(2), 
                                                               dbReader.GetString(3), 
                                                               dbReader.GetString(4), 
                                                               dbReader.GetString(5), 
                                                               dbReader.GetString(6), 
                                                               dbReader.GetString(7), 
                                                               dbReader.GetString(8), 
                                                               dbReader.GetString(9), 
                                                               false, 0,
                                                               dbReader.GetString(10)));
                        }
                    }
                }
            }

            return cantripList;
        }
    }
}
