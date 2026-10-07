using System;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nucleo.Seguridad;

namespace NucleoTest.Seguridad
{
    [TestClass]
    public class ServicioCifradoTests
    {
        [TestMethod]
        public void GenerarClave_DeberiaRetornarBase64De32Bytes()
        {
            string claveBase64 = ServicioCifrado.GenerarClave();
            byte[] clave = Convert.FromBase64String(claveBase64);

            Assert.AreEqual(32, clave.Length, "La clave debe tener 32 bytes (AES-256).");
        }

        [TestMethod]
        public void CifrarYDescifrar_Roundtrip_DeberiaRetornarTextoOriginal()
        {
            string clave = ServicioCifrado.GenerarClave();
            string texto = "Texto de prueba con acentos: áéíóú ñ y símbolos !@#";

            string cifrado = ServicioCifrado.Cifrar(texto, clave);
            string descifrado = ServicioCifrado.Descifrar(cifrado, clave);

            Assert.AreEqual(texto, descifrado);
        }

        [TestMethod]
        public void Cifrar_TextoVacio_DeberiaLanzarArgumentException()
        {
            string clave = ServicioCifrado.GenerarClave();

            Assert.Throws<ArgumentException>(() =>
                ServicioCifrado.Cifrar(string.Empty, clave));
        }

        [TestMethod]
        public void Cifrar_ClaveNoBase64_DeberiaLanzarArgumentException()
        {
            string texto = "dato";
            string claveInvalida = "no-es-base64!!";

            Assert.Throws<ArgumentException>(() =>
                ServicioCifrado.Cifrar(texto, claveInvalida));
        }

        [TestMethod]
        public void Descifrar_ContenidoVacio_DeberiaLanzarArgumentException()
        {
            string clave = ServicioCifrado.GenerarClave();

            Assert.Throws<ArgumentException>(() =>
                ServicioCifrado.Descifrar(string.Empty, clave));
        }

        [TestMethod]
        public void Descifrar_ClaveIncorrecta_DeberiaLanzarCryptographicException()
        {
            string clave1 = ServicioCifrado.GenerarClave();
            string clave2 = ServicioCifrado.GenerarClave(); // distinta
            string texto = "mensaje secreto";

            string cifrado = ServicioCifrado.Cifrar(texto, clave1);

            Assert.Throws<CryptographicException>(() =>
                ServicioCifrado.Descifrar(cifrado, clave2));
        }

        [TestMethod]
        public void Descifrar_ContenidoManipulado_DeberiaLanzarCryptographicException()
        {
            string clave = ServicioCifrado.GenerarClave();
            string texto = "mensaje a corromper";

            string cifradoBase64 = ServicioCifrado.Cifrar(texto, clave);
            byte[] contenido = Convert.FromBase64String(cifradoBase64);

            // Formato: [nonce (12)] [tag (16)] [ciphertext]
            const int TamanoNonce = 12;
            const int TamanoEtiqueta = 16;

            // Corromper un byte dentro de la etiqueta para forzar fallo de autenticación
            if (contenido.Length > TamanoNonce + 0)
            {
                contenido[TamanoNonce] ^= 0xFF;
            }

            string manipuladoBase64 = Convert.ToBase64String(contenido);

            Assert.Throws<CryptographicException>(() =>
                ServicioCifrado.Descifrar(manipuladoBase64, clave));
        }
    }
}