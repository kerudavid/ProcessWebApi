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
        public static string fechaIngreso = string.Empty;
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
            if (string.IsNullOrEmpty(asistenciaModel.NombreUsuario))
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
                    using (var db = new BCMProdEntities())
                    {
                        var asistenciaInfo = (from Asistencia in db.Asistencia
                                              where Asistencia.fk_id_cliente == asistenciaModel.CodigoCliente
                                              select Asistencia).ToList();

                        fechaIngreso = asistenciaModel.Fecha.ToString("dd/MM/yyyy");

                        string fecha = asistenciaInfo.Where(x => String.Compare(x.fecha, asistenciaModel.Fecha.ToString("dd/MM/yyyy"), StringComparison.Ordinal) == 0).Select(x => x.fecha).FirstOrDefault();
                        
                        if (asistenciaInfo.Count > 0 && !string.IsNullOrEmpty(fecha))
                        {                       
                            asistenciaResponseModel.Success = false;
                            asistenciaResponseModel.MessageError = "Este cliente ya había registrado su asistencia";                                                  
                        }
                        else
                        {
                            Asistencia asistencia = new Asistencia();
                            asistencia.fk_id_cliente = asistenciaModel.CodigoCliente;
                            asistencia.fecha = asistenciaModel.Fecha.ToString("dd/MM/yyyy");
                            asistencia.fecha_mod = asistenciaModel.FechaMod.ToString("dd/MM/yyyy");
                            asistencia.hora = asistenciaModel.HoraEntrada.ToString("HH:mm");
                            asistencia.observacion = asistenciaModel.Observaciones;
                            asistencia.sucursal = asistenciaModel.Sucursal;
                            asistencia.usuario = asistenciaModel.NombreUsuario;
                            db.Asistencia.Add(asistencia);
                            db.SaveChanges();
                            asistenciaResponseModel.Success = true;
                        }

                        var planInfo = (from Plan in db.Planes
                                        where Plan.fk_id_cliente == asistenciaModel.CodigoCliente
                                        && Plan.estado == "VIGENTE"
                                        select Plan).FirstOrDefault();
                        
                        if (planInfo!=null){
                            
                            var calendarioInfo = (
                                from Calendario in db.Calendario
                                where Calendario.fk_id_plan== planInfo.id_plan
                                && Calendario.fecha== fechaIngreso
                                && Calendario.estado== "R"
                                select Calendario).FirstOrDefault();
                            
                            if (calendarioInfo != null)
                            {
                                calendarioInfo.estado = "C";
                                calendarioInfo.fecha_mod = fechaIngreso;
                                db.SaveChanges();
                            }
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