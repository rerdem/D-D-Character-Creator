using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator
{
    public class StringExtensions
    {
        /// <summary>
        /// replaces the first occurence of search in text with replacement
        /// </summary>
        /// <param name="text">the text in which the replacement takes place</param>
        /// <param name="search">the string that will be replaced</param>
        /// <param name="replacement">the string that search is replace with</param>
        /// <returns>a string where the first occurence of search in text is replaced by replacement</returns>
        public static string ReplaceFirst(string text, string search, string replacement)
        {
            int index = text.IndexOf(search);

            if (index < 0)
            {
                return text;
            }

            return text.Substring(0, index) + replacement + text.Substring(index + search.Length);
        }
    }
}
