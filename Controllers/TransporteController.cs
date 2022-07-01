using ProcessWebApi.Data;
using ProcessWebApi.Models.EntityModels;
using ProcessWebApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ProcessWebApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Transporte")]
    public class TransporteController: ApiController
    {
        public static string fechaIngreso = string.Empty;
        private bool validacionInfo(TransporteModel transporteModel)
        {
            bool response = true;
            if (transporteModel.CodigoPersona <= 0)
            {
                response = false;
            }
            if (transporteModel.Fecha == null)
            {
                response = false;
            }
            if (transporteModel.Hora == null)
            {
                response = false;
            }
            if (string.IsNullOrEmpty(transporteModel.NombreUsuario))
            {
                response = false;
            }
            if (transporteModel.Sucursal <= 0)
            {
                response = false;
            }
            if (string.IsNullOrEmpty(transporteModel.EntradaSalida))
            {
                response=false;
            }
            return response;
        }
        /// <summary>
        /// Envia los datos de transporte y retorna un estado de exitoso o fallido
        /// </summary>
        /// <param name="transporteModel"></param>
        /// <returns></returns>
        [HttpPost]
        public TransporteResponseModel Transporte([FromBody] TransporteModel transporteModel)
        {
            TransporteResponseModel transporteResponseModel = new TransporteResponseModel();
            try
            {
                if (validacionInfo(transporteModel))
                {
                    // Aqui viene la insercion a la BD
                    using (var db = new BCMEntities())
                    {
                        var transporteInfo = (from Transporte in db.Transporte
                                              where Transporte.fk_id_cliente == transporteModel.CodigoPersona
                                              && Transporte.entrada_salida==transporteModel.EntradaSalida
                                              select Transporte).ToList();

                        fechaIngreso = transporteModel.Fecha.ToString("dd-MM-yyyy");

                        string fecha = transporteInfo.Where(x => String.Compare(x.fecha, transporteModel.Fecha.ToString("dd-MM-yyyy"), StringComparison.Ordinal) == 0).Select(x => x.fecha).FirstOrDefault();

                        if (transporteInfo.Count > 0 && !string.IsNullOrEmpty(fecha))
                        {
                            transporteResponseModel.Success = false;
                            transporteResponseModel.MessageError = "Este cliente ya había registrado su asistencia";
                            return transporteResponseModel;
                        }
                        Transporte transporte = new Transporte();
                        transporte.fk_id_cliente = transporteModel.CodigoPersona;
                        transporte.tipo_cliente = transporteModel.TipoPersona;
                        transporte.id_transportista = transporteModel.IdTransportista;
                        transporte.fecha = transporteModel.Fecha.ToString("dd-MM-yyyy");
                        transporte.hora = transporteModel.Hora.ToString("HH:mm");
                        transporte.observacion = transporteModel.Observaciones;
                        transporte.sucursal = transporteModel.Sucursal;
                        transporte.usuario = transporteModel.NombreUsuario;
                        transporte.entrada_salida = transporteModel.EntradaSalida;
                        transporte.fecha_mod = transporteModel.FechaMod.ToString("dd-MM-yyyy");
                        db.Transporte.Add(transporte);
                        db.SaveChanges();
                        transporteResponseModel.Success = true;
                    }
                }
                else
                {
                    transporteResponseModel.MessageError = "Error, no hay datos que ingresar";
                    transporteResponseModel.Success = false;
                }
            }
            catch (Exception ex)
            {
                transporteResponseModel.MessageError = ex.Message;
                transporteResponseModel.Success = false;
            }

            return transporteResponseModel;
        }
    }
}