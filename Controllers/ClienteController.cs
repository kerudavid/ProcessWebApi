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
    [RoutePrefix("api/Cliente")]
    public class ClienteController : ApiController
    {
        [HttpGet]
        public ClienteResponseModel GetUsuario()
        {
            ClienteResponseModel cliente = new ClienteResponseModel();
            List<ClienteModel> clienteModelList = new List<ClienteModel>();
            try
            {
                using (var db = new BCMProdEntities())
                {
                    var clientesInfo = (from Cliente in db.Cliente
                                        where Cliente.estado!="I"
                                        select Cliente).ToList();
                    /*var clientesInfos = (from Cliente in db.Cliente
                                         where Cliente.id_cliente == 1
                                         select Cliente).FirstOrDefault();*/

                    if (clientesInfo.Count > 0)
                    {
                        
                        foreach (var item in clientesInfo)
                        {
                            ClienteModel clienteModel = new ClienteModel();
                            clienteModel.IdCliente = item.id_cliente;
                            clienteModel.Nombre = item.nombre;
                            clienteModel.CI = item.cedula;
                            clienteModel.IdTransportista = Int32.Parse(item.id_transportista.ToString());//Pasar int? a int
                            clienteModelList.Add(clienteModel);
                        }
                        cliente.ListaCliente = clienteModelList;
                        cliente.success = true;
                    }
                    else
                    {
                        cliente.success = false;
                        cliente.MessageError = "No se encontraron registros de clientes";
                    }
                }
            }
            catch (Exception ex)
            {
                cliente.success = false;
                cliente.MessageError = ex.Message;
            }
            return cliente;
        }
    }
}