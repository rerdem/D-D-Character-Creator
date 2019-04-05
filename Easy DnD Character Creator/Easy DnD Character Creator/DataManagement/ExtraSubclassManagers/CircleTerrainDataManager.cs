using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement.ExtraSubclassManagers
{
    public class CircleTerrainDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public CircleTerrainDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// gets a list of all circle terrains for the given subclass/level
        /// </summary>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="level">current level</param>
        public List<CircleTerrain> getCircleTerrains(string subclass, int level)
        {
            List<CircleTerrain> terrainList = new List<CircleTerrain>();
            CircleTerrain currentTerrain = new CircleTerrain();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT druidCircleTerrains.terrain, spells.name, spells.ritual, spells.level, spells.school, " +
                                      "spells.castTime, spells.range, spells.duration, spells.components, spells.materials, spells.description " +
                                      "FROM druidCircleTerrains " +
                                      "INNER JOIN subclasses ON subclasses.subclassId = druidCircleTerrains.subclassId " +
                                      "INNER JOIN druidCircleTerrainSpells ON druidCircleTerrainSpells.terrainId = druidCircleTerrains.terrainId " +
                                      "INNER JOIN spells ON spells.spellId = druidCircleTerrainSpells.spellId " +
                                      "WHERE subclasses.name = @Subclass AND druidCircleTerrainSpells.level BETWEEN 1 AND @Level";
                command.Parameters.AddWithValue("@Subclass", subclass);
                command.Parameters.AddWithValue("@Level", level.ToString());

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            if (currentTerrain.Name != dbReader.GetString(0))
                            {
                                if (!string.IsNullOrEmpty(currentTerrain.Name))
                                {
                                    terrainList.Add(currentTerrain);
                                }
                                currentTerrain = new CircleTerrain(dbReader.GetString(0));
                            }
                            if (!dbReader.IsDBNull(1))
                            {
                                currentTerrain.addSpell(new Spell(dbReader.GetString(1), dbReader.GetBoolean(2), dbReader.GetInt32(3), dbReader.GetString(4), dbReader.GetString(5), dbReader.GetString(6), dbReader.GetString(7), dbReader.GetString(8), dbReader.GetString(9), dbReader.GetString(10), false));
                            }
                        }
                    }

                    if (!terrainList.Contains(currentTerrain) && !string.IsNullOrEmpty(currentTerrain.Name))
                    {
                        terrainList.Add(currentTerrain);
                    }
                }
            }

            return terrainList;
        }
    }
}
