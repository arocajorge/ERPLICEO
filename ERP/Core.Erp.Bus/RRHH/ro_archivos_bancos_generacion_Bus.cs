using Core.Erp.Data.RRHH;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Data.General;
using Core.Erp.Data.Banco;
using System.IO;
namespace Core.Erp.Bus.RRHH
{
   public class ro_archivos_bancos_generacion_Bus
    {
        ro_archivos_bancos_generacion_Data odata = new ro_archivos_bancos_generacion_Data();
        tb_empresa_Data odata_empresa = new tb_empresa_Data();
        ba_Banco_Cuenta_Data odata_cuenta = new ba_Banco_Cuenta_Data();
        public List<ro_archivos_bancos_generacion_Info> get_list(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin, int IdSucursal, bool estado)
        {
            try
            {
                return odata.get_list(IdEmpresa, fecha_ini, fecha_fin, IdSucursal, estado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_archivos_bancos_generacion_Info get_info(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdArchivo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_archivos_bancos_generacion_Info info)
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

        public bool modificarDB(ro_archivos_bancos_generacion_Info info)
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

        public bool anularDB(ro_archivos_bancos_generacion_Info info)
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
        public int get_secuencia_file(int IdEmpresa, int IdProceso, DateTime FechaActual)
        {
            try
            {
                return odata.get_secuencia_file(IdEmpresa, IdProceso, FechaActual);
            }
            catch (Exception)
            {

                throw;
            }
        }
       
    }
}
