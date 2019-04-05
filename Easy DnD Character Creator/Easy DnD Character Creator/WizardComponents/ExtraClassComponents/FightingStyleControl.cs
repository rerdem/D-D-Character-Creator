using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Easy_DnD_Character_Creator.DataTypes;

namespace Easy_DnD_Character_Creator.WizardComponents.ExtraClassComponents
{
    public partial class FightingStyleControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        public event EventHandler FightingStyleChosen;

        private List<FightingStyle> availableStyles;

        public FightingStyleControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            availableStyles = new List<FightingStyle>();

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
            return "select a fighting style";
        }

        public bool isValid()
        {
            return (fightingStyleListBox.SelectedItems.Count > 0);
        }

        public void populateForm()
        {
            if (!Visited)
            {
                fightingStyleLabel.Text = "You adopt a particular style of fighting as your specialty. Choose one of the following options:";
            }

            availableStyles = wm.DBManager.ExtraClassChoiceData.FightingStyleData.getFightingStyles(wm.Choices.ClassChoice.Name, wm.Choices.Level);
            fightingStyleListBox.BeginUpdate();
            fightingStyleListBox.DataSource = null;
            fightingStyleListBox.DataSource = availableStyles;
            fightingStyleListBox.DisplayMember = "Name";
            fightingStyleListBox.EndUpdate();

            if (Visited)
            {
                for (int i = 0; i < fightingStyleListBox.Items.Count; i++)
                {
                    if (wm.Choices.ClassChoice.FightingStyles.Contains(fightingStyleListBox.Items[i]))
                    {
                        fightingStyleListBox.SetSelected(i, true);
                    }
                }
            }

            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.ClassChoice.FightingStyles.Clear();
            if (fightingStyleListBox.SelectedItems.Count > 0)
            {
                wm.Choices.ClassChoice.FightingStyles.Add((FightingStyle)fightingStyleListBox.SelectedItem);
            }
        }

        private void fightingStyleListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fightingStyleListBox.SelectedItems.Count > 0)
            {
                FightingStyle selectedStyle = (FightingStyle)fightingStyleListBox.SelectedItem;
                if (selectedStyle != null)
                {
                    descriptionLabel.Text = selectedStyle.Description;
                }

                OnFightingStyleChosen(null);
            }
        }

        protected virtual void OnFightingStyleChosen(EventArgs e)
        {
            EventHandler handler = FightingStyleChosen;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
