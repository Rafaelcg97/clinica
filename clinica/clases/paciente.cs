using System;

namespace clinica.clases
{
    public class paciente
    {
        private int id;
        private string nombre;
        private string dui;
        private DateTime fechaNacimiento;
        private string telefono;
        private string correo;

        public paciente()
        {

        }

        public paciente(int id, string nombre, string dui, DateTime fechaNacimiento, string telefono, string correo)
        {
            this.id = id;
            this.nombre = nombre;
            this.dui = dui;
            this.fechaNacimiento = fechaNacimiento;
            this.telefono = telefono;
            this.correo = correo;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Dui { get => dui; set => dui = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Correo { get => correo; set => correo = value; }
    }
}
