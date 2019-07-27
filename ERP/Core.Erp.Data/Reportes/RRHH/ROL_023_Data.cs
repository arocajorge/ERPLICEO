using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
    public class ROL_023_Data
    {
        public List<ROL_023_Info> GetList(int IdEmpresa, int IdSucursal, int IdNomina, int IdNominaTipoLiqui, int IdPeriodo, int IdDivision,  int IdArea, int IdDepartamento)
        {
            try
            {

                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;

                int IdDivisionIni = IdDivision;
                int IdDivisionFin = IdDivision == 0 ? 9999 : IdDivision;

                int IdAreaIni = IdArea;
                int IdAreaFin = IdArea == 0 ? 9999 : IdArea;

                int IdDepartamentoIni = IdDepartamento;
                int IdDepartamentoFin = IdDepartamento == 0 ? 9999 : IdDepartamento;

                List<ROL_023_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.SPROL_023(IdEmpresa, IdSucursalIni, IdSucursalFin, IdNomina, IdNominaTipoLiqui, IdPeriodo, IdDivisionIni, IdDivisionFin, IdAreaIni, IdAreaFin, IdDepartamentoIni, IdDepartamentoFin).Select(q => new ROL_023_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdPeriodo = q.IdPeriodo,
                        ANTICIPO = q.ANTICIPO,
                        DECIMOC = q.DECIMOC,
                        DECIMOT = q.DECIMOT,
                        DIASTRABAJADOS = q.DIASTRABAJADOS,
                        FRESERVA = q.FRESERVA,
                        IdArea = q.IdArea,
                        IdDepartamento = q.IdDepartamento,
                        IdDivision = q.IdDivision,
                        IdEmpleado = q.IdEmpleado,
                        IdNominaTipo = q.IdNominaTipo,
                        IdNominaTipoLiqui = q.IdNominaTipoLiqui,
                        IdRol = q.IdRol,
                        IdSucursal = q.IdSucursal,
                        IESS = q.IESS,
                        NETO = q.NETO,
                        NombreArea = q.NombreArea,
                        NombreDepartamento = q.NombreDepartamento,
                        NombreDivision = q.NombreDivision,
                        OTROEGR = q.OTROEGR,
                        OTROING = q.OTROING,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        PRESTAMO = q.PRESTAMO,
                        SOBRET = q.SOBRET,
                        SUELDO = q.SUELDO,
                        Su_Descripcion = q.Su_Descripcion,
                        TOTALE = q.TOTALE,
                        TOTALI = q.TOTALI,
                        Descripcion = q.Descripcion,
                        DescripcionProcesoNomina = q.DescripcionProcesoNomina,
                        JORNADA=q.JORNADA,
                        UBUCACION=q.UBUCACION,

                        FRESERVA_TOTAL = q.FRESERVA_TOTAL,
                        IESS_TOTAL = q.IESS_TOTAL,
                        Fila = q.Fila
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
