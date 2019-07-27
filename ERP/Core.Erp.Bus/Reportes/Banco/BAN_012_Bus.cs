using Core.Erp.Data.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Banco
{
    public class BAN_012_Bus
    {
        BAN_012_Data odata = new BAN_012_Data();
        public List<BAN_012_Info> GetList(int IdEmpresa, int IdSucursal, DateTime fechaIni, DateTime fechaFin, bool mostrarSaldo0, string IdUsuario)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, fechaIni, fechaFin, mostrarSaldo0, IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
