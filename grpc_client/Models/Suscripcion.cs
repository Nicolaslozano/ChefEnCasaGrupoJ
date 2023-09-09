using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grpc_client.Models
{
    public class SuscripcionClass
    {
        public int idsuscripcion { get; set; }
        public string followed_user { get; set; }
        public string my_user { get; set; }
    }
}
