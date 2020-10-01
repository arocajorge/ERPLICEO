using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Contabilidad
{
    public class FacturasTecnologico_Info
    {
        [JsonProperty("cedulaRuc")]
        public string cedulaRuc { get; set; }
        [JsonProperty("nu_comp")]
        public int nu_comp { get; set; }
        [JsonProperty("baseImpGrav")]
        public int baseImpGrav { get; set; }
        [JsonProperty("valorBaseIva")]
        public double valorBaseIva { get; set; }
        [JsonProperty("NoObjIva")]
        public int NoObjIva { get; set; }
        [JsonProperty("total")]
        public double total { get; set; }
        [JsonProperty("RUC_LCG")]
        public string RUC_LCG { get; set; }
        [JsonProperty("fe_factura")]
        public DateTime fe_factura { get; set; }
    }
}
