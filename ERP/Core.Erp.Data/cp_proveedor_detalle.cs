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
    
    public partial class cp_proveedor_detalle
    {
        public cp_proveedor_detalle()
        {
            this.cp_orden_giro = new HashSet<cp_orden_giro>();
        }
    
        public int IdEmpresa { get; set; }
        public decimal IdProveedor { get; set; }
        public int Secuencia { get; set; }
        public string NombreAdicional { get; set; }
        public string IdTipoCta_acreditacion_cat { get; set; }
        public string num_cta_acreditacion { get; set; }
        public Nullable<int> IdBanco_acreditacion { get; set; }
        public string pr_correo { get; set; }
        public bool Estado { get; set; }
    
        public virtual cp_proveedor cp_proveedor { get; set; }
        public virtual ICollection<cp_orden_giro> cp_orden_giro { get; set; }
    }
}
