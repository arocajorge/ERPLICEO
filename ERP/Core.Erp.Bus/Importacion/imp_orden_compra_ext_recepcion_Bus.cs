using Core.Erp.Data.Importacion;
using Core.Erp.Info.Importacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Inventario;
using Core.Erp.Bus.Inventario;

namespace Core.Erp.Bus.Importacion
{
   public class imp_orden_compra_ext_recepcion_Bus
    {
        imp_orden_compra_ext_recepcion_Data odata = new imp_orden_compra_ext_recepcion_Data();
        imp_ordencompra_ext_Data odata_oc = new imp_ordencompra_ext_Data();
        imp_ordencompra_ext_det_Data odta_det_oc = new imp_ordencompra_ext_det_Data();
        imp_ordencompra_ext_det_Data odata_det = new imp_ordencompra_ext_det_Data();
        public List<imp_orden_compra_ext_recepcion_Info> get_list(int IdEmpresa, DateTime fecha_inicio, DateTime Fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa, fecha_inicio, Fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public imp_orden_compra_ext_recepcion_Info get_info(int IdEmpresa, decimal IdOrdenCompra_ext)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdOrdenCompra_ext);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(imp_orden_compra_ext_recepcion_Info info)
        {
            try
            {
                return odata.guardarDB(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(imp_orden_compra_ext_recepcion_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(imp_orden_compra_ext_recepcion_Info info)
        {
            try
            {
                    return odata.anularDB(info);
               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public imp_orden_compra_ext_recepcion_Info get_rcepcion_mercancia(int IdEmpresa, decimal IdOrdenCompra_ext)
        {
            try
            {
                imp_orden_compra_ext_recepcion_Info info = new imp_orden_compra_ext_recepcion_Info();
                var orden = odata_oc.get_info_recepcion_merca(IdEmpresa, IdOrdenCompra_ext);
                var detalle_oc = odta_det_oc.get_list_recepcion(IdEmpresa, IdOrdenCompra_ext);
                if(orden!=null & detalle_oc!=null)
                {
                    info.pe_cedulaRuc = orden.pe_cedulaRuc;
                    info.pe_nombreCompleto = orden.pe_nombreCompleto;
                    info.or_observacion = orden.oe_observacion;
                    info.IdOrdenCompraExt = orden.IdOrdenCompra_ext;
                    info.oe_fecha = orden.oe_fecha;
                    info.oe_fecha_embarque = orden.oe_fecha_embarque;
                    info.oe_fecha_llegada = orden.oe_fecha_embarque;
                    info.IdCatalogo_via = orden.IdCatalogo_via;
                    info.or_fecha = DateTime.Now;
                    info.lst_detalle = detalle_oc;
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

    
    }
}
