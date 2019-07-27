using Core.Erp.Data.Reportes.Contabilidad;
using Core.Erp.Info.Reportes.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Contabilidad
{
    public class CONTA_004_Bus
    {
        CONTA_004_Data odata = new CONTA_004_Data();
        public List<CONTA_004_Info> GetList(int IdEmpresa, int IdAnio, int IdPeriodoIni, int IdPeriodoFin, bool mostrarSaldo0, string IdUsuario, int IdNivel, bool mostrarAcumulado, string balance)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdAnio, IdPeriodoIni,  IdPeriodoFin, mostrarAcumulado, IdUsuario, IdNivel, mostrarAcumulado, balance);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
