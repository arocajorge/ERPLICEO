using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
    public class ROL_007_Data
    {
        public List<ROL_007_Info> get_list(int IdEmpresa, decimal IdEmpleado, int IdSolicitud)
        {
            try
            {
                List<ROL_007_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWROL_007
                             where q.IdEmpresa == IdEmpresa
                             && q.IdEmpleado == IdEmpleado
                             && q.IdSolicitud == IdSolicitud
                             select new ROL_007_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 pe_apellido = q.pe_apellido,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 IdEmpleado = q.IdEmpleado,
                                 IdSolicitud = q.IdSolicitud,
                                 Anio_Desde = q.Anio_Desde,
                                 Anio_Hasta = q.Anio_Hasta,
                                 Dias_pendiente = q.Dias_pendiente,
                                 Dias_a_disfrutar = q.Dias_a_disfrutar,
                                 Dias_q_Corresponde = q.Dias_q_Corresponde,
                                 AnioServicio = q.AnioServicio,
                                 Fecha = q.Fecha,
                                 Fecha_Desde = q.Fecha_Desde,
                                 Fecha_Hasta = q.Fecha_Hasta,
                                 Fecha_Retorno = q.Fecha_Retorno,
                                 Observacion = q.Observacion,
                                 de_descripcion = q.de_descripcion,
                                 Canceladas = q.Canceladas,
                                 Gozadas_Pgadas = q.Gozadas_Pgadas,
                                 em_fechaIngaRol = q.em_fechaIngaRol,
                                 ca_descripcion = q.ca_descripcion,
                                 pe_nombre = q.pe_nombre
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
