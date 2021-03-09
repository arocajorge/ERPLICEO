using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
using Core.Erp.Info.Helps;
using Core.Erp.Data.General;
using System.Data.SqlClient;

namespace Core.Erp.Data.RRHH
{
   public class ro_empleado_Data
    {
        #region Variables
        ro_division_Data data_division = new ro_division_Data();
        ro_area_Data data_area = new ro_area_Data();
        ro_departamento_Data data_departamento = new ro_departamento_Data();
        ro_cargo_Data data_cargo = new ro_cargo_Data();
        ro_rubro_tipo_Data data_rubro = new ro_rubro_tipo_Data();
        ro_horario_Data data_horario = new ro_horario_Data();
        ro_turno_Data data_turno = new ro_turno_Data();
        ro_empleado_x_rubro_acumulado_Data data_rubro_acumulado = new ro_empleado_x_rubro_acumulado_Data();
        ro_nomina_tipo_Data data_tipo_nomina = new ro_nomina_tipo_Data();
        ro_contrato_Data data_contrato = new ro_contrato_Data();
        tb_persona_Data data_persona = new tb_persona_Data();
        #endregion

        public List<ro_empleado_Info> get_list_combo(int IdEmpresa)
        {
            try
            {
                List<ro_empleado_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                        Lista = (from q in Context.vwro_empleado_combo
                                 where q.IdEmpresa == IdEmpresa
                                 select new ro_empleado_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdEmpleado = q.IdEmpleado,
                                     Empleado=q.Empleado,
                                     pe_cedulaRuc=q.pe_cedulaRuc,
                                     IdTipoNomina= q.IdNomina,
                                     IdSucursal=q.IdSucursal
                                 }).ToList();
                  
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_empleado_Info> get_list_combo_liquidar(int IdEmpresa)
        {
            try
            {
                List<ro_empleado_Info> Lista;
                string estado = cl_enumeradores.eEstadoEmpleadoRRHH.EST_LIQ.ToString()+","+cl_enumeradores.eEstadoEmpleadoRRHH.EST_PLQ.ToString();
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwro_empleado_combo
                             where q.IdEmpresa == IdEmpresa
                             && (q.em_status== "EST_LIQ" || q.em_status== "EST_PLQ")
                             select new ro_empleado_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpleado = q.IdEmpleado,
                                 Empleado = q.Empleado,
                                 pe_cedulaRuc = q.pe_cedulaRuc
                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_empleado_Info> get_list(int IdEmpresa, int IdSucursal, string em_status, bool mostrar_anulados)
        {
            try
            {

                List<ro_empleado_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if (em_status=="")
                    {
                        if (mostrar_anulados)
                            Lista = (from q in Context.vwro_empleados_consulta
                                     where q.IdEmpresa == IdEmpresa
                                     && q.IdSucursal == IdSucursal
                                     select new ro_empleado_Info
                                     {
                                         IdEmpresa = q.IdEmpresa,
                                         IdEmpleado = q.IdEmpleado,
                                         IdPersona = q.IdPersona,
                                         pe_cedulaRuc = q.pe_cedulaRuc,
                                         em_estado = q.em_estado,
                                         em_status = q.em_status,
                                         Empleado = q.Empleado,
                                         em_codigo = q.em_codigo,
                                         em_fechaIngaRol = q.em_fechaIngaRol,
                                         EstadoBool = q.em_estado == "A" ? true : false
                                     }).ToList();
                        else
                            Lista = (from q in Context.vwro_empleados_consulta
                                     where q.IdEmpresa == IdEmpresa
                                     && q.IdSucursal == IdSucursal
                                     && q.em_estado == "A"
                                     select new ro_empleado_Info
                                     {
                                         IdEmpresa = q.IdEmpresa,
                                         IdEmpleado = q.IdEmpleado,
                                         IdPersona = q.IdPersona,
                                         pe_cedulaRuc = q.pe_cedulaRuc,
                                         em_estado = q.em_estado,
                                         em_status = q.em_status,
                                         Empleado = q.Empleado,
                                         em_codigo = q.em_codigo,
                                         em_fechaIngaRol = q.em_fechaIngaRol,

                                         EstadoBool = q.em_estado == "A" ? true : false
                                     }).ToList();
                    }
                    else
                    {
                        if (mostrar_anulados)
                            Lista = (from q in Context.vwro_empleados_consulta
                                     where q.IdEmpresa == IdEmpresa
                                     && q.IdSucursal == IdSucursal
                                     && q.em_status == em_status
                                     select new ro_empleado_Info
                                     {
                                         IdEmpresa = q.IdEmpresa,
                                         IdEmpleado = q.IdEmpleado,
                                         IdPersona = q.IdPersona,
                                         pe_cedulaRuc = q.pe_cedulaRuc,
                                         em_estado = q.em_estado,
                                         em_status = q.em_status,
                                         Empleado = q.Empleado,
                                         em_codigo = q.em_codigo,
                                         em_fechaIngaRol = q.em_fechaIngaRol,
                                         EstadoBool = q.em_estado == "A" ? true : false
                                     }).ToList();
                        else
                            Lista = (from q in Context.vwro_empleados_consulta
                                     where q.IdEmpresa == IdEmpresa
                                     && q.IdSucursal == IdSucursal
                                     && q.em_status == em_status
                                     && q.em_estado == "A"
                                     select new ro_empleado_Info
                                     {
                                         IdEmpresa = q.IdEmpresa,
                                         IdEmpleado = q.IdEmpleado,
                                         IdPersona = q.IdPersona,
                                         pe_cedulaRuc = q.pe_cedulaRuc,
                                         em_estado = q.em_estado,
                                         em_status = q.em_status,
                                         Empleado = q.Empleado,
                                         em_codigo = q.em_codigo,
                                         em_fechaIngaRol = q.em_fechaIngaRol,

                                         EstadoBool = q.em_estado == "A" ? true : false
                                     }).ToList();
                    }
                    
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<ro_empleado_Info> get_list_profesores(int IdEmpresa)
        {
            try
            {
                List<ro_empleado_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwro_empleado_x_jornada
                             where q.IdEmpresa == IdEmpresa
                             && q.Pago_por_horas==true
                             select new ro_empleado_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpleado = q.IdEmpleado,
                                 Empleado = q.Empleado,
                                 IdTipoNomina = q.IdNomina,
                                 IdSucursal = q.IdSucursal,
                                 Valor_horas_matutino=q.Valor_horas_matutino,
                                 Valor_horas_vespertina=q.Valor_horas_vespertina,
                                 Valor_horas_brigada=q.Valor_horas_brigada,
                                 Valor_hora_adicionales = q.Valor_hora_adicionales,
                                 Valor_hora_control_salida = q.Valor_hora_control_salida,  
                                 pe_cedulaRuc=q.pe_cedulaRuc
                                
                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_empleado_Info get_info(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                ro_empleado_Info info_ = new ro_empleado_Info();

                using (SqlConnection connection = new SqlConnection(ConexionERP.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT e.IdEmpresa, e.IdEmpleado_Supervisor, e.IdPersona, e.IdSucursal, e.IdTipoEmpleado, e.em_codigo, e.Codigo_Biometrico, e.em_lugarNacimiento, e.em_CarnetIees, e.em_cedulaMil, e.em_fechaSalida, e.em_fechaIngaRol, e.em_tipoCta, "
                    + " e.em_NumCta, e.em_estado, e.IdCodSectorial, e.IdDepartamento, e.IdTipoSangre, e.IdCargo, e.IdCtaCble_Emplea, e.IdCiudad, e.em_mail, e.IdTipoLicencia, e.IdBanco, e.IdArea, e.IdDivision, e.por_discapacidad, e.carnet_conadis, "
                    + " e.talla_pant, e.talla_camisa, e.talla_zapato, e.em_status, e.IdCondicionDiscapacidadSRI, e.IdTipoIdentDiscapacitadoSustitutoSRI, e.IdentDiscapacitadoSustitutoSRI, e.IdAplicaConvenioDobleImposicionSRI, e.IdTipoResidenciaSRI, "
                    + " e.IdTipoSistemaSalarioNetoSRI, e.es_AcreditaHorasExtras, e.IdTipoAnticipo, e.ValorAnticipo, e.CodigoSectorial, e.em_AnticipoSueldo, e.Marca_Biometrico, e.IdHorario, p.pe_Naturaleza, p.pe_nombre, p.IdTipoDocumento, p.pe_cedulaRuc, "
                    + " p.pe_correo, s.Su_Descripcion, de.de_descripcion, c.ca_descripcion, di.Descripcion, e.IdEmpleado, p.pe_apellido, e.Tiene_ingresos_compartidos, e.Pago_por_horas, e.Valor_horas_vespertina, "
                    + " e.Valor_horas_matutino, e.Valor_maximo_horas_vesp, e.Valor_maximo_horas_mat, e.Valor_horas_brigada, e.GozaMasDeQuinceDiasVaciones, e.DiasVacaciones, e.IdEmpleadoPAdre, e.Valor_hora_adicionales, "
                    + " e.Valor_hora_control_salida, e.CodCatalogo_Ubicacion, e.IdCtaCble_x_pagar_empleado, e.IdSucursalContabilizacion, e.Direccion, e.Telefono, e.Celular, e.CodCatalogoSexo, e.CodCatalogoEstadoCivil, e.FechaNacimiento "
                    + " FROM     dbo.tb_persona AS p INNER JOIN "
                    + " dbo.ro_empleado AS e ON p.IdPersona = e.IdPersona INNER JOIN "
                    + " dbo.ro_cargo AS c ON e.IdEmpresa = c.IdEmpresa AND e.IdCargo = c.IdCargo AND e.IdEmpresa = c.IdEmpresa AND e.IdCargo = c.IdCargo INNER JOIN "
                    + " dbo.ro_Departamento AS de ON e.IdEmpresa = de.IdEmpresa AND e.IdDepartamento = de.IdDepartamento AND e.IdEmpresa = de.IdEmpresa AND e.IdDepartamento = de.IdDepartamento INNER JOIN "
                    + " dbo.tb_sucursal AS s ON e.IdEmpresa = s.IdEmpresa AND e.IdSucursal = s.IdSucursal AND e.IdEmpresa = s.IdEmpresa AND e.IdSucursal = s.IdSucursal INNER JOIN "
                    + " dbo.ro_Division AS di ON e.IdEmpresa = di.IdEmpresa AND e.IdDivision = di.IdDivision "
                    + " WHERE e.IdEmpresa = " + IdEmpresa.ToString() + "and e.IdEmpleado = " + IdEmpleado.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info_ = new ro_empleado_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdEmpleado = Convert.ToDecimal(reader["IdEmpleado"]),
                            IdEmpleado_Supervisor = string.IsNullOrEmpty(reader["IdEmpleado_Supervisor"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdEmpleado_Supervisor"]),
                            IdPersona = Convert.ToDecimal(reader["IdPersona"]),
                            IdSucursal = Convert.ToInt32(reader["IdSucursal"]),
                            IdTipoEmpleado = string.IsNullOrEmpty(reader["IdTipoEmpleado"].ToString()) ? null : reader["IdTipoEmpleado"].ToString(),
                            em_codigo = string.IsNullOrEmpty(reader["em_codigo"].ToString()) ? null : reader["em_codigo"].ToString(),
                            Codigo_Biometrico = string.IsNullOrEmpty(reader["Codigo_Biometrico"].ToString()) ? null : reader["Codigo_Biometrico"].ToString(),
                            em_lugarNacimiento = string.IsNullOrEmpty(reader["em_lugarNacimiento"].ToString()) ? null : reader["em_lugarNacimiento"].ToString(),
                            em_CarnetIees = string.IsNullOrEmpty(reader["em_CarnetIees"].ToString()) ? null : reader["em_CarnetIees"].ToString(),
                            em_cedulaMil = string.IsNullOrEmpty(reader["em_cedulaMil"].ToString()) ? null : reader["em_cedulaMil"].ToString(),
                            em_fechaIngaRol = string.IsNullOrEmpty(reader["em_fechaIngaRol"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["em_fechaIngaRol"]),
                            em_tipoCta = string.IsNullOrEmpty(reader["em_tipoCta"].ToString()) ? null : reader["em_tipoCta"].ToString(),
                            em_NumCta = string.IsNullOrEmpty(reader["em_NumCta"].ToString()) ? null : reader["em_NumCta"].ToString(),
                            em_estado = string.IsNullOrEmpty(reader["em_estado"].ToString()) ? null : reader["em_estado"].ToString(),
                            IdCodSectorial = string.IsNullOrEmpty(reader["IdCodSectorial"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCodSectorial"]),
                            IdDepartamento = Convert.ToInt32(reader["IdDepartamento"]),
                            IdTipoSangre = string.IsNullOrEmpty(reader["IdTipoSangre"].ToString()) ? null : reader["IdTipoSangre"].ToString(),
                            IdCargo = string.IsNullOrEmpty(reader["IdCargo"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCargo"]),
                            IdCtaCble_Emplea = string.IsNullOrEmpty(reader["IdCtaCble_Emplea"].ToString()) ? null : reader["IdCtaCble_Emplea"].ToString(),
                            IdCiudad = string.IsNullOrEmpty(reader["IdCiudad"].ToString()) ? null : reader["IdCiudad"].ToString(),
                            em_mail = string.IsNullOrEmpty(reader["em_mail"].ToString()) ? null : reader["em_mail"].ToString(),
                            IdTipoLicencia = string.IsNullOrEmpty(reader["IdTipoLicencia"].ToString()) ? null : reader["IdTipoLicencia"].ToString(),
                            IdBanco = string.IsNullOrEmpty(reader["IdBanco"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdBanco"]),
                            IdArea = string.IsNullOrEmpty(reader["IdArea"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdArea"]),
                            IdDivision = string.IsNullOrEmpty(reader["IdDivision"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdDivision"]),
                            por_discapacidad = Convert.ToDouble(reader["por_discapacidad"]),
                            carnet_conadis = string.IsNullOrEmpty(reader["carnet_conadis"].ToString()) ? null : reader["carnet_conadis"].ToString(),
                            talla_pant = string.IsNullOrEmpty(reader["talla_pant"].ToString()) ? (double?)null : Convert.ToDouble(reader["talla_pant"]),
                            talla_camisa = string.IsNullOrEmpty(reader["talla_camisa"].ToString()) ? null : reader["talla_camisa"].ToString(),
                            talla_zapato = string.IsNullOrEmpty(reader["talla_zapato"].ToString()) ? (double?)null : Convert.ToDouble(reader["talla_zapato"]),
                            em_status = string.IsNullOrEmpty(reader["em_status"].ToString()) ? null : reader["em_status"].ToString(),
                            IdCondicionDiscapacidadSRI = string.IsNullOrEmpty(reader["IdCondicionDiscapacidadSRI"].ToString()) ? null : reader["IdCondicionDiscapacidadSRI"].ToString(),
                            IdTipoIdentDiscapacitadoSustitutoSRI = string.IsNullOrEmpty(reader["IdTipoIdentDiscapacitadoSustitutoSRI"].ToString()) ? null : reader["IdTipoIdentDiscapacitadoSustitutoSRI"].ToString(),
                            IdentDiscapacitadoSustitutoSRI = string.IsNullOrEmpty(reader["IdentDiscapacitadoSustitutoSRI"].ToString()) ? null : reader["IdentDiscapacitadoSustitutoSRI"].ToString(),
                            IdAplicaConvenioDobleImposicionSRI = string.IsNullOrEmpty(reader["IdAplicaConvenioDobleImposicionSRI"].ToString()) ? null : reader["IdAplicaConvenioDobleImposicionSRI"].ToString(),
                            IdTipoResidenciaSRI = string.IsNullOrEmpty(reader["IdTipoResidenciaSRI"].ToString()) ? null : reader["IdTipoResidenciaSRI"].ToString(),
                            IdTipoSistemaSalarioNetoSRI = string.IsNullOrEmpty(reader["IdTipoSistemaSalarioNetoSRI"].ToString()) ? null : reader["IdTipoSistemaSalarioNetoSRI"].ToString(),
                            es_AcreditaHorasExtras = string.IsNullOrEmpty(reader["es_AcreditaHorasExtras"].ToString()) ? false : Convert.ToBoolean(reader["es_AcreditaHorasExtras"]),
                            IdTipoAnticipo = string.IsNullOrEmpty(reader["IdTipoAnticipo"].ToString()) ? null : reader["IdTipoAnticipo"].ToString(),
                            ValorAnticipo = string.IsNullOrEmpty(reader["ValorAnticipo"].ToString()) ? (double?)null : Convert.ToDouble(reader["ValorAnticipo"]),
                            CodigoSectorial = string.IsNullOrEmpty(reader["CodigoSectorial"].ToString()) ? null : reader["CodigoSectorial"].ToString(),
                            em_AnticipoSueldo = string.IsNullOrEmpty(reader["em_AnticipoSueldo"].ToString()) ? (double?)null : Convert.ToDouble(reader["em_AnticipoSueldo"]),
                            Marca_Biometrico = string.IsNullOrEmpty(reader["Marca_Biometrico"].ToString()) ? false : Convert.ToBoolean(reader["Marca_Biometrico"]),
                            IdHorario = string.IsNullOrEmpty(reader["IdHorario"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdHorario"]),
                            Tiene_ingresos_compartidos = string.IsNullOrEmpty(reader["Tiene_ingresos_compartidos"].ToString()) ? false : Convert.ToBoolean(reader["Tiene_ingresos_compartidos"]),
                            pe_cedulaRuc = string.IsNullOrEmpty(reader["pe_cedulaRuc"].ToString()) ? null : reader["pe_cedulaRuc"].ToString(),
                            pe_nombre = string.IsNullOrEmpty(reader["pe_nombre"].ToString()) ? null : reader["pe_nombre"].ToString(),
                            pe_apellido = string.IsNullOrEmpty(reader["pe_apellido"].ToString()) ? null : reader["pe_apellido"].ToString(),
                            pe_sexo = string.IsNullOrEmpty(reader["CodCatalogoSexo"].ToString()) ? null : reader["CodCatalogoSexo"].ToString(),
                            IdEstadoCivil = string.IsNullOrEmpty(reader["CodCatalogoEstadoCivil"].ToString()) ? null : reader["CodCatalogoEstadoCivil"].ToString(),
                            pe_direccion = string.IsNullOrEmpty(reader["Direccion"].ToString()) ? null : reader["Direccion"].ToString(),
                            pe_telfono_Contacto = string.IsNullOrEmpty(reader["Telefono"].ToString()) ? null : reader["Telefono"].ToString(),
                            pe_celular = string.IsNullOrEmpty(reader["Celular"].ToString()) ? null : reader["Celular"].ToString(),
                            IdTipoDocumento = string.IsNullOrEmpty(reader["IdTipoDocumento"].ToString()) ? null : reader["IdTipoDocumento"].ToString(),
                            pe_correo = string.IsNullOrEmpty(reader["pe_correo"].ToString()) ? null : reader["pe_correo"].ToString(),
                            pe_fechaNacimiento = string.IsNullOrEmpty(reader["FechaNacimiento"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["FechaNacimiento"]),
                            Pago_por_horas = string.IsNullOrEmpty(reader["Pago_por_horas"].ToString()) ? false : Convert.ToBoolean(reader["Pago_por_horas"]),
                            Valor_horas_vespertina = string.IsNullOrEmpty(reader["Valor_horas_vespertina"].ToString()) ? (double?)null : Convert.ToDouble(reader["Valor_horas_vespertina"]),
                            Valor_horas_matutino = string.IsNullOrEmpty(reader["Valor_horas_matutino"].ToString()) ? (double?)null : Convert.ToDouble(reader["Valor_horas_matutino"]),
                            Valor_horas_brigada = string.IsNullOrEmpty(reader["Valor_horas_brigada"].ToString()) ? (double?)null : Convert.ToDouble(reader["Valor_horas_brigada"]),
                            Valor_hora_adicionales = string.IsNullOrEmpty(reader["Valor_hora_adicionales"].ToString()) ? (double?)null : Convert.ToDouble(reader["Valor_hora_adicionales"]),
                            Valor_hora_control_salida = string.IsNullOrEmpty(reader["Valor_hora_control_salida"].ToString()) ? (double?)null : Convert.ToDouble(reader["Valor_hora_control_salida"]),
                            Valor_maximo_horas_mat = string.IsNullOrEmpty(reader["Valor_maximo_horas_mat"].ToString()) ? (double?)null : Convert.ToDouble(reader["Valor_maximo_horas_mat"]),
                            Valor_maximo_horas_vesp = string.IsNullOrEmpty(reader["Valor_maximo_horas_vesp"].ToString()) ? (double?)null : Convert.ToDouble(reader["Valor_maximo_horas_vesp"]),
                            DiasVacaciones = Convert.ToDouble(reader["DiasVacaciones"]),
                            GozaMasDeQuinceDiasVaciones = string.IsNullOrEmpty(reader["GozaMasDeQuinceDiasVaciones"].ToString()) ? false : Convert.ToBoolean(reader["GozaMasDeQuinceDiasVaciones"]),
                            CodCatalogo_Ubicacion = string.IsNullOrEmpty(reader["CodCatalogo_Ubicacion"].ToString()) ? null : reader["CodCatalogo_Ubicacion"].ToString(),
                            IdCtaCble_x_pagar_empleado = string.IsNullOrEmpty(reader["IdCtaCble_x_pagar_empleado"].ToString()) ? null : reader["IdCtaCble_x_pagar_empleado"].ToString(),
                            IdSucursalContabilizacion = string.IsNullOrEmpty(reader["IdSucursalContabilizacion"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdSucursalContabilizacion"]),
                        };
                    }
                }
                /*
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    vwro_empleado_datos_generales info = Context.vwro_empleado_datos_generales.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdEmpleado == IdEmpleado);
                    if (info == null)
                        return null;

                    info_ = new ro_empleado_Info
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdEmpleado = info.IdEmpleado,
                        IdEmpleado_Supervisor = info.IdEmpleado_Supervisor,
                        IdPersona = info.IdPersona,
                        IdSucursal = info.IdSucursal,
                        IdTipoEmpleado = info.IdTipoEmpleado,
                        em_codigo = (info.em_codigo) == null ? info.IdEmpleado.ToString() : info.em_codigo,
                        Codigo_Biometrico = info.Codigo_Biometrico,
                        em_lugarNacimiento = info.em_lugarNacimiento,
                        em_CarnetIees = info.em_CarnetIees,
                        em_cedulaMil = info.em_cedulaMil,
                        em_fechaIngaRol = info.em_fechaIngaRol,
                        em_tipoCta = info.em_tipoCta,
                        em_NumCta = info.em_NumCta,
                        em_estado = info.em_estado,
                        IdCodSectorial = info.IdCodSectorial,
                        IdDepartamento = info.IdDepartamento,
                        IdTipoSangre = info.IdTipoSangre,
                        IdCargo = info.IdCargo,
                        IdCtaCble_Emplea = info.IdCtaCble_Emplea,
                        IdCiudad = info.IdCiudad,
                        em_mail = info.em_mail,
                        IdTipoLicencia = info.IdTipoLicencia,
                        IdBanco = info.IdBanco,
                        IdArea = info.IdArea,
                        IdDivision = info.IdDivision,
                        por_discapacidad = info.por_discapacidad,
                        carnet_conadis = info.carnet_conadis,
                        talla_pant = info.talla_pant,
                        talla_camisa = info.talla_camisa,
                        talla_zapato = info.talla_zapato,
                        em_status = info.em_status,
                        IdCondicionDiscapacidadSRI = info.IdCondicionDiscapacidadSRI,
                        IdTipoIdentDiscapacitadoSustitutoSRI = info.IdTipoIdentDiscapacitadoSustitutoSRI,
                        IdentDiscapacitadoSustitutoSRI = info.IdentDiscapacitadoSustitutoSRI,
                        IdAplicaConvenioDobleImposicionSRI = info.IdAplicaConvenioDobleImposicionSRI,
                        IdTipoResidenciaSRI = info.IdTipoResidenciaSRI,
                        IdTipoSistemaSalarioNetoSRI = info.IdTipoSistemaSalarioNetoSRI,
                        es_AcreditaHorasExtras = info.es_AcreditaHorasExtras,
                        IdTipoAnticipo = info.IdTipoAnticipo,
                        ValorAnticipo = info.ValorAnticipo,
                        CodigoSectorial = info.CodigoSectorial,
                        em_AnticipoSueldo = info.em_AnticipoSueldo,
                        Marca_Biometrico = info.Marca_Biometrico,
                        IdHorario = info.IdHorario,
                        Tiene_ingresos_compartidos = info.Tiene_ingresos_compartidos,
                        pe_cedulaRuc = info.pe_cedulaRuc,
                        pe_nombre = info.pe_nombre,
                        pe_apellido = info.pe_apellido,
                        pe_sexo = info.pe_sexo,
                        IdEstadoCivil = info.IdEstadoCivil,
                        pe_direccion = info.pe_direccion,
                        pe_telfono_Contacto = info.pe_telfono_Contacto,
                        pe_celular = info.pe_celular,
                        IdTipoDocumento = info.IdTipoDocumento,
                        pe_correo = info.pe_correo,
                        pe_fechaNacimiento = info.pe_fechaNacimiento,
                        Pago_por_horas = info.Pago_por_horas,
                        Valor_horas_vespertina = info.Valor_horas_vespertina,
                        Valor_horas_matutino = info.Valor_horas_matutino,
                        Valor_horas_brigada = info.Valor_horas_brigada,
                        Valor_hora_adicionales = info.Valor_hora_adicionales,
                        Valor_hora_control_salida=info.Valor_hora_control_salida,
                        Valor_maximo_horas_mat = info.Valor_maximo_horas_mat,
                        Valor_maximo_horas_vesp=info.Valor_maximo_horas_vesp,
                        DiasVacaciones = info.DiasVacaciones,
                        GozaMasDeQuinceDiasVaciones = info.GozaMasDeQuinceDiasVaciones,
                        CodCatalogo_Ubicacion = info.CodCatalogo_Ubicacion,
                        IdCtaCble_x_pagar_empleado=info.IdCtaCble_x_pagar_empleado,
                        IdSucursalContabilizacion = info.IdSucursalContabilizacion
                    };
                }
                */
                return info_;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_empleado
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdEmpleado) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_empleado_Info info)
        {
            try
            {
                ro_rubros_calculados_Data ru_calclados = new ro_rubros_calculados_Data();

                var info_rub_calculados = ru_calclados.get_info(info.IdEmpresa);
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado Entity = new ro_empleado
                    {
                        IdEmpresa	=info.IdEmpresa	,
                        IdEmpleado	=info.IdEmpleado= get_id(info.IdEmpresa)	,
                        IdEmpleado_Supervisor=info.IdEmpleado_Supervisor,
                        IdPersona	=info.IdPersona,
                        IdSucursal	=info.IdSucursal,
                        IdTipoEmpleado	=info.IdTipoEmpleado,
                        em_codigo	=(info.em_codigo) ==null?info.IdEmpleado.ToString():info.em_codigo,
                        Codigo_Biometrico	=info.Codigo_Biometrico,
                        em_lugarNacimiento	=info.em_lugarNacimiento,
                        em_CarnetIees	=info.em_CarnetIees,
                        em_cedulaMil	=info.em_cedulaMil	,
                        em_fechaSalida	=info.em_fechaSalida	,
                        em_fechaIngaRol	=info.	em_fechaIngaRol	,
                        em_tipoCta	=info.em_tipoCta	,
                        em_NumCta	=info.em_NumCta	,                     
                        em_estado	=info.	em_estado,                    
                        IdCodSectorial	=info.IdCodSectorial	,
                        IdDepartamento	=info.IdDepartamento	,
                        IdTipoSangre	=info.IdTipoSangre	,
                        IdCargo	=info.	IdCargo,
                        IdCtaCble_Emplea	=info.IdCtaCble_Emplea	,
                        IdCiudad	=info.IdCiudad	,
                        em_mail	=info.em_mail	,
                        IdTipoLicencia	=info.IdTipoLicencia	,
                        IdBanco	=info.IdBanco	,
                        IdArea	=info.IdArea	,
                        IdDivision	=info.IdDivision	,
                        Fecha_UltMod	=info.Fecha_UltMod	,
                        por_discapacidad	=info.	por_discapacidad	,
                        carnet_conadis	=info.	carnet_conadis	,
                        talla_pant	=info.	talla_pant	,
                        talla_camisa	=info.	talla_camisa	,
                        talla_zapato	=info.	talla_zapato	,
                        em_status	=info.	em_status	,
                        IdCondicionDiscapacidadSRI	=info.	IdCondicionDiscapacidadSRI	,
                        IdTipoIdentDiscapacitadoSustitutoSRI	=info.	IdTipoIdentDiscapacitadoSustitutoSRI	,
                        IdentDiscapacitadoSustitutoSRI	=info.	IdentDiscapacitadoSustitutoSRI	,
                        IdAplicaConvenioDobleImposicionSRI	=info.	IdAplicaConvenioDobleImposicionSRI	,
                        IdTipoResidenciaSRI	=info.	IdTipoResidenciaSRI	,
                        IdTipoSistemaSalarioNetoSRI	=info.	IdTipoSistemaSalarioNetoSRI	,
                        es_AcreditaHorasExtras	=info.	es_AcreditaHorasExtras	,
                        IdTipoAnticipo	=info.	IdTipoAnticipo	,
                        ValorAnticipo	=info.	ValorAnticipo	,
                        CodigoSectorial	=info.	CodigoSectorial	,
                        em_AnticipoSueldo	=info.em_AnticipoSueldo	,
                        Marca_Biometrico	=info.	Marca_Biometrico	,
                        IdHorario	=info.	IdHorario	,
                        Tiene_ingresos_compartidos=info.Tiene_ingresos_compartidos,
                        IdUsuario = info.IdUsuario,
                        Fecha_Transaccion = info.Fecha_Transaccion=DateTime.Now,
                        Pago_por_horas = info.Pago_por_horas,
                        Valor_horas_vespertina = info.Valor_horas_vespertina,
                        Valor_horas_brigada = info.Valor_horas_brigada,
                        Valor_horas_matutino = info.Valor_horas_matutino,
                        Valor_hora_adicionales = info.Valor_hora_adicionales,
                        Valor_hora_control_salida = info.Valor_hora_control_salida,
                        Valor_maximo_horas_mat = info.Valor_maximo_horas_mat,
                        Valor_maximo_horas_vesp = info.Valor_maximo_horas_vesp,
                       
                        DiasVacaciones = info.DiasVacaciones,
                        GozaMasDeQuinceDiasVaciones = info.GozaMasDeQuinceDiasVaciones,
                        CodCatalogo_Ubicacion = info.CodCatalogo_Ubicacion,
                        IdCtaCble_x_pagar_empleado=info.IdCtaCble_x_pagar_empleado,
                        IdSucursalContabilizacion = info.IdSucursalContabilizacion,

                        FechaNacimiento=info.pe_fechaNacimiento,
                        Direccion=info.pe_direccion,
                        CodCatalogoEstadoCivil = info.IdEstadoCivil,
                        CodCatalogoSexo=info.pe_sexo,
                        Telefono=info.pe_telfono_Contacto,
                        Celular=info.pe_celular
                        
                    };
                    Context.ro_empleado.Add(Entity);


                    if (info_rub_calculados != null)
                    {
                        if(info_rub_calculados.IdRubro_DIII!=null)
                        {
                            ro_empleado_x_rubro_acumulado d3 = new ro_empleado_x_rubro_acumulado
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdEmpleado=info.IdEmpleado,
                                IdRubro=info_rub_calculados.IdRubro_DIII,
                                Fec_Inicio_Acumulacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                                Fec_Fin_Acumulacion= new DateTime(DateTime.Now.Year,11, 30),
                                FechaIngresa =DateTime.Now,
                                UsuarioIngresa=info.IdUsuario
                            
                            };
                            Context.ro_empleado_x_rubro_acumulado.Add(d3);
                        }
                        if (info_rub_calculados.IdRubro_DIV != null)
                        {
                            ro_empleado_x_rubro_acumulado d4 = new ro_empleado_x_rubro_acumulado
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdEmpleado = info.IdEmpleado,
                                IdRubro = info_rub_calculados.IdRubro_DIV,
                                Fec_Inicio_Acumulacion = new DateTime(DateTime.Now.Year-1, 3, 1),
                                Fec_Fin_Acumulacion = new DateTime(DateTime.Now.Year, 2, 28),
                                FechaIngresa = DateTime.Now,

                                UsuarioIngresa = info.IdUsuario

                            };
                            Context.ro_empleado_x_rubro_acumulado.Add(d4);
                        }

                        if (info_rub_calculados.IdRubro_fondo_reserva != null)
                        {
                            ro_empleado_x_rubro_acumulado fr = new ro_empleado_x_rubro_acumulado
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdEmpleado = info.IdEmpleado,
                                IdRubro = info_rub_calculados.IdRubro_fondo_reserva,
                                FechaIngresa = DateTime.Now,
                                UsuarioIngresa = info.IdUsuario

                            };
                            Context.ro_empleado_x_rubro_acumulado.Add(fr);
                        }
                    }

                    if(info.Tiene_ingresos_compartidos)
                    {
                        if(info.lst_empleado_area!=null)
                        {
                            foreach (var item in info.lst_empleado_area)
                            {
                                ro_empleado_x_division_x_area emp_div = new ro_empleado_x_division_x_area
                                {
                                    IdEmpresa=info.IdEmpresa,
                                    IdEmpleado=info.IdEmpleado,
                                    IdArea=item.IdArea,
                                    IDividion=item.IDividion,
                                    Observacion=item.Observacion,
                                    Secuencia=item.Secuencia,
                                    Porcentaje=item.Porcentaje,
                                    CargaGasto = item.CargaGasto
                                };
                                Context.ro_empleado_x_division_x_area.Add(emp_div);
                            }
                        }
                    }
                    //int secuencia = 1;
                    if(info.lst_det.Count()>0)
                    {
                        foreach (var item in info.lst_det)
                        {
                            Context.ro_empleado_x_jornada.Add(new ro_empleado_x_jornada
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdEmpleado = info.IdEmpleado,
                                IdJornada = item.IdJornada,
                                MaxNumHoras = item.MaxNumHoras,
                                ValorHora = item.ValorHora,
                                Secuencia = item.Secuencia
                            });
                        }
                    }

             
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(ro_empleado_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado Entity = Context.ro_empleado.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEmpleado == info.IdEmpleado);
                    if (Entity == null)
                        return false;
                        Entity.IdEmpleado_Supervisor = info.IdEmpleado_Supervisor;
                        Entity.IdPersona = info.IdPersona;
                        Entity.IdSucursal = info.IdSucursal;
                        Entity.IdTipoEmpleado = info.IdTipoEmpleado;
                        Entity.em_codigo = info.em_codigo;
                        Entity.Codigo_Biometrico = info.Codigo_Biometrico;
                        Entity.em_lugarNacimiento = info.em_lugarNacimiento;
                        Entity.em_CarnetIees = info.em_CarnetIees;
                        Entity.em_cedulaMil = info.em_cedulaMil  ;
                        Entity.em_fechaIngaRol = info.em_fechaIngaRol ;
                        Entity.em_tipoCta = info.em_tipoCta    ;
                        Entity.em_NumCta = info.em_NumCta ;
                        Entity.em_estado = info.em_estado;
                        Entity.IdCodSectorial = info.IdCodSectorial    ;
                        Entity.IdDepartamento = info.IdDepartamento    ;
                        Entity.IdTipoSangre = info.IdTipoSangre  ;
                        Entity.IdCargo = info.IdCargo;
                        Entity.IdCtaCble_Emplea = info.IdCtaCble_Emplea  ;
                        Entity.IdCiudad = info.IdCiudad  ;
                        Entity.em_mail = info.em_mail   ;
                        Entity.IdTipoLicencia = info.IdTipoLicencia    ;
                        Entity.IdBanco = info.IdBanco   ;
                        Entity.Pago_por_horas = info.Pago_por_horas;
                        Entity.Valor_horas_vespertina = info.Valor_horas_vespertina;
                        Entity.Valor_horas_brigada = info.Valor_horas_brigada;
                        Entity.Valor_horas_matutino = info.Valor_horas_matutino;
                        Entity.Valor_maximo_horas_mat = info.Valor_maximo_horas_mat;
                        Entity.Valor_maximo_horas_vesp = info.Valor_maximo_horas_vesp;
                        Entity.Valor_hora_adicionales = info.Valor_hora_adicionales;
                        Entity.Valor_hora_control_salida = info.Valor_hora_control_salida;
                        Entity.IdArea = info.IdArea;
                        Entity.IdDivision = info.IdDivision;
                        Entity.Fecha_UltMod = info.Fecha_UltMod  ;
                        Entity.por_discapacidad = info.por_discapacidad    ;
                        Entity.carnet_conadis = info.carnet_conadis  ;
                        Entity.talla_pant = info.talla_pant  ;
                        Entity.talla_camisa = info.talla_camisa    ;
                        Entity.talla_zapato = info.talla_zapato    ;
                        Entity.em_status = info.em_status   ;
                        Entity.IdCondicionDiscapacidadSRI = info.IdCondicionDiscapacidadSRI  ;
                        Entity.IdTipoIdentDiscapacitadoSustitutoSRI = info.IdTipoIdentDiscapacitadoSustitutoSRI    ;
                        Entity.IdentDiscapacitadoSustitutoSRI = info.IdentDiscapacitadoSustitutoSRI  ;
                        Entity.IdAplicaConvenioDobleImposicionSRI = info.IdAplicaConvenioDobleImposicionSRI  ;
                        Entity.IdTipoResidenciaSRI = info.IdTipoResidenciaSRI ;
                        Entity.IdTipoSistemaSalarioNetoSRI = info.IdTipoSistemaSalarioNetoSRI ;
                        Entity.es_AcreditaHorasExtras = info.es_AcreditaHorasExtras  ;
                        Entity.IdTipoAnticipo = info.IdTipoAnticipo  ;
                        Entity.ValorAnticipo = info.ValorAnticipo   ;
                        Entity.CodigoSectorial = info.CodigoSectorial ;
                        Entity.em_AnticipoSueldo = info.em_AnticipoSueldo   ;
                        Entity.Marca_Biometrico = info.Marca_Biometrico    ;
                        Entity.IdHorario = info.IdHorario   ;
                        Entity.IdUsuario = info.IdUsuarioUltModi;
                        Entity.Fecha_UltMod = info.Fecha_Transaccion = DateTime.Now;
                        Entity.Tiene_ingresos_compartidos = info.Tiene_ingresos_compartidos;
                        Entity.DiasVacaciones = info.DiasVacaciones;
                        Entity.GozaMasDeQuinceDiasVaciones = info.GozaMasDeQuinceDiasVaciones;
                        Entity.IdUsuarioUltModi = info.IdUsuarioUltModi;
                        Entity.CodCatalogo_Ubicacion = info.CodCatalogo_Ubicacion;
                        Entity.IdCtaCble_x_pagar_empleado = info.IdCtaCble_x_pagar_empleado;
                        Entity.IdSucursalContabilizacion = info.IdSucursalContabilizacion;

                        Entity.FechaNacimiento = info.pe_fechaNacimiento;
                        Entity.Direccion = info.pe_direccion;
                        Entity.CodCatalogoEstadoCivil = info.IdEstadoCivil;
                        Entity.CodCatalogoSexo = info.pe_sexo;
                        Entity.Telefono = info.pe_telfono_Contacto;
                        Entity.Celular = info.pe_celular;

                    var lst_delete = Context.ro_empleado_x_division_x_area.Where(v => v.IdEmpresa == info.IdEmpresa && v.IdEmpleado == info.IdEmpleado);
                    Context.ro_empleado_x_division_x_area.RemoveRange(lst_delete);
                    if (info.Tiene_ingresos_compartidos)
                    {
                        if (info.lst_empleado_area != null)
                        {
                           foreach (var item in info.lst_empleado_area)
                            {
                                ro_empleado_x_division_x_area emp_div = new ro_empleado_x_division_x_area
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdEmpleado = info.IdEmpleado,
                                    IdArea = item.IdArea,
                                    IDividion = item.IDividion,
                                    Observacion = item.Observacion,
                                    Secuencia = item.Secuencia,
                                    Porcentaje=item.Porcentaje,
                                    CargaGasto = item.CargaGasto
                                };
                                Context.ro_empleado_x_division_x_area.Add(emp_div);
                            }
                        }
                    }
                    var lst_det = Context.ro_empleado_x_jornada.Where(v => v.IdEmpresa == info.IdEmpresa && v.IdEmpleado == info.IdEmpleado);
                    Context.ro_empleado_x_jornada.RemoveRange(lst_det);

                    if (info.lst_det.Count() > 0)
                    {
                        foreach (var item in info.lst_det)
                        {
                            Context.ro_empleado_x_jornada.Add(new ro_empleado_x_jornada
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdEmpleado = info.IdEmpleado,
                                IdJornada = item.IdJornada,
                                MaxNumHoras = item.MaxNumHoras,
                                ValorHora = item.ValorHora,
                                Secuencia = item.Secuencia
                            });
                        }
                    }
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(ro_empleado_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado Entity = Context.ro_empleado.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEmpleado == info.IdEmpleado);
                    if (Entity == null)
                        return false;
                    Entity.em_estado = info.em_estado = "I";
                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.em_status = "EST_LIQ";
                    Entity.Fecha_UltAnu = info.Fecha_UltAnu = DateTime.Now;
                    if(info.Tiene_ingresos_compartidos)
                    {
                        ro_empleado Entity_comp = Context.ro_empleado.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEmpleadoPAdre == info.IdEmpleado);
                        if (Entity_comp == null)
                            return false;
                        Entity_comp.em_estado = info.em_estado = "I";
                        Entity_comp.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                        Entity_comp.Fecha_UltAnu = info.Fecha_UltAnu = DateTime.Now;
                    }
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificar_estadoDB(int IdEmpresa, decimal IdEmpleado, string em_status, DateTime fecha_ingreso, DateTime fecha_salida)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado Entity = Context.ro_empleado.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdEmpleado == IdEmpleado);
                    if (Entity == null)
                        return false;

                    Entity.em_status = em_status;
                    //Entity.em_fechaIngaRol = fecha_ingreso;
                    //Entity.em_fechaSalida = fecha_salida;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB_importacion(int IdEmpresa, List<ro_division_Info> Lista_Division, List<ro_area_Info> Lista_Area, List<ro_departamento_Info> Lista_Departamento, List<ro_cargo_Info> Lista_Cargo,
        List<ro_rubro_tipo_Info> Lista_Rubro, List<ro_horario_Info> Lista_Horario, List<ro_turno_Info> Lista_Turno, List<ro_empleado_Info> Lista_Empleado, List<ro_empleado_x_rubro_acumulado_Info> Lista_RubrosAcumulados, List<ro_nomina_tipo_Info> Lista_TipoNomina, List<ro_contrato_Info> Lista_Contrato,
        List<ro_cargaFamiliar_Info> Lista_CargasFamiliares, List<ro_rol_detalle_x_rubro_acumulado_Info> Lista_ProvisionesAcumuladas, List<ro_historico_vacaciones_x_empleado_Info> Lista_VacacionesList)
        {
            Entities_rrhh Context_rrhh = new Entities_rrhh();
            Entities_general Context_general = new Entities_general();
            var IdRubro = data_rubro.get_id(IdEmpresa);
            var IdEmpleado = get_id(IdEmpresa);
            
            try
            {
                foreach (var item in Lista_Division)
                {
                    ro_Division Entity_division = new ro_Division
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdDivision = item.IdDivision,
                        Descripcion = item.Descripcion,
                        estado = item.estado,
                        IdUsuario = item.IdUsuario,
                        Fecha_Transac = DateTime.Now

                    };

                    Context_rrhh.ro_Division.Add(Entity_division);
                }

                foreach (var item in Lista_Area)
                {
                    ro_area Entity_area = new ro_area
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdDivision = item.IdDivision,
                        IdArea = item.IdArea,
                        Descripcion = item.Descripcion,
                        estado = item.estado,
                        IdUsuario = item.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };

                    Context_rrhh.ro_area.Add(Entity_area);
                }

                foreach (var item in Lista_Departamento)
                {
                    ro_Departamento Entity_departamento = new ro_Departamento
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdDepartamento = item.IdDepartamento,
                        de_descripcion = item.de_descripcion,
                        Estado = item.Estado,
                        IdUsuario = item.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };

                    Context_rrhh.ro_Departamento.Add(Entity_departamento);
                }

                foreach (var item in Lista_Cargo)
                {
                    ro_cargo Entity_cargo = new ro_cargo
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdCargo = item.IdCargo,
                        ca_descripcion = item.ca_descripcion,
                        Estado = item.Estado,
                        IdUsuario = item.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };

                    Context_rrhh.ro_cargo.Add(Entity_cargo);
                }

                foreach (var item in Lista_Rubro)
                {                    
                    ro_rubro_tipo Entity_rubro = new ro_rubro_tipo
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdRubro =  Convert.ToString(IdRubro++),
                        rub_codigo = item.rub_codigo,
                        ru_codRolGen = item.ru_codRolGen,
                        ru_descripcion = item.ru_descripcion,
                        NombreCorto = item.NombreCorto,
                        ru_tipo = item.ru_tipo,
                        ru_estado = item.ru_estado,
                        ru_orden = item.ru_orden,
                        rub_concep = item.rub_concep,
                        rub_ctacon = item.rub_ctacon,
                        rub_provision = item.rub_provision,
                        rub_nocontab = item.rub_nocontab,
                        rub_aplica_IESS = item.rub_aplica_IESS,
                        rub_acumula = item.rub_acumula,
                        rub_acumula_descuento = item.rub_acumula_descuento,
                        IdUsuario = item.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };

                    Context_rrhh.ro_rubro_tipo.Add(Entity_rubro);
                }

                foreach (var item in Lista_Horario)
                {
                    ro_horario Entity_horario = new ro_horario
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdHorario = item.IdHorario,
                        HoraIni = item.HoraIni,
                        HoraFin = item.HoraFin,
                        ToleranciaEnt = item.ToleranciaEnt,
                        ToleranciaReg_lunh = item.ToleranciaReg_lunh,
                        SalLunch = item.SalLunch,
                        RegLunch = item.RegLunch,
                        Descripcion = item.Descripcion,
                        Estado = item.Estado,
                        IdUsuario = item.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };

                    Context_rrhh.ro_horario.Add(Entity_horario);
                }

                foreach (var item in Lista_Turno)
                {
                    ro_turno Entity_turno = new ro_turno
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdTurno = item.IdTurno,
                        tu_descripcion = item.tu_descripcion,
                        Lunes = item.Lunes,
                        Martes = item.Martes,
                        Miercoles = item.Miercoles,
                        Jueves = item.Jueves,
                        Viernes = item.Viernes,
                        Sabado = item.Sabado,
                        Domingo = item.Domingo,
                        Estado = item.Estado,
                        IdUsuario = item.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };

                    Context_rrhh.ro_turno.Add(Entity_turno);
                }
                
                foreach (var item in Lista_Empleado)
                {
                    ro_empleado Entity_empleado = new ro_empleado
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdEmpleado = item.IdEmpleado,
                        IdEmpleado_Supervisor = null,
                        IdPersona = item.IdPersona,
                        IdSucursal = item.IdSucursal,
                        IdTipoEmpleado = item.IdTipoEmpleado,
                        em_codigo = (item.em_codigo) == null ? IdEmpleado.ToString() : item.em_codigo,
                        Codigo_Biometrico = item.Codigo_Biometrico,
                        em_lugarNacimiento = item.em_lugarNacimiento,
                        em_CarnetIees = null,
                        em_cedulaMil = null,
                        em_fechaSalida = null,
                        em_fechaIngaRol = item.em_fechaIngaRol,
                        em_tipoCta = item.em_tipoCta,
                        em_NumCta = item.em_NumCta,
                        em_estado = item.em_estado,
                        IdCodSectorial = null,
                        IdDepartamento = item.IdDepartamento,
                        IdTipoSangre = item.IdTipoSangre,
                        IdCargo = item.IdCargo,
                        IdCtaCble_Emplea = item.IdCtaCble_Emplea,
                        IdCiudad = item.IdCiudad,
                        em_mail = item.em_mail,
                        IdTipoLicencia = item.IdTipoLicencia,
                        IdBanco = item.IdBanco == 0 ? null : item.IdBanco,
                        IdArea = item.IdArea,
                        IdDivision = item.IdDivision,
                        IdUsuario = item.IdUsuario,
                        Fecha_Transaccion = item.Fecha_Transaccion,
                        IdUsuarioUltModi = null,
                        Fecha_UltMod = null,
                        IdUsuarioUltAnu = null,
                        Fecha_UltAnu = null,
                        MotivoAnulacion = null,
                        por_discapacidad = item.por_discapacidad,
                        carnet_conadis = item.carnet_conadis,
                        talla_pant = item.talla_pant,
                        talla_camisa = item.talla_camisa,
                        talla_zapato = item.talla_zapato,
                        em_status = item.em_status,
                        IdCondicionDiscapacidadSRI = item.IdCondicionDiscapacidadSRI,
                        IdTipoIdentDiscapacitadoSustitutoSRI = item.IdTipoIdentDiscapacitadoSustitutoSRI,
                        IdentDiscapacitadoSustitutoSRI = item.IdentDiscapacitadoSustitutoSRI,
                        IdAplicaConvenioDobleImposicionSRI = item.IdAplicaConvenioDobleImposicionSRI,
                        IdTipoResidenciaSRI = item.IdTipoResidenciaSRI,
                        IdTipoSistemaSalarioNetoSRI = item.IdTipoSistemaSalarioNetoSRI,
                        es_AcreditaHorasExtras = item.es_AcreditaHorasExtras,
                        CodigoSectorial = item.CodigoSectorial,
                        IdTipoAnticipo = item.IdTipoAnticipo,
                        ValorAnticipo = item.ValorAnticipo,
                        em_AnticipoSueldo = item.em_AnticipoSueldo,
                        Marca_Biometrico = item.Marca_Biometrico,
                        IdHorario = item.IdHorario == 0 ? null : item.IdHorario,
                        Tiene_ingresos_compartidos = item.Tiene_ingresos_compartidos,
                        Pago_por_horas = item.Pago_por_horas,
                        Valor_maximo_horas_mat = null,
                        Valor_maximo_horas_vesp = null,
                        Valor_horas_vespertina = item.Valor_horas_vespertina,
                        Valor_horas_matutino = item.Valor_horas_matutino,
                        Valor_horas_brigada = item.Valor_horas_brigada,
                        Valor_hora_adicionales = item.Valor_hora_adicionales,
                        GozaMasDeQuinceDiasVaciones = item.GozaMasDeQuinceDiasVaciones,
                        DiasVacaciones = item.DiasVacaciones
                    };

                    Context_rrhh.ro_empleado.Add(Entity_empleado);
                }

                foreach (var item in Lista_RubrosAcumulados)
                {
                    ro_empleado_x_rubro_acumulado Entity_RubroAcumulado = new ro_empleado_x_rubro_acumulado
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdEmpleado = item.IdEmpleado,
                        IdRubro = item.IdRubro,
                        UsuarioIngresa = item.UsuarioIngresa,
                        FechaIngresa = item.FechaIngresa
                    };

                    Context_rrhh.ro_empleado_x_rubro_acumulado.Add(Entity_RubroAcumulado);
                }

                //foreach (var item in Lista_TipoNomina)
                //{
                //    ro_Nomina_Tipo Entity_TipoNomina = new ro_Nomina_Tipo
                //    {
                //        IdEmpresa = item.IdEmpresa,
                //        IdNomina_Tipo = item.IdNomina_Tipo,
                //        Descripcion = item.Descripcion,
                //        IdUsuario = item.IdUsuario,
                //        IdUsuarioAnu = null,
                //        MotivoAnu = null,
                //        IdUsuarioUltModi = null,
                //        FechaAnu = null,
                //        FechaTransac = item.FechaTransac,
                //        FechaUltModi = null,
                //        Estado = item.Estado
                //    };

                //    Context_rrhh.ro_Nomina_Tipo.Add(Entity_TipoNomina);
                //}


                foreach (var item in Lista_Contrato)
                {
                    ro_contrato Entity_contrato = new ro_contrato
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdContrato = item.IdContrato,
                        IdContrato_Tipo = item.IdContrato_Tipo,
                        IdEmpleado = item.IdEmpleado,
                        FechaInicio = item.FechaInicio,
                        FechaFin = item.FechaFin,
                        Sueldo = item.Sueldo,
                        NumDocumento = item.NumDocumento,
                        IdNomina = item.IdNomina,
                        Observacion = item.Observacion,
                        IdUsuario = item.IdUsuario,
                        Estado = item.Estado,
                        EstadoContrato = item.EstadoContrato,
                        Fecha_Transac = item.Fecha_Transac
                    };
                    Context_rrhh.ro_contrato.Add(Entity_contrato);
                }

                //foreach (var item in Lista_CargasFamiliares)
                //{
                //    ro_cargaFamiliar Entity_CargaFamiliar = new ro_cargaFamiliar
                //    {
                //        IdEmpresa = item.IdEmpresa,
                //        IdCargaFamiliar = item.IdCargaFamiliar,
                //        IdEmpleado = item.IdEmpleado,
                //        Cedula = item.Cedula,
                //        Sexo = item.Sexo,
                //        TipoFamiliar = item.TipoFamiliar,
                //        Nombres = item.Nombres,
                //        FechaNacimiento = item.FechaNacimiento,
                //        Estado = item.Estado,
                //        FechaDefucion = item.FechaDefucion,
                //        capacidades_especiales = item.capacidades_especiales
                //    };
                //    Context_rrhh.ro_cargaFamiliar.Add(Entity_CargaFamiliar);
                //}

                Context_rrhh.SaveChanges();
                Context_rrhh.Dispose();                
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
