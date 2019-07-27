using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
    public class ROL_018_Data
    {
        public List<ROL_018_Info> get_list(int IdEmpresa, decimal IdEmpleado, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                decimal IdEmpleadoIni = IdEmpleado;
                decimal IdEmpleadoFin = IdEmpleado == 0 ? 9999 : IdEmpleado;
                fecha_fin = Convert.ToDateTime(fecha_fin.ToShortDateString());
                fecha_ini = Convert.ToDateTime(fecha_ini.ToShortDateString());
                List<ROL_018_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWROL_018
                             where q.IdEmpresa == IdEmpresa
                             && q.IdEmpleado >= IdEmpleadoIni && q.IdEmpleado <= IdEmpleadoFin
                             && q.FechaPago >= fecha_ini 
                             && q.FechaPago <= fecha_fin
                             select new ROL_018_Info
                             {
                                 IdEmpleado = q.IdEmpleado,
                                 IdEmpresa = q.IdEmpresa,
                                 Dias_a_disfrutar = q.Dias_a_disfrutar,
                                 Dias_pendiente = q.Dias_pendiente,
                                 Dias_q_Corresponde = q.Dias_q_Corresponde,
                                 FechaFin = q.FechaFin,
                                 FechaIni = q.FechaIni,
                                 FechaPago = q.FechaPago,
                                 Fecha_Desde = q.Fecha_Desde,
                                 Fecha_Hasta = q.Fecha_Hasta,
                                 IdLiquidacion = q.IdLiquidacion,
                                 IdPeriodo_Fin = q.IdPeriodo_Fin,
                                 IdPeriodo_Inicio = q.IdPeriodo_Inicio,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 Total_Remuneracion = q.Total_Remuneracion,
                                 Total_Vacaciones = q.Total_Vacaciones,
                                 Valor_Cancelar = q.Valor_Cancelar
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
