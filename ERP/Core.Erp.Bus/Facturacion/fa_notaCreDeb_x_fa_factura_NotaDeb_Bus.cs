using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Facturacion
{
    public class fa_notaCreDeb_x_fa_factura_NotaDeb_Bus
    {
        fa_notaCreDeb_x_fa_factura_NotaDeb_Data odata = new fa_notaCreDeb_x_fa_factura_NotaDeb_Data();
        public List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info> get_list_cartera(int IdEmpresa, int IdSucursal, decimal IdCliente, bool mostrar_saldo0)
        {
            try
            {
                return odata.get_list_cartera(IdEmpresa, IdSucursal, IdCliente, mostrar_saldo0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
