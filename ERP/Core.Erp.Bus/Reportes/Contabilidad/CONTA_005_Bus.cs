using Core.Erp.Data.Reportes.Contabilidad;
using Core.Erp.Info.Reportes.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Contabilidad
{
    public class CONTA_005_Bus
    {
        CONTA_005_Data odata = new CONTA_005_Data();
        public List<CONTA_005_Info> GetList(int IdEmpresa, int IdPunto_cargo_grupo, string IdUsuario, DateTime fechaIni, DateTime fechaFin, bool mostrarSaldo0)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdPunto_cargo_grupo, IdUsuario, fechaIni, fechaFin, mostrarSaldo0);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
