using Nucleo.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nucleo
{
    class App
    {
        static void main(String [] args)
        {
            Rol rol = new Rol("Admin", "Admin caca");
            Usuario user = new Usuario("Derick", "Sakpa", "a@a.com", rol, true, DateTime.Now);
            Console.WriteLine(user.ToString());
            user.obtenerResultadoCalculoPin();
        }
    }
}
