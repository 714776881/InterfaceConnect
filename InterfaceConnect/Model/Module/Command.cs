using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommMediator
{
    public class Command : IInterfaceConnector
    {

        private IInterfaceConnector _request;
        private IInterfaceConnector _response;
        private IInterfaceConnector _comm;
        public Command(IInterfaceConnector request, IInterfaceConnector response, IInterfaceConnector comm)
        {
            _request = request;
            _response = response;
            _comm = comm;
        }
        public string Send(string message)
        {
            var request = _request.Send(message);

            var backInfo = _comm.Send(request);

            var response = _response.Send(backInfo);

            return response;
        }
    }
}
