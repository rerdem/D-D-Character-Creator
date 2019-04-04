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
        private int Equipment2ndSelectionAmount;
        private int EquipmentChoiceAmount;

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
            Equipment2ndSelectionAmount = 0;
            EquipmentChoiceAmount = 0;

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

            if (Equipment2ndSelectionAmount != equipmentList2.SelectedItems.Count)
            {
                int missingChoices = Equipment2ndSelectionAmount - equipmentList2.SelectedItems.Count;
                output = $"select {missingChoices} more option(s) in the second equipment box";
            }

            return output;
        }

        public bool isValid()
        {
            return (Equipment2ndSelectionAmount == equipmentList2.SelectedItems.Count);
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
                    foreach (EquipmentItem item in wm.Choices.Equipment1)
                    {
                        if (equipmentList1.Items.IndexOf(item) >= 0)
                        {
                            equipmentList1.SetSelected(equipmentList1.Items.IndexOf(item), true);
                        }
                    }
                }

                //list 2
                if (equipmentList2.Visible)
                {
                    equipmentList2.SetSelected(0, false);
                    foreach (EquipmentItem item in wm.Choices.Equipment2)
                    {
                        if (equipmentList2.Items.IndexOf(item) >= 0)
                        {
                            equipmentList2.SetSelected(equipmentList2.Items.IndexOf(item), true);
                        }
                    }
                }

                //list 3
                if (equipmentList3.Visible)
                {
                    foreach (EquipmentItem item in wm.Choices.Equipment3)
                    {
                        if (equipmentList3.Items.IndexOf(item) >= 0)
                        {
                            equipmentList3.SetSelected(equipmentList3.Items.IndexOf(item), true);
                        }
                    }
                }

                //list 4
                if (equipmentList4.Visible)
                {
                    foreach (EquipmentItem item in wm.Choices.Equipment4)
                    {
                        if (equipmentList4.Items.IndexOf(item) >= 0)
                        {
                            equipmentList4.SetSelected(equipmentList4.Items.IndexOf(item), true);
                        }
                    }
                }
            }

            //set LastClass
            LastClass = wm.Choices.Class;

            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.Equipment1.Clear();
            wm.Choices.Equipment2.Clear();
            wm.Choices.Equipment3.Clear();
            wm.Choices.Equipment4.Clear();
            wm.Choices.ExtraEquipment = "";

            //list 1
            if (equipmentList1.Visible)
            {
                wm.Choices.Equipment1.Add((EquipmentItem)equipmentList1.SelectedItem);
            }

            //list 2
            if (equipmentList2.Visible)
            {
                foreach (EquipmentItem item in equipmentList2.SelectedItems)
                {
                    wm.Choices.Equipment2.Add(item);
                }
            }

            //list 3
            if (equipmentList3.Visible)
            {
                wm.Choices.Equipment3.Add((EquipmentItem)equipmentList3.SelectedItem);
            }
            
            //list 4
            if (equipmentList4.Visible)
            {
                wm.Choices.Equipment4.Add((EquipmentItem)equipmentList4.SelectedItem);
            }

            //extra equipment
            wm.Choices.ExtraEquipment = wm.DBManager.EquipmentData.getExtraEquipment(wm.Choices.Class);
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
            EquipmentChoiceAmount = wm.DBManager.EquipmentData.getEquipmentChoiceAmount(wm.Choices.Class);
            Equipment2ndSelectionAmount = wm.DBManager.EquipmentData.getEquipment2ndSelectionAmount(wm.Choices.Class);
            
            int currentChoice = 1;
            for (int i = 0; i < choiceBoxes.Count; i++)
            {
                if (i >= EquipmentChoiceAmount)
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
                    //choiceBoxes[i].Items.Clear();
                    equipmentOptions[i].Clear();
                    equipmentOptions[i].AddRange(wm.DBManager.EquipmentData.getEquipmentChoices(wm.Choices.RaceChoice.getSelectedSubrace().Name,
                                                                                                 wm.Choices.Class,
                                                                                                 wm.Choices.Subclass,
                                                                                                 currentChoice,
                                                                                                 wm.Choices.Strength.getTotalValue()).ToArray());
                    choiceBoxes[i].DataSource = null;
                    choiceBoxes[i].DataSource = equipmentOptions[i];
                    choiceBoxes[i].DisplayMember = "Name";
                    //foreach (EquipmentItem item in equipmentOptions[i])
                    //{
                    //    choiceBoxes[i].Items.Add(item.Name);
                    //}

                    choiceBoxes[i].EndUpdate();
                    choiceBoxes[i].SetSelected(0, true);
                }

                currentChoice++;
            }

            equipmentLabel2.Text = $"Choose {Equipment2ndSelectionAmount}:";

            if (Equipment2ndSelectionAmount > 1)
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
            descriptionLabel1.Text = constructEquipmentDescription((EquipmentItem)equipmentList1.SelectedItem);

            OnEquipmentSelectionChanged(null);
        }

        private void equipmentList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            syncEquipmentList2Selection();
            if (equipmentList2.SelectedIndices.Count > 0)
            {
                if (equipmentList2.SelectedIndices.Count <= Equipment2ndSelectionAmount)
                {
                    int lastSelectedIndex = equipmentList2Selection.ElementAt(equipmentList2Selection.Count - 1);
                    descriptionLabel2.Text = constructEquipmentDescription((EquipmentItem)equipmentList2.Items[lastSelectedIndex]);
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
            descriptionLabel3.Text = constructEquipmentDescription((EquipmentItem)equipmentList3.SelectedItem);

            OnEquipmentSelectionChanged(null);
        }

        private void equipmentList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            descriptionLabel4.Text = constructEquipmentDescription((EquipmentItem)equipmentList4.SelectedItem);

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
