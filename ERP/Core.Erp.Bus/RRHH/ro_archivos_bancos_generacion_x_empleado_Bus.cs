using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
   public class ro_archivos_bancos_generacion_x_empleado_Bus
    {
        ro_archivos_bancos_generacion_x_empleado_Data odata = new ro_archivos_bancos_generacion_x_empleado_Data();
        public List<ro_archivos_bancos_generacion_x_empleado_Info> get_list(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdArchivo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_archivos_bancos_generacion_x_empleado_Info> get_list(int IdEmpresa, int IdNominaTipo, int IdNominaTipoLiqui, int IdPeriodo, string TipoCuenta, int IdSucursal)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdNominaTipo,IdNominaTipoLiqui,IdPeriodo, TipoCuenta, IdSucursal);
            }
            catch (Exception)
            {

                throw;
            }
        }







    }
}
