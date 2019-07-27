using Core.Erp.Data.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
   public class ROL_021_Data
    {
        public List<ROL_021_Info> get_list(int IdEmpresa, int IdNomina, int IdNominaTipo, int IdPeriodo, 
            int IdSucursal,int IdDivision, int IdArea, string tipoRubro)
        {
            try
            {

                ro_rubros_calculados_Data oda_rubro_calculados = new ro_rubros_calculados_Data();
                var info_rub_calculados = oda_rubro_calculados.get_info(IdEmpresa);
                info_rub_calculados.IdRubro_bono_x_antiguedad = "70";
                int IdSucursalInicio = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;

                int IdAreaInicio = IdArea;
                int IdAreaFin = IdArea == 0 ? 9999 : IdArea;

                int IdDivisionInicio = IdDivision;
                int IdDivisionFin = IdDivision == 0 ? 9999 : IdDivision;

                List<ROL_021_Info> Lista=new List<ROL_021_Info>();
                using (Entities_reportes Context = new Entities_reportes())

                {
                    if (tipoRubro == "E")
                    {
                        Lista = (from q in Context.VWROL_021
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdPeriodo == IdPeriodo
                                 && q.IdDivisionEmpleado >= IdDivisionInicio
                                 && q.IdDivisionEmpleado <= IdDivisionFin
                                 && q.IdAreaEmpleado >= IdAreaInicio
                                 && q.IdAreaEmpleado <= IdAreaFin
                                 && IdSucursalInicio <= q.IdSucursal && q.IdSucursal  <= IdSucursalFin
                                 && q.IdNominaTipo == IdNomina
                                 && q.IdNominaTipoLiqui == IdNominaTipo
                                 && (q.Valor > 0
                                 || q.IdRubro == info_rub_calculados.IdRubro_DIII
                                 || q.IdRubro == info_rub_calculados.IdRubro_DIV
                                 || q.IdRubro == info_rub_calculados.IdRubro_fondo_reserva)
                                 && (q.IdRubro == info_rub_calculados.IdRubro_anticipo || q.IdRubro == info_rub_calculados.IdRubro_tot_egr || q.ru_tipo == "E")
                                 select new ROL_021_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdRol = q.IdRol,
                                     IdSucursal = q.IdSucursal,
                                     IdNominaTipo = q.IdNominaTipo,
                                     IdNominaTipoLiqui = q.IdNominaTipoLiqui,
                                     IdPeriodo = q.IdPeriodo,
                                     IdRubro = q.IdRubro,
                                     Orden = q.Orden,
                                     Valor = q.Valor,
                                     rub_visible_reporte = q.rub_visible_reporte,
                                     Observacion = q.Observacion,
                                     ru_descripcion = q.ru_descripcion,
                                     pe_FechaFin = q.pe_FechaFin,
                                     pe_FechaIni = q.pe_FechaIni,
                                     ru_tipo = q.ru_tipo,
                                     rub_codigo = q.rub_codigo,
                                     ru_codRolGen = q.ru_codRolGen,
                                     ca_descripcion = q.ca_descripcion,
                                     em_codigo = q.em_codigo,
                                     IdEmpleado = q.IdEmpleado,
                                     pe_cedulaRuc = q.pe_cedulaRuc,
                                     pe_nombreCompleto = q.pe_nombreCompleto,
                                     IdArea = q.IdArea,
                                     IdDivisionEmpleado = q.IdDivisionEmpleado,
                                     Descripcion = q.Descripcion,
                                     rub_grupo = q.rub_grupo,
                                     Dias = q.Dias,
                                     AreaEmpleado = q.AreaEmpleado,
                                     Division = q.Division,
                                     su_Descripcion = q.Su_Descripcion,
                                     IdAreaEmpleado = q.IdAreaEmpleado,
                                     IDividion = q.IDividion,
                                     NominaTipoLiqui = q.NominaTipoLiqui,
                                     Porcentaje = q.Porcentaje,
                                     se_distribuye =q.se_distribuye,
                                     NominaTipo = q.NominaTipo,
                                     

                                 }).ToList();
                    }
                    else if(tipoRubro == "I")
                    {
                        Lista = (from q in Context.VWROL_021
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdPeriodo == IdPeriodo
                                 && q.IdDivisionEmpleado >= IdDivisionInicio
                                 && q.IdDivisionEmpleado <= IdDivisionFin
                                 && q.IdAreaEmpleado >= IdAreaInicio
                                 && q.IdAreaEmpleado <= IdAreaFin
                                 && IdSucursalInicio <= q.IdSucursal && q.IdSucursal <= IdSucursalFin
                                 && q.IdNominaTipo == IdNomina
                                 && q.IdNominaTipoLiqui == IdNominaTipo
                                 && (q.Valor > 0
                                 || q.IdRubro == info_rub_calculados.IdRubro_DIII
                                 || q.IdRubro == info_rub_calculados.IdRubro_DIV
                                 || q.IdRubro == info_rub_calculados.IdRubro_fondo_reserva
                                  )
                                 && ( q.ru_tipo == "I" 
                                 //|| q.IdRubro == info_rub_calculados.IdRubro_tot_ing
                                 )
                                 && q.IdRubro != info_rub_calculados.IdRubro_tot_ing
                                 select new ROL_021_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdRol = q.IdRol,
                                     IdSucursal = q.IdSucursal,
                                     IdNominaTipo = q.IdNominaTipo,
                                     IdNominaTipoLiqui = q.IdNominaTipoLiqui,
                                     IdPeriodo = q.IdPeriodo,
                                     IdRubro = q.IdRubro,
                                     Orden = q.Orden,
                                     Valor = q.Valor,
                                     rub_visible_reporte = q.rub_visible_reporte,
                                     Observacion = q.Observacion,
                                     ru_descripcion = q.ru_descripcion,
                                     pe_FechaFin = q.pe_FechaFin,
                                     pe_FechaIni = q.pe_FechaIni,
                                     ru_tipo = q.ru_tipo,
                                     rub_codigo = q.rub_codigo,
                                     ru_codRolGen = q.ru_codRolGen,
                                     ca_descripcion = q.ca_descripcion,
                                     em_codigo = q.em_codigo,
                                     IdEmpleado = q.IdEmpleado,
                                     pe_cedulaRuc = q.pe_cedulaRuc,
                                     pe_nombreCompleto = q.pe_nombreCompleto,
                                     IdArea = q.IdArea,
                                     IdDivisionEmpleado = q.IdDivisionEmpleado,
                                     Descripcion = q.Descripcion,
                                     rub_grupo = q.rub_grupo,
                                     Dias = q.Dias,
                                     AreaEmpleado = q.AreaEmpleado,
                                     Division = q.Division,
                                     su_Descripcion = q.Su_Descripcion,
                                     IdAreaEmpleado = q.IdAreaEmpleado,
                                     IDividion = q.IDividion,
                                     NominaTipoLiqui = q.NominaTipoLiqui,
                                     Porcentaje = q.Porcentaje,
                                     se_distribuye = q.se_distribuye,
                                     NominaTipo = q.NominaTipo,
                                 }).ToList();
                    }
                    else
                    {
                        Lista = (from q in Context.VWROL_021
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdPeriodo == IdPeriodo
                                 && q.IdDivisionEmpleado >= IdDivisionInicio
                                 && q.IdDivisionEmpleado <= IdDivisionFin
                                 && q.IdAreaEmpleado >= IdAreaInicio
                                 && q.IdAreaEmpleado <= IdAreaFin
                                 && IdSucursalInicio <= q.IdSucursal && q.IdSucursal <= IdSucursalFin
                                 && q.IdNominaTipo == IdNomina
                                 && q.IdNominaTipoLiqui == IdNominaTipo
                                 && ( q.Valor > 0 
                                 || q.IdRubro== info_rub_calculados.IdRubro_DIII
                                 || q.IdRubro == info_rub_calculados.IdRubro_DIV
                                 || q.IdRubro == info_rub_calculados.IdRubro_fondo_reserva)
                                 && (
                                 (
                                 q.IdRubro == info_rub_calculados.IdRubro_tot_pagar 
                                 || q.IdRubro == info_rub_calculados.IdRubro_tot_ing 
                                 || q.IdRubro == info_rub_calculados.IdRubro_tot_egr
                                 )
                                 ||( q.ru_tipo == "I" || q.ru_tipo == "E")
                                 )
                                 select new ROL_021_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdRol = q.IdRol,
                                     IdSucursal = q.IdSucursal,
                                     IdNominaTipo = q.IdNominaTipo,
                                     IdNominaTipoLiqui = q.IdNominaTipoLiqui,
                                     IdPeriodo = q.IdPeriodo,
                                     IdRubro = q.IdRubro,
                                     Orden = q.Orden,
                                     Valor = q.Valor,
                                     rub_visible_reporte = q.rub_visible_reporte,
                                     Observacion = q.Observacion,
                                     ru_descripcion = q.ru_descripcion,
                                     pe_FechaFin = q.pe_FechaFin,
                                     pe_FechaIni = q.pe_FechaIni,
                                     ru_tipo = q.ru_tipo,
                                     rub_codigo = q.rub_codigo,
                                     ru_codRolGen = q.ru_codRolGen,
                                     ca_descripcion = q.ca_descripcion,
                                     em_codigo = q.em_codigo,
                                     IdEmpleado = q.IdEmpleado,
                                     pe_cedulaRuc = q.pe_cedulaRuc,
                                     pe_nombreCompleto = q.pe_nombreCompleto,
                                     IdArea = q.IdArea,
                                     IdDivisionEmpleado = q.IdDivisionEmpleado,
                                     Descripcion = q.Descripcion,
                                     rub_grupo = q.rub_grupo,
                                     Dias = q.Dias,
                                     AreaEmpleado = q.AreaEmpleado,
                                     Division = q.Division,
                                     su_Descripcion = q.Su_Descripcion,
                                     IdAreaEmpleado = q.IdAreaEmpleado,
                                     IDividion = q.IDividion,
                                     NominaTipoLiqui = q.NominaTipoLiqui,
                                     Porcentaje = q.Porcentaje,
                                     se_distribuye = q.se_distribuye,
                                     NominaTipo = q.NominaTipo,
                                 }).ToList();
                    }

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
