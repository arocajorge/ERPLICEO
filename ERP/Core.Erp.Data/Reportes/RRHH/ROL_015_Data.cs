using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
   public  class ROL_015_Data
    {
        public List<ROL_015_Info> get_list( int IdEmpresa, decimal IdEmpleado, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                decimal IdEmpleadoIni = IdEmpleado;
                decimal IdEmpleadoFin = IdEmpleado == 0 ? 9999 : IdEmpleado;
                fechaFin = Convert.ToDateTime(fechaFin.ToShortDateString());
                fechaInicio = Convert.ToDateTime(fechaInicio.ToShortDateString());

                List<ROL_015_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPROL_015(fechaInicio, fechaFin, IdEmpresa)
                             where q.IdEmpleado >= IdEmpleadoIni && q.IdEmpleado <= IdEmpleadoFin
                             select new ROL_015_Info
                             {
                                 IdEmpleado = q.IdEmpleado,
                                 IdEmpresa = q.IdEmpresa,
                                 Decimocuarto = q.Decimocuarto,
                                 DecimoTercero = q.DecimoTercero,
                                 IdNominaTipo = q.IdNominaTipo,
                                 IdNominaTipoLiqui = q.IdNominaTipoLiqui,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 Vacaciones = q.Vacaciones
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
