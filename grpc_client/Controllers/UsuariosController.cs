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
using Confluent.Kafka;

namespace grpc.Controllers
{
    // GET: api/TraerUsuario
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly IProducer<string, string> kafkaProducer;


        public UsuariosController()
        {
            // Configura el productor de Kafka
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092" // Reemplaza con la dirección de tu servidor Kafka
            };

            kafkaProducer = new ProducerBuilder<string, string>(config).Build();
        }


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
                    Popular = user.Popular,
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

                kafkaProducer.Produce("PopularidadUsuario", new Message<string, string>
                {
                    Key = segui, // Utiliza el nombre de usuario como clave
                    Value = "1" // El valor es +1
                });


                var suscripcionResponse = cliente.SeguirUsuario(postRecipeFav);
                response = JsonConvert.SerializeObject(suscripcionResponse);
            }
            catch (Exception e)
            {
                response = e.Message + e.StackTrace;
            }

            return response;
        }


        [HttpDelete]
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


                
                kafkaProducer.Produce("PopularidadUsuario", new Message<string, string>
                {
                    Key = segui, 
                    Value = "-1" 
                });


                var suscripcionResponse = cliente.EliminarSeguidor(postRecipeFav);
                response = JsonConvert.SerializeObject(suscripcionResponse);
            }
            catch (Exception e)
            {
                response = e.Message + e.StackTrace;
            }

            return response;
        }

        
    }
}
