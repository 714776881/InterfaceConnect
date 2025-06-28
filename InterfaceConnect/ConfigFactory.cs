using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceConnect
{
    public class ConfigFactory
    {
        public static InterfaceConfig CreateApi(ConfigType type)
        {
            InterfaceConfig api;
            switch (type)
            {
                case ConfigType.HTTP:
                    api = new HttpInterfaceConfig();
                    break;
                case ConfigType.DB:
                    api = new DBInterfaceConfig();
                    break;
                case ConfigType.DATA:
                    api = new DBInterfaceConfig();
                    break;
                case ConfigType.SCRIPT:
                    api = new ScriptConfig();
                    break;
                default:
                    throw new ArgumentException($"Unsupported API type: {type}");
            }
            api.Type = type;
            return api;
        }

        public static InterfaceConfig CreateApi(string jsonContent)
        {
            var api = JsonTool.DeserializeObject<InterfaceConfig>(jsonContent);
            if (api == null)
            {
                throw new ArgumentException($"Failed to deserialize JSON to API object.{jsonContent}");
            }
            return CreateApi(api.Type, jsonContent);
        }

        public static InterfaceConfig Clone(InterfaceConfig config)
        {
            string jsonContent = JsonTool.SerializeObject(config);

            return CreateApi(jsonContent);
        }

        private static InterfaceConfig CreateApi(ConfigType type, string jsonContent)
        {
            switch (type)
            {
                case ConfigType.HTTP:
                    return JsonTool.DeserializeObject<HttpInterfaceConfig>(jsonContent);
                case ConfigType.DB:
                    return JsonTool.DeserializeObject<DBInterfaceConfig>(jsonContent);
                case ConfigType.DATA:
                    return JsonTool.DeserializeObject<DBInterfaceConfig>(jsonContent);
                case ConfigType.SCRIPT:
                    return JsonTool.DeserializeObject<ScriptConfig>(jsonContent);
                default:
                    throw new ArgumentException($"Unsupported API type: {type}");
            }
        }
    }
}
