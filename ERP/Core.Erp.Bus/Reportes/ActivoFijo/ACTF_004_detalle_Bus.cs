using Core.Erp.Data.Reportes.ActivoFijo;
using Core.Erp.Info.Reportes.ActivoFijo;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.ActivoFijo
{
    public class ACTF_004_detalle_Bus
    {
        ACTF_004_detalle_Data odata = new ACTF_004_detalle_Data();

        public List<ACTF_004_detalle_Info> get_list(int IdEmpresa, int IdActivoFijoTipo, int IdCategoriaAF, DateTime fecha_corte, string Estado_Proceso, string IdUsuario)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdActivoFijoTipo, IdCategoriaAF, fecha_corte, Estado_Proceso, IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
