using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nucleo.Modelos;
using System;

namespace NucleoTest.Modelos
{
    [TestClass]
    public class ProyectoTest
    {
        [TestMethod]
        public void Constructor_InicializaPropiedadesCorrectamente()
        {
            DateTime fecha = new DateTime(2026, 1, 15);
            Proyecto proyecto = new Proyecto(
                "Proyecto Alfa",
                "PA",
                "Descripción del proyecto Alfa",
                fecha,
                true,
                false);

            Assert.AreEqual(-1, proyecto.Id);
            Assert.AreEqual("Proyecto Alfa", proyecto.Nombre);
            Assert.AreEqual("PA", proyecto.Prefijo);
            Assert.AreEqual("Descripción del proyecto Alfa", proyecto.Descripcion);
            Assert.AreEqual(fecha, proyecto.FechaCreacion);
            Assert.IsTrue(proyecto.EsActivo);
            Assert.IsFalse(proyecto.EsPublico);
        }

        [TestMethod]
        public void Propiedades_ModificanValoresCorrectamente()
        {
            DateTime fechaOriginal = DateTime.Now;
            Proyecto proyecto = new Proyecto("Nombre", "PR", "Desc", fechaOriginal, true, true);

            DateTime nuevaFecha = fechaOriginal.AddDays(5);
            proyecto.Id = 10;
            proyecto.Nombre = "Nuevo Nombre";
            proyecto.Prefijo = "NN";
            proyecto.Descripcion = "Nueva Desc";
            proyecto.FechaCreacion = nuevaFecha;
            proyecto.EsActivo = false;
            proyecto.EsPublico = false;

            Assert.AreEqual(10, proyecto.Id);
            Assert.AreEqual("Nuevo Nombre", proyecto.Nombre);
            Assert.AreEqual("NN", proyecto.Prefijo);
            Assert.AreEqual("Nueva Desc", proyecto.Descripcion);
            Assert.AreEqual(nuevaFecha, proyecto.FechaCreacion);
            Assert.IsFalse(proyecto.EsActivo);
            Assert.IsFalse(proyecto.EsPublico);
        }

        [TestMethod]
        public void Equals_MismosValores_RetornaTrue()
        {
            DateTime fecha = new DateTime(2026, 1, 1);
            Proyecto p1 = new Proyecto("P1", "Pref1", "Desc1", fecha, true, true);
            Proyecto p2 = new Proyecto("P1", "Pref1", "Desc1", fecha, true, true);

            Assert.IsTrue(p1.Equals(p2));
            Assert.IsTrue(p1.Equals((object)p2));
        }

        [TestMethod]
        public void Equals_MismaReferencia_RetornaTrue()
        {
            Proyecto p1 = new Proyecto("P1", "Pref1", "Desc1", DateTime.Now, true, true);

            Assert.IsTrue(p1.Equals(p1));
        }

        [TestMethod]
        public void Equals_ValoresDiferentes_RetornaFalse()
        {
            DateTime fecha = DateTime.Now;
            Proyecto p1 = new Proyecto("P1", "Pref1", "Desc1", fecha, true, true);
            Proyecto p2 = new Proyecto("P2", "Pref1", "Desc1", fecha, true, true);
            Proyecto p3 = new Proyecto("P1", "Pref2", "Desc1", fecha, true, true);
            Proyecto p4 = new Proyecto("P1", "Pref1", "Desc2", fecha, true, true);
            Proyecto p5 = new Proyecto("P1", "Pref1", "Desc1", fecha, false, true);
            Proyecto p6 = new Proyecto("P1", "Pref1", "Desc1", fecha, true, false);

            Assert.IsFalse(p1.Equals(p2));
            Assert.IsFalse(p1.Equals(p3));
            Assert.IsFalse(p1.Equals(p4));
            Assert.IsFalse(p1.Equals(p5));
            Assert.IsFalse(p1.Equals(p6));
        }

        [TestMethod]
        public void Equals_Null_RetornaFalse()
        {
            Proyecto p = new Proyecto("P1", "Pref1", "Desc1", DateTime.Now, true, true);

            Assert.IsFalse(p.Equals(null));
        }

        [TestMethod]
        public void Equals_ObjetoDeOtroTipo_RetornaFalse()
        {
            Proyecto p = new Proyecto("P1", "Pref1", "Desc1", DateTime.Now, true, true);

            Assert.IsFalse(p.Equals("cadena de texto"));
        }
    }
}
