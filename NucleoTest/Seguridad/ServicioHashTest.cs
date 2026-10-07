using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nucleo.Seguridad;

namespace Pruebas.Nucleo.Seguridad
{
    [TestClass]
    public class ServicioHashTests
    {
        [TestMethod]
        public void Generar_TextoValido_RetornaHashYSalNoVacios()
        {
            var resultado = ServicioHash.Generar("miPassword123");
            Assert.IsFalse(string.IsNullOrWhiteSpace(resultado.Hash));
            Assert.IsFalse(string.IsNullOrWhiteSpace(resultado.Sal));
        }

        [TestMethod]
        public void Generar_MismoTexto_GeneraDiferenteSalYHash()
        {
            var r1 = ServicioHash.Generar("textoIgual");
            var r2 = ServicioHash.Generar("textoIgual");

            Assert.AreNotEqual(r1.Sal, r2.Sal);
            Assert.AreNotEqual(r1.Hash, r2.Hash);
        }

        [TestMethod]
        public void Generar_TextoNuloOBlanco_LanzaArgumentException()
        {
            Assert.Throws<ArgumentException>(() => ServicioHash.Generar(string.Empty));
            Assert.Throws<ArgumentException>(() => ServicioHash.Generar("   "));
        }

        [TestMethod]
        public void Verificar_Correcto_RetornaTrue()
        {
            var resultado = ServicioHash.Generar("claveSegura");
            Assert.IsTrue(ServicioHash.Verificar("claveSegura", resultado));
            Assert.IsTrue(ServicioHash.Verificar("claveSegura", resultado.Hash, resultado.Sal));
        }

        [TestMethod]
        public void Verificar_TextoIncorrecto_RetornaFalse()
        {
            var resultado = ServicioHash.Generar("claveSegura");
            Assert.IsFalse(ServicioHash.Verificar("otraClave", resultado));
            Assert.IsFalse(ServicioHash.Verificar("otraClave", resultado.Hash, resultado.Sal));
        }

        [TestMethod]
        public void Verificar_EntradasVacias_RetornaFalse()
        {
            Assert.IsFalse(ServicioHash.Verificar(string.Empty, string.Empty, string.Empty));
            Assert.IsFalse(ServicioHash.Verificar("texto", string.Empty, string.Empty));
            Assert.IsFalse(ServicioHash.Verificar(string.Empty, "hash", "sal"));
        }

        [TestMethod]
        public void Verificar_Base64Invalido_RetornaFalse()
        {
            Assert.IsFalse(ServicioHash.Verificar("texto", "not_base64", "not_base64"));

            var invalido = new ResultadoHash { Hash = "not_base64", Sal = "not_base64" };
            Assert.IsFalse(ServicioHash.Verificar("texto", invalido));
        }

        [TestMethod]
        public void Verificar_HashManipulado_RetornaFalse()
        {
            var resultado = ServicioHash.Generar("miPassword");
            var bytes = Convert.FromBase64String(resultado.Hash);
            bytes[0] ^= 0xFF; // manipular un byte
            var hashManipulado = Convert.ToBase64String(bytes);

            Assert.IsFalse(ServicioHash.Verificar("miPassword", hashManipulado, resultado.Sal));
        }
    }
}
