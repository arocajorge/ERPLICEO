using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
    public class ro_SancionesPorMarcaciones_det_Bus
    {
        ro_SancionesPorMarcaciones_det_Data odata = new ro_SancionesPorMarcaciones_det_Data();
        public List<ro_SancionesPorMarcaciones_det_Info> get_list(int IdEmpresa, int IdNomina, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdNomina, FechaInicio, FechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_SancionesPorMarcaciones_det_Info> get_list(int IdEmpresa, decimal IdAjuste)
        {
            try
            {
                odata = new ro_SancionesPorMarcaciones_det_Data();
                return odata.get_list(IdEmpresa, IdAjuste);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
