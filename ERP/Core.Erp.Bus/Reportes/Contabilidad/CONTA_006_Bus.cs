using Core.Erp.Data.Reportes.Contabilidad;
using Core.Erp.Info.Reportes.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Contabilidad
{
   public class CONTA_006_Bus
    {
        CONTA_006_Data odata = new CONTA_006_Data();
        public List<CONTA_006_Info> GetList(int IdEmpresa, int IdAnio, bool mostrarSaldo0, string IdUsuario, int IdNivel, bool mostrarAcumulado, string balance)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdAnio, mostrarSaldo0, IdUsuario, IdNivel, mostrarAcumulado, balance);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
