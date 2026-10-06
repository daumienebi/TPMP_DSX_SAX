using System;
using System.Collections.Generic;
using System.Text;
using TestProjectManagementPlatform.Nucleo.Seguridad;

namespace Nucleo.Modelos
{
    public class Usuario
    {
        private int id = -1;
        private String nombre;
        private String apellidos;
        private String email;
        private Rol rol;
        private bool activo;
        private DateTime fechaCaducidad;
        private ResultadoHash password;

        private int pin; // Hay que cifrarlo?

        public Usuario(string nombre, string apellidos, string email, Rol rol, bool activo, DateTime fechaCaducidad)
        {
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.rol = rol;
            this.activo = activo;
            this.fechaCaducidad = fechaCaducidad;
            //password?
            //pin?
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Email { get => email; set => email = value; }
        public bool Activo { get => activo; set => activo = value; }
        public DateTime FechaCaducidad { get => fechaCaducidad; set => fechaCaducidad = value; }
        public Rol Rol { get => rol; set => rol = value; }

        public override bool Equals(object? obj)
        {
            return obj is Usuario usuario &&
                   id == usuario.id &&
                   nombre == usuario.nombre &&
                   apellidos == usuario.apellidos &&
                   email == usuario.email &&
                   EqualityComparer<Rol>.Default.Equals(rol, usuario.rol) &&
                   activo == usuario.activo &&
                   fechaCaducidad == usuario.fechaCaducidad;
        }

        public override string ToString()
        {
            return this.id + " " + this.nombre + " " + this.apellidos + " " + this.email;
        }

        /* commentario*/
        public bool comprobarContrasena(ResultadoHash password) {
            return false;
        }

        /*
            Metodo que sirve para cambiar la contraseña del usuario
        */
        public bool cambiarContrasena(ResultadoHash passwordAnterior, ResultadoHash passwordNuevo) {
            return false;
        }

    }



}
