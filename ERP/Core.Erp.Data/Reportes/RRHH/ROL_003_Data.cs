using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
    public class ROL_003_Data
    {
        public List<ROL_003_Info> get_list(int IdEmpresa, decimal IdEmpleado, decimal IdNovedad)
        {
            try
            {
                List<ROL_003_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWROL_003
                             where q.IdEmpresa == IdEmpresa
                             && q.IdEmpleado == IdEmpleado
                             && q.IdNovedad == IdNovedad
                             select new ROL_003_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpleado = q.IdEmpleado,
                                 IdPersona = q.IdPersona,
                                 IdNovedad = q.IdNovedad,
                                 FechaPago = q.FechaPago,
                                 Valor = q.Valor,
                                 Fecha_Transac = q.Fecha_Transac,
                                 ca_descripcion = q.ca_descripcion,
                                 pe_apellido = q.pe_apellido,
                                 pe_nombre = q.pe_nombre,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 DescripcionProcesoNomina = q.DescripcionProcesoNomina,
                                 Observacion = q.Observacion,
                                 ru_descripcion = q.ru_descripcion,
                                 EstadoCobro = q.EstadoCobro,
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
