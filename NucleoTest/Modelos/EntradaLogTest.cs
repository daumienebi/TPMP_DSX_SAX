using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nucleo.Modelos;
using System;

namespace NucleoTest.Modelos
{
    [TestClass]
    public class EntradaLogTest
    {
        [TestMethod]
        public void Constructor_ConUsuario_InicializaPropiedadesCorrectamente()
        {
            Rol rol = new Rol("Admin", "Rol administrador");
            Usuario usuario = new Usuario("Ana", "López", "ana@test.com", rol, true, DateTime.Now.AddDays(10), "Password123!");
            DateTime fecha = new DateTime(2026, 5, 20, 10, 30, 0);

            EntradaLog log = new EntradaLog(usuario, fecha, TipoEntradaLog.INFO, "Inicio de sesión correcto");

            Assert.AreEqual(-1, log.Id);
            Assert.AreEqual(usuario, log.Usuario);
            Assert.AreEqual(fecha, log.FechaHora);
            Assert.AreEqual(TipoEntradaLog.INFO, log.TipoEntradaLog);
            Assert.AreEqual("Inicio de sesión correcto", log.Descripcion);
        }

        [TestMethod]
        public void Constructor_SinUsuario_InicializaPropiedadesCorrectamente()
        {
            DateTime fecha = new DateTime(2026, 5, 20, 10, 30, 0);

            EntradaLog log = new EntradaLog(fecha, TipoEntradaLog.AVISO, "Evento del sistema sin usuario");

            Assert.AreEqual(-1, log.Id);
            Assert.IsNull(log.Usuario);
            Assert.AreEqual(fecha, log.FechaHora);
            Assert.AreEqual(TipoEntradaLog.AVISO, log.TipoEntradaLog);
            Assert.AreEqual("Evento del sistema sin usuario", log.Descripcion);
        }

        [TestMethod]
        public void Propiedades_ModificanValoresCorrectamente()
        {
            DateTime fecha = DateTime.Now;
            EntradaLog log = new EntradaLog(fecha, TipoEntradaLog.INFO, "Inicial");

            Rol rol = new Rol("User", "Usuario normal");
            Usuario nuevoUsuario = new Usuario("Pedro", "Sanz", "pedro@test.com", rol, true, DateTime.Now, "Pass123!");
            DateTime nuevaFecha = fecha.AddHours(2);

            log.Id = 42;
            log.Usuario = nuevoUsuario;
            log.FechaHora = nuevaFecha;
            log.TipoEntradaLog = TipoEntradaLog.ERROR;
            log.Descripcion = "Modificado";

            Assert.AreEqual(42, log.Id);
            Assert.AreEqual(nuevoUsuario, log.Usuario);
            Assert.AreEqual(nuevaFecha, log.FechaHora);
            Assert.AreEqual(TipoEntradaLog.ERROR, log.TipoEntradaLog);
            Assert.AreEqual("Modificado", log.Descripcion);
        }
    }
}
