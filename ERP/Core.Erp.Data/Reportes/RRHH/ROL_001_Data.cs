using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Reportes.RRHH;
namespace Core.Erp.Data.Reportes.RRHH
{
   public class ROL_001_Data
    {
        public List<ROL_001_Info> get_list(int IdEmpresa, int IdNomina, int IdNominaTipo, int IdPeriodo, int IdSucursal)
        {
            try
            {
                List<ROL_001_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWROL_001
                             where q.IdEmpresa == IdEmpresa
                             && ((IdSucursal!=0) ? q.IdSucursal == IdSucursal : 1==1) 
                             && q.IdNominaTipo == IdNomina
                             && q.IdNominaTipoLiqui == IdNominaTipo
                             && q.IdPeriodo == IdPeriodo
                             && q.rub_visible_reporte==true
                             select new ROL_001_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpleado = q.IdEmpleado,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 IdRubro = q.IdRubro,
                                 ru_codRolGen = q.ru_codRolGen,
                                 Empleado = q.pe_apellido + " " + q.pe_nombre,
                                 ru_descripcion = q.ru_descripcion,
                                 rub_visible_reporte = q.rub_visible_reporte,
                                 Orden = q.Orden,
                                 Valor = q.Valor,
                                 DescripcionProcesoNomina = q.DescripcionProcesoNomina,
                                 Nomina = q.Nomina,
                                 pe_FechaIni = q.pe_FechaIni,
                                 pe_FechaFin = q.pe_FechaFin,
                                 pe_estado = q.pe_estado,
                                 de_descripcion = q.de_descripcion,
                                 IdNominaTipo = q.IdNominaTipo,
                                 IdNominaTipoLiqui = q.IdNominaTipoLiqui,
                                 IdPeriodo = q.IdPeriodo,
                                 Su_Descripcion = q.Su_Descripcion,
                                 Division = q.Division,
                                 IdDivision = q.IdDivision,
                                 em_codigo = q.em_codigo,
                                 IdDepartamento = q.IdDepartamento,
                                 IdArea = q.IdArea,
                                 Area = q.Area,
                                 Periodo=q.Periodo,
                                 em_ruc=q.em_ruc

                             }).ToList();
                }
                return Lista;
            }
            catch (Exception )
            {

                throw;
            }
        }
    }
}
