using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grpc_client.Models
{
    public class RecetaClass
    {
        
        public int idreceta { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int tiempoPreparacion { get; set; }
        public string ingredientes { get; set; }
        public string pasos { get; set; }
        public List<string> url_fotos { get; set; }

        public string usuario_user { get; set; }

        public string nombreCategoria1 { get; set; }
        public int recetaPopular { get; set; }

    }
}




