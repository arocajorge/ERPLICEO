using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
using System.ComponentModel.DataAnnotations;
using Core.Erp.Info.Contabilidad;
namespace Core.Erp.Info.RRHH
{
   public class ro_rol_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo nómina es obligatorio")]
        public int IdNomina_Tipo { get; set; }
        public int? IdSucursal { get; set; }
        [Required(ErrorMessage = "El campo tipo de nómina es obligatorio")]
        public int IdNomina_TipoLiqui { get; set; }
        [Required(ErrorMessage = "El campo périodo es obligatorio")]
        public int IdPeriodo { get; set; }
        public int IdPeriodoSet { get; set; }
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo observación es obligatorio")]
        public string Observacion { get; set; }
        public string Cerrado { get; set; }
        public System.DateTime FechaIngresa { get; set; }
        public string UsuarioIngresa { get; set; }
        public Nullable<System.DateTime> FechaModifica { get; set; }
        public string UsuarioModifica { get; set; }
        public Nullable<System.DateTime> FechaAnula { get; set; }
        public string UsuarioAnula { get; set; }
        public string MotivoAnula { get; set; }
        public string UsuarioCierre { get; set; }
        public Nullable<System.DateTime> FechaCierre { get; set; }
        public string IdCentroCosto { get; set; }
        public decimal IdRol { get; set; }

        public int Anio { get; set; }
        public bool decimoIII { get; set; }
        public bool decimoIV { get; set; }

        public System.DateTime Fechacontabilizacion { get; set; }
        public string DescripcionProcesoNomina { get; set; }
        public string Procesado { get; set; }
        public string Contabilizado { get; set; }
        public System.DateTime pe_FechaIni { get; set; }
        public System.DateTime pe_FechaFin { get; set; }
        public List<ct_cbtecble_det_Info> lst_sueldo_x_pagar { get; set; }
        public List<ct_cbtecble_det_Info> lst_provisiones { get; set; }
        public string region { get; set; }
        public string Su_Descripcion { get; set; }

        public string EstadoRol { get; set; }
        public ro_rol_Info()
        {
            lst_sueldo_x_pagar = new List<ct_cbtecble_det_Info>();
            lst_provisiones = new List<ct_cbtecble_det_Info>();
        }

    }
}
