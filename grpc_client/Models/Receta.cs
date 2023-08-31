using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grpc_client.Models
{
    public class Receta
    {
        
        public int idreceta { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int tiempoPreparacion { get; set; }
        public string ingredientes { get; set; }
        public string pasos { get; set; }


    }
}




