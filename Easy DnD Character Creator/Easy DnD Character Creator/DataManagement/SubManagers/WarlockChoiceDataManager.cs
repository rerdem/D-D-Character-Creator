using Easy_DnD_Character_Creator.DataTypes;
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
            return ((className == "Warlock") && (getEldritchInvocationAmount(level) > 1));
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

        /// <summary>
        /// gets a list of eldritch invocations the warlock may choose with the current level and pact choice
        /// </summary>
        /// <param name="warlockPact">chosen warlock pact</param>
        /// <param name="level">current level</param>
        public List<EldritchInvocation> getEldritchInvocations(string warlockPact, int level)
        {
            List<EldritchInvocation> invocations = new List<EldritchInvocation>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT eldritchInvocations.name, eldritchInvocations.description, eldritchInvocations.level, " +
                                      "eldritchInvocations.pactRestriction, RequiredSpells.name, RequiredSpells.ritual, RequiredSpells.level, " +
                                      "RequiredSpells.school, RequiredSpells.castTime, RequiredSpells.range, RequiredSpells.duration, " +
                                      "RequiredSpells.components, RequiredSpells.materials, RequiredSpells.description, GainedSpells.name, " +
                                      "GainedSpells.ritual, GainedSpells.level, GainedSpells.school, GainedSpells.castTime, GainedSpells.range, " +
                                      "GainedSpells.duration, GainedSpells.components, GainedSpells.materials, GainedSpells.description " +
                                      "FROM eldritchInvocations " +
                                      "LEFT JOIN eldritchInvocationRequiredSpells ON eldritchInvocationRequiredSpells.invocationId = eldritchInvocations.invocationId " +
                                      "LEFT JOIN eldritchInvocationGainedSpells ON eldritchInvocationGainedSpells.invocationId = eldritchInvocations.invocationId " +
                                      "LEFT JOIN spells as RequiredSpells ON RequiredSpells.spellId = eldritchInvocationRequiredSpells.spellId " +
                                      "LEFT JOIN spells as GainedSpells ON GainedSpells.spellId = eldritchInvocationGainedSpells.spellId " +
                                      "WHERE (eldritchInvocations.pactRestriction = @NonPact OR eldritchInvocations.pactRestriction LIKE @Pact) " +
                                      "AND eldritchInvocations.level BETWEEN 1 AND @Level";
                command.Parameters.AddWithValue("@NonPact", string.Empty);
                command.Parameters.AddWithValue("@Pact", "%" + warlockPact + "%");
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            Spell gainedSpell = new Spell();
                            if (!dbReader.IsDBNull(4))
                            {
                                gainedSpell = new Spell(dbReader.GetString(4), dbReader.GetBoolean(5), dbReader.GetInt32(6), dbReader.GetString(7), dbReader.GetString(8), dbReader.GetString(9), dbReader.GetString(10), dbReader.GetString(11), dbReader.GetString(12), dbReader.GetString(13), false);
                            }

                            Spell requiredSpell = new Spell();
                            if (!dbReader.IsDBNull(14))
                            {
                                requiredSpell = new Spell(dbReader.GetString(14), dbReader.GetBoolean(15), dbReader.GetInt32(16), dbReader.GetString(17), dbReader.GetString(18), dbReader.GetString(19), dbReader.GetString(20), dbReader.GetString(21), dbReader.GetString(22), dbReader.GetString(23), false);
                            }

                            invocations.Add(new EldritchInvocation(dbReader.GetString(0), dbReader.GetString(1), dbReader.GetInt32(2), requiredSpell, dbReader.GetString(4), gainedSpell));
                        }
                    }
                }
            }

            return invocations;
        }
    }
}
