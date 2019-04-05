using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public partial class ExportControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private CharacterSheet charSheet;

        public ExportControl(WizardManager inputWizardmanager)
        {
            wm = inputWizardmanager;
            Visited = false;

            charSheet = new CharacterSheet(wm.DBManager, wm.Choices);

            InitializeComponent();
        }

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

        public string getInvalidElements()
        {
            return "";
        }

        public bool isValid()
        {
            return true;
        }

        public void populateForm()
        {
            Visited = true;
        }

        public void saveContent()
        {
            
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Save your character sheet...";
            saveDialog.InitialDirectory = Directory.GetCurrentDirectory();
            saveDialog.DefaultExt = "html";
            saveDialog.Filter = "html files (*.html, *.htm)|*.html;*.htm|All files (*.*)|*.*";
            saveDialog.FilterIndex = 1;
            saveDialog.CheckPathExists = true;
            saveDialog.FileName = $"{wm.Choices.PlayerName} {wm.Choices.ClassChoice.Name}.html";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                charSheet.fillCharacterSheet();
                charSheet.saveToHTML(saveDialog.FileName);
            }
        }
    }
}
