using System;
using System.Collections.Concurrent;

namespace InterfaceConnect
{
    public class ConnectorManager
    {
        private readonly ConcurrentDictionary<string, IInterfaceConnect> _connectors = new ConcurrentDictionary<string, IInterfaceConnect>();
        public IInterfaceConnect GetConnector(InterfaceConfig api,bool isNew = false)
        {
            if (_connectors.TryGetValue(api.Action, out IInterfaceConnect command) && isNew == false)
            {
                return command;
            }
            command = ConnectorFactory.Create(api);
            _connectors[api.Action] =  command;
            return command;
        }
        public void Remove(string action)
        {
            IInterfaceConnect connector;

            if (_connectors.ContainsKey(action))
            {
                _connectors.TryRemove(action,out connector);
            }
        }
    }
}
