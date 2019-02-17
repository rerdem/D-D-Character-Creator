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

        public event EventHandler EquipmentSelectionChanged;

        private List<ListBox> choiceBoxes;
        private List<Label> choiceLabels;
        private List<Label> descriptionLabels;

        private List<int> equipmentList2Selection;

        public EquipmentControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            visited = false;

            choiceBoxes = new List<ListBox>();
            choiceLabels = new List<Label>();
            descriptionLabels = new List<Label>();

            equipmentList2Selection = new List<int>();

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
            string output = "";

            if (wm.DBManager.getEquipment2ndSelectionAmount(wm.Choices.Class) != equipmentList2.SelectedItems.Count)
            {
                int missingChoices = wm.DBManager.getEquipment2ndSelectionAmount(wm.Choices.Class) - equipmentList2.SelectedItems.Count;
                output = $"select {missingChoices} more option(s) in the second equipment box";
            }

            return output;
        }

        public bool isValid()
        {
            return (wm.DBManager.getEquipment2ndSelectionAmount(wm.Choices.Class) == equipmentList2.SelectedItems.Count);
        }

        public void populateForm()
        {
            resetEquipment();

            //TO DO: repopulate from previous choices, if class hasn't changed
        }

        public void saveContent()
        {
            wm.Choices.Equipment = "";

            int lastActiveBox = wm.DBManager.getEquipmentChoiceAmount(wm.Choices.Class) - 1;

            for (int i = 0; i < lastActiveBox; i++)
            {
                foreach (string item in choiceBoxes[i].SelectedItems)
                {
                    if (!string.IsNullOrEmpty(wm.Choices.Equipment))
                    {
                        wm.Choices.Equipment += ", ";
                    }
                    wm.Choices.Equipment += item;
                }
            }

            wm.Choices.Equipment += ", ";
            wm.Choices.Equipment += wm.DBManager.getExtraEquipment(wm.Choices.Class);
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
                    output += wm.DBManager.getEquipmentStats(item.Trim());
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

            if (wm.DBManager.getEquipment2ndSelectionAmount(wm.Choices.Class) > 1)
            {
                equipmentList2.SelectionMode = SelectionMode.MultiSimple;
            }
            else
            {
                equipmentList2.SelectionMode = SelectionMode.One;
            }

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

        private void syncEquipmentList2Selection()
        {
            //add new selected items
            foreach (int index in equipmentList2.SelectedIndices)
            {
                if (!equipmentList2Selection.Contains(index))
                {
                    equipmentList2Selection.Add(index);
                }
            }

            //remove deselected items
            equipmentList2Selection.RemoveAll(index => !equipmentList2.SelectedIndices.Contains(index));
        }

        private void equipmentList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            descriptionLabel1.Text = getDescription(equipmentList1.SelectedItem.ToString());

            OnEquipmentSelectionChanged(null);
        }

        private void equipmentList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            syncEquipmentList2Selection();
            if (equipmentList2.SelectedIndices.Count > 0)
            {
                if (equipmentList2.SelectedIndices.Count <= wm.DBManager.getEquipment2ndSelectionAmount(wm.Choices.Class))
                {
                    int lastSelectedIndex = equipmentList2Selection.ElementAt(equipmentList2Selection.Count - 1);
                    descriptionLabel2.Text = getDescription(equipmentList2.Items[lastSelectedIndex].ToString());
                }
                else
                {
                    int lastSelectedIndex = equipmentList2Selection.ElementAt(equipmentList2Selection.Count - 1);
                    equipmentList2.SelectedIndices.Remove(lastSelectedIndex);
                }
            }

            OnEquipmentSelectionChanged(null);
        }

        private void equipmentList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            descriptionLabel3.Text = getDescription(equipmentList3.SelectedItem.ToString());

            OnEquipmentSelectionChanged(null);
        }

        private void equipmentList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            descriptionLabel4.Text = getDescription(equipmentList4.SelectedItem.ToString());

            OnEquipmentSelectionChanged(null);
        }

        protected virtual void OnEquipmentSelectionChanged(EventArgs e)
        {
            EventHandler handler = EquipmentSelectionChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
