using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using DevExpress.Data.Browsing.Design;
using System.Windows.Forms;
using System.Web.UI;

namespace InterfaceConnect
{
    public class DataParser
    {

        private List<DataSingleRule> _rules;

        private string _template;

        public DataParser(List<DataSingleRule> rules, string template)
        {  
            _rules = rules;
            _template = template;
        }

        public string Parse(Dictionary<string, Object> dic,InfoType infoType)
        {
            if (string.IsNullOrEmpty(_template))
            {
                return string.Empty;
            }
            if (dic == null)
            {
                return _template;
            }
            if (_rules == null )
            {
                return _template;
            }

            string message = _template;
            try
            {
                switch (infoType)
                {
                    case InfoType.JSON:
                        message = GetJsonString(dic, _rules, message, "");
                        break;
                    case InfoType.XML:
                        string ns = string.Empty;
                        message = XmlTool.RemoveNameSapce(message,ref ns);
                        message = GetXmlString(dic, _rules, message, "");
                        message = XmlTool.AddNameSpace(message, ns);
                        message = XmlTool.ToFormatXmlString(message);
                        break;
                    case InfoType.TEXT:
                        message = GetString(dic, _rules, message);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return message;
        }

        private InfoType _info_type;
        private string GetMessage(Dictionary<string,object> data,List<DataSingleRule> rules,string message,string key_path = "")
        {
            foreach (string key in data.Keys)
            {
                string key_full_path = string.IsNullOrEmpty(key_path) ? key : key_path + "." + key;

                List<DataSingleRule> keyRules = GetRuleByKey(key_full_path, rules);
                if (keyRules.Count == 0)
                {
                    continue;
                }

                Object value = data[key];
                if (!(value is IList))
                {
                    var str = (string)value;
                    foreach (DataSingleRule rule in keyRules)
                    {
                        // 单项处理
                        message = RegularExperssion.Replace(message, rule.Position ,str);
                    }
                }
                else
                {
                    var list_dic = (List<Dictionary<string, object>>)value;
                    foreach (DataSingleRule rule in keyRules)
                    {
                        // 集合处理
                        string node_template = RegularExperssion.MatchStr(message, rule.Position);

                        List<string> son_list_str = new List<string>();
                        foreach (Dictionary<string, object> son_data in list_dic)
                        {
                            // 单项处理
                            List<DataSingleRule> son_rules = GetSonRuleByKey(key_full_path, rules);
                            son_list_str.Add(GetMessage((Dictionary<string, object>)son_data, son_rules, node_template, key_full_path));
                        }

                        message = RegularExperssion.Replace(message, rule.Position, string.Join("", son_list_str));
                    }
                }
            }
            return message;
        }
        private static string HandleSingleRule(string message,DataSingleRule rule,string value,InfoType info_type)
        {
            if(info_type == InfoType.TEXT)
            {
                message = RegularExperssion.Replace(message, rule.Position, value);
            }
            else if(info_type == InfoType.JSON)
            {
                var nodePath = rule.Position;
                var nodeValue = (string)value;
                //if (!string.IsNullOrWhiteSpace(fatherKey) && nodePath == fatherPosition)
                //{
                //    // ["1","2","3"]
                //    message = $"\"{nodeValue}\"";
                //}
                //else
                //{
                //    // {"x":"1"}, 12 ..x -> {"x","12"}
                //    message = JsonTool.SetNode(message, nodePath, nodeValue);
                //}
            }
            else if(info_type == InfoType.XML)
            {
                message = XmlTool.SetNode(message, rule.Position, value);
            }
            return message;
        }
        private string GetString(Dictionary<String,object> data,List<DataSingleRule> rules,string message,string father_key = "",string key_path = "")
        {
            foreach (string key in data.Keys)
            {
                string key_full_path = string.IsNullOrEmpty(father_key) ? key : father_key + "." + key;
                List<DataSingleRule> keyRules = GetRuleByKey(key_full_path, rules);
                if (keyRules.Count == 0)
                {
                    continue;
                }

                Object value = data[key];
                if (!(value is IList))
                {
                    foreach (DataSingleRule rule in keyRules)
                    {
                        var node_path = rule.Position;
                        var node_value = (string)value;
                        message = RegularExperssion.Replace(message, node_path, node_value);
                    }
                }
                else
                {
                    var list = (List<Dictionary<string, object>>)value;
                    foreach (DataSingleRule rule in keyRules)
                    {
                        string node_path = rule.Position;
                        string node_template = RegularExperssion.MatchStr(message, node_path);

                        List<string> list_str = new List<string>();
                        foreach (Dictionary<string, object> son_data in list)
                        {
                            List<DataSingleRule> son_rules = GetSonRuleByKey(key_full_path, rules);
                            list_str.Add(GetString((Dictionary<string, object>)son_data, son_rules, node_template, key_full_path, node_path));
                        }
                        message = RegularExperssion.Replace(message, node_path, string.Join("", list_str));
                    }
                }
            }
            return message;
        }
        private string GetJsonString(Dictionary<string, object> data, List<DataSingleRule> rules,string template,string fatherKey,string fatherPosition = "")
        {
            foreach (string key in data.Keys)
            {
                string keyPath = string.IsNullOrEmpty(fatherKey) ? key : fatherKey + "." + key;
                List<DataSingleRule> keyRules = GetRuleByKey(keyPath, rules);
                if (keyRules.Count == 0)
                {
                    continue;
                }
                Object value = data[key];
                if (!(value is IList))
                {
                    foreach (DataSingleRule rule in keyRules)
                    {
                        var nodePath = rule.Position;
                        var nodeValue = (string)value;
                        if(!string.IsNullOrWhiteSpace(fatherKey) && nodePath == fatherPosition)
                        {
                            // ["1","2","3"]
                            template = $"\"{nodeValue}\"";
                        }
                        else
                        {
                            // {"x":"1"}, 12 ..x -> {"x","12"}
                            template = JsonTool.SetNode(template, nodePath, nodeValue);
                        }
                    }
                }
                if (value is IList)
                {
                    var list = (List<Dictionary<string, object>>)value;
                    foreach (DataSingleRule rule in keyRules)
                    {
                        // [{"x":[{"y":"1"},{"z":"2"}]}] -> [{"y":"1"},{"z":"2"}]
                        if (rule.Position == fatherPosition)
                        {
                            string nodePath = rule.Position;
                            var tempStr = new List<string>();
                            foreach (var childrenDic in list)
                            {
                                List<DataSingleRule> childrenConfigs = GetSonRuleByKey(keyPath, rules);
                                tempStr.Add(GetJsonString((Dictionary<string, object>)childrenDic, childrenConfigs, template, keyPath, nodePath));
                            }
                            template = string.Join(",", tempStr);
                        }
                        // [{"x":1,"y",1},{"x":2,"y",2}]
                        else
                        {
                            string nodePath = rule.Position;
                            string nodeTemplate = JsonTool.GetListFirstNode(template, nodePath);
                            List<string> tempStr = new List<string>();
                            foreach (Dictionary<string, object> sonData in list)
                            {
                                List<DataSingleRule> childrenConfigs = GetSonRuleByKey(keyPath, rules);
                                tempStr.Add(GetJsonString((Dictionary<string, object>)sonData, childrenConfigs, nodeTemplate, keyPath, nodePath));
                            }
                            template = JsonTool.AddListNode(template, nodePath, "[" + string.Join(",", tempStr) + "]");
                        }
                    }
                }
            }
            // {"name":"(value)"},value = "12" -> {"name":"12"}
            template = MarkReplacer.ReplaceParentheses(data,template, new StringBuilder(template));
            return template;
        }
        private string GetXmlString(Dictionary<string, object> data, List<DataSingleRule> rules, string template, string fatherKey, string fatherPosition = "")
        {
            foreach (string key in data.Keys)
            {
                string keyPath = string.IsNullOrEmpty(fatherKey) ? key : fatherKey + "." + key;
                List<DataSingleRule> keyRules = GetRuleByKey(keyPath, rules);
                if (keyRules.Count == 0)
                {
                    continue;
                }
                Object value = data[key];
                if (!(value is IList))
                {
                    foreach (DataSingleRule rule in keyRules)
                    {
                        string nodePath = rule.Position;
                        string nodeValue = (string)value;
                        template = XmlTool.SetNode(template, nodePath, nodeValue);
                    }
                }
                if (value is IList)
                {
                    foreach (DataSingleRule rule in keyRules)
                    {
                        // 若子路径和父路径一致，则将父向下合并到子
                        if (rule.Position == fatherPosition)
                        {
                            string nodePath = rule.Position;
                            var list = (List<Dictionary<string, object>>)value;
                            var tempStr = new List<string>();
                            foreach (var childrenDic in list)
                            {
                                List<DataSingleRule> childrenConfigs = GetSonRuleByKey(keyPath, rules);
                                tempStr.Add(GetXmlString((Dictionary<string, object>)childrenDic, childrenConfigs, template, keyPath, nodePath));
                            }
                            template = string.Join("", tempStr);
                        }
                        else
                        {
                            string nodePath = rule.Position;
                            string listTemplate = XmlTool.GetListFirstNode(template, nodePath);
                            List<Dictionary<string, object>> list = (List<Dictionary<string, object>>)value;
                            List<string> tempStr = new List<string>();
                            foreach (Dictionary<string, object> childrenDic in list)
                            {
                                List<DataSingleRule> childrenConfigs = GetSonRuleByKey(keyPath, rules);
                                tempStr.Add(GetXmlString((Dictionary<string, object>)childrenDic, childrenConfigs, listTemplate, keyPath));
                            }
                            template = XmlTool.AddListNode(template, nodePath, string.Join("", tempStr));
                        }
                    }
                }
            }
            // 替换
            template = MarkReplacer.ReplaceParentheses(data,template, new StringBuilder(template));
            return template;
        }

        private List<DataSingleRule> GetSonRuleByKey(string key, List<DataSingleRule> rules)
        {
            List<DataSingleRule> temp_rules = new List<DataSingleRule>();
            foreach (DataSingleRule config in _rules)
            {
                if(string.IsNullOrEmpty(config.Key) ||string.IsNullOrEmpty(key))
                {
                    continue;
                }
                if (config.Key.StartsWith(key) && config.Key != key)
                {
                    temp_rules.Add(config);
                }
            }
            return temp_rules;
        }
        private List<DataSingleRule> GetRuleByKey(string key, List<DataSingleRule> rules)
        {
            List<DataSingleRule> temp_rules = new List<DataSingleRule>(); 
            foreach (DataSingleRule config in _rules)
            {
                if (config.Key == key)
                {
                    temp_rules.Add(config);
                }
            }
            return temp_rules;
        }
    }
}
