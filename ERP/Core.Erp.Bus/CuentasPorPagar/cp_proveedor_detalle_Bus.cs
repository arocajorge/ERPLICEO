using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_proveedor_detalle_Bus
    {
        cp_proveedor_detalle_Data odata = new cp_proveedor_detalle_Data();
        public List<cp_proveedor_detalle_Info> get_list(int IdEmpresa, decimal IdProveedor, bool MostrarAnulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdProveedor, MostrarAnulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public cp_proveedor_detalle_Info get_info(int IdEmpresa, int IdProveedor, int Secuencia)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdProveedor, Secuencia);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(cp_proveedor_detalle_Info info)
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

        public bool modificarDB(cp_proveedor_detalle_Info info)
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

        public bool anularDB(cp_proveedor_detalle_Info info)
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
