using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public partial class IntroControl : UserControl, IWizardControl
    {
        private WizardManager wm;

        private FlowLayoutPanel introPanel;
        
        private GroupBox bookBox;
        private List<CheckBox> bookCheckBoxes;

        private GroupBox presetBox;
        private RadioButton lenientButton;
        private RadioButton averageButton;
        private RadioButton puristButton;

        private ToolTip toolTips;

        private GroupBox settingsBox;
        private Label levelLabel;
        private NumericUpDown characterLevel;
        private CheckBox moneyCheckbox;

        private bool visited;
        public bool Visited
        {
            get
            {
                return visited;
            }
            set
            {
                visited = value;
            }
        }

        public IntroControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            bookCheckBoxes = new List<CheckBox>();
            visited = false;
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            introPanel = new FlowLayoutPanel();
            introPanel.FlowDirection = FlowDirection.LeftToRight;
            introPanel.AutoSize = true;
            introPanel.WrapContents = true;

            //fill book checkboxes
            bookBox = new GroupBox();
            bookBox.AutoSize = true;
            bookBox.Text = "Sourcebooks";
            bookBox.Name = "bookBox";
            List<Book> activeBooks = wm.DBManager.getActiveBooks();

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

            //set up tooltips
            toolTips = new ToolTip();

            toolTips.SetToolTip(lenientButton, "While values will be determined" + Environment.NewLine +
                "by dice rolls, below average rolls" + Environment.NewLine +
                "will be replaced by the average" + Environment.NewLine +
                "result. Additionally, ability scores" + Environment.NewLine +
                "lower than 8 can be rerolled.");
            toolTips.SetToolTip(averageButton, "Attributes that normally" + Environment.NewLine +
                "require dice rolls will be" + Environment.NewLine +
                "determined by average" + Environment.NewLine + 
                "values.");
            toolTips.SetToolTip(puristButton, "Unaltered dice" + Environment.NewLine +
                "results will be" + Environment.NewLine +
                "used.");

            //fill settings box
            settingsBox = new GroupBox();
            settingsBox.AutoSize = true;
            settingsBox.Text = "Character Level && Starting Money";
            settingsBox.Name = "settingsBox";

            levelLabel = new Label();
            levelLabel.Location = new Point(3, 25);
            levelLabel.AutoSize = true;
            levelLabel.Text = "Level (1-20):";
            levelLabel.Name = "levelLabel";
            settingsBox.Controls.Add(levelLabel);

            characterLevel = new NumericUpDown();
            characterLevel.Location = new Point(3 + levelLabel.Width, 23);
            characterLevel.TextAlign = HorizontalAlignment.Center;
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

        public void populateForm()
        {
            foreach (CheckBox checkBox in bookCheckBoxes)
            {
                checkBox.Checked = wm.DBManager.UsedBooks.Contains(checkBox.Text);
            }

            setPreset(wm.Choices.Preset);

            characterLevel.Value = wm.Choices.Level;

            moneyCheckbox.Checked = wm.Choices.AdjustStartingMoney;

            if (!Visited)
            {
                Visited = true;
            }
        }

        public void saveContent()
        {
            wm.DBManager.UsedBooks = getUsedBooks();
            wm.Choices.Preset = getPreset();
            wm.Choices.Level = (int)characterLevel.Value;
            wm.Choices.AdjustStartingMoney = moneyCheckbox.Checked;
        }

        /// <summary>
        /// returns selected books formatted for usage in SQLite queries
        /// </summary>
        private string getUsedBooks()
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

        /// <summary>
        /// returns the chosen creation preset: 
        /// 0 for lenient, 1 for average, 2 for purist
        /// </summary>
        private int getPreset()
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
        /// checks the appropriate radiobuttons: 
        /// 0 for lenient, 1 for average, 2 for purist
        /// </summary>
        private void setPreset(int inputPreset)
        {
            switch(inputPreset)
            {
                case 0: lenientButton.Checked = true;
                    break;
                case 1: averageButton.Checked = true;
                    break;
                default: puristButton.Checked = true;
                    break;
            }
        }

        public bool isValid()
        {
            return (characterLevel.Value > 0);
        }

        public string getInvalidElements()
        {
            string output = "";

            if (characterLevel.Value <= 0)
            {
                output += "character level";
            }

            return output;
        }
    }
}
