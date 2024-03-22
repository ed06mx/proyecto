using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cepdi_Prueba.Models
{
    public class clsUsuario
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; }      

        public string Fecha_Creacion { get; set; }

        public string Usuario { get; set; }

        public string Password { get; set; }

        public string Estatus { get; set; }

        public string Acciones { get; set; }

    }
}