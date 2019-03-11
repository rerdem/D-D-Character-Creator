using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement.ExtraClassManagers
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
        /// checks, if a given class has warlock choices at a given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool hasWarlockChoices(string className, int level)
        {
            return hasWarlockPact(className, level) || hasEldritchInvocations(className, level);
        }

        /// <summary>
        /// checks, if a given class may choose a warlock pact at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool hasWarlockPact(string className, int level)
        {
            return ((className == "Warlock") && (level == 3));
        }

        /// <summary>
        /// gets a list of available warlock pacts
        /// </summary>
        public List<WarlockPact> getWarlockPacts()
        {
            List<WarlockPact> pacts = new List<WarlockPact>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT name, description, spellAmount FROM warlockPacts";

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            pacts.Add(new WarlockPact(dbReader.GetString(0), dbReader.GetString(1), dbReader.GetInt32(2)));
                        }
                    }
                }
            }

            return pacts;
        }

        /// <summary>
        /// gets a list of spells that a given warlock pact may choose from
        /// </summary>
        /// <param name="pact">chosen warlock pact</param>
        public List<Spell> getPactSpells(WarlockPact pact)
        {
            List<Spell> pactSpells = new List<Spell>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT spells.name, spells.ritual, spells.level, spells.school, spells.castTime, spells.range, " +
                                      "spells.duration, spells.components, spells.materials, spells.description " +
                                      "FROM warlockPacts " +
                                      "INNER JOIN warlockPactGainedSpells ON warlockPactGainedSpells.pactId = warlockPacts.pactId " +
                                      "INNER JOIN spells ON spells.spellId = warlockPactGainedSpells.spellId " +
                                      "WHERE warlockPacts.name = @Pact";
                command.Parameters.AddWithValue("@Pact", pact.Name);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            pactSpells.Add(new Spell(dbReader.GetString(0), dbReader.GetBoolean(1), dbReader.GetInt32(2), dbReader.GetString(3), dbReader.GetString(4), dbReader.GetString(5), dbReader.GetString(6), dbReader.GetString(7), dbReader.GetString(8), dbReader.GetString(9), false));
                        }
                    }
                }
            }

            return pactSpells;
        }

        /// <summary>
        /// checks, if a given class may choose eldritch invocations at the given level
        /// </summary>
        /// <param name="className">chosen class</param>
        /// <param name="level">current level</param>
        public bool hasEldritchInvocations(string className, int level)
        {
            return ((className == "Warlock") && (getEldritchInvocationAmount(level) > 0));
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

        ///// <summary>
        ///// gets a list of eldritch invocations the warlock may choose with the current level and pact choice
        ///// </summary>
        ///// <param name="warlockPact">chosen warlock pact</param>
        ///// <param name="level">current level</param>
        //public List<EldritchInvocation> getEldritchInvocations(WarlockPact warlockPact, int level)
        //{
        //    List<EldritchInvocation> invocations = new List<EldritchInvocation>();

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        connection.Open();
        //        command.CommandText = "SELECT eldritchInvocations.name, eldritchInvocations.description, eldritchInvocations.level, " +
        //                              "eldritchInvocations.pactRestriction, eldritchInvocations.hasSpellChoice, RequiredSpells.name, RequiredSpells.ritual, RequiredSpells.level, " +
        //                              "RequiredSpells.school, RequiredSpells.castTime, RequiredSpells.range, RequiredSpells.duration, " +
        //                              "RequiredSpells.components, RequiredSpells.materials, RequiredSpells.description, GainedSpells.name, " +
        //                              "GainedSpells.ritual, GainedSpells.level, GainedSpells.school, GainedSpells.castTime, GainedSpells.range, " +
        //                              "GainedSpells.duration, GainedSpells.components, GainedSpells.materials, GainedSpells.description " +
        //                              "FROM eldritchInvocations " +
        //                              "LEFT JOIN eldritchInvocationRequiredSpells ON eldritchInvocationRequiredSpells.invocationId = eldritchInvocations.invocationId " +
        //                              "LEFT JOIN eldritchInvocationGainedSpells ON eldritchInvocationGainedSpells.invocationId = eldritchInvocations.invocationId " +
        //                              "LEFT JOIN spells as RequiredSpells ON RequiredSpells.spellId = eldritchInvocationRequiredSpells.spellId " +
        //                              "LEFT JOIN spells as GainedSpells ON GainedSpells.spellId = eldritchInvocationGainedSpells.spellId " +
        //                              "WHERE (eldritchInvocations.pactRestriction = @NonPact OR eldritchInvocations.pactRestriction LIKE @Pact) " +
        //                              "AND eldritchInvocations.level BETWEEN 1 AND @Level";
        //        command.Parameters.AddWithValue("@NonPact", string.Empty);
        //        command.Parameters.AddWithValue("@Pact", warlockPact.Name);
        //        command.Parameters.AddWithValue("@Level", level.ToString());

        //        using (SQLiteDataReader dbReader = command.ExecuteReader())
        //        {
        //            while (dbReader.Read())
        //            {
        //                if (!dbReader.IsDBNull(0))
        //                {
        //                    Spell gainedSpell = new Spell();
        //                    if (!dbReader.IsDBNull(5))
        //                    {
        //                        gainedSpell = new Spell(dbReader.GetString(5), dbReader.GetBoolean(6), dbReader.GetInt32(7), dbReader.GetString(8), dbReader.GetString(9), dbReader.GetString(10), dbReader.GetString(11), dbReader.GetString(12), dbReader.GetString(13), dbReader.GetString(14), false);
        //                    }

        //                    Spell requiredSpell = new Spell();
        //                    if (!dbReader.IsDBNull(15))
        //                    {
        //                        requiredSpell = new Spell(dbReader.GetString(15), dbReader.GetBoolean(16), dbReader.GetInt32(17), dbReader.GetString(18), dbReader.GetString(19), dbReader.GetString(20), dbReader.GetString(21), dbReader.GetString(22), dbReader.GetString(23), dbReader.GetString(24), false);
        //                    }

        //                    invocations.Add(new EldritchInvocation(dbReader.GetString(0), dbReader.GetString(1), dbReader.GetInt32(2), requiredSpell, dbReader.GetString(3), gainedSpell, dbReader.GetBoolean(4)));
        //                }
        //            }
        //        }
        //    }

        //    return invocations;
        //}



        /// <summary>
        /// gets a list of eldritch invocations the warlock may choose with the current level and pact choice
        /// </summary>
        /// <param name="knownSpells">list of known spells</param>
        /// <param name="warlockPact">chosen warlock pact</param>
        /// <param name="level">current level</param>
        public List<EldritchInvocation> getEldritchInvocations(List<Spell> knownSpells, WarlockPact warlockPact, int level)
        {
            List<EldritchInvocation> invocations = new List<EldritchInvocation>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT eldritchInvocations.name, eldritchInvocations.description, eldritchInvocations.level, " +
                                      "eldritchInvocations.pactRestriction, eldritchInvocations.hasSpellChoice, RequiredSpells.name, RequiredSpells.ritual, RequiredSpells.level, " +
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
                                      "AND eldritchInvocations.level BETWEEN 1 AND @Level " +
                                      "AND (RequiredSpells.name IS NULL OR RequiredSpells.name IN (@KnownSpells))";
                command.Parameters.AddWithValue("@NonPact", string.Empty);
                command.Parameters.AddWithValue("@Pact", warlockPact.Name);
                command.Parameters.AddWithValue("@Level", level.ToString());
                SQLiteCommandExtensions.AddParametersWithValues(command, "@KnownSpells", knownSpells.Select(spell => spell.Name).ToList());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            Spell gainedSpell = new Spell();
                            if (!dbReader.IsDBNull(5))
                            {
                                gainedSpell = new Spell(dbReader.GetString(5), 
                                                        dbReader.GetBoolean(6), 
                                                        dbReader.GetInt32(7), 
                                                        dbReader.GetString(8), 
                                                        dbReader.GetString(9), 
                                                        dbReader.GetString(10), 
                                                        dbReader.GetString(11), 
                                                        dbReader.GetString(12), 
                                                        dbReader.GetString(13), 
                                                        dbReader.GetString(14), 
                                                        false);
                            }

                            Spell requiredSpell = new Spell();
                            if (!dbReader.IsDBNull(15))
                            {
                                requiredSpell = new Spell(dbReader.GetString(15), 
                                                          dbReader.GetBoolean(16), 
                                                          dbReader.GetInt32(17), 
                                                          dbReader.GetString(18), 
                                                          dbReader.GetString(19), 
                                                          dbReader.GetString(20), 
                                                          dbReader.GetString(21), 
                                                          dbReader.GetString(22), 
                                                          dbReader.GetString(23), 
                                                          dbReader.GetString(24), 
                                                          false);
                            }

                            invocations.Add(new EldritchInvocation(dbReader.GetString(0), 
                                                                   dbReader.GetString(1), 
                                                                   dbReader.GetInt32(2), 
                                                                   requiredSpell, 
                                                                   dbReader.GetString(3), 
                                                                   gainedSpell, 
                                                                   dbReader.GetBoolean(4), 
                                                                   hasInvocationSkillGain(dbReader.GetString(0))));
                        }
                    }
                }
            }

            return invocations;
        }

        /// <summary>
        /// checks, if a given eldritch invocation includes a spell choice
        /// </summary>
        /// <param name="invocation">selected eldritch invocation</param>
        public bool hasInvocationSpellChoice(EldritchInvocation invocation)
        {
            return (invocation.Name == "Book of Ancient Secrets");
        }

        /// <summary>
        /// gets the number of invocation spells a given invocation grants
        /// </summary>
        /// <param name="invocation">selected invocation</param>
        public int invocationSpellChoiceAmount(EldritchInvocation invocation)
        {
            if (invocation.Name == "Book of Ancient Secrets")
            {
                return 2;
            }

            return 0;
        }

        /// <summary>
        /// gets a list of spells for the choice of a given eldritch invocation
        /// </summary>
        /// <param name="invocation">selected eldritch invocation</param>
        public List<Spell> getInvocationSpellOptions(EldritchInvocation invocation)
        {
            List<Spell> spellList = new List<Spell>();

            if (invocation.Name == "Book of Ancient Secrets")
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    connection.Open();
                    command.CommandText = "SELECT name, ritual, level, school, castTime, range, duration, components, materials, description FROM spells " +
                                          "INNER JOIN books ON books.bookid=spells.book " +
                                          "WHERE level = 1 AND ritual = 1 " +
                                          "AND books.title IN (@UsedBooks)";
                    SQLiteCommandExtensions.AddParametersWithValues(command, "@UsedBooks", UsedBooks);

                    using (SQLiteDataReader dbReader = command.ExecuteReader())
                    {
                        while (dbReader.Read())
                        {
                            if (!dbReader.IsDBNull(0))
                            {
                                spellList.Add(new Spell(dbReader.GetString(0), dbReader.GetBoolean(1), dbReader.GetInt32(2), dbReader.GetString(3), dbReader.GetString(4), dbReader.GetString(5), dbReader.GetString(6), dbReader.GetString(6), dbReader.GetString(8), dbReader.GetString(9), false));
                            }
                        }
                    }
                }
            }

            return spellList;
        }

        /// <summary>
        /// checks, if the eldritch invocation with the given name is connected with a skill proficiency gain
        /// </summary>
        /// <param name="invocationName">name of eldritch invocation</param>
        private bool hasInvocationSkillGain(string invocationName)
        {
            bool hasSkillGain = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT skills.name FROM eldritchInvocations " +
                                      "INNER JOIN eldritchInvocationsGainedSkills ON eldritchInvocationsGainedSkills.invocationId = eldritchInvocations.invocationId " +
                                      "INNER JOIN skills ON skills.skillId = eldritchInvocationsGainedSkills.skillId " +
                                      "WHERE eldritchInvocations.name = @Invocation";
                command.Parameters.AddWithValue("@Invocation", invocationName);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        hasSkillGain = !dbReader.IsDBNull(0);
                    }
                }
            }

            return hasSkillGain;
        }

        /// <summary>
        /// gets a list of skills the invocation gains
        /// </summary>
        /// <param name="invocation">chosen invocation</param>
        public List<string> getInvocationGainedSkills(EldritchInvocation invocation)
        {
            List<string> gainedSkills = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT skills.name FROM eldritchInvocations " +
                                      "INNER JOIN eldritchInvocationsGainedSkills ON eldritchInvocationsGainedSkills.invocationId = eldritchInvocations.invocationId " +
                                      "INNER JOIN skills ON skills.skillId = eldritchInvocationsGainedSkills.skillId " +
                                      "WHERE eldritchInvocations.name = @Invocation";
                command.Parameters.AddWithValue("@Invocation", invocation.Name);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            gainedSkills.Add(dbReader.GetString(0));
                        }
                    }
                }
            }

            return gainedSkills;
        }
    }
}
