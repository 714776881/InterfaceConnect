using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceConnect
{
    public class KeyObject
    {
        public KeyObject(string key, Object obj)
        {
            this.Key = key;
            this.Obj = obj;
        }
        public string Key { get; set; }
        public Object Obj { get; set; }
    }

}
