using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
  public  class ro_EmpleadoNovedadCargaMasiva_det_Data
    {

        public List<ro_EmpleadoNovedadCargaMasiva_det_Info> get_list(int IdEmpresa, decimal IdCarga)
        {
            try
            {
                List<ro_EmpleadoNovedadCargaMasiva_det_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwro_EmpleadoNovedadCargaMasiva_det
                             where q.IdEmpresa == IdEmpresa
                                   && q.IdCarga == IdCarga
                             select new ro_EmpleadoNovedadCargaMasiva_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdCarga = q.IdCarga,
                                 IdEmpleado=q.IdEmpleado,
                                 IdEmpresa_nov = q.IdEmpresa_nov,
                                 Observacion = q.Observacion,
                                 Secuancia = q.Secuencia,
                                 IdNovedad=q.IdNovedad,
                                 pe_cedulaRuc=q.pe_cedulaRuc,
                                 pe_nombre=q.pe_nombre,
                                 em_codigo=q.em_codigo,
                                 Valor=q.Valor,
                                 pe_apellido=q.pe_apellido+" "+q.pe_nombre
                                  
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
