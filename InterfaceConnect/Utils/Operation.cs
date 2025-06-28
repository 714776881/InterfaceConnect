using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceConnect
{
    public class Operation
    {
        public static string FindOperation(string operation, InterfaceConfig api)
        {
            if (api.Configs == null) return string.Empty;
            var configs = api.Configs;
            foreach (var config in configs)
            {
                if (config.Key == operation)
                {
                    return config.Value.Trim();
                }
            }
            return string.Empty;
        }
        public static string ExeOperation(string operation, string message, PythonScript script)
        {
            if (string.IsNullOrEmpty(operation)) return message;

            var config = InterfaceInvoker.ConfigManager.GetInterfaceConfig(operation, message);
            if (config != null)
            {
                // 一个接口内的所有操作共享同一个脚本运行环境，接口之间不共享脚本运行环境
                var connector = InterfaceInvoker.ConnectorManager.GetConnector(config);

                Logger.LogInfo("执行操作：\n" + operation);
                if (connector is BaseInterfaceConnect)
                {
                    var scriptConnector = (BaseInterfaceConnect)connector;
                    message = scriptConnector.Send(message, script);
                }
                else
                {
                    message = connector.Send(message);
                }
                Logger.LogInfo("执行结果：\n" + message);
            }
            else
            {
                Logger.LogError("未发现该操作(" + operation + ")，请检查配置项!");
            }
            return message;
        }
    }
}
