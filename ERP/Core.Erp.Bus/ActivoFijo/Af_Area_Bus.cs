using Core.Erp.Data.ActivoFijo;
using Core.Erp.Info.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.ActivoFijo
{
    public class Af_Area_Bus
    {
        Af_Area_Data odata = new Af_Area_Data();
    
        public List<Af_Area_Info> GetList(int IdEmpresa, bool mostrar_anulados)
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

        public Af_Area_Info GetInfo(int IdEmpresa, decimal IdArea)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, IdArea);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(Af_Area_Info info)
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

        public bool ModificarDB(Af_Area_Info info)
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

        public bool AnularDB(Af_Area_Info info)
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
