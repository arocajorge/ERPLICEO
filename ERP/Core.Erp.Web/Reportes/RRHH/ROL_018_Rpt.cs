﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.RRHH
{
    public partial class ROL_018_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ROL_018_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_018_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdEmpleado = p_IdEmpleado.Value== null ? 0 : Convert.ToDecimal(p_IdEmpleado.Value);
            DateTime fecha_ini = p_fecha_ini.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime fecha_fin = p_fecha_fin.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_fin.Value);

            ROL_018_Bus bus_rpt = new ROL_018_Bus();
            List<ROL_018_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdEmpleado, fecha_ini, fecha_fin);
            this.DataSource = lst_rpt;
        }
    }
}
