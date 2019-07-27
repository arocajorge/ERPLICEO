using Core.Erp.Data.ActivoFijo;
using Core.Erp.Info.ActivoFijo;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.ActivoFijo
{
    public class Af_Depreciacion_Det_Bus
    {
        Af_Depreciacion_Det_Data odata = new Af_Depreciacion_Det_Data();

        public List<Af_Depreciacion_Det_Info> get_list(int IdEmpresa, decimal IdDepreciacion)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdDepreciacion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Af_Depreciacion_Det_Info> get_list_a_depreciar(int IdEmpresa, int IdPeriodo, string IdUsuario)
        {
            try
            {
                return odata.get_list_a_depreciar(IdEmpresa, IdPeriodo, IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
