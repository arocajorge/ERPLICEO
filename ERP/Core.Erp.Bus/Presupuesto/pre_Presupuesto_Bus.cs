using Core.Erp.Data.Presupuesto;
using Core.Erp.Info.Presupuesto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Presupuesto
{
    public class pre_Presupuesto_Bus
    {
        pre_Presupuesto_Data oData = new pre_Presupuesto_Data();
        pre_PresupuestoDet_Data oData_det = new pre_PresupuestoDet_Data();

        public List<pre_Presupuesto_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdPeriodo, bool MostrarAnulados)
        {
            try
            {
                return oData.get_list(IdEmpresa, IdSucursal, IdPeriodo, MostrarAnulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public pre_Presupuesto_Info GetInfo(int IdEmpresa, int IdPresupuesto)
        {
            try
            {
                pre_Presupuesto_Info info_ = new pre_Presupuesto_Info();
                
                info_ = oData.get_info(IdEmpresa, IdPresupuesto);
                if (info_ == null)
                    info_ = new pre_Presupuesto_Info();
                info_.ListaPresupuestoDet = oData_det.GetList(IdEmpresa, IdPresupuesto);
                if (info_.ListaPresupuestoDet == null)
                {
                    info_.ListaPresupuestoDet = new List<pre_PresupuestoDet_Info>();
                }

                return info_;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarBD(pre_Presupuesto_Info info)
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

        public bool ModificarBD(pre_Presupuesto_Info info)
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

        public bool AnularBD(pre_Presupuesto_Info info)
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

        public bool AprobarBD(pre_Presupuesto_Info info)
        {
            try
            {
                return oData.AprobarBD(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
