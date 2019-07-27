using Core.Erp.Info.Helps;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
    public class ROL_020_Data
    {
        public List<ROL_020_Info> GetList(int IdEmpresa, int IdSucursal, int IdNominaTipo, int IdNomina, int IdPeriodo,  int IdDivision, int IdArea, int IdProceso)
        {
            try
            {
                int IdSucursalInicio = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;

                int IdAreaInicio = IdArea;
                int IdAreaFin = IdArea == 0 ? 9999 : IdArea;

                int IdDivisionInicio = IdDivision;
                int IdDivisionFin = IdDivision == 0 ? 9999 : IdDivision;

                List<ROL_020_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.VWROL_020.Where(q => q.IdEmpresa == IdEmpresa
                     && IdSucursalInicio <= q.IdSucursal
                     && q.IdSucursal <= IdSucursalFin
                     && q.IdNominaTipo == IdNominaTipo
                     && q.IdNomina == IdNomina
                     && q.IdPeriodo == IdPeriodo
                     && IdDivisionInicio <= q.IdDivision
                     && q.IdDivision <= IdDivisionFin
                     && IdAreaInicio <= q.IdArea
                     && q.IdArea <= IdAreaFin
                     &&q.IdProceso == IdProceso
                    ).Select(q => new ROL_020_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdPeriodo = q.IdPeriodo,
                        IdNomina = q.IdNomina,
                        IdNominaTipo = q.IdNominaTipo,
                        em_codigo = q.em_codigo,
                        em_NumCta = q.em_NumCta,
                        em_tipoCta = q.em_tipoCta,
                        IdCuentaBancaria = q.IdCuentaBancaria,
                        IdArchivo = q.IdArchivo,
                        IdProceso = q.IdProceso,
                        Nombres = q.Nombres,
                        pe_cedulaRuc = q.pe_cedulaRuc,
                        Secuancia = q.Secuancia,
                        Valor = q.Valor,
                        Descripcion = q.Descripcion,
                        DescripcionProcesoNomina = q.DescripcionProcesoNomina,
                        pe_FechaFin = q.pe_FechaFin,
                        pe_FechaIni = q.pe_FechaIni,
                        Area=q.Area,
                        Division=q.Division

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
