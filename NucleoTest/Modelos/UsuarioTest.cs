using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nucleo.Modelos;
using System;
using System.Reflection;

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
        public void Propiedades_ModificanValoresCorrectamente()
        {
            DateTime fechaCad = DateTime.Now.AddDays(30);
            Usuario usuario = new Usuario("Ana", "López", "ana@test.com", _rolPrueba, true, fechaCad, "Password123!");
            Rol nuevoRol = new Rol("Editor", "Rol editor") { Id = 2 };
            DateTime nuevaFecha = fechaCad.AddDays(30);

            usuario.Id = 42;
            usuario.Nombre = "Beatriz";
            usuario.Apellidos = "Martín";
            usuario.Email = "beatriz@test.com";
            usuario.Activo = false;
            usuario.TieneCredencialesConfiguradas = false;
            usuario.FechaCaducidad = nuevaFecha;
            usuario.Rol = nuevoRol;

            Assert.AreEqual(42, usuario.Id);
            Assert.AreEqual("Beatriz", usuario.Nombre);
            Assert.AreEqual("Martín", usuario.Apellidos);
            Assert.AreEqual("beatriz@test.com", usuario.Email);
            Assert.IsFalse(usuario.Activo);
            Assert.IsFalse(usuario.TieneCredencialesConfiguradas);
            Assert.AreEqual(nuevaFecha, usuario.FechaCaducidad);
            Assert.AreEqual(nuevoRol, usuario.Rol);
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
        public void ObtenerResultadoCalculoPin_PinMenorQue1000_LanzaInvalidOperationException()
        {
            Usuario usuario = new Usuario("Ana", "López", "ana@test.com", _rolPrueba, true, DateTime.Now.AddDays(30), "Password123!");
            var pinField = typeof(Usuario).GetField("pin", BindingFlags.NonPublic | BindingFlags.Instance);
            pinField!.SetValue(usuario, 999);

            Assert.Throws<InvalidOperationException>(() => usuario.obtenerResultadoCalculoPin());
        }

        [TestMethod]
        public void ObtenerResultadoCalculoPin_PinMayorQue10000_LanzaInvalidOperationException()
        {
            Usuario usuario = new Usuario("Ana", "López", "ana@test.com", _rolPrueba, true, DateTime.Now.AddDays(30), "Password123!");
            var pinField = typeof(Usuario).GetField("pin", BindingFlags.NonPublic | BindingFlags.Instance);
            pinField!.SetValue(usuario, 10001);

            Assert.Throws<InvalidOperationException>(() => usuario.obtenerResultadoCalculoPin());
        }

        [TestMethod]
        public void ObtenerResultadoCalculoPin_PinFormatoInvalido_LanzaInvalidOperationException()
        {
            Usuario usuario = new Usuario("Ana", "López", "ana@test.com", _rolPrueba, true, DateTime.Now.AddDays(30), "Password123!");
            var pinField = typeof(Usuario).GetField("pin", BindingFlags.NonPublic | BindingFlags.Instance);
            pinField!.SetValue(usuario, 10000);

            Assert.Throws<InvalidOperationException>(() => usuario.obtenerResultadoCalculoPin());
        }

        [TestMethod]
        public void Equals_MismoUsuario_RetornaTrue()
        {
            DateTime fecha = new DateTime(2026, 12, 31);
            Usuario u1 = new Usuario("Carlos", "Gómez", "carlos@test.com", _rolPrueba, true, fecha, "Clave123!");
            u1.Id = 5;

            Usuario u2 = new Usuario("Carlos", "Gómez", "carlos@test.com", _rolPrueba, true, fecha, "OtraClave!");
            u2.Id = 5;

            var pinField = typeof(Usuario).GetField("pin", BindingFlags.NonPublic | BindingFlags.Instance);
            pinField!.SetValue(u2, u1.Pin);

            Assert.IsTrue(u1.Equals(u2));
        }

        [TestMethod]
        public void Equals_DiferenciasIndividuales_RetornanFalse()
        {
            DateTime fecha = new DateTime(2026, 12, 31);
            Usuario baseUser()
            {
                var u = new Usuario("Carlos", "Gómez", "carlos@test.com", _rolPrueba, true, fecha, "Clave123!");
                u.Id = 5;
                return u;
            }

            var u1 = baseUser();
            var pinField = typeof(Usuario).GetField("pin", BindingFlags.NonPublic | BindingFlags.Instance);
            pinField!.SetValue(u1, 5555);

            // Objeto de otro tipo
            Assert.IsFalse(u1.Equals("cadena"));

            // Diferente Id
            var uId = baseUser();
            pinField!.SetValue(uId, 5555);
            uId.Id = 99;
            Assert.IsFalse(u1.Equals(uId));

            // Diferente Nombre
            var uNom = baseUser();
            pinField!.SetValue(uNom, 5555);
            uNom.Nombre = "Otro";
            Assert.IsFalse(u1.Equals(uNom));

            // Diferente Apellidos
            var uAp = baseUser();
            pinField!.SetValue(uAp, 5555);
            uAp.Apellidos = "OtroApellido";
            Assert.IsFalse(u1.Equals(uAp));

            // Diferente Email
            var uMail = baseUser();
            pinField!.SetValue(uMail, 5555);
            uMail.Email = "otro@test.com";
            Assert.IsFalse(u1.Equals(uMail));

            // Diferente Rol
            var uRol = baseUser();
            pinField!.SetValue(uRol, 5555);
            uRol.Rol = new Rol("Distinto", "Desc");
            Assert.IsFalse(u1.Equals(uRol));

            // Diferente Activo
            var uAct = baseUser();
            pinField!.SetValue(uAct, 5555);
            uAct.Activo = false;
            Assert.IsFalse(u1.Equals(uAct));

            // Diferente Pin
            var uPin = baseUser();
            pinField!.SetValue(uPin, 6666);
            Assert.IsFalse(u1.Equals(uPin));

            // Diferente TieneCredencialesConfiguradas
            var uCred = baseUser();
            pinField!.SetValue(uCred, 5555);
            uCred.TieneCredencialesConfiguradas = false;
            Assert.IsFalse(u1.Equals(uCred));

            // Diferente FechaCaducidad
            var uFec = baseUser();
            pinField!.SetValue(uFec, 5555);
            uFec.FechaCaducidad = fecha.AddDays(10);
            Assert.IsFalse(u1.Equals(uFec));
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
            StringAssert.Contains(texto, "Activo");
        }

        [TestMethod]
        public void ToString_ValoresNulosYInactivo_MuestraFormatoPorDefecto()
        {
            Usuario usuario = new Usuario("Lucía", "Méndez", "lucia@test.com", _rolPrueba, false, DateTime.Now, "Clave123!");
            usuario.Id = 11;
            usuario.Nombre = null!;
            usuario.Apellidos = null!;
            usuario.Email = null!;
            usuario.Rol = null!;
            usuario.FechaCaducidad = default;

            string texto = usuario.ToString();

            StringAssert.Contains(texto, "Sin rol");
            StringAssert.Contains(texto, "Inactivo");
            StringAssert.Contains(texto, "Sin fecha");
            StringAssert.Contains(texto, "sin-email");
        }
    }
}
