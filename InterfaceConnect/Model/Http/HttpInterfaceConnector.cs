using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace InterfaceConnect
{
    public class HttpInterfaceConnector : BaseInterfaceConnect
    {
        private readonly HttpInterfaceConfig _httpConfig;

        public HttpInterfaceConnector(HttpInterfaceConfig config):base(config)
        {
            if(config == null)
            {
                throw new Exception("HttpSender init error,httpApi is null!");
            }
            try
            {
                _httpConfig = config;
                Init(_httpConfig);
            }
            catch(Exception ex)
            {
                throw new Exception($"{config.Action}：Api to HttpApi conversion error.{ex}");
            }
        }


        DataExpression evaluator;
        DataExpression des_evaluator;
        private void Init(HttpInterfaceConfig config)
        {
            ParseConfig(_httpConfig.Configs);
            evaluator = new DataExpression(_httpConfig.Rules);
            des_evaluator = new DataExpression(_httpConfig.DesRules);
        }

        private void ParseConfig(List<KeyValue> configs)
        {
            if (configs == null) return;

            foreach(var pair in configs )
            {
                // 建立SSL安全通道
                if(pair.Key == "SSL" && pair.Value == "1")
                {
                    // .net 4.0
                    //ServicePointManager.SecurityProtocol = (SecurityProtocolType)12288 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)12288 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
                }
                if(pair.Key.ToLower() == "timeout")
                {
                    _timeout = Convert.ToInt32(pair.Value);
                }
            }
        }
        private string _url;
        private List<KeyValue> _headers;
        private List<KeyValue> _parmeters;
        private int _timeout = 10000;

        public List<KeyValue> GetHeaders()
        {
            return _headers;
        }
        public List<KeyValue> GetParmeters()
        {
            return _parmeters;
        }
        public string GetUrl()
        {
            return _url;
        }

        public override string BuildRequest(string message)
        {
            var dic = DictionaryTool.ParseJsonStr(message);

            dic = evaluator.Interpret(dic);

            DataParser inDicParser = new DataParser(_httpConfig.Rules, _httpConfig.Template);
            message = inDicParser.Parse(dic,_httpConfig.InfoType);

            _url = MarkReplacer.ReplaceParentheses(dic, _httpConfig.Url, new StringBuilder(_httpConfig.Url));
            _headers = MarkReplacer.ReplaceParentheses(dic, _httpConfig.Hearders);
            _parmeters = MarkReplacer.ReplaceParentheses(dic, _httpConfig.Parmeters);

            message = MarkReplacer.ReplaceParentheses(dic, message, new StringBuilder(message));

            var outer_template = _httpConfig.OuterTemplate.Trim();
            if (!string.IsNullOrEmpty(outer_template))
            {
                // 智能识别消息类型，分别进行处理
                if(JsonTool.IsJson(outer_template))
                {
                    DataParser out_parser = new DataParser(_httpConfig.Rules, outer_template);
                    outer_template = out_parser.Parse(dic,InfoType.JSON);
                }
                else if(XmlTool.IsXml(outer_template))
                {
                    DataParser out_parser = new DataParser(_httpConfig.Rules, outer_template);
                    outer_template = out_parser.Parse(dic, InfoType.XML);
                }
                var outer_message = MarkReplacer.ReplaceParentheses(dic, _httpConfig.OuterTemplate, new StringBuilder(_httpConfig.OuterTemplate));
                if (JsonTool.IsJson(outer_template))
                {
                    message = JsonTool.Escape(message);
                }
                message = outer_message.Replace("((message))", message);
            }
            return message;
        }
        public override string SendRequest(string message)
        {
            var httpHelper = new HttpSend(_url, _httpConfig.Method, _headers, _parmeters, _timeout);
            
            message = httpHelper.Send(message);

            InfoPathHandler responseHandler = new InfoPathHandler(_httpConfig.ParsePath);
            message = responseHandler.Handle(message);
            return message;
        }
        public override string HandleResponse(string message)
        {
            var desDicParser = new DataDesParser(_httpConfig.DesRules,_httpConfig.ParseInfoType);
            var dic = desDicParser.Parse(message);
            if(dic == null)
            {
                return message;
            }
            dic = des_evaluator.Interpret(dic);
            return DictionaryTool.ConvertToJsonStr(dic);
        }
    }
}