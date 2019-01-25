using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_DnD_Character_Creator
{
    public partial class frmMainWindow : Form
    {
        public WizardManager WM { get; }

        public frmMainWindow(WizardManager inputWizardManager)
        {
            WM = inputWizardManager;
            InitializeComponent();
            refreshWindow();
        }

        private void refreshWindow()
        {
            switch(WM.CurrentState)
            {
                case WizardState.race:
                    break;
                case WizardState.appearance:
                    break;
                case WizardState.classBackground:
                    break;
                case WizardState.stats:
                    break;
                case WizardState.languages:
                    break;
                case WizardState.skills:
                    break;
                case WizardState.equipment:
                    break;
                case WizardState.spells:
                    break;
                case WizardState.extraChoices:
                    break;
                case WizardState.story:
                    break;
                case WizardState.export:
                    break;
                default: //WizardState.intro
                    contentFlowPanel.Controls.Add(new IntroControl(WM.DBManager));
                    break;
            }
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
