using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
    public class ROL_013_Data
    {
        public List<ROL_013_Info> get_list(int IdEmpresa, int IdNomina, int IdSucursal, DateTime FechaIni, DateTime FechaFin, decimal IdEmpleado, int IdDivision, int IdArea)
        {
            try
            {
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;

                decimal IdEmpleadoIni = IdEmpleado;
                decimal IdEmpleadoFin = IdEmpleado == 0 ? 999999999 : IdEmpleado;

                int IdDivisionIni = IdDivision;
                int IdDivisionFin = IdDivision == 0 ? 9999 : IdDivision;

                int IdArealIni = IdArea;
                int IdAreaFin = IdArea == 0 ? 9999 : IdArea;

                List<ROL_013_Info> Lista = new List<ROL_013_Info>();
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Context.SetCommandTimeOut(3000);
                    Lista = Context.SPROL_013(IdEmpresa, IdNomina, IdSucursalIni, IdSucursalFin, IdEmpleadoIni, IdEmpleadoFin,
                                            IdDivisionIni, IdDivisionFin, IdArealIni, IdAreaFin, FechaIni, FechaFin).Select(q=> new ROL_013_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdRol = q.IdRol,
                        IdEmpleado = q.IdEmpleado,
                        IdArea = q.IdArea,
                        IdDivision = q.IdDivision,
                        IdRubro = q.IdRubro,
                        em_codigo = q.em_codigo,
                        Provision = q.Provision ?? 0,
                        Estado = q.Estado,
                        IdSucursal = q.IdSucursal,
                        de_descripcion = q.de_descripcion,
                        Su_Descripcion = q.Su_Descripcion,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        Division = q.Division,
                        Area = q.Area,
                        Mes = q.Mes,
                        Prestamo = q.Prestamo,
                        Sueldo = q.Sueldo ??0
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
