﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorPagar
{
    public class CXP_011_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdSolicitud { get; set; }
        public int IdSucursal { get; set; }
        public System.DateTime Fecha { get; set; }
        public decimal IdProveedor { get; set; }
        public string Concepto { get; set; }
        public bool Estado { get; set; }
        public double Valor { get; set; }
        public string Solicitante { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string Su_Descripcion { get; set; }
        public string Nombre { get; set; }
        public string GiradoA { get; set; }
        public string num_cta_acreditacion { get; set; }
        public int IdBanco_acreditacion { get; set; } // by acueva 19/05/2021
        public string Banco { get; set; } // by acueva 19/05/2021
        public string TipoCta { get; set; } // by acueva 19/05/2021
    }
}
