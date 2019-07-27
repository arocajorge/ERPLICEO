using Core.Erp.Data.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Facturacion
{
    public class FAC_005_Bus
    {
        FAC_005_Data odata = new FAC_005_Data();
        public List<FAC_005_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdCliente, DateTime Fecha_ini, DateTime Fecha_fin, ref List<FAC_005_resumen_Info> lst_resumen)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdCliente, Fecha_ini, Fecha_fin, ref lst_resumen);   
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
