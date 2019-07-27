using Core.Erp.Data.General;
using Core.Erp.Info.CuentasPorPagar;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Erp.Data.CuentasPorPagar
{
    public class cp_proveedor_Data
    {
        public List<cp_proveedor_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<cp_proveedor_Info> Lista;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.vwcp_proveedor_consulta
                                 join t in Context.cp_proveedor_clase
                                 on new { q.IdEmpresa, q.IdClaseProveedor } equals new { t.IdEmpresa, t.IdClaseProveedor }
                                 where q.IdEmpresa == IdEmpresa
                                 select new cp_proveedor_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdProveedor = q.IdProveedor,
                                     pr_estado = q.pr_estado,
                                     descripcion_clas_prove = t.descripcion_clas_prove,
                                     IdEntidad=q.IdProveedor,
                                     info_persona = new Info.General.tb_persona_Info
                                     {
                                         IdPersona = q.IdPersona,
                                         pe_nombreCompleto = q.pe_nombreCompleto,
                                         pe_cedulaRuc = q.pe_cedulaRuc
                                     },

                                     EstadoBool = q.pr_estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.vwcp_proveedor_consulta
                                 join t in Context.cp_proveedor_clase
                                 on new { q.IdEmpresa, q.IdClaseProveedor } equals new { t.IdEmpresa, t.IdClaseProveedor }
                                 where q.IdEmpresa == IdEmpresa
                                 && q.pr_estado == "A"
                                 select new cp_proveedor_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdProveedor = q.IdProveedor,
                                     pr_estado = q.pr_estado,
                                     descripcion_clas_prove = t.descripcion_clas_prove,
                                     info_persona = new Info.General.tb_persona_Info
                                     {
                                         IdPersona = q.IdPersona,
                                         pe_nombreCompleto = q.pe_nombreCompleto,
                                         pe_cedulaRuc = q.pe_cedulaRuc
                                     },

                                     EstadoBool = q.pr_estado == "A" ? true : false
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        } 

        public cp_proveedor_Info get_info(int IdEmpresa, decimal IdProveedor)
        {
            try
            {
                cp_proveedor_Info info = new cp_proveedor_Info();
                Entities_cuentas_por_pagar Context_p = new Entities_cuentas_por_pagar();
                Entities_general Context_g = new Entities_general();

                cp_proveedor Entity = Context_p.cp_proveedor.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdProveedor == IdProveedor);
                    if (Entity == null) return null;
                    info = new cp_proveedor_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdProveedor = Entity.IdProveedor,
                        IdPersona = Entity.IdPersona,
                        IdClaseProveedor = Entity.IdClaseProveedor,
                        IdCiudad = Entity.IdCiudad,
                        IdBanco_acreditacion = Entity.IdBanco_acreditacion,
                        IdCtaCble_CXP = Entity.IdCtaCble_CXP,
                        IdCtaCble_Gasto = Entity.IdCtaCble_Gasto,
                        IdTipoCta_acreditacion_cat = Entity.IdTipoCta_acreditacion_cat,
                        pr_contribuyenteEspecial_bool = Entity.pr_contribuyenteEspecial == "S" ? true : false,
                        es_empresa_relacionada = Entity.es_empresa_relacionada,
                        num_cta_acreditacion = Entity.num_cta_acreditacion,
                        pr_codigo = Entity.pr_codigo,
                        pr_plazo = Entity.pr_plazo,
                        pr_estado = Entity.pr_estado,
                        pr_correo = Entity.pr_correo,
                        pr_direccion = Entity.pr_direccion,
                        pr_telefonos = Entity.pr_telefonos,
                        pr_celular = Entity.pr_celular,
                        IdCtaCble_Anticipo = Entity.IdCtaCble_Anticipo
                    };
                tb_persona Entity_p = Context_g.tb_persona.Where(q => q.IdPersona == info.IdPersona).FirstOrDefault();
                info.info_persona = new Info.General.tb_persona_Info
                {
                    IdPersona = Entity_p.IdPersona,
                    pe_apellido = Entity_p.pe_apellido,
                    pe_nombre = Entity_p.pe_nombre,
                    pe_cedulaRuc = Entity_p.pe_cedulaRuc,
                    pe_nombreCompleto = Entity_p.pe_nombreCompleto,
                    pe_razonSocial = Entity_p.pe_razonSocial,
                    pe_Naturaleza = Entity_p.pe_Naturaleza,
                    IdTipoDocumento = Entity_p.IdTipoDocumento
                };

                Context_p.Dispose();
                Context_g.Dispose();
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    var lst = from q in Context.cp_proveedor
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdProveedor) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(cp_proveedor_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar() )
                {
                    cp_proveedor Entity = new cp_proveedor
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdProveedor = info.IdProveedor=get_id(info.IdEmpresa),
                        IdPersona = info.IdPersona,
                        IdClaseProveedor = info.IdClaseProveedor,
                        IdCiudad = info.IdCiudad,
                        IdBanco_acreditacion = info.IdBanco_acreditacion,
                        IdCtaCble_CXP = info.IdCtaCble_CXP,
                        IdCtaCble_Gasto = info.IdCtaCble_Gasto,
                        IdTipoCta_acreditacion_cat = info.IdTipoCta_acreditacion_cat,
                        num_cta_acreditacion = info.num_cta_acreditacion,
                        pr_codigo = info.pr_codigo,
                        pr_plazo = info.pr_plazo,
                        pr_estado = info.pr_estado="A",
                        pr_contribuyenteEspecial = info.pr_contribuyenteEspecial_bool == true ? "S" : "N",
                        es_empresa_relacionada = info.es_empresa_relacionada,
                        pr_celular = info.pr_celular,
                        pr_telefonos = info.pr_telefonos,
                        pr_direccion = info.pr_direccion,
                        pr_correo = info.pr_correo,
                        IdCtaCble_Anticipo = info.IdCtaCble_Anticipo,

                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.cp_proveedor.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(cp_proveedor_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_proveedor Entity = Context.cp_proveedor.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdProveedor == info.IdProveedor);
                    if (Entity == null) return false;

                    //Entity.IdPersona = info.IdPersona;
                    Entity.IdClaseProveedor = info.IdClaseProveedor;
                    Entity.IdCiudad = info.IdCiudad;
                    Entity.IdBanco_acreditacion = info.IdBanco_acreditacion;
                    Entity.IdCtaCble_CXP = (info.IdCtaCble_CXP)== "== Seleccione =="?null: info.IdCtaCble_CXP;
                    Entity.IdCtaCble_Gasto =  (info.IdCtaCble_Gasto) == "== Seleccione ==" ? null : info.IdCtaCble_Gasto;
                    Entity.IdTipoCta_acreditacion_cat = info.IdTipoCta_acreditacion_cat;
                    Entity.num_cta_acreditacion = info.num_cta_acreditacion;
                    Entity.pr_codigo = info.pr_codigo;
                    Entity.pr_plazo = info.pr_plazo;
                    Entity.pr_contribuyenteEspecial = info.pr_contribuyenteEspecial_bool == true ? "S" : "N";
                    Entity.es_empresa_relacionada = info.es_empresa_relacionada;
                    Entity.pr_correo = info.pr_correo;
                    Entity.pr_direccion = info.pr_direccion;
                    Entity.pr_telefonos = info.pr_telefonos;
                    Entity.pr_celular = info.pr_celular;
                    Entity.IdCtaCble_Anticipo = info.IdCtaCble_Anticipo;

                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = DateTime.Now;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(cp_proveedor_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_proveedor Entity = Context.cp_proveedor.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdProveedor == info.IdProveedor);
                    if (Entity == null) return false;

                    Entity.pr_estado = info.pr_estado="I";

                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = DateTime.Now;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public cp_proveedor_Info get_info_x_num_cedula(int IdEmpresa, string pe_cedulaRuc)
        {
            try
            {
                cp_proveedor_Info info = new cp_proveedor_Info
                {
                    info_persona = new Info.General.tb_persona_Info()
                };

                Entities_general Context_general = new Entities_general();
                tb_persona Entity_p = Context_general.tb_persona.Where(q => q.pe_cedulaRuc == pe_cedulaRuc).FirstOrDefault();
                if (Entity_p == null)
                {
                    Context_general.Dispose();
                    return info;
                }
                Entities_cuentas_por_pagar Context_cxp = new Entities_cuentas_por_pagar();
                cp_proveedor Entity_c = Context_cxp.cp_proveedor.Where(q => q.IdEmpresa == IdEmpresa && q.IdPersona == Entity_p.IdPersona).FirstOrDefault();
                if (Entity_c == null)
                {
                    info.IdPersona = Entity_p.IdPersona;
                    info.info_persona = new Info.General.tb_persona_Info
                    {
                        IdPersona = Entity_p.IdPersona,
                        pe_apellido = Entity_p.pe_apellido,
                        pe_nombre = Entity_p.pe_nombre,
                        pe_cedulaRuc = Entity_p.pe_cedulaRuc,
                        pe_nombreCompleto = Entity_p.pe_nombreCompleto,
                        pe_razonSocial = Entity_p.pe_razonSocial,
                        pe_celular = Entity_p.pe_celular,
                        pe_telfono_Contacto = Entity_p.pe_telfono_Contacto,
                        pe_correo = Entity_p.pe_correo,
                        pe_direccion = Entity_p.pe_direccion
                    };
                    Context_general.Dispose();
                    Context_cxp.Dispose();
                    return info;
                }
                info = new cp_proveedor_Info
                {
                    IdEmpresa = Entity_c.IdEmpresa,
                    IdProveedor = Entity_c.IdProveedor,
                    IdPersona = Entity_p.IdPersona,
                    info_persona = new Info.General.tb_persona_Info
                    {
                        IdPersona = Entity_p.IdPersona,
                        pe_apellido = Entity_p.pe_apellido,
                        pe_nombre = Entity_p.pe_nombre,
                        pe_cedulaRuc = Entity_p.pe_cedulaRuc,
                        pe_nombreCompleto = Entity_p.pe_nombreCompleto,
                        pe_razonSocial = Entity_p.pe_razonSocial,
                        pe_celular = Entity_p.pe_celular,
                        pe_telfono_Contacto = Entity_p.pe_telfono_Contacto,
                        pe_correo = Entity_p.pe_correo,
                        pe_direccion = Entity_p.pe_direccion
                    }
                };

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }



        #region metodo baja demanda

        public List<cp_proveedor_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<cp_proveedor_Info> Lista = new List<cp_proveedor_Info>();
            Lista = get_list(IdEmpresa, skip, take, args.Filter);

            return Lista;
        }

        public cp_proveedor_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            decimal id;
            if (args.Value == null || !decimal.TryParse(args.Value.ToString(), out id))
                return null;
            return get_info_demanda(IdEmpresa, Convert.ToDecimal(args.Value));
        }

        public cp_proveedor_Info get_info_demanda(int IdEmpresa, decimal IdProducto)
        {
            cp_proveedor_Info info = new cp_proveedor_Info();

            using (Entities_cuentas_por_pagar Contex = new Entities_cuentas_por_pagar())
            {
                info = (from q in Contex.vwcp_proveedor_consulta
                      
                        select new cp_proveedor_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdPersona = q.IdPersona,
                            IdProveedor = q.IdProveedor,
                            pr_codigo = q.pr_codigo,
                            info_persona = new Info.General.tb_persona_Info
                            {
                                pe_cedulaRuc=q.pe_cedulaRuc,
                                pe_nombreCompleto=q.pe_nombreCompleto
                            }
                           
                        }).FirstOrDefault();

            }
          
            return info;
        }

        public List<cp_proveedor_Info> get_list(int IdEmpresa, int skip, int take, string filter)
        {
            try
            {
                List<cp_proveedor_Info> Lista = new List<cp_proveedor_Info>();

                Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar();

                var lst = (from
                          p in Context.vwcp_proveedor_consulta
                          
                           where
                            p.IdEmpresa == IdEmpresa
                           
                            && (p.IdProveedor.ToString() + " " + p.pe_nombreCompleto).Contains(filter)
                           select new
                           {
                               p.IdEmpresa,
                               p.IdPersona,
                               p.IdProveedor,
                               p.pe_cedulaRuc,
                               p.pr_codigo,
                               p.pe_nombreCompleto
                             
                           })
                             .OrderBy(p => p.IdProveedor)
                             .Skip(skip)
                             .Take(take)
                             .ToList();


                foreach (var q in lst)
                {
                    Lista.Add(new cp_proveedor_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdPersona = q.IdPersona,
                        IdProveedor = q.IdProveedor,
                        pr_codigo=q.pr_codigo,
                        info_persona=new Info.General.tb_persona_Info
                        {
                            pe_cedulaRuc=q.pe_cedulaRuc,
                            pe_nombreCompleto=q.pe_nombreCompleto,
                        }
                        
                    });
                }

                Context.Dispose();
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion



    }
}
