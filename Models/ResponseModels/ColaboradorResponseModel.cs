using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProcessWebApi.Data;
using ProcessWebApi.Models.EntityModels;



namespace ProcessWebApi.Models.ResponseModels
{
    public class ColaboradorResponseModel
    {
        public List<ColaboradorModel> ListaColab { get; set; }
        public string MessageError { get; set; }
        public bool success { get; set; }
    }
}