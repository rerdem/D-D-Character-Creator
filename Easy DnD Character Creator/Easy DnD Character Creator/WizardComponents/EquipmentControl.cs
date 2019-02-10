using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public partial class EquipmentControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private List<ListBox> choiceBoxes;
        private List<Label> choiceLabels;
        private List<Label> descriptionLabels;

        public EquipmentControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            visited = false;
            choiceBoxes = new List<ListBox>();
            choiceLabels = new List<Label>();
            descriptionLabels = new List<Label>();
            InitializeComponent();
            initializeCarrierLists();
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
            resetEquipment();
        }

        public void saveContent()
        {
            
        }

        private string getDescription(string input)
        {
            string output = "";

            //remove any parentheses with digits inbetween
            input = Regex.Replace(input, @"\s\([\d]+\)", "");

            //construct description for one or more items
            if (input.Contains(","))
            {
                string[] items = input.Split(',');

                foreach (string item in items)
                {
                    output += wm.DBManager.getEquipmentStats(item);
                    output += Environment.NewLine;
                    output += Environment.NewLine;
                }
            }
            else
            {
                output = wm.DBManager.getEquipmentStats(input);
            }

            return output;
        }

        private void resetEquipment()
        {
            int currentChoice = 1;
            for (int i = 0; i < choiceBoxes.Count; i++)
            {
                if (i >= wm.DBManager.getEquipmentChoiceAmount(wm.Choices.Class))
                {
                    choiceBoxes[i].Visible = false;
                    choiceLabels[i].Visible = false;
                    descriptionLabels[i].Visible = false;
                }
                else
                {
                    choiceBoxes[i].Visible = true;
                    choiceLabels[i].Visible = true;
                    descriptionLabels[i].Visible = true;
                    choiceBoxes[i].BeginUpdate();
                    choiceBoxes[i].Items.Clear();
                    choiceBoxes[i].Items.AddRange(wm.DBManager.getEquipmentChoices(wm.Choices.Subrace,
                                                                                   wm.Choices.Class,
                                                                                   wm.Choices.Subclass,
                                                                                   currentChoice,
                                                                                   wm.Choices.Strength.getTotalValue()).ToArray());
                    choiceBoxes[i].EndUpdate();
                    choiceBoxes[i].SetSelected(0, true);
                }

                currentChoice++;
            }

            equipmentLabel2.Text = $"Choose {wm.DBManager.getEquipment2ndSelectionAmount(wm.Choices.Class)}:";
            inventoryLabel.Text = wm.DBManager.getExtraEquipment(wm.Choices.Class);
        }

        private void initializeCarrierLists()
        {
            choiceBoxes.Add(equipmentList1);
            choiceBoxes.Add(equipmentList2);
            choiceBoxes.Add(equipmentList3);
            choiceBoxes.Add(equipmentList4);

            choiceLabels.Add(equipmentLabel1);
            choiceLabels.Add(equipmentLabel2);
            choiceLabels.Add(equipmentLabel3);
            choiceLabels.Add(equipmentLabel4);

            descriptionLabels.Add(descriptionLabel1);
            descriptionLabels.Add(descriptionLabel2);
            descriptionLabels.Add(descriptionLabel3);
            descriptionLabels.Add(descriptionLabel4);
        }

        private void equipmentList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            descriptionLabel1.Text = getDescription(equipmentList1.SelectedItem.ToString());
        }

        private void equipmentList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void equipmentList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            descriptionLabel3.Text = getDescription(equipmentList3.SelectedItem.ToString());
        }

        private void equipmentList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            descriptionLabel4.Text = getDescription(equipmentList4.SelectedItem.ToString());
        }
    }
}
