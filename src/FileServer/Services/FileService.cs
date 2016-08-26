using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Bioskynet.Services;

namespace Bioskynet.Services{
    class FileService : FileServer.FileServerBase{
        public override Task<Empty> Heartbeat(Empty request, ServerCallContext context)
        {
            return Task.FromResult(request);
        }
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var reply = new Services.HelloReply();
            reply.Message = $"Hello {request.Name}!";
            return Task.FromResult(reply);
        }
    } 
}