using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
    public class ROL_011_Data
    {
        public List<ROL_011_Info> get_list(int IdEmpresa , int IdHorasExtras)
        {
            try
            {
                List<ROL_011_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWROL_011
                             where q.IdEmpresa == IdEmpresa
                             && q.IdHorasExtras == IdHorasExtras
                             select new ROL_011_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdHorasExtras = q.IdHorasExtras,
                                 IdNominaTipo = q.IdNominaTipo,
                                 IdNominaTipoLiqui = q.IdNominaTipoLiqui,
                                 IdPeriodo = q.IdPeriodo,
                                 IdEmpleado = q.IdEmpleado,
                                 FechaRegistro = q.FechaRegistro,
                                 time_entrada1 = q.time_entrada1,
                                 time_entrada2 = q.time_entrada2,
                                 time_salida1 = q.time_salida1,
                                 time_salida2 = q.time_salida2,
                                 hora_extra25 = q.hora_extra25,
                                 hora_extra50 = q.hora_extra50,
                                 hora_extra100 = q.hora_extra100,
                                 Valor25 = q.Valor25,
                                 Valor50 = q.Valor50,
                                 Valor100 = q.Valor100,
                                 Sueldo_base = q.Sueldo_base,
                                 hora_atraso = q.hora_atraso,
                                 hora_temprano = q.hora_temprano,
                                 hora_trabajada = q.hora_trabajada,
                                 pe_apellido = q.pe_apellido,
                                 pe_FechaFin = q.pe_FechaFin,
                                 pe_FechaIni = q.pe_FechaIni,
                                 pe_nombre = q.pe_nombre,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 ca_descripcion = q.ca_descripcion,
                                 Descripcion = q.Descripcion,
                                 DescripcionProcesoNomina = q.DescripcionProcesoNomina


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
