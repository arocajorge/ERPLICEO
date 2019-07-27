using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Contabilidad
{
    public class ct_RevisionContable_Bus
    {
        ct_RevisionContable_Data odata = new ct_RevisionContable_Data();
        public List<ct_RevisionContableFacturas_Info> get_list_facturas(int IdEmpresa, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                return odata.get_list_facturas(IdEmpresa, FechaIni, FechaFin);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
