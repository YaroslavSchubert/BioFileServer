using Grpc.Core;
using System;

namespace Bioskynet.Services
{
    class FileServiceManager
    {
        Server _server;
        string ServerAddress = "0.0.0.0";
        int ServerPort = 65000;

        public void Start()
        {
            _server = new Server
            {
                Services = { FileService.BindService(new FileServer()) },
                Ports = { new ServerPort(ServerAddress, ServerPort, ServerCredentials.Insecure) }
            };
            _server.Start();
            Console.WriteLine($"FileService started at {ServerAddress}:{ServerPort} ...");
        }

        public void Stop()
        {
            _server?.ShutdownAsync().Wait();
            Console.WriteLine("FileService stopped...");
        }
    }

}