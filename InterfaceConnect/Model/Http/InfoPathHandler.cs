using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceConnect
{
    public class InfoPathHandler
    {
        private string _parsePath;

        public InfoPathHandler(string outcome_path)
        {
            _parsePath = outcome_path;
        }
        public string Handle(string message)
        {
            if(string.IsNullOrWhiteSpace(message) || string.IsNullOrWhiteSpace(_parsePath))
            {
                return message;
            }
            try
            {
                if(JsonTool.IsJson(message))
                {
                    message = JsonTool.GetNode(message, _parsePath);
                }
                else if(XmlTool.IsXml(message))
                {
                    message = XmlTool.GetNodeByNamespace(message, _parsePath);
                    message = message.Replace("&lt;", "<").Replace("&gt;", ">");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            return message;
        }
        public string HandleByType(string message, InfoType infoType)
        {
            if (string.IsNullOrWhiteSpace(message) || string.IsNullOrWhiteSpace(_parsePath))
            {
                return message;
            }
            try
            {
                switch (infoType)
                {
                    case InfoType.JSON:
                        message = JsonTool.GetNode(message, _parsePath);
                        break;
                    case InfoType.XML:
                        message = XmlTool.GetNodeByNamespace(message, _parsePath);
                        message = message.Replace("&lt;", "<").Replace("&gt;", ">");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            return message;
        }
    }
}