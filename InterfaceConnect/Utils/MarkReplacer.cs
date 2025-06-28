using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace InterfaceConnect
{
    public class MarkReplacer
    {
        private static readonly string MATCHRULE = "(?<=\\()[^\\)]+"; // 匹配两个小括号之间的内容
        public static string ReplaceParentheses(Dictionary<string, object> data,string message, StringBuilder str)
        {
            //优化匹配
            var texts = RegularExperssion.MatcheList(message, MATCHRULE);
            foreach (var text in texts)
            {
                var key = text.Trim();
                if (data.TryGetValue(key, out var value) && value != null)
                {
                    str.Replace("(" + key + ")", value.ToString().Trim());
                }
            }
            foreach (var item in data)
            {
                str.Replace("(" + item.Key + ")",item.Value.ToString().Trim());
            }
            return str.ToString();
        }
        public static List<KeyValue> ReplaceParentheses(Dictionary<string, object> data, List<KeyValue> keyValues)
        {
            var copy_keyValues = JsonTool.Clone<List<KeyValue>>(keyValues);

            foreach (var kv in copy_keyValues)
            {
                kv.Value = ReplaceParentheses(data, kv.Value, new StringBuilder(kv.Value));
            }
            return copy_keyValues;
        }
    }
}
