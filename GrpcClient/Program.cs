using System;
using System.Threading.Tasks;
using Grpc.Net.Client;


namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:9090");
            var client = new receta.recetaClient(channel);

        }
    }
}
