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
    
    public partial class ro_prestamo
    {
        public ro_prestamo()
        {
            this.ro_prestamo_detalle = new HashSet<ro_prestamo_detalle>();
            this.ro_PrestamoMasivo_Det = new HashSet<ro_PrestamoMasivo_Det>();
        }
    
        public int IdEmpresa { get; set; }
        public decimal IdPrestamo { get; set; }
        public decimal IdEmpleado { get; set; }
        public string IdRubro { get; set; }
        public bool descuento_mensual { get; set; }
        public bool descuento_quincena { get; set; }
        public bool descuento_men_quin { get; set; }
        public bool descuento_ben_soc { get; set; }
        public bool Estado { get; set; }
        public System.DateTime Fecha { get; set; }
        public double MontoSol { get; set; }
        public int NumCuotas { get; set; }
        public System.DateTime Fecha_PriPago { get; set; }
        public string Observacion { get; set; }
        public Nullable<int> IdEmpresa_dc { get; set; }
        public Nullable<int> IdTipoCbte { get; set; }
        public Nullable<decimal> IdCbteCble { get; set; }
        public Nullable<int> IdEmpresa_op { get; set; }
        public Nullable<decimal> IdOrdenPago { get; set; }
        public string IdUsuarioAprueba { get; set; }
        public string EstadoAprob { get; set; }
        public string IdUsuario { get; set; }
        public System.DateTime Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotiAnula { get; set; }
        public Nullable<double> cod_prestamo { get; set; }
        public string IdTipo_Persona { get; set; }
        public Nullable<decimal> IdEntidad { get; set; }
        public Nullable<decimal> IdPersona { get; set; }
        public bool GeneraOP { get; set; }
    
        public virtual ro_catalogo ro_catalogo { get; set; }
        public virtual ICollection<ro_prestamo_detalle> ro_prestamo_detalle { get; set; }
        public virtual ro_rubro_tipo ro_rubro_tipo { get; set; }
        public virtual ICollection<ro_PrestamoMasivo_Det> ro_PrestamoMasivo_Det { get; set; }
        public virtual ro_empleado ro_empleado { get; set; }
    }
}
