using Core.Erp.Data.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Helps
{
   public class FilesHelper_Bus
    {
        FilesHelper_Data odata = new FilesHelper_Data();
        public bool Guardar_xml(byte[] buffer, string nom_archivo, string ftp_url, string ftp_usuario, string ftp_contrasenia)
        {
            try
            {
                return odata.Guardar_xml(buffer, nom_archivo, ftp_url, ftp_usuario, ftp_contrasenia);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string Get_ATS(string nom_archivo)
        {
            try
            {
                return odata.Get_ATS(nom_archivo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
