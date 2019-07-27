using Core.Erp.Data.Presupuesto;
using Core.Erp.Info.Presupuesto;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Presupuesto
{
    public class pre_rubro_Bus
    {
        pre_rubro_Data odata = new pre_rubro_Data();

        public List<pre_rubro_Info> GetList(int IdEmpresa, bool MostrarAnulado)
        {
            try
            {
                return odata.GetList(IdEmpresa, MostrarAnulado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public pre_rubro_Info GetInfo(int IdEmpresa, int IdRubro)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, IdRubro);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarBD(pre_rubro_Info info)
        {
            try
            {
                return odata.GuardarBD(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarBD(pre_rubro_Info info)
        {
            try
            {
                return odata.ModificarBD(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularBD(pre_rubro_Info info)
        {
            try
            {
                return odata.AnularBD(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
