using System;

namespace AddNewItem_Template.Shared
{
    public static class StringExtensions
    {
        public static string TrimEnd(this string str, string trim, bool caseSensitive)
        {
            if (!caseSensitive)
            {
                return str.TrimEnd(trim.ToCharArray());
            }

            if (str.EndsWith(trim, StringComparison.CurrentCultureIgnoreCase))
            {
                int startIndex = str.Length - trim.Length;
                return str.Remove(startIndex, trim.Length);
            }

            return str;
        }

        // taken from https://stackoverflow.com/a/21755933/11787265
        public static string FirstCharToLowerCase(this string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsLower(str[0]))
                return str;

            return char.ToLower(str[0]) + str.Substring(1);
        }
    }
}
