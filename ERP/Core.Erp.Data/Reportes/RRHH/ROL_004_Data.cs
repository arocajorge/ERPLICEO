using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
   public class ROL_004_Data
    {
        public List<ROL_004_Info> get_list(int IdEmpresa, int IdUtilidad)
        {
            try
            {
                List<ROL_004_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWROL_004
                             where q.IdEmpresa == IdEmpresa
                             && q.IdUtilidad == IdUtilidad
                             select new ROL_004_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdPeriodo = q.IdPeriodo,
                                 UtilidadDerechoIndividual = q.UtilidadDerechoIndividual,
                                 LimiteDistribucionUtilidad = q.LimiteDistribucionUtilidad,
                                 DiasTrabajados = q.DiasTrabajados,
                                 CargasFamiliares = q.CargasFamiliares,
                                 ValorIndividual = q.ValorIndividual,
                                 ValorCargaFamiliar = q.ValorCargaFamiliar,
                                 ValorExedenteIESS = q.ValorExedenteIESS,
                                 ValorTotal = q.ValorTotal,
                                 IdEmpleado = q.IdEmpleado,
                                 Nombres = q.Nombres,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 ca_descripcion = q.ca_descripcion,
                                 em_codigo = q.em_codigo,
                                 IdUtilidad = q.IdUtilidad
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
