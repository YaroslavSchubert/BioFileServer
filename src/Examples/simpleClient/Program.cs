using System;
using System.IO;
using Grpc.Core;
using Bioskynet.Services;
using Google.Protobuf;

namespace Bioskynet.SimpleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Channel channel = new Channel("127.0.0.1:12337", ChannelCredentials.Insecure);

            var client = new FileService.FileServiceClient(channel);
            String user = "Bruce";

            var reply = client.SayHello(new HelloRequest { Name = user });
            Console.WriteLine("Greeting: " + reply.Message);

            var fileResult = client.CreateFile(new FileBytes()
            {
                Data = ByteString.CopyFrom(File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "index.html")))
            });
            Console.WriteLine($"File created. Id: {fileResult.Id}");

            var fileBytes = client.GetFile(fileResult);
            Console.WriteLine($"File received. File bytes: {fileBytes}");

            client.DeleteFile(fileResult);
            Console.WriteLine($"File {fileResult.Id} deleted");

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
