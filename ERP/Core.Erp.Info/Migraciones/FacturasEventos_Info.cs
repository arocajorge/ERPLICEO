using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Migraciones
{
   public class FacturasEventos_Info
    {


        public bool seleccionado { get; set; }
        public long cod_fact { get; set; }
        public int cod_evento { get; set; }
        public string nu_ced_ruc { get; set; }
        public string nu_ced_clte { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string direccion { get; set; }
        public string telef { get; set; }
        public Nullable<decimal> cant { get; set; }
        public Nullable<decimal> v_unit { get; set; }
        public Nullable<decimal> subtotal { get; set; }
        public Nullable<decimal> v_iva { get; set; }
        public Nullable<decimal> total { get; set; }
        public string tipo_pago { get; set; }
        public string observacion { get; set; }
        public Nullable<int> bd_est { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string usuario { get; set; }
        public string lider { get; set; }
        public Nullable<int> co_tarjeta { get; set; }
        public Nullable<int> @ref { get; set; }
        public Nullable<int> lote { get; set; }
        public string recibos { get; set; }
        public Nullable<bool> aprobada_enviar_sri { get; set; }
    }
}
