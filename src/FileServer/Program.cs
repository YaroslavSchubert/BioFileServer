using System;
using NLog;
using Bioskynet.Services;

namespace Bioskynet
{
    public class Program
    {
        private static ILogger _logger;
        static FileServiceManager fileServiceManager = new FileServiceManager();
        public static void Main(string[] args)
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
            fileServiceManager.Start();
            Console.WriteLine("FileServer started");
            Console.ReadLine();
            fileServiceManager.Stop();
        }
    }
}
