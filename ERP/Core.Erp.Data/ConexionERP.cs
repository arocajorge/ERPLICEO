using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data
{
    public class ConexionERP
    {

        public static string GetConnectionString()
        {
            string user = "sa";
            string ip = @"192.168.25.29\SQL2014";
            string password = "Sysadm753";
            string InitialCatalog = "DBERP";

            //string user = "sa";
            //string ip = "localhost";
            //string password = "admin*2016";
            //string InitialCatalog = "DBERP_ACADEMICO";

            return "data source=" + ip + ";initial catalog=" + InitialCatalog + ";user id=" + user + ";password=" + password + ";MultipleActiveResultSets=True;";
        }
    }
}
