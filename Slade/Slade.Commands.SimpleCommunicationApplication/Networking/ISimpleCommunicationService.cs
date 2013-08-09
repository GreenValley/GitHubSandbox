using System.ServiceModel;
namespace Slade.Commands.SimpleCommunicationApplication.Networking
{
    [ServiceContract]
    public interface ISimpleCommunicationService
    {
        [OperationContract]
        void SendMessage(string message);
    }
}