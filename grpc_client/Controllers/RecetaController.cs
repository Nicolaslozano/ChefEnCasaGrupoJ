﻿using Confluent.Kafka;
using grpc_client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace grpc_client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaController
    {

        private readonly IProducer<string, string> kafkaProducer;


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
                var recetaporid = cliente.TraerRecetaPorId(postIdReceta);
                response = JsonConvert.SerializeObject(recetaporid);
            }
            catch (Exception e)
            {
                return e.Message + e.StackTrace;
            }

            return response;
        }


        [HttpGet]
        [Route("GetRecetasToUser")]
        public async Task<string> GetRecetasToUserAsync(string usu) {
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
        public async Task<string> GetRecetasToTimeAsync(int desde , int hasta)
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
    }
}

