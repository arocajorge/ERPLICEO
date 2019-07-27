using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Contabilidad
{
    public class ct_anio_fiscal_x_tb_sucursal_Bus
    {
        ct_anio_fiscal_x_tb_sucursal_Data odata = new ct_anio_fiscal_x_tb_sucursal_Data();
        public List<ct_anio_fiscal_x_tb_sucursal_Info> get_list(int IdEmpresa, int IdSucursal)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ct_anio_fiscal_x_tb_sucursal_Info get_info(int IdEmpresa, int IdSucursal, int IdanioFiscal)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdSucursal, IdanioFiscal);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool guardarDB(ct_anio_fiscal_x_tb_sucursal_Info info)
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
        public bool modificarDB(ct_anio_fiscal_x_tb_sucursal_Info info)
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
        public bool anularDB(ct_anio_fiscal_x_tb_sucursal_Info info)
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
