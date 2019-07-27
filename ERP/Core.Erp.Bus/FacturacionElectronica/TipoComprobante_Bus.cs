using Core.Erp.Data.FacturacionElectronica;
using Core.Erp.Info.FacturacionElectronica;
using Core.Erp.Info.FacturacionElectronica.Factura_V2;
using Core.Erp.Info.FacturacionElectronica.GuiaRemision;
using Core.Erp.Info.FacturacionElectronica.NotaCredito;
using Core.Erp.Info.FacturacionElectronica.NotaDebito;
using Core.Erp.Info.FacturacionElectronica.Retencion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.FacturacionElectronica
{
   public class TipoComprobante_Bus
    {

        TipoComprobante_Data odata = new TipoComprobante_Data();
        public factura get_info_factura(TipoComprobante_Info info)
        {
            try
            {
                return odata.get_info_factura(info);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public comprobanteRetencion get_info_retencion(TipoComprobante_Info info)
        {
            try
            {
                return odata.get_info_retencion(info);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public guiaRemision get_info_guia(TipoComprobante_Info info)
        {
            try
            {
                return odata.get_info_guia(info);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public notaCredito get_info_nota_credito(TipoComprobante_Info info)
        {
            try
            {
                return odata.get_info_nota_credito(info);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public notaDebito get_info_nota_debito(TipoComprobante_Info info)
        {
            try
            {
                return odata.get_info_nota_debito(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public factura get_facturas_eventos(TipoComprobante_Info info)
        {
            try
            {
                return odata.get_facturas_eventos(info);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public TipoComprobante_Info get_info_comprobante_a_generar()
        {
            try
            {
                return odata.get_info_comprobante_a_generar();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
