using System;
using System.Collections.Generic;
using System.Text;

namespace Nucleo.Seguridad.Servicios
{
    public class Formato
    {
        public static String FormatearFecha(DateTime fecha)
        {
            if (fecha == null) return "no consta";
            else
            {
                return fecha.ToString("dd/MM/yyyy");
            }
        }
    }
}
