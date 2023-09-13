using grpc_client.Models;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grpc.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SuscripcionesController
    {
    
        [HttpGet]
        [Route("GetSeg")]
        public async Task<string> GetSegAsync(string seg)
        {
            string response;
            try
            {
                Console.WriteLine("entro");
                // This switch must be set before creating the GrpcChannel/HttpClient.
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);;
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Suscripciones.SuscripcionesClient(channel);

                var postRecipe = new seg
                {
                    S = seg
                };
                List<Suscripcion> seguid = new();
               
                using (var call = cliente.TraerSeguidores(postRecipe))
                    while (await call.ResponseStream.MoveNext())
                    {
                        var currentRecipe = call.ResponseStream.Current;
                        seguid.Add(currentRecipe);
                    }
                response = JsonConvert.SerializeObject(seguid);
            }
            catch (RpcException rpcEx)
            {
                // Handle gRPC-specific exceptions
                Console.WriteLine($"gRPC Exception: {rpcEx.Status}");
                return rpcEx.Status.ToString();
            }
            catch (Exception e)
            {
                // Handle general exceptions
                Console.WriteLine($"Exception: {e.Message}");
                return e.Message + e.StackTrace;
            }

            return response;
        }


    }

}
