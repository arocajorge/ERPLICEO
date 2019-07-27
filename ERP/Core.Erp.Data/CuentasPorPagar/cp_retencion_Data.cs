using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Data.General;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Data.Contabilidad;
using Core.Erp.Data.Facturacion;

namespace Core.Erp.Data.CuentasPorPagar
{
   public class cp_retencion_Data
    {
        tb_sis_Documento_Tipo_Talonario_Data odata_talonario = new tb_sis_Documento_Tipo_Talonario_Data();
        tb_sis_Documento_Tipo_Talonario_Info info_documento = new tb_sis_Documento_Tipo_Talonario_Info();
        fa_PuntoVta_Data odata_pto = new fa_PuntoVta_Data();
        public List<cp_retencion_Info> get_list(int IdEmpresa, int IdSucursal, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {

                List<cp_retencion_Info> lista = new List<cp_retencion_Info>();
                fecha_ini = fecha_ini.Date;
                fecha_fin = fecha_fin.Date;

                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    lista = (from item in Context.vwcp_retencion
                             where item.IdEmpresa == IdEmpresa
                             && IdSucursalIni <= item.IdSucursal
                             && item.IdSucursal <= IdSucursalFin
                             && fecha_ini <= item.fecha
                             && item.fecha <= fecha_fin
                             orderby item.IdRetencion descending
                             select new cp_retencion_Info
                             {

                                 IdEmpresa = item.IdEmpresa,
                                 IdRetencion = item.IdRetencion,
                                 pe_apellido = item.pe_apellido,
                                 pe_nombre = item.pe_nombre,
                                 pe_razonSocial = item.pe_razonSocial,
                                 NumRetencion = item.NumRetencion,
                                 serie1 = item.serie1,
                                 serie2 = item.serie2,
                                 co_serie = item.co_serie,
                                 co_factura = item.co_factura,
                                 co_baseImponible = item.co_baseImponible,
                                 Estado = item.Estado,
                                 pe_nombreCompleto=item.pe_nombreCompleto,
                                 fecha = item.fecha,
                                 NAutorizacion = item.NAutorizacion,
                                 EstadoBool = item.Estado == "A" ? true : false

                             }).ToList();
                }

                return lista;
            }
            catch (Exception )
            {
               
                throw ;
            }
        }
        public cp_retencion_Info get_info(int IdEmpresa, decimal IdRetencion)
        {
            try
            {
                cp_retencion_Info info = new cp_retencion_Info();
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    vwcp_retencion Entity = Context.vwcp_retencion.FirstOrDefault(q => q.IdEmpresa == IdEmpresa & q.IdRetencion==IdRetencion);
                    if (Entity == null) return null;
                    info = new cp_retencion_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdProveedor = Entity.IdProveedor,
                        ct_IdTipoCbte = Entity.ct_IdTipoCbte,
                        ct_IdCbteCble = Entity.ct_IdCbteCble,
                        IdRetencion = Entity.IdRetencion,
                        CodDocumentoTipo = Entity.CodDocumentoTipo,
                        serie1 = Entity.serie1,
                        serie2 = Entity.serie2,
                        NumRetencion = Entity.NumRetencion,
                        NAutorizacion = Entity.NAutorizacion,
                        observacion = Entity.observacion,
                        fecha = Convert.ToDateTime(Entity.fecha.ToShortDateString()),
                        co_baseImponible = Entity.co_baseImponible,
                        co_serie = Entity.co_serie,
                        co_factura = Entity.co_factura,
                        co_subtotal_iva = Entity.co_subtotal_iva,
                        co_subtotal_siniva = Entity.co_subtotal_siniva,
                        co_valoriva = Entity.co_valoriva,
                        pe_razonSocial = Entity.pe_razonSocial,
                        IdTipoCbte_Ogiro = Entity.IdTipoCbte_Ogiro,
                        IdCbteCble_Ogiro = Entity.IdCbteCble_Ogiro,
                        IdSucursal_cxp = Entity.IdSucursal_cxp,
                        IdSucursal = Entity.IdSucursal,
                        IdPuntoVta = Entity.IdPuntoVta,

                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public cp_retencion_Info get_info(int IdEmpresa_Ogiro, decimal IdCbteCble_Ogiro,int IdTipoCbte_Ogiro)
        {
            try
            {
                cp_retencion_Info info = new cp_retencion_Info();
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_retencion Entity = Context.cp_retencion.FirstOrDefault(q => q.IdEmpresa == IdEmpresa_Ogiro & q.IdCbteCble_Ogiro == IdCbteCble_Ogiro && q.IdTipoCbte_Ogiro== IdTipoCbte_Ogiro);
                    if (Entity == null) return null;
                    info = new cp_retencion_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdRetencion = Entity.IdRetencion,
                        CodDocumentoTipo = Entity.CodDocumentoTipo,
                        IdTipoCbte_Ogiro=Entity.IdTipoCbte_Ogiro,
                        IdCbteCble_Ogiro=Entity.IdCbteCble_Ogiro,
                        IdEmpresa_Ogiro=Entity.IdEmpresa_Ogiro,
                        serie1 = Entity.serie1,
                        serie2 = Entity.serie2,
                        NumRetencion = Entity.NumRetencion,
                        NAutorizacion = Entity.NAutorizacion,
                        observacion = Entity.observacion,
                        fecha = Convert.ToDateTime(Entity.fecha),
                        IdSucursal = Entity.IdSucursal ?? 0,
                        IdPuntoVta = Entity.IdPuntoVta
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Boolean guardarDB(cp_retencion_Info info)
        {
            Boolean res = true;
            tb_sis_Documento_Tipo_Talonario_Info info_documento = new tb_sis_Documento_Tipo_Talonario_Info();
            tb_sis_Documento_Tipo_Talonario_Data data_talonario = new tb_sis_Documento_Tipo_Talonario_Data();

            var punto_venta = odata_pto.get_info(info.IdEmpresa, info.IdSucursal, info.IdPuntoVta ?? 0);
            if (punto_venta != null)
            {
                if (punto_venta.EsElectronico == true)
                {
                    info_documento = odata_talonario.GetUltimoNoUsado(info.IdEmpresa, punto_venta.codDocumentoTipo, punto_venta.Su_CodigoEstablecimiento, punto_venta.cod_PuntoVta, punto_venta.EsElectronico, true);
                }
                else
                {
                    info_documento.IdEmpresa = info.IdEmpresa;
                    info_documento.CodDocumentoTipo = info.CodDocumentoTipo;
                    info_documento.Establecimiento = info.serie1;
                    info_documento.PuntoEmision = info.serie2;
                    info_documento.NumDocumento = info.NumRetencion;
                    info_documento.IdSucursal = info.IdSucursal;
                    info_documento.Usado = true;

                    data_talonario.modificar_estado_usadoDB(info_documento);                    
                }
                
            }
            
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    var lstf = Context.cp_retencion.Where(q => q.IdEmpresa_Ogiro == info.IdEmpresa && q.IdTipoCbte_Ogiro == info.IdTipoCbte_Ogiro && q.IdCbteCble_Ogiro == info.IdCbteCble_Ogiro).FirstOrDefault();
                    if (lstf != null)
                        return false;
                    
                    Context.cp_retencion.Add(new cp_retencion
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdEmpresa_Ogiro = info.IdEmpresa_Ogiro,
                        IdCbteCble_Ogiro = info.IdCbteCble_Ogiro,
                        IdTipoCbte_Ogiro = info.IdTipoCbte_Ogiro,
                        IdRetencion = info.IdRetencion = get_id(info.IdEmpresa),
                        CodDocumentoTipo = info.CodDocumentoTipo = "RETEN",
                        
                        serie1 = info.serie1 = info_documento == null ? null : info_documento.Establecimiento,
                        serie2 = info.serie2 = info_documento == null ? null : info_documento.PuntoEmision,
                        NumRetencion = info.NumRetencion = info_documento == null ? null : info_documento.NumDocumento,

                        NAutorizacion = info.NAutorizacion,
                        observacion = info.observacion,
                        fecha = info.fecha.Date,
                        Estado = "A",
                        IdPuntoVta = info.IdPuntoVta,
                        IdSucursal = info.IdSucursal,
                        Fecha_Transac = DateTime.Now,
                        IdUsuario = info.IdUsuario,
                    });

                    int Secuencia = 1;
                    foreach (var item in info.detalle)
                    {
                        Context.cp_retencion_det.Add(new cp_retencion_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdRetencion = info.IdRetencion,
                            Idsecuencia = Secuencia++,
                            re_tipoRet = item.re_tipoRet,
                            re_Codigo_impuesto=item.re_Codigo_impuesto,
                            re_baseRetencion=(double)item.re_baseRetencion,
                            re_Porcen_retencion= (double)item.re_Porcen_retencion,
                            re_valor_retencion= (double)item.re_valor_retencion,
                            IdCodigo_SRI=item.IdCodigo_SRI,
                            IdUsuario=info.IdUsuario,
                            re_estado="A"
                        });                        
                    }
                    Context.SaveChanges();

                    if (Math.Round((double)info.detalle.Sum(q=>q.re_valor_retencion),2) > 0.01)
                    {
                        ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();
                        var param = Context.cp_parametros.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                        var diario = odata_ct.armar_info(info.info_comprobante.lst_ct_cbtecble_det, info.IdEmpresa, info.IdSucursal, (int)param.pa_IdTipoCbte_x_Retencion, 0,
                            "Comprobante contable de retención #" + info.serie1 + " " + info.serie2 + " " + info.NumRetencion
                        , info.fecha);
                        odata_ct.guardarDB(diario);

                        Context.cp_retencion_x_ct_cbtecble.Add(new cp_retencion_x_ct_cbtecble
                        {
                            rt_IdEmpresa = info.IdEmpresa,
                            rt_IdRetencion = info.IdRetencion,

                            ct_IdEmpresa = diario.IdEmpresa,
                            ct_IdTipoCbte = diario.IdTipoCbte,
                            ct_IdCbteCble = diario.IdCbteCble,
                            Observacion = "Relacion"
                        });
                        Context.SaveChanges();
                    }
                }
            
                return res;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_retencion_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool modificarDB(cp_retencion_Info info)
        {
            try
            {
                int sec = 1;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    var contact = Context.cp_retencion.FirstOrDefault(minfo => minfo.IdEmpresa == info.IdEmpresa && minfo.IdRetencion == info.IdRetencion);
                    if (contact != null)
                    {
                        if (string.IsNullOrEmpty(contact.NumRetencion))
                        {
                            
                            tb_sis_Documento_Tipo_Talonario_Data odata_talonario = new tb_sis_Documento_Tipo_Talonario_Data();
                            var punto_venta = odata_pto.get_info(info.IdEmpresa, info.IdSucursal, info.IdPuntoVta ?? 0);
                            if (punto_venta != null)
                                info_documento = odata_talonario.GetUltimoNoUsado(info.IdEmpresa, cl_enumeradores.eTipoDocumento.RETEN.ToString(), info.serie1, info.serie2, punto_venta.EsElectronico, true);
                            
                            contact.serie1 = info.serie1 = info_documento == null ? null : info_documento.Establecimiento;
                            contact.serie2 = info.serie2 = info_documento == null ? null : info_documento.PuntoEmision;
                            contact.NumRetencion = info.NumRetencion = info_documento == null ? null : info_documento.NumDocumento;
                            
                        }
                        //contact.IdPuntoVta = info.IdPuntoVta;
                        contact.IdSucursal = info.IdSucursal;
                        contact.fecha = info.fecha;
                        contact.observacion = info.observacion;
                        contact.IdUsuarioUltMod = info.IdUsuarioUltMod;
                        contact.Fecha_UltMod = DateTime.Now;
                        contact.IdUsuarioUltMod = info.IdUsuario;

                        if (info.detalle != null)
                        {
                            var lista = Context.cp_retencion_det.Where(minfo => minfo.IdEmpresa == info.IdEmpresa && minfo.IdRetencion == info.IdRetencion);
                            Context.cp_retencion_det.RemoveRange(lista);

                            foreach (var item in info.detalle)
                            {
                                cp_retencion_det Entity = new cp_retencion_det
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdRetencion = info.IdRetencion,
                                    Idsecuencia = sec,
                                    re_tipoRet = item.re_tipoRet,
                                    re_baseRetencion = (double)item.re_baseRetencion,
                                    IdCodigo_SRI = item.IdCodigo_SRI,
                                    re_Codigo_impuesto = item.re_Codigo_impuesto,
                                    re_valor_retencion = Math.Round((double)item.re_valor_retencion, 2, MidpointRounding.AwayFromZero),
                                    re_Porcen_retencion = (double)item.re_Porcen_retencion,
                                    re_estado = "A"
                                };
                                Context.cp_retencion_det.Add(Entity);
                                sec++;
                            }
                        }
                        Context.SaveChanges();

                        if (Math.Round((double)info.detalle.Sum(q => q.re_valor_retencion), 2) > 0.01)
                        {
                            ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();
                            var param = Context.cp_parametros.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                            var diario = odata_ct.armar_info(info.info_comprobante.lst_ct_cbtecble_det, info.IdEmpresa, info.IdSucursal, (info.info_comprobante.IdTipoCbte == 0 ? Convert.ToInt32(param.pa_IdTipoCbte_x_Retencion) : info.info_comprobante.IdTipoCbte), 0,
                                "Comprobante contable de retención #" + info.serie1 + " " + info.serie2 + " " + info.NumRetencion, info.fecha);
                            if (diario != null)
                            {
                                var rel = Context.cp_retencion_x_ct_cbtecble.Where(q => q.rt_IdEmpresa == info.IdEmpresa && q.rt_IdRetencion == info.IdRetencion).FirstOrDefault();
                                if (rel == null)
                                {
                                    if (odata_ct.guardarDB(diario))
                                    {
                                        Context.cp_retencion_x_ct_cbtecble.Add(new cp_retencion_x_ct_cbtecble
                                        {
                                            rt_IdEmpresa = info.IdEmpresa,
                                            rt_IdRetencion = info.IdRetencion,

                                            ct_IdEmpresa = diario.IdEmpresa,
                                            ct_IdTipoCbte = diario.IdTipoCbte,
                                            ct_IdCbteCble = diario.IdCbteCble,
                                            Observacion = "Relacion"
                                        });
                                        Context.SaveChanges();
                                    }
                                }
                                else
                                {
                                    diario.IdCbteCble = rel.ct_IdCbteCble;
                                    odata_ct.modificarDB(diario);
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_retencion_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;

            }
        }
        public Boolean anularDB(cp_retencion_Info info)
        {
            try
            {
                Boolean res = false;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    var contact = Context.cp_retencion.FirstOrDefault(minfo => minfo.IdEmpresa == info.IdEmpresa && minfo.IdRetencion == info.IdRetencion);
                    if (contact != null)
                    {
                        contact.IdEmpresa_Ogiro = null;
                        contact.IdTipoCbte_Ogiro = null;
                        contact.IdCbteCble_Ogiro = null;

                        contact.Estado = "I";
                        contact.observacion = "*ANULADO* " + contact.observacion;
                        contact.Fecha_UltAnu = DateTime.Now;
                        contact.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                        Context.SaveChanges();
                        res = true;
                    }
                }
                return res;
            }
            catch (Exception )
            {
                throw;

            }
        }
        public decimal get_id(int IdEmpresa)
        {
            decimal Id;
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Id = Context.cp_retencion.Count(C => C.IdEmpresa == IdEmpresa);

                    if (Id == 0)
                        Id = 1;
                    else
                    {
                        decimal select_ = (from C in Context.cp_retencion
                                           where C.IdEmpresa == IdEmpresa
                                           select C.IdRetencion).Max();
                        Id = select_ + 1;
                    }
                    return Id;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ModificarEstadoAutorizacion(int IdEmpresa, int IdTipoCbte_Ogiro, decimal IdCbteCble_Ogiro)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    var Entity = Context.cp_retencion.Where(q => q.IdEmpresa == IdEmpresa && q.IdTipoCbte_Ogiro == IdTipoCbte_Ogiro && q.IdCbteCble_Ogiro == IdCbteCble_Ogiro).FirstOrDefault();
                    if (Entity != null)
                    {
                        Entity.aprobada_enviar_sri = true;
                        Context.SaveChanges();
                    }
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
