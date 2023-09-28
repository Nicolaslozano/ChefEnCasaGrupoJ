using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace KafkaConsumerApp.Models
{
    public class Comen
    {
        [Key]
        public int idcomentarios { get; set; }
        public int? recet { get; set; }
        public string? usuario_comen { get; set; }
        public string? comentario { get; set; }
    }


}