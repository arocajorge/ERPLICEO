using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
   public class ROL_016_Data
    {
        public List<ROL_016_Info> get_list(int IdEmpresa, decimal IdEmpleado, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                decimal IdEmpleadoIni = IdEmpleado;
                decimal IdEmpleadoFin = IdEmpleado == 0 ? 9999 : IdEmpleado;
                List<ROL_016_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWROL_016
                             where q.IdEmpresa == IdEmpresa
                             && q.IdEmpleado >= IdEmpleadoIni && q.IdEmpleado <= IdEmpleadoFin
                             select new ROL_016_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpleado = q.IdEmpleado,
                                 ca_descripcion = q.ca_descripcion,
                                 Contrato = q.Contrato ,
                                 Egresos = q.Egresos,
                                FechaIngreso = q.FechaIngreso,
                                FechaSalida = q.FechaSalida,
                                IdActaFiniquito = q.IdActaFiniquito,
                                IdCausaTerminacion = q.IdCausaTerminacion,
                                IdTipoCatalogo= q.IdTipoCatalogo,
                                Ingresos = q.Ingresos,
                                Liquido = q.Liquido,
                                Observacion = q.Observacion,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                pe_nombreCompleto = q.pe_nombreCompleto,
                                UltimaRemuneracion = q.UltimaRemuneracion
                                 

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
