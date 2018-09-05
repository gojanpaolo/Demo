using Contracts;
using System;

namespace Host
{
    public class HelloWorldService : IHelloWorldService
    {
        public void HelloWorld()
        {
            Console.WriteLine("Hello World");
        }
    }
}
