using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Core.Erp.Info.Contabilidad.ATS
{

  /// <comentarios/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlRootAttribute("iva", Namespace = "", IsNullable = false)]
  public partial class iva
  {

      private ivaTypeTipoIDInformante tipoIDInformanteField;

      private string idInformanteField;

      private string razonSocialField;

      private string anioField;

      private string mesField;

      private string numEstabRucField;

      private decimal totalVentasField;

      private bool totalVentasFieldSpecified;

      private codigoOperativoType codigoOperativoField;

      private List<detalleCompras> comprasField;

      private List<detalleVentas> ventasField;

      private List<ventaEst> ventasEstablecimientoField;

      private List<detalleExportacionesType> exportacionesField;

      private List<detalleRecapType> recapField;

      private List<detalleFideicomisosType> fideicomisosField;

      private List<detalleAnulados> anuladosField;

      private List<detalleRendFinancierosType> rendFinancierosField;

      /// <comentarios/>
      public ivaTypeTipoIDInformante TipoIDInformante
      {
          get
          {
              return this.tipoIDInformanteField;
          }
          set
          {
              this.tipoIDInformanteField = value;
          }
      }

      /// <comentarios/>
      public string IdInformante
      {
          get
          {
              return this.idInformanteField;
          }
          set
          {
              this.idInformanteField = value;
          }
      }

      /// <comentarios/>
      public string razonSocial
      {
          get
          {
              return this.razonSocialField;
          }
          set
          {
              this.razonSocialField = value;
          }
      }

      /// <comentarios/>
      [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
      public string Anio
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

      /// <comentarios/>
      public string Mes
      {
          get
          {
              return this.mesField;
          }
          set
          {
              this.mesField = value;
          }
      }

      /// <comentarios/>
      [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
      public string numEstabRuc
      {
          get
          {
              return this.numEstabRucField;
          }
          set
          {
              this.numEstabRucField = value;
          }
      }

      /// <comentarios/>
      public decimal totalVentas
      {
          get
          {
              return this.totalVentasField;
          }
          set
          {
              this.totalVentasField = value;
          }
      }

      /// <comentarios/>
      [System.Xml.Serialization.XmlIgnoreAttribute()]
      public bool totalVentasSpecified
      {
          get
          {
              return this.totalVentasFieldSpecified;
          }
          set
          {
              this.totalVentasFieldSpecified = value;
          }
      }

      /// <comentarios/>
      public codigoOperativoType codigoOperativo
      {
          get
          {
              return this.codigoOperativoField;
          }
          set
          {
              this.codigoOperativoField = value;
          }
      }

      /// <comentarios/>
      [System.Xml.Serialization.XmlArrayItemAttribute("detalleCompras", IsNullable = false)]
      public List<detalleCompras> compras
      {
          get
          {
              return this.comprasField;
          }
          set
          {
              this.comprasField = value;
          }
      }

      /// <comentarios/>
      [System.Xml.Serialization.XmlArrayItemAttribute("detalleVentas", IsNullable = false)]
      public List<detalleVentas> ventas
      {
          get
          {
              return this.ventasField;
          }
          set
          {
              this.ventasField = value;
          }
      }

      /// <comentarios/>
      [System.Xml.Serialization.XmlArrayItemAttribute("ventaEst", IsNullable = false)]
      public List<ventaEst> ventasEstablecimiento
      {
          get
          {
              return this.ventasEstablecimientoField;
          }
          set
          {
              this.ventasEstablecimientoField = value;
          }
      }

      /// <comentarios/>
      [System.Xml.Serialization.XmlArrayItemAttribute("detalleExportaciones", IsNullable = false)]
      public List<detalleExportacionesType> exportaciones
      {
          get
          {
              return this.exportacionesField;
          }
          set
          {
              this.exportacionesField = value;
          }
      }

      /// <comentarios/>
      [System.Xml.Serialization.XmlArrayItemAttribute("detalleRecap", IsNullable = false)]
      public List<detalleRecapType> recap
      {
          get
          {
              return this.recapField;
          }
          set
          {
              this.recapField = value;
          }
      }

      /// <comentarios/>
      [System.Xml.Serialization.XmlArrayItemAttribute("detalleFideicomisos", IsNullable = false)]
      public List<detalleFideicomisosType> fideicomisos
      {
          get
          {
              return this.fideicomisosField;
          }
          set
          {
              this.fideicomisosField = value;
          }
      }

      /// <comentarios/>
      [System.Xml.Serialization.XmlArrayItemAttribute("detalleAnulados", IsNullable = false)]
      public List<detalleAnulados> anulados
      {
          get
          {
              return this.anuladosField;
          }
          set
          {
              this.anuladosField = value;
          }
      }

      /// <comentarios/>
      [System.Xml.Serialization.XmlArrayItemAttribute("detalleRendFinancieros", IsNullable = false)]
      public List<detalleRendFinancierosType> rendFinancieros
      {
          get
          {
              return this.rendFinancierosField;
          }
          set
          {
              this.rendFinancierosField = value;
          }
      }
  }


}
