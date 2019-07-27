using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
    public class ROL_005_Data
    {
        public List<ROL_005_Info> get_list(int IdEmpresa, decimal IdActaFiniquito)
        {
            try
            {
                List<ROL_005_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWROL_005
                             where q.IdEmpresa == IdEmpresa
                             && q.IdActaFiniquito == IdActaFiniquito
                             select new ROL_005_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdActaFiniquito = q.IdActaFiniquito,
                                 IdCausaTerminacion = q.IdCausaTerminacion,
                                 IdContrato = q.IdContrato,
                                 FechaIngreso = q.FechaIngreso,
                                 FechaSalida = q.FechaSalida,
                                 UltimaRemuneracion = q.UltimaRemuneracion,
                                 Observacion = q.Observacion,
                                 Ingresos = q.Ingresos,
                                 Egresos = q.Egresos,
                                 EsMujerEmbarazada = q.EsMujerEmbarazada,
                                 EsDirigenteSindical = q.EsDirigenteSindical,
                                 EsPorDiscapacidad = q.EsPorDiscapacidad,
                                 EsPorEnfermedadNoProfesional = q.EsPorEnfermedadNoProfesional,
                                 IdSecuencia = q.IdSecuencia,
                                 DescripcionDetalle = q.DescripcionDetalle,
                                 Valor = q.Valor,
                                 IdCargo = q.IdCargo,
                                 NumDocumento = q.NumDocumento,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_apellido = q.pe_apellido,
                                 pe_nombre = q.pe_nombre,
                                 ca_descripcion = q.ca_descripcion,
                                 ru_descripcion = q.ru_descripcion,
                                 IdEmpleado = q.IdEmpleado,
                                 NombreCompleto = q.NombreCompleto,
                                 Su_Descripcion = q.Su_Descripcion,
                                 TipoTerminacion = q.TipoTerminacion
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
