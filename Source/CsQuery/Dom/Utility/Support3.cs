using System;
using System.Collections.Generic;

using System.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;


using CsQuery.StringScanner;
using CsQuery.Implementation;

namespace CsQuery.Utility
{
    /// <summary>
    /// Some static methods that didn't fit in anywhere else. 
    /// </summary>
    public static class Support3
    {

        /// <summary>
        ///  Gets a resource from the calling assembly
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public static Stream GetResourceStream(string resourceName)
        {
            return GetResourceStream(resourceName, Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Gets a resource name using the assembly and resource name
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static Stream GetResourceStream(string resourceName, Assembly assembly)
        {

            Stream fileStream = assembly.GetManifestResourceStream(resourceName);
            return (fileStream);
        }

        /// <summary>
        /// Gets an embedded resource from an assembly by name
        /// </summary>
        ///
        /// <param name="resourceName">
        /// The resource name
        /// </param>
        /// <param name="assembly">
        /// The assembly name
        /// </param>
        ///
        /// <returns>
        /// The resource stream.
        /// </returns>

        //public static Stream GetResourceStream(string resourceName, string assembly)
        //{
        //    Assembly loadedAssembly = Assembly.Load(assembly);
        //    return GetResourceStream(resourceName, loadedAssembly);
        //}
        /// <summary>
        ///Convert a string value to a double, or zero if non-numeric
        /// </summary>
        ///
        /// <param name="value">
        /// The value.
        /// </param>
        ///
        /// <returns>
        /// A double.
        /// </returns>

        public static double DoubleOrZero(string value)
        {
            double dblVal;
            if (double.TryParse(value, out dblVal))
            {
                return dblVal;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Convert a string value to an integer, or zero if non-numeric
        /// </summary>
        ///
        /// <param name="value">
        /// The value.
        /// </param>
        ///
        /// <returns>
        /// An integer
        /// </returns>

        public static int IntOrZero(string value)
        {
            int intVal;
            if (int.TryParse(value, out intVal))
            {
                return intVal;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Given a string, convert each uppercase letter to a "-" followed by the lower case letter.
        /// E.g. "fontSize" becomes "font-size".
        /// </summary>
        ///
        /// <param name="name">
        /// The string to uncamelcase
        /// </param>
        ///
        /// <returns>
        /// A string
        /// </returns>

        public static string FromCamelCase(string name)
        {

            if (String.IsNullOrEmpty(name))
            {
                return "";
            }

            int pos = 0;
            StringBuilder output = new StringBuilder();

            while (pos < name.Length)
            {
                char cur = name[pos];
                if (cur >= 'A' && cur <= 'Z')
                {
                    if (pos > 0 && name[pos - 1] != '-')
                    {
                        output.Append("-");
                    }
                    output.Append(Char.ToLower(cur));
                }
                else
                {
                    output.Append(cur);
                }
                pos++;
            }
            return output.ToString();

        }

        /// <summary>
        /// Convert an enum to a lowercased attribute value
        /// </summary>
        ///
        /// <param name="value">
        /// The value.
        /// </param>
        ///
        /// <returns>
        /// The attribute value of a string
        /// </returns>

        public static string EnumToAttribute(Enum value)
        {
            return value.ToString().ToLower();
        }
    }

}
