using Core.Erp.Data.Migraciones;
using Core.Erp.Info.Migraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Migraciones
{
   public class FacturasEventos_Bus
    {
        FacturasEventos_Data odata = new FacturasEventos_Data();
        public List<FacturasEventos_Info> get_list(DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                return odata.get_lis(FechaInicio, FechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool ModificarEstado_aprobacion(FacturasEventos_Info info)
        {
            try
            {
                return odata.ModificarEstado_aprobacion(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
