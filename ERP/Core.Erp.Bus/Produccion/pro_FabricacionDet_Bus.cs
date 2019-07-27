using Core.Erp.Data.Produccion;
using Core.Erp.Info.Produccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Produccion
{
    public class pro_FabricacionDet_Bus
    {
        pro_FabricacionDet_Data odata = new pro_FabricacionDet_Data();
        public List<pro_FabricacionDet_Info> GetList(int IdEmpresa, decimal IdFabricacion)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdFabricacion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<pro_FabricacionDet_Info> GetProductoFacturadosPorFecha(int IdEmpresa, int IdSucursal, int IdBodega, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                return odata.GetProductoFacturadosPorFecha(IdEmpresa, IdSucursal, IdBodega,  FechaIni,  FechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
