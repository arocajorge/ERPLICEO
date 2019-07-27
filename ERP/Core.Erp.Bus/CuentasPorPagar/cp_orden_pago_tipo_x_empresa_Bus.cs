using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Data.CuentasPorPagar;
namespace Core.Erp.Bus.CuentasPorPagar
{
   public class cp_orden_pago_tipo_x_empresa_Bus
    {
        cp_orden_pago_tipo_x_empresa_Data oData = new cp_orden_pago_tipo_x_empresa_Data();
        cp_orden_pago_tipo_Data odata_tipo = new cp_orden_pago_tipo_Data();
        cp_orden_pago_tipo_Info info_op_tipo = new cp_orden_pago_tipo_Info();
        public List<cp_orden_pago_tipo_x_empresa_Info> get_list(int IdEmpresa)
        {
            try
            {
                return oData.get_list(IdEmpresa);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public cp_orden_pago_tipo_x_empresa_Info get_info(int IdEmpresa, string IdTipo_op)
        {
            try
            {
                return oData.get_info(IdEmpresa, IdTipo_op);
            }
            catch (Exception)
            {

                throw;
            }
        }
     
        public bool guardarDB(cp_orden_pago_tipo_x_empresa_Info info)
        {
            try
            {
                info_op_tipo.IdTipo_op = info.IdTipo_op;
                info_op_tipo.Descripcion = info.Descripcion;
                info_op_tipo.GeneraDiario = info.GeneraDiario;
                info_op_tipo.GeneraDiario = "S";
                if (odata_tipo.guardarDB(info_op_tipo))
                {
                    return oData.guardarDB(info);
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(cp_orden_pago_tipo_x_empresa_Info info)
        {
            try
            {
                info_op_tipo.IdTipo_op = info.IdTipo_op;
                info_op_tipo.Descripcion = info.Descripcion;
                info_op_tipo.GeneraDiario = info.GeneraDiario;
                if (odata_tipo.modificarDB(info_op_tipo))
                {
                    return oData.modificarDB(info);
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool anularDB(cp_orden_pago_tipo_x_empresa_Info info)
        {
            try
            {
                return oData.anularDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool si_existe(cp_orden_pago_tipo_x_empresa_Info info)
        {
            try
            {
                return oData.si_existe(info);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
