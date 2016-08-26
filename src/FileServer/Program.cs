using System;
using Bioskynet.Services;

namespace Bioskynet
{
    public class Program
    {
        static FileServiceManager fileServiceManager = new FileServiceManager();
        public static void Main(string[] args)
        {
            fileServiceManager.Start();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            fileServiceManager.Stop();
        }
    }
}
