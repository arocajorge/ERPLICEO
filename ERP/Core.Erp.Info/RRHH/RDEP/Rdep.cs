using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH.RDEP
{
    using System.Xml.Serialization;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class rdep
    {

        private string numRucField;

        private string anioField;

        private List<datRetRelDepTyp> retRelDepField;

        /// <remarks/>
        public string numRuc
        {
            get
            {
                return this.numRucField;
            }
            set
            {
                this.numRucField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string anio
        {
            get
            {
                return this.anioField;
            }
            set
            {
                this.anioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("datRetRelDep", IsNullable = false)]
        public List< datRetRelDepTyp> retRelDep
        {
            get
            {
                return this.retRelDepField;
            }
            set
            {
                this.retRelDepField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class datRetRelDepTyp
    {

        private datEmpTyp empleadoField;

        private decimal suelSalField;

        private decimal sobSuelComRemuField;

        private decimal partUtilField;

        private decimal intGrabGenField;

        private decimal impRentEmplField;

        private decimal decimTerField;

        private decimal decimCuarField;

        private decimal fondoReservaField;

        private decimal salarioDignoField;

        private decimal otrosIngRenGravField;

        private decimal ingGravConEsteEmplField;

        private string sisSalNetField;

        private decimal apoPerIessField;

        private decimal aporPerIessConOtrosEmplsField;

        private decimal deducViviendaField;

        private bool deducViviendaFieldSpecified;

        private decimal deducSaludField;

        private bool deducSaludFieldSpecified;

        private decimal deducEducaField;

        private bool deducEducaFieldSpecified;

        private decimal deducAliementField;

        private bool deducAliementFieldSpecified;

        private decimal deducVestimField;

        private bool deducVestimFieldSpecified;

        private decimal deducArtycultField;

        private bool deducArtycultFieldSpecified;

        private decimal exoDiscapField;

        private decimal exoTerEdField;

        private decimal basImpField;

        private decimal impRentCausField;

        private decimal valRetAsuOtrosEmplsField;

        private decimal valImpAsuEsteEmplField;

        private decimal valRetField;

        private contribucionType contribucionField;

        /// <remarks/>
        public datEmpTyp empleado
        {
            get
            {
                return this.empleadoField;
            }
            set
            {
                this.empleadoField = value;
            }
        }

        /// <remarks/>
        public decimal suelSal
        {
            get
            {
                return this.suelSalField;
            }
            set
            {
                this.suelSalField = value;
            }
        }

        /// <remarks/>
        public decimal sobSuelComRemu
        {
            get
            {
                return this.sobSuelComRemuField;
            }
            set
            {
                this.sobSuelComRemuField = value;
            }
        }

        /// <remarks/>
        public decimal partUtil
        {
            get
            {
                return this.partUtilField;
            }
            set
            {
                this.partUtilField = value;
            }
        }

        /// <remarks/>
        public decimal intGrabGen
        {
            get
            {
                return this.intGrabGenField;
            }
            set
            {
                this.intGrabGenField = value;
            }
        }

        /// <remarks/>
        public decimal impRentEmpl
        {
            get
            {
                return this.impRentEmplField;
            }
            set
            {
                this.impRentEmplField = value;
            }
        }

        /// <remarks/>
        public decimal decimTer
        {
            get
            {
                return this.decimTerField;
            }
            set
            {
                this.decimTerField = value;
            }
        }

        /// <remarks/>
        public decimal decimCuar
        {
            get
            {
                return this.decimCuarField;
            }
            set
            {
                this.decimCuarField = value;
            }
        }

        /// <remarks/>
        public decimal fondoReserva
        {
            get
            {
                return this.fondoReservaField;
            }
            set
            {
                this.fondoReservaField = value;
            }
        }

        /// <remarks/>
        public decimal salarioDigno
        {
            get
            {
                return this.salarioDignoField;
            }
            set
            {
                this.salarioDignoField = value;
            }
        }

        /// <remarks/>
        public decimal otrosIngRenGrav
        {
            get
            {
                return this.otrosIngRenGravField;
            }
            set
            {
                this.otrosIngRenGravField = value;
            }
        }

        /// <remarks/>
        public decimal ingGravConEsteEmpl
        {
            get
            {
                return this.ingGravConEsteEmplField;
            }
            set
            {
                this.ingGravConEsteEmplField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string sisSalNet
        {
            get
            {
                return this.sisSalNetField;
            }
            set
            {
                this.sisSalNetField = value;
            }
        }

        /// <remarks/>
        public decimal apoPerIess
        {
            get
            {
                return this.apoPerIessField;
            }
            set
            {
                this.apoPerIessField = value;
            }
        }

        /// <remarks/>
        public decimal aporPerIessConOtrosEmpls
        {
            get
            {
                return this.aporPerIessConOtrosEmplsField;
            }
            set
            {
                this.aporPerIessConOtrosEmplsField = value;
            }
        }

        /// <remarks/>
        public decimal deducVivienda
        {
            get
            {
                return this.deducViviendaField;
            }
            set
            {
                this.deducViviendaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool deducViviendaSpecified
        {
            get
            {
                return this.deducViviendaFieldSpecified;
            }
            set
            {
                this.deducViviendaFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal deducSalud
        {
            get
            {
                return this.deducSaludField;
            }
            set
            {
                this.deducSaludField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool deducSaludSpecified
        {
            get
            {
                return this.deducSaludFieldSpecified;
            }
            set
            {
                this.deducSaludFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal deducEduca
        {
            get
            {
                return this.deducEducaField;
            }
            set
            {
                this.deducEducaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool deducEducaSpecified
        {
            get
            {
                return this.deducEducaFieldSpecified;
            }
            set
            {
                this.deducEducaFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal deducAliement
        {
            get
            {
                return this.deducAliementField;
            }
            set
            {
                this.deducAliementField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool deducAliementSpecified
        {
            get
            {
                return this.deducAliementFieldSpecified;
            }
            set
            {
                this.deducAliementFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal deducVestim
        {
            get
            {
                return this.deducVestimField;
            }
            set
            {
                this.deducVestimField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool deducVestimSpecified
        {
            get
            {
                return this.deducVestimFieldSpecified;
            }
            set
            {
                this.deducVestimFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal deducArtycult
        {
            get
            {
                return this.deducArtycultField;
            }
            set
            {
                this.deducArtycultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool deducArtycultSpecified
        {
            get
            {
                return this.deducArtycultFieldSpecified;
            }
            set
            {
                this.deducArtycultFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal exoDiscap
        {
            get
            {
                return this.exoDiscapField;
            }
            set
            {
                this.exoDiscapField = value;
            }
        }

        /// <remarks/>
        public decimal exoTerEd
        {
            get
            {
                return this.exoTerEdField;
            }
            set
            {
                this.exoTerEdField = value;
            }
        }

        /// <remarks/>
        public decimal basImp
        {
            get
            {
                return this.basImpField;
            }
            set
            {
                this.basImpField = value;
            }
        }

        /// <remarks/>
        public decimal impRentCaus
        {
            get
            {
                return this.impRentCausField;
            }
            set
            {
                this.impRentCausField = value;
            }
        }

        /// <remarks/>
        public decimal valRetAsuOtrosEmpls
        {
            get
            {
                return this.valRetAsuOtrosEmplsField;
            }
            set
            {
                this.valRetAsuOtrosEmplsField = value;
            }
        }

        /// <remarks/>
        public decimal valImpAsuEsteEmpl
        {
            get
            {
                return this.valImpAsuEsteEmplField;
            }
            set
            {
                this.valImpAsuEsteEmplField = value;
            }
        }

        /// <remarks/>
        public decimal valRet
        {
            get
            {
                return this.valRetField;
            }
            set
            {
                this.valRetField = value;
            }
        }

        /// <remarks/>
        public contribucionType contribucion
        {
            get
            {
                return this.contribucionField;
            }
            set
            {
                this.contribucionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class datEmpTyp
    {

        private benGalpgType benGalpgField;

        private datEmpTypTipIdRet tipIdRetField;

        private string idRetField;

        private string apellidoTrabField;

        private string nombreTrabField;

        private string estabField;

        private resciTyp residenciaTrabField;

        private string paisResidenciaField;

        private convImposTyp aplicaConvenioField;

        private discapTyp tipoTrabajDiscapField;

        private string porcentajeDiscapField;

        private tipIdDiscapTyp tipIdDiscapField;

        private string idDiscapField;

        /// <remarks/>
        public benGalpgType benGalpg
        {
            get
            {
                return this.benGalpgField;
            }
            set
            {
                this.benGalpgField = value;
            }
        }

        /// <remarks/>
        public datEmpTypTipIdRet tipIdRet
        {
            get
            {
                return this.tipIdRetField;
            }
            set
            {
                this.tipIdRetField = value;
            }
        }

        /// <remarks/>
        public string idRet
        {
            get
            {
                return this.idRetField;
            }
            set
            {
                this.idRetField = value;
            }
        }

        /// <remarks/>
        public string apellidoTrab
        {
            get
            {
                return this.apellidoTrabField;
            }
            set
            {
                this.apellidoTrabField = value;
            }
        }

        /// <remarks/>
        public string nombreTrab
        {
            get
            {
                return this.nombreTrabField;
            }
            set
            {
                this.nombreTrabField = value;
            }
        }

        /// <remarks/>
        public string estab
        {
            get
            {
                return this.estabField;
            }
            set
            {
                this.estabField = value;
            }
        }

        /// <remarks/>
        public resciTyp residenciaTrab
        {
            get
            {
                return this.residenciaTrabField;
            }
            set
            {
                this.residenciaTrabField = value;
            }
        }

        /// <remarks/>
        public string paisResidencia
        {
            get
            {
                return this.paisResidenciaField;
            }
            set
            {
                this.paisResidenciaField = value;
            }
        }

        /// <remarks/>
        public convImposTyp aplicaConvenio
        {
            get
            {
                return this.aplicaConvenioField;
            }
            set
            {
                this.aplicaConvenioField = value;
            }
        }

        /// <remarks/>
        public discapTyp tipoTrabajDiscap
        {
            get
            {
                return this.tipoTrabajDiscapField;
            }
            set
            {
                this.tipoTrabajDiscapField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string porcentajeDiscap
        {
            get
            {
                return this.porcentajeDiscapField;
            }
            set
            {
                this.porcentajeDiscapField = value;
            }
        }

        /// <remarks/>
        public tipIdDiscapTyp tipIdDiscap
        {
            get
            {
                return this.tipIdDiscapField;
            }
            set
            {
                this.tipIdDiscapField = value;
            }
        }

        /// <remarks/>
        public string idDiscap
        {
            get
            {
                return this.idDiscapField;
            }
            set
            {
                this.idDiscapField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
    [System.SerializableAttribute()]
    public enum benGalpgType
    {

        /// <remarks/>
        SI,

        /// <remarks/>
        NO,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
    [System.SerializableAttribute()]
    public enum datEmpTypTipIdRet
    {

        /// <remarks/>
        C,

        /// <remarks/>
        P,

        /// <remarks/>
        E,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
    [System.SerializableAttribute()]
    public enum resciTyp
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("00")]
        Item00,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
    [System.SerializableAttribute()]
    public enum convImposTyp
    {

        /// <remarks/>
        SI,

        /// <remarks/>
        NO,

        /// <remarks/>
        NA,

        /// <remarks/>
        SD,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
    [System.SerializableAttribute()]
    public enum discapTyp
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("00")]
        Item00,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04")]
        Item04,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
    [System.SerializableAttribute()]
    public enum tipIdDiscapTyp
    {

        /// <remarks/>
        C,

        /// <remarks/>
        P,

        /// <remarks/>
        E,

        /// <remarks/>
        N,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class contribucionType
    {

        private decimal remunContrEstEmplField;

        private decimal remunContrOtrEmplField;

        private decimal exonRemunContrField;

        private decimal totRemunContrField;

        private int numMesTrabContrEstEmplField;

        private int numMesTrabContrOtrEmplField;

        private int totNumMesTrabContrField;

        private decimal remunMenPromContrField;

        private int numMesContrGenEstEmplField;

        private int numMesContrGenOtrEmplField;

        private int totNumMesContrGenField;

        private decimal totContrGenField;

        private decimal credTribDonContrOtrEmplField;

        private decimal credTribDonContrEstEmplField;

        private decimal credTribDonContrNOEstEmplField;

        private decimal totCredTribDonContrField;

        private decimal contrPagField;

        private decimal contrAsuOtrEmplField;

        private decimal contrRetOtrEmplField;

        private decimal contrAsuEstEmplField;

        private decimal contrRetEstEmplField;

        /// <remarks/>
        public decimal remunContrEstEmpl
        {
            get
            {
                return this.remunContrEstEmplField;
            }
            set
            {
                this.remunContrEstEmplField = value;
            }
        }

        /// <remarks/>
        public decimal remunContrOtrEmpl
        {
            get
            {
                return this.remunContrOtrEmplField;
            }
            set
            {
                this.remunContrOtrEmplField = value;
            }
        }

        /// <remarks/>
        public decimal exonRemunContr
        {
            get
            {
                return this.exonRemunContrField;
            }
            set
            {
                this.exonRemunContrField = value;
            }
        }

        /// <remarks/>
        public decimal totRemunContr
        {
            get
            {
                return this.totRemunContrField;
            }
            set
            {
                this.totRemunContrField = value;
            }
        }

        /// <remarks/>
        public int numMesTrabContrEstEmpl
        {
            get
            {
                return this.numMesTrabContrEstEmplField;
            }
            set
            {
                this.numMesTrabContrEstEmplField = value;
            }
        }

        /// <remarks/>
        public int numMesTrabContrOtrEmpl
        {
            get
            {
                return this.numMesTrabContrOtrEmplField;
            }
            set
            {
                this.numMesTrabContrOtrEmplField = value;
            }
        }

        /// <remarks/>
        public int totNumMesTrabContr
        {
            get
            {
                return this.totNumMesTrabContrField;
            }
            set
            {
                this.totNumMesTrabContrField = value;
            }
        }

        /// <remarks/>
        public decimal remunMenPromContr
        {
            get
            {
                return this.remunMenPromContrField;
            }
            set
            {
                this.remunMenPromContrField = value;
            }
        }

        /// <remarks/>
        public int numMesContrGenEstEmpl
        {
            get
            {
                return this.numMesContrGenEstEmplField;
            }
            set
            {
                this.numMesContrGenEstEmplField = value;
            }
        }

        /// <remarks/>
        public int numMesContrGenOtrEmpl
        {
            get
            {
                return this.numMesContrGenOtrEmplField;
            }
            set
            {
                this.numMesContrGenOtrEmplField = value;
            }
        }

        /// <remarks/>
        public int totNumMesContrGen
        {
            get
            {
                return this.totNumMesContrGenField;
            }
            set
            {
                this.totNumMesContrGenField = value;
            }
        }

        /// <remarks/>
        public decimal totContrGen
        {
            get
            {
                return this.totContrGenField;
            }
            set
            {
                this.totContrGenField = value;
            }
        }

        /// <remarks/>
        public decimal credTribDonContrOtrEmpl
        {
            get
            {
                return this.credTribDonContrOtrEmplField;
            }
            set
            {
                this.credTribDonContrOtrEmplField = value;
            }
        }

        /// <remarks/>
        public decimal credTribDonContrEstEmpl
        {
            get
            {
                return this.credTribDonContrEstEmplField;
            }
            set
            {
                this.credTribDonContrEstEmplField = value;
            }
        }

        /// <remarks/>
        public decimal credTribDonContrNOEstEmpl
        {
            get
            {
                return this.credTribDonContrNOEstEmplField;
            }
            set
            {
                this.credTribDonContrNOEstEmplField = value;
            }
        }

        /// <remarks/>
        public decimal totCredTribDonContr
        {
            get
            {
                return this.totCredTribDonContrField;
            }
            set
            {
                this.totCredTribDonContrField = value;
            }
        }

        /// <remarks/>
        public decimal contrPag
        {
            get
            {
                return this.contrPagField;
            }
            set
            {
                this.contrPagField = value;
            }
        }

        /// <remarks/>
        public decimal contrAsuOtrEmpl
        {
            get
            {
                return this.contrAsuOtrEmplField;
            }
            set
            {
                this.contrAsuOtrEmplField = value;
            }
        }

        /// <remarks/>
        public decimal contrRetOtrEmpl
        {
            get
            {
                return this.contrRetOtrEmplField;
            }
            set
            {
                this.contrRetOtrEmplField = value;
            }
        }

        /// <remarks/>
        public decimal contrAsuEstEmpl
        {
            get
            {
                return this.contrAsuEstEmplField;
            }
            set
            {
                this.contrAsuEstEmplField = value;
            }
        }

        /// <remarks/>
        public decimal contrRetEstEmpl
        {
            get
            {
                return this.contrRetEstEmplField;
            }
            set
            {
                this.contrRetEstEmplField = value;
            }
        }
    }
}
