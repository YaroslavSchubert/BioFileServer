using Grpc.Core;
using System;
using Services;
using NLog;

namespace Bioskynet.Services
{
    class FileServiceManager
    {
        private ILogger _logger;
        Server _server;
        string ServerAddress = "0.0.0.0";
        int ServerPort = 65000;

        public FileServiceManager()
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public void Start()
        {
            _server = new Server
            {
                Services = { FileService.BindService(new FileServer()) },
                Ports = { new ServerPort(ServerAddress, ServerPort, ServerCredentials.Insecure) }
            };
            _server.Start();
            _logger.Info($"FileService started at {ServerAddress}:{ServerPort} ...");
        }

        public void Stop()
        {
            _server?.ShutdownAsync().Wait();
            _logger.Info("FileService stopped successfully");
        }
    }
}