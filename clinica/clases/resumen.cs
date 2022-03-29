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

        public resumen(int id, string fecha, string nombre, string dui, string motivo, string diagnostico, string plan, string examenes, string personales, string familiares, string g, string pv, string pc, string pp, string ab, string v, string p, string fur, string fpp, double talla, double peso)
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
                double resultado = (peso / 2.2) / Math.Pow(talla, 2);
                return resultado.ToString();
            }
        }
    }
}
