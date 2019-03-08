using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using Easy_DnD_Character_Creator.DataManagement;
using Easy_DnD_Character_Creator.DataTypes;

namespace Easy_DnD_Character_Creator
{
    public class DataManager
    {
        private string ConnectionString { get; }
        public List<string> UsedBooks { get; private set; }

        public RaceDataManager RaceData { get; }
        public AppearanceDataManager AppearanceData { get; }
        public ClassDataManager ClassData { get; }
        public BackgroundDataManager BackgroundData { get; }
        public AbilityDataManager AbilityData { get; }
        public LanguageDataManager LanguageData { get; }
        public SkillDataManager SkillData { get; }
        public EquipmentDataManager EquipmentData { get; }
        public SpellDataManager SpellData { get; }
        public ExtraRaceChoiceDataManager ExtraRaceChoiceData { get; }
        public ExtraClassChoiceDataManager ExtraClassChoiceData { get; }
        public ExtraSubclassChoiceDataManager ExtraSubclassChoiceData { get; }

        public DataManager()
        {
            ConnectionString = "Data Source=";
            ConnectionString += Directory.GetCurrentDirectory();
            ConnectionString += "\\data.sqlite;Version=3;Read Only=True;";

            UsedBooks = new List<string>();
            UsedBooks.Add("Player's Handbook");

            RaceData = new RaceDataManager(ConnectionString, UsedBooks);
            AppearanceData = new AppearanceDataManager(ConnectionString, UsedBooks);
            ClassData = new ClassDataManager(ConnectionString, UsedBooks);
            BackgroundData = new BackgroundDataManager(ConnectionString, UsedBooks);
            AbilityData = new AbilityDataManager(ConnectionString, UsedBooks);
            LanguageData = new LanguageDataManager(ConnectionString, UsedBooks);
            SkillData = new SkillDataManager(ConnectionString, UsedBooks);
            EquipmentData = new EquipmentDataManager(ConnectionString, UsedBooks);
            SpellData = new SpellDataManager(ConnectionString, UsedBooks);
            ExtraRaceChoiceData = new ExtraRaceChoiceDataManager(ConnectionString, UsedBooks);
            ExtraClassChoiceData = new ExtraClassChoiceDataManager(ConnectionString, UsedBooks);
            ExtraSubclassChoiceData = new ExtraSubclassChoiceDataManager(ConnectionString, UsedBooks);
        }

        public void setUsedBooks(List<string> inputUsedBooks)
        {
            UsedBooks = inputUsedBooks;
            RaceData.UsedBooks = inputUsedBooks;
            AppearanceData.UsedBooks = inputUsedBooks;
            ClassData.UsedBooks = inputUsedBooks;
            BackgroundData.UsedBooks = inputUsedBooks;
            AbilityData.UsedBooks = inputUsedBooks;
            LanguageData.UsedBooks = inputUsedBooks;
            SkillData.UsedBooks = inputUsedBooks;
            EquipmentData.UsedBooks = inputUsedBooks;
            SpellData.UsedBooks = inputUsedBooks;
            ExtraRaceChoiceData.UsedBooks = inputUsedBooks;
            ExtraClassChoiceData.setUsedBooks(inputUsedBooks);
            ExtraSubclassChoiceData.UsedBooks = inputUsedBooks;
        }

        /// <summary>
        /// returns a list of all active books
        /// </summary>
        public List<Book> getActiveBooks()
        {
            List<Book> activeBooks = new List<Book>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM books WHERE active = 1";
                
                using (SQLiteDataReader dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (!dbReader.IsDBNull(0))
                        {
                            activeBooks.Add(new Book(dbReader.GetInt32(0), dbReader.GetString(1), dbReader.GetString(2), dbReader.GetInt32(3), dbReader.GetInt32(4)));
                        }
                    }
                }
            }

            return activeBooks;
        }

        //public bool hasExtraChoices(string subrace, string className, string subclass, int level)
        //{
        //    return ExtraRaceChoiceData.hasExtraRaceChoices(subrace) || ExtraClassChoiceData.hasExtraClassChoices(className, level) || ExtraSubclassChoiceData.hasExtraSubclassChoices(subclass, level);
        //}
    }
}