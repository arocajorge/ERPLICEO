using Core.Erp.Data.Reportes.Inventario;
using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.Inventario
{
    public class INV_007_Bus
    {
        INV_007_Data odata = new INV_007_Data();
    
        public List<INV_007_Info> get_list(int IdEmpresa, int IdSucursalOrigen, int IdBodegaOrigen, decimal IdTransferencia)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursalOrigen, IdBodegaOrigen, IdTransferencia);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
