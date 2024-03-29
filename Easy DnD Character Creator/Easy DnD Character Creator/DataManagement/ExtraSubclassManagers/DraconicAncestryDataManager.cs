﻿using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement.ExtraSubclassManagers
{
    public class DraconicAncestryDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public DraconicAncestryDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// gets a list of all possible dragon ancestries
        /// </summary>
        public List<DraconicAncestry> getDraconicAncestries()
        {
            List<DraconicAncestry> ancestries = new List<DraconicAncestry>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT dragon, damageType FROM draconicAncestry";

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            ancestries.Add(new DraconicAncestry(dbReader.GetString(0), dbReader.GetString(1)));
                        }
                    }
                }
            }

            return ancestries;
        }
    }
}
