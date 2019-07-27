using Core.Erp.Data.Contabilidad;
using Core.Erp.Info;
using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Contabilidad
{
    public class ct_cbtecble_Plantilla_Bus
    {
        ct_cbtecble_Plantilla_Data odata = new ct_cbtecble_Plantilla_Data();
        ct_cbtecble_Plantilla_det_Data odata_det = new ct_cbtecble_Plantilla_det_Data();

        public List<ct_cbtecble_Plantilla_Info> GetList(int IdEmpresa, bool MostrarAnulado)
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

        public ct_cbtecble_Plantilla_Info GetInfo(int IdEmpresa, decimal IdPlantilla)
        {
            try
            {
                ct_cbtecble_Plantilla_Info info = new ct_cbtecble_Plantilla_Info();
                info = odata.get_info(IdEmpresa, IdPlantilla);

                if (info == null)
                    info = new ct_cbtecble_Plantilla_Info();
                info.lst_cbtecble_plantilla_det = odata_det.get_list(IdEmpresa, IdPlantilla);
                if (info.lst_cbtecble_plantilla_det == null)
                {
                    info.lst_cbtecble_plantilla_det = new List<ct_cbtecble_Plantilla_det_Info>();
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarBD(ct_cbtecble_Plantilla_Info info)
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

        public bool ModificarBD(ct_cbtecble_Plantilla_Info info)
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

        public bool AnularBD(ct_cbtecble_Plantilla_Info info)
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
