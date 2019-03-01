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
using Easy_DnD_Character_Creator.DataTypes;

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public partial class EquipmentControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;
        private string LastClass;

        public event EventHandler EquipmentSelectionChanged;

        private List<ListBox> choiceBoxes;
        private List<Label> choiceLabels;
        private List<Label> descriptionLabels;
        
        private List<int> equipmentList2Selection;

        private List<List<EquipmentItem>> equipmentOptions;
        private List<EquipmentItem> equipmentOptions1;
        private List<EquipmentItem> equipmentOptions2;
        private List<EquipmentItem> equipmentOptions3;
        private List<EquipmentItem> equipmentOptions4;

        public EquipmentControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;
            LastClass = "";

            choiceBoxes = new List<ListBox>();
            choiceLabels = new List<Label>();
            descriptionLabels = new List<Label>();

            equipmentList2Selection = new List<int>();

            equipmentOptions = new List<List<EquipmentItem>>();
            equipmentOptions1 = new List<EquipmentItem>();
            equipmentOptions2 = new List<EquipmentItem>();
            equipmentOptions3 = new List<EquipmentItem>();
            equipmentOptions4 = new List<EquipmentItem>();

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

            if (wm.DBManager.EquipmentData.getEquipment2ndSelectionAmount(wm.Choices.Class) != equipmentList2.SelectedItems.Count)
            {
                int missingChoices = wm.DBManager.EquipmentData.getEquipment2ndSelectionAmount(wm.Choices.Class) - equipmentList2.SelectedItems.Count;
                output = $"select {missingChoices} more option(s) in the second equipment box";
            }

            return output;
        }

        public bool isValid()
        {
            return (wm.DBManager.EquipmentData.getEquipment2ndSelectionAmount(wm.Choices.Class) == equipmentList2.SelectedItems.Count);
        }

        public void populateForm()
        {
            if ((!Visited) || (hasClassChanged()))
            {
                resetEquipment();
            }

            if ((Visited) && (!hasClassChanged()))
            {
                //list 1
                if (equipmentList1.Visible)
                {
                    if (equipmentList1.Items.IndexOf(wm.Choices.Equipment1) >= 0)
                    {
                        equipmentList1.SetSelected(equipmentList1.Items.IndexOf(wm.Choices.Equipment1), true);
                    }
                }

                //list 2
                if (equipmentList2.Visible)
                {
                    equipmentList2.SetSelected(0, false);
                    foreach (string item in wm.Choices.Equipment2.Split(','))
                    {
                        if (equipmentList2.Items.IndexOf(item.Trim()) >= 0)
                        {
                            equipmentList2.SetSelected(equipmentList2.Items.IndexOf(item.Trim()), true);
                        }
                    }
                }

                //list 3
                if (equipmentList3.Visible)
                {
                    if (equipmentList3.Items.IndexOf(wm.Choices.Equipment3) >= 0)
                    {
                        equipmentList3.SetSelected(equipmentList3.Items.IndexOf(wm.Choices.Equipment3), true);
                    }
                }

                //list 4
                if (equipmentList4.Visible)
                {
                    if (equipmentList4.Items.IndexOf(wm.Choices.Equipment4) >= 0)
                    {
                        equipmentList4.SetSelected(equipmentList4.Items.IndexOf(wm.Choices.Equipment4), true);
                    }
                }
            }

            //set LastClass
            LastClass = wm.Choices.Class;

            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.Equipment1 = "";
            wm.Choices.Equipment2 = "";
            wm.Choices.Equipment3 = "";
            wm.Choices.Equipment4 = "";
            wm.Choices.Equipment5 = "";

            //list 1
            if (equipmentList1.Visible)
            {
                wm.Choices.Equipment1 = equipmentList1.SelectedItem.ToString();
            }

            //list 2
            if (equipmentList2.Visible)
            {
                foreach (string item in equipmentList2.SelectedItems)
                {
                    if (!string.IsNullOrEmpty(wm.Choices.Equipment2))
                    {
                        wm.Choices.Equipment2 += ", ";
                    }
                    wm.Choices.Equipment2 += item;
                }
            }

            //list 3
            if (equipmentList3.Visible)
            {
                wm.Choices.Equipment3 = equipmentList3.SelectedItem.ToString();
            }
            
            //list 4
            if (equipmentList4.Visible)
            {
                wm.Choices.Equipment4 = equipmentList4.SelectedItem.ToString();
            }

            //extra equipment
            wm.Choices.Equipment5 = wm.DBManager.EquipmentData.getExtraEquipment(wm.Choices.Class);
        }

        private bool hasClassChanged()
        {
            return (wm.Choices.Class != LastClass);
        }

        private string constructEquipmentDescription(EquipmentItem input)
        {
            string output = "";

            if (input is Weapon)
            {
                output = EquipmentFormatter.formatWeaponDescription(input as Weapon);
            }
            else if (input is Armor)
            {
                output = EquipmentFormatter.formatArmorDescription(input as Armor);
            }
            else if (input is Pack)
            {
                Pack item = input as Pack;
                output = item.Content;
            }
            else if (input is MultiItem)
            {
                MultiItem multiItem = input as MultiItem;
                foreach (EquipmentItem item in multiItem.Items)
                {
                    if (!string.IsNullOrEmpty(output))
                    {
                        output += Environment.NewLine;
                        output += Environment.NewLine;
                    }
                    output += constructEquipmentDescription(item);
                }
            }

            return output;
        }

        private void resetEquipment()
        {
            int currentChoice = 1;
            for (int i = 0; i < choiceBoxes.Count; i++)
            {
                if (i >= wm.DBManager.EquipmentData.getEquipmentChoiceAmount(wm.Choices.Class))
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
                    equipmentOptions[i].Clear();
                    equipmentOptions[i].AddRange(wm.DBManager.EquipmentData.getEquipmentChoices(wm.Choices.Subrace,
                                                                                                 wm.Choices.Class,
                                                                                                 wm.Choices.Subclass,
                                                                                                 currentChoice,
                                                                                                 wm.Choices.Strength.getTotalValue()).ToArray());
                    foreach (EquipmentItem item in equipmentOptions[i])
                    {
                        choiceBoxes[i].Items.Add(item.Name);
                    }

                    choiceBoxes[i].EndUpdate();
                    choiceBoxes[i].SetSelected(0, true);
                }

                currentChoice++;
            }

            equipmentLabel2.Text = $"Choose {wm.DBManager.EquipmentData.getEquipment2ndSelectionAmount(wm.Choices.Class)}:";

            if (wm.DBManager.EquipmentData.getEquipment2ndSelectionAmount(wm.Choices.Class) > 1)
            {
                equipmentList2.SelectionMode = SelectionMode.MultiSimple;
            }
            else
            {
                equipmentList2.SelectionMode = SelectionMode.One;
            }

            inventoryLabel.Text = wm.DBManager.EquipmentData.getExtraEquipment(wm.Choices.Class);
        }

        private void initializeCarrierLists()
        {
            choiceBoxes.Add(equipmentList1);
            choiceBoxes.Add(equipmentList2);
            choiceBoxes.Add(equipmentList3);
            choiceBoxes.Add(equipmentList4);

            equipmentOptions.Add(equipmentOptions1);
            equipmentOptions.Add(equipmentOptions2);
            equipmentOptions.Add(equipmentOptions3);
            equipmentOptions.Add(equipmentOptions4);

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
            EquipmentItem currentSelectedItem = equipmentOptions1[equipmentOptions1.FindIndex(item => item.Name.Equals(equipmentList1.SelectedItem.ToString()))];
            descriptionLabel1.Text = constructEquipmentDescription(currentSelectedItem);

            OnEquipmentSelectionChanged(null);
        }

        private void equipmentList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            syncEquipmentList2Selection();
            if (equipmentList2.SelectedIndices.Count > 0)
            {
                if (equipmentList2.SelectedIndices.Count <= wm.DBManager.EquipmentData.getEquipment2ndSelectionAmount(wm.Choices.Class))
                {
                    int lastSelectedIndex = equipmentList2Selection.ElementAt(equipmentList2Selection.Count - 1);
                    EquipmentItem currentSelectedItem = equipmentOptions2[equipmentOptions2.FindIndex(item => item.Name.Equals(equipmentList2.Items[lastSelectedIndex].ToString()))];
                    descriptionLabel2.Text = constructEquipmentDescription(currentSelectedItem);
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
            EquipmentItem currentSelectedItem = equipmentOptions3[equipmentOptions3.FindIndex(item => item.Name.Equals(equipmentList3.SelectedItem.ToString()))];
            descriptionLabel3.Text = constructEquipmentDescription(currentSelectedItem);

            OnEquipmentSelectionChanged(null);
        }

        private void equipmentList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            EquipmentItem currentSelectedItem = equipmentOptions4[equipmentOptions4.FindIndex(item => item.Name.Equals(equipmentList4.SelectedItem.ToString()))];
            descriptionLabel4.Text = constructEquipmentDescription(currentSelectedItem);

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
