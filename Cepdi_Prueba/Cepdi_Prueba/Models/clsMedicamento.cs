using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cepdi_Prueba.Models
{
    public class clsMedicamento
    {
        public int IdMedicamento { get; set; }

        public string Nombre { get; set; }

        public string Concentracion { get; set; }

        public string Precio { get; set; }

        public string Presentacion { get; set; }

        public string Habilitado { get; set; }

        public string Acciones { get; set; }
        public int IdFormaFarmaucetica { get; set; }
      }

    }
