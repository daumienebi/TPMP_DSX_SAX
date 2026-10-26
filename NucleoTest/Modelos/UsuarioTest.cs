using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nucleo.Modelos;
using System;

namespace NucleoTest.Modelos
{
    [TestClass]
    public class UsuarioTest
    {
        private Rol _rolPrueba = null!;

        [TestInitialize]
        public void SetUp()
        {
            _rolPrueba = new Rol("Tester", "Rol para pruebas");
            _rolPrueba.Id = 1;
        }

        [TestMethod]
        public void Constructor_InicializaPropiedadesCorrectamente()
        {
            DateTime fechaCad = DateTime.Now.AddDays(30);
            Usuario usuario = new Usuario("Ana", "López", "ana@test.com", _rolPrueba, true, fechaCad, "Password123!");

            Assert.AreEqual("Ana", usuario.Nombre);
            Assert.AreEqual("López", usuario.Apellidos);
            Assert.AreEqual("ana@test.com", usuario.Email);
            Assert.AreEqual(_rolPrueba, usuario.Rol);
            Assert.IsTrue(usuario.Activo);
            Assert.IsTrue(usuario.TieneCredencialesConfiguradas);
            Assert.AreEqual(fechaCad, usuario.FechaCaducidad);
            Assert.IsTrue(usuario.Pin >= 1000 && usuario.Pin <= 9999, "El PIN debe tener 4 dígitos.");
        }

        [TestMethod]
        public void CompruebaPassword_PasswordCorrecto_RetornaTrue()
        {
            Usuario usuario = new Usuario("Ana", "López", "ana@test.com", _rolPrueba, true, DateTime.Now.AddDays(30), "MiClave123!");

            Assert.IsTrue(usuario.CompruebaPassword("MiClave123!"));
        }

        [TestMethod]
        public void CompruebaPassword_PasswordIncorrecto_RetornaFalse()
        {
            Usuario usuario = new Usuario("Ana", "López", "ana@test.com", _rolPrueba, true, DateTime.Now.AddDays(30), "MiClave123!");

            Assert.IsFalse(usuario.CompruebaPassword("ClaveErronea1!"));
        }

        [TestMethod]
        public void CambiaPassword_ConPasswordAnteriorCorrectoYNuevoValido_CambiaPassword()
        {
            Usuario usuario = new Usuario("Ana", "López", "ana@test.com", _rolPrueba, true, DateTime.Now.AddDays(30), "PasswordAnterior1!");

            bool resultado = usuario.CambiaPassword("PasswordAnterior1!", "NuevoPassword123!");

            Assert.IsTrue(resultado);
            Assert.IsTrue(usuario.CompruebaPassword("NuevoPassword123!"));
            Assert.IsFalse(usuario.CompruebaPassword("PasswordAnterior1!"));
        }

        [TestMethod]
        public void CambiaPassword_ConPasswordAnteriorIncorrecto_LanzaExcepcion()
        {
            Usuario usuario = new Usuario("Ana", "López", "ana@test.com", _rolPrueba, true, DateTime.Now.AddDays(30), "PasswordAnterior1!");

            Assert.Throws<Exception>(() => usuario.CambiaPassword("PasswordMalo1!", "NuevoPassword123!"));
        }

        [TestMethod]
        public void CambiaPassword_ConNuevoPasswordInvalido_LanzaExcepcion()
        {
            Usuario usuario = new Usuario("Ana", "López", "ana@test.com", _rolPrueba, true, DateTime.Now.AddDays(30), "PasswordAnterior1!");

            // Invalido: menos de 8 caracteres o sin caracteres especiales
            Assert.Throws<Exception>(() => usuario.CambiaPassword("PasswordAnterior1!", "corta"));
        }

        [TestMethod]
        public void CambiaPassword_SinPasswordAnterior_ConPasswordValido_CambiaPassword()
        {
            Usuario usuario = new Usuario("Ana", "López", "ana@test.com", _rolPrueba, true, DateTime.Now.AddDays(30), "PasswordAnterior1!");

            bool resultado = usuario.CambiaPassword("SuperSegura99!");

            Assert.IsTrue(resultado);
            Assert.IsTrue(usuario.CompruebaPassword("SuperSegura99!"));
            Assert.IsTrue(usuario.TieneCredencialesConfiguradas);
        }

