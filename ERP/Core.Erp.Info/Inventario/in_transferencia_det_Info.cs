using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
   public class in_transferencia_det_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursalOrigen { get; set; }
        public int IdBodegaOrigen { get; set; }
        public decimal IdTransferencia { get; set; }
        public int dt_secuencia { get; set; }
        public decimal IdProducto { get; set; }
        public double dt_cantidad { get; set; }
        public string tr_Observacion { get; set; }
        public string IdUnidadMedida { get; set; }

        #region Campos que no existen en la tabla
        public string pr_descripcion { get; set; }
        public string tp_ManejaInven { get; set; }
        public bool se_distribuye { get; set; }
        public double CantidadAnterior { get; set; }
        #endregion
    }
}
