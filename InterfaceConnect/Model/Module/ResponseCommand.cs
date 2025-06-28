using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommMediator
{
    public class ResponseCommand: IInterfaceConnector
    {
        private ParseConfig _parseConfig;

        public ResponseCommand(ParseConfig parseConfig)
        {
            _parseConfig = parseConfig;
        }
        public string Send(string message)
        {
            var desDicParser = new DicDesParser(_parseConfig.OutParseConfigs);
            var dic = desDicParser.Parse(message,_parseConfig.InfoType);

            SQLExpression evaluator = new SQLExpression(_parseConfig.OutParseConfigs);
            dic = evaluator.Interpret(dic);

            return DictionaryHelper.ConvertToJsonStr(dic);
        }
    }
}
