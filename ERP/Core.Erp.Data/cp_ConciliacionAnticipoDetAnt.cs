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
    
    public partial class cp_ConciliacionAnticipoDetAnt
    {
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion { get; set; }
        public int Secuencia { get; set; }
        public decimal IdOrdenPago { get; set; }
        public double MontoAplicado { get; set; }
    
        public virtual cp_ConciliacionAnticipo cp_ConciliacionAnticipo { get; set; }
        public virtual cp_orden_pago cp_orden_pago { get; set; }
    }
}
