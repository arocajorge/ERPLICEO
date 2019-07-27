using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Data.Reportes.Inventario;
using Core.Erp.Info.Reportes.Inventario;

namespace Core.Erp.Bus.Reportes.Inventario
{
   public class INV_004_Bus
    {
        INV_004_Data odata = new INV_004_Data();
        public List<INV_004_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdProducto, int IdMarca)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal,  IdBodega, IdProducto, IdMarca);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
