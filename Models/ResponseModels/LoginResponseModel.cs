using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessWebApi.Models.ResponseModels
{
    public class LoginResponseModel
    {
        public string MessageError { get; set; }
        public bool Success { get; set; }
        public int UserLevel { get; set; }
        public int Sucursal { get; set; }
        public int IdUsuario { get; set; }
    }
}