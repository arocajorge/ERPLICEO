using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Facturacion
{
    public class fa_proforma_Bus
    {
        fa_proforma_Data odata = new fa_proforma_Data();

        public List<fa_proforma_Info> get_list(int IdEmpresa,int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal,  Fecha_ini, Fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public fa_proforma_Info get_info(int IdEmpresa, int IdSucursal, decimal IdProforma)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdSucursal, IdProforma);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_proforma_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(fa_proforma_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(fa_proforma_Info info)
        {
            try
            {
                return odata.anularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
