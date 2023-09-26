using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grpc_client.Models
{
    public class ComentariosClass
    { 
        public int idcomentarios { get; set; }
        public int recet { get; set; }
        public string usuario_comen { get; set; }
        public string comentario { get; set; }
    }
}