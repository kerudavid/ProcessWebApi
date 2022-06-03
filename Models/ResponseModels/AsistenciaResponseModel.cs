using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessWebApi.Models.ResponseModels
{
    /// <summary>
    /// Respuesta de la accion insertar Asistencia
    /// </summary>
    public class AsistenciaResponseModel
    {

        // La sucursal viene de la enfermera
        public string MessageError { get; set; }
        public bool Success { get; set; }
    }
}