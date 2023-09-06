using Grpc.Net.Client;
using grpc_client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Grpc.Core;

namespace grpc_client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaController
    {
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
                    UsuarioIdusuario = receta.usuario_idusuario,
                    NombreCategoria = receta.nombreCategoria1,
                    

            };
                foreach (var stringUrl in receta.url_fotos)
                {
                    postRecipe.UrlFotos.Add(stringUrl);
                }

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
        [Route("EditarReceta/")]
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


        [HttpPost]
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
    }
}

