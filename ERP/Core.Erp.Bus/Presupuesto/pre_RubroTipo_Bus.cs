using Core.Erp.Data.Presupuesto;
using Core.Erp.Info.Presupuesto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Presupuesto
{
    public class pre_RubroTipo_Bus
    {
        pre_RubroTipo_Data oData = new pre_RubroTipo_Data();

        public List<pre_RubroTipo_Info> GetList(int IdEmpresa, bool MostrarAnulado)
        {
            try
            {
                return oData.GetList(IdEmpresa, MostrarAnulado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public pre_RubroTipo_Info GetInfo(int IdEmpresa, int IdRubro)
        {
            try
            {
                return oData.GetInfo(IdEmpresa, IdRubro);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarBD(pre_RubroTipo_Info info)
        {
            try
            {
                return oData.GuardarBD(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarBD(pre_RubroTipo_Info info)
        {
            try
            {
                return oData.ModificarBD(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularBD(pre_RubroTipo_Info info)
        {
            try
            {
                return oData.AnularBD(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
