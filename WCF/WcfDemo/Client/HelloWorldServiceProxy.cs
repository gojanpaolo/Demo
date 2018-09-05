using Contracts;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Client
{
    public class HelloWorldServiceProxy : ClientBase<IHelloWorldService>
    {
        public HelloWorldServiceProxy() : 
            base(new ServiceEndpoint(
                ContractDescription.GetContract(typeof(IHelloWorldService)),
                new NetNamedPipeBinding(), 
                new EndpointAddress("net.pipe://localhost/my-services/hello-service")))
        {
        }

        public void InvokeHelloWorld()
        {
            Channel.HelloWorld();
        }
    }
}
