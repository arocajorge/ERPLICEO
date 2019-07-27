using Core.Erp.Data.Banco;
using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Banco
{
    public class ba_TipoFlujo_Plantilla_Bus
    {
        ba_TipoFlujo_Plantilla_Data odata = new ba_TipoFlujo_Plantilla_Data();
        ba_TipoFlujo_PlantillaDet_Data odata_det = new ba_TipoFlujo_PlantillaDet_Data();

        public List<ba_TipoFlujo_Plantilla_Info> GetList(int IdEmpresa, bool MostrarAnulado)
        {
            try
            {
                return odata.get_list(IdEmpresa, MostrarAnulado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ba_TipoFlujo_Plantilla_Info GetInfo(int IdEmpresa, decimal IdPlantilla)
        {
            try
            {
                ba_TipoFlujo_Plantilla_Info info = new ba_TipoFlujo_Plantilla_Info();
                info = odata.get_info(IdEmpresa, IdPlantilla);

                if (info == null)
                    info = new ba_TipoFlujo_Plantilla_Info();
                info.Lista_TipoFlujo_PlantillaDet = odata_det.get_list(IdEmpresa, IdPlantilla);
                if (info.Lista_TipoFlujo_PlantillaDet == null)
                {
                    info.Lista_TipoFlujo_PlantillaDet = new List<ba_TipoFlujo_PlantillaDet_Info>();
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarBD(ba_TipoFlujo_Plantilla_Info info)
        {
            try
            {
                return odata.GuardarBD(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarBD(ba_TipoFlujo_Plantilla_Info info)
        {
            try
            {
                return odata.ModificarBD(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularBD(ba_TipoFlujo_Plantilla_Info info)
        {
            try
            {
                return odata.AnularBD(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
