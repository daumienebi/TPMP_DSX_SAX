using System;
using System.Collections.Generic;
using System.Text;

namespace Nucleo.Modelos
{
    public class Proyecto
    {
        private int id = -1;
        private string nombre;
        private string prefijo;
        private string descripcion;

        private bool esActivo;

        private bool esPublico;

        public Proyecto(string nombre, string prefijo, string descripcion, bool esActivo, bool esPublico)
        {
            this.nombre = nombre;
            this.prefijo = prefijo;
            this.descripcion = descripcion;
            this.esActivo = esActivo;
            this.esPublico = esPublico;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Prefijo { get => prefijo; set => prefijo = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public bool EsActivo { get => esActivo; set => esActivo = value; }
        public bool EsPublico { get => esPublico; set => esPublico = value; }

        public override bool Equals(object? obj)
        {
            return obj is Proyecto proyecto &&
                   nombre == proyecto.nombre &&
                   prefijo == proyecto.prefijo &&
                   descripcion == proyecto.descripcion &&
                   esActivo == proyecto.esActivo &&
                   esPublico == proyecto.esPublico;
        }


        // disponibilidad?
        //requisitos?
    }
}
