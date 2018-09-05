using Contracts;
using System;
using System.Diagnostics;
using System.ServiceModel;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var channelFactory = new ChannelFactory<IHelloWorldService>(
                new NetNamedPipeBinding(),
                new EndpointAddress("net.pipe://localhost/my-services/hello-service")))
            {
                while (true)
                {
                    var watch = Stopwatch.StartNew();

                    IClientChannel channel = null;
                    try
                    {
                        var helloWorldService = channelFactory.CreateChannel();
                        helloWorldService.HelloWorld();
                        channel = (IClientChannel)helloWorldService;
                        channel.Close();
                    }
                    catch (EndpointNotFoundException)
                    {

                    }
                    catch (CommunicationException)
                    {
                        channel.Abort();
                    }
                    catch (TimeoutException)
                    {
                        channel.Abort();
                    }
                    catch (Exception)
                    {
                        channel.Abort();
                        throw;
                    }

                    Console.WriteLine(watch.ElapsedMilliseconds);
                    Console.ReadLine();
                }
            }
        }

        static void UseProxy()
        {
            using (var proxy = new HelloWorldServiceProxy())
            {
                proxy.InvokeHelloWorld();
            }
        }
    }
}
