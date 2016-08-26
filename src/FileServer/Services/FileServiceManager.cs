using Grpc.Core;
using Bioskynet.Services;
using System;

namespace Bioskynet.Services
{

    class FileServiceManager
    {
        Server _server;

        public void Start()
        {
            _server = new Server
            {
                Services = { FileServer.BindService(new FileService()) },
                Ports = { new ServerPort("localhost", 12337, ServerCredentials.Insecure) }
            };
            _server.Start();
            Console.WriteLine("FileService started...");
        }

        public void Stop()
        {
            _server?.ShutdownAsync().Wait();
            Console.WriteLine("FileService stopped...");
        }
    }

}