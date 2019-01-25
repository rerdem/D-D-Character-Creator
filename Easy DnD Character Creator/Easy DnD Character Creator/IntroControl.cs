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

        private GroupBox bookBox;
        private List<CheckBox> bookCheckBoxes;

        private GroupBox presetBox;
        private GroupBox settingsBox;

        public IntroControl(DataManager inputDataManager)
        {
            dm = inputDataManager;
            bookCheckBoxes = new List<CheckBox>();
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            introPanel = new FlowLayoutPanel();
            introPanel.FlowDirection = FlowDirection.LeftToRight;
            introPanel.WrapContents = false;

            bookBox = new GroupBox();
            bookBox.AutoSize = true;
            bookBox.Text = "Sourcebooks";
            bookBox.Name = "bookbox";
            List<Book> activeBooks = dm.getActiveBooks();

            int xCoord = 3;
            int yCoord = 23;
            int yStep = 20;

            foreach (Book book in activeBooks)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = book.Title;
                checkBox.Name = book.Shorthand + "Box";
                checkBox.Location = new Point(xCoord, yCoord);
                if (book.Mandatory)
                {
                    checkBox.Enabled = false;
                }
                bookCheckBoxes.Add(checkBox);
                bookBox.Controls.Add(checkBox);
                yCoord += yStep;
            }



            presetBox = new GroupBox();


            settingsBox = new GroupBox();

            introPanel.Controls.AddRange(new Control[]
            {
                bookBox,
                presetBox,
                settingsBox
            });

            Controls.Add(introPanel);
        }
    }
}
