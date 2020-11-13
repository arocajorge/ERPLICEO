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
            string ip = string.Empty;
            string password = string.Empty;
            string user = string.Empty;
            string InitialCatalog = string.Empty;

            string Cadena = "LICEO";

            switch (Cadena)
            {
                case "LOCAL":
                    ip = @"localhost";
                    password = "admin*2016";
                    user = "sa";
                    InitialCatalog = "DBERP_ACADEMICO";
                    break;
                case "LICEO":
                    ip = @"192.168.25.29\SQL2014";
                    password = "Sysadm753";
                    user = "sa";
                    InitialCatalog = "DBERP";
                    break;
            }

            return "data source=" + ip + ";initial catalog=" + InitialCatalog + ";user id=" + user + ";password=" + password + ";MultipleActiveResultSets=True;";
        }
    }
}
