using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
    public class ROL_025_Data
    {
        public List<ROL_025_Info> get_list(int IdEmpresa, int IdSucursal, int IdNomina_Tipo, int IdPeriodo, int IdDivision, int IdArea)
        {
            try
            {
                int IdSucursalInicio = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;

                int IdDivisionInicio = IdDivision;
                int IdDivisionFin = IdDivision == 0 ? 9999 : IdDivision;

                int IdAreaInicio = IdArea;
                int IdAreaFin = IdArea == 0 ? 9999 : IdArea;

                List<ROL_025_Info> Lista = new List<ROL_025_Info>();
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPROL_025(IdEmpresa, IdSucursalInicio, IdSucursalFin, IdNomina_Tipo, IdPeriodo, IdDivisionInicio, IdDivisionFin, IdAreaInicio, IdAreaFin)
                             select new ROL_025_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdRol = q.IdRol,
                                 IdEmpleado = q.IdEmpleado,
                                 pe_apellido = q.pe_apellido+' '+ q.pe_nombre,                               
                                 ca_descripcion = q.ca_descripcion,
                                 FechaInicio = q.FechaInicio,
                                 pe_FechaFin = q.pe_FechaFin,
                                 pe_FechaIni = q.pe_FechaIni,
                                 Descripcion = q.Descripcion,
                                 dias = q.dias,
                                 IdPeriodo = q.IdPeriodo,
                                 IdRubro = q.IdRubro,
                                 IdPersona = q.IdPersona,
                                 Valor = q.Valor,
                                 IdRow = q.IdRow,
                                 Division = q.Division,
                                 Area = q.Area
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
