using Core.Erp.Data.Reportes.Contabilidad;
using Core.Erp.Info.Reportes.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Contabilidad
{
    public class CONTA_003_balances_Bus
    {
        CONTA_003_balances_Data odata = new CONTA_003_balances_Data();
        public List<CONTA_003_balances_Info> get_list(int IdEmpresa, int IdAnio, DateTime fechaIni, DateTime fechaFin, string IdUsuario, int IdNivel, bool mostrarSaldo0, string balance, bool MostrarSaldoAcumulado)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAnio, fechaIni, fechaFin, IdUsuario, IdNivel, mostrarSaldo0, balance, MostrarSaldoAcumulado);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
