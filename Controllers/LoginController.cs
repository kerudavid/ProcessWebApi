using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ProcessWebApi.Data;
using ProcessWebApi.Models.EntityModels;
using ProcessWebApi.Models.ResponseModels;
namespace ProcessWebApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Login")]
    public class LoginController: ApiController
    {
        
        [HttpPost]
        public LoginResponseModel Login([FromBody] UsuarioLoginModel usuarioLoginModel)
        {

            LoginResponseModel responseModel = new LoginResponseModel();
            try
            {
                using (var db = new ClubMemoriaDBEntities())
                {
                    //Validar el estado del usuario, para proceder al login
                    var userInfo = (from Usuario in db.Usuario
                                    where Usuario.NombreUsuario.Equals(usuarioLoginModel.usuario)
                                    //where StringComparison.Ordinal, Usuario.Clave, string2,compareResult ==0

                                    where Usuario.Clave.Equals(usuarioLoginModel.password,StringComparison.CurrentCulture)
                                    select Usuario).FirstOrDefault();
                    
                    if (userInfo!=null)
                    {
                        if (userInfo.Estado != "A")
                        {
                            responseModel.Success = false;
                            responseModel.MessageError = "Usuario Incativo";
                            return responseModel;
                        }
                        if (userInfo.Nivel > 2)
                        {
                            responseModel.Success = false;
                            responseModel.MessageError = "Este usuario no tiene permisos de acceso";
                            return responseModel;
                        }
                        responseModel.Success = true;
                        responseModel.IdUsuario = userInfo.IdUsuario;
                        responseModel.UserLevel = userInfo.Nivel;
                        responseModel.Sucursal = userInfo.Sucursal;
                    }
                    else
                    {
                        responseModel.Success = false;
                        responseModel.MessageError = "Nombre de usuario o Password Incorrectos";
                    }
                }

            }
            catch (Exception ex)
            {
                responseModel.MessageError=ex.Message;
                responseModel.Success = false;
            }

            return responseModel;
        }
    }
}