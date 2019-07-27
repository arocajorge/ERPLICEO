using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Contabilidad
{
    public class ct_punto_cargo_grupo_Bus
    {
        ct_punto_cargo_grupo_Data odata = new ct_punto_cargo_grupo_Data();
        public List<ct_punto_cargo_grupo_Info> GetList(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                return odata.GetList(IdEmpresa, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ct_punto_cargo_grupo_Info GetInfo(int IdEmpresa, int IdPunto_cargo_grupo)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, IdPunto_cargo_grupo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(ct_punto_cargo_grupo_Info info)
        {
            try
            {
                return odata.GuardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB(ct_punto_cargo_grupo_Info info)
        {
            try
            {
                return odata.ModificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool AnularDB(ct_punto_cargo_grupo_Info info)
        {
            try
            {
                return odata.AnularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
