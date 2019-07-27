using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.Facturacion;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Facturacion
{
    public class fa_notaCreDeb_Bus
    {
        fa_notaCreDeb_Data odata = new fa_notaCreDeb_Data();
        public List<fa_notaCreDeb_consulta_Info> get_list(int IdEmpresa, int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin, string CreDeb)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, Fecha_ini, Fecha_fin, CreDeb);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DocumentoExiste(int IdEmpresa, string CodDocumentoTipo, string Serie1, string Serie2, string NumNota_Impresa)
        {
            try
            {
                return odata.DocumentoExiste(IdEmpresa, CodDocumentoTipo, Serie1, Serie1, NumNota_Impresa);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public fa_notaCreDeb_Info get_info(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdSucursal, IdBodega, IdNota);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(fa_notaCreDeb_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "fa_notaCreDeb_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(fa_notaCreDeb_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "fa_notaCreDeb_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool anularDB(fa_notaCreDeb_Info info)
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

        public bool modificarEstadoAutorizacion(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            try
            {
                return odata.modificarEstadoAutorizacion(IdEmpresa, IdSucursal, IdBodega, IdNota);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
