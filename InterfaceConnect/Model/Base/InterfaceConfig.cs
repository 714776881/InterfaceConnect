using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceConnect
{
    public class InterfaceConfig
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string PreAction { get; set; }
        public ConfigType Type { get; set; }
        public string HitRule { get; set; }
        public string MockData { get; set; }

        public List<KeyValue> Configs; // 配置扩展
    }
    public enum ConfigType
    {
        HTTP,
        DB,
        DATA,
        SCRIPT,
    }
}
