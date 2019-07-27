using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
    public class ROL_010_Data
    {
        public List<ROL_010_Info> get_list(int IdEmpresa, int IdSucursal, int IdDivision, int IdArea, int IdTipoNomina, string em_status, string Ubicacion)
        {
            try
            {
                int IdSucursalInicio = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;

                int IdDivisionInicio = IdDivision;
                int IdDivisionFin = IdDivision == 0 ? 9999 : IdDivision;

                int IdAreaInicio = IdArea;
                int IdAreaFin = IdArea == 0 ? 9999 : IdArea;

                int IdTipoNominaInicio = IdTipoNomina;
                int IdTipoNominaFin = IdTipoNomina == 0 ? 9999 : IdTipoNomina;

                List<ROL_010_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    if (em_status == "" )
                    {
                        Lista = (from q in Context.VWROL_010
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal >= IdSucursalInicio
                                 && q.IdSucursal <= IdSucursalFin
                                 && q.IdDivision >= IdDivisionInicio
                                && q.IdDivision <= IdDivisionFin
                                && q.IdArea >= IdAreaInicio
                                && q.IdArea <= IdAreaFin
                                && q.IdTipoNomina >= IdTipoNominaInicio
                                && q.IdTipoNomina <= IdTipoNominaFin
                                 select new ROL_010_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdEmpleado = q.IdEmpleado,
                                     IdDivision = q.IdDivision,
                                     ca_descripcion = q.ca_descripcion,
                                     antiguedad_string = q.antiguedad_string,
                                     Empleado = q.Empleado,
                                     em_fechaIngaRol = q.em_fechaIngaRol,
                                     em_fechaSalida = q.em_fechaSalida,
                                     em_fecha_ingreso = q.em_fecha_ingreso,
                                     EstadoEmpleado = q.EstadoEmpleado,
                                     pe_cedulaRuc = q.pe_cedulaRuc,
                                     IdTipoNomina = q.IdTipoNomina,
                                     Su_Descripcion = q.Su_Descripcion,
                                     DescDivision = q.DescDivision,
                                     DescArea = q.DescArea,
                                     de_descripcion = q.de_descripcion,
                                     pe_fechaNacimiento = q.pe_fechaNacimiento,
                                     EstadoCivil = q.EstadoCivil,
                                     edad = q.edad,
                                     CodCatalogo_Ubicacion = q.CodCatalogo_Ubicacion,
                                     UbicacionGeneral = q.UbicacionGeneral,
                                     TipoNomina = q.TipoNomina,
                                     pe_correo = q.pe_correo,
                                     pe_direccion = q.pe_direccion,
                                     pe_telfono_Contacto = q.pe_telfono_Contacto
                                 }).ToList();
                    }
                    else
                    {
                        Lista = (from q in Context.VWROL_010
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal >= IdSucursalInicio
                                 && q.IdSucursal <= IdSucursalFin
                                 && q.IdDivision >= IdDivisionInicio
                                && q.IdDivision <= IdDivisionFin
                                && q.IdArea >= IdAreaInicio
                                && q.IdArea <= IdAreaFin
                                && q.IdTipoNomina >= IdTipoNominaInicio
                                && q.IdTipoNomina <= IdTipoNominaFin
                                && em_status.Contains(q.em_status)
                                 select new ROL_010_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdEmpleado = q.IdEmpleado,
                                     IdDivision = q.IdDivision,
                                     ca_descripcion = q.ca_descripcion,
                                     antiguedad_string = q.antiguedad_string,
                                     Empleado = q.Empleado,
                                     em_fechaIngaRol = q.em_fechaIngaRol,
                                     em_fechaSalida = q.em_fechaSalida,
                                     em_fecha_ingreso = q.em_fecha_ingreso,
                                     EstadoEmpleado = q.EstadoEmpleado,
                                     pe_cedulaRuc = q.pe_cedulaRuc,
                                     IdTipoNomina = q.IdTipoNomina,
                                     Su_Descripcion = q.Su_Descripcion,
                                     DescDivision = q.DescDivision,
                                     DescArea = q.DescArea,
                                     de_descripcion = q.de_descripcion,
                                     pe_fechaNacimiento = q.pe_fechaNacimiento,
                                     EstadoCivil = q.EstadoCivil,
                                     edad = q.edad,
                                     CodCatalogo_Ubicacion = q.CodCatalogo_Ubicacion,
                                     UbicacionGeneral = q.UbicacionGeneral,
                                     TipoNomina = q.TipoNomina,
                                     pe_correo = q.pe_correo,
                                     pe_direccion = q.pe_direccion,
                                     pe_telfono_Contacto = q.pe_telfono_Contacto
                                 }).ToList();
                    }                    
                }
                if(!string.IsNullOrEmpty(Ubicacion))
                Lista = Lista.Where(q => q.CodCatalogo_Ubicacion == Ubicacion).ToList();
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
