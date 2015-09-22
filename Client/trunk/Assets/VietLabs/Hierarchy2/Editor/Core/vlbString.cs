using System;
using System.Collections;
using System.Text;

namespace vietlabs
{
    internal static class vlbString
    {
        public static string ReplaceEvery(this string src, IList oldStrArr, string newStr) {
            for (var i = 0; i < oldStrArr.Count; i++) {
                src = src.Replace((string)oldStrArr[i], newStr);
            }
            return src;
        }

        public static string[] Split(this string src, string spliter, bool removeEmpty = false) {
            return src.Split(new [] { spliter }, removeEmpty ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
        }

        public static string Duplicate(this string str, int nTimes) {
            if (string.IsNullOrEmpty(str)) return str;

            var builder = new StringBuilder();
            for (var i = 0; i < nTimes; i++) {builder.Append(str); }
            return builder.ToString();
        }

        public static string Enum2String(this Enum e) {
            if (e == null) return null;
            var names = Enum.GetNames(e.GetType());
            var values = Enum.GetValues(e.GetType());
            return names[Array.IndexOf(values, e)];
        }

        public static string InQuote(this object str) { return "\"" + str + "\""; }
        public static string InBrace(this object str) { return "{" + str + "}"; }
        public static string InParenthese(this object str) { return "(" + str + ")"; }
        public static string InSquareBrace(this object str) { return "[" + str + "]"; }


    }
}

