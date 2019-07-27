using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.General;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.RRHH;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class ArchivosActualizacionSueldoController : Controller
    {
        #region Variables
        List<ro_nomina_tipo_Info> lista_nomina = new List<ro_nomina_tipo_Info>();
        ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
        ro_Nomina_Tipoliquiliqui_Bus bus_nomina_tipo = new ro_Nomina_Tipoliquiliqui_Bus();
        List<ro_Nomina_Tipoliqui_Info> lst_nomina_tipo = new List<ro_Nomina_Tipoliqui_Info>();
        ro_periodo_x_ro_Nomina_TipoLiqui_Bus bus_periodos_x_nomina = new ro_periodo_x_ro_Nomina_TipoLiqui_Bus();
        List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> lst_periodos = new List<ro_periodo_x_ro_Nomina_TipoLiqui_Info>();
        ro_ArchivosIess_Info_lst ro_ArchivosIess_Info_lst = new ro_ArchivosIess_Info_lst();
        ro_ArchivosIess_Bus bus_archivos = new ro_ArchivosIess_Bus();
        #endregion

        public ActionResult GridViewPartial_archivos_iess()
        {
            try
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                List<ro_ArchivosIess_Info> model = ro_ArchivosIess_Info_lst.get_list();
                return PartialView("_GridViewPartial_archivos_iess", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Nuevo()
        {
            ro_ArchivosIess_Info model = new ro_ArchivosIess_Info();
            model.IdNominaTipo = 1;
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdPeriodo = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0'));
            cargar_combos(0, 0);
            return View(model);
        }
        [HttpPost]
        public FileResult Nuevo(ro_ArchivosIess_Info model)
        {
            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var empresa = bus_empresa.get_info(Convert.ToInt32(SessionFixed.IdEmpresa));
            string archivo = "";
            model.lst_detalle = ro_ArchivosIess_Info_lst.get_list();
            foreach (var item in model.lst_detalle)
            {
                archivo += empresa.em_ruc + ";";
                archivo += "0001" + ";";
                archivo += DateTime.Now.Year.ToString() + ";";
                archivo += DateTime.Now.Month.ToString().PadLeft(2, '0') + ";";
                archivo += "INS" + ";";
                archivo += item.pe_cedulaRuc + ";";
                archivo += item.Valor + ";";
                archivo += "X";

                archivo += "\n";
            }

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(archivo);
            return File(byteArray, "application/xml", "HORAS_EXTRAS_"+ DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() +  ".txt");

        }

        private void cargar_combos(int IdNomina_Tipo, int IdNomina_Tipo_Liqui)
        {
            try
            {
                int IdEmpresa = Convert.ToInt16(SessionFixed.IdEmpresa);
                lista_nomina = bus_nomina.get_list(IdEmpresa, false);
                lst_nomina_tipo = bus_nomina_tipo.get_list(IdEmpresa, IdNomina_Tipo);
                lst_periodos = bus_periodos_x_nomina.get_list(IdEmpresa, IdNomina_Tipo, IdNomina_Tipo_Liqui);
                ViewBag.lst_nomina = lista_nomina;
                ViewBag.lst_nomina_tipo = lst_nomina_tipo;
                ViewBag.lst_periodos = lst_periodos;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult get_list(int IdEmpresa=0, int IdNomina_Tipo=0,int IdNomina_TipoLiqui=0, int IdPeriodo=0)
        {
            try
            {

                var lst_detalle = bus_archivos.get_lis(IdEmpresa, IdNomina_Tipo, IdNomina_TipoLiqui, IdPeriodo);
                ro_ArchivosIess_Info_lst.set_list(lst_detalle);

                return Json("", JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }
        }


    }

         public class ro_ArchivosIess_Info_lst
        {
        string variable = "ro_ArchivosIess_Info";
        public List<ro_ArchivosIess_Info> get_list()
        {
            if (HttpContext.Current.Session[variable] == null)
            {
                List<ro_ArchivosIess_Info> list = new List<ro_ArchivosIess_Info>();

                HttpContext.Current.Session[variable] = list;
            }
            return (List<ro_ArchivosIess_Info>)HttpContext.Current.Session[variable];
        }

        public void set_list(List<ro_ArchivosIess_Info> list)
        {
            HttpContext.Current.Session[variable] = list;
        }
    }
}