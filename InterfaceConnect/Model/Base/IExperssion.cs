using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceConnect
{
    interface IExperssion
    {
         Dictionary<string, object> Interpret(Dictionary<string, object> dic);
    }
}
