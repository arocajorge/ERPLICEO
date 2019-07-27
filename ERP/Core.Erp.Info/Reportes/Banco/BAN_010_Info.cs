using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Banco
{
    public class BAN_010_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdArchivo { get; set; }
        public int Secuencia { get; set; }
        public int IdEmpresa_OP { get; set; }
        public decimal IdOrdenPago { get; set; }
        public int Secuencia_OP { get; set; }
        public bool Estado { get; set; }
        public double Valor { get; set; }
        public decimal Secuencial_reg_x_proceso { get; set; }
        public bool Contabilizado { get; set; }
        public Nullable<System.DateTime> Fecha_proceso { get; set; }
        public string IdTipoCta_acreditacion_cat { get; set; }
        public string num_cta_acreditacion { get; set; }
        public Nullable<int> IdBanco_acreditacion { get; set; }
        public string pr_direccion { get; set; }
        public string pr_correo { get; set; }
        public string IdTipoDocumento { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string CodigoLegalBanco { get; set; }
        public string Referencia { get; set; }
        public string IdTipo_Persona { get; set; }
        public decimal IdPersona { get; set; }
        public decimal IdEntidad { get; set; }
        public System.DateTime cb_Fecha { get; set; }
        public string ba_descripcion { get; set; }
        public string NomCuenta { get; set; }
        public string NombreProceso { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Observacion { get; set; }
    }
}
