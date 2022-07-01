using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessWebApi.Models.EntityModels
{
    public class ClienteModel
    {
        public int IdCliente { get; set; }
        public int IdTransportista { get; set; }
        public string Nombre { get; set; }
        public string CI { get; set; }
    }
}