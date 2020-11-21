using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data
{
    public class ConexionERP
    {

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString;
        }
    }
}
