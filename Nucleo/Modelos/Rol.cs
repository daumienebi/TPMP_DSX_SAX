using System;
using System.Collections.Generic;
using System.Text;

namespace Nucleo.Modelos
{
    public class Rol
    {
        private int id = -1;
        private string nombre;
        private string descripcion;
        
        // Implementar ACL para los permisis
        // no hacerlos como lo maneja testlink

        public Rol(int id, string nombre, string descripcion)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
        }

        public override bool Equals(object? obj)
        {
            return obj is Rol rol &&
                   id == rol.id &&
                   nombre == rol.nombre &&
                   descripcion == rol.descripcion;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}
