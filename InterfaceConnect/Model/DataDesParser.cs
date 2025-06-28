using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace InterfaceConnect
{
    public class DataDesParser
    {
        private List<DataSingleRule> _rules;
        private InfoType _info_type;
        public DataDesParser(List<DataSingleRule> rules, InfoType infoType)
        {
            _rules = rules;
            _info_type = infoType;
        }
        public Dictionary<string, Object> Parse(string message)
        {
            Dictionary<string, object> dic = null;
            try
            {
                switch (_info_type)
                {
                    case InfoType.JSON:
                        dic = ParseJsonStr(_rules, message, "");
                        break;
                    case InfoType.XML:
                        var ns = string.Empty;
                        message = XmlTool.RemoveNameSapce(message, ref ns);
                        dic = ParseXmlStr(_rules, message, "");
                        message = XmlTool.AddNameSpace(message, ns);
                        break;
                    case InfoType.TEXT:
                        dic = ParseStr(_rules, message, "");
                        break;
                    default:
                        dic = null;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dic;
        }

        private Dictionary<string, object> ParseStr(List<DataSingleRule> rules, string message, string fatherKey)
        {
            var data = new Dictionary<string, object>();
            var list_rules = rules.Where(x => x.KeyType == KeyType.ARRAY).ToList();
            foreach (var rule in rules)
            {
                if (string.IsNullOrEmpty(rule.Key))
                {
                    continue;
                }

                string key = string.IsNullOrEmpty(fatherKey) ? rule.Key : rule.Key.Replace(fatherKey + ".", "");
                if (rule.KeyType == KeyType.OBJECT)
                {
                    var node_path = rule.Position;
                    var node_value = RegularExperssion.MatchStr(message, node_path);
                    data[key] = node_value;
                }
                if(rule.KeyType == KeyType.ARRAY)
                {
                    var son_data = new List<Dictionary<string, object>>();
                    var son_rule_s = rules.Where(x => x.Key.StartsWith(rule.Key + ".") && rule.Key != x.Key).ToList();
                    var son_message_s = RegularExperssion.MatcheList(message, rule.Position);
                    foreach (var son_message in son_message_s)
                    {
                        son_data.Add(ParseStr(son_rule_s, son_message, rule.Key));
                    }
                    data[key] = son_data;
                }
            }
            return data;
        }
        // 这里有空需要抽象出一个 INodeHelper , 来分别处理 JSON、XML、TEXT
        private Dictionary<string, object> ParseJsonStr(List<DataSingleRule> rules, string message, string fatherKey)
        {
            var dic = new Dictionary<string, object>();
            var setRules = rules.Where(x => x.KeyType == KeyType.ARRAY).ToList();
            foreach (var rule in rules)
            {
                if (string.IsNullOrEmpty(rule.Key))
                {
                    continue;
                }
                var tempRule = setRules.Find(x => rule.Key.StartsWith(x.Key + ".") && rule.Key != x.Key);
                if (tempRule != null)
                {
                    continue;
                }
                string key = string.IsNullOrEmpty(fatherKey) ? rule.Key : rule.Key.Replace(fatherKey + ".", "");
                if (rule.KeyType == KeyType.OBJECT)
                {
                    var position = rule.Position;
                    var value = JsonTool.GetNode(message, position);
                    dic[key] = value;
                }
                if (rule.KeyType == KeyType.ARRAY)
                {
                    var sonDic = new List<Dictionary<string, object>>();
                    var sonRules = rules.Where(x => x.Key.StartsWith(rule.Key + ".") && rule.Key != x.Key).ToList();
                    var sonMessages = JsonTool.GetChildrenNodes(message, rule.Position);
                    foreach (var sonMessage in sonMessages)
                    {
                        sonDic.Add(ParseJsonStr(sonRules, sonMessage, rule.Key));
                    }
                    dic[key] = sonDic;
                }
            }
            return dic;
        }
        private Dictionary<string, object> ParseXmlStr(List<DataSingleRule> rules, string message, string fatherKey)
        {
            var dic = new Dictionary<string, object>();
            var setRules = rules.Where(x => x.KeyType == KeyType.ARRAY).ToList();
            foreach (var rule in rules)
            {
                if (string.IsNullOrEmpty(rule.Key))
                {
                    continue;
                }
                var tempRule = setRules.Find(x => rule.Key.StartsWith(x.Key) && rule.Key != x.Key);
                if (tempRule != null)
                {
                    continue;
                }
                string key = string.IsNullOrEmpty(fatherKey) ? rule.Key : rule.Key.Replace(fatherKey + ".", "");
                if (rule.KeyType == KeyType.OBJECT)
                {
                    var position = rule.Position;
                    var value = XmlTool.GetNode(message, position);
                    dic[key] = value;
                }
                if (rule.KeyType == KeyType.ARRAY)
                {
                    var sonDic = new List<Dictionary<string, object>>();
                    var sonRules = rules.Where(x => x.Key.StartsWith(rule.Key) && rule.Key != x.Key).ToList();
                    var sonMessages = XmlTool.GetChildrenNodes(message, rule.Position);
                    foreach (var sonMessage in sonMessages)
                    {
                        sonDic.Add(ParseXmlStr(sonRules, sonMessage, rule.Key));
                    }
                    dic[key] = sonDic;
                }
            }
            return dic;
        }
    }
}