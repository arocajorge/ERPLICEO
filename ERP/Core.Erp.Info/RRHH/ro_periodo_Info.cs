using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_periodo_Info
    {
        public int IdEmpresa { get; set; }
        public int IdPeriodo { get; set; }
        public Nullable<int> pe_anio { get; set; }
        public Nullable<int> pe_mes { get; set; }
        [Required(ErrorMessage = "El campo fecha inicio es obligatorio")]
        [DataType(DataType.Date)]
        public System.DateTime pe_FechaIni { get; set; }
        [Required(ErrorMessage = "El campo fecha fin es obligatorio")]
        [DataType(DataType.Date)]
        public System.DateTime pe_FechaFin { get; set; }
        public string pe_estado { get; set; }
        public bool EstadoBool { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> FechaHoraAnul { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
        public string Cod_region { get; set; }
        public bool Carga_Todos_Empleados { get; set; }
        public Nullable<bool> Carga_Todos_Empl { get; set; }

        public string CodCatalogo { get; set; }
        public string IdUsuario { get; set; }

    }
}
