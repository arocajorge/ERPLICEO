using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
    public class fa_parametro_Data
    {
        public fa_parametro_Info get_info(int IdEmpresa)
        {
            try
            {
                fa_parametro_Info info = new fa_parametro_Info();
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_parametro Entity = Context.fa_parametro.FirstOrDefault(q => q.IdEmpresa == IdEmpresa);
                    if (Entity != null)
                        info = new fa_parametro_Info
                        {
                            IdEmpresa = Entity.IdEmpresa,
                            IdMovi_inven_tipo_Factura = Entity.IdMovi_inven_tipo_Factura,
                            IdTipoCbteCble_Factura = Entity.IdTipoCbteCble_Factura,
                            IdTipoCbteCble_NC = Entity.IdTipoCbteCble_NC,
                            IdTipoCbteCble_ND = Entity.IdTipoCbteCble_ND,
                            IdCtaCble_SubTotal_Vtas_x_Default = Entity.IdCtaCble_SubTotal_Vtas_x_Default,
                            NumeroDeItemFact = Entity.NumeroDeItemFact,
                            NumeroDeItemProforma = Entity.NumeroDeItemProforma,
                            IdCaja_Default_Factura = Entity.IdCaja_Default_Factura,
                            IdCtaCble_CXC_Vtas_x_Default = Entity.IdCtaCble_CXC_Vtas_x_Default,
                            IdCtaCble_IVA = Entity.IdCtaCble_IVA,
                            pa_IdCtaCble_descuento = Entity.pa_IdCtaCble_descuento,
                            pa_Contabiliza_descuento = Entity.pa_Contabiliza_descuento,
                            clave_desbloqueo_precios = Entity.clave_desbloqueo_precios,
                            DiasTransaccionesAFuturo = Entity.DiasTransaccionesAFuturo,
                            IdClienteConsumidorFinal = Entity.IdClienteConsumidorFinal,
                            MontoMaximoConsumidorFinal = Entity.MontoMaximoConsumidorFinal
                        };
                    else
                        info = null;
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_parametro_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_parametro Entity = Context.fa_parametro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa);
                    if (Entity == null)
                    {
                        Entity = new fa_parametro
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdMovi_inven_tipo_Factura = info.IdMovi_inven_tipo_Factura,
                            IdTipoCbteCble_Factura = info.IdTipoCbteCble_Factura,
                            IdTipoCbteCble_NC = info.IdTipoCbteCble_NC,
                            IdTipoCbteCble_ND = info.IdTipoCbteCble_ND,
                            IdCtaCble_SubTotal_Vtas_x_Default = info.IdCtaCble_SubTotal_Vtas_x_Default,
                            NumeroDeItemFact = info.NumeroDeItemFact,
                            NumeroDeItemProforma = info.NumeroDeItemProforma,
                            IdCaja_Default_Factura = info.IdCaja_Default_Factura,
                            IdCtaCble_CXC_Vtas_x_Default = info.IdCtaCble_CXC_Vtas_x_Default,
                            IdCtaCble_IVA = info.IdCtaCble_IVA,
                            pa_IdCtaCble_descuento = info.pa_IdCtaCble_descuento,
                            pa_Contabiliza_descuento = info.pa_Contabiliza_descuento,
                            clave_desbloqueo_precios = info.clave_desbloqueo_precios,
                            DiasTransaccionesAFuturo = info.DiasTransaccionesAFuturo,
                            IdClienteConsumidorFinal = info.IdClienteConsumidorFinal,
                            MontoMaximoConsumidorFinal = info.MontoMaximoConsumidorFinal
                        };
                        Context.fa_parametro.Add(Entity);
                    }
                    else
                    {

                        Entity.IdMovi_inven_tipo_Factura = info.IdMovi_inven_tipo_Factura;
                        Entity.IdTipoCbteCble_Factura = info.IdTipoCbteCble_Factura;
                        Entity.IdTipoCbteCble_NC = info.IdTipoCbteCble_NC;
                        Entity.IdTipoCbteCble_ND = info.IdTipoCbteCble_ND;
                        Entity.IdCtaCble_SubTotal_Vtas_x_Default = info.IdCtaCble_SubTotal_Vtas_x_Default;
                        Entity.NumeroDeItemFact = info.NumeroDeItemFact;
                        Entity.NumeroDeItemProforma = info.NumeroDeItemProforma;
                        Entity.IdCaja_Default_Factura = info.IdCaja_Default_Factura;
                        Entity.IdCtaCble_CXC_Vtas_x_Default = info.IdCtaCble_CXC_Vtas_x_Default;
                        Entity.IdCtaCble_IVA = info.IdCtaCble_IVA;
                        Entity.pa_IdCtaCble_descuento = info.pa_IdCtaCble_descuento;
                        Entity.pa_Contabiliza_descuento = info.pa_Contabiliza_descuento;
                        Entity.clave_desbloqueo_precios = info.clave_desbloqueo_precios;
                        Entity.DiasTransaccionesAFuturo = info.DiasTransaccionesAFuturo;
                        Entity.IdClienteConsumidorFinal = info.IdClienteConsumidorFinal;
                        Entity.MontoMaximoConsumidorFinal = info.MontoMaximoConsumidorFinal;
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
