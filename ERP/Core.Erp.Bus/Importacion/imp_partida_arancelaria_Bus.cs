using Core.Erp.Data.Importacion;
using Core.Erp.Info.Importacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Importacion
{
    public class imp_partida_arancelaria_Bus
    {
        imp_partida_arancelaria_Data odata = new imp_partida_arancelaria_Data();
        public List<imp_partida_arancelaria_Info> get_list(bool mostrar_Anulados)
        {
            try
            {
                return odata.get_list(mostrar_Anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public imp_partida_arancelaria_Info get_info(decimal IdArancel)
        {
            try
            {
                return odata.get_info(IdArancel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_si_existe_codigo(string CodigoPartidaArancelaria)
        {
            try
            {
                return odata.validar_si_existe_codigo(CodigoPartidaArancelaria);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(imp_partida_arancelaria_Info info)
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

        public bool modificarDB(imp_partida_arancelaria_Info info)
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

        public bool anularDB(imp_partida_arancelaria_Info info)
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
