using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nucleo.Seguridad;

namespace Nucleo.Seguridad.Tests
{
    [TestClass]
    public class ResultadoHashTests
    {
        [TestMethod]
        public void Equals_Null_ReturnsFalse()
        {
            var a = new ResultadoHash { Hash = "h", Sal = "s" };
            ResultadoHash? b = null;
            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void Equals_SameReference_ReturnsTrue()
        {
            var a = new ResultadoHash { Hash = "h", Sal = "s" };
            var b = a;
            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Equals_SameHashAndSal_ReturnsTrue()
        {
            var a = new ResultadoHash { Hash = "h", Sal = "s" };
            var b = new ResultadoHash { Hash = "h", Sal = "s" };
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a.Equals((object)b));
        }

        [TestMethod]
        public void Equals_DifferentHashOrSal_ReturnsFalse()
        {
            var a = new ResultadoHash { Hash = "h1", Sal = "s" };
            var b = new ResultadoHash { Hash = "h2", Sal = "s" };
            var c = new ResultadoHash { Hash = "h1", Sal = "s2" };
            Assert.IsFalse(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
        }

        [TestMethod]
        public void OperatorEquality_BothNull_ReturnsTrue()
        {
            ResultadoHash? a = null;
            ResultadoHash? b = null;
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
        }

        [TestMethod]
        public void OperatorEquality_LeftNullRightNotNull_ReturnsFalse()
        {
            ResultadoHash? a = null;
            ResultadoHash? b = new ResultadoHash { Hash = "h", Sal = "s" };
            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);
        }

        [TestMethod]
        public void OperatorEquality_EqualObjects_ReturnsTrue()
        {
            var a = new ResultadoHash { Hash = "h", Sal = "s" };
            var b = new ResultadoHash { Hash = "h", Sal = "s" };
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
        }

        [TestMethod]
        public void GetHashCode_EqualObjects_SameHashCode()
        {
            var a = new ResultadoHash { Hash = "h", Sal = "s" };
            var b = new ResultadoHash { Hash = "h", Sal = "s" };
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod]
        public void Creacion_IsRecent()
        {
            var a = new ResultadoHash { Hash = "h", Sal = "s" };
            var diff = DateTime.Now - a.Creacion;
            Assert.IsTrue(diff >= TimeSpan.Zero && diff < TimeSpan.FromSeconds(5), "La propiedad Creacion debe establecerse en el momento de creación.");
        }
    }
}