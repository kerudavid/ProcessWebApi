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
                using (var db = new ClubMemoriaDBEntities())
                {
                    var clientesInfo = (from Cliente in db.Cliente
                                        select Cliente).ToList();
                    var clientesInfos = (from Cliente in db.Cliente
                                         where Cliente.IdCliente == 1
                                         select Cliente).FirstOrDefault();

                    if (clientesInfo.Count > 0)
                    {
                        
                        foreach (var item in clientesInfo)
                        {
                            ClienteModel clienteModel = new ClienteModel();
                            clienteModel.IdCliente = item.IdCliente;
                            clienteModel.Nombre = item.Nombre;
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