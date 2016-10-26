using System;
using System.IO;
using Grpc.Core;
using Bioskynet.Services;
using Google.Protobuf;

namespace Bioskynet.SimpleClient
{
    public class Program
    {
        // string ServiceAddress = "192.168.1.59"
        static string ServiceAddress = "localhost";
        static string ServicePort = "65000";

        public static void Main(string[] args)
        {
            Channel channel = new Channel($"{ServiceAddress}:{ServicePort}", ChannelCredentials.Insecure);
            var client = new FileService.FileServiceClient(channel);
            String user = "Bruce";

            Console.WriteLine($"SimpleClient connected to file service at {ServiceAddress}:{ServicePort}");


            var reply = client.SayHello(new HelloRequest { Name = user });
            Console.WriteLine("Greeting: " + reply.Message);

            var fileResult = client.CreateFile(new FileBytes()
            {
                Data = ByteString.CopyFrom(File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "index.html")))
            });
            Console.WriteLine($"File created. Id: {fileResult.Id}");

            var fileBytes = client.GetFile(fileResult);
            Console.WriteLine($"File received. File bytes: {fileBytes}");

            var exists = client.FileExists(fileResult);
            Console.WriteLine($"File {fileResult.Id} exists: {exists.Result}");

            client.DeleteFile(fileResult);
            Console.WriteLine($"File {fileResult.Id} deleted");

            exists = client.FileExists(fileResult);
            Console.WriteLine($"File {fileResult.Id} exists: {exists.Result}");            

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
