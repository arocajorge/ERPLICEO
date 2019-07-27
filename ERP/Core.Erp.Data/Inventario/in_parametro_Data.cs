using Core.Erp.Info.Inventario;
using System;
using System.Linq;

namespace Core.Erp.Data.Inventario
{
    public class in_parametro_Data
    {
        public in_parametro_Info get_info(int IdEmpresa)
        {
            try
            {
                in_parametro_Info info = new in_parametro_Info();

                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_parametro Entity = Context.in_parametro.FirstOrDefault(q => q.IdEmpresa == IdEmpresa);
                    if (Entity == null) return null;
                    info = new in_parametro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMovi_inven_tipo_egresoBodegaOrigen = Entity.IdMovi_inven_tipo_egresoBodegaOrigen,
                        IdMovi_inven_tipo_ingresoBodegaDestino = Entity.IdMovi_inven_tipo_ingresoBodegaDestino,
                        IdMovi_Inven_tipo_x_Dev_Inv_x_Ing = Entity.IdMovi_Inven_tipo_x_Dev_Inv_x_Ing,
                        IdMovi_Inven_tipo_x_Dev_Inv_x_Erg = Entity.IdMovi_Inven_tipo_x_Dev_Inv_x_Erg,
                        P_IdCtaCble_transitoria_transf_inven = Entity.P_IdCtaCble_transitoria_transf_inven,
                        P_IdMovi_inven_tipo_default_ing = Entity.P_IdMovi_inven_tipo_default_ing,
                        P_IdMovi_inven_tipo_default_egr = Entity.P_IdMovi_inven_tipo_default_egr,
                        P_IdMovi_inven_tipo_ingreso_x_compra = Entity.P_IdMovi_inven_tipo_ingreso_x_compra,
                        P_Dias_menores_alerta_desde_fecha_actual_rojo = Entity.P_Dias_menores_alerta_desde_fecha_actual_rojo,
                        P_Dias_menores_alerta_desde_fecha_actual_amarillo = Entity.P_Dias_menores_alerta_desde_fecha_actual_amarillo,
                        DiasTransaccionesAFuturo = Entity.DiasTransaccionesAFuturo,
                        IdMovi_inven_tipo_Cambio = Entity.IdMovi_inven_tipo_Cambio,
                        IdMovi_inven_tipo_Consignacion = Entity.IdMovi_inven_tipo_Consignacion,
                        IdMovi_inven_tipo_elaboracion_egr = Entity.IdMovi_inven_tipo_elaboracion_egr,
                        IdMovi_inven_tipo_elaboracion_ing = Entity.IdMovi_inven_tipo_elaboracion_ing,
                        IdMotivo_Inv_elaboracion_egr = Entity.IdMotivo_Inv_elaboracion_egr,
                        IdMotivo_Inv_elaboracion_ing = Entity.IdMotivo_Inv_elaboracion_ing,
                        IdMotivo_Inv_ajuste_egr = Entity.IdMotivo_Inv_ajuste_egr,
                        IdMotivo_Inv_ajuste_ing = Entity.IdMotivo_Inv_ajuste_ing,
                        IdMovi_inven_tipo_ajuste_egr = Entity.IdMovi_inven_tipo_ajuste_egr,
                        IdMovi_inven_tipo_ajuste_ing = Entity.IdMovi_inven_tipo_ajuste_ing,
                        IdCatalogoEstadoAjuste = Entity.IdCatalogoEstadoAjuste
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(in_parametro_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_parametro Entity = Context.in_parametro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa);
                    if (Entity == null)
                    {
                        Entity = new in_parametro
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdMovi_inven_tipo_egresoBodegaOrigen = info.IdMovi_inven_tipo_egresoBodegaOrigen,
                            IdMovi_inven_tipo_ingresoBodegaDestino = info.IdMovi_inven_tipo_ingresoBodegaDestino,                            
                            IdMovi_Inven_tipo_x_Dev_Inv_x_Ing = info.IdMovi_Inven_tipo_x_Dev_Inv_x_Ing,
                            IdMovi_Inven_tipo_x_Dev_Inv_x_Erg = info.IdMovi_Inven_tipo_x_Dev_Inv_x_Erg,
                            P_IdCtaCble_transitoria_transf_inven = info.P_IdCtaCble_transitoria_transf_inven,
                            P_IdMovi_inven_tipo_default_ing = info.P_IdMovi_inven_tipo_default_ing,
                            P_IdMovi_inven_tipo_default_egr = info.P_IdMovi_inven_tipo_default_egr,
                            P_IdMovi_inven_tipo_ingreso_x_compra = info.P_IdMovi_inven_tipo_ingreso_x_compra,
                            P_Dias_menores_alerta_desde_fecha_actual_rojo = info.P_Dias_menores_alerta_desde_fecha_actual_rojo,
                            P_Dias_menores_alerta_desde_fecha_actual_amarillo = info.P_Dias_menores_alerta_desde_fecha_actual_amarillo,
                            DiasTransaccionesAFuturo = info.DiasTransaccionesAFuturo,
                            IdMovi_inven_tipo_Cambio = info.IdMovi_inven_tipo_Cambio,
                            IdMovi_inven_tipo_Consignacion = info.IdMovi_inven_tipo_Consignacion,
                            IdMovi_inven_tipo_elaboracion_egr = info.IdMovi_inven_tipo_elaboracion_egr,
                            IdMovi_inven_tipo_elaboracion_ing = info.IdMovi_inven_tipo_elaboracion_ing,
                            IdMotivo_Inv_elaboracion_egr = info.IdMotivo_Inv_elaboracion_egr,
                            IdMotivo_Inv_elaboracion_ing = info.IdMotivo_Inv_elaboracion_ing,
                            IdMotivo_Inv_ajuste_egr = Entity.IdMotivo_Inv_ajuste_egr,
                            IdMotivo_Inv_ajuste_ing = Entity.IdMotivo_Inv_ajuste_ing,
                            IdMovi_inven_tipo_ajuste_egr = Entity.IdMovi_inven_tipo_ajuste_egr,
                            IdMovi_inven_tipo_ajuste_ing = Entity.IdMovi_inven_tipo_ajuste_ing,
                            IdCatalogoEstadoAjuste = Entity.IdCatalogoEstadoAjuste
                        };
                        Context.in_parametro.Add(Entity);
                    }
                    else
                    {
                        Entity.IdMovi_inven_tipo_egresoBodegaOrigen = info.IdMovi_inven_tipo_egresoBodegaOrigen;
                        Entity.IdMovi_inven_tipo_ingresoBodegaDestino = info.IdMovi_inven_tipo_ingresoBodegaDestino;
                        Entity.IdMovi_Inven_tipo_x_Dev_Inv_x_Ing = info.IdMovi_Inven_tipo_x_Dev_Inv_x_Ing;
                        Entity.IdMovi_Inven_tipo_x_Dev_Inv_x_Erg = info.IdMovi_Inven_tipo_x_Dev_Inv_x_Erg;
                        Entity.P_IdCtaCble_transitoria_transf_inven = info.P_IdCtaCble_transitoria_transf_inven;
                        Entity.P_IdMovi_inven_tipo_default_ing = info.P_IdMovi_inven_tipo_default_ing;
                        Entity.P_IdMovi_inven_tipo_default_egr = info.P_IdMovi_inven_tipo_default_egr;
                        Entity.P_IdMovi_inven_tipo_ingreso_x_compra = info.P_IdMovi_inven_tipo_ingreso_x_compra;
                        Entity.P_Dias_menores_alerta_desde_fecha_actual_rojo = info.P_Dias_menores_alerta_desde_fecha_actual_rojo;
                        Entity.P_Dias_menores_alerta_desde_fecha_actual_amarillo = info.P_Dias_menores_alerta_desde_fecha_actual_amarillo;
                        Entity.DiasTransaccionesAFuturo = info.DiasTransaccionesAFuturo;
                        Entity.IdMovi_inven_tipo_Cambio = info.IdMovi_inven_tipo_Cambio;
                        Entity.IdMovi_inven_tipo_Consignacion = info.IdMovi_inven_tipo_Consignacion;
                        Entity.IdMovi_inven_tipo_elaboracion_egr = info.IdMovi_inven_tipo_elaboracion_egr;
                        Entity.IdMovi_inven_tipo_elaboracion_ing = info.IdMovi_inven_tipo_elaboracion_ing;
                        Entity.IdMotivo_Inv_elaboracion_egr = info.IdMotivo_Inv_elaboracion_egr;
                        Entity.IdMotivo_Inv_elaboracion_ing = info.IdMotivo_Inv_elaboracion_ing;
                        Entity.IdMotivo_Inv_ajuste_egr = info.IdMotivo_Inv_ajuste_egr;
                        Entity.IdMotivo_Inv_ajuste_ing = info.IdMotivo_Inv_ajuste_ing;
                        Entity.IdMovi_inven_tipo_ajuste_egr = info.IdMovi_inven_tipo_ajuste_egr;
                        Entity.IdMovi_inven_tipo_ajuste_ing = info.IdMovi_inven_tipo_ajuste_ing;
                        Entity.IdCatalogoEstadoAjuste = info.IdCatalogoEstadoAjuste;
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
