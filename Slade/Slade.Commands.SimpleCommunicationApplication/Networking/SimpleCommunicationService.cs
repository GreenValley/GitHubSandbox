using System.ServiceModel;
namespace Slade.Commands.SimpleCommunicationApplication.Networking
{
    public delegate void MessageReceivedEventHandler(string message);

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SimpleCommunicationService : ISimpleCommunicationService
    {
        public event MessageReceivedEventHandler MessageReceived;

        public void SendMessage(string message)
        {
            var handler = MessageReceived;
            if (handler != null)
            {
                handler(message);
            }
        }
    }
}