using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceConnect
{
    public class DBInterfaceConfig : RuleConfig
    {
        public DBType DBtype { get; set; }
        public string Connection { get; set; }
        public SQLType SQLType { get; set; }
        public List<StoredProcedureParameter> Parameters { get; set; }
    }
}