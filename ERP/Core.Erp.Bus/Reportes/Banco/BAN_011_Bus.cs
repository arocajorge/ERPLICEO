using Core.Erp.Data.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Banco
{
    public class BAN_011_Bus
    {
        BAN_011_Data odata = new BAN_011_Data();
        public List<BAN_011_Info> GetList(int IdEmpresa, string IdUsuario, int IdSucursal, DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdUsuario, IdSucursal, fechaIni, fechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
