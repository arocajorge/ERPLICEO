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
    public class ImagenesController : Controller
    {
        const string UploadDirectory = "~/Content/imagenes/";
        public string Name { get; set; }
        public ActionResult Imagenes()
        {
            return PartialView("Imagenes");
        }
        public ActionResult DragAndDropImageUpload([ModelBinder(typeof(DevExpressEditorsBinder))]IEnumerable<UploadedFile> ucDragAndDrop)
        {

            FileStream fS = new FileStream(ucDragAndDrop.FirstOrDefault().FileNameInStorage, FileMode.Open, FileAccess.Read);
            byte[] b = new byte[fS.Length];
            fS.Read(b, 0, (int)fS.Length);
            fS.Close();
            Session["imagen"] = b;
            return UploadedFilesContainer(true);
        }
        public ActionResult UploadedFilesContainer(bool useExtendedPopup)
        {
            ViewData["UseExtendedPopup"] = useExtendedPopup;
            return PartialView("UploadedFilesContainer");
        }



         }

}