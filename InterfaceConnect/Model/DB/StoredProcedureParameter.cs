using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace InterfaceConnect
{
    public class StoredProcedureParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public DbType Type { get; set; }
        public ParameterDirection Direction { get; set; }
    }
}
