using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace KafkaConsumerApp.Models
{
    public class Rece
    {
        [Key]
        public int idreceta { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int tiempoPreparacion { get; set; }
        public string ingredientes { get; set; }
        public string pasos { get; set; }
        public string? url_foto1 { get; set; }
        public string? url_foto2 { get; set; }
        public string? url_foto3 { get; set; }
        public string? url_foto4 { get; set; }
        public string? url_foto5 { get; set; }
        public string? usuario_user { get; set; }
        public string? nombreCategoria1 { get; set; }
        public int? recetaPopular { get; set; }


    }


}