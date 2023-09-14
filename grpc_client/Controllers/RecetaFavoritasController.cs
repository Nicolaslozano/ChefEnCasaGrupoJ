using Grpc.Net.Client;
using grpc_client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Grpc.Core;

namespace grpc_client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaFavoritasController
    {
        [HttpPost]
        public string PostRecetaFavorita(RecetaFavClass recetaFav)
        {
            string response;
            try
            {
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new RecetaFav.RecetaFavClient(channel);

                var postRecipeFav = new RecetaFavoritas
                {
                    IdrecetaFavoritas = recetaFav.idrecetaFavoritas,
                    RecetasFavoritascol = recetaFav.recetasFavoritascol,
                    UsuarioUserfav = recetaFav.usuario_userfav,

                };

                var recetaResponse = cliente.AgregarRecetaFav(postRecipeFav);
                response = JsonConvert.SerializeObject(recetaResponse);
            }
            catch (Exception e)
            {
                response = e.Message + e.StackTrace;
            }

            return response;
        }





        [HttpGet]
        [Route("GetRecetasFav")]
        public async Task<string> GetRecetasFavAsync(string nombreUsuario)
        {
            string response;
            try
            {
                // This switch must be set before creating the GrpcChannel/HttpClient.
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new RecetaFav.RecetaFavClient(channel);

                var postRecipe = new Usuariologueado
                {
                    NombreUsuario = nombreUsuario
                };
                List<RecetaFavCompleta> recetas = new();
                using (var call = cliente.TraerRecetasFav(postRecipe))
                    while (await call.ResponseStream.MoveNext())
                    {
                        var currentRecipe = call.ResponseStream.Current;
                        recetas.Add(currentRecipe);
                    }
                response = JsonConvert.SerializeObject(recetas);
            }
            catch (Exception e)
            {
                return e.Message + e.StackTrace;
            }

            return response;
        }


    }
}
