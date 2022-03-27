using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinica.clases
{
    public class paciente
    {
        private string nombre;
        private string dui;
        private string fechaNacimiento;
        private string telefono;
        private string correo;

        public paciente(string nombre, string dui, string fechaNacimiento, string telefono, string correo)
        {
            this.nombre = nombre;
            this.dui = dui;
            this.fechaNacimiento = fechaNacimiento;
            this.telefono = telefono;
            this.correo = correo;
        }
    }
}
