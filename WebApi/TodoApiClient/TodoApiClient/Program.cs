using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TodoApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            AccessSEServer();
            AccessDebugWebApi();
            AccessGitHubApi();
            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }

        static void AccessSEServer()
        {
					  string seServerApiUrl = ; //
            using (var handler = new HttpClientHandler())
            using (var client = new HttpClient(handler))
            {
                handler.Credentials = CredentialCache.DefaultCredentials;
                string msg = client.GetStringAsync(seServerApiUrl).Result;
                Console.WriteLine(msg);
            }
        }

        static void AccessDebugWebApiGlobalSettings()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = new HttpClient())
            {
                string msg = client.GetStringAsync("http://localhost:5000/api/values").Result;
                Console.WriteLine(msg);
            }
        }

        static void AccessDebugWebApi()
        {
            using (var handler = new HttpClientHandler())
            using (var client = new HttpClient(handler))
            {
                handler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls11;

                string msg = client.GetStringAsync("http://localhost:5000/api/values").Result;
                Console.WriteLine(msg);
            }
        }

        static void AccessGitHubApi()
        {
            using (var handler = new HttpClientHandler())
            using (var client = new HttpClient(handler))
            {
                handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

                var msg = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos").Result;

                Console.WriteLine(msg);
            }
        }
    }
}
