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
    
    public partial class Cliente
    {
        public int id_cliente { get; set; }
        public string cedula { get; set; }
        public string nombre { get; set; }
        public string apodo { get; set; }
        public string fecha_ingreso { get; set; }
        public string fecha_free { get; set; }
        public string sexo { get; set; }
        public string estado { get; set; }
        public Nullable<int> aula { get; set; }
        public Nullable<int> dia_nacim { get; set; }
        public Nullable<int> mes_nacim { get; set; }
        public Nullable<int> anio_nacim { get; set; }
        public string telefono { get; set; }
        public string nombre_contacto { get; set; }
        public string parentesco_contacto { get; set; }
        public string telefono_contacto { get; set; }
        public string celular_contacto { get; set; }
        public string encargado_pago { get; set; }
        public string parentesco_pago { get; set; }
        public string telefono_pago { get; set; }
        public string cedula_pago { get; set; }
        public string celular_pago { get; set; }
        public string email_pago { get; set; }
        public string medio_pago { get; set; }
        public string pariente_transp { get; set; }
        public string direccion { get; set; }
        public string toma_transp { get; set; }
        public Nullable<int> id_transportista { get; set; }
        public string retirarse_solo { get; set; }
        public string nombre_factu { get; set; }
        public string cedula_factu { get; set; }
        public string direccion_factu { get; set; }
        public string email_factu { get; set; }
        public Nullable<int> sucursal { get; set; }
        public string observacion { get; set; }
        public string usuario { get; set; }
        public string fecha_mod { get; set; }
    }
}
