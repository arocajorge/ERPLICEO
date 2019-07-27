using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Bus.Migraciones;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.Helps;
using Core.Erp.Info.Migraciones;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Contabilidad.Controllers
{
    public class ImportacionDiariosController : Controller
    {
        #region Variables
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        ct_cbtecble_Bus bus_comprobante = new ct_cbtecble_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        ImportacionDiarios_Bus bus_ImporacionDiarios = new ImportacionDiarios_Bus();
        ImportacionDiarios_List ImportacionDiarios_Lista = new ImportacionDiarios_List();
        List<ImportacionDiarios_Info> ListaImportacionDiarios = new List<ImportacionDiarios_Info>();
        string mensaje = string.Empty;
        #endregion

        #region Index
        public ActionResult Index()
        {
            ImportacionDiarios_Info model = new ImportacionDiarios_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                cb_Fecha = DateTime.Now
            };

            cargar_filtros(model.IdEmpresa);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_ImportacionDiarios(string tipo_documento)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.tipo_documento = tipo_documento == "" || tipo_documento == null ? "" : tipo_documento;
            var IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);

            var model = ImportacionDiarios_Lista.get_list(IdTransaccionSession);
            return PartialView("_GridViewPartial_ImportacionDiarios", model);
        }
        #endregion

        #region Funciones del Grid
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ImportacionDiarios_Info info_det)
        {
            if (ModelState.IsValid)
                ImportacionDiarios_Lista.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ImportacionDiarios_Info model = new ImportacionDiarios_Info();
            model.ListaTipoDocumento = ImportacionDiarios_Lista.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_ImportacionDiarios", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ImportacionDiarios_Info info_det)
        {

            if (ModelState.IsValid)
                ImportacionDiarios_Lista.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            ImportacionDiarios_Info model = new ImportacionDiarios_Info();
            model.ListaTipoDocumento = ImportacionDiarios_Lista.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_ImportacionDiarios", model.ListaTipoDocumento);
        }

        public ActionResult EditingDelete(int Secuencial)
        {
            ImportacionDiarios_Lista.DeleteRow(Secuencial, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ImportacionDiarios_Info model = new ImportacionDiarios_Info();
            model.ListaTipoDocumento = ImportacionDiarios_Lista.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
         
            return PartialView("_GridViewPartial_ImportacionDiarios", model.ListaTipoDocumento);
        }
        #endregion

        #region Metodos
        private void cargar_filtros(int IdEmpresa)
        {
            try
            {
                var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
                ViewBag.lst_sucursal = lst_sucursal;

                ct_cbtecble_tipo_Bus bus_tipo_comprobante = new ct_cbtecble_tipo_Bus();
                var lst_tipo_comprobante = bus_tipo_comprobante.get_list(IdEmpresa, false);
                ViewBag.lst_tipo_comprobante = lst_tipo_comprobante;

                Dictionary<string, string> lst_tipo_documento = new Dictionary<string, string>();
                lst_tipo_documento.Add("FACTURAS", "FACTURAS");
                lst_tipo_documento.Add("COBROS", "COBROS");
                lst_tipo_documento.Add("NOTADEBITO", "NOTA DE DEBITO");
                lst_tipo_documento.Add("NOTACREDITO", "NOTA DE CREDITO");
                ViewBag.lst_tipo_movimiento = lst_tipo_documento;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool validar(ct_cbtecble_Info i_validar, ref string msg)
        {
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.cb_Fecha, cl_enumeradores.eModulo.CONTA, i_validar.IdSucursal, ref mensaje))
            {
                return false;
            }

            if (i_validar.lst_ct_cbtecble_det.Count == 0)
            {
                mensaje = "Debe ingresar registros en el detalle";
                return false;
            }

            if (Math.Round(i_validar.lst_ct_cbtecble_det.Sum(q => q.dc_Valor), 2) != 0)
            {
                mensaje = "La suma de los detalles debe ser 0";
                return false;
            }

            if (i_validar.lst_ct_cbtecble_det.Where(q => q.dc_Valor == 0).Count() > 0)
            {
                mensaje = "Existen detalles con valor 0 en el debe o haber";
                return false;
            }

            if (i_validar.lst_ct_cbtecble_det.Where(q => string.IsNullOrEmpty(q.IdCtaCble)).Count() > 0)
            {
                mensaje = "Existen detalles sin cuenta contable";
                return false;
            }

            return true;
        }
        #endregion

        #region Metodos ComboBox bajo demanda
        public ActionResult CmbCuenta()
        {
            ImportacionDiarios_Info model = new ImportacionDiarios_Info();
            return PartialView("_CmbCuenta", model);
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

        #region json
        public JsonResult buscar_diarios(string tipo_documento)
        {
            try
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                var IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
                var model = bus_ImporacionDiarios.get_list(tipo_documento);

                foreach (var item in model)
                {
                    var info_cta = bus_plancta.get_info(IdEmpresa, item.IdCtaCble);
                    if (info_cta == null)
                    {
                        item.IdCtaCble = "";
                        item.pc_Cuenta = "";
                        item.dc_Valor = item.dc_ValorDebe > 0 ? item.dc_ValorDebe : item.dc_ValorHaber;
                    }
                    else
                    {
                        item.IdCtaCble = info_cta.IdCtaCble;
                        item.pc_Cuenta = info_cta.IdCtaCble + " - " + info_cta.pc_Cuenta;
                        item.dc_Valor = item.dc_ValorDebe > 0 ? item.dc_ValorDebe : item.dc_ValorHaber;
                    }
                    
                }

                ImportacionDiarios_Lista.set_list(model, IdTransaccionSession);

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }            
        }
        #endregion

        #region Acciones
        [HttpPost]
        public ActionResult Index(ImportacionDiarios_Info model)
        {
            model.ListaTipoDocumento = ImportacionDiarios_Lista.get_list(model.IdTransaccionSession);
            ct_cbtecble_Info model_comprobante = new ct_cbtecble_Info();

            model_comprobante.IdEmpresa = model.IdEmpresa;
            model_comprobante.IdTipoCbte = model.IdTipoCbte;
            model_comprobante.IdSucursal = model.IdSucursal;
            model_comprobante.CodCbteCble = model.Codigo;
            model_comprobante.cb_Fecha = model.cb_Fecha;
            model_comprobante.cb_Valor = Convert.ToDouble( model.ListaTipoDocumento.Sum(q=> q.dc_ValorDebe) );
            model_comprobante.cb_Observacion = model.cb_Observacion;
            model_comprobante.lst_ct_cbtecble_det = new List<ct_cbtecble_det_Info>();
            model_comprobante.IdUsuario = SessionFixed.IdUsuario;

            foreach (var item in model.ListaTipoDocumento)
            {
                ct_cbtecble_det_Info lista_det = new ct_cbtecble_det_Info();
                lista_det.dc_Valor = Convert.ToDouble(item.dc_Valor);
                lista_det.dc_Observacion = item.Detalle;
                lista_det.IdCtaCble = item.IdCtaCble;
                lista_det.dc_para_conciliar = false;

                model_comprobante.lst_ct_cbtecble_det.Add(lista_det);
            }            

            if (!validar(model_comprobante, ref mensaje))
            {
                cargar_filtros(model.IdEmpresa);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            
            if (!bus_comprobante.guardarDB(model_comprobante))
            {
                cargar_filtros(model.IdEmpresa);
                return View(model);
            }

            bus_ImporacionDiarios.eliminar_x_tipo_doc(model.tipo_documento);

            return RedirectToAction("Index");
        }

        #endregion
    }

    public class ImportacionDiarios_List
    {
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        string variable = "ImportacionDiarios_Info";
        public List<ImportacionDiarios_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<ImportacionDiarios_Info> list = new List<ImportacionDiarios_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ImportacionDiarios_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ImportacionDiarios_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ImportacionDiarios_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);

            List<ImportacionDiarios_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencial = list.Count == 0 ? 1 : list.Max(q => q.Secuencial) + 1;
            info_det.dc_Valor = info_det.dc_ValorDebe > 0 ? info_det.dc_ValorDebe : info_det.dc_ValorHaber;
            info_det.dc_ValorDebe = info_det.dc_ValorDebe;
            info_det.dc_ValorHaber = info_det.dc_ValorHaber;

            if (info_det.IdCtaCble != null)
            {
                var cta = bus_plancta.get_info(IdEmpresa, info_det.IdCtaCble);
                if (cta != null)
                {
                    info_det.pc_Cuenta = cta.IdCtaCble + " - " + cta.pc_Cuenta;
                }                  
            }


            list.Add(info_det);
        }

        public void UpdateRow(ImportacionDiarios_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);

            ImportacionDiarios_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencial == info_det.Secuencial).First();
            edited_info.IdCtaCble = info_det.IdCtaCble;
            edited_info.dc_Valor = info_det.dc_ValorDebe > 0 ? info_det.dc_ValorDebe : info_det.dc_ValorHaber;
            edited_info.dc_ValorDebe = info_det.dc_ValorDebe;
            edited_info.dc_ValorHaber = info_det.dc_ValorHaber;

            var cta = bus_plancta.get_info(IdEmpresa, info_det.IdCtaCble);
            if (cta != null)
            {
                info_det.pc_Cuenta = cta.IdCtaCble + " - " + cta.pc_Cuenta;
            }

            edited_info.pc_Cuenta = info_det.pc_Cuenta;

        }

        public void DeleteRow(int Secuencial, decimal IdTransaccionSession)
        {
            List<ImportacionDiarios_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencial == Secuencial).First());
        }
    }
}