using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ProcessWebApi.Models.ResponseModels;
using ProcessWebApi.Models.EntityModels;
using ProcessWebApi.Data;

namespace ProcessWebApi.Controllers
{
    /// <summary>
    /// Poner el routePrefix al igual que el nombre del controlador, poner nombres de controladores diferentes.
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/Catering")]
    public class CateringController: ApiController
    {
        public static string fechaIngreso = string.Empty;
        private bool validacionInfo(CateringModel almuerzoModel)
        {
            bool response = true;
            if (almuerzoModel.CodigoPersona <= 0)
            {
                response = false;
            }
            if (almuerzoModel.Fecha == null)
            {
                response = false;
            }
            if (almuerzoModel.Hora == null)
            {
                response = false;
            }
            if (string.IsNullOrEmpty(almuerzoModel.NombreUsuario))
            {
                response = false;
            }
            if (almuerzoModel.Sucursal <= 0)
            {
                response = false;
            }
            return response;
        }
        /// <summary>
        /// Envia los datos de asistencia y retorna un estado de exitoso o fallido
        /// </summary>
        /// <param name="almuerzoModel"></param>
        /// <returns></returns>
        [HttpPost]
        public CateringResponseModel Almuerzo([FromBody] CateringModel almuerzoModel)
        {
            CateringResponseModel almuerzoResponseModel = new CateringResponseModel();
            try
            {
                if (validacionInfo(almuerzoModel))
                {
                    // Aqui viene la insercion a la BD
                    using (var db = new BCMEntities())
                    {
                        Catering catering = new Catering();
                        catering.fk_id_cliente = almuerzoModel.CodigoPersona;
                        catering.tipo_cliente = almuerzoModel.TipoPersona;
                        catering.tipo_menu = almuerzoModel.TipoMenu;
                        catering.fecha = almuerzoModel.Fecha.ToString("dd/MM/yyyy");
                        catering.hora = almuerzoModel.Hora.ToString("HH:mm");
                        catering.observacion = almuerzoModel.Observaciones;
                        catering.sucursal = almuerzoModel.Sucursal;
                        catering.usuario = almuerzoModel.NombreUsuario;
                        catering.fecha_mod = almuerzoModel.FechaMod.ToString("dd/MM/yyyy");
                        db.Catering.Add(catering);
                        db.SaveChanges();
                        almuerzoResponseModel.Success = true;                     
                    }
                }
                else
                {
                    almuerzoResponseModel.MessageError = "Error, no hay datos que ingresar";
                    almuerzoResponseModel.Success = false;
                }
            }
            catch (Exception ex)
            {
                almuerzoResponseModel.MessageError = ex.Message;
                almuerzoResponseModel.Success = false;
            }

            return almuerzoResponseModel;
        }
    }
}