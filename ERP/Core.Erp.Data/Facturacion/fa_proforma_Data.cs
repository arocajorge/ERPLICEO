using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
  public  class fa_proforma_Data
    {
         public List<fa_proforma_Info> get_list(int IdEmpresa, int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                List<fa_proforma_Info> Lista;
                Fecha_ini = Fecha_ini.Date;
                Fecha_fin = Fecha_fin.Date;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Lista = (from q in Context.vwfa_proforma
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && Fecha_ini <= q.pf_fecha && q.pf_fecha <= Fecha_fin
                             orderby q.IdProforma descending
                             select new fa_proforma_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdProforma = q.IdProforma,
                                 pf_codigo = q.pf_codigo,
                                 pf_observacion = q.pf_observacion,
                                 pf_fecha = q.pf_fecha,
                                 estado = q.estado,
                                 pr_dias_entrega = q.pr_dias_entrega,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 pd_subtotal = q.pd_subtotal,
                                 pd_iva = q.pd_iva,
                                 pd_total = q.pd_total,
                                 EstadoCierre = q.EstadoCierre,
                                 Su_Descripcion = q.Su_Descripcion
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public fa_proforma_Info get_info(int IdEmpresa, int IdSucursal, decimal IdProforma)
        {
            try
            {
                fa_proforma_Info info = new fa_proforma_Info();
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_proforma Entity = Context.fa_proforma.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdProforma == IdProforma);
                    if (Entity == null) return null;
                    info = new fa_proforma_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdProforma = Entity.IdProforma,
                        IdCliente = Entity.IdCliente,
                        IdTerminoPago = Entity.IdTerminoPago,
                        pf_plazo = Entity.pf_plazo,
                        pf_codigo = Entity.pf_codigo,
                        pf_observacion = Entity.pf_observacion,
                        pf_fecha = Entity.pf_fecha,
                        pf_fecha_vcto = Entity.pf_fecha_vcto,
                        estado = Entity.estado,
                        IdBodega = Entity.IdBodega,
                        IdVendedor = Entity.IdVendedor,
                        pr_dias_entrega = Entity.pr_dias_entrega,
                        IdNivel = Entity.IdNivel,
                        IdCatalogo_FormaPago= Entity.IdCatalogo_FormaPago
                    };

                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal get_id(int IdEmpresa, int IdSucursal)
        {

            try
            {
                decimal ID = 1;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var lst = from q in Context.fa_proforma
                              where q.IdEmpresa == IdEmpresa
                              && q.IdSucursal == IdSucursal
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdProforma) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_proforma_Info info)
        {
            try
            {
                int secuencia = 1;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Context.fa_proforma.Add(new fa_proforma
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdProforma = info.IdProforma=get_id(info.IdEmpresa, info.IdSucursal),
                        IdCliente = info.IdCliente,
                        IdTerminoPago = info.IdTerminoPago,
                        pf_plazo = info.pf_plazo,
                        pf_codigo = info.pf_codigo,
                        pf_observacion = info.pf_observacion,
                        pf_fecha = info.pf_fecha,
                        pf_fecha_vcto = info.pf_fecha_vcto,
                        estado = info.estado = true,
                        IdBodega = info.IdBodega,
                        IdVendedor = info.IdVendedor,
                        pr_dias_entrega = info.pr_dias_entrega,
                        IdNivel = info.IdNivel,
                        IdCatalogo_FormaPago = info.IdCatalogo_FormaPago,

                        IdUsuario_creacion = info.IdUsuario_creacion,
                        fecha_creacion = DateTime.Now
                    });

                    foreach (var item in info.lst_det)
                    {
                        Context.fa_proforma_det.Add(new fa_proforma_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = info.IdSucursal,
                            IdProforma = info.IdProforma,
                            Secuencia = secuencia++,
                            IdProducto = item.IdProducto,
                            pd_cantidad = item.pd_cantidad,
                            pd_precio = item.pd_precio,
                            pd_por_descuento_uni = item.pd_por_descuento_uni,
                            pd_descuento_uni = item.pd_descuento_uni,
                            pd_precio_final = item.pd_precio_final,
                            pd_subtotal = item.pd_subtotal,
                            IdCod_Impuesto = item.IdCod_Impuesto,
                            pd_por_iva = item.pd_por_iva,
                            pd_iva = item.pd_iva,
                            anulado = item.anulado,
                            pd_total = item.pd_total,
                        });
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

        public bool modificarDB(fa_proforma_Info info)
        {
            try
            {
                int secuencia = 1;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_proforma Entity = Context.fa_proforma.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdProforma == info.IdProforma);
                    if (Entity == null) return false;

                    Entity.IdCliente = info.IdCliente;
                    Entity.IdTerminoPago = info.IdTerminoPago;
                    Entity.pf_plazo = info.pf_plazo;
                    Entity.pf_codigo = info.pf_codigo;
                    Entity.pf_observacion = info.pf_observacion;
                    Entity.pf_fecha = info.pf_fecha;
                    Entity.pf_fecha_vcto = info.pf_fecha_vcto;
                    Entity.IdBodega = info.IdBodega;
                    Entity.IdVendedor = info.IdVendedor;
                    Entity.pr_dias_entrega = info.pr_dias_entrega;
                    Entity.IdNivel = info.IdNivel;
                    Entity.IdUsuario_modificacion = info.IdUsuario_modificacion;
                    Entity.fecha_modificacion = DateTime.Now;
                    Entity.IdCatalogo_FormaPago = info.IdCatalogo_FormaPago;

                    var lst = Context.fa_proforma_det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdProforma == info.IdProforma);
                    Context.fa_proforma_det.RemoveRange(lst);

                    foreach (var item in info.lst_det)
                    {
                        Context.fa_proforma_det.Add(new fa_proforma_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = info.IdSucursal,
                            IdProforma = info.IdProforma,
                            Secuencia = secuencia++,
                            IdProducto = item.IdProducto,
                            pd_cantidad = item.pd_cantidad,
                            pd_precio = item.pd_precio,
                            pd_por_descuento_uni = item.pd_por_descuento_uni,
                            pd_descuento_uni = item.pd_descuento_uni,
                            pd_precio_final = item.pd_precio_final,
                            pd_subtotal = item.pd_subtotal,
                            IdCod_Impuesto = item.IdCod_Impuesto,
                            pd_por_iva = item.pd_por_iva,
                            pd_iva = item.pd_iva,
                            anulado = item.anulado,
                            pd_total = item.pd_total,
                        });
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

        public bool anularDB(fa_proforma_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_proforma Entity = Context.fa_proforma.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdProforma == info.IdProforma);
                    if (Entity == null) return false;

                    Entity.estado = info.estado = false;

                    Entity.IdUsuario_anulacion = info.IdUsuario_anulacion;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.fecha_anulacion = DateTime.Now;

                    var lst = Context.fa_proforma_det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdProforma == info.IdProforma).ToList();
                    foreach (var item in lst)
                    {
                        item.anulado = true;
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

    }
}
