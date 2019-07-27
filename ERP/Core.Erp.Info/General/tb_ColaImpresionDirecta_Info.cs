using System;

namespace Core.Erp.Info.General
{
    public class tb_ColaImpresionDirecta_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdImpresion { get; set; }
        public string CodReporte { get; set; }
        public string IPUsuario { get; set; }
        public string IPImpresora { get; set; }
        public string Parametros { get; set; }
        public string Usuario { get; set; }
        public string NombreEmpresa { get; set; }
        public System.DateTime FechaEnvio { get; set; }
        public Nullable<System.DateTime> FechaImpresion { get; set; }
        public string Comentario { get; set; }
        public Nullable<int> NumCopias { get; set; }
    }
}
