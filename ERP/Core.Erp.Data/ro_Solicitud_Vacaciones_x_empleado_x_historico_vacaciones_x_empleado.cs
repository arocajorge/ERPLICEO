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
    
    public partial class ro_Solicitud_Vacaciones_x_empleado_x_historico_vacaciones_x_empleado
    {
        public int IdEmpresa_sol { get; set; }
        public decimal IdEmpleado_sol { get; set; }
        public int IdSolicitud { get; set; }
        public int IdEmpresa_vaca { get; set; }
        public decimal IdEmpleado_vaca { get; set; }
        public int IdVacacion { get; set; }
        public string Observacion { get; set; }
    
        public virtual ro_historico_vacaciones_x_empleado ro_historico_vacaciones_x_empleado { get; set; }
        public virtual ro_Solicitud_Vacaciones_x_empleado ro_Solicitud_Vacaciones_x_empleado { get; set; }
    }
}
