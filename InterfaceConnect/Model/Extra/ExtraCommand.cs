using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommMediator
{
    public abstract class ExtraApiCommand : BaseInterfaceConnector
    {
        private readonly ExtraConfig extraApi;
        public ExtraApiCommand(InterfaceConfig api):base(api)
        {
            if (api == null)
            {
                throw new ArgumentNullException("ExtraCommand init error,httpApi is null!");
            }
            try
            {
                extraApi = (ExtraConfig)api;
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException($"{api.Action}：Api to ExtraCommand conversion error.{ex}");
            }
        }
    }
}
