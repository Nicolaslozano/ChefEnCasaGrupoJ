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
                    UsuarioIdusuario = recetaFav.usuario_idusuario,

            };

                var recetaResponse = cliente.AgregarRecetaFav (postRecipeFav);
                response = JsonConvert.SerializeObject(recetaResponse);
            }
            catch (Exception e)
            {
                response = e.Message + e.StackTrace;
            }

            return response;
        }





        [HttpPost]
        [Route("GetRecetasFav")]
        public async Task<string> GetRecetasFavAsync(int idusuario)
        {
            string response;
            try
            {
                // This switch must be set before creating the GrpcChannel/HttpClient.
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new RecetaFav.RecetaFavClient(channel);

                var postRecipe = new UsuarioLogueado
                {
                    UsuarioIdusuario = idusuario
                };
                var call = cliente.TraerRecetasFav(postRecipe);
                response = JsonConvert.SerializeObject(call);
            }
            catch (Exception e)
            {
                return e.Message + e.StackTrace;
            }

            return response;
        }


    }
}
