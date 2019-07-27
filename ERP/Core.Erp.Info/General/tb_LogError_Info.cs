namespace Core.Erp.Info.General
{
    public class tb_LogError_Info
    {
        public decimal IdError { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string InnerException { get; set; }
        public string IdUsuario { get; set; }
        public string Clase { get; set; }
        public string Metodo { get; set; }
    }
}
