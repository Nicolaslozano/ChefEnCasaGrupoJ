using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grpc_client.Models
{
    public class KafkaData
    {

        public string NombreUsuario { get; set; }
        public string TituloReceta { get; set; }
        public string UrlPrimeraFoto { get; set; }
    }
}