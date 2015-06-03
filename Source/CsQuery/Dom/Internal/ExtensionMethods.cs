using System;
using System.Collections.Generic;
using System.Collections; 
using CsQuery.StringScanner;
 
namespace CsQuery.ExtensionMethods.Internal
{
    static class ExtensionMethods
    {
        /// <summary>
        /// Clean a string by converts null to an empty string and trimming any whitespace from the beginning and end
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CleanUp(this string value)
        {
            return (value ?? String.Empty).Trim();
        } 
        /// <summary>
        /// Perform a string split using whitespace demarcators (' ', tab, newline, return) and trimming each result
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IEnumerable<string> SplitClean(this string text)
        {
            return SplitClean(text, CharacterData.charsHtmlSpaceArray);
        }

        /// <summary>
        /// Perform a string split that also trims whitespace from each result and removes duplicats
        /// </summary>
        /// <param name="text"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static IEnumerable<string> SplitClean(this string text, char separator)
        {
            char[] sep = new char[1];
            sep[0] = separator;
            return SplitClean(text, sep);
        }

        /// <summary>
        /// Perform a string split that also trims whitespace from each result and removes duplicats
        /// </summary>
        /// <param name="text"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static IEnumerable<string> SplitClean(this string text, char[] separator)
        {

            string[] list = (text ?? "").Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (list.Length > 0)
            {
                HashSet<string> UniqueList = new HashSet<string>();
                for (int i = 0; i < list.Length; i++)
                {
                    if (UniqueList.Add(list[i]))
                    {
                        yield return list[i].Trim();
                    }
                }
            }
            yield break;
        }

    }
}
 