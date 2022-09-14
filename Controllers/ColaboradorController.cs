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
    [RoutePrefix("api/Colaborador")]
    public class ColaboradorController: ApiController
    {
        [HttpGet]
        public ColaboradorResponseModel GetColaborador()
        {
            ColaboradorResponseModel colaboradorResponseModel = new ColaboradorResponseModel();
            List<ColaboradorModel> colaboradorModelList = new List<ColaboradorModel>();
            try
            {
                using (var db = new BCMProdEntities())
                {
                    var colaboradorInfo = (from Colaborador in db.Colaborador
                                        select Colaborador).ToList();


                    if (colaboradorInfo.Count > 0)
                    {

                        foreach (var item in colaboradorInfo)
                        {
                            ColaboradorModel colaboradorModel = new ColaboradorModel();
                            colaboradorModel.IdColaborador = item.id_colaborador;
                            colaboradorModel.NombreColaborador = item.nombre;
                            colaboradorModel.CIColaborador = item.cedula;
                            colaboradorModelList.Add(colaboradorModel);
                        }
                        colaboradorResponseModel.ListaColab = colaboradorModelList;
                        colaboradorResponseModel.success = true;
                    }
                    else
                    {
                        colaboradorResponseModel.success = false;
                        colaboradorResponseModel.MessageError = "No se encontraron registros de colaboradores";
                    }
                }
            }
            catch (Exception ex)
            {
                colaboradorResponseModel.success = false;
                colaboradorResponseModel.MessageError = ex.Message;
            }
            return colaboradorResponseModel;
        }
    }
}