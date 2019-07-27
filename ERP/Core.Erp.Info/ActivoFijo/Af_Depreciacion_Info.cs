using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.ActivoFijo
{
    public class Af_Depreciacion_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdDepreciacion { get; set; }
        [StringLength(20, ErrorMessage = "el campo código debe tener máximo 20 caracteres")]
        public string Cod_Depreciacion { get; set; }
        [Required(ErrorMessage ="El campo periodo es obligatorio")]
        public int IdPeriodo { get; set; }
        [StringLength(200, ErrorMessage = "el campo observación debe tener máximo 200 caracteres")]
        public string Descripcion { get; set; }
        public System.DateTime Fecha_Depreciacion { get; set; }
        public int Num_Act_Depre { get; set; }
        public double Valor_Tot_Act { get; set; }
        public double Valor_Tot_Depre { get; set; }
        public double Valor_Tot_DepreAcum { get; set; }
        public double Valot_Tot_Importe { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnula { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public Nullable<int> IdEmpresa_ct { get; set; }
        public Nullable<int> IdTipoCbte { get; set; }
        public Nullable<decimal> IdCbteCble { get; set; }

        //campos que no existen en la tabla

        public List<Af_Depreciacion_Det_Info> lst_detalle { get; set; }
        public List<ct_cbtecble_det_Info> lst_detalle_ct { get; set; }
    }
}