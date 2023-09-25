using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace KafkaConsumerApp.Models
{
    public class User
    {
        [Key]
        public int idusuario { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public int popular { get; set; }

    }


}