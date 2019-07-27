using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using Core.Erp.Info.Reportes.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Bus.Reportes.Contabilidad;

namespace Core.Erp.Web.Reportes.Contabilidad
{
    public partial class CONTA_004_ER_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public int[] IntArray { get; set; }
        List<CONTA_004_Info> lst_rpt = new List<CONTA_004_Info>();
        public CONTA_004_ER_Rpt()
        {
            InitializeComponent();
        }

        private void CONTA_004_ER_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {


            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdAnio = string.IsNullOrEmpty(p_IdAnio.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAnio.Value);
            string IdUsuario = string.IsNullOrEmpty(p_IdUsuario.Value.ToString()) ? "" : Convert.ToString(p_IdUsuario.Value);
            int IdNivel = string.IsNullOrEmpty(p_IdNivel.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNivel.Value);
            int IdPeriodoIni = string.IsNullOrEmpty(p_IdPeriodoIni.Value.ToString()) ? 0 : Convert.ToInt32(p_IdPeriodoIni.Value);
            int IdPeriodoFin = string.IsNullOrEmpty(p_IdPeriodoFin.Value.ToString()) ? 0 : Convert.ToInt32(p_IdPeriodoFin.Value);
            bool mostrarSaldo0 = string.IsNullOrEmpty(p_mostrarSaldo0.Value.ToString()) ? false : Convert.ToBoolean(p_mostrarSaldo0.Value);
            string balance = string.IsNullOrEmpty(p_balance.Value.ToString()) ? "" : Convert.ToString(p_balance.Value);
            bool mostrarAcumulado = string.IsNullOrEmpty(p_mostrarAcumulado.Value.ToString()) ? false : Convert.ToBoolean(p_mostrarAcumulado.Value);

            CONTA_004_Bus bus_rpt = new CONTA_004_Bus();
            string Sucursal = "";

            tb_FiltroReportes_Bus bus_filtro = new tb_FiltroReportes_Bus();
            Sucursal = bus_filtro.GuardarDB(IdEmpresa, IntArray, IdUsuario);

            lst_rpt.AddRange(bus_rpt.GetList(IdEmpresa, IdAnio, IdPeriodoIni,IdPeriodoFin, mostrarSaldo0, IdUsuario, IdNivel, mostrarAcumulado, balance));
            lst_rpt.ForEach(q => q.Su_Descripcion = Sucursal);
            this.DataSource = lst_rpt;

            lblPeriodoIni.Text = IdPeriodoIni.ToString().Length == 6 ? new DateTime(Convert.ToInt32(IdPeriodoIni.ToString().Substring(0, 4)), Convert.ToInt32(IdPeriodoIni.ToString().Substring(4, 2)), 1).ToString("MMMM yyyy") : "";
            lblPeriodoFin.Text = IdPeriodoFin.ToString().Length == 6 ? new DateTime(Convert.ToInt32(IdPeriodoFin.ToString().Substring(0, 4)), Convert.ToInt32(IdPeriodoFin.ToString().Substring(4, 2)), 1).ToString("MMMM yyyy") : "";

            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            if (emp != null)
            {
                lblDireccion.Text = emp.em_direccion;
                lblTelefono.Text = string.IsNullOrEmpty(emp.em_telefonos) ? "" : "Tel. " + emp.em_telefonos;
                if (emp.em_logo != null)
                {
                    ImageConverter obj = new ImageConverter();
                    lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
                }
            }
        }
    }
}
