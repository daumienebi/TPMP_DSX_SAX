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
            // primero comprobar si la contraseña anterior era correcto
            
            return false;
        }

        /*
            Generar el pin del usuario cuando entra por primera vez
         */
        public int generarPin()
        {
            // Podemos generar un numero aleatorio
            Random random = new Random(DateTime.Now.Millisecond);//the seed so that it doesnt generate the same set of numbers


            return -1;
        }

        /*
         Comprueba si el pin del usuario es correcto
         */
        public bool comprobarPin(int pin)
        {
            return false;
        }

        /*
          Realizar el calculo correspondiente con el PIN de usuario y devuelve el
          valor.Por ejemplo, para recuperar la contraseña, el usuario tendría que
          introducir el resultado de multiplicar el primer digito del PIN por 4 y
          el segundo por 120 y restar el último digito al resultado de esa operación.
         */
        public int obtenerResultadoCalculoPin() {
            // Maybe pass the pin to a a string then split it to get all individual values
            // before carrying on the numeric operation
            //String cadenaPin = this.pin.ToString();
            string [] digitosPin = this.pin.ToString().Split("");
            for (int i = 0; i < digitosPin.Length; i++) { 
                Console.WriteLine(digitosPin[i]);
            }
            return -1;
        }
    }



}
