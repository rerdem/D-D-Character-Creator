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
    public partial class ClassControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        public ClassControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            visited = false;
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

            if ((classListBox.SelectedItems.Count == 0) || (subclassListBox.SelectedItems.Count == 0))
            {
                output += classBox.Text;
            }

            return output;
        }

        public bool isValid()
        {
            return (classListBox.SelectedItems.Count > 0) && (subclassListBox.SelectedItems.Count > 0);
        }

        public void populateForm()
        {
            fillClassListBox();
            if (!Visited)
            {
                Visited = true;
            }
        }

        public void saveContent()
        {
            wm.Choices.Class = classListBox.SelectedItem.ToString();
            wm.Choices.Subclass = subclassListBox.SelectedItem.ToString();
        }

        private void fillClassListBox()
        {
            List<string> classList = wm.DBManager.getClasses();

            classListBox.BeginUpdate();
            classListBox.Items.Clear();
            foreach (string race in classList)
            {
                classListBox.Items.Add(race);
            }
            classListBox.EndUpdate();

            if (classListBox.Items.Contains(wm.Choices.Class))
            {
                classListBox.SetSelected(classListBox.Items.IndexOf(wm.Choices.Class), true);
            }
            else
            {
                classListBox.SetSelected(0, true);
            }
        }

        private void fillSubclassListBox(string inputClass)
        {
            List<string> subclassList = wm.DBManager.getSubclasses(inputClass, wm.Choices.Level);

            subclassListBox.BeginUpdate();
            subclassListBox.Items.Clear();
            foreach (string subrace in subclassList)
            {
                subclassListBox.Items.Add(subrace);
            }
            subclassListBox.EndUpdate();

            if (subclassListBox.Items.Contains(wm.Choices.Subclass))
            {
                subclassListBox.SetSelected(subclassListBox.Items.IndexOf(wm.Choices.Subclass), true);
            }
            else
            {
                subclassListBox.SetSelected(0, true);
            }
        }

        private void classListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillSubclassListBox(classListBox.SelectedItem.ToString());
        }

        private void subclassListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            descriptionLabel.Text = wm.DBManager.getClassDescription(classListBox.SelectedItem.ToString());
            descriptionLabel.Text += Environment.NewLine;
            descriptionLabel.Text += wm.DBManager.getSubclassDescription(subclassListBox.SelectedItem.ToString());

            saveContent();

            //OnSubraceChanged(null);
        }

        //protected virtual void OnSubraceChanged(EventArgs e)
        //{
        //    EventHandler handler = SubraceChanged;
        //    if (handler != null)
        //    {
        //        handler(this, e);
        //    }
        //}
    }
}
