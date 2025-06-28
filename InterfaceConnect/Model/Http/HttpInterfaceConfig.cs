using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using RestSharp;

namespace InterfaceConnect
{
    public class HttpInterfaceConfig: RuleConfig
    {
        public string Url { get; set; }
        public Method Method { get; set; }
        public List<KeyValue> Parmeters { get; set; }
        public List<KeyValue> Hearders { get; set; }
    }
}
