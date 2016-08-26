using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;
using Grpc.Core;
using System.IO;
using System;
using Bioskynet.Services;

namespace Bioskynet.Services
{
    class FileService : FileServer.FileServerBase
    {
        public override Task<EmptyMessage> Heartbeat(EmptyMessage request, ServerCallContext context) => Task.FromResult(request);
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var reply = new Services.HelloReply();
            reply.Message = $"Hello {request.Name}!";
            return Task.FromResult(reply);
        }
        public override Task<FileMessage> CreateFile(FileBytes bytes, ServerCallContext context)
        {
            var fileResult = Utils.GenerateFilePathAndGuid();
            var fileMessage = new FileMessage() { Id = fileResult[1] };
            var filePath = fileResult[0];
            Utils.WriteFile(filePath, bytes.Data.ToByteArray());
            return Task.FromResult(fileMessage);
        }
        public override Task<FileBytes> GetFile(FileMessage file, ServerCallContext context)
        {
            Guid temp;
            if(!Guid.TryParse(file.Id, out temp))
                throw new FormatException("File ID is invalid");
            var filePath = Utils.GetFilePath(file.Id);            
            var fileBytes = new FileBytes()
            {
                Data = ByteString.CopyFrom(Utils.ReadFile(filePath))
            };
            return Task.FromResult(fileBytes);
        }
        public override Task<EmptyMessage> DeleteFile(FileMessage file, ServerCallContext context)
        {
            Utils.DeleteFile(Utils.GetFilePath(file.Id));
            return Task.FromResult(new EmptyMessage());
        }
    }
}