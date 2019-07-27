using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Data.General;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Bus.General;

namespace Core.Erp.Bus.Facturacion
{
   public class fa_guia_remision_Bus
    {
        fa_guia_remision_Data odata = new fa_guia_remision_Data();
        fa_guia_remision_det_Data odata_det = new fa_guia_remision_det_Data();
        fa_guia_remision_det_x_factura_Data odata_guia_x_fac = new fa_guia_remision_det_x_factura_Data();
        tb_sis_Documento_Tipo_Talonario_Data data_talonario = new tb_sis_Documento_Tipo_Talonario_Data();
        tb_sis_Documento_Tipo_Talonario_Info info_talonario = new tb_sis_Documento_Tipo_Talonario_Info();
        fa_factura_x_fa_guia_remision_Data odata_fac_x_guia = new fa_factura_x_fa_guia_remision_Data();

        public List<fa_guia_remision_Info> get_list(int IdEmpresa, DateTime fecha_inicio, DateTime fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa,fecha_inicio, fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }
      
        public fa_guia_remision_Info get_info(int IdEmpresa, decimal IdGuiaRemision)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdGuiaRemision);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_guia_remision_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "fa_guia_remision_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        }
        public bool modificarDB(fa_guia_remision_Info info)
        {
            try
            {
                odata_guia_x_fac.eliminar(info.IdEmpresa,info.IdGuiaRemision);
                odata_det.eliminar(info.IdEmpresa, info.IdGuiaRemision);
                odata_fac_x_guia.eliminar(info.IdEmpresa, info.IdGuiaRemision);
                return odata.modificarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "fa_guia_remision_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        }
        public bool anularDB(fa_guia_remision_Info info)
        {
            try
            {
                info_talonario.IdEmpresa = info.IdEmpresa;
                info_talonario.IdSucursal = info.IdSucursal;
                info_talonario.CodDocumentoTipo = cl_enumeradores.eTipoDocumento.GUIA.ToString();
                info_talonario.Establecimiento = info.Serie1;
                info_talonario.PuntoEmision = info.Serie2;
                info_talonario.NumDocumento = info.NumGuia_Preimpresa;
                info_talonario.Usado = false;
                if (odata.anularDB(info))
                {
                    odata_guia_x_fac.eliminar(info.IdEmpresa, info.IdGuiaRemision);
                    odata_fac_x_guia.eliminar(info.IdEmpresa, info.IdGuiaRemision);
                    data_talonario.modificar_estado_usadoDB(info_talonario);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string validar(fa_guia_remision_Info info)
        {
            try
            {
                string mensaje = "";
                if (info.IdCliente == 0)
                    mensaje = "Seleccione cliente";
                if (info.lst_detalle == null)
                    mensaje = "No existe detalle para la guia";
                else
                    if(info.lst_detalle.Count()==0)
                    mensaje = "No existe detalle para la guia";

                
                /*
               var resultado = data_talonario.GetUltimoNoUsado(info.IdEmpresa, info.Serie1, info.Serie2, "GUIA");
                if (resultado == null)
                    mensaje = "La numeración "+info.Serie1+"-"+info.Serie2 + "-" + info.NumGuia_Preimpresa+" no esta creado en talonario";
                else
                {
                    if(resultado.NumDocumento==null)
                        mensaje = "La numeración " + info.Serie1 + "-" + info.Serie2 + "-" + info.NumGuia_Preimpresa + " no esta creado en talonario";
                }
              if(  odata.si_existe(info.IdEmpresa, info.Serie1, info.Serie2, info.NumGuia_Preimpresa))
                    mensaje = "La numeración " + info.Serie1 + "-" + info.Serie2 + "-" + info.NumGuia_Preimpresa + " ya fue usada";
*/
                return mensaje;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
