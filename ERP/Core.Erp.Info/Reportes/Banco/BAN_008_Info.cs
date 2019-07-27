using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Banco
{
    public class BAN_008_Info
    {
        public Nullable<int> IdEmpresa { get; set; }
        public int IdTipoCbte { get; set; }
        public decimal IdCbteCble { get; set; }
        public int secuencia { get; set; }
        public Nullable<decimal> SaldoInicial { get; set; }
        public string cb_Observacion { get; set; }
        public string cb_Cheque { get; set; }
        public string cb_giradoA { get; set; }
        public Nullable<System.DateTime> cb_Fecha { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public string Tipo { get; set; }
        public Nullable<decimal> ValorAbsoluto { get; set; }
        public int Orden { get; set; }
        public string Referencia { get; set; }
        public string ba_descripcion { get; set; }
        public Nullable<int> IdBanco { get; set; }
        public string Estado { get; set; }
        public int IdSucursal { get; set; }
        public string Su_Descripcion { get; set; }
        public string tc_TipoCbte { get; set; }
        public string TipoAgrupacion { get; set; }
        public int OrdenRegistros { get; set; }
        public string MotivoNota { get; set; }
        public string Flujo { get; set; }
    }
}
