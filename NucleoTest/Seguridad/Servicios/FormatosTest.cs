using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nucleo.Seguridad.Servicios;
using System;

namespace NucleoTest.Seguridad.Servicios
{
    [TestClass]
    public class FormatosTest
    {
        [TestMethod]
        public void Constructor_CreaInstancia()
        {
            var formatos = new Formatos();
            Assert.IsNotNull(formatos);
        }

        [TestMethod]
        public void FormatearFecha_FechaValida_RetornaFormatoCorrecto()
        {
            DateTime fecha = new DateTime(2026, 11, 12);
            string resultado = Formatos.FormatearFecha(fecha);
            Assert.AreEqual("12/11/2026", resultado);
        }

        [TestMethod]
        public void FormatearFecha_FechaNull_RetornaNoConsta()
        {
            string resultado = Formatos.FormatearFecha(null);
            Assert.AreEqual("no consta", resultado);
        }
    }
}

