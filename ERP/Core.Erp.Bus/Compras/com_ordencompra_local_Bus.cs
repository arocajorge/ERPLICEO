using Core.Erp.Bus.General;
using Core.Erp.Data.Compras;
using Core.Erp.Info.Compras;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Compras
{
   public class com_ordencompra_local_Bus
    {
        com_ordencompra_local_Data odata = new com_ordencompra_local_Data();
        public List<com_ordencompra_local_Info> get_list(int IdEmpresa, int IdSucursal, DateTime fecha_ini, DateTime fecha_fin, bool mostrar_anulados, string Tipo)
        {
            try
            {
                return odata.get_list(IdEmpresa,  IdSucursal,  fecha_ini,   fecha_fin, mostrar_anulados, Tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public com_ordencompra_local_Info get_info(int IdEmpresa, int IdSucursal, decimal IdOrdenCompra)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdSucursal, IdOrdenCompra);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(com_ordencompra_local_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "com_ordencompra_local_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool modificarDB(com_ordencompra_local_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "com_ordencompra_local_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool anularDB(com_ordencompra_local_Info info)
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
        public List<com_ordencompra_local_Info> GetListPorAprobar(int IdEmpresa, int IdSucursal, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                return odata.GetListPorAprobar(IdEmpresa, IdSucursal, fecha_ini, fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool AprobarOC(int IdEmpresa, int IdSucursal, string[] Lista, string MotivoAprobacion, string IdUsuarioAprobacion)
        {
            try
            {
                return odata.AprobarOC(IdEmpresa,  IdSucursal, Lista, MotivoAprobacion, IdUsuarioAprobacion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool RechazarOC(int IdEmpresa, int IdSucursal, string[] Lista, string MotivoAprobacion, string IdUsuarioAprobacion)
        {
            try
            {
                return odata.RechazarOC(IdEmpresa,  IdSucursal, Lista, MotivoAprobacion, IdUsuarioAprobacion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<com_ordencompra_local_Info> get_list_x_ingresar(int IdEmpresa, int IdSucursal, decimal IdResponsable)
        {
            try
            {
                return odata.get_list_x_ingresar(IdEmpresa, IdSucursal, IdResponsable);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
