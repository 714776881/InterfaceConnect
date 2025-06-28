using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceConnect
{
    public class ScriptConnector : BaseInterfaceConnect
    {
       private ScriptConfig _config;
       public ScriptConnector(ScriptConfig config):base(config)
        {
            _config = config;
        }
        public override string Send(string message, PythonScript script = null)
        {
            if (script == null)
            {
                script = new PythonScript();
            }
            // 前置操作
            message = Operation.ExeOperation(Operation.FindOperation("before_operation", _config), message, script);
            string result = string.Empty;
            try
            {
                Logger.LogInfo("输入参数：\n" + message);
                var configs = _config.Configs.ToDictionary(kv => kv.Key, kv => (string)kv.Value);
                configs.Add("template", _config.Template);
                configs.Add("outertemplate", _config.OuterTemplate);
                // 转换JOSN、XML数据
                object data = message;
                if (_config.MessageType == InfoType.JSON)
                {
                    data = JsonTool.DeserializeObject<Dictionary<string, object>>(message);
                }
                result = JsonTool.TryToString(script.Execute(_config.Script, configs, data));
                Logger.LogInfo("输出参数：\n" + result);
                // 后置操作
                message = Operation.ExeOperation(Operation.FindOperation("after_operation", _config), message, script);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public override string BuildRequest(string message)
        {
            return message;
        }
        public override string SendRequest(string message)
        {
            return message;
        }
        public override string HandleResponse(string message)
        {
            return message;
        }

    }
}