using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceConnect
{
    class ConnectorFactory
    {
        public static IInterfaceConnect Create(InterfaceConfig api)
        {
            switch (api.Type)
            {
                case ConfigType.HTTP:
                    return new HttpInterfaceConnector((HttpInterfaceConfig)api);
                case ConfigType.DB:
                    return new DBInterfaceConnector((DBInterfaceConfig)api);
                case ConfigType.DATA:
                    return new DBInterfaceConnector((DBInterfaceConfig)api);
                case ConfigType.SCRIPT:
                    return new ScriptConnector((ScriptConfig)api);
                default:
                    throw new NotSupportedException($"API Type {api.Type} not supported.");
            }
        }
        private static BaseInterfaceConnect GetConnectorByClassName(string className)
        {
            if (string.IsNullOrEmpty(className))
            {
                return null;
            }
            try
            {
                Object obj = ReflectionTool.CreateInstance(className);
                if (obj == null)
                {
                    return null;
                }
                return (BaseInterfaceConnect)obj;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"creat {className} error!{ex}");
            }
            return null;
        }
    }
}
