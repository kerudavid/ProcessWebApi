using ProcessWebApi.Data;
using ProcessWebApi.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessWebApi.Models.ResponseModels
{
    public class ClienteResponseModel
    {
        public List<ClienteModel> ListaCliente { get; set; }
        public string MessageError { get; set; }
        public bool success { get; set; }
    }
}