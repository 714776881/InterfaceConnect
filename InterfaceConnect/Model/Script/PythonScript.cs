using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.IO;

namespace InterfaceConnect
{
    // 支持Python脚本的运行，支持Python 2.7.5版本，不完全支持Python的所有模块，基本支持可见模块
    public class PythonScript
    {
        private static ScriptEngine _engine;
        private ScriptScope _scope;
        public PythonScript()
        {
            try
            {
                if(_engine == null)
                {
                    // 创建一个Python运行时环境
                    _engine = Python.CreateEngine();
                    // 设置当前路径为搜索路径
                    var paths = _engine.GetSearchPaths().ToList();
                    paths.Add(Directory.GetCurrentDirectory());
                    _engine.SetSearchPaths(paths);
                }
                if(_scope == null)
                {
                    // 创建一个Python作用域
                    _scope = _engine.CreateScope();
                }
            }
            catch (Exception)
            {
                Logger.LogError("Python 脚本解释器，初始化失败！");
                throw;
            }
        }
        public ScriptScope GetScope()
        {
            return _scope;
        }
        public dynamic Execute(string script_code, Dictionary<string,string> config, object message,string message_name = "message",string result_name = "result",string config_name = "config")
        {
            _scope.SetVariable(config_name, config);
            _scope.SetVariable(message_name, message);
            _engine.Execute(script_code, _scope);
            return _scope.GetVariable(result_name);
        }
        public dynamic Execute(string script_code, string message = "",string message_name = "message", string result_name = "result")
        {
            _scope.SetVariable(message_name, message);
            _engine.Execute(script_code, _scope);
            return _scope.GetVariable(result_name);
        }
        public void Dispose()
        {
            _engine = null;
            _scope = null;
        }
    }
}
