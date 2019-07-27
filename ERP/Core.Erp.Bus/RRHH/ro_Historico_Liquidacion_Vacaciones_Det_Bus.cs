using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.RRHH
{
    public class ro_Historico_Liquidacion_Vacaciones_Det_Bus
    {
        ro_Historico_Liquidacion_Vacaciones_Det_Data data = new ro_Historico_Liquidacion_Vacaciones_Det_Data();
        public bool Guardar_DB(ro_Historico_Liquidacion_Vacaciones_Det_Info info)
        {
            try
            {
                return data.Guardar_DB(info);
            }
            catch (Exception )
            {

                throw;
            }
        }

        public bool Eliminar(ro_Historico_Liquidacion_Vacaciones_Info info)
        {
            try
            {
                return data.Eliminar(info);
            }
            catch (Exception )
            {
                throw;
            }
        }
        public List<ro_Historico_Liquidacion_Vacaciones_Det_Info> Get_Lis(int IdEmpresa, decimal idempleado, decimal idsolicitud)
        {
            try
            {
                return data.Get_Lis(IdEmpresa, idempleado, idsolicitud);
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}
