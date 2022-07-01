using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessWebApi.Models.EntityModels
{
    public class ColaboradorModel
    {
        public int IdColaborador { get; set; }
        public string NombreColaborador { get; set; }
        public int sucursal { get; set; }
        public string CIColaborador { get; set; }   
    }
}