        [TestMethod]
        public void CambiaPassword_SinPasswordAnterior_ConPasswordInvalido_LanzaExcepcion()
        {
            Usuario usuario = new Usuario("Ana", "López", "ana@test.com", _rolPrueba, true, DateTime.Now.AddDays(30), "PasswordAnterior1!");

            Assert.Throws<Exception>(() => usuario.CambiaPassword("simple"));
        }

        [TestMethod]
        public void GenerarPin_GeneraPinEntre1000Y9999()
        {
            Usuario usuario = new Usuario("Ana", "López", "ana@test.com", _rolPrueba, true, DateTime.Now.AddDays(30), "Password123!");

            int nuevoPin = usuario.generarPin();

            Assert.IsTrue(nuevoPin >= 1000 && nuevoPin <= 9999);
            Assert.AreEqual(nuevoPin, usuario.Pin);
        }

        [TestMethod]
        public void ComprobarPin_PinCorrecto_RetornaTrue()
        {
            Usuario usuario = new Usuario("Ana", "López", "ana@test.com", _rolPrueba, true, DateTime.Now.AddDays(30), "Password123!");

            Assert.IsTrue(usuario.comprobarPin(usuario.Pin));
        }

        [TestMethod]
        public void ComprobarPin_PinIncorrecto_RetornaFalse()
        {
            Usuario usuario = new Usuario("Ana", "López", "ana@test.com", _rolPrueba, true, DateTime.Now.AddDays(30), "Password123!");

            int pinErroneo = (usuario.Pin == 1234) ? 4321 : 1234;
            Assert.IsFalse(usuario.comprobarPin(pinErroneo));
        }

        [TestMethod]
        public void ObtenerResultadoCalculoPin_CalculoCoincideConFormula()
        {
            Usuario usuario = new Usuario("Ana", "López", "ana@test.com", _rolPrueba, true, DateTime.Now.AddDays(30), "Password123!");

            string pinStr = usuario.Pin.ToString();
            int primero = int.Parse(pinStr.Substring(0, 1));
            int segundo = int.Parse(pinStr.Substring(1, 1));
            int ultimo = int.Parse(pinStr.Substring(3, 1));
            int esperado = (primero * 4) + (segundo * 120) - ultimo;

            int resultado = usuario.obtenerResultadoCalculoPin();

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void Equals_MismoUsuario_RetornaTrue()
        {
            DateTime fecha = new DateTime(2026, 12, 31);
            Usuario u1 = new Usuario("Carlos", "Gómez", "carlos@test.com", _rolPrueba, true, fecha, "Clave123!");
            u1.Id = 5;

            Usuario u2 = new Usuario("Carlos", "Gómez", "carlos@test.com", _rolPrueba, true, fecha, "OtraClave!");
            u2.Id = 5;
            // Para ser iguales según Equals de Usuario, deben coincidir id, nombre, apellidos, email, rol, activo, fechaCaducidad, pin, tieneCredencialesConfiguradas
            // Asignamos el mismo PIN para comparar igualdad de campos del modelo
            while (u2.Pin != u1.Pin)
            {
                u2.generarPin();
            }

            Assert.IsTrue(u1.Equals(u2));
        }

        [TestMethod]
        public void Equals_DiferenteEmail_RetornaFalse()
        {
            DateTime fecha = new DateTime(2026, 12, 31);
            Usuario u1 = new Usuario("Carlos", "Gómez", "carlos@test.com", _rolPrueba, true, fecha, "Clave123!");
            Usuario u2 = new Usuario("Carlos", "Gómez", "distinto@test.com", _rolPrueba, true, fecha, "Clave123!");

            Assert.IsFalse(u1.Equals(u2));
        }

        [TestMethod]
        public void Equals_Null_RetornaFalse()
        {
            Usuario u1 = new Usuario("Carlos", "Gómez", "carlos@test.com", _rolPrueba, true, DateTime.Now, "Clave123!");

            Assert.IsFalse(u1.Equals(null));
        }

        [TestMethod]
        public void ToString_ContieneNombreYEmail()
        {
            Usuario usuario = new Usuario("Lucía", "Méndez", "lucia@test.com", _rolPrueba, true, DateTime.Now, "Clave123!");
            usuario.Id = 10;

            string texto = usuario.ToString();

            StringAssert.Contains(texto, "Lucía");
            StringAssert.Contains(texto, "Méndez");
            StringAssert.Contains(texto, "lucia@test.com");
        }
    }
}
