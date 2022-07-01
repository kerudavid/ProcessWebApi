using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessWebApi.Models.ResponseModels
{
    /// <summary>
    /// Respuesta de la accion insertar Almuerzo
    /// </summary>
    public class CateringResponseModel
    {
        public string MessageError { get; set; }
        public bool Success { get; set; }
    }
}