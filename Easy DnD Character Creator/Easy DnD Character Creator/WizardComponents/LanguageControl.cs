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

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public partial class LanguageControl : UserControl, IWizardControl
    {
        private WizardManager wm;
        private bool visited;

        private string lastCharacterInfo;

        private List<Language> languages;
        private ToolTip toolTips;

        private List<CheckBox> standardExoticBoxes;
        private List<CheckBox> uncheckedDisabledBoxes;
        private List<CheckBox> classBoxes;

        public event EventHandler LanguageSelectionChanged;

        public LanguageControl(WizardManager inputWizardManager)
        {
            wm = inputWizardManager;
            Visited = false;

            languages = new List<Language>();

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
            string output = "";

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
            int maximumLanguages = wm.DBManager.LanguageData.getDefaultLanguageCount(wm.Choices.RaceChoice.getSelectedSubrace().Name, wm.Choices.ClassChoice.Name, wm.Choices.ClassChoice.getSelectedSubclass().Name);
            maximumLanguages += wm.DBManager.LanguageData.getExtraLanguageCount(wm.Choices.RaceChoice.getSelectedSubrace().Name, wm.Choices.ClassChoice.getSelectedSubclass().Name, wm.Choices.BackgroundChoice.Name);

            if (currentlySelectedLanguages != maximumLanguages)
            {
                int missingLanguages = maximumLanguages - currentlySelectedLanguages;
                output = $"select {missingLanguages} more language(s)";
            }

            return output;
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
            int maximumLanguages = wm.DBManager.LanguageData.getDefaultLanguageCount(wm.Choices.RaceChoice.getSelectedSubrace().Name, wm.Choices.ClassChoice.Name, wm.Choices.ClassChoice.getSelectedSubclass().Name);
            maximumLanguages += wm.DBManager.LanguageData.getExtraLanguageCount(wm.Choices.RaceChoice.getSelectedSubrace().Name, wm.Choices.ClassChoice.getSelectedSubclass().Name, wm.Choices.BackgroundChoice.Name);

            return (currentlySelectedLanguages == maximumLanguages);
        }

        public void populateForm()
        {
            if (!Visited && hasCharacterInfoChanged())
            {
                resetAllBoxes();
                setDefaultLanguages();
            }

            if (Visited && !hasCharacterInfoChanged())
            {
                foreach (CheckBox box in standardExoticBoxes)
                {
                    if (wm.Choices.Languages.Contains(box.Text))
                    {
                        box.Checked = true;
                    }
                }
            }

            setCharacterInfo();

            Visited = true;
        }

        public void saveContent()
        {
            wm.Choices.Languages.Clear();

            foreach (CheckBox box in standardExoticBoxes)
            {
                if (box.Checked)
                {
                    wm.Choices.Languages.Add(box.Text);
                }
            }
        }

        private void setCharacterInfo()
        {
            lastCharacterInfo = wm.Choices.RaceChoice.getSelectedSubrace().Name + wm.Choices.ClassChoice.Name + wm.Choices.ClassChoice.getSelectedSubclass().Name + wm.Choices.BackgroundChoice.Name;
        }

        private bool hasCharacterInfoChanged()
        {
            string currentCharacterInfo = wm.Choices.RaceChoice.getSelectedSubrace().Name + wm.Choices.ClassChoice.Name + wm.Choices.ClassChoice.getSelectedSubclass().Name + wm.Choices.BackgroundChoice.Name;
            return (currentCharacterInfo != lastCharacterInfo);
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
            int maximumLanguages = wm.DBManager.LanguageData.getDefaultLanguageCount(wm.Choices.RaceChoice.getSelectedSubrace().Name, wm.Choices.ClassChoice.Name, wm.Choices.ClassChoice.getSelectedSubclass().Name);
            maximumLanguages += wm.DBManager.LanguageData.getExtraLanguageCount(wm.Choices.RaceChoice.getSelectedSubrace().Name, wm.Choices.ClassChoice.getSelectedSubclass().Name, wm.Choices.BackgroundChoice.Name);
            
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
            List<string> defaultLanguages = wm.DBManager.LanguageData.getDefaultRaceLanguages(wm.Choices.RaceChoice.getSelectedSubrace().Name);
            defaultLanguages.AddRange(wm.DBManager.LanguageData.getDefaultClassLanguages(wm.Choices.ClassChoice.Name));
            defaultLanguages.AddRange(wm.DBManager.LanguageData.getDefaultSubclassLanguages(wm.Choices.ClassChoice.getSelectedSubclass().Name));

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

            languages = wm.DBManager.LanguageData.getLanguages();
            
            //standard languages
            List<string> standardLanguages = languages.Where(language => language.Type=="Standard").Select(language => language.Name).ToList();

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
            List<string> exoticLanguages = languages.Where(language => language.Type == "Exotic").Select(language => language.Name).ToList();

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
            List<string> classLanguages = languages.Where(language => language.Type == "Class").Select(language => language.Name).ToList();

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
                Language currentLanguage = languages.First(language => language.Name == box.Text);

                string tipText = $"Typical Speakers: {currentLanguage.Speakers}{Environment.NewLine}Script: {currentLanguage.Script}";
                toolTips.SetToolTip(box, tipText);
            }

            foreach (CheckBox box in classBoxes)
            {
                Language currentLanguage = languages.First(language => language.Name == box.Text);
                string tipText = $"Typical Speakers: {currentLanguage.Speakers}{Environment.NewLine}Script: {currentLanguage.Script}";
                toolTips.SetToolTip(box, tipText);
            }
        }

        private void languageCheck_CheckedChanged(object sender, EventArgs e)
        {
            validateCheckboxes();

            OnLanguageSelectionChanged(null);
        }

        protected virtual void OnLanguageSelectionChanged(EventArgs e)
        {
            EventHandler handler = LanguageSelectionChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
