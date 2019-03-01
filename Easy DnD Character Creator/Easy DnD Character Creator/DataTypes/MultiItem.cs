using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataTypes
{
    public class MultiItem : EquipmentItem
    {
        //public string Name { get; }
        public List<EquipmentItem> Items { get; }

        public MultiItem()
        {
            Items = new List<EquipmentItem>();
        }

        public void addItem(EquipmentItem newItem)
        {
            Items.Add(newItem);
            refreshName();
        }

        public void removeItem(EquipmentItem itemToDelete)
        {
            Items.Remove(itemToDelete);
            refreshName();
        }

        private void refreshName()
        {
            string newName = "";

            foreach (EquipmentItem item in Items)
            {
                if (!string.IsNullOrEmpty(newName))
                {
                    newName += ", ";
                }
                newName += item.Name;
            }

            Name = newName;
        }
    }
}
