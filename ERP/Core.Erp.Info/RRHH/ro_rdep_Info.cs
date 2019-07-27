using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
    public class ro_rdep_Info
    {        
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int Id_Rdep { get; set; }
        public int IdSucursal { get; set; }
        public int pe_anio { get; set; }
        public int IdNomina_Tipo { get; set; }
        public string Su_CodigoEstablecimiento { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }

        [Required(ErrorMessage = ("El campo motivo de anulación es obligatorio"))]
        public string MotiAnula { get; set; }

        #region Campos que no existen en la tabla
        public string Su_Descripcion { get; set; }
        public string Descripcion { get; set; }
        public int IdEmpleado { get; set; }
        
        public List<ro_rdep_det_Info> Lista_Rdep_Det { get; set; }
        #endregion



    }
}
