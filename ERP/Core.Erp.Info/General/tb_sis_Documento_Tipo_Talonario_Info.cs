using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
    public class tb_sis_Documento_Tipo_Talonario_Info
    {

        public int IdEmpresa { get; set; }
        public string CodDocumentoTipo { get; set; }
        [Required(ErrorMessage = "El campo establecimiento es obligatorio")]
        [StringLength(3, MinimumLength = 1, ErrorMessage = "el campo establecimiento debe tener mínimo 1 caracter y máximo 3")]
        public string Establecimiento { get; set; }
        [Required(ErrorMessage = "El campo punto de emisión es obligatorio")]
        [StringLength(3, MinimumLength = 1, ErrorMessage = "el campo punto de emisión debe tener mínimo 1 caracter y máximo 3")]
        public string PuntoEmision { get; set; }
        [Required(ErrorMessage = "El campo número de documento es obligatorio")]
        [StringLength(9, MinimumLength = 1, ErrorMessage = "el campo número de documento debe tener mínimo 1 caracter y máximo 9")]
        public string NumDocumento { get; set; }
        public Nullable<System.DateTime> FechaCaducidad { get; set; }
        public bool Usado { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        [Required(ErrorMessage = "El campo sucursal es obligatorio")]
        public int IdSucursal { get; set; }
        public string NumAutorizacion { get; set; }
        public bool es_Documento_Electronico { get; set; }

        //campos fuera de la tabla

        public string Documentofinal { get; set; }
        public int Cantidad { get; set; }

    }
}
