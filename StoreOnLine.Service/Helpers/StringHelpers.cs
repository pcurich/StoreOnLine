using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace StoreOnLine.Service.Helpers
{
    public static class StringHelpers
    {
        public static string ToSeoUrl(this string url)
        {
            var encodedUrl = (url ?? "").ToLower(); //minusculas
            encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and"); // remplazar & por and
            encodedUrl = encodedUrl.Replace("'", ""); //limpiar 
            // remove invalid characters
           // encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");
            // remove duplicates
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");
            encodedUrl = encodedUrl.Trim('-');// trim leading & trailing characters
            return encodedUrl;
        }
        public static string Left(this string param, int length)
        {
            var result = param.Substring(0, length);
            return result;
        }

        public static string Right(this string param, int length)
        {
            var result = param.Substring(param.Length - length, length);
            return result;
        }

        public static string Middle(this string param, int startIndex, int length)
        {
            var result = param.Substring(startIndex, length);
            return result;
        }

        public static string Middle(this string param, int startIndex)
        {
            var result = param.Substring(startIndex);
            return result;
        }
        public static string Limit(this string text, int limitcharaters)
        {
            if (text.Length > limitcharaters)
                text = text.Substring(0, limitcharaters) + " ...";
            return text;
        }
        public static string ToUpperCaseFirst(this string paragraph)
        {
            paragraph = paragraph.Trim();
            // Check for empty string.
            if (string.IsNullOrEmpty(paragraph))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(paragraph[0]) + paragraph.Substring(1).ToLower();
        }
        /// <summary>
        /// Format DateTime to MM/dd/yyyy hh:mm:ss
        /// </summary>
        /// <param name="dt">Date to be convert</param>
        /// <returns>MM/dd/yyy hh:mm:ss</returns>
        public static string ToSqlformat(this DateTime dt)
        {
            return dt.ToString("MM/dd/yyyy hh:mm:ss");
        }
        /// <summary>
        /// Use to convert to unsign word
        /// </summary>
        public static string ToKeyWord(this string unicodeString)
        {
            try
            {
                //Remove VietNamese character
                unicodeString = unicodeString.ToLower();
                unicodeString = Regex.Replace(unicodeString, "[áàảãạâấầẩẫậăắằẳẵặ]", "a");
                unicodeString = Regex.Replace(unicodeString, "[éèẻẽẹêếềểễệ]", "e");
                unicodeString = Regex.Replace(unicodeString, "[iíìỉĩị]", "i");
                unicodeString = Regex.Replace(unicodeString, "[óòỏõọơớờởỡợôốồổỗộ]", "o");
                unicodeString = Regex.Replace(unicodeString, "[úùủũụưứừửữự]", "u");
                unicodeString = Regex.Replace(unicodeString, "[yýỳỷỹỵ]", "y");
                unicodeString = Regex.Replace(unicodeString, "[đ]", "d");

                //Remove special character
                unicodeString = Regex.Replace(unicodeString, "['~!@#$%^&*()-+=:?/>.<,{}[]|]\\]", " ");
                //Remove space
                unicodeString = Regex.Replace(unicodeString, "[-+-]", " ");
                unicodeString = Regex.Replace(unicodeString, @"\s+", " ");
                //return unicodeString;
                //one more remove invalid characters
                var regex = new Regex(@"[^a-zA-Z0-9\s]", RegexOptions.None);
                return regex.Replace(unicodeString, "");
            }
            catch
            {
                return "";
            }
        }
        public static string ToSeoString(this string text)
        {
            return text.ToKeyWord().Replace(" ", "-");
        }

        public static string ToFolderYmd(DateTime dt)
        {
            return dt.Year + "/" + dt.Month + "/" + dt.Day;
        }

        public static string HighlightKeyWords(this string text, string keywords, string cssClass, bool fullMatch)
        {
            if (text == String.Empty || string.IsNullOrEmpty(keywords) || cssClass == String.Empty)
                return text;

            var words = keywords.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (!fullMatch)
                return words.Select(word => word.Trim()).Aggregate(text,
                             (current, pattern) =>
                             Regex.Replace(current,
                                             pattern,
                                               string.Format("<span class='bold' style=\"background-color:{0}\">{1}</span>",
                                               cssClass,"$0"),
                                               RegexOptions.IgnoreCase));

            return words.Select(word => "\\b" + word.Trim().Replace(" ", "\\b|\\b") + "\\b")
                        .Aggregate(text, (current, pattern) =>
                                  Regex.Replace(current,
                                  pattern,
                                    string.Format("<span class='bold' style=\"background-color:{0}\">{1}</span>",
                                    cssClass,"$0"),
                                    RegexOptions.IgnoreCase));

        }

        public static string ReplaceEx(string original, string pattern, string replacement)
        {
            int position0, position1;
            var count = position0 = 0;
            var upperString = original.ToUpper();
            var upperPattern = pattern.ToUpper();

            int inc = (original.Length / pattern.Length) *
                      (replacement.Length - pattern.Length);

            var chars = new char[original.Length + Math.Max(0, inc)];

            while ((position1 = upperString.IndexOf(upperPattern, position0, StringComparison.Ordinal)) != -1)
            {
                for (var i = position0; i < position1; ++i)
                    chars[count++] = original[i];

                foreach (char t in replacement)
                    chars[count++] = t;

                position0 = position1 + pattern.Length;
            }

            if (position0 == 0) 
                return original;

            for (var i = position0; i < original.Length; ++i)
                chars[count++] = original[i];

            return new string(chars, 0, count);
        }
    }
}
