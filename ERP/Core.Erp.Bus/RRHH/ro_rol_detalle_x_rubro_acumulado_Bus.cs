using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.RRHH
{
    public class ro_rol_detalle_x_rubro_acumulado_Bus
    {
        ro_rol_detalle_x_rubro_acumulado_Data odata = new ro_rol_detalle_x_rubro_acumulado_Data();
        public double get_valor_x_rubro_acumulado(int IdEmpresa, decimal IdEmpleado, string IdRubro)
        {
            try
            {
                return odata.get_valor_x_rubro_acumulado(IdEmpresa, IdEmpleado, IdRubro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public double get_vac_x_mes_x_anio(int IdEmpresa, decimal IdEmpleado, int Anio, int mes)
        {
            try
            {
                return odata.get_vac_x_mes_x_anio(IdEmpresa, IdEmpleado, Anio, mes);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_rol_detalle_x_rubro_acumulado_Info> GetList_BeneficiosSociales(int IdEmpresa, int IdSucursal, int IdNomina_Tipo, string IdRubro, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                return odata.GetList_BeneficiosSociales(IdEmpresa, IdSucursal, IdNomina_Tipo, IdRubro, FechaIni, FechaFin);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ModificarBD(List<ro_rol_detalle_x_rubro_acumulado_Info> Lista)
        {
            try
            {
                return odata.ModificarBD(Lista);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
