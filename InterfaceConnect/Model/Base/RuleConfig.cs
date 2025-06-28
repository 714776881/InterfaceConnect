using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceConnect
{
    public class RuleConfig: InterfaceConfig
    {
        public MapNode[] InMapNodes { get; set; }
        public MapNode[] OutMapNodes { get; set; }
        public string Template { get; set; }
        public InfoType InfoType { get; set; }
        public List<DataSingleRule> Rules { get; set; }
        public string OuterTemplate { get; set; }
        public InfoType ParseInfoType { get; set; }
        public List<DataSingleRule> DesRules { get; set; }
        public string ParsePath { get; set; }
    }
}
