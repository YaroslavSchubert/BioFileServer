using System;
using System.IO;

namespace BioFileServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var currentDir = Directory.GetCurrentDirectory();
            var dirPath = Path.Combine(currentDir, "files");
            Directory.CreateDirectory(dirPath);
            var fileName = Guid.NewGuid().ToString();
            var filePath = System.IO.Path.Combine(dirPath, fileName);
            
            Console.WriteLine(filePath);

            string text = Console.ReadLine();
            while (text != "#q")
            {
                Utils.WriteFile(filePath, text);
                Console.WriteLine(Utils.ReadFile(filePath));
                text = Console.ReadLine();
            }
        }
    }
}
