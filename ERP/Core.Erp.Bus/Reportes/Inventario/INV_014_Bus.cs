using Core.Erp.Data.Reportes.Inventario;
using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Inventario
{
    public class INV_014_Bus
    {
        INV_014_Data odata = new INV_014_Data();

        public List<INV_014_Info> get_list(int IdEmpresa, decimal IdConsignacion)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdConsignacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
