using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Contabilidad
{
    public class ct_CierrePorModuloPorSucursal_Bus
    {
        ct_CierrePorModuloPorSucursal_Data odata = new ct_CierrePorModuloPorSucursal_Data();

        public List<ct_CierrePorModuloPorSucursal_Info> GetList(int IdEmpresa, int IdSucursal, bool MostrarAnulado)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, MostrarAnulado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ct_CierrePorModuloPorSucursal_Info GetInfo(int IdEmpresa, int IdRubro)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, IdRubro);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarBD(ct_CierrePorModuloPorSucursal_Info info)
        {
            try
            {
                return odata.GuardarBD(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarBD(ct_CierrePorModuloPorSucursal_Info info)
        {
            try
            {
                return odata.ModificarBD(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB(ct_CierrePorModuloPorSucursal_Info info)

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
