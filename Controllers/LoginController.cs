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
                using (var db = new BCMProdEntities())
                {
                    //Validar el estado del usuario, para proceder al login
                    var userInfo = (from Usuario in db.Usuario
                                    where Usuario.usuario1.CompareTo(usuarioLoginModel.usuario)==0
                                    select Usuario).FirstOrDefault();
                                                        //where Usuario.usuario1.Equals(usuarioLoginModel.usuario)
                                                        //where StringComparison.Ordinal, Usuario.Clave, string2,compareResult ==0
                                                        //where Usuario.clave.Equals(usuarioLoginModel.password,StringComparison.CurrentCulture)
                    if (userInfo!=null)
                    {
                        if (String.Compare(userInfo.clave, usuarioLoginModel.password, StringComparison.CurrentCulture) != 0)
                        {
                            responseModel.Success = false;
                            responseModel.MessageError = "Nombre de usuario o Password Incorrectos";
                            return responseModel;
                        }
                        if (userInfo.estado != "A")
                        {
                            responseModel.Success = false;
                            responseModel.MessageError = "Usuario Incativo";
                            return responseModel;
                        }
                        if (userInfo.nivel > 2)
                        {
                            responseModel.Success = false;
                            responseModel.MessageError = "Este usuario no tiene permisos de acceso";
                            return responseModel;
                        }
                        responseModel.Success = true;
                        responseModel.IdUsuario = userInfo.id_usuario;
                        responseModel.UserLevel = userInfo.nivel;
                        responseModel.Sucursal = userInfo.sucursal;
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