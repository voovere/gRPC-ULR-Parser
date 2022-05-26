using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Web;
using Parse;
using Grpc.Net.Client;

namespace Client
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Parser.ParserClient(channel);

            Console.WriteLine("Enter url: ");
            string url = Console.ReadLine();

            var reply = await client.ParseUrlAsync(new ParserRequest { Url = url });
            Console.WriteLine("Parsed url: ");
            Console.WriteLine("Protocol: {0}", reply.Scheme);
            Console.WriteLine("Host: {0}", reply.Host);
            Console.WriteLine("Path: {0}", reply.Path);
            Console.WriteLine("Query: {0}", reply.Query);
            NameValueCollection Parms = HttpUtility.ParseQueryString(reply.Query);
            Console.WriteLine("Parms: {0}", Parms.Count);
            foreach (string x in Parms.AllKeys)
                Console.WriteLine("\tParm: {0} = {1}", x, Parms[x]);

            Console.WriteLine("Shutting down");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
