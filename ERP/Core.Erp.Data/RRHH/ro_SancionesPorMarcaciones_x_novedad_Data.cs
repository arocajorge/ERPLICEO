using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
  public  class ro_SancionesPorMarcaciones_x_novedad_Data
    {

        public List<ro_SancionesPorMarcaciones_x_novedad_Info> get_list(int IdEmpresa, int IdAjuste)
        {
            try
            {
                List<ro_SancionesPorMarcaciones_x_novedad_Info> lista;
                using (Entities_rrhh context = new Entities_rrhh())
                {
                    lista = (from q in context.vwro_SancionesPorMarcaciones_x_novedad

                             where q.IdEmpresa == IdEmpresa
                             && q.IdAjuste == IdAjuste
                             select new ro_SancionesPorMarcaciones_x_novedad_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdAjuste = q.IdAjuste,
                                 Secuencia = q.Secuencia,
                                 IdNovedad = q.IdNovedad,
                                 IdEmpleado = q.IdEmpleado,
                                 IdEmpresa_nov = q.IdEmpresa_nov,
                                 IdNomina_Tipo = q.IdNomina_Tipo,
                                 IdNomina_TipoLiqui = q.IdNomina_TipoLiqui,
                                 Valor = q.Valor,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_apellido = q.pe_apellido,
                                 pe_nombre = q.pe_nombre,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 em_codigo = q.em_codigo,
                             }).ToList();
                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
