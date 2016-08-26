using System;
using Grpc.Core;
using Bioskynet.Services;

namespace Bioskynet.SimpleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Channel channel = new Channel("127.0.0.1:12337", ChannelCredentials.Insecure);

            var client = new FileServer.FileServerClient(channel);
            String user = "Bruce";

            var reply = client.SayHello(new HelloRequest { Name = user });
            Console.WriteLine("Greeting: " + reply.Message);

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
