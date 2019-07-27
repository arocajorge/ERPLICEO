using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Produccion
{
    public class PRO_001_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdFabricacion { get; set; }
        public int Secuencia { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public string in_Su_Descripcion { get; set; }
        public string in_bo_Descripcion { get; set; }
        public string eg_Su_Descripcion { get; set; }
        public string eg_bo_Descripcion { get; set; }
        public string in_NombreTipo { get; set; }
        public string eg_NombreTipo { get; set; }
        public Nullable<decimal> egr_IdNumMovi { get; set; }
        public Nullable<decimal> ing_IdNumMovi { get; set; }
        public string Signo { get; set; }
        public decimal IdProducto { get; set; }
        public string IdUnidadMedida { get; set; }
        public double Cantidad { get; set; }
        public double Costo { get; set; }
        public bool RealizaMovimiento { get; set; }
        public string pr_descripcion { get; set; }
        public string NombreUnidad { get; set; }
        public int Orden { get; set; }
        public string Tipo { get; set; }
        public string Movimiento { get; set; }
    }
}
