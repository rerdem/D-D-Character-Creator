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
        /// checks, if a given subrace has any additional choices to make
        /// </summary>
        /// <param name="subrace">given subrace</param>
        public bool hasExtraRaceChoices(string subrace)
        {
            //currently the only possible race choice is an extra cantrip for High Elves
            return hasExtraRaceCantripChoice(subrace);
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
                command.CommandText = "SELECT * FROM spells " +
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
                            cantripList.Add(new Spell(dbReader.GetString(1), dbReader.GetBoolean(2), dbReader.GetInt32(3), dbReader.GetString(4), dbReader.GetString(5), dbReader.GetString(6), dbReader.GetString(7), dbReader.GetString(8), dbReader.GetString(9), dbReader.GetString(10), false));
                        }
                    }
                }
            }

            return cantripList;
        }

        public string getExtraRaceChoiceIntroText(string subrace)
        {
            if (subrace == "High Elf")
            {
                return "As a High Elf, please choose a cantrip you know:";
            }

            return "";
        }
    }
}
