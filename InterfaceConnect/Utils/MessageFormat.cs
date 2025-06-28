using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System;

namespace InterfaceConnect
{
    public class MessageFormat
    {
        public static string OutputMessage(string message)
        {
            try
            {
                if (IsJson(message))
                {
                    return OutputJson(message);
                }
                else if (IsXml(message))
                {
                    return OutputXml(message);
                }
                else
                {
                    return message;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return message;
        }
        public static bool IsJson(string str)
        {
            try
            {
                JToken.Parse(str);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }
        public static string OutputJson(string jsonStr)
        {
            JObject obj = JObject.Parse(jsonStr);
            return obj.ToString();
        }
        public static bool IsXml(string str)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(str);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }
        public static string OutputXml(string xmlStr)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlStr);
            return xmlDoc.InnerXml;
        }
    }
}
