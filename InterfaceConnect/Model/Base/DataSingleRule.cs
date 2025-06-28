using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceConnect
{
    // 单规则节点：对用单一节点
    // 多规则节点：对应数组集合
    public class DataSingleRule
    {
        public string Key { get; set; }
        public KeyType KeyType { get; set; }
        public string Formula { get; set; }
        public string Position { get; set; }
        public string Remark { get; set; }
        public List<DataSingleRule> Rules { get; set; }
    }
}
