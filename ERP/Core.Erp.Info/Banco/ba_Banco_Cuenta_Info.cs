using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Banco
{
    public class ba_Banco_Cuenta_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdBanco { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        public string ba_descripcion { get; set; }
        public string ba_Tipo { get; set; }
        [Required(ErrorMessage = "El campo # de cuenta es obligatorio")]
        public string ba_Num_Cuenta { get; set; }
        public int ba_num_digito_cheq { get; set; }
        [Required(ErrorMessage = "El campo cuenta contable es obligatoria")]
        public string IdCtaCble { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public byte[] ReporteChequeComprobante { get; set; }
        public byte[] ReporteCheque { get; set; }
        public bool Imprimir_Solo_el_cheque { get; set; }
        public int IdBanco_Financiero { get; set; }
        public bool EsFlujoObligatorio { get; set; }

        #region Campos de auditoria
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotiAnula { get; set; }
        public int IdSucursal { get; set; }

        public List<ba_Banco_Cuenta_x_tb_sucursal_Info> lstDet { get; set; }
        #endregion

    }
}
