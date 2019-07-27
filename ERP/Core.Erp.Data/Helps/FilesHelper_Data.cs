using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Data.General;
using System.IO;
using Core.Erp.Info.General;

namespace Core.Erp.Data.Helps
{
   public class FilesHelper_Data
    {
        
        public  bool Guardar_xml(byte[] buffer, string nom_archivo,string ftp_url, string ftp_usuario, string ftp_contrasenia)
        {
            try
            {
                tbl_usuario_ftp_Info info_ftp = new tbl_usuario_ftp_Info();
                tbl_usuario_ftp_Data data_ftp = new tbl_usuario_ftp_Data();

                info_ftp = data_ftp.get_info();
                if (info_ftp == null)
                    info_ftp = new tbl_usuario_ftp_Info();
                string ftpurl = String.Format("{0}/{1}/{2}", ftp_url, nom_archivo, nom_archivo);

                #region Crear directorio
                try
                {
                    WebRequest request = WebRequest.Create(String.Format("{0}/{1}/", ftp_url, nom_archivo));
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    request.Credentials = new NetworkCredential(ftp_usuario, ftp_contrasenia);
                    WebResponse response = request.GetResponse();
                }
                catch (Exception)
                {
                    //Si existe se cae
                }
                #endregion

                #region Enviar imágen
                Stream streamObj = System.IO.Stream.Null;
                streamObj.Read(buffer, 0, buffer.Length);
                streamObj.Close();
                streamObj = null;
                var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                requestObj.Credentials = new NetworkCredential(ftp_usuario, ftp_contrasenia);
                Stream requestStream = requestObj.GetRequestStream();
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Flush();
                requestStream.Close();
                requestObj = null;

                return true;
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public  string  Get_ATS(string nom_archivo)
        {
            try
            {
                return "";
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
