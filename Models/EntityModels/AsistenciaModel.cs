using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessWebApi.Models.EntityModels
{
    /// <summary>
    /// Modelo que refleja los campos de la tabla asistencia
    /// </summary>
    public class AsistenciaModel
    {
        // int IdAsistencia { get; set; }
        public int CodigoCliente { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime HoraEntrada { get; set; }
        public string Observaciones { get; set; }
        // La sucursal viene de la enfermera
        public int Sucursal { get; set; }
        public int Usuario { get; set; }
        public DateTime FechaMod { get; set; }


    }
}