﻿using Easy_DnD_Character_Creator.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.WizardComponents
{
    public class SpellFormatter
    {
        public static string formatSpellDescription(Spell spell)
        {
            string output = "";
            
            if (spell != null)
            {
                //title
                output = spell.Name;
                output += Environment.NewLine;

                //level, school, ritual
                output += "(Level ";
                output += spell.Level.ToString();
                output += " ";
                output += spell.School;

                if (spell.Ritual)
                {
                    output += ", Ritual";
                }

                output += ")";
                output += Environment.NewLine;

                //casting time, range
                output += "Casting Time: ";
                output += spell.CastTime;
                output += " | Range: ";
                output += spell.Range;
                output += Environment.NewLine;

                //components, duration
                output += "Components: ";
                output += spell.Components;
                output += " | Duration: ";
                output += spell.Duration;
                output += Environment.NewLine;

                output += Environment.NewLine;

                //description
                output += spell.Description;

                //use proper line breaks
                output = output.Replace("<br>", Environment.NewLine);
            }
            
            return output;
        }
    }
}
