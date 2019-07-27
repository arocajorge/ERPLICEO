using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Inventario
{
    public class in_Ajuste_Bus
    {
        in_Ajuste_Data odata = new in_Ajuste_Data();
        in_Ing_Egr_Inven_Data odata_mov_inv = new in_Ing_Egr_Inven_Data();
        in_parametro_Data odata_parametro = new in_parametro_Data();
        public List<in_Ajuste_Info> get_list(int IdEmpresa, int IdSucursal, bool mostrar_anulados, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, mostrar_anulados, fecha_ini, fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public in_Ajuste_Info get_info(int IdEmpresa, int IdSucursal, decimal IdAjuste)
        {
            try
            {
                return odata.get_info(IdEmpresa,IdSucursal, IdAjuste);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(in_Ajuste_Info info)
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

        public bool modificarDB(in_Ajuste_Info info)
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

        public bool anularDB(in_Ajuste_Info info)
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
