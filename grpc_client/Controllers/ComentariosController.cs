using Grpc.Net.Client;
using grpc_client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Grpc.Core;

namespace grpc_client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController
    {
        [HttpPost]
        public string PostComentarios(ComentariosClass comenta)
        {
            string response;
            try
            {
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Comentarios1.Comentarios1Client(channel);

                var postRecipeFav = new Comentarios
                {
                    Idcomentarios = comenta.idcomentarios,
                    Recet = comenta.recet,
                    UsuarioComen = comenta.usuario_comen,
                    Comentario = comenta.comentario,

                };

                var recetaResponse = cliente.AgregarComentario(postRecipeFav);
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
