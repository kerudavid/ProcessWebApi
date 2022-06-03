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
    [AllowAnonymous]
    [RoutePrefix("api/Asistencia")]
    public class AsistenciaController : ApiController
    {
        private bool validacionInfo(AsistenciaModel asistenciaModel)
        {
            bool response = true;
            if (asistenciaModel.CodigoCliente <= 0)
            {
                response = false;
            }
            if (asistenciaModel.Fecha == null)
            {
                response = false;
            }
            if (asistenciaModel.HoraEntrada == null)
            {
                response = false;
            }
            if (asistenciaModel.Usuario <= 0)
            {
                response = false;
            }
            if (asistenciaModel.Sucursal <= 0)
            {
                response = false;
            }
            return response;
        }
        /// <summary>
        /// Envia los datos de asistencia y retorna un estado de exitoso o fallido
        /// </summary>
        /// <param name="asistenciaModel"></param>
        /// <returns></returns>
        [HttpPost]
        public AsistenciaResponseModel InsertarAsistencia([FromBody] AsistenciaModel asistenciaModel)
        {
            AsistenciaResponseModel asistenciaResponseModel = new AsistenciaResponseModel();
            try
            {
                if (validacionInfo(asistenciaModel))
                {
                    // Aqui viene la insercion a la BD
                    using (var db = new ClubMemoriaDBEntities())
                    {
                        var asistenciaInfo = (from Asistencia in db.Asistencia
                                              where Asistencia.IdCliente == asistenciaModel.CodigoCliente
                                              where Asistencia.Fecha.Equals(asistenciaModel.Fecha)
                                              select Asistencia).FirstOrDefault();
                        if (asistenciaInfo != null)
                        {
                            asistenciaResponseModel.Success = false;
                            asistenciaResponseModel.MessageError = "Este cliente ya había registrado su asistencia";
                        }
                        else
                        {
                            Asistencia asistencia = new Asistencia();
                            asistencia.IdCliente = asistenciaModel.CodigoCliente;
                            asistencia.Fecha = asistenciaModel.Fecha;
                            asistencia.FechaModificacion = asistenciaModel.FechaMod;
                            asistencia.HoraIngreso = asistenciaModel.HoraEntrada;
                            asistencia.Observaciones = asistenciaModel.Observaciones;
                            asistencia.Sucursal = asistenciaModel.Sucursal;
                            asistencia.IdUsuario = asistenciaModel.Usuario;
                            db.Asistencia.Add(asistencia);
                            db.SaveChanges();
                            asistenciaResponseModel.Success = true;
                        }
                    }
                }
                else
                {
                    asistenciaResponseModel.MessageError = "Error, no hay datos que ingresar";
                    asistenciaResponseModel.Success = false;
                }
            }
            catch (Exception ex)
            {
                asistenciaResponseModel.MessageError = ex.Message;
                asistenciaResponseModel.Success = false;
            }

            return asistenciaResponseModel;
        }

        
    }
}