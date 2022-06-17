using System;

namespace clinica.clases
{
    public class consulta:paciente
    {
        private int idConsulta;
        private bool tipoConsulta;
        private DateTime fechaConsulta;
        private string personales;
        private string familiares;
        private DateTime fur;
        private string g;
        private string pv;
        private string pc;
        private string pp;
        private string ab;
        private string nv;
        private string pa;
        private float talla;
        private float peso;
        private string hallazgosFisicos;
        private DateTime fpp;
        private string eg;
        private string au;
        private string present;
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
        private string motivoConsulta;
        private string historiaClinica;
        private string diagnostico;
        private string planTerapeutico;

        public consulta() 
        { 
        }

        public consulta(int id, string nombre, int idConsulta, bool tipoConsulta, DateTime fechaConsulta, string personales, string familiares, DateTime fur, string g, string pv, string pc, string pp, string ab, string nv, string pa, float talla, float peso, string hallazgosFisicos, DateTime fpp, string eg, string au, string present, string fcf, string mf, string rh, string hb, string vih, string vdlr, string ego, string uro, string glc, string os, string usg, string motivoConsulta, string historiaClinica, string diagnostico, string planTerapeutico)
        {
            Id = id;
            Nombre = nombre;
            this.idConsulta = idConsulta;
            this.tipoConsulta = tipoConsulta;
            this.fechaConsulta = fechaConsulta;
            this.personales = personales;
            this.familiares = familiares;
            this.fur = fur;
            this.g = g;
            this.pv = pv;
            this.pc = pc;
            this.pp = pp;
            this.ab = ab;
            this.nv = nv;
            this.pa = pa;
            this.talla = talla;
            this.peso = peso;
            this.hallazgosFisicos = hallazgosFisicos;
            this.fpp = fpp;
            this.eg = eg;
            this.au = au;
            this.present = present;
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
            this.motivoConsulta = motivoConsulta;
            this.historiaClinica = historiaClinica;
            this.diagnostico = diagnostico;
            this.planTerapeutico = planTerapeutico;
        }

        public int IdConsulta { get => idConsulta; set => idConsulta = value; }
        public bool TipoConsulta { get => tipoConsulta; set => tipoConsulta = value; }
        public DateTime FechaConsulta { get => fechaConsulta; set => fechaConsulta = value; }
        public string Personales { get => personales; set => personales = value; }
        public string Familiares { get => familiares; set => familiares = value; }
        public DateTime Fur { get => fur; set => fur = value; }
        public string G { get => g; set => g = value; }
        public string Pv { get => pv; set => pv = value; }
        public string Pc { get => pc; set => pc = value; }
        public string Pp { get => pp; set => pp = value; }
        public string Ab { get => ab; set => ab = value; }
        public string Nv { get => nv; set => nv = value; }
        public string Pa { get => pa; set => pa = value; }
        public float Talla { get => talla; set => talla = value; }
        public float Peso { get => peso; set => peso = value; }
        public string HallazgosFisicos { get => hallazgosFisicos; set => hallazgosFisicos = value; }
        public DateTime Fpp { get => fpp; set => fpp = value; }
        public string Eg { get => eg; set => eg = value; }
        public string Au { get => au; set => au = value; }
        public string Present { get => present; set => present = value; }
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
        public string MotivoConsulta { get => motivoConsulta; set => motivoConsulta = value; }
        public string HistoriaClinica { get => historiaClinica; set => historiaClinica = value; }
        public string Diagnostico { get => diagnostico; set => diagnostico = value; }
        public string PlanTerapeutico { get => planTerapeutico; set => planTerapeutico = value; }

        public string Imc
        {
            get
            {
                double resultado = Math.Round((peso / 2.2) / Math.Pow(talla, 2), 4);
                return resultado.ToString();
            }
        }
        public string Edad
        {
            get
            {
                int edad = DateTime.Today.AddTicks(-FechaNacimiento.Ticks).Year - 1;
                return edad.ToString();
            }
        }
    }
}
