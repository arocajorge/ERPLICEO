using Core.Erp.Data.Presupuesto;
using Core.Erp.Info.Presupuesto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Presupuesto
{
    public class pre_Periodo_Bus
    {
        pre_Periodo_Data oData = new pre_Periodo_Data();

        public List<pre_Periodo_Info> GetList(int IdEmpresa, bool MostrarAnulado, bool MostrarCerrado)
        {
            try
            {
                return oData.GetList(IdEmpresa, MostrarAnulado, MostrarCerrado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public pre_Periodo_Info GetInfo(int IdEmpresa, int IdPeriodo)
        {
            try
            {
                return oData.GetInfo(IdEmpresa, IdPeriodo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public pre_Periodo_Info GetInfo_UltimoPeriodoAbierto(int IdEmpresa)
        {
            try
            {
                return oData.GetInfo_UltimoPeriodoAbierto(IdEmpresa);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool GuardarBD(pre_Periodo_Info info)
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

        public bool ModificarBD(pre_Periodo_Info info)
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

        public bool AnularBD(pre_Periodo_Info info)
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
