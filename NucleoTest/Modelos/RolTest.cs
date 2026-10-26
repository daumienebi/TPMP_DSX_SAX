using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nucleo.Modelos;

namespace NucleoTest.Modelos
{
    [TestClass]
    public class RolTest
    {
        [TestMethod]
        public void Constructor_InicializaPropiedadesCorrectamente()
        {
            Rol rol = new Rol("Administrador", "Todos los permisos");

            Assert.AreEqual(-1, rol.Id);
            Assert.AreEqual("Administrador", rol.Nombre);
            Assert.AreEqual("Todos los permisos", rol.Descripcion);
        }

        [TestMethod]
        public void Propiedades_ModificanValoresCorrectamente()
        {
            Rol rol = new Rol("NombreOriginal", "DescOriginal");

            rol.Id = 5;
            rol.Nombre = "NuevoNombre";
            rol.Descripcion = "NuevaDesc";

            Assert.AreEqual(5, rol.Id);
            Assert.AreEqual("NuevoNombre", rol.Nombre);
            Assert.AreEqual("NuevaDesc", rol.Descripcion);
        }

        [TestMethod]
        public void Equals_MismosValores_RetornaTrue()
        {
            Rol r1 = new Rol("Usuario", "Permisos limitados") { Id = 2 };
            Rol r2 = new Rol("Usuario", "Permisos limitados") { Id = 2 };

            Assert.IsTrue(r1.Equals(r2));
            Assert.IsTrue(r1.Equals((object)r2));
        }

        [TestMethod]
        public void Equals_MismaReferencia_RetornaTrue()
        {
            Rol r = new Rol("Usuario", "Permisos limitados");

            Assert.IsTrue(r.Equals(r));
        }

        [TestMethod]
        public void Equals_ValoresDiferentes_RetornaFalse()
        {
            Rol r1 = new Rol("Usuario", "Permisos limitados") { Id = 2 };
            Rol r2 = new Rol("Usuario", "Permisos limitados") { Id = 3 };
            Rol r3 = new Rol("Admin", "Permisos limitados") { Id = 2 };
            Rol r4 = new Rol("Usuario", "Otra descripcion") { Id = 2 };

            Assert.IsFalse(r1.Equals(r2));
            Assert.IsFalse(r1.Equals(r3));
            Assert.IsFalse(r1.Equals(r4));
        }

        [TestMethod]
        public void Equals_Null_RetornaFalse()
        {
            Rol r = new Rol("Usuario", "Permisos limitados");

            Assert.IsFalse(r.Equals(null));
        }

        [TestMethod]
        public void Equals_ObjetoDeOtroTipo_RetornaFalse()
        {
            Rol r = new Rol("Usuario", "Permisos limitados");

            Assert.IsFalse(r.Equals(12345));
        }
    }
}
