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
            var fileName = Guid.NewGuid().ToString();
            var filePath = GetFilePath(fileName);
            if (File.Exists(filePath))
                throw new Exception("File already exists");
            return new[] { filePath, fileName };
        }

        public static string GetFilePath(string fileId)
        {
            var currentDir = Directory.GetCurrentDirectory();
            var dirPath = Path.Combine(currentDir, "files");
            Directory.CreateDirectory(dirPath);
            return System.IO.Path.Combine(dirPath, fileId);
        }

    }
}