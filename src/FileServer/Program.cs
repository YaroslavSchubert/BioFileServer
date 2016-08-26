using System;
using System.IO;
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
            // var currentDir = Directory.GetCurrentDirectory();
            // var dirPath = Path.Combine(currentDir, "files");
            // Directory.CreateDirectory(dirPath);
            // var fileName = Guid.NewGuid().ToString();
            // var filePath = System.IO.Path.Combine(dirPath, fileName);

            // Console.WriteLine(filePath);

            // string text = Console.ReadLine();
            // while (text != "#q")
            // {
            //     Utils.WriteFile(filePath, text);
            //     Console.WriteLine(Utils.ReadFile(filePath));
            //     text = Console.ReadLine();
            // }
        }
    }
}
