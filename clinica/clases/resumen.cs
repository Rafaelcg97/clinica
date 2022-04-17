using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinica.clases
{
    public class resumen
    {
        private int id;
        private string fecha;
        private string nombre;
        private string dui;
        private string motivo;
        private string diagnostico;
        private string plan;
        private string examenes;
        private string personales;
        private string familiares;
        private string g;
        private string pv;
        private string pc;
        private string pp;
        private string ab;
        private string v;
        private string p;
        private string fur;
        private string fpp;
        private double talla;
        private double peso;
        private DateTime nacimiento;
        private string hallazgos;
        private string eg;
        private string au;
        private string presentacion;
        private string fcf;
        private string mf;
        private string rh;
        private string hb;
        private string vih;
        private string vdlr;
        private string ego;
        private string uro;
        private string glc;
        private string os;
        private string usg;
        private bool seleccionado;

        public resumen(string nombre, DateTime nacimiento, string fur, string fpp, string g, string pv, string pc, string pp, string ab, string v, string familiares, string personales, double peso, double talla)
        {
            this.nombre = nombre;
            this.nacimiento = nacimiento;
            this.fur = fur;
            this.fpp = fpp;
            this.g = g;
            this.pv = pv;
            this.pc = pc;
            this.pp = pp;
            this.ab = ab;
            this.v = v;
            this.familiares = familiares;
            this.personales = personales;
            this.peso = peso;
            this.talla = talla;
        }

        public resumen(string fecha, double peso, string eg, string p, string au, string presentacion, string fcf, string mf)
        {
            this.fecha = fecha;
            this.peso = peso;
            this.eg = eg;
            this.p = p;
            this.au = au;
            this.presentacion = presentacion;
            this.fcf = fcf;
            this.mf = mf;

        }

        public resumen(int id, string fecha, string nombre, string dui, string motivo, string diagnostico, string plan, string examenes, string personales, string familiares, string g, string pv, string pc, string pp, string ab, string v, string p, string fur, string fpp, double talla, double peso, DateTime nacimiento, string hallazgos, string eg, string au, string presentacion, string fcf, string mf, string rh, string hb, string vih, string vdlr, string ego, string uro, string glc, string os, string usg, bool seleccionado)
        {
            this.id = id;
            this.fecha = fecha;
            this.nombre = nombre;
            this.dui = dui;
            this.motivo = motivo;
            this.diagnostico = diagnostico;
            this.plan = plan;
            this.examenes = examenes;
            this.personales = personales;
            this.familiares = familiares;
            this.g = g;
            this.pv = pv;
            this.pc = pc;
            this.pp = pp;
            this.ab = ab;
            this.v = v;
            this.p = p;
            this.fur = fur;
            this.fpp = fpp;
            this.talla = talla;
            this.peso = peso;
            this.nacimiento = nacimiento;
            this.hallazgos = hallazgos;
            this.eg = eg;
            this.au = au;
            this.presentacion = presentacion;
            this.fcf = fcf;
            this.mf = mf;
            this.rh = rh;
            this.hb = hb;
            this.vih = vih;
            this.vdlr = vdlr;
            this.ego = ego;
            this.uro = uro;
            this.glc = glc;
            this.os = os;
            this.usg = usg;
            this.seleccionado = seleccionado;
        }

        public int Id { get => id; set => id = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Dui { get => dui; set => dui = value; }
        public string Motivo { get => motivo; set => motivo = value; }
        public string Diagnostico { get => diagnostico; set => diagnostico = value; }
        public string Plan { get => plan; set => plan = value; }
        public string Examenes { get => examenes; set => examenes = value; }
        public string Personales { get => personales; set => personales = value; }
        public string Familiares { get => familiares; set => familiares = value; }
        public string G { get => g; set => g = value; }
        public string Pv { get => pv; set => pv = value; }
        public string Pc { get => pc; set => pc = value; }
        public string Pp { get => pp; set => pp = value; }
        public string Ab { get => ab; set => ab = value; }
        public string V { get => v; set => v = value; }
        public string P { get => p; set => p = value; }
        public string Fur { get => fur; set => fur = value; }
        public string Fpp { get => fpp; set => fpp = value; }
        public double Talla { get => talla; set => talla = value; }
        public double Peso { get => peso; set => peso = value; }
        public string Imc
        {
            get
            {
                double resultado = Math.Round((peso / 2.2) / Math.Pow(talla, 2),4);
                return resultado.ToString();
            }
        }
        public string Edad
        {
            get
            {
                int edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
                return edad.ToString();
            }
        }

        public DateTime Nacimiento { get => nacimiento; set => nacimiento = value; }
        public string Hallazgos { get => hallazgos; set => hallazgos = value; }
        public string Eg { get => eg; set => eg = value; }
        public string Au { get => au; set => au = value; }
        public string Presentacion { get => presentacion; set => presentacion = value; }
        public string Fcf { get => fcf; set => fcf = value; }
        public string Mf { get => mf; set => mf = value; }
        public string Rh { get => rh; set => rh = value; }
        public string Hb { get => hb; set => hb = value; }
        public string Vih { get => vih; set => vih = value; }
        public string Vdlr { get => vdlr; set => vdlr = value; }
        public string Ego { get => ego; set => ego = value; }
        public string Uro { get => uro; set => uro = value; }
        public string Glc { get => glc; set => glc = value; }
        public string Os { get => os; set => os = value; }
        public string Usg { get => usg; set => usg = value; }
        public bool Seleccionado { get => seleccionado; set => seleccionado = value; }
    }
}
