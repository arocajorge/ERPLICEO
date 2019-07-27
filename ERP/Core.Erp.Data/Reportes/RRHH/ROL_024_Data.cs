using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
    public class ROL_024_Data
    {
        public List<ROL_024_Info> GetList(int IdEmpresa, int IdSucursal, int IdNominaTipo, int IdNominaTipoLiqui, decimal IdPeriodo)
        {
            try
            {
                List<ROL_024_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.VWROL_024.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdNominaTipo == IdNominaTipo
                    && q.IdSucursal == IdSucursal
                    && q.IdNominaTipoLiqui == IdNominaTipoLiqui
                    && q.IdPeriodo == IdPeriodo
                    ).Select(q => new ROL_024_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdPeriodo = q.IdPeriodo,
                        IdNominaTipoLiqui = q.IdNominaTipoLiqui,
                        IdNominaTipo = q.IdNominaTipo,
                        IdEmpleado = q.IdEmpleado,
                        IdRol = q.IdRol,
                        IdSucursal = q.IdSucursal,
                        Dias = q.Dias,
                        NomNomina = q.NomNomina,
                        NomNominaTipo = q.NomNominaTipo,
                        pe_cedulaRuc = q.pe_cedulaRuc,
                        pe_FechaFin = q.pe_FechaFin,
                        pe_FechaIni = q.pe_FechaIni,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        Su_Descripcion = q.Su_Descripcion,
                        Valor = q.Valor
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
