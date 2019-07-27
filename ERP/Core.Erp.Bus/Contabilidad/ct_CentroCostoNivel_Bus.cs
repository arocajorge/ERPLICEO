using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Contabilidad
{
    public class ct_CentroCostoNivel_Bus
    {
        ct_CentroCostoNivel_Data odata = new ct_CentroCostoNivel_Data();

        public List<ct_CentroCostoNivel_Info> get_list(int IdEmpresa, bool mostrar_anulados)
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

        public ct_CentroCostoNivel_Info get_info(int IdEmpresa, int IdNivel)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdNivel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ct_CentroCostoNivel_Info info)
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

        public bool modificarDB(ct_CentroCostoNivel_Info info)
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

        public bool anularDB(ct_CentroCostoNivel_Info info)
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

        public bool validar_existe_nivel(int IdEmpresa, int IdNivel)
        {
            try
            {
                return odata.validar_existe_nivel(IdEmpresa, IdNivel);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
