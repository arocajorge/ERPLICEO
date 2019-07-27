using Core.Erp.Info.Helps;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_archivos_bancos_generacion_Info
    {


        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdArchivo { get; set; }
        [Required(ErrorMessage = "El campo nómina  es obligatorio")]
        public int IdNomina { get; set; }
        [Required(ErrorMessage = "El campo tipo liquidación nómina  es obligatorio")]
        public int IdNominaTipo { get; set; }
        public int IdPeriodo { get; set; }
        public int IdCuentaBancaria { get; set; }
        [Required(ErrorMessage = "El campo proceso bancario  es obligatorio")]
        public int IdProceso { get; set; }
        public int? IdSucursal { get; set; }
        public string estado { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotiAnula { get; set; }
        public bool EstadoBool { get; set; }

        #region MyRegion
        public string Descripcion { get; set; }
        public string DescripcionProcesoNomina { get; set; }
        public System.DateTime pe_FechaIni { get; set; }
        public System.DateTime pe_FechaFin { get; set; }
        public string NombreProceso { get; set; }
        public string IdProceso_bancario_tipo { get; set; }
        public string Su_Descripcion { get; set; }

        #endregion
        public cl_enumeradores.eTipoProcesoBancario TipoFile { get; set; }
        public List < ro_archivos_bancos_generacion_x_empleado_Info> detalle { get; set; }

      public  ro_archivos_bancos_generacion_Info()
        {
            detalle = new List<ro_archivos_bancos_generacion_x_empleado_Info>();
        }

    }
}
