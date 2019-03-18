using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement.ExtraSubclassManagers
{
    public class ElementalDisciplineDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public ElementalDisciplineDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// checks, if the given subclass may choose elemental disciplines at the given level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public bool hasDisciplines(string subclass, int level)
        {
            return (getDisciplineAmount(subclass, level) > 0);
        }

        /// <summary>
        /// gets the amount of elemental disciplines the given subclass may choose at the given level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public int getDisciplineAmount(string subclass, int level)
        {
            int amount = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT disciplinesKnown FROM elementalDisciplinesKnown " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = elementalDisciplinesKnown.subclassId " +
                                      "WHERE subclasses.name = @Subclass AND elementalDisciplinesKnown.level BETWEEN 1 AND @Level " +
                                      "ORDER BY elementalDisciplinesKnown.level DESC LIMIT 1";
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
        /// gets a list of available elemental disciplines at the given level depending on whether they are selectable or mandatory
        /// </summary>
        /// <param name="level">current level</param>
        /// <param name="mandatory">are disciplines to get mandatory</param>
        private List<ElementalDiscipline> getDisciplines(int level, bool mandatory)
        {
            List<ElementalDiscipline> disciplines = new List<ElementalDiscipline>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT elementalDisciplines.name, elementalDisciplines.description, " +
                                      "spells.name, spells.ritual, spells.level, spells.school, spells.castTime, spells.range, " +
                                      "spells.duration, spells.components, spells.materials, spells.description, elementalDisciplines.mandatory " +
                                      "FROM elementalDisciplines " +
                                      "LEFT JOIN elementalDisciplinesGainedSpells ON elementalDisciplinesGainedSpells.disciplineId = elementalDisciplines.disciplineId " +
                                      "LEFT JOIN spells ON spells.spellId = elementalDisciplinesGainedSpells.spellId " +
                                      "WHERE elementalDisciplines.level BETWEEN 1 AND @Level " +
                                      "AND elementalDisciplines.mandatory = @Mandatory";
                command.Parameters.AddWithValue("@Level", level.ToString());
                if (mandatory)
                {
                    command.Parameters.AddWithValue("@Mandatory", 1);
                }
                else
                {
                    command.Parameters.AddWithValue("@Mandatory", 0);
                }                

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            if (!dbReader.IsDBNull(2))
                            {
                                Spell gainedSpell = new Spell(dbReader.GetString(2), dbReader.GetBoolean(3), dbReader.GetInt32(4), dbReader.GetString(5), dbReader.GetString(6), dbReader.GetString(7), dbReader.GetString(8), dbReader.GetString(9), dbReader.GetString(10), dbReader.GetString(11), false);
                                disciplines.Add(new ElementalDiscipline(dbReader.GetString(0), dbReader.GetString(1), gainedSpell));
                            }
                            else
                            {
                                disciplines.Add(new ElementalDiscipline(dbReader.GetString(0), dbReader.GetString(1), new Spell()));
                            }
                        }
                    }
                }
            }

            return disciplines;
        }

        /// <summary>
        /// gets a list of choosable elemental disciplines at the given level
        /// </summary>
        /// <param name="level">current level</param>
        public List<ElementalDiscipline> getDisciplineOptions(int level)
        {
            return getDisciplines(level, false);
        }

        /// <summary>
        /// gets a list of mandatory elemental disciplines at the given level
        /// </summary>
        /// <param name="level">current level</param>
        public List<ElementalDiscipline> getMandatoryDisciplines(int level)
        {
            return getDisciplines(level, true);
        }
    }
}
