//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Erp.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class vwro_EmpleadoAnticipoBeneficio
    {
        public int IdEmpresa { get; set; }
        public decimal IdAnticipo { get; set; }
        public System.DateTime FechaDesde { get; set; }
        public System.DateTime FechaHasta { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public double Valor { get; set; }
        public string ru_descripcion { get; set; }
        public string pe_nombreCompleto { get; set; }
        public bool Estado { get; set; }
    }
}
