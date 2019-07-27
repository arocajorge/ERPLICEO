using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
    public class fa_notaCreDeb_det_Data
    {
        public List<fa_notaCreDeb_det_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            try
            {
                List<fa_notaCreDeb_det_Info> Lista;

                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Lista = (from q in Context.vwfa_notaCreDeb_det
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdBodega == IdBodega
                             && q.IdNota == IdNota
                             select new fa_notaCreDeb_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdNota = q.IdNota,
                                 Secuencia = q.Secuencia,
                                 IdProducto = q.IdProducto,
                                 sc_cantidad = q.sc_cantidad,
                                 sc_Precio = q.sc_Precio,
                                 sc_descUni = q.sc_descUni,
                                 sc_PordescUni = q.sc_PordescUni,
                                 sc_precioFinal = q.sc_precioFinal,
                                 sc_subtotal = q.sc_subtotal,
                                 sc_iva = q.sc_iva,
                                 sc_total = q.sc_total,
                                 sc_costo = q.sc_costo,
                                 sc_observacion = q.sc_observacion,
                                 vt_por_iva = q.vt_por_iva,
                                 IdPunto_Cargo = q.IdPunto_Cargo,
                                 IdPunto_cargo_grupo = q.IdPunto_cargo_grupo,
                                 IdCod_Impuesto_Iva = q.IdCod_Impuesto_Iva,
                                 IdCentroCosto = q.IdCentroCosto,
                                 pr_descripcion = q.pr_descripcion,
                                 nom_presentacion = q.nom_presentacion,
                                 lote_fecha_vcto = q.lote_fecha_vcto,
                                 lote_num_lote = q.lote_num_lote,
                                 sc_cantidad_factura=q.sc_cantidad_factura
                             }).ToList();
                }
                Lista.ForEach(V =>
                {
                    V.pr_descripcion = V.pr_descripcion + " " + V.nom_presentacion + " - " + V.lote_num_lote + " - " + (V.lote_fecha_vcto != null ? Convert.ToDateTime(V.lote_fecha_vcto).ToString("dd/MM/yyyy") : "");
                });

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<fa_notaCreDeb_det_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, string CodDocumento)
        {
            try
            {
                List<fa_notaCreDeb_det_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    if (CodDocumento == "FACT")
                    {
                        Lista = (from q in Context.vwfa_factura_det
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 && q.IdBodega == IdBodega
                                 && q.IdCbteVta == IdCbteVta
                                 select new fa_notaCreDeb_det_Info
                                 {
                                     IdProducto = q.IdProducto,
                                     sc_cantidad = q.vt_cantidad,
                                     sc_descUni = q.vt_DescUnitario,
                                     sc_precioFinal = q.vt_PrecioFinal,
                                     sc_Precio = q.vt_Precio,
                                     sc_subtotal = q.vt_Subtotal,
                                     sc_observacion = q.vt_detallexItems,
                                     sc_iva = q.vt_iva,
                                     sc_PordescUni = q.vt_PorDescUnitario,
                                     vt_por_iva = q.vt_por_iva,
                                     sc_total = q.vt_total,
                                     IdCentroCosto = q.IdCentroCosto,
                                     IdCod_Impuesto_Iva = q.IdCod_Impuesto_Iva,
                                     IdPunto_Cargo = q.IdPunto_Cargo,
                                     IdPunto_cargo_grupo = q.IdPunto_cargo_grupo,
                                     Secuencia = q.Secuencia,
                                     pr_descripcion = q.pr_descripcion,
                                     nom_presentacion = q.nom_presentacion,
                                     lote_num_lote = q.lote_num_lote,
                                     lote_fecha_vcto = q.lote_fecha_vcto,
                                     sc_cantidad_factura=q.vt_cantidad
                                 }).ToList();
                    }else
                        Lista = (from q in Context.vwfa_notaCreDeb_det
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 && q.IdBodega == IdBodega
                                 && q.IdNota == IdCbteVta
                                 select new fa_notaCreDeb_det_Info
                                 {
                                     IdProducto = q.IdProducto,
                                     sc_cantidad = q.sc_cantidad,
                                     sc_descUni = q.sc_descUni,
                                     sc_precioFinal = q.sc_precioFinal,
                                     sc_Precio = q.sc_Precio,
                                     sc_subtotal = q.sc_subtotal,
                                     sc_observacion = q.sc_observacion,
                                     sc_iva = q.sc_iva,
                                     sc_PordescUni = q.sc_PordescUni,
                                     vt_por_iva = q.vt_por_iva,
                                     sc_total = q.sc_total,
                                     IdCentroCosto = q.IdCentroCosto,
                                     IdCod_Impuesto_Iva = q.IdCod_Impuesto_Iva,
                                     IdPunto_Cargo = q.IdPunto_Cargo,
                                     IdPunto_cargo_grupo = q.IdPunto_cargo_grupo,
                                     Secuencia = q.Secuencia,
                                     pr_descripcion = q.pr_descripcion,
                                     nom_presentacion = q.nom_presentacion,
                                     lote_num_lote = q.lote_num_lote,
                                     lote_fecha_vcto = q.lote_fecha_vcto,
                                     sc_cantidad_factura=q.sc_cantidad_factura
                                 }).ToList();

                }
                Lista.ForEach(V =>
                {
                    V.pr_descripcion = V.pr_descripcion + " " + V.nom_presentacion + " - " + V.lote_num_lote + " - " + (V.lote_fecha_vcto != null ? Convert.ToDateTime(V.lote_fecha_vcto).ToString("dd/MM/yyyy") : "");
                });
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
