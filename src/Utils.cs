using System;
using System.IO;

namespace Bioskynet
{
    class Utils
    {
        public static void WriteFile(string filePath, string message)
        {
            var logFile = System.IO.File.OpenWrite(filePath);
            using (var logWriter = new System.IO.StreamWriter(logFile))
            {

                logWriter.WriteLine(message);
            }
        }

        public static string ReadFile(string filePath)
        {
            string fileContent;
            using (StreamReader reader = File.OpenText(filePath))
            {
                fileContent = reader.ReadToEnd();
            }
            return fileContent;
        }

    }
}