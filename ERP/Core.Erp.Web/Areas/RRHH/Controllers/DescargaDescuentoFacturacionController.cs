using Core.Erp.Bus.General;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using Core.Erp.Web.Helps;
using DevExpress.Utils.About;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class DescargaDescuentoFacturacionController : Controller
    {
        #region Variables
        ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
        ro_empleado_novedad_det_List Lista_DescuentosEmpleados = new ro_empleado_novedad_det_List();
        ro_empleado_novedad_det_Excel_List Lista_Excel = new ro_empleado_novedad_det_Excel_List();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        ro_empleado_novedad_det_Bus bus_novedad_empleado = new ro_empleado_novedad_det_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        string rutafile = System.IO.Path.GetTempPath();
        #endregion

        #region Vistas
        [ValidateInput(false)]
        public ActionResult GridViewPartial_DescuentosEmpleado()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<ro_empleado_novedad_det_Info> model = new List<ro_empleado_novedad_det_Info>();
            model = Lista_DescuentosEmpleados.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_DescuentosEmpleado", model);
        }
        #endregion

        public ActionResult Nuevo(DateTime? fecha_ini, DateTime? fecha_fin, int IdSucursal = 0, int IdNomina = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdNomina = IdNomina,
                fecha_ini = fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(fecha_ini),
                fecha_fin = fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(fecha_fin),
                FormatoDescuento = false,
                FormatoPrestamo = false,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
            };

            ViewBag.IdSucursal = model.IdSucursal;
            ViewBag.IdNomina = model.IdNomina;
            ViewBag.fecha_ini = model.fecha_ini;
            ViewBag.fecha_fin = model.fecha_fin;

            cargar_combos_consulta(model);
            var Lista = new List<ro_empleado_novedad_det_Info>();
            Lista_DescuentosEmpleados.set_list(Lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(cl_filtros_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var Lista = new List<ro_empleado_novedad_det_Info>();
            Lista = bus_novedad_empleado.get_list_descuentos(model.IdEmpresa, model.IdSucursal, model.FormatoDescuento, model.FormatoPrestamo, model.fecha_ini, model.fecha_fin);
            Lista_DescuentosEmpleados.set_list(Lista, model.IdTransaccionSession);
            var lst_descuentos = Lista_DescuentosEmpleados.get_list(model.IdTransaccionSession);
            List<ro_empleado_novedad_det_Info> lst_excel = new List<ro_empleado_novedad_det_Info>();

            if (model.FormatoDescuento == true)
            {
                foreach (var item in lst_descuentos)
                {
                    var info_excel = new ro_empleado_novedad_det_Info {
                        pe_cedulaRuc = item.pe_cedulaRuc,
                        pe_nombreCompleto = item.pe_nombreCompleto,
                        CodigoRubroDescto = item.CodigoRubroDescto,
                        Total = item.Total,
                        CantidadHoras = (item.CantidadHoras == null ? 0 : item.CantidadHoras)
                    };

                    lst_excel.Add(info_excel);
                }
            }

            if (model.FormatoPrestamo == true)
            {
                foreach (var item in lst_descuentos)
                {
                    var info_excel = new ro_empleado_novedad_det_Info
                    {
                        pe_cedulaRuc = item.pe_cedulaRuc,
                        pe_nombreCompleto = item.pe_nombreCompleto,
                        CodigoRubroDescto = item.CodigoRubroDescto,
                        Total = item.Total,
                        CantidadHoras = item.Num_Coutas
                    };

                    lst_excel.Add(info_excel);
                }
            }

            Lista_Excel.set_list(lst_excel, model.IdTransaccionSession);
            cargar_combos_consulta(model);
            return View(model);
        }

        private void cargar_combos_consulta(cl_filtros_Info model)
        {
            //int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_sucursal = bus_sucursal.GetList(model.IdEmpresa, SessionFixed.IdUsuario, true);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_nomina = bus_nomina.get_list(model.IdEmpresa, false);
            ViewBag.lst_nomina = lst_nomina;
        }

        #region Excel
        public void WriteTsv<T>(IEnumerable<T> data, TextWriter output)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write(prop.DisplayName); // header
                output.Write("\t");
            }
            output.WriteLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    output.Write("\t");
                }
                output.WriteLine();
            }
        }

        public void ExportListFromTsv()
        {
            var Lista = Lista_Excel.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            //var data = new[] {
            //    new { Cedula = "CEDULA", Empleado = "EMPLEADO", CodRubro = "CODIGO RUBRO", Monto = "MONTO", Cantidad = "NUM HORAS/COUTAS" }
            //};

            var lst = new List<Excel_List>();
            //var cabecera = new Excel_List
            //{
            //    Cedula = "CEDULA",
            //    Empleado = "EMPLEADO",
            //    CodRubro = "CODIGO RUBRO",
            //    Monto = "MONTO",
            //    Cantidad = "NUM HORAS/COUTAS",
            //};
            //lst.Add(cabecera);

            foreach (var item in Lista)
            {
                var info = new Excel_List {
                    Cedula = Convert.ToString(item.pe_cedulaRuc),
                    Empleado = Convert.ToString(item.pe_nombreCompleto),
                    CodRubro = Convert.ToString(item.CodigoRubroDescto),
                    Monto = Convert.ToString(item.Total),
                    Cantidad = Convert.ToString((item.CantidadHoras == null ? 0 : item.CantidadHoras))
                };

                lst.Add(info);
            }

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Contact.xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            WriteTsv(lst, Response.Output);
            Response.End();
        }
        #endregion
    }

    public class ro_empleado_novedad_det_List
    {
        string variable = "ro_empleado_novedad_det_Info";
        public List<ro_empleado_novedad_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_empleado_novedad_det_Info> list = new List<ro_empleado_novedad_det_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_empleado_novedad_det_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_empleado_novedad_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class ro_empleado_novedad_det_Excel_List
    {
        string variable = "ro_empleado_novedad_det_Excel_Info";
        public List<ro_empleado_novedad_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_empleado_novedad_det_Info> list = new List<ro_empleado_novedad_det_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_empleado_novedad_det_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_empleado_novedad_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class Excel_List
    {
        public string Cedula { get; set; }
        public string Empleado { get; set; }
        public string CodRubro { get; set; }
        public string Monto { get; set; }
        public string Cantidad { get; set; }
    }
}