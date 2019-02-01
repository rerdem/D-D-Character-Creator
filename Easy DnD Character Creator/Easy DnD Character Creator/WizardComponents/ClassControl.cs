﻿using System;
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
    public partial class ClassControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        public event EventHandler ClassChanged;
        public event EventHandler ClassChoiceChanged;

        private int ChoiceAmount { get; set; }

        public ClassControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            visited = false;
            ChoiceAmount = 0;
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

            if (wm.DBManager.classHasExtraChoice(classListBox.SelectedItem.ToString()))
            {
                if (extraChoiceBox.SelectedItems.Count != ChoiceAmount)
                {
                    if (!string.IsNullOrEmpty(output))
                    {
                        output += ", ";
                    }

                    output += "class tool proficiency amount";
                }
            }

            return output;
        }

        public bool isValid()
        {
            if (wm.DBManager.classHasExtraChoice(classListBox.SelectedItem.ToString()))
            {
                return (classListBox.SelectedItems.Count > 0) 
                    && (subclassListBox.SelectedItems.Count > 0)
                    && (extraChoiceBox.SelectedItems.Count == ChoiceAmount);
            }
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

            if (wm.DBManager.classHasExtraChoice(classListBox.SelectedItem.ToString()))
            {
                string proficiencyString = "";
                foreach (object obj in extraChoiceBox.SelectedItems)
                {
                    if (!string.IsNullOrEmpty(proficiencyString))
                    {
                        proficiencyString += ", ";
                    }
                    proficiencyString += obj.ToString();
                }
                wm.Choices.ClassProficiency = proficiencyString;
            }
            else
            {
                wm.Choices.ClassProficiency = "";
            }
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

        private void fillExtraChoiceBox(string classChoice)
        {
            List<string> choiceList = wm.DBManager.getExtraClassProficiencies(classChoice);

            extraChoiceBox.BeginUpdate();
            extraChoiceBox.Items.Clear();
            foreach (string entry in choiceList)
            {
                extraChoiceBox.Items.Add(entry);
            }
            extraChoiceBox.EndUpdate();

            if (!string.IsNullOrEmpty(wm.Choices.ClassProficiency))
            {
                string[] savedProficiencies = wm.Choices.ClassProficiency.Split(',');
                foreach (string entry in savedProficiencies)
                {
                    if (extraChoiceBox.Items.Contains(entry.Trim()))
                    {
                        extraChoiceBox.SetSelected(extraChoiceBox.Items.IndexOf(entry.Trim()), true);
                    }
                }
            }
        }

        private void classListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillSubclassListBox(classListBox.SelectedItem.ToString());

            if (wm.DBManager.classHasExtraChoice(classListBox.SelectedItem.ToString()))
            {
                descriptionLabel.MaximumSize = new Size(520, descriptionLabel.MaximumSize.Height);
                extraChoiceLayout.Visible = true;

                ChoiceAmount = wm.DBManager.getExtraClassProficiencyAmount(classListBox.SelectedItem.ToString());
                extraChoiceLabel.Text = extraChoiceLabel.Text.ToString().Replace(Regex.Match(extraChoiceLabel.Text, @"\d+").Value, ChoiceAmount.ToString());
                if (ChoiceAmount > 1)
                {
                    extraChoiceBox.SelectionMode = SelectionMode.MultiSimple;
                }
                else
                {
                    extraChoiceBox.SelectionMode = SelectionMode.One;
                }

                fillExtraChoiceBox(classListBox.SelectedItem.ToString());
            }
            else
            {
                descriptionLabel.MaximumSize = new Size(650, descriptionLabel.MaximumSize.Height);
                extraChoiceLayout.Visible = false;
                ChoiceAmount = 0;
            }

            OnClassChanged(null);
        }

        private void subclassListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            descriptionLabel.Text = wm.DBManager.getClassDescription(classListBox.SelectedItem.ToString());
            descriptionLabel.Text += Environment.NewLine;
            descriptionLabel.Text += wm.DBManager.getSubclassDescription(subclassListBox.SelectedItem.ToString());

            saveContent();
        }

        private void extraChoiceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (extraChoiceBox.SelectedItems.Count > ChoiceAmount)
            {
                extraChoiceBox.SelectedItems.Remove(extraChoiceBox.SelectedItems[ChoiceAmount]);
            }
            saveContent();

            OnClassChoiceChanged(null);
        }

        protected virtual void OnClassChanged(EventArgs e)
        {
            EventHandler handler = ClassChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnClassChoiceChanged(EventArgs e)
        {
            EventHandler handler = ClassChoiceChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}