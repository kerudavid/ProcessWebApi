//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProcessWebApi.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Asistencia
    {
        public int id_asistencia { get; set; }
        public int fk_id_cliente { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string observacion { get; set; }
        public int sucursal { get; set; }
        public string usuario { get; set; }
        public string fecha_mod { get; set; }
    }
}
