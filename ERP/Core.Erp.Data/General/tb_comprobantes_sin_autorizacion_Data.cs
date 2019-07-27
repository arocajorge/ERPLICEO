using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Helps;
namespace Core.Erp.Data.General
{
   public class tb_comprobantes_sin_autorizacion_Data
    {
        public List<tb_comprobantes_sin_autorizacion_Info> get_list(int IdEmpresa, string Tipo_doc, DateTime Fecha_ini, DateTime Fecha_fin, int IdSucursal)
        {
            try
            {
                Fecha_fin = Convert.ToDateTime(Fecha_fin.ToShortDateString());
                Fecha_ini = Convert.ToDateTime(Fecha_ini.ToShortDateString());
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 99999 : IdSucursal;
                int sec = 0;
                List<tb_comprobantes_sin_autorizacion_Info> Lista;
                using (Entities_general Context=new Entities_general())
                {
                   
                            if (Tipo_doc=="")
                                Lista = (from q in Context.vwtb_comprobantes_no_autorizados
                                         where q.IdEmpresa==IdEmpresa
                                         && q.vt_fecha>=Fecha_ini
                                         && q.vt_fecha<=Fecha_fin
                                         && IdSucursalIni <= q.IdSucursal
                                         && q.IdSucursal <= IdSucursalFin
                                         select new tb_comprobantes_sin_autorizacion_Info
                                         {
                                             IdEmpresa = q.IdEmpresa,
                                             IdCbteVta = q.IdCbteVta,
                                             Tipo_documento = q.Tipo_documento,
                                             Documento = q.Documento,
                                             
                                             pe_nombreCompleto = q.pe_nombreCompleto,
                                             vt_fecha = q.vt_fecha,
                                             vt_Observacion=q.vt_Observacion,
                                             vt_serie1=q.vt_serie1,
                                             vt_serie2=q.vt_serie2,
                                             DocumentoDoc=q.DocumentoDoc
                                         }).ToList();
                            else
                                Lista = (from q in Context.vwtb_comprobantes_no_autorizados
                                         where q.Tipo_documento == Tipo_doc
                                         where q.IdEmpresa == IdEmpresa
                                         && q.vt_fecha >= Fecha_ini
                                         && q.vt_fecha <= Fecha_fin
                                         && IdSucursalIni <= q.IdSucursal
                                         && q.IdSucursal <= IdSucursalFin
                                         select new tb_comprobantes_sin_autorizacion_Info
                                         {
                                             IdEmpresa = q.IdEmpresa,
                                             IdCbteVta = q.IdCbteVta,
                                             Tipo_documento = q.Tipo_documento,
                                             Documento = q.Documento,
                                             pe_nombreCompleto = q.pe_nombreCompleto,
                                             vt_fecha = q.vt_fecha,
                                             vt_Observacion = q.vt_Observacion,
                                             vt_serie1 = q.vt_serie1,
                                             vt_serie2 = q.vt_serie2,
                                             DocumentoDoc = q.DocumentoDoc
                                         }).ToList();

                    Lista.ForEach(v => v.secuencia = sec++);
                    return Lista;

                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarEstado(tb_comprobantes_sin_autorizacion_Info info)
        {
            try
            {
                if(info.Tipo_documento==cl_enumeradores.eTipoDocumento.FACT.ToString())
                using (Entities_facturacion Context=new Entities_facturacion())
                {
                    var Entity = Context.fa_factura.Where(q => q.IdEmpresa == info.IdEmpresa 
                    && q.IdCbteVta == info.IdCbteVta 
                    && q.vt_serie1 == info.vt_serie1 
                    && q.vt_serie2==info.vt_serie2
                    && q.vt_NumFactura==info.DocumentoDoc).FirstOrDefault();
                    Entity.aprobada_enviar_sri = true;
                    Context.SaveChanges();
                }
                if (info.Tipo_documento == cl_enumeradores.eTipoDocumento.RETEN.ToString())
                    using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    var Entity = Context.cp_retencion.Where(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdRetencion == info.IdCbteVta
                    && q.serie1 == info.vt_serie1
                    && q.serie2 == info.vt_serie2
                    && q.NumRetencion == info.DocumentoDoc).FirstOrDefault();
                    Entity.aprobada_enviar_sri = true;
                    Context.SaveChanges();
                }
                if (info.Tipo_documento == cl_enumeradores.eTipoDocumento.NTCR.ToString())
                    using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var Entity = Context.fa_notaCreDeb.Where(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdNota == info.IdCbteVta
                    && q.Serie1 == info.vt_serie1
                    && q.Serie2 == info.vt_serie2
                    && q.NumNota_Impresa == info.DocumentoDoc).FirstOrDefault();
                    Entity.aprobada_enviar_sri = true;
                    Context.SaveChanges();
                    }
                if (info.Tipo_documento == cl_enumeradores.eTipoDocumento.GUIA.ToString())

                    using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var Entity = Context.fa_guia_remision.Where(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdGuiaRemision == info.IdCbteVta
                    && q.Serie1 == info.vt_serie1
                    && q.Serie2 == info.vt_serie2
                    && q.NumGuia_Preimpresa == info.DocumentoDoc).FirstOrDefault();
                    Entity.aprobada_enviar_sri = true;
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
