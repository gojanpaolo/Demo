using System.ServiceModel;

namespace Contracts
{
    [ServiceContract]
    public interface IHelloWorldService
    {
        [OperationContract]
        void HelloWorld();
    }
}
