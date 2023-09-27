using Grpc.Net.Client;
using grpc_client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Grpc.Core;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace grpc_client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaFavoritasController
    {

        private readonly IProducer<string, string> kafkaProducer;


        public RecetaFavoritasController()
        {
            // Configura el productor de Kafka
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092" // Reemplaza con la dirección de tu servidor Kafka
            };

            kafkaProducer = new ProducerBuilder<string, string>(config).Build();
        }



        [HttpPost]
        public async Task<IActionResult> PostRecetaFavorita(RecetaFavClass recetaFav)
        {
            string response;
            bool encontrada = false;
            try
            {
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new RecetaFav.RecetaFavClient(channel);
                var cliente2 = new Recetas.RecetasClient(channel);

                var postRecipe = new Usuariologueado
                {
                    NombreUsuario = recetaFav.usuario_userfav
                };
                
                using (var call = cliente.TraerRecetasFav(postRecipe))
                {
                    await foreach (var recetaFavCompleta in call.ResponseStream.ReadAllAsync())
                    {
                        if (recetaFav.recetasFavoritascol.Equals(recetaFavCompleta.Idreceta))
                        {
                            encontrada = true;
                            break;
                        }
                    }
                }
                if (!encontrada)
                {

                    var postIdReceta = new RecetaId
                    {
                        Idreceta = recetaFav.recetasFavoritascol
                    };

                    var recetaResponse = await cliente2.TraerRecetaPorIdAsync(postIdReceta);

                    if (!recetaFav.usuario_userfav.Equals(recetaResponse.UsuarioUser))
                    {               
                        var postRecipeFav = new RecetaFavoritas
                        {
                            IdrecetaFavoritas = recetaFav.idrecetaFavoritas,
                            RecetasFavoritascol = recetaFav.recetasFavoritascol,
                            UsuarioUserfav = recetaFav.usuario_userfav,

                        };

                        var recetaResponse3 = cliente.AgregarRecetaFav(postRecipeFav);
                        response = JsonConvert.SerializeObject(recetaResponse3);

                        var popularidadMessage = new
                        {
                            IdReceta = recetaFav.recetasFavoritascol,
                            Puntaje = 1
                        };

                        var popularidadMessageJson = JsonConvert.SerializeObject(popularidadMessage);
                        await kafkaProducer.ProduceAsync("PopularidadReceta", new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = popularidadMessageJson });
                        return new OkObjectResult("Se agrego a favoritos la receta");

                    }
                    else
                    {
                        return new OkObjectResult("No puedes agregar a favoritos tu propia receta");
                    }
                }
                else
                {
                    return new OkObjectResult("Ya lo tenes agregado a favoritos");
                }
                
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message + e.StackTrace);
            }

        }


        [HttpDelete]
        [Route("DeleteRecetaFav")]
        public async Task<string> DeleteRecetaFav (int rec, string us)
        {
            string response;
            bool encontrada = false;
            try
            {
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new RecetaFav.RecetaFavClient(channel);

                var postRecipe = new Usuariologueado
                {
                    NombreUsuario = us
                };

                using (var call = cliente.TraerRecetasFav(postRecipe))
                {
                    await foreach (var recetaFavCompleta in call.ResponseStream.ReadAllAsync())
                    {
                        if (rec.Equals(recetaFavCompleta.Idreceta))
                        {
                            encontrada = true;
                            break;
                        }
                    }
                }
                if (encontrada)
                {

                    var postRecipeFav = new SeguiRece
                    {
                        Rec = rec,
                        Us = us,
                    };


                    var popularidadMessage = new
                    {
                        IdReceta = rec,
                        Puntaje = -1
                    };

                    var popularidadMessageJson = JsonConvert.SerializeObject(popularidadMessage);
                    await kafkaProducer.ProduceAsync("PopularidadReceta", new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = popularidadMessageJson });

                    var suscripcionResponse = cliente.EliminarRecetaFav(postRecipeFav);
                    response = JsonConvert.SerializeObject(suscripcionResponse);
                }
                else
                {
                    return ("No se encontro la receta que quiere eliminar de sus favoritos");
                }
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
