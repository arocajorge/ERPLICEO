using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Facturacion
{
   public class fa_NivelDescuento_Bus
    {
        fa_NivelDescuento_Data odata = new fa_NivelDescuento_Data();
        public List<fa_NivelDescuento_Info> GetList(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                return odata.GetList(IdEmpresa, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public fa_NivelDescuento_Info GetInfo(int IdEmpresa, int IdNivel)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, IdNivel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool GuardarDB(fa_NivelDescuento_Info info)
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
        public bool ModificarDB(fa_NivelDescuento_Info info)
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
        public bool AnularDB(fa_NivelDescuento_Info info)
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
