using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
        private bool TieneCredencialesConfiguradas;
        private DateTime fechaCaducidad;
        private ResultadoHash password;
        private int pin;

        // Constantes para el cálculo del PIN (definidas en la clase)
        private const int FactorPrimerDigito = 4; // Factor para el primer dígito del PIN
        private const int FactorSegundoDigito = 120; // Factor para el segundo dígito del PIN
        private const int minValuePin = 1000; // valor mínimo del PIN (4 dígitos)
        private const int maxValuePin = 10_000; // valor máximo del PIN (4 dígitos)

        public Usuario(string nombre, string apellidos, string email, Rol rol, bool activo, DateTime fechaCaducidad,string password)
        {
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.rol = rol;
            this.activo = activo;
            this.fechaCaducidad = fechaCaducidad;
            this.password = ServicioHash.Generar(password); // Generar el hash de la contraseña al crear el usuario
            if (this.password != null)
            {
                this.TieneCredencialesConfiguradas = true;
            }
            else
            {
                this.TieneCredencialesConfiguradas = false;
            }
        }

        public Usuario(String nombre, string apellidos, String email, string password) {
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            // Generar el hash de la contraseña al crear el usuario 
            this.password = ServicioHash.Generar(password);
            if(this.password != null)
            {
                this.TieneCredencialesConfiguradas = true;
            }
            else
            {
                this.TieneCredencialesConfiguradas = false;
            }
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Email { get => email; set => email = value; }
        public bool Activo { get => activo; set => activo = value; }
        public bool TieneCredencialesConfiguradas { get => TieneCredencialesConfiguradas; set => TieneCredencialesConfiguradas = value; }
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
            // No incluimos información sensible como el password o pin etc..
            string rolTexto = rol?.Nombre ?? "Sin rol";
            string nombreCompleto = $"{nombre ?? string.Empty} {apellidos ?? string.Empty}".Trim();
            string activoTexto = activo ? "Activo" : "Inactivo";
            string fechaTexto = fechaCaducidad == default ? "Sin fecha" : fechaCaducidad.ToString("yyyy-MM-dd");
            return $"Usuario: {id} - {nombreCompleto} ({email ?? "sin-email"}) | {rolTexto} | {activoTexto} | Caduca: {fechaTexto}";
        }

        /* comprueba si la contraseña es correcta */
        public bool CompruebaPassword(String password) {
            // Primero volver a generar el hash de la contraseña introducida utilizando
            // la sal almacenada y luego comparar el hash generado con el almacenado 
            // para ver si son iguales. Si son iguales, la contraseña es correcta, de lo contrario no lo es.
            return ServicioHash.Verificar(password, this.password);
        }

        /*
            Metodo que sirve para cambiar la contraseña del usuario
        */
        public bool CambiaPassword(String passwordAnterior, String passwordNuevo) {
            // Primero comprobar si la contraseña anterior y luego
            if (!CompruebaPassword(passwordAnterior)) {
                throw new Exception("La contraseña anterior no es correcta");
            }
            if (!ServicioContrasenas.EsValida(passwordNuevo)) {
                throw new Exception("La nueva contraseña no es válida");
            }
            // generar el hash de la nueva contraseña y almacenarlo
            this.password = ServicioHash.Generar(passwordNuevo);
            return true;
        }

        public 

        /*
            Generar el pin del usuario cuando entra por primera vez
         */
        public int generarPin()
        {
            // RandomNumberGenerator.GetInt32(min, max) devuelve valores >= min y < max
            int nuevoPin = RandomNumberGenerator.GetInt32(minValuePin, maxValuePin);
            this.pin = nuevoPin;
            return this.pin;
        }

        /*
         Comprueba si el pin del usuario es correcto
         */
        public bool comprobarPin(int pin)
        {
            return this.pin == pin;
        }

        /*
          Realizar el calculo correspondiente con el PIN de usuario y devuelve el
          valor.Por ejemplo, para recuperar la contraseña, el usuario tendría que
          introducir el resultado de multiplicar el primer digito del PIN por 4 y
          el segundo por 120 y restar el último digito al resultado de esa operación.
         */
        public int obtenerResultadoCalculoPin()
        {
            // El PIN en nuestra aplicación siempre se genera entre 1000 y 9999.
            if (this.pin < minValuePin || this.pin > maxValuePin)
            {
                throw new InvalidOperationException("PIN inválido: debe estar entre 1000 y 9999.");
            }
            // Convertir a string (no hay ceros a la izquierda) y validar formato
            string s = this.pin.ToString();
            if (s.Length != 4 || !char.IsDigit(s[0]) || !char.IsDigit(s[1]) || !char.IsDigit(s[3]))
            {
                throw new InvalidOperationException("Formato de PIN inválido.");
            }
            // Obtener dígitos como strings y parsear a enteros
            int primero = int.Parse(s.Substring(0, 1));
            int segundo = int.Parse(s.Substring(1, 1));
            int ultimo = int.Parse(s.Substring(3, 1));
            // Realizar el cálculo: primer dígito * 4 + segundo dígito * 120 - último dígito
            return (primero * FactorPrimerDigito) + (segundo * FactorSegundoDigito) - ultimo;
        }
    }
}
