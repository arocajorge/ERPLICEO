using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

                List<ROL_010_Info> Lista = new List<ROL_010_Info>();
                using (SqlConnection connection = new SqlConnection(ConexionERP.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT DISTINCT "
                    + " dbo.ro_empleado.IdEmpresa, dbo.ro_empleado.IdSucursal, dbo.ro_empleado.IdDivision, dbo.ro_empleado.IdArea, dbo.ro_empleado.IdEmpleado, dbo.ro_contrato.IdNomina IdTipoNomina, su.Su_Descripcion, "
                    + " di.Descripcion AS DescDivision, ar.Descripcion AS DescArea, de.de_descripcion, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_apellido + ' ' + dbo.tb_persona.pe_nombre AS Empleado, dbo.ro_empleado.FechaNacimiento, "
                    + " cat.ca_descripcion AS EstadoCivil, datediff(year, dbo.tb_persona.pe_fechaNacimiento, getdate()) AS edad, ro_empleado.em_fechaIngaRol em_fecha_ingreso, CAST(dbo.ro_empleado.em_fechaSalida AS date) em_fechaSalida, "
                    + " dbo.ro_cargo.ca_descripcion, dbo.ro_catalogo.ca_descripcion AS EstadoEmpleado, dbo.ro_empleado.em_status, dbo.ro_empleado.em_fechaIngaRol, cast(CASE WHEN IIF(em_fechaSalida = NULL, datediff(dayofyear, em_fechaSalida, "
                    + " em_fechaIngaRol), datediff(dayofyear, em_fechaIngaRol, getdate())) >= 360 THEN IIF(em_fechaSalida = NULL, datediff(dayofyear, em_fechaSalida, em_fechaIngaRol), datediff(dayofyear, em_fechaIngaRol, getdate())) "
                    + " / 360 ELSE 0 END AS varchar(20)) + ' año(s), ' + cast(CASE WHEN IIF(em_fechaSalida = NULL, datediff(dayofyear, em_fechaSalida, em_fechaIngaRol), datediff(dayofyear, em_fechaIngaRol, getdate())) >= 30 THEN(IIF(em_fechaSalida = NULL, "
                    + " datediff(dayofyear, em_fechaSalida, em_fechaIngaRol), datediff(dayofyear, em_fechaIngaRol, getdate())) / 30) - (CASE WHEN IIF(em_fechaSalida = NULL, datediff(dayofyear, em_fechaSalida, em_fechaIngaRol), datediff(dayofyear, "
                    + " em_fechaIngaRol, getdate())) >= 360 THEN IIF(em_fechaSalida = NULL, datediff(dayofyear, em_fechaSalida, em_fechaIngaRol), datediff(dayofyear, em_fechaIngaRol, getdate())) / 360 ELSE 0 END * 12) ELSE 0 END AS varchar(20)) "
                    + " + ' mes(es)' AS antiguedad_string, ro_empleado.CodCatalogo_Ubicacion, CA.ca_descripcion UbicacionGeneral, nt.Descripcion TipoNomina, dbo.tb_persona.pe_correo, dbo.ro_empleado.Celular, dbo.ro_empleado.Telefono,  dbo.ro_empleado.Direccion, "
                    + " cat1.ca_descripcion sexo "
                    + " FROM dbo.ro_catalogo INNER JOIN "
                    + " dbo.tb_persona INNER JOIN "
                    + " dbo.ro_empleado ON dbo.tb_persona.IdPersona = dbo.ro_empleado.IdPersona INNER JOIN "
                    + " tb_sucursal su ON su.IdEmpresa = dbo.ro_empleado.IdEmpresa AND su.IdSucursal = dbo.ro_empleado.IdSucursal INNER JOIN "
                    + " ro_Division di ON di.IdEmpresa = dbo.ro_empleado.IdEmpresa AND di.IdDivision = dbo.ro_empleado.IdDivision INNER JOIN "
                    + " ro_area ar ON ar.IdEmpresa = dbo.ro_empleado.IdEmpresa AND ar.IdArea = dbo.ro_empleado.IdArea INNER JOIN "
                    + " ro_Departamento de ON de.IdEmpresa = dbo.ro_empleado.IdEmpresa AND de.IdDepartamento = dbo.ro_empleado.IdDepartamento LEFT JOIN "
                    + " tb_Catalogo cat ON cat.CodCatalogo = dbo.ro_empleado.CodCatalogoEstadoCivil LEFT JOIN "
                    + " tb_Catalogo cat1 ON cat1.CodCatalogo = dbo.ro_empleado.CodCatalogoSexo INNER JOIN "
                    + " dbo.ro_cargo ON dbo.ro_empleado.IdEmpresa = dbo.ro_cargo.IdEmpresa AND dbo.ro_empleado.IdCargo = dbo.ro_cargo.IdCargo ON dbo.ro_catalogo.CodCatalogo = dbo.ro_empleado.em_status INNER JOIN "
                    + " dbo.ro_contrato ON dbo.ro_empleado.IdEmpresa = dbo.ro_contrato.IdEmpresa AND dbo.ro_empleado.IdEmpleado = dbo.ro_contrato.IdEmpleado LEFT JOIN "
                    + " ro_catalogo AS CA ON CA.CodCatalogo = ro_empleado.CodCatalogo_Ubicacion INNER JOIN "
                    + " dbo.ro_Nomina_Tipo nt ON nt.IdEmpresa = dbo.ro_empleado.IdEmpresa AND nt.IdNomina_Tipo = dbo.ro_contrato.IdNomina "
                    + " WHERE(dbo.ro_empleado.em_estado = 'A') "
                    + " and dbo.ro_empleado.IdEmpresa = " + IdEmpresa.ToString()
                    + " and dbo.ro_empleado.IdSucursal between " + IdSucursalInicio.ToString() + " and " + IdSucursalFin.ToString()
                    + " and dbo.ro_empleado.IdDivision between " + IdDivisionInicio.ToString() + " and " + IdDivisionFin.ToString()
                    + " and dbo.ro_empleado.IdArea between " + IdAreaInicio.ToString() + " and " + IdAreaFin.ToString()
                    + " and dbo.ro_contrato.IdNomina between " + IdTipoNominaInicio.ToString() + " and " + IdTipoNominaFin.ToString();
                    if (em_status!="")
                    {
                        query += " and dbo.ro_empleado.em_status = '" + em_status.ToString() +"'";
                    }
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ROL_010_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdEmpleado = Convert.ToDecimal(reader["IdEmpleado"]),
                            IdDivision = string.IsNullOrEmpty(reader["IdDivision"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdDivision"]),
                            ca_descripcion = string.IsNullOrEmpty(reader["ca_descripcion"].ToString()) ? null : reader["ca_descripcion"].ToString(),
                            antiguedad_string = string.IsNullOrEmpty(reader["antiguedad_string"].ToString()) ? null : reader["antiguedad_string"].ToString(),
                            Empleado = string.IsNullOrEmpty(reader["Empleado"].ToString()) ? null : reader["Empleado"].ToString(),
                            em_fechaIngaRol = string.IsNullOrEmpty(reader["em_fechaIngaRol"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["em_fechaIngaRol"]),
                            em_fechaSalida = string.IsNullOrEmpty(reader["em_fechaSalida"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["em_fechaSalida"]),
                            em_fecha_ingreso = string.IsNullOrEmpty(reader["em_fecha_ingreso"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["em_fecha_ingreso"]),
                            EstadoEmpleado = string.IsNullOrEmpty(reader["EstadoEmpleado"].ToString()) ? null : reader["EstadoEmpleado"].ToString(),
                            pe_cedulaRuc = string.IsNullOrEmpty(reader["pe_cedulaRuc"].ToString()) ? null : reader["pe_cedulaRuc"].ToString(),
                            IdTipoNomina = string.IsNullOrEmpty(reader["IdTipoNomina"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdTipoNomina"]),
                            Su_Descripcion = string.IsNullOrEmpty(reader["Su_Descripcion"].ToString()) ? null : reader["Su_Descripcion"].ToString(),
                            DescDivision = string.IsNullOrEmpty(reader["DescDivision"].ToString()) ? null : reader["DescDivision"].ToString(),
                            DescArea = string.IsNullOrEmpty(reader["DescArea"].ToString()) ? null : reader["DescArea"].ToString(),
                            de_descripcion = string.IsNullOrEmpty(reader["de_descripcion"].ToString()) ? null : reader["de_descripcion"].ToString(),
                            pe_fechaNacimiento = string.IsNullOrEmpty(reader["FechaNacimiento"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["FechaNacimiento"]),
                            EstadoCivil = string.IsNullOrEmpty(reader["EstadoCivil"].ToString()) ? null : reader["EstadoCivil"].ToString(),
                            edad = string.IsNullOrEmpty(reader["edad"].ToString()) ? (int?)null : Convert.ToInt32(reader["edad"]),
                            CodCatalogo_Ubicacion = string.IsNullOrEmpty(reader["CodCatalogo_Ubicacion"].ToString()) ? null : reader["CodCatalogo_Ubicacion"].ToString(),
                            UbicacionGeneral = string.IsNullOrEmpty(reader["UbicacionGeneral"].ToString()) ? null : reader["UbicacionGeneral"].ToString(),
                            TipoNomina = string.IsNullOrEmpty(reader["TipoNomina"].ToString()) ? null : reader["TipoNomina"].ToString(),
                            pe_correo = string.IsNullOrEmpty(reader["pe_correo"].ToString()) ? null : reader["pe_correo"].ToString(),
                            pe_direccion = string.IsNullOrEmpty(reader["Direccion"].ToString()) ? null : reader["Direccion"].ToString(),
                            pe_telfono_Contacto = string.IsNullOrEmpty(reader["Telefono"].ToString()) ? null : reader["Telefono"].ToString(),
                            sexo = string.IsNullOrEmpty(reader["sexo"].ToString()) ? null : reader["sexo"].ToString(),
                            
                        });
                    }
                    reader.Close();
                }
                /*
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
                                     pe_telfono_Contacto = q.pe_telfono_Contacto,
                                     sexo = q.sexo
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
                                     pe_telfono_Contacto = q.pe_telfono_Contacto,
                                     sexo = q.sexo
                                 }).ToList();
                    }                    
                }
                */
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
