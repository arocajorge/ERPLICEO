using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Facturacion
{
    public class fa_MotivoTraslado_Bus
    {
        fa_MotivoTraslado_Data odata = new fa_MotivoTraslado_Data();

        public List<fa_MotivoTraslado_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, mostrar_anulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public fa_MotivoTraslado_Info GetInfo(int IdEmpresa, int IdMotivoTraslado)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, IdMotivoTraslado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(fa_MotivoTraslado_Info info)

        {
            try
            {
                return odata.GuardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB(fa_MotivoTraslado_Info info)

        {
            try
            {
                return odata.ModificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB(fa_MotivoTraslado_Info info)

        {
            try
            {
                return odata.AnularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
