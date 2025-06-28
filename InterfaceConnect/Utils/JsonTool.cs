using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace InterfaceConnect
{
    public class JsonTool
    {
        public static T Clone<T>(object obj)
        {
            string str = SerializeObject(obj);
            return DeserializeObject<T>(str);
        }
        public static string ToFormatJsonString(string jsonStr)
        {
            try
            {
                JToken obj = JToken.Parse(jsonStr);
                return obj.ToString();
            }
            catch (Exception ex)
            {
                return jsonStr;
            }
        }

        // 将消息转换为JSON对象的文本字符串
        public static string Escape(string escapedString)
        {
            var data = new { message = escapedString };

            var str = JsonConvert.SerializeObject(data, Formatting.Indented);

            var start_index = str.IndexOf("\"", 16);
            var end_index = str.LastIndexOf("\"");

            return str.Substring(start_index + 1, end_index - start_index);
        }

        public static bool IsJson(string str)
        {
            try
            {
                JObject.Parse(str);
            }
            catch
            {
                return false;
            }
            return true;
        }


        public static string TryToString(object obj)
        {
            if (obj is string)
            {
                return obj.ToString();
            }
            else
            {
                return SerializeObject(obj);
            }
        }
        public static string SerializeObject(Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static T DeserializeObject<T>(string info)
        {
            return (T)JsonConvert.DeserializeObject(info, typeof(T));
        }

        public static string MergeJson(string json1, string json2)
        {
            if(string.IsNullOrEmpty(json1))
            {
                return json2;
            }
            if(string.IsNullOrEmpty(json2))
            {
                return json1;
            }

            JObject obj1 = JObject.Parse(json1);
            JObject obj2 = JObject.Parse(json2);
            // 合并两个JObject
            obj1.Merge(obj2, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union // 处理数组合并
            });
            // 得到融合后的JSON消息
            return obj1.ToString();
        }
        public static string GetListFirstNode(string jsonStr, string path)
        {
            JObject obj = JObject.Parse(jsonStr);
            var tokens = obj.SelectTokens(path);
            if (tokens == null || tokens.Count() == 0)
            {
                return "";
            }
            if (tokens.ElementAt(0).Count() == 0)
            {
                return "";
            }
            var node = tokens.ElementAt(0)[0].ToString();
            if(!node.StartsWith("{") || !node.StartsWith("["))
            {
                return string.Empty;
            }
            return tokens.ElementAt(0)[0].ToString();
        }
        public static bool FindNode(string jsonStr, string path)
        {
            JObject obj = JObject.Parse(jsonStr);
            var tokens = obj.SelectTokens(path);
            if (tokens == null || tokens.Count() == 0)
            {
                return false;
            }
            return true;
        }

        public static string AddListNode(string jsonStr, string path, string value)
        {
            JObject obj = JObject.Parse(jsonStr);
            var tokens = obj.SelectTokens(path);
            if (tokens == null || tokens.Count() == 0)
            {
                Console.WriteLine("当前节点" + path + ",未配置对应的模板！");
                return jsonStr;
            }
            // 这里要兼容["市中心1","市中心2"]
            JArray newarray = JArray.Parse(value);
            foreach (var tk in tokens)
            {
                JArray array = (JArray)tk;
                array.RemoveAll();
                foreach (JToken k in newarray)
                {
                    array.Add(k);
                }
            }
            return obj.ToString();
        }
        public static string GetNode(string jsonStr, string path)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(jsonStr))
            {
                return string.Empty;
            }
            JToken token = JToken.Parse(jsonStr);
            var nodes = token.SelectTokens(path);
            foreach (var node in nodes)
            {
                return node.ToString();
            }
            return string.Empty;
        }
        public static List<string> GetChildrenNodes(string jsonStr, string path)
        {
            var result = new List<string>();
            JToken token = JToken.Parse(jsonStr);
            var nodes = token.SelectTokens(path);
            foreach (var node in nodes)
            {
                if (node is JObject) continue;

                JArray array = (JArray)node;
                foreach (JToken jk in array)
                {
                    result.Add(jk.ToString());
                }
            }
            return result;
        }
        public static string SetNode(string jsonStr, string path, string value)
        {
            if(string.IsNullOrEmpty(jsonStr))
            {
                return string.Empty;
            }
            if(string.IsNullOrEmpty(path))
            {
                return jsonStr;
            }
            JToken obj = JToken.Parse(jsonStr);
            var tokens = obj.SelectTokens(path);
            if (tokens == null || tokens.Count() == 0)
            {
                return jsonStr;
            }
            foreach(var tk in tokens)
            {
                var jv = (JValue)tk;
                var xx = jv.Value;
                // 根据目标数据节点，设置数据类型
                if(xx is long || xx is int || xx is double)
                {
                    jv.Value = Convert.ToInt32(value);
                }
                else if(xx is double)
                {
                    jv.Value = Convert.ToDouble(value);
                }
                else if(xx is bool)
                {
                    if(value == "1" || value.ToUpper() == "TRUE")
                    {
                        jv.Value = true;
                    }
                    if(value == "0" || value.ToUpper() == "FALSE")
                    {
                        jv.Value = false;
                    }
                }
                else if(xx is null)
                {
                    if(value.ToUpper() == "NULL")
                    {
                        jv.Value = null;
                    }
                }
                else
                {
                    tk.Replace(JToken.FromObject(value));
                }
            }
            return obj.ToString();
        }
    }
}
