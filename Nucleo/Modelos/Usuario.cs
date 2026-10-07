using System;
using System.Collections.Generic;
using System.Text;
using Nucleo.Seguridad;

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
        private int pin;

        public Usuario(string nombre, string apellidos, string email, Rol rol, bool activo, DateTime fechaCaducidad)
        {
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.rol = rol;
            this.activo = activo;
            this.fechaCaducidad = fechaCaducidad;
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
            return $"Usuario: {id} - {nombre} {apellidos} ({email})";
        }

        /* comprueba si la contraseña es correcta */
        public bool comprobarContrasena(String password) {
            // primero volver a generar el hash de la contraseña introducida utilizando
            // la sal almacenada y luego comparar el hash generado con el almacenado 
            // para ver si son iguales. Si son iguales, la contraseña es correcta, de lo contrario no lo es.
            return false;
        }

        /*
            Metodo que sirve para cambiar la contraseña del usuario
        */
        public bool cambiarContrasena(String passwordAnterior, String passwordNuevo) {
            // primero comprobar si la contraseña anterior y luego
            // generar el hash de la nueva contraseña y almacenarlo
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
