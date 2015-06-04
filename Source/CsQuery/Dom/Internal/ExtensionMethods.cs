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
            return SplitClean(text, new char[] { separator });
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
            int j = 0;
            if ((j = list.Length) > 0)
            {
                var uniquelist = new Dictionary<string, int>();
                for (int i = 0; i < j; ++i)
                {
                    string item = list[i].Trim();
                    if (!uniquelist.ContainsKey(item))
                    {
                        uniquelist.Add(item, 0);
                        yield return item;
                    }
                }
            }
            yield break;
        }

    }
}
