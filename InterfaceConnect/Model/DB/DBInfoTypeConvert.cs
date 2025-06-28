using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace InterfaceConnect
{
    public class DBInfoTypeConvert
    {
        public static object ConvertStringToDbType(string value, System.Data.DbType type)
        {
            switch (type)
            {
                case DbType.String:
                    return value;
                case DbType.Int32:
                    return int.Parse(value);
                case DbType.Double:
                    return double.Parse(value);
                case DbType.Decimal:
                    return decimal.Parse(value);
                case DbType.DateTime:
                    return DateTime.Parse(value);
                default:
                    throw new ArgumentException("Invalid DbType");
            }
        }
    }
}
