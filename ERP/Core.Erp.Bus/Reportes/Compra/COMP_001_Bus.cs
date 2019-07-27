using Core.Erp.Data.Reportes;
using Core.Erp.Info.Reportes.Compra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Compra
{
    public class COMP_001_Bus
    {
        COMP_001_Data odata = new COMP_001_Data();
        public List<COMP_001_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdOrdenCompra)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, IdOrdenCompra);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
