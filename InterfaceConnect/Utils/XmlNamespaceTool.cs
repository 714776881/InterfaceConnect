using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace InterfaceConnect
{
    public class XmlNamespaceTool
    {
        public static List<KeyValue> GetNamespace(string xmlString)
        {
            string pattern = @"xmlns:([\w]+)=""([^""]+)""";
            MatchCollection matches = Regex.Matches(xmlString, pattern);
            //xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
            List<KeyValue> items = new List<KeyValue>();
            foreach (Match match in matches)
            {
                string namespaceName = match.Groups[1].Value;
                string namespaceValue = match.Groups[2].Value;
                KeyValue item = new KeyValue(namespaceName, namespaceValue, "");
                items.Add(item);
            }
            // xmlns="ZLSoft"
            pattern = @"xmlns=""([^""]*)""";
            matches = Regex.Matches(xmlString, pattern);
            // xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
            foreach (Match match in matches)
            {
                string value = match.Groups[1].Value;

                KeyValue item;
                if (value.Contains("/"))
                {
                    item = new KeyValue("ns", value, "");
                }
                else
                {
                    item = new KeyValue(value, value, "");
                }

                items.Add(item);
            }
            return items;
        }

    }
}
