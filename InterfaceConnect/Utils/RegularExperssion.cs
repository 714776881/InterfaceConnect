using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace InterfaceConnect
{
    public class RegularExperssion
    {
        public static string MatchStr(string message, string pattern)
        {
            string matchedText = "";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(message);
            if (match.Success)
            {
                if(match.Groups.Count > 0)
                {
                    matchedText = match.Groups[match.Groups.Count - 1].Value;
                }
                else
                {
                    matchedText = match.Value;
                }
            }
            return matchedText;
        }
        public static List<string> MatcheList(string message, string pattern)
        {
            List<string> matches = new List<string>();
            Regex regex = new Regex(pattern);
            MatchCollection matchCollection = regex.Matches(message);

            foreach (Match match in matchCollection)
            {
                matches.Add(match.Value);
            }
            return matches;
        }
        public static string Replace(string input, string pattern, string replacement)
        {
            if (string.IsNullOrEmpty(pattern)) return input;

            return new Regex(pattern).Replace(input, replacement);
        }
    }
}
