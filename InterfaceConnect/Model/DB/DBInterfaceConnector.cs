using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Dapper;

namespace InterfaceConnect
{
    public class DBInterfaceConnector: BaseInterfaceConnect
    {
        private DBInterfaceConfig _config;

        private DatabaseHelper _databaseHelper;
        public DBInterfaceConnector(DBInterfaceConfig config) : base(config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("ExtraCommand init error,httpApi is null!");
            }
            try
            {
                _config = config;
                Init(_config);
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException($"{config.Action}：Api to ExtraCommand conversion error.{ex}");
            }
            _databaseHelper = new DatabaseHelper(_config.DBtype, _config.Connection);
        }

        DataExpression evaluator;
        DataExpression des_evaluator;

        private void Init(DBInterfaceConfig config)
        {
            evaluator = new DataExpression(_config.Rules);
            des_evaluator = new DataExpression(_config.DesRules);
        }

        DynamicParameters _parameter;
        public override string BuildRequest(string message)
        {
            var backMessage = string.Empty;

            var dic = DictionaryTool.ParseJsonStr(message);

            dic = evaluator.Interpret(dic);
            
            backMessage = MarkReplacer.ReplaceParentheses(dic, _config.Template, new StringBuilder(_config.Template));

            var parameter_run_info = string.Empty;
            if (_config.SQLType == SQLType.STOREPROCEDUCE)
            {
                var storeProceduce = _config.Template;
                var parameters = _config.Parameters;

                _parameter = new DynamicParameters();
                foreach (var item in parameters)
                {
                    // 将规则计算的结果通过关联路径注入到对应的值
                    var matchingConfig = _config.Rules.FirstOrDefault(config => item.Name == config.Position);
                    if (matchingConfig != null)
                    {
                        var value = dic[matchingConfig.Key].ToString();
                        item.Value = DBInfoTypeConvert.ConvertStringToDbType(value, item.Type);
                    }

                    if (item.Direction == System.Data.ParameterDirection.Input)
                    {
                        _parameter.Add(item.Name, item.Value, item.Type, item.Direction);
                    }
                    else
                    {
                        _parameter.Add(item.Name, item.Value, item.Type, item.Direction, size: 1000);
                    }
                    parameter_run_info += item.Name + ":" + Convert.ToString(item.Value) + ",\n";
                }
            }
            return backMessage;
        }

        public override string SendRequest(string message)
        {
            var backMessage = string.Empty;
            // 数据库执行SQL，并将结果序列化成JSON消息
            switch (_config.SQLType)
            {
                case SQLType.QUERYFIRST:
                    backMessage = JsonConvert.SerializeObject(_databaseHelper.QueryFirst(message));
                    break;
                case SQLType.QUERY:
                    backMessage = JsonConvert.SerializeObject(_databaseHelper.Query(message));
                    break;
                case SQLType.NOQUERY:
                    backMessage = ExeResultToString(_databaseHelper.Execute(message));
                    break;
                case SQLType.STOREPROCEDUCE:
                    if (_parameter == null) throw new Exception("请执行上一个步骤后，再次尝试！");
                    var result = _databaseHelper.ExecuteStoredProcedure(message, _parameter);
                    backMessage = DynamicParametersToJsonString(result);
                    break;
            }
            // 将json信息进行格式化处理
            backMessage = JsonTool.ToFormatJsonString(backMessage);
            return backMessage;
        }
        public override string HandleResponse(string message)
        {
            var desDicParser = new DataDesParser(_config.DesRules,_config.ParseInfoType);
            var dic = desDicParser.Parse(message);

            dic = des_evaluator.Interpret(dic);
            return DictionaryTool.ConvertToJsonStr(dic);
        }
        private static string DynamicParametersToJsonString(DynamicParameters parameters)
        {
            var jsonStr = "{";
            var items = parameters.ParameterNames;
            foreach(var item in items)
            {
                var value = Convert.ToString(parameters.Get<object>(item));
                jsonStr += $"\"{item}\":\"{value}\",";
            }
            jsonStr += "}";
            return jsonStr;
        }
        private static string ExeResultToString(int i)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if(i > -1)
            {
                dic["code"] = "1";
                dic["message"] = "执行成功！";
            }
            else
            {
                dic["code"] = i.ToString();
                dic["message"] = "非查询语句，执行错误！";
            }
            return JsonTool.SerializeObject(dic);
        }
    }
}
