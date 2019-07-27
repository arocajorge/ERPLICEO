using Core.Erp.Data.Reportes.Inventario;
using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Inventario
{
    public class INV_013_Bus
    {
        INV_013_Data odata = new INV_013_Data();

        public List<INV_013_Info> get_list(int IdEmpresa, decimal IdProducto)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdProducto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
