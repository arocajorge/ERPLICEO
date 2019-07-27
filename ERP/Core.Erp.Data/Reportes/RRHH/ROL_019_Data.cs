using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
    public class ROL_019_Data
    {
        public List<ROL_019_Info> get_list(int IdEmpresa, int IdSucursal, int IdNominaTipoLiqui, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {

                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;

                int IdNominaTipoLiquiIni = IdNominaTipoLiqui;
                int IdNominaTipoLiquiFin = IdNominaTipoLiqui == 0 ? 9999 : IdNominaTipoLiqui;

                fecha_fin = Convert.ToDateTime(fecha_fin.ToShortDateString());
                fecha_ini = Convert.ToDateTime(fecha_ini.ToShortDateString());
                List<ROL_019_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWROL_019
                             where q.IdEmpresa == IdEmpresa
                             && IdSucursalIni <= q.IdSucursal && q.IdSucursal <= IdSucursalFin
                             && IdNominaTipoLiquiIni <= q.IdNominaTipoLiqui && q.IdNominaTipoLiqui <= IdNominaTipoLiquiFin
                             && fecha_ini <= q.FechaPago
                             && q.FechaPago <= fecha_fin
                             select new ROL_019_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdNominaTipoLiqui = q.IdNominaTipoLiqui,
                                 IdEmpleado = q.IdEmpleado,
                                 IdRubro = q.IdRubro,
                                 FechaPago = q.FechaPago,
                                 Descripcion = q.Descripcion,
                                 em_codigo = q.em_codigo ,
                                 Estado = q.Estado,
                                 EstadoPago = q.EstadoPago,
                                 IdArea = q.IdArea, 
                                 IdDivision = q.IdDivision,
                                 IdNomina = q.IdNomina,
                                 IdPrestamo = q.IdPrestamo,
                                 IdSucursal = q.IdSucursal,
                                 Nomina = q.Nomina,
                                 NumCuota = q.NumCuota,
                                 Observacion_det = q.Observacion_det,
                                 pe_apellido = q.pe_apellido,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                pe_nombre = q.pe_nombre,
                                Saldo = q.Saldo,
                                SaldoInicial = q.SaldoInicial,
                                Su_Descripcion = q.Su_Descripcion,
                                TotalCuota = q.TotalCuota,
                                ru_descripcion = q.ru_descripcion
                                  
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
