using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using Newtonsoft.Json.Linq;

namespace InterfaceConnect
{
    public class DictionaryTool
    {
        public static Dictionary<string,object> ParseJsonStr(string jsonStr)
        {
            if(string.IsNullOrEmpty(jsonStr))
            {
                return new Dictionary<string, object>();
            }
            JObject obj = JObject.Parse(jsonStr);
            return JObjectToDictionary(obj);
        }

        private static char SplitChar = '.';

        public static Dictionary<string, object> DataRowToDictionary(DataRow row)
        {
            var dictionary = new Dictionary<string, object>();

            foreach (DataColumn column in row.Table.Columns)
            {
                dictionary[column.ColumnName] = row[column].ToString();
            }

            return dictionary;
        }

        public static Dictionary<string, object> JObjectToDictionary(JObject jObject)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (var item in jObject)
            {
                string key = item.Key;
                JToken value = item.Value;
                if (value.Type == JTokenType.Object)
                {
                    Dictionary<string, object> subDictionary = JObjectToDictionary(value as JObject);
                    foreach (var subItem in subDictionary)
                    {
                        string subKey = key + SplitChar + subItem.Key;
                        dictionary[subKey] = subItem.Value;
                    }
                }
                else if (value.Type == JTokenType.Array)
                {
                    List<Dictionary<string, object>> subList = new List<Dictionary<string, object>>();
                    foreach (var subValue in value)
                    {
                        if (subValue.Type == JTokenType.Object)
                        {
                            subList.Add(JObjectToDictionary(subValue as JObject));
                        }
                        else
                        {
                            var dic = new Dictionary<string, object>();
                            dic.Add("value", subValue.ToString());
                            subList.Add(dic);
                        }
                    }
                    dictionary[key] = subList;
                }
                else
                {
                    dictionary[key] = value.ToString();
                }
            }
            return dictionary;
        }

        public static string  ConvertToJsonStr(Dictionary<string,object> dic)
        {
            if (dic == null) return string.Empty;
            return ConvertToJObject(dic).ToString();
        }

        public static JObject ConvertToJObject(Dictionary<string, object> dict)
        {
            JObject result = new JObject();

            foreach (KeyValuePair<string, object> kvp in dict)
            {
                string[] keys = kvp.Key.Split(SplitChar);

                JObject current = result;

                for (int i = 0; i < keys.Length - 1; i++)
                {
                    if (current[keys[i]] == null)
                    {
                        current[keys[i]] = new JObject();
                    }
                    if(current[keys[i]] is JValue)
                    {
                        current[keys[i]] = new JObject();
                    }
                    current = (JObject)current[keys[i]];
                }

                if (kvp.Value is string)
                {
                    current[keys[keys.Length - 1]] = new JValue(kvp.Value);
                }
                else if (kvp.Value is List<Dictionary<string, object>>)
                {
                    current[keys[keys.Length - 1]] = ConvertList((List<Dictionary<string, object>>)kvp.Value);
                }
            }

            return result;
        }

        private static JArray ConvertList(List<Dictionary<string, object>> list)
        {
            JArray result = new JArray();

            foreach (Dictionary<string, object> dict in list)
            {
                result.Add(ConvertToJObject(dict));
            }

            return result;
        }

        public static string GetValue(string key, Dictionary<string, object> dic)
        {
            if(string.IsNullOrEmpty(key) || dic == null || dic.Count == 0)
            {
                return string.Empty;
            }
            return GetValueInDictionary(key, dic, "");
        }
        private static string GetValueInDictionary(string key, Dictionary<string, object> dic, string fatherKey)
        {
            if (dic.ContainsKey(key))
            {
                object value = dic[key];
                if (value.GetType().Name == "String")
                {
                    return (string)value;
                }
            }
            foreach (string k in dic.Keys)
            {
                if (k != key) continue;
                object value = dic[k];
                if (value.GetType().Name == "List`1")
                {
                    return GetValueInDictionary(key, dic, string.IsNullOrEmpty(fatherKey) ? key : fatherKey + "." + key);
                }
            }
            return string.Empty;
        }

    }
}
