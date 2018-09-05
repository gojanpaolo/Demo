using Contracts;
using System;
using System.ServiceModel;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceHost = new ServiceHost(typeof(HelloWorldService), new Uri("net.pipe://localhost/my-services"));
            serviceHost.AddServiceEndpoint(typeof(IHelloWorldService), new NetNamedPipeBinding(), "hello-service");
            serviceHost.Open();

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }
    }
}
