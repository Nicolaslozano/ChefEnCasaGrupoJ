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
    // GET: api/TraerUsuario
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        [HttpGet]
        public string GetIniciarSesion(string username, string password)
        {
            string response;
            try
            {
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Usuarios.UsuariosClient(channel);

                var user = new Username
                {
                    User = username,
                    Password = password
                };

                var usuario = cliente.TraerUsuario(user);

                response = JsonConvert.SerializeObject(usuario);
            }
            catch (RpcException e)
            {
                response = e.Status.Detail + e.Status.StatusCode;
            }
            catch (Exception e)
            {
                response = e.Message + e.StackTrace;
            }

            return response;
        }


        [HttpPost]
        public string PostUsuario(Usuario user)
        {
            string response;
            try
            {
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Usuarios.UsuariosClient(channel);

                var postUser = new Usuario
                {
                    Nombre = user.Nombre,
                    Email = user.Email,
                    User = user.User,
                    Password = user.Password,
                };

                var usuarioResponse = cliente.AltaUsuario(postUser);
                response = JsonConvert.SerializeObject(usuarioResponse);
            }
            catch (Exception e)
            {
                response = e.Message + e.StackTrace;
            }

            return response;
        }



        [HttpPost]
        [Route("PostSeguidor")]
        public string PostSeguidor (string user, string segui)
        {
            string response;
            try
            {
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Usuarios.UsuariosClient(channel);

                var postRecipeFav = new Seguidores
                {
                    User = user,
                    Segui = segui,
                };

                var suscripcionResponse = cliente.SeguirUsuario(postRecipeFav);
                response = JsonConvert.SerializeObject(suscripcionResponse);
            }
            catch (Exception e)
            {
                response = e.Message + e.StackTrace;
            }

            return response;
        }


        [HttpPost]
        [Route("DeleteSeguidor")]
        public string DeleteSeguidor (string user, string segui)
        {
            string response;
            try
            {
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Usuarios.UsuariosClient(channel);

                var postRecipeFav = new Seguidores
                {
                    User = user,
                    Segui = segui,
                };

                var suscripcionResponse = cliente.EliminarSeguidor(postRecipeFav);
                response = JsonConvert.SerializeObject(suscripcionResponse);
            }
            catch (Exception e)
            {
                response = e.Message + e.StackTrace;
            }

            return response;
        }

        [HttpPost]
        [Route("GetSeguidores")]
        public async Task<string> GetSeguidores(string seg)
        {
            string response;
            try
            {
                // This switch must be set before creating the GrpcChannel/HttpClient.
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new Usuarios.UsuariosClient(channel);

                var postRecipe = new seg
                {
                    S = seg
                };
                List<Listseguidos> ListaSeguidos = new();
                using (var call = cliente.TraerSeguidores(postRecipe))
                    while (await call.ResponseStream.MoveNext())
                    {
                        var currentRecipe = call.ResponseStream.Current;
                        ListaSeguidos.Add(currentRecipe);
                    }
                response = JsonConvert.SerializeObject(ListaSeguidos);
            }
            catch (Exception e)
            {
                return e.Message + e.StackTrace;
            }

            return response;
        }

    }
}
