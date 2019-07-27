using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.General
{
   public class tb_banco_procesos_bancarios_x_empresa_Bus
    {
        tb_banco_procesos_bancarios_x_empresa_Data odata = new tb_banco_procesos_bancarios_x_empresa_Data();

        public List<tb_banco_procesos_bancarios_x_empresa_Info> get_list(int IdEmpresa, bool mostrar_anulados)
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
        public List<tb_banco_procesos_bancarios_x_empresa_Info> get_list(int IdEmpresa, int IdBanco, bool SeContabiliza)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdBanco, SeContabiliza);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<tb_banco_procesos_bancarios_x_empresa_Info> get_list(int IdEmpresa, int IdBanco)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdBanco);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public tb_banco_procesos_bancarios_x_empresa_Info get_info(int IdEmpresa, int IdProceso)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdProceso);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(tb_banco_procesos_bancarios_x_empresa_Info info)
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

        public bool modificarDB(tb_banco_procesos_bancarios_x_empresa_Info info)
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
        public bool anularDB(tb_banco_procesos_bancarios_x_empresa_Info info)
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
