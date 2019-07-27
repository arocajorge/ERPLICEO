using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Contabilidad.ATS.ATS_Info
{
   public class compras_Info
    {
        public int IdEmpresa { get; set; }
        public int IdPeriodo { get; set; }
        public int Secuencia { get; set; }
        public string codSustento { get; set; }
        public string tpIdProv { get; set; }
        public string idProv { get; set; }
        public string tipoComprobante { get; set; }
        public string parteRel { get; set; }
        public string tipoProv { get; set; }
        public string denopr { get; set; }
        public System.DateTime fechaRegistro { get; set; }
        public string establecimiento { get; set; }
        public string puntoEmision { get; set; }
        public string secuencial { get; set; }
        public System.DateTime fechaEmision { get; set; }
        public string autorizacion { get; set; }
        public decimal baseNoGraIva { get; set; }
        public decimal baseImponible { get; set; }
        public decimal baseImpGrav { get; set; }
        public decimal baseImpExe { get; set; }
        public decimal montoIce { get; set; }
        public decimal montoIva { get; set; }
        public string pagoLocExt { get; set; }
        public string denopago { get; set; }
        public string paisEfecPago { get; set; }
        public string aplicConvDobTrib { get; set; }
        public string pagExtSujRetNorLeg { get; set; }
        public string formaPago { get; set; }
        public string docModificado { get; set; }
        public string estabModificado { get; set; }
        public string ptoEmiModificado { get; set; }
        public string secModificado { get; set; }
        public string autModificado { get; set; }
    }
}
