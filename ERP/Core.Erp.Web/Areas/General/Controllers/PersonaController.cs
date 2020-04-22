using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using Core.Web.Bus.General;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.General.Controllers
{
    [SessionTimeout]
    public class PersonaController : Controller
    {
        #region Variables
        tb_Catalogo_Bus bus_catalogo = new tb_Catalogo_Bus();
        tb_Religion_Bus bus_religion = new tb_Religion_Bus();
        tb_profesion_Bus bus_profesion = new tb_profesion_Bus();
        tb_GrupoEtnico_Bus bus_grupoetnico = new tb_GrupoEtnico_Bus();
        #endregion

        #region Index
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_persona()
        {
            List<tb_persona_Info> model = bus_persona.get_list(true);
            return PartialView("_GridViewPartial_persona", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            var lst_sexo = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.SEXO), false);
            var lst_estado_civil = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.ESTCIVIL), false);
            var lst_tipo_doc = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPODOC), false);
            var lst_tipo_cta = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIP_CTA_AC), false);
            var lst_tipo_naturaleza = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPONATPER), false);
            var lst_tipo_sangre = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPOSANGRE), false);
            var lst_profesion = bus_profesion.GetList(false);
            var lst_religion = bus_religion.GetList(false);
            var lst_tipo_discapacidad = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPODISCAP), false);
            lst_tipo_discapacidad.Add(new tb_Catalogo_Info { CodCatalogo = "", ca_descripcion = "" });
            var lst_grupoetnico = bus_grupoetnico.GetList(false);

            ViewBag.lst_sexo = lst_sexo;
            ViewBag.lst_estado_civil = lst_estado_civil;
            ViewBag.lst_tipo_doc = lst_tipo_doc;
            ViewBag.lst_tipo_cta = lst_tipo_cta;
            ViewBag.lst_tipo_naturaleza = lst_tipo_naturaleza;
            ViewBag.lst_profesion = lst_profesion;
            ViewBag.lst_religion = lst_religion;
            ViewBag.lst_tipo_discapacidad = lst_tipo_discapacidad;
            ViewBag.lst_grupoetnico = lst_grupoetnico;
            ViewBag.lst_tipo_sangre = lst_tipo_sangre;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo()
        {
            tb_persona_Info model = new tb_persona_Info
            {
                pe_Naturaleza = "NATU",
                CodCatalogoCONADIS = "",
                CodCatalogoSangre = "O+"
            };

            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(tb_persona_Info model)
        {
            var return_naturaleza = "";
            if (bus_persona.validar_existe_cedula(model.IdTipoDocumento, model.pe_cedulaRuc, model.IdPersona) != 0)
            {
                ViewBag.mensaje = "El número de documento ya se encuentra registrado";
                cargar_combos();
                return View(model);
            }

            if (bus_persona.validar_existe_cedula(model.pe_cedulaRuc) != 0)
            {
                ViewBag.mensaje = "El número de documento ya se encuentra registrado";
                cargar_combos();
                return View(model);
            }

            if ((cl_funciones.ValidaIdentificacion(model.IdTipoDocumento, model.pe_Naturaleza, model.pe_cedulaRuc, ref return_naturaleza)))
            {
                model.pe_Naturaleza = return_naturaleza;
                if (!bus_persona.guardarDB(model))
                {
                    cargar_combos();
                    return View(model);
                }
            }
            else
            {
                ViewBag.mensaje = "Número de identificación inválida";
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index", "Persona");
        }

        public ActionResult Modificar(decimal IdPersona = 0)
        {
            tb_persona_Info model = bus_persona.get_info(IdPersona);
            if (model == null)
                return RedirectToAction("Index", "Persona");

            model.CodCatalogoCONADIS = (model.CodCatalogoCONADIS == null ? "" : model.CodCatalogoCONADIS);
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(tb_persona_Info model)
        {
            var return_naturaleza = "";
            if (bus_persona.validar_existe_cedula(model.IdTipoDocumento, model.pe_cedulaRuc, model.IdPersona) != 0)
            {
                ViewBag.mensaje = "El número de documento ya se encuentra registrado";
                cargar_combos();
                return View(model);
            }
            if ((cl_funciones.ValidaIdentificacion(model.IdTipoDocumento, model.pe_Naturaleza, model.pe_cedulaRuc, ref return_naturaleza)))
            {
                model.pe_Naturaleza = return_naturaleza;
                if (!bus_persona.modificarDB(model))
                {
                    cargar_combos();
                    return View(model);
                }
            }
            else
            {
                ViewBag.mensaje = "Número de identificación inválida";
                cargar_combos();
                return View(model);
            }
            
            return RedirectToAction("Index", "Persona");
        }

        public ActionResult Anular(decimal IdPersona = 0)
        {
            tb_persona_Info model = bus_persona.get_info(IdPersona);
            if (model == null)
                return RedirectToAction("Index", "Persona");

            model.CodCatalogoCONADIS = (model.CodCatalogoCONADIS == null ? "" : model.CodCatalogoCONADIS);
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(tb_persona_Info model)
        {
            if (!bus_persona.anularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", "Persona");
        }

        #endregion

        #region Json
        public JsonResult Validar_cedula_ruc(string naturaleza = "", string tipo_documento = "", string cedula_ruc = "")
        {
            var return_naturaleza = "";
            var isValid = cl_funciones.ValidaIdentificacion(tipo_documento, naturaleza, cedula_ruc, ref return_naturaleza);

            return Json(new { isValid= isValid, return_naturaleza = return_naturaleza }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class tb_persona_List
    {
        string Variable = "tb_persona_Info";
        public List<tb_persona_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<tb_persona_Info> list = new List<tb_persona_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<tb_persona_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<tb_persona_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}