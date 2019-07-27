using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Contabilidad
{
    public class ct_periodo_Bus
    {
        ct_periodo_Data odata = new ct_periodo_Data();
        public List<ct_periodo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, true);

            }
            catch (Exception)
            {

               throw;
            }
        }

        public ct_periodo_Info get_info(int IdEmpresa, int IdPeriodo)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdPeriodo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ct_periodo_Info info)
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

        public bool modificarDB(ct_periodo_Info info)
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

        public bool anularDB(ct_periodo_Info info)
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

        public bool ValidarFechaTransaccion(int IdEmpresa, DateTime Fecha, cl_enumeradores.eModulo Modulo, int IdSucursal, ref string mensaje)
        {
            try
            {
                return odata.ValidarFechaTransaccion(IdEmpresa,Fecha, Modulo, IdSucursal, ref mensaje);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
