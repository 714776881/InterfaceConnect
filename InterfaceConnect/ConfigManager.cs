 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InterfaceConnect
{
    public class ConfigManager
    {
        private static readonly Lazy<ConfigManager> lazy = new Lazy<ConfigManager>(() => new ConfigManager());
        public static ConfigManager Instance => lazy.Value;

        // 存放配置信息
        private Dictionary<string,InterfaceConfig> apis = new Dictionary<string, InterfaceConfig>();

        private ConfigManager()
        {
            LoadApis();
        }

        private static string configFolder = AppSettingsTool.ReadSetting("InterfaceConfigFilesPath", "InterfaceConfig");
        // 加载配置文件，创建 Api 对象，并把它们存储到字典中
        private void LoadApis()
        {
            
            string[] jsonFiles = Directory.GetFiles(configFolder, "*.json");
            foreach (string jsonFile in jsonFiles)
            {
                try
                {
                    string jsonContent = File.ReadAllText(jsonFile);
                    InterfaceConfig api = ConfigFactory.CreateApi(jsonContent);
                    if (api != null)
                    {
                        apis[api.Action] = api;
                    }
                    else
                    {
                        Console.WriteLine($"解析文件失败！{jsonContent}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"读取文件失败！{jsonFile}， {ex}");
                }
            }
        }
        // 获取所有的 Api
        public Dictionary<string, InterfaceConfig> GetAllApis()
        {
            return apis;
        }
        // 根据 Action 获取 Api
        public InterfaceConfig GetInterfaceConfig(string action,string message = "")
        {
            return apis.ContainsKey(action) ? apis[action] : null;
        }
        public InterfaceConfig GetInterfaceConfigByHitRule(string action,string message)
        {
            var config = apis.ContainsKey(action) ? apis[action] : null;
            if (config != null && !string.IsNullOrWhiteSpace(message))
            {
                config = IsTagetConfig(config, message);
            }
            return config;
        }
        private InterfaceConfig IsTagetConfig(InterfaceConfig config,string message)
        {
            var configs = GetApisByName(config.Name);
            foreach(var item in configs)
            {
                var jsonPath = item.HitRule;

                if (string.IsNullOrWhiteSpace(jsonPath)) continue;

                if(JsonTool.FindNode(message, jsonPath))
                {
                    return item;
                }
            }
            return config;
        }
        private List<InterfaceConfig> GetApisByName(string name)
        {
            return apis.Values.Where(x => { return x.Name == name; }).ToList();
        }
        // 保存 Api 到配置文件
        public bool SaveApiAsync(InterfaceConfig api)
        {
            // 修改内存
            apis[api.Action] = api;
            // 修改文件
            var filePath = Path.Combine(configFolder, $"{api.Action}.json");
            var fileContents = JsonTool.SerializeObject(api);
            try
            {
                var sw = new StreamWriter(filePath);
                sw.Write(fileContents);
                sw.Flush();
                sw.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"保存文件失败！{filePath}, {ex}");
                return false;
            }
        }
        // 删除指定 Action 的 Api
        public bool RemoveApiAsync(string action)
        {
            if (apis.ContainsKey(action))
            {
                var filePath = Path.Combine(configFolder, $"{action}.json");
                try
                {
                    File.Delete(filePath);
                    apis.Remove(action);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"删除文件失败！{filePath}, {ex}");
                    return false;
                }
            }
            return false;
        }

    }
}
