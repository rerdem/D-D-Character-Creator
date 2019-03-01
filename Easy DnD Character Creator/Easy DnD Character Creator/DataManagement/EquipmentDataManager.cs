using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public class EquipmentDataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; set; }

        public EquipmentDataManager(string inputConnectionString, List<string> inputUsedBooks)
        {
            ConnectionString = inputConnectionString;
            UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// gets the number of equipment choices the specified class must make
        /// </summary>
        /// <param name="className">chosen class</param>
        public int getEquipmentChoiceAmount(string className)
        {
            int choiceAmount = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT choices FROM equipmentChoices " +
                                      "INNER JOIN classes ON classes.classid = equipmentChoices.classId " +
                                      "WHERE classes.name=@Class";
                command.Parameters.AddWithValue("@Class", className);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            choiceAmount = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return choiceAmount;
        }

        /// <summary>
        /// gets the number of items the specified class can choose for their 2nd choice
        /// </summary>
        /// <param name="className">chosen class</param>
        public int getEquipment2ndSelectionAmount(string className)
        {
            int selectionAmount = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT choice2Amount FROM equipmentChoices " +
                                      "INNER JOIN classes ON classes.classid = equipmentChoices.classId " +
                                      "WHERE classes.name=@Class";
                command.Parameters.AddWithValue("@Class", className);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            selectionAmount = dbReader.GetInt32(0);
                        }
                    }
                }
            }

            return selectionAmount;
        }

        /// <summary>
        /// gets a list of available choices for the specified parameters
        /// </summary>
        /// <param name="subrace">chosen subrace</param>
        /// <param name="className">chosen class</param>
        /// <param name="subclass">chosen subclass</param>
        /// <param name="choice">which set of options should be returned</param>
        /// <param name="strength">character's strength</param>
        public List<EquipmentItem> getEquipmentChoices(string subrace, string className, string subclass, int choice, int strength)
        {
            List<EquipmentItem> outputList = new List<EquipmentItem>();

            if (choice <= getEquipmentChoiceAmount(className))
            {
                //Barbarian
                if (className == "Barbarian")
                {
                    if (choice == 1)
                    {
                        outputList.AddRange(getWeaponList("martial", "melee").ToArray());
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getWeaponList("simple", "*").ToArray());
                        outputList[outputList.FindIndex(item => item.Name.Equals("handaxe"))].Name = "handaxe (2)";
                    }
                }

                //Bard
                if (className == "Bard")
                {
                    if (choice == 1)
                    {
                        outputList.Add(getWeapon("rapier"));
                        outputList.Add(getWeapon("longsword"));
                        outputList.AddRange(getWeaponList("simple", "*").ToArray());
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getPackChoices(className));
                    }

                    if (choice == 3)
                    {
                        outputList.AddRange(getToolList("musical instrument").ToArray());
                    }
                }

                //Cleric
                if (className == "Cleric")
                {
                    if (choice == 1)
                    {
                        outputList.AddRange(getWeaponList("mace").ToArray());

                        if ((subrace == "Hill Dwarf") || (subrace == "Mountain Dwarf")
                            || (subclass == "Tempest Domain") || (subclass == "War Domain"))
                        {
                            outputList.AddRange(getWeaponList("warhammer").ToArray());
                        }
                    }

                    if (choice == 2)
                    {
                        outputList.Add(getArmor("scale mail armor"));
                        outputList.Add(getArmor("leather armor"));

                        Armor armorForStrengthCheck = getArmor("chain mail armor");
                        if (strength > armorForStrengthCheck.StrengthRequirement)
                        {
                            if ((subclass == "Life Domain") || (subclass == "Nature Domain")
                             || (subclass == "Tempest Domain") || (subclass == "War Domain"))
                            {
                                outputList.Add(armorForStrengthCheck);
                            }
                        }
                    }

                    if (choice == 3)
                    {
                        outputList.Add(getWeapon("light crossbow"));
                        outputList.AddRange(getWeaponList("simple", "*").ToArray());
                    }

                    if (choice == 4)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }

                //Druid
                if (className == "Druid")
                {
                    if (choice == 1)
                    {
                        outputList.AddRange(getWeaponList("simple", "*").ToArray());
                        outputList.Add(getArmor("shield"));
                    }

                    if (choice == 2)
                    {
                        outputList.Add(getWeapon("scimitar"));
                        outputList.AddRange(getWeaponList("simple", "melee").ToArray());
                    }
                }

                //Fighter
                if (className == "Fighter")
                {
                    if (choice == 1)
                    {
                        Armor armorForStrengthCheck = getArmor("chain mail armor");
                        if (strength > armorForStrengthCheck.StrengthRequirement)
                        {
                            outputList.Add(armorForStrengthCheck);
                        }

                        MultiItem multiItem = new MultiItem();
                        multiItem.addItem(getArmor("leather armor"));
                        multiItem.addItem(getWeapon("longbow"));
                        outputList.Add(multiItem);

                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getWeaponList("martial", "*").ToArray());
                        outputList.Add(getArmor("shield"));
                    }

                    if (choice == 3)
                    {
                        outputList.Add(getWeapon("light crossbow"));
                        outputList.Add(getWeapon("handaxe"));
                        outputList[outputList.FindIndex(item => item.Name.Equals("handaxe"))].Name = "handaxe (2)";
                    }

                    if (choice == 4)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }

                //Monk
                if (className == "Monk")
                {
                    if (choice == 1)
                    {
                        outputList.Add(getWeapon("shortsword"));
                        outputList.AddRange(getWeaponList("simple", "*").ToArray());
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }

                //Paladin
                if (className == "Paladin")
                {
                    if (choice == 1)
                    {
                        outputList.AddRange(getWeaponList("simple", "melee").ToArray());
                        outputList[outputList.FindIndex(item => item.Name.Equals("javelin"))].Name = "javelin (5)";
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getWeaponList("martial", "*").ToArray());
                        outputList.Add(getArmor("shield"));
                    }

                    if (choice == 3)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }

                //Ranger
                if (className == "Ranger")
                {
                    if (choice == 1)
                    {
                        outputList.Add(getArmor("scale mail armor"));
                        outputList.Add(getArmor("leather armor"));
                    }

                    if (choice == 2)
                    {
                        outputList.Add(getWeapon("shortsword"));
                        outputList.AddRange(getWeaponList("simple", "melee").ToArray());
                    }

                    if (choice == 3)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }

                //Rogue
                if (className == "Rogue")
                {
                    if (choice == 1)
                    {
                        outputList.Add(getWeapon("rapier"));
                        outputList.Add(getWeapon("shortsword"));
                    }

                    if (choice == 2)
                    {
                        outputList.Add(getWeapon("shortbow"));
                        outputList.Add(getWeapon("shortsword"));
                    }

                    if (choice == 3)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }

                //Sorcerer
                if (className == "Sorcerer")
                {
                    if (choice == 1)
                    {
                        outputList.Add(getWeapon("light crossbow"));
                        outputList.AddRange(getWeaponList("simple", "*").ToArray());
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getToolList("arcane tool").ToArray());
                    }

                    if (choice == 3)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }

                //Warlock
                if (className == "Warlock")
                {
                    if (choice == 1)
                    {
                        outputList.Add(getWeapon("light crossbow"));
                        outputList.AddRange(getWeaponList("simple", "*").ToArray());
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getToolList("arcane tool").ToArray());
                    }

                    if (choice == 3)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }

                //Wizard
                if (className == "Wizard")
                {
                    if (choice == 1)
                    {
                        outputList.Add(getWeapon("quarterstaff"));
                        outputList.Add(getWeapon("dagger"));
                    }

                    if (choice == 2)
                    {
                        outputList.AddRange(getToolList("arcane tool").ToArray());
                    }

                    if (choice == 3)
                    {
                        outputList.AddRange(getPackChoices(className).ToArray());
                    }
                }
            }
            return outputList;
        }

        /// <summary>
        /// gets additional default equipment for the specified class
        /// </summary>
        /// <param name="className">chosen class</param>
        public string getExtraEquipment(string className)
        {
            string extraEquipment = "";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT equipment FROM extraEquipment " +
                                      "INNER JOIN classes ON extraEquipment.classId=classes.classid " +
                                      "WHERE classes.name=@Class";
                command.Parameters.AddWithValue("@Class", className);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            extraEquipment = dbReader.GetString(0);
                        }
                    }
                }
            }

            return extraEquipment;
        }

        /// <summary>
        /// gets a list of weapons with the specified type and range
        /// </summary>
        /// <param name="type">type: simple, martial or wildcard *</param>
        /// <param name="range">range: melee, ranged or wildcard *</param>
        public List<Weapon> getWeaponList(string type, string range)
        {
            List<Weapon> weapons = new List<Weapon>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT name, damage, damageType, properties FROM weapons " +
                                      "INNER JOIN books ON weapons.book = books.bookid " +
                                      "WHERE type LIKE @Type AND range LIKE @Range " +
                                      "AND books.title IN (@UsedBooks)";
                //replace * with % for sqlite wildcard
                command.Parameters.AddWithValue("@Type", type.Replace('*', '%'));
                command.Parameters.AddWithValue("@Range", range.Replace('*', '%'));
                SQLiteCommandExtensions.AddParametersWithValues(command, "@UsedBooks", UsedBooks);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            weapons.Add(new Weapon(dbReader.GetString(0), dbReader.GetString(1), dbReader.GetString(2), dbReader.GetString(3)));
                        }
                    }
                }
            }

            return weapons;
        }

        /// <summary>
        /// gets a list of weapons with the specified name
        /// </summary>
        /// <param name="name">name of weapon to get</param>
        public List<Weapon> getWeaponList(string name)
        {
            List<Weapon> weapons = new List<Weapon>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT name, damage, damageType, properties FROM weapons " +
                                      "INNER JOIN books ON weapons.book = books.bookid " +
                                      "WHERE name LIKE @Weapon " +
                                      "AND books.title IN (@UsedBooks)";
                //replace * with % for sqlite wildcard
                command.Parameters.AddWithValue("@Weapon", name.Replace('*', '%'));
                SQLiteCommandExtensions.AddParametersWithValues(command, "@UsedBooks", UsedBooks);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            weapons.Add(new Weapon(dbReader.GetString(0), dbReader.GetString(1), dbReader.GetString(2), dbReader.GetString(3)));
                        }
                    }
                }
            }

            return weapons;
        }

        ///// <summary>
        ///// checks, if the specified item is a weapon
        ///// </summary>
        ///// <param name="name">item to check</param>
        //public bool isWeapon(string name)
        //{
        //    bool isWeapon = false;

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        connection.Open();
        //        command.CommandText = "SELECT * FROM weapons " +
        //                              "WHERE name=@Weapon";
        //        command.Parameters.AddWithValue("@Weapon", name);

        //        using (SQLiteDataReader dbReader = command.ExecuteReader())
        //        {
        //            while (dbReader.Read())
        //            {
        //                isWeapon = !dbReader.IsDBNull(0);
        //            }
        //        }
        //    }

        //    return isWeapon;
        //}

        /// <summary>
        /// gets the weapon object with the specified name
        /// </summary>
        /// <param name="name">chosen weapon</param>
        public Weapon getWeapon(string name)
        {
            Weapon weapon = new Weapon();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT name, damage, damageType, properties FROM weapons " +
                                      "WHERE name=@Weapon";
                command.Parameters.AddWithValue("@Weapon", name);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            weapon = new Weapon(dbReader.GetString(0), dbReader.GetString(1), dbReader.GetString(2), dbReader.GetString(3));
                        }
                    }
                }
            }

            return weapon;
        }

        /// <summary>
        /// gets a list of armors with the specified name or of of the specified type
        /// </summary>
        /// <param name="name">chosen name or wildcard *</param>
        /// <param name="type">chosen type or wildcard *</param>
        public List<Armor> getArmorList(string name, string type)
        {
            List<Armor> armor = new List<Armor>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM armor " +
                                      "INNER JOIN books ON armor.book = books.bookid " +
                                      "WHERE name LIKE @Armor AND type LIKE @Type " +
                                      "AND books.title IN (@UsedBooks)";
                //replace * with % for sqlite wildcard
                command.Parameters.AddWithValue("@Armor", name.Replace('*', '%'));
                command.Parameters.AddWithValue("@Type", type.Replace('*', '%'));
                SQLiteCommandExtensions.AddParametersWithValues(command, "@UsedBooks", UsedBooks);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            bool hasStealthDisadvantage = (dbReader.GetString(8) != "-");
                            armor.Add(new Armor(dbReader.GetString(0), dbReader.GetString(1), dbReader.GetInt32(3), dbReader.GetInt32(4), dbReader.GetString(5), dbReader.GetInt32(6), dbReader.GetInt32(7), hasStealthDisadvantage));
                        }
                    }
                }
            }

            return armor;
        }

        ///// <summary>
        ///// checks, if the specified item is armor
        ///// </summary>
        ///// <param name="name">item to check</param>
        //public bool isArmor(string name)
        //{
        //    bool isArmor = false;

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        connection.Open();
        //        command.CommandText = "SELECT * FROM armor " +
        //                              "WHERE name=@Armor";
        //        command.Parameters.AddWithValue("@Armor", name);

        //        using (SQLiteDataReader dbReader = command.ExecuteReader())
        //        {
        //            if (dbReader.Read())
        //            {
        //                isArmor = !dbReader.IsDBNull(0);
        //            }
        //        }
        //    }

        //    return isArmor;
        //}

        /// <summary>
        /// gets the armor object with the specified name
        /// </summary>
        /// <param name="name">chosen armor</param>
        public Armor getArmor(string name)
        {
            Armor armor = new Armor();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM armor " +
                                      "WHERE name=@Armor";
                command.Parameters.AddWithValue("@Armor", name);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            bool hasStealthDisadvantage = (dbReader.GetString(8) != "-");
                            armor = new Armor(dbReader.GetString(0), dbReader.GetString(1), dbReader.GetInt32(3), dbReader.GetInt32(4), dbReader.GetString(5), dbReader.GetInt32(6), dbReader.GetInt32(7), hasStealthDisadvantage);
                        }
                    }
                }
            }

            return armor;
        }

        ///// <summary>
        ///// gets the strength requirement for the specified armore
        ///// </summary>
        ///// <param name="name">chosen armor</param>
        ///// <returns>the strength requirement or 99 when armor not found in database</returns>
        //public int getArmorStrengthRequirement(string name)
        //{
        //    //high value to prevent usage of armor that is not in database
        //    int strengthReq = 99;

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        connection.Open();
        //        command.CommandText = "SELECT strength FROM armor " +
        //                              "WHERE name=@Armor";
        //        command.Parameters.AddWithValue("@Armor", name);

        //        using (SQLiteDataReader dbReader = command.ExecuteReader())
        //        {
        //            if (dbReader.Read())
        //            {
        //                if (!dbReader.IsDBNull(0))
        //                {
        //                    strengthReq = dbReader.GetInt32(0);
        //                }
        //            }
        //        }
        //    }

        //    return strengthReq;
        //}

        /// <summary>
        /// gets a list of tools of the specified type
        /// </summary>
        /// <param name="type">type of tools to return</param>
        public List<Tool> getToolList(string type)
        {
            List<Tool> tools = new List<Tool>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT name FROM tools " +
                                      "WHERE type=@Type";
                command.Parameters.AddWithValue("@Type", type);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            tools.Add(new Tool(dbReader.GetString(0)));
                        }
                    }
                }
            }

            return tools;
        }

        /// <summary>
        /// gets a list of packs the specified class can choose from
        /// </summary>
        /// <param name="className">chosen class</param>
        public List<Pack> getPackChoices(string className)
        {
            List<Pack> packs = new List<Pack>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT equipmentPacks.name, equipmentPacks.content FROM equipmentPacks " +
                                      "INNER JOIN equipmentPackChoices ON equipmentPacks.packId=equipmentPackChoices.packId " +
                                      "INNER JOIN classes ON equipmentPackChoices.classId=classes.classid " +
                                      "WHERE classes.name=@Class";
                command.Parameters.AddWithValue("@Class", className);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            packs.Add(new Pack(dbReader.GetString(0), dbReader.GetString(1)));
                        }
                    }
                }
            }

            return packs;
        }

        /// <summary>
        /// gets the content of the specified equipment pack
        /// </summary>
        /// <param name="pack">chosen equipment pack</param>
        public string getPackContent(string pack)
        {
            string content = "";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT content FROM equipmentPacks " +
                                      "WHERE name=@Pack";
                command.Parameters.AddWithValue("@Pack", pack);

                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            content = dbReader.GetString(0);
                        }
                    }
                }
            }

            return content;
        }

        ///// <summary>
        ///// checks, if a given item is an equipment pack
        ///// </summary>
        ///// <param name="pack">item to check</param>
        //public bool isPack(string pack)
        //{
        //    bool isPack = false;

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        connection.Open();
        //        command.CommandText = "SELECT * FROM equipmentPacks " +
        //                              "WHERE name=@Pack";
        //        command.Parameters.AddWithValue("@Pack", pack);

        //        using (SQLiteDataReader dbReader = command.ExecuteReader())
        //        {
        //            if (dbReader.Read())
        //            {
        //                isPack = !dbReader.IsDBNull(0);
        //            }
        //        }
        //    }

        //    return isPack;
        //}

        //public string getWeaponType(string name)
        //{
        //    string type = "";

        //    DBConnection.Open();
        //    SQLiteDataReader dbReader;
        //    SQLiteCommand dbQuery;
        //    dbQuery = DBConnection.CreateCommand();
        //    dbQuery.CommandText = "SELECT type FROM weapons " +
        //                          "WHERE name=\"";
        //    dbQuery.CommandText += name;
        //    dbQuery.CommandText += "\"";

        //    dbReader = dbQuery.ExecuteReader();
        //    if (dbReader.Read())
        //    {
        //        if (!dbReader.IsDBNull(0))
        //        {
        //            type = dbReader.GetString(0);
        //        }
        //    }
        //    DBConnection.Close();

        //    return type;
        //}

        //public string getWeaponDamage(string name)
        //{
        //    string damage = "";

        //    DBConnection.Open();
        //    SQLiteDataReader dbReader;
        //    SQLiteCommand dbQuery;
        //    dbQuery = DBConnection.CreateCommand();
        //    dbQuery.CommandText = "SELECT damage FROM weapons " +
        //                          "WHERE name=\"";
        //    dbQuery.CommandText += name;
        //    dbQuery.CommandText += "\"";

        //    dbReader = dbQuery.ExecuteReader();
        //    if (dbReader.Read())
        //    {
        //        if (!dbReader.IsDBNull(0))
        //        {
        //            damage = dbReader.GetString(0);
        //        }
        //    }
        //    DBConnection.Close();

        //    return damage;
        //}

        //public string getWeaponDamageType(string name)
        //{
        //    string damageType = "";

        //    DBConnection.Open();
        //    SQLiteDataReader dbReader;
        //    SQLiteCommand dbQuery;
        //    dbQuery = DBConnection.CreateCommand();
        //    dbQuery.CommandText = "SELECT damageType FROM weapons " +
        //                          "WHERE name=\"";
        //    dbQuery.CommandText += name;
        //    dbQuery.CommandText += "\"";

        //    dbReader = dbQuery.ExecuteReader();
        //    if (dbReader.Read())
        //    {
        //        if (!dbReader.IsDBNull(0))
        //        {
        //            damageType = dbReader.GetString(0);
        //        }
        //    }
        //    DBConnection.Close();

        //    return damageType;
        //}

        //public string getWeaponProperties(string name)
        //{
        //    string properties = "";

        //    DBConnection.Open();
        //    SQLiteDataReader dbReader;
        //    SQLiteCommand dbQuery;
        //    dbQuery = DBConnection.CreateCommand();
        //    dbQuery.CommandText = "SELECT properties FROM weapons " +
        //                          "WHERE name=\"";
        //    dbQuery.CommandText += name;
        //    dbQuery.CommandText += "\"";

        //    dbReader = dbQuery.ExecuteReader();
        //    if (dbReader.Read())
        //    {
        //        if (!dbReader.IsDBNull(0))
        //        {
        //            properties = dbReader.GetString(0);
        //        }
        //    }
        //    DBConnection.Close();

        //    return properties;
        //}
    }
}
