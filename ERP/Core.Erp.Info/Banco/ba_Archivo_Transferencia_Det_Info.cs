using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Banco
{
    public class ba_Archivo_Transferencia_Det_Info
    {
        public string pe_cedulaRuc;
        public string pr_correo;
        public string pr_direccion;
        public string num_cta_acreditacion;

        public int IdEmpresa { get; set; }
        public decimal IdArchivo { get; set; }
        public int Secuencia { get; set; }
        public int IdEmpresa_OP { get; set; }
        public decimal IdOrdenPago { get; set; }
        public int Secuencia_OP { get; set; }
        public bool Estado { get; set; }
        public double Valor { get; set; }
        public decimal Secuencial_reg_x_proceso { get; set; }       
        public string Referencia { get; set; }
        

        #region Campos que no existen en la tabla
        public Nullable<System.DateTime> Fecha_Factura { get; set; }
        public string Nom_Beneficiario { get; set; }
        public decimal IdPersona { get; set; }
        public string IdTipoPersona { get; set; }
        public int? IdBanco_acreditacion { get; set; }
        public string IdTipoCta_acreditacion_cat { get; set; }
        public string IdTipoDocumento { get; set; }
        public string CodigoLegalBanco { get; set; }
        public string Observacion { get; set; }
        public decimal? IdEntidad { get; set; }
        public string IdCtaCble { get; set; }
        public string pc_Cuenta { get; set; }
        public int? IdEmpresa_cxp { get; set; }
        public int? IdTipoCbte_cxp { get; set; }
        public decimal? IdCbteCble_cxp { get; set; }
        public string pr_telefonos { get; set; }
        #endregion

    }
}
