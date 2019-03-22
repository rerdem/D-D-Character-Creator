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
    public partial class NameControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private string lastSubrace;

        public event EventHandler NameChanged;

        public NameControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

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
            string output = "";

            if (string.IsNullOrEmpty(characterNameBox.Text)) {
                output += "select a character name";
            }

            if (string.IsNullOrEmpty(playerNameBox.Text))
            {
                if (!string.IsNullOrEmpty(output))
                {
                    output += ", ";
                }
                output += "enter a player name";
            }

            return output;
        }

        public bool isValid()
        {
            return ((!string.IsNullOrEmpty(characterNameBox.Text)) && (!string.IsNullOrEmpty(playerNameBox.Text)));
        }

        public void populateForm()
        {
            resetContent();

            if (Visited)
            {
                playerNameBox.Text = wm.Choices.PlayerName;
                if (wm.Choices.IsMale)
                {
                    maleButton.Checked = true;
                }
                else
                {
                    femaleButton.Checked = true;
                }

                if (!hasSubraceChanged())
                {
                    characterNameBox.Text = wm.Choices.CharacterName;
                }
            }

            lastSubrace = wm.Choices.Subrace;
            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.PlayerName = playerNameBox.Text;
            wm.Choices.CharacterName = characterNameBox.Text;
            wm.Choices.IsMale = maleButton.Checked;
        }

        private void resetContent()
        {
            characterNameBox.Text = "";
            playerNameBox.Text = "";
            if (wm.getRandomNumber(1, 2) > 0)
            {
                maleButton.Checked = true;
            }
            else
            {
                femaleButton.Checked = true;
            }
        }

        private bool hasSubraceChanged()
        {
            return (lastSubrace != wm.Choices.Subrace);
        }

        private void randomNameButton_Click(object sender, EventArgs e)
        {
            characterNameBox.Text = wm.generateCharacterName(maleButton.Checked);
        }

        private void characterNameBox_TextChanged(object sender, EventArgs e)
        {
            OnNameChanged(null);
        }

        private void playerNameBox_TextChanged(object sender, EventArgs e)
        {
            OnNameChanged(null);
        }

        protected virtual void OnNameChanged(EventArgs e)
        {
            EventHandler handler = NameChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
