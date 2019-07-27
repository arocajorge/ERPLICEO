using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Produccion
{
    public class pro_FabricacionDet_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdFabricacion { get; set; }
        public int Secuencia { get; set; }
        public string Signo { get; set; }
        public decimal IdProducto { get; set; }
        public string IdUnidadMedida { get; set; }
        public double Cantidad { get; set; }
        public double Costo { get; set; }
        public bool RealizaMovimiento { get; set; }
        

        #region Campos que no existen en la tabla
        public string pr_descripcion { get; set; }
        public Nullable<System.DateTime> vt_fecha { get; set; }
        public string NombreUnidad { get; set; }
        public double stock { get; set; }
        public Nullable<double> CantidadFabricada { get; set; }
        public string tp_ManejaInven { get; set; }
        public bool se_distribuye { get; set; }
        public double CantidadAnterior { get; set; }
        #endregion

    }
}
