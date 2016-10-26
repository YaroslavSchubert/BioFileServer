using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using System;
using System.IO;

namespace Bioskynet.Services
{
    class FileServer : FileService.FileServiceBase
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
            //TODO Lock file
            Guid temp;
            if (!Guid.TryParse(file.Id, out temp))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "File ID is invalid"));
            }
            var filePath = Utils.GetFilePathById(file.Id);

            if (!File.Exists(filePath))
            {
                throw new RpcException(new Status(StatusCode.NotFound, "File doesn't exist"));
            }

            var fileBytes = new FileBytes()
            {
                Data = ByteString.CopyFrom(Utils.ReadFile(filePath))
            };
            return Task.FromResult(fileBytes);
        }

        public override Task<ExistMessage> FileExists(FileMessage msg, ServerCallContext context)
        {
            var exist = File.Exists(Utils.GetFilePathById(msg.Id));
            return Task.FromResult(new ExistMessage() { Result = exist });
        }

        public override Task<EmptyMessage> DeleteFile(FileMessage file, ServerCallContext context)
        {
            Utils.DeleteFile(Utils.GetFilePathById(file.Id));
            return Task.FromResult(new EmptyMessage());
        }
    }
}