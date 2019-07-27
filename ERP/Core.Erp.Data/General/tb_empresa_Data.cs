using Core.Erp.Info.General;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_empresa_Data
    {
        public List<tb_empresa_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<tb_empresa_Info> Lista;

                using (Entities_general Context = new Entities_general())
                {
                    if(mostrar_anulados)
                    Lista = (from q in Context.tb_empresa
                             select new tb_empresa_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 codigo = q.codigo,
                                 em_nombre = q.em_nombre,
                                 em_ruc = q.em_ruc,
                                 Estado = q.Estado,
                                 em_direccion = q.em_direccion,
                                 em_telefonos = q.em_telefonos,

                                 EstadoBool = q.Estado == "A" ? true : false
                             }).ToList();
                    else
                        Lista = (from q in Context.tb_empresa
                                 where q.Estado == "A"
                                 select new tb_empresa_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     codigo = q.codigo,
                                     em_nombre = q.em_nombre,
                                     em_ruc = q.em_ruc,
                                     Estado = q.Estado,
                                     em_direccion = q.em_direccion,
                                     em_telefonos = q.em_telefonos,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_empresa_Info get_info(int IdEmpresa)
        {
            try
            {
                tb_empresa_Info info = new tb_empresa_Info();

                using (Entities_general Context = new Entities_general())
                {
                    tb_empresa Entity = Context.tb_empresa.FirstOrDefault(q => q.IdEmpresa == IdEmpresa);
                    if (Entity == null) return null;
                    info = new tb_empresa_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        codigo = Entity.codigo,
                        em_nombre = Entity.em_nombre,
                        RazonSocial = Entity.RazonSocial,
                        NombreComercial = Entity.NombreComercial,
                        ContribuyenteEspecial = Entity.ContribuyenteEspecial,
                        em_ruc = Entity.em_ruc,
                        em_gerente = Entity.em_gerente,
                        em_contador = Entity.em_contador,
                        em_rucContador = Entity.em_rucContador,
                        em_telefonos = Entity.em_telefonos,
                        em_direccion = Entity.em_direccion,
                        em_logo = Entity.em_logo,
                        em_fechaInicioContable = Entity.em_fechaInicioContable,
                        Estado = Entity.Estado,
                        em_fechaInicioActividad = Entity.em_fechaInicioActividad,
                        cod_entidad_dinardap = Entity.cod_entidad_dinardap,
                        em_Email = Entity.em_Email,
                        EstadoBool = (Entity.Estado == "A") ? true : false
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int get_id()
        {
            try
            {
                int ID = 1;

                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.tb_empresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdEmpresa) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_empresa_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_empresa Entity = new tb_empresa
                    {
                        IdEmpresa = info.IdEmpresa = get_id(),
                        codigo = info.codigo,
                        em_nombre = info.em_nombre,
                        RazonSocial = info.RazonSocial,
                        NombreComercial = info.NombreComercial,
                        ContribuyenteEspecial = info.ContribuyenteEspecial,
                        em_ruc = info.em_ruc,
                        em_gerente = info.em_gerente,
                        em_contador = info.em_contador,
                        em_rucContador = info.em_rucContador,
                        em_telefonos = info.em_telefonos,
                        em_direccion = info.em_direccion,
                        em_logo = info.em_logo,
                        em_fechaInicioContable = info.em_fechaInicioContable,
                        Estado = info.Estado = "A",
                        em_fechaInicioActividad = Convert.ToDateTime(info.em_fechaInicioActividad),
                        cod_entidad_dinardap = info.cod_entidad_dinardap,
                        em_Email = info.em_Email,
                    };
                    Context.tb_empresa.Add(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(tb_empresa_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_empresa Entity = Context.tb_empresa.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa);
                    if (Entity == null) return false;
                    Entity.codigo = info.codigo;
                    Entity.em_nombre = info.em_nombre;
                    Entity.RazonSocial = info.RazonSocial;
                    Entity.NombreComercial = info.NombreComercial;
                    Entity.ContribuyenteEspecial = info.ContribuyenteEspecial;
                    Entity.em_ruc = info.em_ruc;
                    Entity.em_gerente = info.em_gerente;
                    Entity.em_contador = info.em_contador;
                    Entity.em_rucContador = info.em_rucContador;
                    Entity.em_telefonos = info.em_telefonos;
                    Entity.em_direccion = info.em_direccion;
                    Entity.em_logo = info.em_logo;
                    Entity.em_fechaInicioContable = info.em_fechaInicioContable;
                    Entity.em_fechaInicioActividad = Convert.ToDateTime(info.em_fechaInicioActividad);
                    Entity.cod_entidad_dinardap = info.cod_entidad_dinardap;
                    Entity.em_Email = info.em_Email;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(tb_empresa_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_empresa Entity = Context.tb_empresa.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa);
                    if (Entity == null) return false;
                    Entity.Estado = info.Estado = "I";
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDbImportacion(List<tb_empresa_Info> Lista_Empresa, List<tb_sucursal_Info> Lista_Sucursal, List<tb_bodega_Info> Lista_Bodega)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {


                    if (Lista_Empresa.Count > 0)
                    {
                        foreach (var item in Lista_Empresa)
                        {
                            tb_empresa Entity_Emp = new tb_empresa
                            {
                                IdEmpresa = item.IdEmpresa,
                                codigo = item.codigo,
                                em_nombre = item.em_nombre,
                                RazonSocial = item.RazonSocial,
                                NombreComercial = item.NombreComercial,
                                ContribuyenteEspecial = item.ContribuyenteEspecial,
                                em_ruc = item.em_ruc,
                                em_gerente = item.em_gerente,
                                em_contador = item.em_contador,
                                em_rucContador = item.em_rucContador,
                                em_telefonos = item.em_telefonos,
                                em_direccion = item.em_direccion,
                                em_fechaInicioContable = item.em_fechaInicioContable,
                                cod_entidad_dinardap = item.cod_entidad_dinardap,
                                em_Email = item.em_Email,
                                Estado = item.Estado = "A"
                            };
                            Context.tb_empresa.Add(Entity_Emp);
                        }
                    }

                    if (Lista_Sucursal.Count > 0)
                    {
                        foreach (var item in Lista_Sucursal)
                        {
                            tb_sucursal Entity_Sur = new tb_sucursal
                            {
                                IdEmpresa = item.IdEmpresa,
                                IdSucursal = item.IdSucursal,
                                codigo = item.codigo,
                                Su_Descripcion = item.Su_Descripcion,
                                Su_CodigoEstablecimiento = item.Su_CodigoEstablecimiento,
                                Su_Ruc = item.Su_Ruc,
                                Su_JefeSucursal = item.Su_JefeSucursal,
                                Su_Telefonos = item.Su_Telefonos,
                                Su_Direccion = item.Su_Direccion,
                                Estado = item.Estado = "A",
                                IdUsuario = item.IdUsuario,
                            };
                            Context.tb_sucursal.Add(Entity_Sur);
                        }
                    }

                    if (Lista_Bodega.Count > 0)
                    {
                        foreach (var item in Lista_Bodega)
                        {
                            tb_bodega Entity_Bod = new tb_bodega
                            {

                                IdEmpresa = item.IdEmpresa,
                                IdSucursal = item.IdSucursal,
                                IdBodega = item.IdBodega,
                                cod_bodega = item.cod_bodega,
                                bo_Descripcion = item.bo_Descripcion,
                                IdCtaCtble_Inve = string.IsNullOrEmpty(item.IdCtaCtble_Inve) ? null : item.IdCtaCtble_Inve,
                                Estado = item.Estado = "A",
                                IdUsuario = item.IdUsuario,

                            };
                            Context.tb_bodega.Add(Entity_Bod);
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



        #region Bajo demanda
        public tb_empresa_Info get_info_demanda(int value)
        {
            tb_empresa_Info info = new tb_empresa_Info();
            using (Entities_general Contex = new Entities_general())
            {
                info = (from q in Contex.tb_empresa
                        where q.IdEmpresa == value
                        select new tb_empresa_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            codigo = q.codigo,
                            em_nombre = q.em_nombre
                        }).FirstOrDefault();
            }
            return info;
        }
        public tb_empresa_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            decimal id;
            if (args.Value == null || !decimal.TryParse(args.Value.ToString(), out id))
                return null;
            return get_info_demanda((int)args.Value);
        }
        public List<tb_empresa_Info> get_list(int skip, int take, string filter)
        {
            try
            {
                List<tb_empresa_Info> Lista = new List<tb_empresa_Info>();

                Entities_general context_g = new Entities_general();

                var lstg = context_g.tb_empresa.Where(q => q.Estado == "A" &&(q.IdEmpresa.ToString() + " " + q.em_nombre).Contains(filter)).OrderBy(q => q.IdEmpresa).Skip(skip).Take(take);
                foreach (var q in lstg)
                {
                    Lista.Add(new tb_empresa_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        codigo = q.codigo,
                        em_nombre = q.em_nombre
                    });
                }
                context_g.Dispose();
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<tb_empresa_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<tb_empresa_Info> Lista = new List<tb_empresa_Info>();
            Lista = get_list(skip, take, args.Filter);
            return Lista;
        }

        #endregion
    }
}
