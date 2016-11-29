using System;
using System.IO;
using NLog;

namespace Bioskynet
{
    class Utils
    {
        private static ILogger _logger;

        static Utils()
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
        }
        public static void WriteFile(string filePath, byte[] data)
        {
            _logger.Trace($"WriteFile: Path={filePath}, size={data.Length}");
            File.WriteAllBytes(filePath, data);
        }
        public static string ReadFileText(string filePath)
        {
            _logger.Trace($"ReadFileText: Start; path={filePath}");
            string fileContent;
            using (StreamReader reader = File.OpenText(filePath))
            {
                fileContent = reader.ReadToEnd();
            }
            _logger.Trace($"ReadFileText: End; path={filePath}");
            return fileContent;
        }

        public static byte[] ReadFile(string filePath)
        {
            _logger.Trace($"ReadFile: Start; Path={filePath}");
            var result = File.ReadAllBytes(filePath);
            _logger.Trace($"ReadFile: End; Path={filePath}, size={result.Length}");
            return result;
        }

        public static void DeleteFile(string filePath)
        {
            _logger.Trace($"DeleteFile: Path={filePath}");
            File.Delete(filePath);
        }

        public static string[] GenerateFilePathAndGuid()
        {
            _logger.Trace($"GenerateFilePathAndGuid: Start");
            string fileName;
            string filePath;
            while (true)
            {
                fileName = Guid.NewGuid().ToString();
                filePath = GetFilePathById(fileName);
                _logger.Trace($"GenerateFilePathAndGuid: Generating for name={fileName}, path={filePath}");
                if (!File.Exists(filePath))
                {
                    using (File.Create(filePath)) ;
                    _logger.Trace($"GenerateFilePathAndGuid: Generated; Name={fileName}, path={filePath}");
                    break;
                }
            }
            _logger.Trace($"GenerateFilePathAndGuid: End; Name={fileName}, path={filePath}");
            return new[] { filePath, fileName };
        }

        public static string GetFilePathById(string fileId)
        {
            _logger.Trace($"GetFilePathById: Start; id={fileId}");
            var currentDir = Directory.GetCurrentDirectory();
            var dirPath = Path.Combine(currentDir, "files");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            var result = System.IO.Path.Combine(dirPath, fileId);
            _logger.Trace($"GetFilePathById: End; id={fileId}, path={result} ");
            return result;
        }

    }
}