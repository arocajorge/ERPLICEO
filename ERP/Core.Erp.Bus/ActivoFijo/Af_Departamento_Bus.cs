using Core.Erp.Data.ActivoFijo;
using Core.Erp.Info.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.ActivoFijo
{
   public class Af_Departamento_Bus
    {
        Af_Departamento_Data odata = new Af_Departamento_Data();
        public List<Af_Departamento_Info> GetList(int IdEmpresa, decimal IdArea, bool mostrar_anulados)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdArea, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Af_Departamento_Info GetInfo(int IdEmpresa, decimal IdArea, decimal IdDepartamento)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, IdArea, IdDepartamento);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool GuardarDB(Af_Departamento_Info info)
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
        public bool ModificarDB(Af_Departamento_Info info)
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
        public bool AnularDB(Af_Departamento_Info info)
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
