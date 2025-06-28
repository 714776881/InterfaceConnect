using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace InterfaceConnect
{
    public class ReflectionTool
    {
        public static object CreateInstance(string className)
        {
            Type type = Type.GetType(className);
            if (type != null)
            {
                try
                {
                    object instance = Activator.CreateInstance(type);
                    return instance;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Con't create class with {className},{ex}");
                }
            }
            else
            {
                throw new NotSupportedException($"Con't find class with {className}");
            }
        }


    }
}
