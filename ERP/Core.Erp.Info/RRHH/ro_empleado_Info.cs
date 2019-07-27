using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.General;
using System.ComponentModel.DataAnnotations;

namespace Core.Erp.Info.RRHH
{
   public class ro_empleado_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdEmpleado { get; set; }
        public Nullable<decimal> IdEmpleado_Supervisor { get; set; }
        public decimal IdPersona { get; set; }
        [Required(ErrorMessage = ("el campo sucursal es obligatorio"))]
        public int IdSucursal { get; set; }
        [Required(ErrorMessage = ("el campo tipo de empleado es obligatorio"))]
        public string IdTipoEmpleado { get; set; }
        public string em_codigo { get; set; }
        public string Codigo_Biometrico { get; set; }
        [Required(ErrorMessage = ("el campo lugar de nacimiento es obligatorio"))]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo lugar de nacimiento debe tener mínimo 1 caracter y máximo 50")]
        public string em_lugarNacimiento { get; set; }
        public string em_CarnetIees { get; set; }
        public string em_cedulaMil { get; set; }
        public Nullable<System.DateTime> em_fechaSalida { get; set; }
        [Required(ErrorMessage = ("el campo fecha ingreso es obligatorio"))]
        public Nullable<System.DateTime> em_fechaIngaRol { get; set; }
        public string em_tipoCta { get; set; }
        public string em_NumCta { get; set; }
       
        public string em_estado { get; set; }
        public bool EstadoBool { get; set; }
        public double em_sueldoBasicoMen { get; set; }
        public Nullable<double> em_SueldoExtraMen { get; set; }
        public Nullable<double> em_MovilizacionQuincenal { get; set; }
        public byte[] em_foto { get; set; }
        public string em_empEspecial { get; set; }
        public string em_pagoFdoRsv { get; set; }
        public string em_huella { get; set; }
        public Nullable<int> IdCodSectorial { get; set; }
        public int IdDepartamento { get; set; }
        public string IdTipoSangre { get; set; }
        [Required(ErrorMessage = ("el campo cargo es obligatorio"))]
        public Nullable<int> IdCargo { get; set; }
        public string IdCtaCble_Emplea { get; set; }
        [Required(ErrorMessage = ("el campo ciudad es obligatorio"))]

        public string IdCiudad { get; set; }
        [Required(ErrorMessage = ("el campo correo es obligatorio"))]
        public string em_mail { get; set; }
        public string IdTipoLicencia { get; set; }
        public Nullable<int> IdBanco { get; set; }

        [Required(ErrorMessage = ("el campo área es obligatorio"))]

        public Nullable<int> IdArea { get; set; }
        [Required(ErrorMessage = ("el campo división es obligatorio"))]
        public Nullable<int> IdDivision { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transaccion { get; set; }
        public string IdUsuarioUltModi { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public double por_discapacidad { get; set; }
        public string carnet_conadis { get; set; }
        public string recibi_uniforme { get; set; }
        public Nullable<double> talla_pant { get; set; }
        public string talla_camisa { get; set; }
        public Nullable<double> talla_zapato { get; set; }
        
        public string em_status { get; set; }
        public string IdCondicionDiscapacidadSRI { get; set; }
        public string IdTipoIdentDiscapacitadoSustitutoSRI { get; set; }
        public string IdentDiscapacitadoSustitutoSRI { get; set; }
        public string IdAplicaConvenioDobleImposicionSRI { get; set; }
        public string IdTipoResidenciaSRI { get; set; }
        public string IdTipoSistemaSalarioNetoSRI { get; set; }
        public bool es_AcreditaHorasExtras { get; set; }
        public string IdTipoAnticipo { get; set; }
        public Nullable<double> ValorAnticipo { get; set; }
        public string CodigoSectorial { get; set; }
        public Nullable<bool> es_TruncarDecimalAnticipo { get; set; }
        public Nullable<double> em_AnticipoSueldo { get; set; }
        public Nullable<int> IdBanco_Acreditacion { get; set; }
        public bool Marca_Biometrico { get; set; }
        public string em_motivo_salisa { get; set; }
        public tb_persona_Info info_persona { get; set; }
        public Nullable<int> IdHorario { get; set; }
        public bool Tiene_ingresos_compartidos { get; set; }
        public bool Pago_por_horas { get; set; }
        public Nullable<double> Valor_horas_vespertina { get; set; }
        public Nullable<double> Valor_horas_matutino { get; set; }
        public Nullable<double> Valor_maximo_horas_vesp { get; set; }
        public Nullable<double> Valor_maximo_horas_mat { get; set; }
        public Nullable<double> Valor_horas_brigada { get; set; }
        public Nullable<double> Valor_hora_adicionales { get; set; }
        public Nullable<double> Valor_hora_control_salida { get; set; }
        public string IdCtaCble_x_pagar_empleado { get; set; }

        public string CodCatalogo_Ubicacion { get; set; }

        public bool GozaMasDeQuinceDiasVaciones { get; set; }
        public double DiasVacaciones { get; set; }
        public Nullable<int> IdSucursalContabilizacion { get; set; }


        #region datos personas
        [StringLength(100, MinimumLength = 0, ErrorMessage = "el campo apellido debe tener máximo 100 caracteres")]
        public string pe_apellido { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "el campo nombres debe tener máximo 100 caracteres")]
        public string pe_nombre { get; set; }
        [Required(ErrorMessage = ("el campo sexo es obligatorio"))]
        public string pe_sexo { get; set; }
        [Required(ErrorMessage = ("el campo estado civil es obligatorio"))]

        public string IdEstadoCivil { get; set; }
        [StringLength(150, MinimumLength = 0, ErrorMessage = "el campo dirección debe tener máximo 150 caracteres")]
        public string pe_direccion { get; set; }
        [Required(ErrorMessage = ("el campo telefono es obligatorio"))]
        public string pe_telfono_Contacto { get; set; }
        [Required(ErrorMessage = ("el campo celular es obligatorio"))]
        public string pe_celular { get; set; }
        [Required(ErrorMessage = ("el campo tipo de documento es obligatorio"))]
        public string IdTipoDocumento { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "el campo correo debe tener máximo 100 caracteres")]
        public string pe_correo { get; set; }
        [Required(ErrorMessage = ("el campo fecha nacimiento es obligatorio"))]

        public Nullable<System.DateTime> pe_fechaNacimiento { get; set; }


        #endregion


        public string de_descripcion { get; set; }
        public string ar_descripcion { get; set; }
        public string di_descripcion { get; set; }
        public string ca_descripcion { get; set; }
        public string ba_descripcion { get; set; }        
        public string ho_descripcion { get; set; }

        //
        public int? IdTipoNomina { get; set; }
        public string Empleado { get; set; }
        [Required(ErrorMessage = ("el campo número de documento es obligatorio"))]
        public string pe_cedulaRuc { get; set; }
        public ro_EmpleadoFoto_Info info_foto { get; set; }
        public List<ro_empleado_x_division_x_area_Info> lst_empleado_area { get; set; }
        public ro_empleado_Info()
        {
            info_persona = new tb_persona_Info();info_foto = new ro_EmpleadoFoto_Info();
        }
        public List<ro_empleado_x_jornada_Info> lst_det { get; set; }
    }
}
