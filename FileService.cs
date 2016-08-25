using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Services;

namespace Bioskynet{
    class FileService : Services.BioFileServer.BioFileServerBase{
        public override Task<Empty> Heartbeat(Empty request, ServerCallContext context)
        {
            return Task.FromResult(request);
        }
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var reply = new Services.HelloReply();
            reply.Message = "Hello reply!";
            return Task.FromResult(reply);
        }
    } 
}