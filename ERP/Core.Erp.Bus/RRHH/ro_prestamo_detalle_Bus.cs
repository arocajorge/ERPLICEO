using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.RRHH
{
    public  class ro_prestamo_detalle_Bus
    {

        #region variables
        ro_prestamo_Info _Info = new ro_prestamo_Info();
        List<ro_prestamo_detalle_Info> lst_detalle = new List<ro_prestamo_detalle_Info>();
        ro_contrato_Bus bus_contrato = new ro_contrato_Bus();
        ro_empleado_novedad_Bus bus_noveddes = new ro_empleado_novedad_Bus();
        ro_prestamo_detalle_Data odata = new ro_prestamo_detalle_Data();
        #endregion
        public List<ro_prestamo_detalle_Info> get_list(int IdEmpresa, decimal IdPrestamo)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdPrestamo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_prestamo_detalle_Info> get_list_cuota_pendientes(int IdEmpresa, decimal IdPrestamo)
        {
            try
            {
                return odata.get_list_cuota_pendientes(IdEmpresa, IdPrestamo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public double get_valor_cuotas_pendientes(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                return odata.get_valor_cuotas_pendientes(IdEmpresa, IdEmpleado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_prestamo_detalle_Info get_info(int IdEmpresa, decimal IdPrestamo, int Secuencia)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdPrestamo, Secuencia);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(List<ro_prestamo_detalle_Info> info)
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
        public bool anularDB(ro_prestamo_Info info)
        {
            try
            {
                return odata.eliminarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

     

    }
}
