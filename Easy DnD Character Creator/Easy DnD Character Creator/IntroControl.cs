using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_DnD_Character_Creator
{
    public partial class IntroControl : UserControl
    {
        private DataManager dm;

        private FlowLayoutPanel introPanel;
        private Label introLabel;

        private GroupBox bookBox;
        private List<CheckBox> bookCheckBoxes;

        private GroupBox presetBox;
        private RadioButton lenientButton;
        private RadioButton averageButton;
        private RadioButton puristButton;

        private GroupBox settingsBox;
        private Label levelLabel;
        private NumericUpDown characterLevel;
        private CheckBox moneyCheckbox;

        public IntroControl(DataManager inputDataManager)
        {
            dm = inputDataManager;
            bookCheckBoxes = new List<CheckBox>();
            InitializeComponent();
        }

        /// <summary>
        /// returns the chosen creation preset: 
        /// 0 for lenient, 1 for average, 2 for purist
        /// </summary>
        public int getPreset()
        {
            if (lenientButton.Checked)
            {
                return 0;
            }

            if (averageButton.Checked)
            {
                return 1;
            }

            return 2;
        }

        /// <summary>
        /// returns selected books formatted for usage in SQLite queries
        /// </summary>
        public string getUsedBooks()
        {
            string usedBooks = "";
            foreach (CheckBox checkBox in bookCheckBoxes)
            {
                if (checkBox.Checked)
                {
                    if (usedBooks != "")
                    {
                        usedBooks += ", ";
                    }

                    usedBooks += "\"";
                    usedBooks += checkBox.Text;
                    usedBooks += "\"";
                }
            }
            return usedBooks;
        }

        public void InitializeComponent()
        {
            introPanel = new FlowLayoutPanel();
            introPanel.FlowDirection = FlowDirection.LeftToRight;
            introPanel.AutoSize = true;
            introPanel.WrapContents = true;

            introLabel = new Label();
            introLabel.AutoSize = true;
            introLabel.Font = new Font(introLabel.Font.FontFamily, 14);
            introLabel.Text = "Please select the used books, creation preset and character level.";
            introLabel.Name = "introLabel";
            introPanel.Controls.Add(introLabel);
            introPanel.SetFlowBreak(introLabel, true);

            //fill book checkboxes
            bookBox = new GroupBox();
            bookBox.AutoSize = true;
            bookBox.Text = "Sourcebooks";
            bookBox.Name = "bookBox";
            List<Book> activeBooks = dm.getActiveBooks();

            int xCoord = 3;
            int yCoord = 23;
            int yStep = 20;

            foreach (Book book in activeBooks)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.AutoSize = true;
                checkBox.Text = book.Title;
                checkBox.Name = book.Shorthand + "Box";
                checkBox.Location = new Point(xCoord, yCoord);
                if (book.Mandatory)
                {
                    checkBox.Checked = true;
                    checkBox.Enabled = false;
                }
                bookCheckBoxes.Add(checkBox);
                bookBox.Controls.Add(checkBox);
                yCoord += yStep;
            }

            introPanel.Controls.Add(bookBox);


            //fill preset box
            presetBox = new GroupBox();
            presetBox.AutoSize = true;
            presetBox.Text = "Creation Presets";
            presetBox.Name = "presetBox";

            lenientButton = new RadioButton();
            lenientButton.AutoSize = true;
            lenientButton.Text = "Lenient Character Creation";
            lenientButton.Name = "lenientButton";
            lenientButton.Checked = true;
            lenientButton.Location = new Point(3, 23);
            presetBox.Controls.Add(lenientButton);

            averageButton = new RadioButton();
            averageButton.AutoSize = true;
            averageButton.Text = "Average Character Creation";
            averageButton.Name = "averageButton";
            averageButton.Location = new Point(3, 43);
            presetBox.Controls.Add(averageButton);

            puristButton = new RadioButton();
            puristButton.AutoSize = true;
            puristButton.Text = "Purist Character Creation";
            puristButton.Name = "puristButton";
            puristButton.Location = new Point(3, 63);
            presetBox.Controls.Add(puristButton);

            introPanel.Controls.Add(presetBox);

            //fill settings box
            settingsBox = new GroupBox();
            settingsBox.AutoSize = true;
            settingsBox.Text = "Character Level && Starting Money";
            settingsBox.Name = "settingsBox";

            levelLabel = new Label();
            levelLabel.Location = new Point(3, 25);
            levelLabel.AutoSize = true;
            levelLabel.Text = "Level:";
            levelLabel.Name = "levelLabel";
            settingsBox.Controls.Add(levelLabel);

            characterLevel = new NumericUpDown();
            characterLevel.Location = new Point(3 + levelLabel.Width, 23);
            //characterLevel.Size = new Size(50, 30);
            characterLevel.Minimum = 1;
            characterLevel.Maximum = 20;
            characterLevel.Value = 1;
            characterLevel.ReadOnly = true;
            settingsBox.Controls.Add(characterLevel);

            moneyCheckbox = new CheckBox();
            moneyCheckbox.AutoSize = true;
            moneyCheckbox.Location = new Point(3, 53);
            moneyCheckbox.Text = "Adjust starting money to character level";
            moneyCheckbox.Name = "moneyCheckbox";
            moneyCheckbox.Checked = true;
            settingsBox.Controls.Add(moneyCheckbox);

            introPanel.Controls.Add(settingsBox);

            Controls.Add(introPanel);
            Size = new Size(950, 550);
        }
    }
}
