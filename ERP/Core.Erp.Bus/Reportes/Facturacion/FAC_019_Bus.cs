using Core.Erp.Data.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Facturacion
{
    public class FAC_019_Bus
    {
        FAC_019_Data odata = new FAC_019_Data();
        public List<FAC_019_Info> GetList(int IdEmpresa, decimal IdCliente, int IdVendedor, DateTime fechaCorte, bool mostrarSoloVencido, bool mostrarSaldo0, string IdUsuario)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdCliente, IdVendedor, fechaCorte, mostrarSoloVencido, mostrarSaldo0, IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
