using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceConnect
{
    public class KeyValue
    {
        public KeyValue(string key,string value,string description)
        {
            this.Key = key;
            this.Value = value;
            this.Description = description;
        }
        public string Key
        {
            get;set;
        }
        public string Value
        {
            get;set;
        }
        public string Description
        {
            get;set;
        }
        override
        public string ToString()
        {
            return string.Format("{0}={1},", Key, Value);
        }

    }
}
