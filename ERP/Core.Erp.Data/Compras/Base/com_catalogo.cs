//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Erp.Data.Compras.Base
{
    using System;
    using System.Collections.Generic;
    
    public partial class com_catalogo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public com_catalogo()
        {
            this.com_ordencompra_local = new HashSet<com_ordencompra_local>();
            this.com_parametro = new HashSet<com_parametro>();
            this.com_parametro1 = new HashSet<com_parametro>();
        }
    
        public string IdCatalogocompra { get; set; }
        public string IdCatalogocompra_tipo { get; set; }
        public string CodCatalogo { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public string Abrebiatura { get; set; }
        public string NombreIngles { get; set; }
        public Nullable<int> Orden { get; set; }
        public string IdUsuario { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> FechaUltMod { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotiAnula { get; set; }
    
        public virtual com_catalogo_tipo com_catalogo_tipo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<com_ordencompra_local> com_ordencompra_local { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<com_parametro> com_parametro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<com_parametro> com_parametro1 { get; set; }
    }
}
