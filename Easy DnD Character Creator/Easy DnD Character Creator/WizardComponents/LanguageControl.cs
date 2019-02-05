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
    public partial class LanguageControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;
        
        private ToolTip toolTips;

        private List<CheckBox> standardExoticBoxes;
        private List<CheckBox> uncheckedDisabledBoxes;
        private List<CheckBox> classBoxes;

        public LanguageControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            InitializeComponent();
            initializeCheckboxes();
            initializeToolTips();
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
            //number of currently selected languages
            int currentlySelectedLanguages = 0;
            foreach (CheckBox box in standardExoticBoxes)
            {
                if (box.Checked)
                {
                    currentlySelectedLanguages++;
                }
            }
            foreach (CheckBox box in classBoxes)
            {
                if (box.Checked)
                {
                    currentlySelectedLanguages++;
                }
            }

            //number of maximum possible selected languages
            int maximumLanguages = wm.DBManager.getDefaultLanguageCount(wm.Choices.Subrace, wm.Choices.Class, wm.Choices.Subclass);
            maximumLanguages += wm.DBManager.getExtraLanguageCount(wm.Choices.Subrace, wm.Choices.Subclass, wm.Choices.Background);

            return (currentlySelectedLanguages == maximumLanguages);
        }

        public void populateForm()
        {
            resetAllBoxes();
            setDefaultLanguages();
        }

        public void saveContent()
        {
            
        }

        private void validateCheckboxes()
        {
            //number of currently selected languages
            int currentlySelectedLanguages = 0;
            foreach (CheckBox box in standardExoticBoxes)
            {
                if (box.Checked)
                {
                    currentlySelectedLanguages++;
                }
            }
            foreach (CheckBox box in classBoxes)
            {
                if (box.Checked)
                {
                    currentlySelectedLanguages++;
                }
            }
            
            //number of maximum possible selected languages
            int maximumLanguages = wm.DBManager.getDefaultLanguageCount(wm.Choices.Subrace, wm.Choices.Class, wm.Choices.Subclass);
            maximumLanguages += wm.DBManager.getExtraLanguageCount(wm.Choices.Subrace, wm.Choices.Subclass, wm.Choices.Background);
            
            //change box status
            if (currentlySelectedLanguages >= maximumLanguages)
            {
                disableUncheckedBoxes();
            }
            else
            {
                enableUncheckedBoxes();
            }
        }

        private void enableUncheckedBoxes()
        {
            foreach (CheckBox box in standardExoticBoxes)
            {
                if (!box.Checked)
                {
                    box.Enabled = true;
                }
            }
        }

        private void disableUncheckedBoxes()
        {
            foreach (CheckBox box in standardExoticBoxes)
            {
                if (!box.Checked)
                {
                    box.Enabled = false;
                }
            }
        }

        private void setDefaultLanguages()
        {
            List<string> defaultLanguages = wm.DBManager.getDefaultRaceLanguages(wm.Choices.Subrace);
            defaultLanguages.AddRange(wm.DBManager.getDefaultClassLanguages(wm.Choices.Class));
            defaultLanguages.AddRange(wm.DBManager.getDefaultSubclassLanguages(wm.Choices.Subclass));

            foreach (CheckBox box in standardExoticBoxes)
            {
                if (defaultLanguages.Contains(box.Text))
                {
                    box.Checked = true;
                    box.Enabled = false;
                }
            }
            foreach (CheckBox box in classBoxes)
            {
                if (defaultLanguages.Contains(box.Text))
                {
                    box.Checked = true;
                    box.Enabled = false;
                }
            }
        }

        private void resetAllBoxes()
        {
            foreach (CheckBox box in standardExoticBoxes)
            {
                box.Checked = false;
                box.Enabled = true;
            }
            foreach (CheckBox box in classBoxes)
            {
                box.Checked = false;
                box.Enabled = false;
            }
        }

        private void initializeCheckboxes()
        {
            standardExoticBoxes = new List<CheckBox>();
            uncheckedDisabledBoxes = new List<CheckBox>();
            classBoxes = new List<CheckBox>();

            //standard languages
            List<string> standardLanguages = wm.DBManager.getLanguages("Standard");

            foreach (string language in standardLanguages)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.Name = language + "Box";
                checkbox.Text = language;
                checkbox.CheckedChanged += languageCheck_CheckedChanged;
                standardLanguageLayout.Controls.Add(checkbox);
                standardExoticBoxes.Add(checkbox);
            }

            //exotic languages
            List<string> exoticLanguages = wm.DBManager.getLanguages("Exotic");

            foreach (string language in exoticLanguages)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.Name = language + "Box";
                checkbox.Text = language;
                checkbox.CheckedChanged += languageCheck_CheckedChanged;
                exoticLanguageLayout.Controls.Add(checkbox);
                standardExoticBoxes.Add(checkbox);
            }

            //class langauges
            List<string> classLanguages = wm.DBManager.getLanguages("Class");

            foreach (string language in classLanguages)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.Name = language + "Box";
                checkbox.Text = language;
                checkbox.Enabled = false;
                checkbox.CheckedChanged += languageCheck_CheckedChanged;
                classLanguageLayout.Controls.Add(checkbox);
                classBoxes.Add(checkbox);
            }
        }

        private void initializeToolTips()
        {
            toolTips = new ToolTip();

            foreach (CheckBox box in standardExoticBoxes)
            {
                string tipText = "Typical Speakers: " + wm.DBManager.getLanguageSpeakers(box.Text) + Environment.NewLine;
                tipText += "Script: " + wm.DBManager.getLanguageScript(box.Text);
                toolTips.SetToolTip(box, tipText);
            }

            foreach (CheckBox box in classBoxes)
            {
                string tipText = "Typical Speakers: " + wm.DBManager.getLanguageSpeakers(box.Text) + Environment.NewLine;
                tipText += "Script: " + wm.DBManager.getLanguageScript(box.Text);
                toolTips.SetToolTip(box, tipText);
            }
        }

        private void languageCheck_CheckedChanged(object sender, EventArgs e)
        {
            validateCheckboxes();
        }
    }
}
