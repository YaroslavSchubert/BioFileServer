using System;
using System.IO;
using Grpc.Core;
using Services;
using Google.Protobuf;

namespace Bioskynet.SimpleClient
{
    public class Program
    {
        // static string ServiceAddress = "192.168.1.200";
        static string ServiceAddress = "localhost";
        static string ServicePort = "65000";

        public static void Main(string[] args)
        {
            Channel channel = new Channel($"{ServiceAddress}:{ServicePort}", ChannelCredentials.Insecure);
            var client = new FileService.FileServiceClient(channel);

            Console.WriteLine($"SimpleClient connected to file service at {ServiceAddress}:{ServicePort}");
           
            var fileResult = client.Create(new FileBytes()
            {
                Data = ByteString.CopyFrom(File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "index.html")))
            });
            Console.WriteLine($"File created. Id: {fileResult.Id}");

            var fileBytes = client.Get(fileResult);
            Console.WriteLine($"File received. File bytes: {fileBytes}");

            var exists = client.Exists(fileResult);
            Console.WriteLine($"File {fileResult.Id} exists: {exists.Result}");

            client.Delete(fileResult);
            Console.WriteLine($"File {fileResult.Id} deleted");

            exists = client.Exists(fileResult);
            Console.WriteLine($"File {fileResult.Id} exists: {exists.Result}");            

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
