using Grpc.Net.Client;
using grpc_client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Grpc.Core;
using Confluent.Kafka;
using System;
using System.Text;
using System.Threading.Tasks;

namespace grpc_client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController
    {

        private readonly IProducer<string, string> kafkaProducer;


        public ComentariosController()
        {
            // Configura el productor de Kafka
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092" // dirección de tu servidor Kafka
            };

            kafkaProducer = new ProducerBuilder<string, string>(config).Build();
        }


        [HttpPost]
        public async Task<IActionResult> PostComentarios(ComentariosClass comenta)
        {
            
            try
            {
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Comentarios1.Comentarios1Client(channel);
                var cliente2 = new Recetas.RecetasClient(channel);

                // Enviar comentario al topic "Comentarios" de Kafka
                var comentarioMessage = new
                {
                    Usuario = comenta.usuario_comen,
                    Receta = comenta.recet,
                    Comentario = comenta.comentario
                };
                
                var comentarioMessageJson = JsonConvert.SerializeObject(comentarioMessage);
                await kafkaProducer.ProduceAsync("Comentarios", new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = comentarioMessageJson });

                var postIdReceta = new RecetaId
                {
                    Idreceta = comenta.recet
                };


                var recetaResponse = await cliente2.TraerRecetaPorIdAsync(postIdReceta);

                
                // Comparar el string con comenta.usuario_comen
                 if (!comenta.usuario_comen.Equals(recetaResponse.UsuarioUser))
                {
                    // Enviar mensaje al topic "PopularidadReceta" de Kafka
                    var popularidadMessage = new
                    {
                        IdReceta = comenta.recet,
                        Puntaje = 1
                    };

                    var popularidadMessageJson = JsonConvert.SerializeObject(popularidadMessage);
                    await kafkaProducer.ProduceAsync("PopularidadReceta", new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = popularidadMessageJson });
                }

                return new OkResult();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message + e.StackTrace);
            }
      
        }


        [HttpGet]
        [Route("GetComentariosToReceta")]
        public async Task<string> GetComentariosToRecetaAsync(int reid)
        {
            string response;
            try
            {
                // This switch must be set before creating the GrpcChannel/HttpClient.
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Comentarios1.Comentarios1Client(channel);
                
                var postRecipe = new Rid
                {
                    Reid = reid
                };
                List<Comentarios> comentarios = new();
                using (var call = cliente.TraerComentariosPorIdReceta(postRecipe))
                    while (await call.ResponseStream.MoveNext())
                    {
                        var currentRecipe = call.ResponseStream.Current;
                        comentarios.Add(currentRecipe);
                    }
                response = JsonConvert.SerializeObject(comentarios);
            }
            catch (Exception e)
            {
                return e.Message + e.StackTrace;
            }

            return response;
        }


    }
}
