using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfaceConnect
{
    public abstract class BaseInterfaceConnect : IInterfaceConnect
    {
        private InterfaceConfig _config;
        public BaseInterfaceConnect(InterfaceConfig config)
        {
            if(config == null)
            {
                throw new NullReferenceException($"ApiCommandBase init false!");
            }
            _config = config;
        }

        public virtual string Send(string message)
        {
            return Send(message,null);
        }
        public virtual string Send(string message,PythonScript script = null)
        {
            if(script == null)
            {
                script = new PythonScript();
            }
            try
            {
                // 前置操作
                message = FindAndExeOperation("before_operation", message, script);

                Logger.LogInfo("输入参数：\n" + message);
                message = BuildRequest(message);

                // 发送前操作
                message = FindAndExeOperation("before_send_operation",message, script);

                Logger.LogInfo("请求消息：\n" + message);
                message = SendRequest(message);

                // 发送后操作
                message = FindAndExeOperation("after_send_operation", message, script);

                Logger.LogInfo("响应消息：\n" + message);
                message = HandleResponse(message);

                // 后置操作
                message = FindAndExeOperation("after_operation", message, script);
                Logger.LogInfo("输出参数: \n" + message);
            }
            catch (Exception)
            {
                var action = Operation.FindOperation("after_throwing_operation", _config);
                if (!string.IsNullOrEmpty(action))
                {
                    // 异常操作
                    message = Operation.ExeOperation(action, message, script);
                }
                else
                {
                    throw;
                }
            }
            return message;
        }

        public string FindAndExeOperation(string action,string message, PythonScript script)
        {
            message = Operation.ExeOperation(Operation.FindOperation(action, _config), message, script);
            return message;
        }

        public abstract string BuildRequest(string message);

        public abstract string SendRequest(string message);

        public abstract string HandleResponse(string backInfo);
    }
}
