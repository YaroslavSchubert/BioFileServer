using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using System;
using System.IO;
using Services;
using Google.Protobuf.WellKnownTypes;
using NLog;

namespace Bioskynet.Services
{
    class FileServer : FileService.FileServiceBase
    {
        private ILogger _logger;

        public FileServer()
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public override Task<FileMessage> Create(FileBytes bytes, ServerCallContext context)
        {
            _logger.Trace($"Create: start processing, byteSize={bytes.CalculateSize()}");
            try
            {
                var fileResult = Utils.GenerateFilePathAndGuid();
                var fileMessage = new FileMessage() { Id = fileResult[1] };
                var filePath = fileResult[0];
                Utils.WriteFile(filePath, bytes.Data.ToByteArray());
                _logger.Trace($"Create: Success, id={fileMessage.Id}");
                return Task.FromResult(fileMessage);
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex, "Create: ");
                throw new RpcException(new Status(StatusCode.Internal, "Failed to create file"));
            }
        }
        public override Task<FileBytes> Get(FileMessage file, ServerCallContext context)
        {
            //TODO Lock file
            try
            {
                Guid temp;
                if (!Guid.TryParse(file.Id, out temp))
                {
                    _logger.Error($"Get: invalid FileID={file.Id}");
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "File ID is invalid"));
                }
                _logger.Trace($"Get: start processing, id={file.Id}");
                var filePath = Utils.GetFilePathById(file.Id);

                if (!File.Exists(filePath))
                {
                    _logger.Error($"Get: File doesn't exist; FileID={file.Id}");
                    throw new RpcException(new Status(StatusCode.NotFound, "File doesn't exist"));
                }

                var fileBytes = new FileBytes()
                {
                    Data = ByteString.CopyFrom(Utils.ReadFile(filePath))
                };
                _logger.Trace($"Get: Success, id={file.Id}; byteSize={fileBytes.CalculateSize()}");
                return Task.FromResult(fileBytes);
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex, "Get: ");
                throw new RpcException(new Status(StatusCode.Internal, "Failed to get file"));
            }
        }

        public override Task<ExistMessage> Exists(FileMessage msg, ServerCallContext context)
        {
            try
            {
                var exist = File.Exists(Utils.GetFilePathById(msg.Id));
                _logger.Trace($"Exists: {exist}, id={msg.Id}");
                return Task.FromResult(new ExistMessage() { Result = exist });
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex, "Exists: ");
                throw new RpcException(new Status(StatusCode.Internal, "Exists Failed"));
            }
        }

        public override Task<Empty> Delete(FileMessage file, ServerCallContext context)
        {
            try
            {
                Utils.DeleteFile(Utils.GetFilePathById(file.Id));
                _logger.Trace($"Delete: id={file.Id}");
                return Task.FromResult(new Empty());
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex, "Delete: ");
                throw new RpcException(new Status(StatusCode.Internal, "Delete Failed"));
            }
        }
    }
}