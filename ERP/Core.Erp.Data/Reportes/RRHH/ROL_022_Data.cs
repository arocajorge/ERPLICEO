using Core.Erp.Data.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
   public class ROL_022_Data
    {
     
        public List<ROL_022_Info> get_list(int IdEmpresa, int IdNomina, int IdNominaTipo, int IdPeriodo,
       int IdSucursal, int IdDivision, int IdArea)
        {
            try
            {

                ro_rubros_calculados_Data oda_rubro_calculados = new ro_rubros_calculados_Data();
                var info_rub_calculados = oda_rubro_calculados.get_info(IdEmpresa);
                int IdSucursalInicio = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;

                int IdAreaInicio = IdArea;
                int IdAreaFin = IdArea == 0 ? 9999 : IdArea;

                int IdDivisionInicio = IdDivision;
                int IdDivisionFin = IdDivision == 0 ? 9999 : IdDivision;

                List<ROL_022_Info> Lista = new List<ROL_022_Info>();
                using (Entities_reportes Context = new Entities_reportes())

                {

                    Lista = (from q in Context.SPROL_022(IdEmpresa, IdNomina, IdNominaTipo, IdPeriodo)
                             where q.IdEmpresa == IdEmpresa
                             && q.IdDivision >= IdDivisionInicio
                             && q.IdDivision <= IdDivisionFin
                             && q.IdArea >= IdAreaInicio
                             && q.IdArea <= IdAreaFin
                             && q.IdSucursal >= IdSucursalInicio
                             && q.IdSucursal <= IdSucursalFin
                             && q.IdNomina_Tipo == IdNomina
                             && q.IdNomina_TipoLiqui == IdNominaTipo
                             && q.IdPeriodo == IdPeriodo
                             && q.Valor > 0
                             //&& q.IdRubro==info_rub_calculados.IdRubro_tot_pagar
                             select new ROL_022_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdDivision = q.IdDivision,
                                 IdArea = q.IdArea,
                                 IdEmpleado = q.IdEmpleado,
                                 IdJornada = q.IdJornada,
                                 IdNomina_Tipo = q.IdNomina_Tipo,
                                 IdPeriodo = q.IdPeriodo,
                                 Descripcion = q.Descripcion,
                                 ca_descripcion = q.ca_descripcion,
                                 ru_descripcion = q.ru_descripcion,
                                 empleado = q.empleado,
                                 ru_orden = q.ru_orden,
                                 Valor = q.Valor,
                                 ru_tipo = q.ru_tipo,
                                 Su_Descripcion = q.Su_Descripcion,
                                 FechaIni = q.FechaIni,
                                 FechaFin = q.FechaFin,
                                 NomArea = q.NomArea,
                                 NomDivision = q.NomDivision,
                                 NomNomina = q.NomNomina,
                                 NomNominaTipo = q.NomNominaTipo,
                                IdRubro= q.IdRubro
                                 
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
