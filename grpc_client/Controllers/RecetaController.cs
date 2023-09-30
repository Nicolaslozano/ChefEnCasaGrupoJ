using Confluent.Kafka;
using grpc_client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Net.Client;
using static Google.Rpc.Context.AttributeContext.Types;
using System.Net;


namespace grpc_client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaController
    {

        private readonly IProducer<string, string> kafkaProducer;
        private readonly ConsumerConfig kafkaConsumerConfig;


        public RecetaController()
        {
            // Configura el productor de Kafka
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092" // Reemplaza con la dirección de tu servidor Kafka

            };

            kafkaProducer = new ProducerBuilder<string, string>(config).Build();
        }


        [HttpPost]
        public string PostReceta(RecetaClass receta)
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
                    Titulo = receta.titulo,
                    Descripcion = receta.descripcion,
                    TiempoPreparacion = receta.tiempoPreparacion,
                    Ingredientes = receta.ingredientes,
                    Pasos = receta.pasos,
                    UsuarioUser = receta.usuario_user,
                    NombreCategoria = receta.nombreCategoria1,
                    RecetaPopular = receta.recetaPopular,
                    Puntuacion = receta.puntuacion,
                    CantPuntuacion = receta.cantPuntuacion,
                };
                foreach (var stringUrl in receta.url_fotos)
                {
                    postRecipe.UrlFotos.Add(stringUrl);
                }


                // Serializar la información para enviar a Kafka
                var kafkaData = new
                {
                    NombreUsuario = receta.usuario_user,
                    TituloReceta = receta.titulo,
                    UrlPrimeraFoto = receta.url_fotos.FirstOrDefault()
                };

                // Serializar la información para enviar a Kafka como mensaje JSON
                string kafkaJson = JsonConvert.SerializeObject(kafkaData);

                // se envia la información a Kafka en el topic "Novedades"
                kafkaProducer.ProduceAsync("Novedades", new Message<string, string>
                {
                    Key = null, // Puedes proporcionar una clave si es relevante
                    Value = kafkaJson
                });

                //guardar la receta en la base de datos
                var recetaResponse = cliente.AltaReceta(postRecipe);
                response = JsonConvert.SerializeObject(recetaResponse);
            }
            catch (Exception e)
            {
                response = e.Message + e.StackTrace;
            }

            return response;
        }

        [HttpGet]
        [Route("GetRecetas")]
        public async Task<string> GetRecetasAsync()
        {
            string response;
            try
            {
                // This switch must be set before creating the GrpcChannel/HttpClient.
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Recetas.RecetasClient(channel);

                List<Receta> recetas = new();
                using (var call = cliente.TraerRecetas(new NuloReceta()))
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




        [HttpGet]
        [Route("GetRecetasPopulares")]
        public async Task<string> GetRecetasPopularesAsync()
        {
            string response;
            try
            {
                // This switch must be set before creating the GrpcChannel/HttpClient.
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Recetas.RecetasClient(channel);

                List<Receta> recetas = new();
                using (var call = cliente.TraerRecetasPopulares(new NuloReceta()))
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



        [HttpPut]
        [Route("EditarReceta")]
        public string EditarRecetas(RecetaClass receta)
        {
            string response;
            try
            {
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Recetas.RecetasClient(channel);



                var editRecipe = new RecetaEditar
                {
                    Idreceta = receta.idreceta,
                    Titulo = receta.titulo,
                    Descripcion = receta.descripcion,
                    TiempoPreparacion = receta.tiempoPreparacion,
                    Ingredientes = receta.ingredientes,
                    Pasos = receta.pasos,
                    NombreCategoria = receta.nombreCategoria1,
                };
                foreach (var stringUrl in receta.url_fotos)
                {
                    editRecipe.UrlFotos.Add(stringUrl);
                }
                var productoResponse = cliente.EditarReceta(editRecipe);
                response = JsonConvert.SerializeObject(productoResponse);

            }
            catch (Exception e)
            {
                response = e.Message + e.StackTrace;
            }

            return response;
        }

        [HttpGet]
        [Route("receta")]
        public string GetRecetasById(int idreceta)
        {
            string response;
            try
            {
                // This switch must be set before creating the GrpcChannel/HttpClient.
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Recetas.RecetasClient(channel);

                var postIdReceta = new RecetaId
                {
                    Idreceta = idreceta
                };
                response = JsonConvert.SerializeObject(cliente.TraerRecetaPorId(postIdReceta));
            }
            catch (Exception e)
            {
                return e.Message + e.StackTrace;
            }

            return response;
        }


        [HttpGet]
        [Route("GetRecetasToUser")]
        public async Task<string> GetRecetasToUserAsync(string usu)
        {
            string response;
            try
            {
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Recetas.RecetasClient(channel);

                var postRecipe = new Usuariolog
                {
                    Usu = usu
                };

                List<Receta> recetas = new();
                using (var call = cliente.TraerRecetasPorUsuario(postRecipe))
                    while (await call.ResponseStream.MoveNext())
                    {
                        var currentRecipe = call.ResponseStream.Current;
                        recetas.Add(currentRecipe);
                    }
                response = JsonConvert.SerializeObject(recetas);
            }
            catch (Exception e)
            {
                response = e.Message + e.StackTrace;
            }

            return response;
        }



        [HttpGet]
        [Route("GetRecetasToCategoria")]
        public async Task<string> GetRecetasToCategoriaAsync(string usu)
        {
            string response;
            try
            {
                // This switch must be set before creating the GrpcChannel/HttpClient.
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Recetas.RecetasClient(channel);

                var postRecipe = new Usuariolog
                {
                    Usu = usu
                };
                List<Receta> recetas = new();
                using (var call = cliente.TraerRecetasPorCategoria(postRecipe))
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


        [HttpGet]
        [Route("GetRecetasToTitle")]
        public async Task<string> GetRecetasToTitleAsync(string usu)
        {
            string response;
            try
            {
                // This switch must be set before creating the GrpcChannel/HttpClient.
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Recetas.RecetasClient(channel);

                var postRecipe = new Usuariolog
                {
                    Usu = usu
                };
                List<Receta> recetas = new();
                using (var call = cliente.TraerRecetasPorTitulo(postRecipe))
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


        [HttpGet]
        [Route("GetRecetasToTime")]
        public async Task<string> GetRecetasToTimeAsync(int desde, int hasta)
        {
            string response;
            try
            {
                // This switch must be set before creating the GrpcChannel/HttpClient.
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Recetas.RecetasClient(channel);

                var postRecipe = new tiempo
                {
                    Desde = desde,
                    Hasta = hasta,
                };
                List<Receta> recetas = new();
                using (var call = cliente.TraerRecetasPorTiempo(postRecipe))
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

        [HttpGet]
        [Route("GetRecetasToIngredients")]
        public async Task<string> GetRecetasToIngredientsAsync(string usu)
        {
            string response;
            try
            {
                // This switch must be set before creating the GrpcChannel/HttpClient.
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Recetas.RecetasClient(channel);

                var postRecipe = new Usuariolog
                {
                    Usu = usu
                };
                List<Receta> recetas = new();
                using (var call = cliente.TraerRecetasPorIngredientes(postRecipe))
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



        [HttpPost]
        [Route("PostCalificacion")]
        public async Task<IActionResult> PostCalificacion(int idRec, string nomusu, int califi)
        {
            string response;
            try
            {
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Recetas.RecetasClient(channel);


                var postIdReceta = new RecetaId
                {
                    Idreceta = idRec
                };

                var recetaResponse = await cliente.TraerRecetaPorIdAsync(postIdReceta);

                // Comparar el string 
                if (!nomusu.Equals(recetaResponse.UsuarioUser))
                {
                    // Enviar mensaje al topic "PopularidadReceta" de Kafka
                    var popularidadMessage = new
                    {
                        IdReceta = idRec,
                        Puntaje = califi
                    };

                    var postRecipe = new Puntua
                    {
                        Idreceta = idRec,
                        Puntuacion = califi,
                    };

                    var popularidadMessageJson = JsonConvert.SerializeObject(popularidadMessage);
                    await kafkaProducer.ProduceAsync("PopularidadReceta", new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = popularidadMessageJson });
                    cliente.AgregarPuntuacion(postRecipe);
                    return new OkObjectResult("La calificación fue exitosa");
                }
                else
                {
                    return new OkObjectResult("No puedes calificar tu propia receta");
                }

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message + e.StackTrace);
            }

        }

        [HttpGet]
        [Route("GetPromedioCalificacion")]
        public async Task<IActionResult> GetPromedioCalificacionAsync(int idRec)
        {

            try
            {
                // This switch must be set before creating the GrpcChannel/HttpClient.
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Recetas.RecetasClient(channel);

                var postRecipe = new RecetaId
                {
                    Idreceta = idRec
                };
                var promed = cliente.TraerPromedioPuntuacion(postRecipe);
                var promedio = promed.Promedio;
                return new JsonResult(promed);
            }
            catch (Exception e)
            {
                return new ObjectResult(new { error = e.Message + e.StackTrace })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }





        [HttpGet]
        [Route("GetNovedades")]
        public async Task<string> GetNovedadesAsync()
        {
            string response;
            try
            {
                // This switch must be set before creating the GrpcChannel/HttpClient.
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Recetas.RecetasClient(channel);

                // Crear un consumidor de Kafka
                var conf = new ConsumerConfig
                {
                    BootstrapServers = "localhost:9092",
                    GroupId = "my-consumer-group",
                    AutoOffsetReset = AutoOffsetReset.Earliest // Cambia esto por la dirección de tu servidor Kafka
                };

                List<Receta> recetas = new();

                using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
                {
                    c.Subscribe("Novedades");

                    // Configurar un temporizador para que la operación termine después de 2 segundos
                    //var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                    //var cancellationToken = cancellationTokenSource.Token;

                    while (true)
                    {
                        
                        var cr = c.Consume(TimeSpan.FromMilliseconds(10000));

                        if (cr == null)
                        {
                            break; // Sal del bucle si no hay más mensajes
                        }

                        try
                        {
                            var kafkaData = JsonConvert.DeserializeObject<KafkaData>(cr.Value);

                            // Continuar con el procesamiento de kafkaData
                            string TituloReceta = kafkaData.TituloReceta;
                            string uuusu = kafkaData.NombreUsuario;

                            var postRecipe = new doble
                            {
                                Tit = TituloReceta,
                                Uer = uuusu
                            };
                            recetas.Add(cliente.TraerRecetasPorTituloyUsuario(postRecipe));

                        }
                        catch (JsonSerializationException jsonEx)
                        {
                            // Captura las excepciones de deserialización JSON
                            Console.WriteLine("Error de deserialización JSON: " + jsonEx.Message);
                            Console.WriteLine("Mensaje Kafka recibido: " + cr.Value);
                        }
                        catch (Exception ex)
                        {
                            // Captura otras excepciones
                            Console.WriteLine("Error inesperado: " + ex.Message);
                            Console.WriteLine("Mensaje Kafka recibido: " + cr.Value);
                        }
                    }
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