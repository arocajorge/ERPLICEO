using Core.Erp.Bus.General;
using Core.Erp.Data.General;
using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.RRHH
{
    public class ro_empleado_Bus
    {
        ro_empleado_Data odata = new ro_empleado_Data();
        tb_persona_Data odata_per = new tb_persona_Data();

        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public List<ro_empleado_Info> get_list_combo(int IdEmpresa)
        {
            try
            {
                return odata.get_list_combo(IdEmpresa);
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
                return odata.get_list_profesores(IdEmpresa);
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
                return odata.get_list_combo_liquidar(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_empleado_Info> get_list(int IdEmpresa, int IdSucursal, string em_status, bool estado)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, em_status, estado);
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
                return odata.get_info(IdEmpresa, IdEmpleado);
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
                bool si_grabo = false;
                info.info_persona.pe_Naturaleza = "NATU";
                info.info_persona.pe_nombreCompleto = info.pe_apellido + " " + info.pe_nombre;
                info.info_persona.pe_razonSocial = info.pe_apellido + " " + info.pe_nombre;
                decimal IdPersona = bus_persona.validar_existe_cedula(info.info_persona.pe_cedulaRuc);
                if (IdPersona != 0)
                {
                    info.info_persona.IdPersona = IdPersona;
                  si_grabo=  bus_persona.modificarDB(info.info_persona);
                }
                else
                {
                    info.info_persona.pe_Naturaleza = "NATU";
                    info.info_persona.pe_nombreCompleto = info.pe_apellido  + " " + info.pe_nombre;
                    info.info_persona.pe_razonSocial = null;
                    info.info_persona.IdPersona = info.IdPersona;
                    info.info_persona.pe_apellido = info.pe_apellido;
                    info.info_persona.pe_nombre = info.pe_nombre;
                    info.info_persona.IdTipoDocumento = info.IdTipoDocumento;
                    info.info_persona.pe_cedulaRuc = info.pe_cedulaRuc;
                    info.info_persona.pe_direccion = info.pe_direccion;
                    info.info_persona.pe_telfono_Contacto = info.pe_telfono_Contacto;
                    info.info_persona.pe_celular = info.pe_celular;
                    info.info_persona.pe_correo = info.pe_correo;
                    info.info_persona.pe_sexo = info.pe_sexo;
                    info.info_persona.IdEstadoCivil = info.IdEstadoCivil;
                    info.info_persona.pe_fechaNacimiento = info.pe_fechaNacimiento;
                    si_grabo = bus_persona.guardarDB(info.info_persona);
                }
                if(si_grabo)
                {
                    odata = new ro_empleado_Data();
                    info.em_estado = "A";
                    info.IdPersona = info.info_persona.IdPersona;
                    si_grabo= odata.guardarDB(info);
                }
                return si_grabo;
            }
            catch (Exception )
            {
                throw;
            }
        }

        public bool modificarDB(ro_empleado_Info info)
        {

            try
            {
                bool si_grabo = false;
                info.info_persona.pe_Naturaleza = "NATU";
                info.info_persona.pe_nombreCompleto = info.pe_apellido + " " + info.pe_nombre;
                info.info_persona.pe_razonSocial = info.pe_apellido + " " + info.pe_nombre;
                if (info.IdPersona != 0)
                {
                    info.info_persona.IdPersona = info.IdPersona;
                    info.info_persona.pe_apellido = info.pe_apellido;
                    info.info_persona.pe_nombre = info.pe_nombre;
                    info.info_persona.IdTipoDocumento = info.IdTipoDocumento;
                    info.info_persona.pe_cedulaRuc = info.pe_cedulaRuc;
                    info.info_persona.pe_direccion = info.pe_direccion;
                    info.info_persona.pe_telfono_Contacto = info.pe_telfono_Contacto;
                    info.info_persona.pe_celular = info.pe_celular;
                    info.info_persona.pe_correo = info.pe_correo;
                    info.info_persona.pe_sexo = info.pe_sexo;
                    info.info_persona.IdEstadoCivil = info.IdEstadoCivil;
                    info.info_persona.pe_fechaNacimiento = info.pe_fechaNacimiento;

                    si_grabo = bus_persona.modificarDB(info.info_persona);
                }
               
                if (si_grabo)
                {
                    odata = new ro_empleado_Data();
                    info.em_estado = "A";
                    si_grabo = odata.modificarDB(info);
                }
                return si_grabo;
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
                return odata.anularDB(info);
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
                return odata.get_id(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB_importacion(int IdEmpresa, List<ro_division_Info> Lista_Division, List<ro_area_Info> Lista_Area, List<ro_departamento_Info> Lista_Departamento,List<ro_cargo_Info> Lista_Cargo,
                                        List<ro_rubro_tipo_Info> Lista_Rubro, List<ro_horario_Info> Lista_Horario, List<ro_turno_Info> Lista_Turno, List<ro_empleado_Info> Lista_Empleado, List<ro_empleado_x_rubro_acumulado_Info> Lista_RubrosAcumulados, List<ro_nomina_tipo_Info> Lista_TipoNomina, List<ro_contrato_Info> Lista_Contrato,
                                        List<ro_cargaFamiliar_Info> Lista_CargasFamiliares, List<ro_rol_detalle_x_rubro_acumulado_Info> Lista_ProvisionesAcumuladas, List<ro_historico_vacaciones_x_empleado_Info> Lista_VacacionesList)
        {
            try
            {
                var count = 0;
                foreach (var item in Lista_Empleado)
                {
                    if (item.info_persona.IdPersona == 0)
                    {
                        item.info_persona = odata_per.armar_info(item.info_persona);
                        if (odata_per.guardarDB(item.info_persona))
                        {
                            item.IdPersona = item.info_persona.IdPersona;
                            count++;
                        }
                    }
                    else
                    {
                        odata_per.modificarDB(item.info_persona);
                        count++;
                    }
                }
                if (Lista_Empleado.Count == count)
                {
                    return odata.guardarDB_importacion(IdEmpresa, Lista_Division, Lista_Area, Lista_Departamento, Lista_Cargo,
                                                    Lista_Rubro, Lista_Horario, Lista_Turno, Lista_Empleado, Lista_RubrosAcumulados, Lista_TipoNomina, Lista_Contrato,
                                                    Lista_CargasFamiliares, Lista_ProvisionesAcumuladas, Lista_VacacionesList);
                }
                return true;           
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
