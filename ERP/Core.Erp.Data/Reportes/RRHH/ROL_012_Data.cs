using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
   public class ROL_012_Data
    {
        public List<ROL_012_Info> get_list(int IdEmpresa,  DateTime fecha_inicio, DateTime fecha_fin, string IdRubro)
        {
            try
            {
                List<ROL_012_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPROL_012(IdEmpresa,  fecha_inicio, fecha_fin, IdRubro)
                             select new ROL_012_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdDepartamento = q.IdDepartamento,
                                 IdEmpleado = q.IdEmpleado,
                                 IdPrestamo = q.IdPrestamo,
                                 pe_apellido = q.pe_apellido,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_nombre = q.pe_apellido + " " + q.pe_nombre,
                                 EstadoPago = q.EstadoPago,
                                 de_descripcion = q.de_descripcion,
                                 Total_Cancelado = q.Total_Cancelado,
                                 Total_Pendiente_pago = q.Total_Pendiente_pago,
                                 Total_Prestamo = q.Total_Prestamo,
                                 Observacion = q.Observacion,
                                 IdRubro = q.IdRubro,
                                 ru_descripcion = q.ru_descripcion
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
