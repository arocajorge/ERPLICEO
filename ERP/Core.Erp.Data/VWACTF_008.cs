//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Erp.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class VWACTF_008
    {
        public int IdEmpresa { get; set; }
        public int IdActivoFijo { get; set; }
        public string Af_Nombre { get; set; }
        public string Af_observacion { get; set; }
        public string Estado { get; set; }
        public System.DateTime Af_fecha_compra { get; set; }
        public double Af_costo_compra { get; set; }
        public int Af_Vida_Util { get; set; }
        public decimal IdEmpleadoEncargado { get; set; }
        public decimal IdEmpleadoCustodio { get; set; }
        public string EmpleadoEncargado { get; set; }
        public string EmpleadoCustodio { get; set; }
        public decimal IdDepartamento { get; set; }
        public int IdSucursal { get; set; }
        public string Su_Descripcion { get; set; }
        public string NomDepartamento { get; set; }
        public int Cantidad { get; set; }
        public string NomCategoria { get; set; }
        public string NomTipo { get; set; }
        public string NomArea { get; set; }
        public decimal IdArea { get; set; }
        public string NomModelo { get; set; }
        public int IdActivoFijoTipo { get; set; }
        public int IdCategoriaAF { get; set; }
    }
}
