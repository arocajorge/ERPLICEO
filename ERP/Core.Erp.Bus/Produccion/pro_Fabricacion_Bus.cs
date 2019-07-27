using Core.Erp.Data.Produccion;
using Core.Erp.Info.Produccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Produccion
{
   public class pro_Fabricacion_Bus
    {
        pro_Fabricacion_Data odata = new pro_Fabricacion_Data();
        public List<pro_Fabricacion_Info> GetList(int IdEmpresa,int IdSucursal, DateTime fecha_ini, DateTime fecha_fin, bool mostrar_anulados)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal,  fecha_ini,  fecha_fin, mostrar_anulados);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public pro_Fabricacion_Info GetInfo(int IdEmpresa, decimal IdFabricacion)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, IdFabricacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool GuardarDB(pro_Fabricacion_Info info)
        {
            try
            {
                return odata.GuardarDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ModificarDB(pro_Fabricacion_Info info)
        {
            try
            {
                return odata.ModificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool AnularDB(pro_Fabricacion_Info info)
        {
            try
            {
                return odata.AnularDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
