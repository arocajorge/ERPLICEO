using Core.Erp.Data.Compras;
using Core.Erp.Info.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Compras
{
   public class com_comprador_Bus
    {
        com_comprador_Data odata = new com_comprador_Data();
        public List<com_comprador_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public com_comprador_Info get_info(int IdEmpresa, decimal IdComprador)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdComprador);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(com_comprador_Info info)
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
        public bool modificarDB(com_comprador_Info info)
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
        public bool anularDB(com_comprador_Info info)
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

        public com_comprador_Info get_info_x_IdUsuario(int IdEmpresa, string IdUsuario)
        {
            try
            {
                return odata.get_info_x_IdUsuario(IdEmpresa, IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
