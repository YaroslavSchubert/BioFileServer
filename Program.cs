using System;

namespace BioFileServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var logPath = System.IO.Path.GetTempFileName();
            Console.WriteLine(logPath);

            string text = Console.ReadLine();
            while (text != "#q")
            {
                Utils.WriteFile(logPath, text);
                Console.WriteLine(Utils.ReadFile(logPath));
                text = Console.ReadLine();
            }
        }

        
    }
}
