using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator
{
    public class CharacterSheetStringBuilder
    {
        private DataManager DBManager { get; }
        private ChoiceManager Choices { get; }

        //HTML template snippets
        private string HTMLRadioButton;
        private string HTMLRadioButtonChecked;
        private string HTMLTableRow;
        private string HTMLSpellbook;
        private string HTMLSpellbookEntry;
        private string HTMLWildShapeList;
        private string HTMLWildShapeEntry;

        public int ProficiencyBonus { get; set; }
        public string WeaponProficiencies { get; set; }
        public string SpellcastingAbility { get; set; }

        public CharacterSheetStringBuilder(DataManager inputDataManager, ChoiceManager inputChoiceManager)
        {
            DBManager = inputDataManager;
            Choices = inputChoiceManager;

            initializeHTMLSnippets();

            ProficiencyBonus = 0;
            WeaponProficiencies = "";
            SpellcastingAbility = "";
        }

        private void initializeHTMLSnippets()
        {
            HTMLRadioButton = "<input type=\"radio\" name=\"@name@\"@checked@>@text@<br>" + Environment.NewLine;
            HTMLRadioButtonChecked = " checked=\"checked\"";
            HTMLTableRow = "\t<tr>" + Environment.NewLine +
                           "\t\t<td>@name@</td>" + Environment.NewLine +
                           "\t\t<td>@atkmodifier@</td>" + Environment.NewLine +
                           "\t\t<td>@damage@</td>" + Environment.NewLine +
                           "\t</tr>" + Environment.NewLine;
            HTMLSpellbook = "<div class=\"header\">" + Environment.NewLine +
                            "\t<div class=\"charname\">" + Environment.NewLine +
                            "\t\t@charactername@ - Spellbook" + Environment.NewLine +
                            "\t</div>" + Environment.NewLine +
                            "</div>" + Environment.NewLine + Environment.NewLine +
                            "<div class=\"main\">" + Environment.NewLine +
                            "\t@spellbook@" + Environment.NewLine +
                            "</div>" + Environment.NewLine;
            HTMLSpellbookEntry = "<div class=\"spellbookentry\">" + Environment.NewLine +
                                 "\t<h1>@name@</h1>" + Environment.NewLine +
                                 "\tLevel @level@ @school@ @ritual@<br>" + Environment.NewLine +
                                 "\t<b>casting time:</b> @casttime@ | <b>range:</b> @range@<br>" + Environment.NewLine +
                                 "\t<b>components:</b> @components@ | <b>duration:</b> @duration@<br><br>" + Environment.NewLine +
                                 "\t@description@" + Environment.NewLine +
                                 "</div>" + Environment.NewLine;
            HTMLWildShapeList = "<div class=\"header\">" + Environment.NewLine +
                                "\t<div class=\"charname\">" + Environment.NewLine +
                                "\t\t@charactername@ - Wild Shape Beasts (@terrain@)" + Environment.NewLine +
                                "\t</div>" + Environment.NewLine +
                                "</div>" + Environment.NewLine + Environment.NewLine +
                                "<div class=\"main\">" + Environment.NewLine +
                                "\t<table>" + Environment.NewLine +
                                "\t\t@wildshapelist@" + Environment.NewLine +
                                "\t</table>" + Environment.NewLine +
                                "</div>" + Environment.NewLine;
            HTMLWildShapeEntry = "\t<tr>" + Environment.NewLine +
                                 "\t\t<td>@name@</td>" + Environment.NewLine +
                                 "\t\t<td>@cr@</td>" + Environment.NewLine +
                                 "\t\t<td>@fly@</td>" + Environment.NewLine +
                                 "\t\t<td>@swim@</td>" + Environment.NewLine +
                                 "\t</tr>" + Environment.NewLine;
        }

        public string constructClassLevelString()
        {
            string output = Choices.Class;
    
            if (Choices.Subclass != "---")
            {
                output += $" ({Choices.Subclass}";
    
                if (Choices.HasDraconicAncestry)
                {
                    output += $", {Choices.Ancestry.Color}";
                }
                output += ")";
            }
    
            output += $" {Choices.Level.ToString()}";
    
            return output;
        }
    
        public string constructAlignmentString()
        {
            return $"{Choices.LawAlignment} {Choices.MoralityAlignment}";
        }

        public string constructSkillString()
        {
            string output = "";
            List<Skill> skills = DBManager.SkillData.getSkills();
            List<string> allKnownSkills = Choices.AllKnownSkills;
    
            foreach (Skill skill in skills)
            {
                string currentSkill = HTMLRadioButton.Replace("@name@", skill.Name);
                AbilityScore currentSkillAbility = Choices.Abilities.FirstOrDefault(ability => ability.Name == skill.Ability);
                string buttonLabel = $"{skill.Name} ({skill.Ability.Substring(0, 3)})";
    
                if (allKnownSkills.Contains(skill.Name))
                {
                    int proficientModifier = 0;
    
                    if ((Choices.ClassSkills.Contains(skill.Name)) && Choices.ClassDoublesProficiency)
                    {
                        proficientModifier = currentSkillAbility.getModifier() + (ProficiencyBonus * 2);
                    }
                    else if ((Choices.SubclassSkills.Contains(skill.Name)) && Choices.SubclassDoublesProficiency)
                    {
                        proficientModifier = currentSkillAbility.getModifier() + (ProficiencyBonus * 2);
                    }
                    else
                    {
                        proficientModifier = currentSkillAbility.getModifier() + ProficiencyBonus;
                    }
    
                    currentSkill = currentSkill.Replace("@text@", $"{proficientModifier.ToString("+#;-#;+0")} {buttonLabel}");
                    currentSkill = currentSkill.Replace("@checked@", HTMLRadioButtonChecked);
                }
                else
                {
                    currentSkill = currentSkill.Replace("@text@", $"{currentSkillAbility.getModifier().ToString("+#;-#;+0")} {buttonLabel}");
                    currentSkill = currentSkill.Replace("@checked@", "");
                }
    
                output += currentSkill;
            }
    
            return output;
        }

        public int calculatePassivePerception()
        {
            int passivePerception = 0;
            int modifier = Choices.Wisdom.getModifier();
    
            if (Choices.AllKnownSkills.Contains("Perception"))
            {
                int proficientModifier = 0;
    
                if ((Choices.ClassSkills.Contains("Perception")) && Choices.ClassDoublesProficiency)
                {
                    proficientModifier = modifier + (ProficiencyBonus * 2);
                }
                else if ((Choices.SubclassSkills.Contains("Perception")) && Choices.SubclassDoublesProficiency)
                {
                    proficientModifier = modifier + (ProficiencyBonus * 2);
                }
                else
                {
                    proficientModifier = modifier + ProficiencyBonus;
                }
    
                passivePerception = 10 + proficientModifier;
            }
            else
            {
                passivePerception = 10 + modifier;
            }
    
            return passivePerception;
        }

        public string constructToolProficiencyString()
        {
            //general tool proficiencies
            List<string> toolProficiencies = DBManager.ExportData.getToolProficiencies(Choices.RaceChoice.getSelectedSubrace().Name, Choices.Class, Choices.Subclass, Choices.BackgroundChoice.Name);

            //race proficiencies
            if (Choices.RaceChoice.getSelectedSubrace().HasProficiencyChoice)
            {
                if (!toolProficiencies.Contains(Choices.RaceChoice.getSelectedSubrace().Proficiency))
                {
                    toolProficiencies.Add(Choices.RaceChoice.getSelectedSubrace().Proficiency);
                }
            }
    
            //class proficiencies
            if (Choices.ClassProficiencies.Count > 0)
            {
                foreach (string entry in Choices.ClassProficiencies)
                {
                    if (!toolProficiencies.Contains(entry))
                    {
                        toolProficiencies.Add(entry);
                    }
                }
            }

            //subclass proficiencies
            if (Choices.HasExtraToolProficiencies)
            {
                if (!toolProficiencies.Contains(Choices.SubclassToolProficiency))
                {
                    toolProficiencies.Add(Choices.SubclassToolProficiency);
                }
            }

            //background proficiencies
            if (Choices.BackgroundChoice.HasProficiencyChoice)
            {
                if (!toolProficiencies.Contains(Choices.BackgroundChoice.Proficiency))
                {
                    toolProficiencies.Add(Choices.BackgroundChoice.Proficiency);
                }
            }
    
            return string.Join(", ", toolProficiencies);
        }

        public string constructLanguageProficiencyString()
        {
            if (DBManager.LanguageData.hasExtraLanguages(Choices.RaceChoice.getSelectedSubrace().Name, Choices.Subclass, Choices.BackgroundChoice.Name))
            {
                return string.Join(", ", Choices.Languages);
            }
    
            return string.Join(", ", DBManager.LanguageData.getDefaultLanguages(Choices.RaceChoice.getSelectedSubrace().Name, Choices.Class, Choices.Subclass));
        }

        public int calculateAC()
        {
            int armoredAC = 0;
            int unarmoredAC = 10;
    
            //calculate armored AC
            List<Armor> ownedArmor = getOwnedArmor();
            if (ownedArmor.Count > 0)
            {
                Armor bodyArmor = ownedArmor.FirstOrDefault(armor => armor.AC > 0);
                if (bodyArmor != null)
                {
                    int armorModifier = 0;

                    AbilityScore armorModifierAbility = Choices.Abilities.FirstOrDefault(ability => ability.Name == bodyArmor.AdditionalModifier);
                    if (armorModifierAbility != null)
                    {
                        armorModifier = armorModifierAbility.getModifier();
                    }

                    if (bodyArmor.AdditionalModifierLimit > 0)
                    {
                        if (armorModifier > bodyArmor.AdditionalModifierLimit)
                        {
                            armorModifier = bodyArmor.AdditionalModifierLimit;
                        }
                    }
                    armoredAC = bodyArmor.AC + armorModifier;
                }
            }
    
            //calculate unarmored AC
            List<string> unarmoredAbilities = DBManager.ExportData.getUnarmoredDefenseAbilities(Choices.Class);
    
            foreach (string ability in unarmoredAbilities)
            {
                AbilityScore unarmoredModifierAbility = Choices.Abilities.FirstOrDefault(score => score.Name == ability);
                unarmoredAC += unarmoredModifierAbility.getModifier();
            }
    
            return Math.Max(armoredAC, unarmoredAC);
        }
    
        private List<Armor> getOwnedArmor()
        {
            List<Armor> ownedArmor = new List<Armor>();
    
            //find armor in chosen equipment
            foreach (EquipmentItem item in Choices.ChosenEquipment)
            {
                Armor currentArmor = item as Armor;
                if (currentArmor != null)
                {
                    ownedArmor.Add(currentArmor);
                }
    
                MultiItem currentMultiItem = item as MultiItem;
                if (currentMultiItem != null)
                {
                    foreach (EquipmentItem itemPart in currentMultiItem.Items)
                    {
                        Armor multiArmor = itemPart as Armor;
                        if (multiArmor != null)
                        {
                            if (multiArmor.AC > 0)
                            {
                                ownedArmor.Add(multiArmor);
                            }
                        }
                    }
                }
            }
    
            //check extra equipment
            foreach (string entry in Choices.ExtraEquipment.Split(',').ToList())
            {
                Armor currentArmor = DBManager.EquipmentData.getArmor(entry);
                if (!string.IsNullOrEmpty(currentArmor.Name))
                {
                    if (currentArmor.AC > 0)
                    {
                        ownedArmor.Add(currentArmor);
                    }
                }
            }
    
            return ownedArmor;
        }

        public string constructAttackTableString()
        {
            string output = "";
    
            foreach (Weapon weapon in getOwnedWeapons())
            {
                //name
                string sanitizedWeaponName = Regex.Replace(weapon.Name, @"\s\([\d]+\)", "").Trim();
                string weaponEntry = HTMLTableRow.Replace("@name@", sanitizedWeaponName);
                int attackModifier = 0;
    
                //atkmodifier
                if (weapon.IsMelee)
                {
                    if (weapon.IsFinesse)
                    {
                        attackModifier = Math.Max(Choices.Strength.getModifier(), Choices.Dexterity.getModifier());
                    }
                    else
                    {
                        attackModifier = Choices.Strength.getModifier();
                    }
                }
                else
                {
                    attackModifier = Choices.Dexterity.getModifier();
                }
    
                if (isProficientInWeapon(weapon))
                {
                    weaponEntry = weaponEntry.Replace("@atkmodifier@", (attackModifier + ProficiencyBonus).ToString("+#;-#;+0"));
                }
                else
                {
                    weaponEntry = weaponEntry.Replace("@atkmodifier@", attackModifier.ToString("+#;-#;+0"));
                }
    
                //damage
                weaponEntry = weaponEntry.Replace("@damage@", string.Join(" ", new string[] { weapon.Damage, weapon.DamageType, attackModifier.ToString("+#;-#;+0") }));
    
                //add entry to output
                output += weaponEntry;
            }
    
            return output;
        }

        public string constructAttackBoxString()
        {
            string output = "";
    
            //weapon properties
            foreach (Weapon weapon in getOwnedWeapons())
            {
                if (weapon.Properties != "-")
                {
                    string sanitizedWeaponName = Regex.Replace(weapon.Name, @"\s\([\d]+\)", "");
                    output += $"{sanitizedWeaponName}: {weapon.Properties}<br>" + Environment.NewLine;
                }
            }
    
            //armor/shield properties
            foreach (Armor armor in getOwnedArmor())
            {
                string armorString = "";
    
                if (armor.AdditionalAC > 0)
                {
                    if (string.IsNullOrEmpty(armorString))
                    {
                        armorString = $"{armor.Name}: ";
                    }
                    armorString += $"{armor.AdditionalAC.ToString("+#;-#;+0")} AC";
                }
    
                if (armor.StealthDisadvantage)
                {
                    if (string.IsNullOrEmpty(armorString))
                    {
                        armorString = $"{armor.Name}: ";
                    }
                    else
                    {
                        armorString += ", ";
                    }
                    armorString += "stealth disadvantage";
                }
    
                if (!string.IsNullOrEmpty(armorString))
                {
                    output += armorString + Environment.NewLine;
                }
            }
    
            return output;
        }
    
        private List<Weapon> getOwnedWeapons()
        {
            List<Weapon> ownedWeapons = new List<Weapon>();
    
            //find weapons in chosen equipment
            foreach (EquipmentItem item in Choices.ChosenEquipment)
            {
                Weapon currentWeapon = item as Weapon;
                if (currentWeapon != null)
                {
                    ownedWeapons.Add(currentWeapon);
                }
    
                MultiItem currentMultiItem = item as MultiItem;
                if (currentMultiItem != null)
                {
                    foreach (EquipmentItem itemPart in currentMultiItem.Items)
                    {
                        Weapon multiWeapon = itemPart as Weapon;
                        if (multiWeapon != null)
                        {
                            ownedWeapons.Add(multiWeapon);
                        }
                    }
                }
            }

            //get weapons from extra equipment
            foreach (string entry in Choices.ExtraEquipment.Split(',').ToList())
            {
                string sanitizedWeaponName = Regex.Replace(entry, @"\s\([\d]+\)", "").Trim();
                Weapon currentWeapon = DBManager.EquipmentData.getWeapon(sanitizedWeaponName);
                if (!string.IsNullOrEmpty(currentWeapon.Name))
                {
                    currentWeapon.Name = entry;
                    ownedWeapons.Add(currentWeapon);
                }
            }
    
            return ownedWeapons;
        }
    
        private bool isProficientInWeapon(Weapon weapon)
        {
            string sanitizedWeaponName = Regex.Replace(weapon.Name, @"\s\([\d]+\)", "");
            return WeaponProficiencies.Contains(sanitizedWeaponName) || WeaponProficiencies.Contains(weapon.Type);
        }

        public string constructEquipmentString()
        {
            //chosen equipment
            string output = "";

            foreach (EquipmentItem item in Choices.ChosenEquipment)
            {
                if (!string.IsNullOrEmpty(output))
                {
                    output += ", ";
                }

                if (item is Pack)
                {
                    Pack pack = (Pack)item;
                    output += pack.Content;
                }
                else
                {
                    output += item.Name;
                }
            }
            
            output += $", {Choices.ExtraEquipment}";

            //background equipment
            if (Choices.BackgroundChoice.HasProficiencyChoice)
            {
                output += $", {Choices.BackgroundChoice.Proficiency}";
            }
            output += $", {Choices.BackgroundChoice.Equipment}";

            //substitute pack for pack content
            if (output.Contains("pack"))
            {
                int packIndex = output.IndexOf("pack");
                int commaAfterPack = output.IndexOf(",", packIndex);
                int packNameFirstLetter = output.LastIndexOf(",", packIndex) + 2;
                string packName = output.Substring(packNameFirstLetter, commaAfterPack - packNameFirstLetter);
                output.Replace(packName, DBManager.EquipmentData.getPackContent(packName));
            }

            return output;
        }

        /// <summary>
        /// splits the feature list after the given percent and formats them to HTML strings
        /// </summary>
        /// <param name="featureList">list of features to process</param>
        /// <param name="splitPercent">percentage of features in the first string</param>
        /// <returns>a list of strings of HTML formatted features</returns>
        public List<string> constructFeatureString(List<Feature> featureList, float splitPercent)
        {
            string firstFeatureString = "";
            string secondFeatureString = "";

            for (int i = 0; i < featureList.Count; i++)
            {
                string featureString = $"<b>{featureList[i].Name}.</b> {featureList[i].Description}<br><br>";

                if (i < (featureList.Count * splitPercent))
                {
                    firstFeatureString += featureString + Environment.NewLine + Environment.NewLine;
                }
                else
                {
                    secondFeatureString += featureString + Environment.NewLine + Environment.NewLine;
                }
            }

            List<string> outputList = new List<string>();
            outputList.Add(firstFeatureString);
            outputList.Add(secondFeatureString);

            return outputList;
        }

        public string formatSpellcastingAbility()
        {
            if (SpellcastingAbility.Length < 3)
            {
                return SpellcastingAbility.ToUpper();
            }

            return SpellcastingAbility.Substring(0, 3).ToUpper();
        }

        public int calculateSpellModifier()
        {
            AbilityScore abilityScore = Choices.Abilities.FirstOrDefault(ability => ability.Name == SpellcastingAbility);
            if (abilityScore != null)
            {
                return abilityScore.getModifier() + ProficiencyBonus;
            }

            return 0;
        }

        public int calculateSpellDC()
        {
            return 8 + calculateSpellModifier();
        }

        public string constructSpellbook(List<Spell> spellList)
        {
            string output = "";

            if (spellList.Count > 0)
            {
                output = HTMLSpellbook;
                string spellbook = "";

                //sort spell list
                spellList.Sort((s1, s2) => s1.Name.CompareTo(s2.Name));

                foreach (Spell spell in spellList)
                {
                    BonusSpell bonusSpell = spell as BonusSpell;
                    ExtraRaceSpell raceSpell = spell as ExtraRaceSpell;

                    string entry = HTMLSpellbookEntry;

                    if (raceSpell != null)
                    {
                        string spellName = raceSpell.Name + " (" + raceSpell.SpellcastingAbility + ")";
                        entry = entry.Replace("@name@", spellName);

                        string spellLevel = raceSpell.Level.ToString() + " (max. " + raceSpell.MaxSpellLevel.ToString() + ")";
                        entry = entry.Replace("@level@", spellLevel);
                    }
                    else
                    {
                        entry = entry.Replace("@name@", spell.Name);
                        entry = entry.Replace("@level@", spell.Level.ToString());
                    }

                    if (bonusSpell != null)
                    {
                        if (bonusSpell.OnlyAsRitual)
                        {
                            entry = entry.Replace("@ritual@", "(only as ritual)");
                        }
                        else
                        {
                            if (bonusSpell.Ritual)
                            {
                                entry = entry.Replace("@ritual@", "(ritual)");
                            }
                            else
                            {
                                entry = entry.Replace("@ritual@", "");
                            }
                        }

                    }
                    else
                    {
                        if (spell.Ritual)
                        {
                            entry = entry.Replace("@ritual@", "(ritual)");
                        }
                        else
                        {
                            entry = entry.Replace("@ritual@", "");
                        }
                    }

                    entry = entry.Replace("@school@", spell.School);
                    entry = entry.Replace("@casttime@", spell.CastTime);
                    entry = entry.Replace("@range@", spell.Range);
                    entry = entry.Replace("@components@", spell.Components);
                    entry = entry.Replace("@duration@", spell.Duration);
                    entry = entry.Replace("@description@", spell.Description);

                    spellbook += entry;
                }

                output = output.Replace("@charactername@", Choices.CharacterName);
                output = output.Replace("@spellbook@", spellbook);
            }

            return output;
        }

        public string constructWildShapeList()
        {
            string output = "";

            if (Choices.HasWildShape)
            {
                output = HTMLWildShapeList.Replace("@charactername@", Choices.CharacterName);
                output = output.Replace("@terrain@", Choices.TerrainChoice.Name);

                string beastList = "";

                foreach (WildShapeBeast beast in Choices.TerrainChoice.Beasts)
                {
                    string entry = HTMLWildShapeEntry.Replace("@name@", beast.Name);
                    entry = entry.Replace("@cr@", beast.CR.ToString());

                    if (beast.Fly)
                    {
                        entry = entry.Replace("@fly@", "Fly");
                    }
                    else
                    {
                        entry = entry.Replace("@fly@", "-");
                    }

                    if (beast.Swim)
                    {
                        entry = entry.Replace("@swim@", "Swim");
                    }
                    else
                    {
                        entry = entry.Replace("@swim@", "-");
                    }

                    beastList += entry;
                }

                output = output.Replace("@wildshapelist@", beastList);
            }

            return output;
        }
    }
}
