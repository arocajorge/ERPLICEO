using Core.Erp.Bus.FacturacionElectronica;
using Core.Erp.Bus.General;
using Core.Erp.Info.FacturacionElectronica;
using Core.Erp.Info.FacturacionElectronica.Factura_V2;
using Core.Erp.Web.Reportes.Facturacion;
using DevExpress.XtraPrinting;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Timers;
using System.Xml.Serialization;

namespace Core.Erp.WindowsService
{
    public partial class ImpresionDirecta : ServiceBase
    {
        public ImpresionDirecta()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = TimeSpan.FromSeconds(3).TotalMilliseconds;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Serv_ImpresionDirecta();
        }

        protected override void OnStop()
        {
        }


        public void Serv_ImpresionDirecta()
        {
            #region Variables
            tb_sis_reporte_x_tb_empresa_Bus bus_rep_x_emp = new tb_sis_reporte_x_tb_empresa_Bus();
            tb_ColaImpresionDirecta_Bus bus_colaImpresion = new tb_ColaImpresionDirecta_Bus();
            string RootReporte = System.IO.Path.GetTempPath() + "Rpt_Facturacion.repx";
            string IPLocal = string.Empty;
            int IdSucursal = 0;
            int IdBodega = 0;
            decimal IdCbteVta = 0;
            #endregion

            #region GetIPAdress

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    IPLocal = ip.ToString();
                }
            }
            #endregion

            #region GetImpresion
            var Impresion = bus_colaImpresion.GetInfoPorImprimir(IPLocal);
            if (Impresion == null)
                return;

            bus_colaImpresion.ModificarDB(Impresion);
            #endregion

            try
            {
                var reporte = bus_rep_x_emp.GetInfo(Impresion.IdEmpresa, Impresion.CodReporte);
                if (!string.IsNullOrEmpty(Impresion.Parametros))
                {
                    string[] array = Impresion.Parametros.Split(',');
                    if (array.Count() > 2)
                    {
                        IdSucursal = Convert.ToInt32(array[0]);
                        IdBodega = Convert.ToInt32(array[1]);
                        IdCbteVta = Convert.ToDecimal(array[2]);
                    }
                }
                #region 


                switch (Impresion.CodReporte)
                {
                    case "FAC_003":
                        FAC_003_Rpt RPT_003 = new FAC_003_Rpt();

                        #region Cargo diseño desde base                        
                        if (reporte != null)
                        {
                            System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                            RPT_003.LoadLayout(RootReporte);
                        }
                        #endregion

                        #region Parametros
                        if (!string.IsNullOrEmpty(Impresion.Parametros))
                        {
                            RPT_003.p_IdEmpresa.Value = Impresion.IdEmpresa;
                            RPT_003.p_IdBodega.Value = IdBodega;
                            RPT_003.p_IdSucursal.Value = IdSucursal;
                            RPT_003.p_IdCbteVta.Value = IdCbteVta;
                            RPT_003.p_mostrar_cuotas.Value = false;
                            RPT_003.PrinterName = Impresion.IPImpresora;
                            RPT_003.CreateDocument();
                        }
                        #endregion

                        PrintToolBase tool003 = new PrintToolBase(RPT_003.PrintingSystem);
                        for (int i = 0; i < Impresion.NumCopias; i++)
                        {
                            if (string.IsNullOrEmpty(Impresion.IPImpresora))
                                tool003.Print();
                            else
                                tool003.Print(Impresion.IPImpresora);

                        }
                        break;
                    case "FAC_013":
                        FAC_013_Rpt RPT_013 = new FAC_013_Rpt();

                        #region Cargo diseño desde base                        
                        if (reporte != null)
                        {
                            System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                            RPT_013.LoadLayout(RootReporte);
                        }
                        #endregion

                        #region Parametros
                        if (!string.IsNullOrEmpty(Impresion.Parametros))
                        {
                            RPT_013.p_IdEmpresa.Value = Impresion.IdEmpresa;
                            RPT_013.p_IdBodega.Value = IdBodega;
                            RPT_013.p_IdSucursal.Value = IdSucursal;
                            RPT_013.p_IdCbteVta.Value = IdCbteVta;
                            RPT_013.PrinterName = Impresion.IPImpresora;
                            RPT_013.CreateDocument();
                        }
                        #endregion
                        PrintToolBase tool013 = new PrintToolBase(RPT_013.PrintingSystem);
                        for (int i = 0; i < Impresion.NumCopias; i++)
                        {
                            if (string.IsNullOrEmpty(Impresion.IPImpresora))
                                tool013.Print();
                            else
                                tool013.Print(Impresion.IPImpresora);

                        }
                        break;
                }
                #endregion
                bus_colaImpresion.ModificarDB(Impresion);
            }
            catch (Exception ex)
            {
                Impresion.Comentario = ex.Message.ToString();
                bus_colaImpresion.ModificarDB(Impresion);
            }
        }

        public void Serv_GeneradorDocumentosElectronicos()
        {
            try
            {

                
                #region Variables
                TipoComprobante_Bus bus_comprobante = new TipoComprobante_Bus();
                TipoComprobante_Info info_tipo_comprobante = new TipoComprobante_Info();
                info_tipo_comprobante = bus_comprobante.get_info_comprobante_a_generar();
                StreamWriter myWriter;
                #endregion


                switch (info_tipo_comprobante.TipoDocumento)
                {
                    
                    case Info.Helps.cl_enumeradores.eTipoDocumento.FACT:
                        #region Generando facturas
                        var info_factura = bus_comprobante.get_info_factura(info_tipo_comprobante);
                        if (info_factura != null && info_factura.infoTributaria.secuencial != null)
                        {

                            string sIdCbteFact = info_factura.infoTributaria.razonSocial.Substring(0, 3) + "FAC" + info_factura.infoTributaria.estab + "-" + info_factura.infoTributaria.ptoEmi + "-" + info_factura.infoTributaria.secuencial;
                            XmlSerializerNamespaces NamespaceObject = new XmlSerializerNamespaces();
                            NamespaceObject.Add("", "");
                            XmlSerializer mySerializer = new XmlSerializer(typeof(factura));
                            myWriter = new StreamWriter(@"C:\Comprobantes EFIXED\Repositorio de Comprobantes" + sIdCbteFact + ".xml");
                            mySerializer.Serialize(myWriter, info_factura, NamespaceObject);
                            myWriter.Close();
                        }

                        #endregion

                        break;
                    case Info.Helps.cl_enumeradores.eTipoDocumento.GUIA:

                        #region Generando guia remision
                        var info_guia = bus_comprobante.get_info_factura(info_tipo_comprobante);
                        if (info_guia != null && info_guia.infoTributaria.secuencial != null)
                        {

                            string sIdCbteFact = info_guia.infoTributaria.razonSocial.Substring(0, 3) + "GUI" + info_guia.infoTributaria.estab + "-" + info_guia.infoTributaria.ptoEmi + "-" + info_guia.infoTributaria.secuencial;
                            XmlSerializerNamespaces NamespaceObject = new XmlSerializerNamespaces();
                            NamespaceObject.Add("", "");
                            XmlSerializer mySerializer = new XmlSerializer(typeof(factura));
                            myWriter = new StreamWriter(@"C:\Comprobantes EFIXED\Repositorio de Comprobantes" + sIdCbteFact + ".xml");
                            mySerializer.Serialize(myWriter, info_guia, NamespaceObject);
                            myWriter.Close();
                        }

                        #endregion
                        break;
                    case Info.Helps.cl_enumeradores.eTipoDocumento.NTCR:

                        #region Generando nota credito
                        var info_nc = bus_comprobante.get_info_factura(info_tipo_comprobante);
                        if (info_nc != null && info_nc.infoTributaria.secuencial != null)
                        {

                            string sIdCbteFact = info_nc.infoTributaria.razonSocial.Substring(0, 3) + "NTC" + info_nc.infoTributaria.estab + "-" + info_nc.infoTributaria.ptoEmi + "-" + info_nc.infoTributaria.secuencial;
                            XmlSerializerNamespaces NamespaceObject = new XmlSerializerNamespaces();
                            NamespaceObject.Add("", "");
                            XmlSerializer mySerializer = new XmlSerializer(typeof(factura));
                            myWriter = new StreamWriter(@"C:\Comprobantes EFIXED\Repositorio de Comprobantes" + sIdCbteFact + ".xml");
                            mySerializer.Serialize(myWriter, info_nc, NamespaceObject);
                            myWriter.Close();
                        }

                        #endregion
                        break;
                    case Info.Helps.cl_enumeradores.eTipoDocumento.NTDB:


                        #region Generando nota debito
                        var info_nd = bus_comprobante.get_info_factura(info_tipo_comprobante);
                        if (info_nd != null && info_nd.infoTributaria.secuencial != null)
                        {

                            string sIdCbteFact = info_nd.infoTributaria.razonSocial.Substring(0, 3) + "NTD" + info_nd.infoTributaria.estab + "-" + info_nd.infoTributaria.ptoEmi + "-" + info_nd.infoTributaria.secuencial;
                            XmlSerializerNamespaces NamespaceObject = new XmlSerializerNamespaces();
                            NamespaceObject.Add("", "");
                            XmlSerializer mySerializer = new XmlSerializer(typeof(factura));
                            myWriter = new StreamWriter(@"C:\Comprobantes EFIXED\Repositorio de Comprobantes" + sIdCbteFact + ".xml");
                            mySerializer.Serialize(myWriter, info_nd, NamespaceObject);
                            myWriter.Close();
                        }

                        #endregion
                        break;
                    case Info.Helps.cl_enumeradores.eTipoDocumento.RETEN:

                        #region Generando retencion
                        var info_retencion = bus_comprobante.get_info_factura(info_tipo_comprobante);
                        if (info_retencion != null && info_retencion.infoTributaria.secuencial != null)
                        {

                            string sIdCbteFact = info_retencion.infoTributaria.razonSocial.Substring(0, 3) + "RET" + info_retencion.infoTributaria.estab + "-" + info_retencion.infoTributaria.ptoEmi + "-" + info_retencion.infoTributaria.secuencial;
                            XmlSerializerNamespaces NamespaceObject = new XmlSerializerNamespaces();
                            NamespaceObject.Add("", "");
                            XmlSerializer mySerializer = new XmlSerializer(typeof(factura));
                            myWriter = new StreamWriter(@"C:\Comprobantes EFIXED\Repositorio de Comprobantes" + sIdCbteFact + ".xml");
                            mySerializer.Serialize(myWriter, info_retencion, NamespaceObject);
                            myWriter.Close();
                        }

                        #endregion
                        break;
                    default:
                        break;
                }
               






            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
