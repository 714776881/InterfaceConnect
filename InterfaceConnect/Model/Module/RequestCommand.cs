using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommMediator
{
    public class RequestCommand: IInterfaceConnector
    {
        private ParseConfig _parseConfig;

        public RequestCommand(ParseConfig parseConfig)
        {
            _parseConfig = parseConfig;
        }
        public string Send(string message)
        {
            var dic = DictionaryHelper.ParseJsonStr(message);

            SQLExpression evaluator = new SQLExpression(_parseConfig.InParseConfigs);
            dic = evaluator.Interpret(dic);

            DicParser inDicParser = new DicParser(_parseConfig.InParseConfigs, _parseConfig.Template);
            message = inDicParser.Parse(dic,_parseConfig.InfoType);

            message =  MarkReplacer.ReplaceParentheses(dic, message);
            return message;
        }
    }
}
