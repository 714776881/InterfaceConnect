using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;

namespace InterfaceConnect
{
    public class InterfaceInvoker
    {
        public static T Invoke<T>(string action, Object obj)
        {
            string message = JsonTool.SerializeObject(obj);
            string backMessage = Invoke(action, message);
            if (!string.IsNullOrEmpty(backMessage))
            {
                return JsonTool.DeserializeObject<T>(backMessage);
            }
            return default;
        }
        public static string Invoke(string action, string message)
        {
            if (string.IsNullOrEmpty(action))
            {
                throw new Exception("action is empty!");
            }

            InterfaceConfig api = ConfigManager.GetInterfaceConfigByHitRule(action, message.Replace("\\n","").Replace("\\N","").Replace("<","小于").Replace("=", "等于").Replace(">", "大于"));

            if (api == null)
            {
                throw new NullReferenceException("Can't find action," + action);
            }

            message = PreInvoke(api, message); 

            Logger.LogInfo("调用接口：" + action);
            return Send(api, message);
        }

        private static string PreInvoke(InterfaceConfig api, string message)
        {
            if(string.IsNullOrEmpty(api.PreAction))
            {
                return message;
            }

            InterfaceConfig preApi = ConfigManager.GetInterfaceConfigByHitRule(api.PreAction, message);
            if (preApi == null)
            {                                                                                                                               
                throw new ActionNullReferenceException();
            }

            if (!string.IsNullOrEmpty(preApi.PreAction))
            {
                message = PreInvoke(preApi, message);
            }

            Logger.LogInfo("调用前置接口：" + preApi.Action);
            return JsonTool.MergeJson(message, Send(preApi, message));
        }

        private static string Send(InterfaceConfig config, string message)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var connector = ConnectorManager.GetConnector(config);
                var backMessage = connector.Send(message);
                stopwatch.Stop();
                Logger.LogInfo("执行时间：" + stopwatch.ElapsedMilliseconds + " 毫秒");
                return backMessage;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        public readonly static ConnectorManager ConnectorManager = new ConnectorManager();

        public readonly static ConfigManager ConfigManager = ConfigManager.Instance;
    }
}