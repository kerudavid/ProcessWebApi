using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessWebApi.Models.EntityModels
{
    public class UsuarioLoginModel
    {
        public int IdUsuario { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
    }
}