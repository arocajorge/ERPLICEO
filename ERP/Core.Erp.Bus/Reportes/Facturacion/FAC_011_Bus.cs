using Core.Erp.Data.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Facturacion
{
    public class FAC_011_Bus
    {
        FAC_011_Data odata = new FAC_011_Data();
        public List<FAC_011_Info> get_list(int IdEmpresa, decimal IdCliente, DateTime fechaIni, DateTime fechaFin, bool mostrarAnulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdCliente, fechaIni, fechaFin, mostrarAnulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
