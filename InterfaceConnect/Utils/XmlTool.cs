using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;
using System.Text.RegularExpressions;

namespace InterfaceConnect
{
    public class XmlTool
    {
        public static bool IsXml(string str)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(str);
            }
            catch
            {
                return false;
            }
            return true;
        }
        // 获取节点
        public static string GetNodeByNamespace(string xmlString, string path)
        {
            if(string.IsNullOrWhiteSpace(xmlString))
            {
                return string.Empty;
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);

            XmlNamespaceManager nsMgr = new XmlNamespaceManager(doc.NameTable);
            var namespaces = XmlNamespaceTool.GetNamespace(xmlString);
            foreach(var item in namespaces)
            {
                nsMgr.AddNamespace(item.Key, item.Value);
            }
            XmlNodeList nodes = doc.SelectNodes(path, nsMgr);
            foreach(XmlNode node in nodes)
            {
                return node.InnerText;
            }
            return string.Empty;
        }
        public static string GetListFirstNode(string data, string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            XmlNode parent = doc.SelectSingleNode(path);
            if (parent == null || parent.ChildNodes == null || parent.ChildNodes.Count == 0)
            {
                return data;
            }
            XmlNode node = parent.ChildNodes[0];
            return node.OuterXml;
        }

        public static void GetAllNamespaces(XmlDocument xmlDoc)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            foreach (XmlAttribute attribute in xmlDoc.DocumentElement.Attributes)
            {
                if (attribute.Name.StartsWith("xmlns"))
                {
                    string prefix = attribute.Name.Contains(":") ? attribute.Name.Split(':')[1] : string.Empty;
                    nsmgr.AddNamespace(prefix, attribute.Value);
                }
            }

            foreach (XmlNode node in xmlDoc.ChildNodes)
            {
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    if (attribute.Name.StartsWith("xmlns"))
                    {
                        string prefix = attribute.Name.Contains(":") ? attribute.Name.Split(':')[1] : string.Empty;
                        nsmgr.AddNamespace(prefix, attribute.Value);
                    }
                }
            }

            foreach (var prefix in nsmgr)
            {
                Console.WriteLine("Prefix: {0}, Namespace: {1}", prefix, nsmgr.LookupNamespace(prefix.ToString()));
            }
        }
        public static string AddListNode(string data, string path, string value)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            XmlNode parent = doc.SelectSingleNode(path);
            if(parent == null ||parent.ChildNodes == null || parent.ChildNodes.Count == 0)
            {
                return data;
            }
            parent.InnerXml = value;
            return doc.OuterXml;
        }
        public static string ToFormatXmlString(string xmlString)
        {
            try
            {
                XDocument x_doc = XDocument.Parse(xmlString);
                if(x_doc.Declaration == null)
                {
                    return x_doc.ToString();
                }
                return x_doc.Declaration.ToString() + "\n" +  x_doc.ToString();
            }
            catch
            {
                return xmlString;
            }
        }
        public static string GetNode(string data, string path)
        {
            if(string.IsNullOrEmpty(data) || string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
            XPathDocument doc = new XPathDocument(new StringReader(data));
            XPathNavigator navigator = doc.CreateNavigator();
            XPathNodeIterator iterator = navigator.Select(path);
            var value = string.Empty;
            while (iterator.MoveNext())
            {
                value = iterator.Current.InnerXml;
            }
            return value;
        }
        public static readonly string NameSpaceTag = "error=\"I am a placeholder for a namespace, you have found me\"";
        public static string RemoveNameSapce(string xmlMessage,ref string ns)
        {
            string pattern = "xmlns=\"[^\"]*\"";
            var item =  Regex.Match(xmlMessage, pattern);
            if(item != null)
            {
                ns = item.Value;
            }
            return Regex.Replace(xmlMessage, pattern, NameSpaceTag);
        }

        public static string AddNameSpace(string xmlMessage,string ns)
        {
            return xmlMessage.Replace(NameSpaceTag, ns);
        }

        public static List<string> GetChildrenNodes(string data, string path)
        {
            var value = new List<string>();
            if (string.IsNullOrEmpty(data) || string.IsNullOrEmpty(path))
            {
                return value;
            }
            XPathDocument doc = new XPathDocument(new StringReader(data));
            XPathNavigator navigator = doc.CreateNavigator();
            XPathNodeIterator iterator = navigator.Select(path);
            while (iterator.MoveNext())
            {
                XPathNodeIterator childNodes = iterator.Current.SelectChildren(XPathNodeType.Element);
                while (childNodes.MoveNext())
                {
                    value.Add(childNodes.Current.OuterXml);
                }
            }
            return value;
        }

        public static string SetNode(string data, string path, string value)
        {
            if (string.IsNullOrEmpty(data) || string.IsNullOrEmpty(path))
            {
                return data;
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            var nodes =  doc.SelectNodes(path);
            foreach(XmlNode node in nodes)
            {
                node.InnerXml = value;
            }
            return doc.OuterXml;
        }
    }
}