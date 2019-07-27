using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Helps;
using DevExpress.Web;

namespace Core.Erp.Data.General
{
    public class tb_persona_Data
    {
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, string IdTipoPersona)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<tb_persona_Info> Lista = new List<tb_persona_Info>();
            Lista = get_list(IdEmpresa, IdTipoPersona, skip, take, args.Filter);
            return Lista;
        }

        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa, string IdTipoPersona)
        {
            decimal id;
            if (args.Value == null || !decimal.TryParse(args.Value.ToString(), out id))
                return null;
            return get_info(IdEmpresa, IdTipoPersona, (decimal)args.Value);
        }

        public List<tb_persona_Info> get_list_bajo_demanda_cliente_x_tipo(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, int Idtipo_cliente)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<tb_persona_Info> Lista = new List<tb_persona_Info>();
            Lista = get_list_cliente_x_tipo(IdEmpresa, Idtipo_cliente, skip, take, args.Filter);
            return Lista;
        }

        public tb_persona_Info get_info_bajo_demanda_cliente_x_tipo(ListEditItemRequestedByValueEventArgs args, int IdEmpresa, int Idtipo_cliente)
        {
            decimal id;
            if (args.Value == null || !decimal.TryParse(args.Value.ToString(), out id))
                return null;
            return get_info_cliente_x_tipo(IdEmpresa, Idtipo_cliente, (decimal)args.Value);
        }

        public List<tb_persona_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<tb_persona_Info> Lista;

                using (Entities_general Context = new Entities_general())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.tb_persona
                                 select new tb_persona_Info
                                 {
                                     IdPersona = q.IdPersona,
                                     pe_nombreCompleto = q.pe_nombreCompleto,
                                     IdTipoDocumento = q.IdTipoDocumento,
                                     pe_cedulaRuc = q.pe_cedulaRuc,
                                     pe_estado = q.pe_estado,

                                     EstadoBool = q.pe_estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.tb_persona
                                 where q.pe_estado == "A"
                                 select new tb_persona_Info
                                 {
                                     IdPersona = q.IdPersona,
                                     pe_nombreCompleto = q.pe_nombreCompleto,
                                     IdTipoDocumento = q.IdTipoDocumento,
                                     pe_cedulaRuc = q.pe_cedulaRuc,
                                     pe_estado = q.pe_estado,

                                     EstadoBool = q.pe_estado == "A" ? true : false
                                 }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<tb_persona_Info> get_list(int IdEmpresa, string IdTipo_persona, int skip, int take, string filter)
        {
            try
            {
                List<tb_persona_Info> Lista = new List<tb_persona_Info>();

                Entities_general context_g = new Entities_general();
                switch (IdTipo_persona)
                {
                    case "PERSONA":
                        var lstg = context_g.tb_persona.Where(q => q.pe_estado == "A" && (q.IdPersona.ToString() + " " + q.pe_cedulaRuc + " " + q.pe_nombreCompleto).Contains(filter)).OrderBy(q => q.IdPersona).Skip(skip).Take(take);
                        foreach (var q in lstg)
                        {
                            Lista.Add(new tb_persona_Info
                            {
                                IdPersona = q.IdPersona,
                                pe_nombreCompleto = q.pe_nombreCompleto,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                IdEntidad = q.IdPersona
                            });
                        }
                        break;
                    case "CLIENTE":
                        Entities_facturacion context_f = new Entities_facturacion();
                        var lstf = context_f.vwfa_cliente_consulta.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == "A" && (q.IdCliente.ToString() + " " + q.pe_cedulaRuc + " " + q.pe_nombreCompleto).Contains(filter)).OrderBy(q => q.IdCliente).Skip(skip).Take(take);
                        foreach (var q in lstf)
                        {
                            Lista.Add(new tb_persona_Info
                            {
                                IdPersona = q.IdPersona,
                                pe_nombreCompleto = q.pe_nombreCompleto,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                IdEntidad = q.IdCliente,
                                CodPersona = q.Descripcion_tip_cliente
                            });
                        }
                        context_f.Dispose();
                        break;
                    case "EMPLEA":
                        Entities_rrhh context_e = new Entities_rrhh();
                        var lstr = context_e.vwro_empleados_consulta.Where(q => q.IdEmpresa == IdEmpresa && q.em_estado == "A" && (q.IdEmpleado.ToString() + " " + q.pe_cedulaRuc + " " + q.Empleado).Contains(filter)).OrderBy(q => q.IdEmpleado).Skip(skip).Take(take);
                        foreach (var q in lstr)
                        {
                            Lista.Add(new tb_persona_Info
                            {
                                IdPersona = q.IdPersona,
                                pe_nombreCompleto = q.Empleado,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                IdEntidad = q.IdEmpleado
                            });
                        }
                        context_e.Dispose();
                        break;
                    case "PROVEE":
                        Entities_cuentas_por_pagar context_p = new Entities_cuentas_por_pagar();
                        var lstp = context_p.vwcp_proveedor_consulta.Where(q => q.IdEmpresa == IdEmpresa && q.pr_estado == "A" && (q.IdProveedor.ToString() + " " + q.pe_cedulaRuc + " " + q.pe_nombreCompleto).Contains(filter)).OrderBy(q => q.IdProveedor).Skip(skip).Take(take);
                        foreach (var q in lstp)
                        {
                            Lista.Add(new tb_persona_Info
                            {
                                IdPersona = q.IdPersona,
                                pe_nombreCompleto = q.pe_nombreCompleto,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                IdEntidad = q.IdProveedor
                            });
                        }
                        context_p.Dispose();
                        break;
                }

                context_g.Dispose();
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public decimal validar_existe_cedula(string pe_CedulaRuc)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    pe_CedulaRuc = pe_CedulaRuc == null ? "" : pe_CedulaRuc.Trim();

                    var lst = from q in Context.tb_persona
                              where q.pe_cedulaRuc == pe_CedulaRuc
                              select q;

                    if (lst.Count() > 0)
                        return lst.FirstOrDefault().IdPersona;
                    else
                        return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private decimal get_id()
        {
            try
            {
                decimal ID = 1;

                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.tb_persona
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdPersona) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public tb_persona_Info get_info(int IdEmpresa, string IdTipoPersona, decimal IdEntidad)
        {
            tb_persona_Info info = new tb_persona_Info();

            Entities_general context_g = new Entities_general();
            switch (IdTipoPersona)
            {
                case "PERSONA":
                    info = (from q in context_g.tb_persona
                            where q.pe_estado == "A"
                            && q.IdPersona == IdEntidad
                            select new tb_persona_Info
                            {
                                IdPersona = q.IdPersona,
                                pe_nombreCompleto = q.pe_nombreCompleto,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                IdEntidad = q.IdPersona
                            }).FirstOrDefault();
                    break;
                case "CLIENTE":
                    Entities_facturacion context_f = new Entities_facturacion();
                    info = (from q in context_f.vwfa_cliente_consulta
                            where q.Estado == "A"
                            && q.IdEmpresa == IdEmpresa
                            && q.IdCliente == IdEntidad
                            select new tb_persona_Info
                            {
                                IdPersona = q.IdPersona,
                                pe_nombreCompleto = q.pe_nombreCompleto,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                IdEntidad = q.IdCliente
                            }).FirstOrDefault();
                    context_f.Dispose();
                    break;
                case "EMPLEA":
                    Entities_rrhh context_e = new Entities_rrhh();
                    info = (from q in context_e.vwro_empleados_consulta
                            where q.em_estado == "A"
                            && q.IdEmpresa == IdEmpresa
                            && q.IdEmpleado == IdEntidad
                            select new tb_persona_Info
                            {
                                IdPersona = q.IdPersona,
                                pe_nombreCompleto = q.Empleado,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                IdEntidad = q.IdEmpleado
                            }).FirstOrDefault();
                    context_e.Dispose();
                    break;
                case "PROVEE":
                    Entities_cuentas_por_pagar context_p = new Entities_cuentas_por_pagar();
                    info = (from q in context_p.vwcp_proveedor_consulta
                            where q.pr_estado == "A"
                            && q.IdEmpresa == IdEmpresa
                            && q.IdProveedor == IdEntidad
                            select new tb_persona_Info
                            {
                                IdPersona = q.IdPersona,
                                pe_nombreCompleto = q.pe_nombreCompleto,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                IdEntidad = q.IdProveedor
                            }).FirstOrDefault();
                    context_p.Dispose();
                    break;
            }

            context_g.Dispose();

            return info;
        }
        public tb_persona_Info get_info(decimal IdPersona)
        {
            try
            {
                tb_persona_Info info = new tb_persona_Info();

                using (Entities_general Context = new Entities_general())
                {
                    tb_persona Entity = Context.tb_persona.FirstOrDefault(q => q.IdPersona == IdPersona);
                    if (Entity == null) return null;
                    info = new tb_persona_Info
                    {
                        IdPersona = Entity.IdPersona,
                        CodPersona = Entity.CodPersona,
                        pe_Naturaleza = Entity.pe_Naturaleza,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        pe_razonSocial = Entity.pe_razonSocial,
                        pe_apellido = Entity.pe_apellido,
                        pe_nombre = Entity.pe_nombre,
                        IdTipoDocumento = Entity.IdTipoDocumento,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        pe_direccion = Entity.pe_direccion,
                        pe_telfono_Contacto = Entity.pe_telfono_Contacto,
                        pe_celular = Entity.pe_celular,
                        pe_correo = Entity.pe_correo,
                        pe_sexo = Entity.pe_sexo,
                        IdEstadoCivil = Entity.IdEstadoCivil,
                        pe_fechaNacimiento = Entity.pe_fechaNacimiento,
                        pe_estado = Entity.pe_estado,
                        IdTipoCta_acreditacion_cat = Entity.IdTipoCta_acreditacion_cat,
                        num_cta_acreditacion = Entity.num_cta_acreditacion,
                        IdBanco_acreditacion = Entity.IdBanco_acreditacion
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(tb_persona_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_persona Entity = new tb_persona
                    {
                        IdPersona = info.IdPersona = get_id(),
                        CodPersona = info.CodPersona,
                        pe_Naturaleza = info.pe_Naturaleza,
                        pe_nombreCompleto = info.pe_nombreCompleto,
                        pe_razonSocial = info.pe_razonSocial,
                        pe_apellido = info.pe_apellido,
                        pe_nombre = info.pe_nombre,
                        IdTipoDocumento = info.IdTipoDocumento,
                        pe_cedulaRuc = info.pe_cedulaRuc.Trim(),
                        pe_direccion = info.pe_direccion,
                        pe_telfono_Contacto = info.pe_telfono_Contacto,
                        pe_celular = info.pe_celular,
                        pe_correo = info.pe_correo,
                        pe_sexo = info.pe_sexo,
                        IdEstadoCivil = info.IdEstadoCivil,
                        pe_fechaNacimiento = info.pe_fechaNacimiento,
                        pe_estado = info.pe_estado = "A",
                        pe_fechaCreacion = info.pe_fechaCreacion = DateTime.Now,
                        IdTipoCta_acreditacion_cat = info.IdTipoCta_acreditacion_cat,
                        num_cta_acreditacion = info.num_cta_acreditacion,
                        IdBanco_acreditacion = info.IdBanco_acreditacion,
                    };
                    Context.tb_persona.Add(Entity);
                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(tb_persona_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_persona Entity = Context.tb_persona.FirstOrDefault(q => q.IdPersona == info.IdPersona);
                    if (Entity == null) return false;
                    Entity.pe_Naturaleza = info.pe_Naturaleza;
                    Entity.pe_nombreCompleto = info.pe_nombreCompleto;
                    Entity.pe_razonSocial = info.pe_razonSocial;
                    Entity.pe_apellido = info.pe_apellido;
                    Entity.pe_nombre = info.pe_nombre;
                    Entity.IdTipoDocumento = info.IdTipoDocumento;
                    Entity.pe_cedulaRuc = info.pe_cedulaRuc;
                    Entity.pe_direccion = info.pe_direccion;
                    Entity.pe_telfono_Contacto = info.pe_telfono_Contacto;
                    Entity.pe_celular = info.pe_celular;
                    Entity.pe_correo = info.pe_correo;
                    Entity.pe_sexo = info.pe_sexo;
                    Entity.IdEstadoCivil = info.IdEstadoCivil;
                    Entity.pe_fechaNacimiento = info.pe_fechaNacimiento;
                    Entity.IdTipoCta_acreditacion_cat = info.IdTipoCta_acreditacion_cat;
                    Entity.num_cta_acreditacion = info.num_cta_acreditacion;
                    Entity.IdBanco_acreditacion = info.IdBanco_acreditacion;

                    Entity.pe_fechaModificacion = DateTime.Now;
                    Entity.pe_UltUsuarioModi = info.pe_UltUsuarioModi;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(tb_persona_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_persona Entity = Context.tb_persona.FirstOrDefault(q => q.IdPersona == info.IdPersona);
                    if (Entity == null) return false;
                    Entity.pe_estado = "I";
                    Entity.Fecha_UltAnu = info.Fecha_UltAnu = DateTime.Now;
                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public tb_persona_Info armar_info(tb_persona_Info info)
        {
            tb_persona_Info info_retorno = new tb_persona_Info
            {
                //Campos obligatorios en toda pantalla
                pe_nombre = info.pe_nombre,
                pe_apellido = info.pe_apellido,
                pe_nombreCompleto = info.pe_nombreCompleto,
                pe_cedulaRuc = info.pe_cedulaRuc,
                pe_Naturaleza = info.pe_Naturaleza,
                IdTipoDocumento = info.IdTipoDocumento,
                pe_razonSocial = info.pe_razonSocial,

                //Campos opcionales
                pe_direccion = info.pe_direccion,
                pe_telfono_Contacto = info.pe_telfono_Contacto,
                pe_celular = info.pe_celular,
                pe_correo = info.pe_correo,
                pe_fechaNacimiento = info.pe_fechaNacimiento,

                //Si vienen null se pone un valor default
                IdEstadoCivil = string.IsNullOrEmpty(info.IdEstadoCivil) ? "SOLTE" : info.IdEstadoCivil,
                pe_sexo = string.IsNullOrEmpty(info.pe_sexo) ? "SEXO_MAS" : info.pe_sexo,
            };
            return info_retorno;
        }

        public List<tb_persona_Info> get_list_cliente_x_tipo(int IdEmpresa, int Idtipo_cliente, int skip, int take, string filter)
        {
            try
            {
                List<tb_persona_Info> Lista = new List<tb_persona_Info>();

                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    if (Idtipo_cliente == 0)
                    {
                        var lstf = Context.vwfa_cliente_consulta.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == "A" && (q.IdCliente.ToString() + " " + q.pe_cedulaRuc + " " + q.pe_nombreCompleto).Contains(filter)).OrderBy(q => q.IdCliente).Skip(skip).Take(take);

                        foreach (var q in lstf)
                        {
                            Lista.Add(new tb_persona_Info
                            {
                                IdPersona = q.IdPersona,
                                pe_nombreCompleto = q.pe_nombreCompleto,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                IdEntidad = q.IdCliente,
                                CodPersona = q.Descripcion_tip_cliente
                            });
                        }
                    }
                    else
                    {
                        var lstf = Context.vwfa_cliente_consulta.Where(q => q.IdEmpresa == IdEmpresa && q.Idtipo_cliente == Idtipo_cliente && q.Estado == "A" && (q.IdCliente.ToString() + " " + q.pe_cedulaRuc + " " + q.pe_nombreCompleto).Contains(filter)).OrderBy(q => q.IdCliente).Skip(skip).Take(take);

                        foreach (var q in lstf)
                        {
                            Lista.Add(new tb_persona_Info
                            {
                                IdPersona = q.IdPersona,
                                pe_nombreCompleto = q.pe_nombreCompleto,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                IdEntidad = q.IdCliente,
                                CodPersona = q.Descripcion_tip_cliente
                            });
                        }
                    }                    
                }
                                   
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_persona_Info get_info_cliente_x_tipo(int IdEmpresa, int Idtipo_cliente, decimal IdEntidad)
        {
            tb_persona_Info info = new tb_persona_Info();

            using (Entities_facturacion Context = new Entities_facturacion())
            {
                if (Idtipo_cliente == 0)
                {
                    vwfa_cliente_consulta Entity = Context.vwfa_cliente_consulta.FirstOrDefault(q => q.Estado == "A" && q.IdEmpresa == IdEmpresa && q.IdCliente == IdEntidad);

                    if (Entity == null) return null;
                    info = new tb_persona_Info
                    {
                        IdPersona = Entity.IdPersona,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        IdEntidad = Entity.IdCliente,
                        CodPersona = Entity.Descripcion_tip_cliente
                    };
                }
                else
                {
                    vwfa_cliente_consulta Entity = Context.vwfa_cliente_consulta.FirstOrDefault(q => q.Estado == "A" && q.Idtipo_cliente == Idtipo_cliente
                                                                                                && q.IdEmpresa == IdEmpresa && q.IdCliente == IdEntidad);

                    if (Entity == null) return null;
                    info = new tb_persona_Info
                    {
                        IdPersona = Entity.IdPersona,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        IdEntidad = Entity.IdCliente,
                        CodPersona = Entity.Descripcion_tip_cliente
                    };
                }
                                
            }

            return info;

        }
    }
}
