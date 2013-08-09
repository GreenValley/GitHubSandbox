using System.ServiceModel;

namespace Slade.Commands.SimpleCommunicationApplication.Networking
{
    public class SimpleCommunicationServiceClient : ClientBase<ISimpleCommunicationService>, ISimpleCommunicationService
    {
        public SimpleCommunicationServiceClient()
            : base(new BasicHttpBinding(), new EndpointAddress("http://localhost:80/service"))
        {
            Open();
        }

        public void SendMessage(string message)
        {
            Channel.SendMessage(message);
        }
    }
}