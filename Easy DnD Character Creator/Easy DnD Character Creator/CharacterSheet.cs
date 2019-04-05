using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator
{
    public class CharacterSheet
    {
        private DataManager DBManager { get; }
        private ChoiceManager Choices { get; }

        private CharacterSheetStringBuilder CharSheetStrings { get; }

        //full HTML template file as a string
        private string template;

        private string HTMLRadioButtonChecked;
        private List<Spell> allKnownSpells;


        public CharacterSheet(DataManager inputDataManager, ChoiceManager inputChoiceManager)
        {
            DBManager = inputDataManager;
            Choices = inputChoiceManager;

            CharSheetStrings = new CharacterSheetStringBuilder(inputDataManager, inputChoiceManager);

            template = "";

            HTMLRadioButtonChecked = " checked=\"checked\"";
            allKnownSpells = new List<Spell>();
        }

        /// <summary>
        /// saves the current HTML template to the given file
        /// </summary>
        /// <param name="path">full path of the save file</param>
        public void saveToHTML(string path)
        {
            int indexOfLastBackslash = path.LastIndexOf("\\");
            FileInfo file = new FileInfo(path.Substring(0, indexOfLastBackslash));
            file.Directory.Create();
            File.WriteAllText(path, template);
        }

        /// <summary>
        /// fills the html template with the character choices
        /// </summary>
        public void fillCharacterSheet()
        {
            //reset to blank template
            template = DBManager.ExportData.getHTMLTemplate();

            //get information necessary for final calculations
            allKnownSpells = constructSpellList();
            CharSheetStrings.ProficiencyBonus = DBManager.ExportData.getProficiencyBonus(Choices.Level);
            CharSheetStrings.WeaponProficiencies = string.Join(", ", DBManager.ExportData.getWeaponProficiencies(Choices.RaceChoice.getSelectedSubrace().Name, Choices.ClassChoice.Name, Choices.ClassChoice.getSelectedSubclass().Name, Choices.BackgroundChoice.Name));
            CharSheetStrings.SpellcastingAbility = DBManager.SpellData.getSpellcastingAbility(Choices.ClassChoice.Name, Choices.ClassChoice.getSelectedSubclass().Name);

            //fill template with information

            //character name
            template = template.Replace("@charactername@", Choices.CharacterName);

            //class & level
            template = template.Replace("@classlevel@", CharSheetStrings.constructClassLevelString());

            //background
            template = template.Replace("@background@", Choices.BackgroundChoice.Name);

            //playername
            template = template.Replace("@playername@", Choices.PlayerName);

            //race
            template = template.Replace("@race@", Choices.RaceChoice.getSelectedSubrace().Name);

            //alignment
            template = template.Replace("@alignment@", CharSheetStrings.constructAlignmentString());

            //xp
            template = template.Replace("@xp@", DBManager.ExportData.getXpForLevel(Choices.Level).ToString());

            //stats, saving throws
            List<string> saveProficiencies = DBManager.ExportData.getSaveProficiencies(Choices.ClassChoice.Name);
            foreach (AbilityScore score in Choices.Abilities)
            {
                template = template.Replace("@" + score.getShortName() + "@", score.getTotalValue().ToString());
                template = template.Replace("@" + score.getShortName() + "mod@", score.getModifier().ToString("+#;-#;+0"));
                
                if (saveProficiencies.Contains(score.Name))
                {
                    int proficientModifier = score.getModifier() + CharSheetStrings.ProficiencyBonus;
                    template = template.Replace("@" + score.getShortName() + "save@", proficientModifier.ToString("+#;-#;+0") + " " + score.Name);
                    template = template.Replace("@" + score.getShortName() + "saveproficiency@", HTMLRadioButtonChecked);
                }
                else
                {
                    template = template.Replace("@" + score.getShortName() + "save@", score.getModifier().ToString("+#;-#;+0") + " " + score.Name);
                    template = template.Replace("@" + score.getShortName() + "saveproficiency@", "");
                }
            }

            //intiative
            template = template.Replace("@initiative@", Choices.Dexterity.getModifier().ToString("+#;-#;+0"));

            //skills
            template = template.Replace("@skills@", CharSheetStrings.constructSkillString());

            //passive perception
            template = template.Replace("@passiveperception@", CharSheetStrings.calculatePassivePerception().ToString());

            //proficiency bonus
            template = template.Replace("@proficiencybonus@", CharSheetStrings.ProficiencyBonus.ToString("+#;-#;+0"));

            //proficiencies
            //armor
            template = template.Replace("@armorproficiencies@", string.Join(", ", DBManager.ExportData.getArmorProficiencies(Choices.RaceChoice.getSelectedSubrace().Name, 
                                                                                                                             Choices.ClassChoice.Name, 
                                                                                                                             Choices.ClassChoice.getSelectedSubclass().Name, 
                                                                                                                             Choices.BackgroundChoice.Name)));

            //weapons
            template = template.Replace("@weaponproficiencies@", CharSheetStrings.WeaponProficiencies);

            //tools
            template = template.Replace("@toolproficiencies@", string.Join(", ", CharSheetStrings.constructToolProficiencyString()));

            //languages
            template = template.Replace("@languageproficiencies@", string.Join(", ", CharSheetStrings.constructLanguageProficiencyString()));

            //ac
            template = template.Replace("@ac@", CharSheetStrings.calculateAC().ToString());

            //speed
            template = template.Replace("@speed@", DBManager.RaceData.getSpeed(Choices.RaceChoice.getSelectedSubrace().Name).ToString());

            //HP
            template = template.Replace("@hp@", Choices.HP.ToString());

            //hit dice
            template = template.Replace("@hitdice@", Choices.Level.ToString() + Choices.ClassChoice.HitDie);

            //weapons & attacks
            template = template.Replace("@attacktableentries@", CharSheetStrings.constructAttackTableString());
            template = template.Replace("@attackboxtext@", CharSheetStrings.constructAttackBoxString());

            //equipment
            template = template.Replace("@equipment@", CharSheetStrings.constructEquipmentString());

            //money
            template = template.Replace("@cp@", Convert.ToString(0));
            template = template.Replace("@sp@", Convert.ToString(0));
            template = template.Replace("@ep@", Convert.ToString(0));
            template = template.Replace("@pp@", Convert.ToString(0));

            if (Choices.AdjustStartingMoney)
            {
                template = template.Replace("@gp@", Choices.BackgroundChoice.Gold + DBManager.getStartingGoldAdjustment(Choices.Level).ToString());
            }
            else
            {
                template = template.Replace("@gp@", Choices.BackgroundChoice.Gold.ToString());
            }

            //personality traits, ideals, bonds, flaws, backstory
            template = template.Replace("@personalitytraits@", Choices.Trait);
            template = template.Replace("@ideals@", Choices.Ideal);
            template = template.Replace("@bonds@", Choices.Bond);
            template = template.Replace("@flaws@", Choices.Flaw);
            template = template.Replace("@backstory@", Choices.Backstory);

            //character appearance
            template = template.Replace("@age@", Choices.Age.ToString());
            template = template.Replace("@height@", Choices.Height);
            template = template.Replace("@weight@", Choices.Weight);
            template = template.Replace("@eyes@", Choices.EyeColor);
            template = template.Replace("@skin@", Choices.SkinColor);
            template = template.Replace("@hair@", Choices.HairColor);

            //features
            List<string> featureStrings = CharSheetStrings.constructFeatureString(constructFeatureList(), 0.5f);
            template = template.Replace("@features1@", featureStrings[0]);
            template = template.Replace("@features2@", featureStrings[1]);

            //spellcasting ability
            template = template.Replace("@spellcastingability@", CharSheetStrings.formatSpellcastingAbility());

            //spell DC
            template = template.Replace("@spelldc@", CharSheetStrings.calculateSpellDC().ToString());

            //spell attack modifier
            template = template.Replace("@spellatk@", CharSheetStrings.calculateSpellModifier().ToString("+#;-#;+0"));

            //number of spell slots
            for (int i = 1; i < 10; i++)
            {
                template = template.Replace($"@slot{i}@", DBManager.SpellData.getSpellSlots(Choices.ClassChoice.Name, Choices.ClassChoice.getSelectedSubclass().Name, Choices.Level, i).ToString());
            }

            //cantrips and spells
            foreach (Spell spell in allKnownSpells)
            {
                if (spell.isCantrip())
                {
                    template = StringExtensions.ReplaceFirst(template, "@cantrip@", spell.Name);
                }
                else
                {
                    template = StringExtensions.ReplaceFirst(template, $"@spell{spell.Level}@", spell.Name);
                }
            }

            //remove remaining spell strings
            template = template.Replace("@cantrip@", "<br>" + Environment.NewLine);
            for (int i = 0; i < 10; i++)
            {
                template = template.Replace($"@spell{i}@", "<br>" + Environment.NewLine);
            }

            //spellbook
            template = template.Replace("@spellbook@", CharSheetStrings.constructSpellbook(allKnownSpells));

            //wild shape
            template = template.Replace("@wildshapelist@", CharSheetStrings.constructWildShapeList());
        }

        private List<Feature> constructFeatureList()
        {
            List<Feature> featureList = DBManager.ExportData.getFeatures(Choices.RaceChoice.getSelectedSubrace().Name, Choices.ClassChoice.Name, Choices.ClassChoice.getSelectedSubclass().Name, Choices.BackgroundChoice.Name, Choices.Level);

            //add additional feature
            //fighting style
            if (Choices.ClassChoice.HasFightingStyle)
            {
                foreach (FightingStyle style in Choices.ClassChoice.FightingStyles)
                {
                    featureList.Add(new Feature(style.Name, style.Description));
                }
            }

            ////Rogue Thieves' Tools exptertise
            //if (!string.IsNullOrEmpty(DBManager.ExtraClassChoiceData.ExtraClassSkillData.getExtraSkillCheckbox(Choices.ClassChoice.Name)))
            //{
            //    if (Choices.ClassChoice.NameSkills.Contains(DBManager.ExtraClassChoiceData.ExtraClassSkillData.getExtraSkillCheckbox(Choices.ClassChoice.Name)))
            //    {
            //        featureList.Add(DBManager.ExportData.getFeatureById(60));
            //    }
            //}

            //class/subclass expertise
            if (Choices.ClassChoice.HasExtraSkills)
            {
                if (Choices.ClassChoice.DoublesProficiency)
                {
                    string description = $"Your proficiency bonus is doubled for any ability check using {Choices.ClassChoice.ExtraSkills}.";
                    featureList.Add(new Feature($"{Choices.ClassChoice.Name} Expertise", description));
                }
            }

            if (Choices.ClassChoice.getSelectedSubclass().HasExtraSkills)
            {
                if (Choices.ClassChoice.getSelectedSubclass().DoublesProficiency)
                {
                    string description = $"Your proficiency bonus is doubled for any ability check using {Choices.ClassChoice.getSelectedSubclass().ExtraSkills}.";
                    featureList.Add(new Feature($"{Choices.ClassChoice.getSelectedSubclass().Name} Expertise", description));
                }
            }

            //background choices
            if (Choices.BackgroundChoice.HasStoryChoice)
            {
                string featureTitle = $"{Choices.BackgroundChoice.StoryChoice.Name} ({Choices.BackgroundChoice.StoryChoice.getSelectedOption()})";
                featureList.Add(new Feature(featureTitle, Choices.BackgroundChoice.StoryChoice.Description));
            }

            //elemental disciplines
            if (Choices.ClassChoice.getSelectedSubclass().HasElementalDisciplines)
            {
                foreach (ElementalDiscipline discipline in new List<ElementalDiscipline>().Concat(Choices.ClassChoice.getSelectedSubclass().MandatoryDisciplines)
                                                                                          .Concat(Choices.ClassChoice.getSelectedSubclass().ChosenDisciplines)
                                                                                          .ToList())
                {
                    featureList.Add(new Feature(discipline.Name, discipline.Description));
                }
            }


            //warlock pact & invocations
            if (Choices.ClassChoice.HasWarlockChoices)
            {
                //pact
                featureList.Add(new Feature(Choices.ClassChoice.WarlockPactChoice.Name, Choices.ClassChoice.WarlockPactChoice.Description));

                //invocations
                foreach (EldritchInvocation invocation in Choices.ClassChoice.WarlockInvocations)
                {
                    featureList.Add(new Feature(invocation.Name, invocation.Description));
                }
            }

            //hunter features
            if (Choices.ClassChoice.getSelectedSubclass().HasHunterChoices)
            {
                foreach (ChoiceFeature feature in Choices.ClassChoice.getSelectedSubclass().HunterFeatures)
                {
                    string featureTitle = feature.Name + " (" + feature.getSelectedOption().Name + ")";
                    featureList.Add(new Feature(featureTitle, feature.getSelectedOption().Description));
                }
            }

            //metamagic
            if (Choices.ClassChoice.HasMetamagic)
            {
                foreach (Metamagic entry in Choices.ClassChoice.SorcererMetamagic)
                {
                    featureList.Add(new Feature(entry.Name, entry.Description));
                }
            }

            //totem features
            if (Choices.ClassChoice.getSelectedSubclass().HasTotems)
            {
                foreach (ChoiceFeature totem in Choices.ClassChoice.getSelectedSubclass().TotemFeatures)
                {
                    string featureTitle = totem.Name + " (" + totem.getSelectedOption().Name + ")";
                    featureList.Add(new Feature(featureTitle, totem.getSelectedOption().Description));
                }
            }

            //maneuvers
            if (Choices.ClassChoice.getSelectedSubclass().HasManeuvers)
            {
                foreach (Maneuver maneuver in Choices.ClassChoice.getSelectedSubclass().Maneuvers)
                {
                    featureList.Add(new Feature(maneuver.Name, maneuver.Description));
                }
            }

            //favored enemy
            if (Choices.ClassChoice.HasFavoredEnemy)
            {
                foreach (Feature feature in featureList.Where(entry => entry.Name == "Favored Enemy"))
                {
                    feature.Description = $"({Choices.ClassChoice.FavoredEnemies}) {feature.Description}";
                }
            }

            //favored terrain
            if (Choices.ClassChoice.HasFavoredTerrain)
            {
                foreach (Feature feature in featureList.Where(entry => entry.Name == "Natural Explorer"))
                {
                    feature.Description = $"({Choices.ClassChoice.FavoredTerrains}) {feature.Description}";
                }
            }

            //Beast Compantion
            if (Choices.ClassChoice.getSelectedSubclass().HasCompanion)
            {
                foreach (Feature feature in featureList.Where(entry => entry.Name == "Ranger's Companion"))
                {
                    string compantionString = $"{Choices.ClassChoice.getSelectedSubclass().BeastCompanion.Name} -> {Choices.ClassChoice.getSelectedSubclass().BeastCompanion.Book} p.{Choices.ClassChoice.getSelectedSubclass().BeastCompanion.Page}";
                    feature.Description = $"({compantionString }) {feature.Description}";
                }
            }

            //rage
            foreach (Feature feature in featureList.Where(entry => entry.Name == "Rage"))
            {
                BarbarianRage rage = DBManager.ExportData.getBarbarianRage(Choices.Level);
                feature.Description = $"({rage.RageAmount}, {rage.DamageBonus}) {feature.Description}";
            }

            //bardic inspiration
            foreach (Feature feature in featureList.Where(entry => entry.Name == "Bardic Inspiration"))
            {
                feature.Description = $"({DBManager.ExportData.getBardicInspirationDie(Choices.Level)}) {feature.Description}";
            }

            //sorcery points
            foreach (Feature feature in featureList.Where(entry => entry.Name == "Font of Magic"))
            {
                feature.Description = $"({DBManager.ExportData.getSorceryPoints(Choices.Level)} Sorcery Points) {feature.Description}";
            }

            //ki
            foreach (Feature feature in featureList.Where(entry => entry.Name == "Ki"))
            {
                feature.Description = $"({DBManager.ExportData.getKiPoints(Choices.Level)}) {feature.Description}";
            }

            //martial arts
            foreach (Feature feature in featureList.Where(entry => entry.Name == "Martial Arts"))
            {
                feature.Description = $"({DBManager.ExportData.getMartialArtsDie(Choices.Level)}) {feature.Description}";
            }

            //dragonborn breath weapon
            foreach (Feature feature in featureList.Where(entry => entry.Name == "Breath Weapon"))
            {
                feature.Description = DBManager.ExportData.getBreathWeapon(Choices.RaceChoice.getSelectedSubrace().Name, Choices.Level);
            }

            return featureList;
        }

        private List<Spell> constructSpellList()
        {
            List<Spell> spellList = new List<Spell>();

            //spellcasting
            if (Choices.ClassChoice.HasSpellcasting)
            {
                if (Choices.ClassChoice.ChoosesSpells)
                {
                    spellList.AddRange(Choices.Spells);
                }
                else
                {
                    //so far Paladin is the only spellcaster that doesn't choose spells, but knows them all
                    spellList.AddRange(DBManager.SpellData.getCantripOptions(Choices.ClassChoice.Name, Choices.ClassChoice.getSelectedSubclass().Name));
                    spellList.AddRange(DBManager.SpellData.getSpellOptions(Choices.ClassChoice.Name, Choices.ClassChoice.getSelectedSubclass().Name, Choices.Level));
                }
            }

            //race spell
            if (Choices.RaceChoice.getSelectedSubrace().HasExtraSpells)
            {
                spellList.AddRange(Choices.RaceSpells);
            }

            //subclass spell
            if (Choices.ClassChoice.getSelectedSubclass().HasExtraSpells)
            {
                spellList.AddRange(Choices.ClassChoice.getSelectedSubclass().ExtraSpells);
            }

            //warlock spells (pact/invocations)
            if (Choices.ClassChoice.HasWarlockChoices)
            {
                spellList.AddRange(Choices.ClassChoice.WarlockPactSpells);
                spellList.AddRange(Choices.ClassChoice.WarlockInvocationSpells);

                foreach (EldritchInvocation invocation in Choices.ClassChoice.WarlockInvocations)
                {
                    if (!string.IsNullOrEmpty(invocation.GainedSpell.Name))
                    {
                        spellList.Add(invocation.GainedSpell);
                    }
                }
            }

            //elemental disciplines
            if (Choices.ClassChoice.getSelectedSubclass().HasElementalDisciplines)
            {
                foreach (ElementalDiscipline discipline in Choices.ClassChoice.getSelectedSubclass().MandatoryDisciplines)
                {
                    if (!string.IsNullOrEmpty(discipline.GainedSpell.Name))
                    {
                        spellList.Add(discipline.GainedSpell);
                    }
                }

                foreach (ElementalDiscipline discipline in Choices.ClassChoice.getSelectedSubclass().ChosenDisciplines)
                {
                    if (!string.IsNullOrEmpty(discipline.GainedSpell.Name))
                    {
                        spellList.Add(discipline.GainedSpell);
                    }
                }
            }

            //druid circle spells
            if (Choices.ClassChoice.getSelectedSubclass().HasCircleTerrain)
            {
                spellList.AddRange(Choices.ClassChoice.getSelectedSubclass().DruidCircleTerrain.Spells);
            }

            //bonus spells (Shadow arts, totem warrior)
            spellList.AddRange(DBManager.SpellData.getSubclasBonusSpells(Choices.ClassChoice.getSelectedSubclass().Name));

            return spellList;
        }


    }
}
