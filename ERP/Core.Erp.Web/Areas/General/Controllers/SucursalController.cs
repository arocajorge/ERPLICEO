using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.General;
using Core.Erp.Bus.General;
using Core.Erp.Web.Helps;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Contabilidad;
using DevExpress.Web;
using Core.Erp.Bus.Facturacion;

namespace Core.Erp.Web.Areas.General.Controllers
{
    [SessionTimeout]
    public class SucursalController : Controller
    {
        #region Index / Metodo

        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        fa_catalogo_Bus bus_forma_pago = new fa_catalogo_Bus();
        fa_NivelDescuento_Bus bus_nivel_descuento = new fa_NivelDescuento_Bus();
        tb_sucursal_FormaPago_x_fa_NivelDescuento_Bus bus_formapago_x_niveldescuento = new tb_sucursal_FormaPago_x_fa_NivelDescuento_Bus();
        tb_sucursal_FormaPago_x_fa_NivelDescuento_List FormaPago_x_NivelDescuento_List = new tb_sucursal_FormaPago_x_fa_NivelDescuento_List();
        List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info> Lista_FormaPago_x_NivelDescuento = new List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info>();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_sucursal()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_sucursal.get_list(IdEmpresa,true);
            return PartialView("_GridViewPartial_sucursal", model);
        }        
        #endregion

        #region Detalle
        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_forma_pago = bus_forma_pago.get_list(15, false);
            ViewBag.lst_forma_pago = lst_forma_pago;

            var lst_nivel_descuento = bus_nivel_descuento.GetList(IdEmpresa, false);
            ViewBag.lst_nivel_descuento = lst_nivel_descuento;
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_Sucursal_x_NivelDescuento()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            int IdTransaccionSession = Convert.ToInt32(SessionFixed.IdTransaccionSession);
            cargar_combos_detalle();

            var model = FormaPago_x_NivelDescuento_List.get_list(IdTransaccionSession);
            return PartialView("_GridViewPartial_Sucursal_x_NivelDescuento", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] tb_sucursal_FormaPago_x_fa_NivelDescuento_Info info_det)
        {
            List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info> ListaDetalle = new List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info>();
            ListaDetalle = FormaPago_x_NivelDescuento_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            var existe = ListaDetalle.Where(q=> q.IdCatalogo== info_det.IdCatalogo).FirstOrDefault();
            if(existe == null)
                FormaPago_x_NivelDescuento_List.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info> model = new List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info>();
            model = FormaPago_x_NivelDescuento_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_Sucursal_x_NivelDescuento", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] tb_sucursal_FormaPago_x_fa_NivelDescuento_Info info_det)
        {
            tb_sucursal_FormaPago_x_fa_NivelDescuento_Info existe = new tb_sucursal_FormaPago_x_fa_NivelDescuento_Info();
            //if (ModelState.IsValid)
            List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info> ListaDetalle = new List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info>();
            ListaDetalle = FormaPago_x_NivelDescuento_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            if (ListaDetalle.Count > 1)
            {
                existe = ListaDetalle.Where(q => q.IdCatalogo == info_det.IdCatalogo).FirstOrDefault();
            }
            else
            {
                existe = null;
            }

            if (existe == null)
                FormaPago_x_NivelDescuento_List.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info> model = new List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info>();
            model = FormaPago_x_NivelDescuento_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_Sucursal_x_NivelDescuento", model);
        }
        public ActionResult EditingDelete(int Secuencia)
        {
            FormaPago_x_NivelDescuento_List.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info> model = new List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info>();
            model = FormaPago_x_NivelDescuento_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_Sucursal_x_NivelDescuento", model);
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0 )
        {
            tb_sucursal_Info model = new tb_sucursal_Info
            {
                IdEmpresa = IdEmpresa
            };

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.ListaNivelDescuento = new List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info>();
            FormaPago_x_NivelDescuento_List.set_list(model.ListaNivelDescuento, model.IdTransaccionSession);

            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(tb_sucursal_Info model)
        {
            model.ListaNivelDescuento = FormaPago_x_NivelDescuento_List.get_list(model.IdTransaccionSession);

            if (!bus_sucursal.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0 , int IdSucursal = 0)
        {
            tb_sucursal_Info model = bus_sucursal.get_info(IdEmpresa, IdSucursal);
            if(model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.ListaNivelDescuento = bus_formapago_x_niveldescuento.GetList(IdEmpresa, model.IdSucursal);
            FormaPago_x_NivelDescuento_List.set_list(model.ListaNivelDescuento, model.IdTransaccionSession);

            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(tb_sucursal_Info model)
        {
            model.ListaNivelDescuento = FormaPago_x_NivelDescuento_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            if (!bus_sucursal.modificarDB(model))
            {
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0 , int IdSucursal = 0)
        {
            tb_sucursal_Info model = bus_sucursal.get_info(IdEmpresa, IdSucursal);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.ListaNivelDescuento = bus_formapago_x_niveldescuento.GetList(IdEmpresa, model.IdSucursal);
            FormaPago_x_NivelDescuento_List.set_list(model.ListaNivelDescuento, model.IdTransaccionSession);

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(tb_sucursal_Info model)
        {
            model.ListaNivelDescuento = FormaPago_x_NivelDescuento_List.get_list(model.IdTransaccionSession);

            if (!bus_sucursal.anularDB(model))
            {
                return View(model);
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Metodos ComboBox bajo demanda
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        
        #region CmbCuenta_Sucursal
        public ActionResult CmbCuenta_Sucursal()
        {
            tb_sucursal_Info model = new tb_sucursal_Info();
            return PartialView("_CmbCuenta_Sucursal", model);
        }

        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region CmbCuenta_Sucursal_IVA
        public ActionResult CmbCuenta_Sucursal_IVA()
        {
            tb_sucursal_Info model = new tb_sucursal_Info();
            return PartialView("_CmbCuenta_Sucursal_IVA", model);
        }

        public List<ct_plancta_Info> get_list_bajo_demanda_ctacble_iva(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda_ctacble_iva(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region CmbCuenta_Sucursal_IVA0
        public ActionResult CmbCuenta_Sucursal_IVA0()
        {
            tb_sucursal_Info model = new tb_sucursal_Info();
            return PartialView("_CmbCuenta_Sucursal_IVA0", model);
        }

        public List<ct_plancta_Info> get_list_bajo_demanda_ctacble_iva0(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda_ctacble_iva0(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #endregion

    }
    public class tb_sucursal_List
    {
        string Variable = "tb_sucursal_Info";
        public List<tb_sucursal_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<tb_sucursal_Info> list = new List<tb_sucursal_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<tb_sucursal_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<tb_sucursal_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class tb_sucursal_FormaPago_x_fa_NivelDescuento_List
    {
        string Variable = "tb_sucursal_FormaPago_x_fa_NivelDescuento_Info";
        public List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info> list = new List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(tb_sucursal_FormaPago_x_fa_NivelDescuento_Info info_det, decimal IdTransaccionSession)
        {
            List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det.IdNivel = info_det.IdNivel;
            info_det.IdCatalogo = info_det.IdCatalogo;

            list.Add(info_det);
        }

        public void UpdateRow(tb_sucursal_FormaPago_x_fa_NivelDescuento_Info info_det, decimal IdTransaccionSession)
        {
            tb_sucursal_FormaPago_x_fa_NivelDescuento_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdNivel = info_det.IdNivel;
            edited_info.IdCatalogo = info_det.IdCatalogo;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }
}