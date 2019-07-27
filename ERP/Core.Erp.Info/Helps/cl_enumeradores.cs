namespace Core.Erp.Info.Helps
{
    public class cl_enumeradores
    {

        public enum eTipoCbteBancario
        {
            CHEQ,
            DEPO,
            NCBA,
            NDBA
        }
        public enum eTipoDocumento
        {
            COTIZ,
            FACT,
            GUIA,
            NTCR,
            NTDB,
            NTPEDI,
            NTVTA,
            ORDESP,
            PEDI,
            RETEN
        }
        public enum eTipoCatalogoGeneral
        {
            SEXO = 1,
            ESTCIVIL = 2,
            TIPODOC = 3,
            TIPONATPER = 5,
            TIP_CTA_AC = 27
        }

        public enum eTipoSexoGeneral
        {
            SEXO_FEM ,
            SEXO_MAS
        }        

        public enum eTipoCatalogoBanco
        {
            CALTAMOR = 1,
            EST_CB_BA = 2,
            EST_CH = 3,
            EST_CONCI = 4,
            EST_PAG = 5
        }

        public enum eTipoCatalogoFact
        {
            FormaDePago = 15            
        }

        public enum eCatalogoFact
        {
            EFEC,
            TARCRE,
            CRED,
            TARDEB
        }

        public enum eTipoBusquedaProducto
        {
            SOLOPADRES,
            SOLOHIJOS,
            PORMODULO,
            TODOS,
            TODOS_MENOS_PADRES,
            PORSUCURSAL,
            SOLOSERVICIOS
        }        

        public enum eTipoCatalogoInventario
        {
            EST_APROB = 1,
            FECH_CONTA = 4,
            TIPO_CONTA_CTA = 6,
            ING_EGR = 8
        }
        
        public enum eEstadoEmpleadoRRHH
        {
            EST_ACT=1,
            EST_DES=2,
            EST_INC=3,
            EST_LIQ=4,
            EST_PER=5,
            EST_PLQ=6,
            EST_SUB=7,
            EST_VAC=8,
            EST_VB=9,

        }
        public enum eEstadoContratoRRHH
        {
            ECT_ACT,
            ECT_LIQ,
            ECT_PLQ
        }
        public enum eJornadaRRHH
        {
            MATUTINA,
            VESPERTINA,
            BRIGADA,
            HORA_ADICI,
            HORA_CONT_SAL,
            HORAS_RECARGO

        }
        public enum eTipoCuentaRRHH
        {
            AHO,
            CHE,
            COR,
            EFE,
            VRT
        }
        public enum eTipoPeriodoRRHH
        {
            BEN_SOCIAL,
            MEN,
            QUINCE,
            SEM
        }
        public enum eTipoTerminacioncontratoRRHH
        {

            CTL_01,
            CTL_02,
            CTL_03,
            CTL_04,
            CTL_05,
            CTL_06,
            CTL_07,
            CTL_08,
            CTL_09,
            CTL_10,
            CTL_11
        }
        public enum eTipoPermisoRRHH
        {

           MATERNIDAD,
           PATERNIDAD,
           ENFERMEDAD,
           PERSONAL
        }
        public enum eTipoMarcacionRRHH
        {

            IN1,
            IN2,
            OUT1,
            OUT2,
            RLUNCH,
            SLUNCH
        }
        public enum eTipoIngEgr
        {
            ING,
            EGR
        }
        public enum eModulo
        {
            INV,
            FAC,
            COM,
            ACF,
            RRHH,
            IMP,
            CONTA,
            CAJA,
            BANCO,
            CXC,
            CXP
        }

        public enum eCobroFactura
        {
            EFEC,
            TARJ,
            CRED
        }

        public enum eTipoCatalogoAF
        {
            TIP_COLOR,
            TIP_MODELO,
            TIP_ESTADO_AF,
            TIP_MARCA,
            TIP_UBICACION,
            TIP_CTACBLE
        }
        public enum eTipoCatalogoCOM
        {
            EST_ANU,
            EST_APRO
        }


        public enum eTipoMejBajAF
        {
            Mejo_Acti,
            Baja_Acti
        }

        public enum eTipoPersona
        {
            CLIENTE,
            EMPLEA,
            PERSONA,
            PROVEE
        }

        public enum eEstadoCierreBanco
        {
            PRE_CONCIL,
            CONCILIADO
        }

        public enum eEstadoCierreCaja
        {
            EST_CIE_ABI,
            EST_CIE_CER
        }

        public enum eTipoOrdenPago
        {
            ANTI_EMPLE,
            ANTI_PROVEE,
            FACT_PROVEE,
            LIQ_HAB,
            OTROS_CONC,
            PRESTAMOS,
            VACACIONES
        }

        public enum eEstadoAprobacionOrdenPago
        {
            APRO,
            PENDI,
            REPRO
        }

        public enum eFormaPagoOrdenPago
        {
            CHEQUE,
            EFEC,
            NTDEB_BAN,
            TARJE_CRE
        }

        public enum eTipoServicioCXP
        {
            BIEN,
            SERVI,
            AMBAS
        }
        public enum eTipoNotaCXP
        {
            T_TIP_NOTA_INT,
            T_TIP_NOTA_SRI
        }
        public enum eTiLocalizacionCXP
        {
            LOC,
            EXT
        }  
        public enum eTipoCobroTomaCuentaDe
        {
            TIP_COBRO,
            CAJA
        }

        public enum eTipoProcesoBancario
        {
            NCR,
            ROL_ELECTRONICO,
            NCR_TRASN
        }
        
    }
}
