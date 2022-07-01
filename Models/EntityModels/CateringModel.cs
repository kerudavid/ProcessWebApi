using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessWebApi.Models.EntityModels
{
    public class CateringModel
    {
        public int CodigoPersona { get; set; }
        public int CodigoColab { get; set; }
        public string TipoPersona { get; set; }
        public string TipoMenu { get; set; }
        public string CIColaborador { get; set; }
        public string CICliente { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public string Observaciones { get; set; }
        // La sucursal viene de la enfermera
        public int Sucursal { get; set; }
        public int Usuario { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime FechaMod { get; set; }
    }
}