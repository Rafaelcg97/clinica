using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinica.clases
{
    public class aviso
    {
        private int id;
        private string notificacion;

        public aviso(int id, string notificacion)
        {
            this.id = id;
            this.notificacion = notificacion;
        }

        public int Id { get => id; set => id = value; }
        public string Notificacion { get => notificacion; set => notificacion = value; }
    }
}
