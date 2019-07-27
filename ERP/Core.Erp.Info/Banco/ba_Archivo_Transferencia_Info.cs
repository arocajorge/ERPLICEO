using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.Helps;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Banco
{
    public class ba_Archivo_Transferencia_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdArchivo { get; set; }
        public string cod_archivo { get; set; }
        [Required(ErrorMessage = "El campo banco es obligatorio")]
        public int IdBanco { get; set; }
        [Required(ErrorMessage = "El campo proceso bancario es obligatorio")]
        public int IdProceso_bancario { get; set; }
        public string Cod_Empresa { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        public string Nom_Archivo { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public System.DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        [Required(ErrorMessage = "El campo observación es obligatorio")]
        public string Observacion { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string Motivo_anulacion { get; set; }
        public Nullable<System.DateTime> Fecha_Proceso { get; set; }
        public bool Contabilizado { get; set; }
        public int IdSucursal { get; set; }
        public decimal SecuencialInicial { get; set; }

        public double cb_Valor { get; set; }
        public cl_enumeradores.eTipoProcesoBancario TipoFile { get; set; }
        public List<ba_Archivo_Transferencia_Det_Info> Lst_det { get; set; }
        public List<ba_archivo_transferencia_x_ba_tipo_flujo_Info> Lst_Flujo { get; set; }
        public List<ct_cbtecble_det_Info> Lst_diario { get; set; }
        public int? IdTipoCbte { get; set; }
        public decimal? IdCbteCble { get; set; }
    }
}
