namespace Core.Erp.Web.Reportes.CuentasPorPagar
{
    partial class CXP_012_Rpt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.p_IdEmpresa = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdRetencion = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.SubReporte_RIDE3 = new DevExpress.XtraReports.UI.XRSubreport();
            this.SubReporte_RIDE2 = new DevExpress.XtraReports.UI.XRSubreport();
            this.SubReporte_RIDE1 = new DevExpress.XtraReports.UI.XRSubreport();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Expanded = false;
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // p_IdEmpresa
            // 
            this.p_IdEmpresa.Name = "p_IdEmpresa";
            this.p_IdEmpresa.Type = typeof(int);
            this.p_IdEmpresa.ValueInfo = "0";
            this.p_IdEmpresa.Visible = false;
            // 
            // p_IdRetencion
            // 
            this.p_IdRetencion.Name = "p_IdRetencion";
            this.p_IdRetencion.Type = typeof(int);
            this.p_IdRetencion.ValueInfo = "0";
            this.p_IdRetencion.Visible = false;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.SubReporte_RIDE3,
            this.SubReporte_RIDE2,
            this.SubReporte_RIDE1});
            this.ReportHeader.HeightF = 666.66F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // SubReporte_RIDE3
            // 
            this.SubReporte_RIDE3.LocationFloat = new DevExpress.Utils.PointFloat(16.75F, 641.66F);
            this.SubReporte_RIDE3.Name = "SubReporte_RIDE3";
            this.SubReporte_RIDE3.ReportSource = new Core.Erp.Web.Reportes.CuentasPorPagar.CXP_012_Rpt_retencion();
            this.SubReporte_RIDE3.SizeF = new System.Drawing.SizeF(763.1247F, 23F);
            this.SubReporte_RIDE3.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.SubReporte_RIDE3_BeforePrint);
            // 
            // SubReporte_RIDE2
            // 
            this.SubReporte_RIDE2.LocationFloat = new DevExpress.Utils.PointFloat(16.75F, 388.87F);
            this.SubReporte_RIDE2.Name = "SubReporte_RIDE2";
            this.SubReporte_RIDE2.ReportSource = new Core.Erp.Web.Reportes.CuentasPorPagar.CXP_012_Rpt_retencion();
            this.SubReporte_RIDE2.SizeF = new System.Drawing.SizeF(763.1248F, 23F);
            this.SubReporte_RIDE2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.SubReporte_RIDE2_BeforePrint);
            // 
            // SubReporte_RIDE1
            // 
            this.SubReporte_RIDE1.LocationFloat = new DevExpress.Utils.PointFloat(16.75F, 130.86F);
            this.SubReporte_RIDE1.Name = "SubReporte_RIDE1";
            this.SubReporte_RIDE1.ReportSource = new Core.Erp.Web.Reportes.CuentasPorPagar.CXP_012_Rpt_retencion();
            this.SubReporte_RIDE1.SizeF = new System.Drawing.SizeF(763.1247F, 23F);
            this.SubReporte_RIDE1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.SubReporte_RIDE1_BeforePrint);
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(Core.Erp.Info.Reportes.CuentasPorPagar.CXP_012_Info);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // CXP_012_Rpt
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.p_IdEmpresa,
            this.p_IdRetencion});
            this.Version = "17.2";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.CXP_012_Rpt_BeforePrint_1);
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        public DevExpress.XtraReports.Parameters.Parameter p_IdEmpresa;
        public DevExpress.XtraReports.Parameters.Parameter p_IdRetencion;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRSubreport SubReporte_RIDE1;
        private DevExpress.XtraReports.UI.XRSubreport SubReporte_RIDE2;
        private DevExpress.XtraReports.UI.XRSubreport SubReporte_RIDE3;
    }
}
