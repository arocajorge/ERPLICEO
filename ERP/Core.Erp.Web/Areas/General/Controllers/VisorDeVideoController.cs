using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.General.Controllers
{
    [SessionTimeout]
    public class VisorDeVideoController : Controller
    {
        tb_visor_video_Bus bus_pais = new tb_visor_video_Bus();
        #region vistas
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_visor_video()
        {
            List<tb_visor_video_Info> model = new List<tb_visor_video_Info>();
            model = bus_pais.get_list(true);
            return PartialView("_GridViewPartial_visor_video", model);
        }
        public ActionResult ReproducirVideo(string Cod_video)
        {
            tb_visor_video_Info model = new tb_visor_video_Info();
            model = bus_pais.get_info(Cod_video);
            if (model == null)
                return RedirectToAction("Index");
            ViewBag.Cod_video = model.Cod_video+".mp4";

            return View(model);
        }
        #endregion
        #region acciones
        public ActionResult Nuevo()
        {
            tb_visor_video_Info model = new tb_visor_video_Info();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(tb_visor_video_Info model)
        {
            if (bus_pais.si_existe(model.Cod_video))
            {
                ViewBag.mensaje = "El código ya se encuentra registrado";
                return View(model);
            }
            if (!bus_pais.guardarDB(model))
            {
                return View(model);
            }
            model.video = tb_visor_video_video.video;
            string patch = Path.Combine(Server.MapPath("~/Content"), model.Cod_video);
            if (System.IO.File.Exists(patch + ".mp4"))
                System.IO.File.Delete(patch + ".mp4");
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(patch + ".mp4", true))
            {
                file.WriteLine(model.video);
                file.Close();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(string Cod_video)
        {
            tb_visor_video_Info model = bus_pais.get_info(Cod_video);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(tb_visor_video_Info model)
        {
            if (!bus_pais.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(string Cod_video)
        {
            tb_visor_video_Info model = bus_pais.get_info(Cod_video);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(tb_visor_video_Info model)
        {
            if (!bus_pais.anularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        const string UploadDirectory = "~/Content/imagenes/";

        public UploadedFile UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControl", tb_visor_video_video.UploadValidationSettings, tb_visor_video_video.FileUploadComplete);

            byte[] model = tb_visor_video_video.video;
            UploadedFile file = new UploadedFile();
            return file;
        }
    }

    public class tb_visor_video_video
    {
        public static byte[] video { get; set; }
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".vmv", ".mp4" },
            MaxFileSize = 999999999999999999
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {

            if (e.UploadedFile.IsValid)
            {
                video = e.UploadedFile.FileBytes;
            }
        }
    }

}