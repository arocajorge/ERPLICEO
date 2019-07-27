using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
   public class tbl_usuario_ftp_Data
    {
        public tbl_usuario_ftp_Info get_info()
        {
            try
            {
                tbl_usuario_ftp_Info Info = new tbl_usuario_ftp_Info();
                using (Entities_general Context=new Entities_general())
                {
                    Info = (from q in Context.tbl_usuario_ftp
                             select new tbl_usuario_ftp_Info
                             {
                                 Id = q.Id,
                                 ftp_usuario = q.ftp_usuario,
                                 ftp_contrasenia = q.ftp_contrasenia,
                                 ftp_url = q.ftp_url
                             }).FirstOrDefault();
                }
                return Info;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
