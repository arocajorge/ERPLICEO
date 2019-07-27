using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Facturacion
{
    public class FAC_018_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdNota { get; set; }
        public string Su_Descripcion { get; set; }
        public System.DateTime no_fecha { get; set; }
        public decimal IdCliente { get; set; }
        public Nullable<int> IdTipoNota { get; set; }
        public string No_Descripcion { get; set; }
        public string NumNota { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string pe_cedulaRuc { get; set; }
        public double ValorAplicado { get; set; }
        public string NumDocumentoAplica { get; set; }
        public string NumDocumentoReemplazo { get; set; }
        public Nullable<double> Subtotal0 { get; set; }
        public Nullable<double> SubtotalIVA { get; set; }
        public Nullable<double> ValorIva { get; set; }
        public Nullable<double> Total { get; set; }
        public string Estado { get; set; }
        public string NomEstado { get; set; }
        public int Orden { get; set; }
        public string CreDeb { get; set; }
    }
}
