using System;
using System.IO;

namespace Bioskynet
{
    class Utils
    {
        public static void WriteFileText(string filePath, string message)
        {
            var logFile = System.IO.File.OpenWrite(filePath);
            using (var logWriter = new System.IO.StreamWriter(logFile))
            {
                logWriter.WriteLine(message);
            }
        }

        public static void WriteFile(string filePath, byte[] data) => File.WriteAllBytes(filePath, data);
        public static string ReadFileText(string filePath)
        {
            string fileContent;
            using (StreamReader reader = File.OpenText(filePath))
            {
                fileContent = reader.ReadToEnd();
            }
            return fileContent;
        }

        public static byte[] ReadFile(string filePath) => File.ReadAllBytes(filePath);

        public static void DeleteFile(string filePath) => File.Delete(filePath);

        public static string[] GenerateFilePathAndGuid()
        {
            string fileName;
            string filePath;
            while (true)
            {
                fileName = Guid.NewGuid().ToString();
                filePath = GetFilePathById(fileName);
                if (!File.Exists(filePath)){
                    using(File.Create(filePath));
                    break;
                }
            }
            return new[] { filePath, fileName };
        }

        public static string GetFilePathById(string fileId)
        {
            var currentDir = Directory.GetCurrentDirectory();
            var dirPath = Path.Combine(currentDir, "files");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            return System.IO.Path.Combine(dirPath, fileId);
        }

    }
}