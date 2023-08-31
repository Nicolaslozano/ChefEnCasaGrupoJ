using Grpc.Net.Client;
using grpc_client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace grpc_client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaController
    {
        [HttpPost]
        public string PostReceta(Receta receta)
        {
            string response;
            try
            {
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Recetas.RecetasClient(channel);  

                var postRecipe = new Receta
                {
                    Titulo = receta.Titulo,
                    Descripcion = receta.Descripcion,
                    TiempoPreparacion = receta.TiempoPreparacion,
                    Ingredientes = receta.Ingredientes,
                    Pasos = receta.Pasos,
                };

                var recetaResponse = cliente.AltaReceta(postRecipe);
                response = JsonConvert.SerializeObject(recetaResponse);
            }
            catch (Exception e)
            {
                response = e.Message + e.StackTrace;
            }

            return response;
        }
    }
}
