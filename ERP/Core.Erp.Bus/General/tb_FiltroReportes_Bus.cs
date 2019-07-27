using Core.Erp.Data.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.General
{
    public class tb_FiltroReportes_Bus
    {
        tb_FiltroReportes_Data odata = new tb_FiltroReportes_Data();

        public string GuardarDB(int IdEmpresa, int[] IdSucursal, string IdUsuario)
        {
            try
            {
                return odata.GuardarDB(IdEmpresa, IdSucursal, IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
