using Backend;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nucleo.Modelos;
using System;

namespace BackendTest
{
    [TestClass]
    public class DatosTest
    {
        private Datos _datos = null!;

        [TestInitialize]
        public void SetUp()
        {
            _datos = new Datos();
        }

        #region Inicializacion
        [TestMethod]
        public void Constructor_InicializaColeccionesPorDefecto()
        {
            Assert.IsNotNull(_datos.Usuarios);
            Assert.IsNotNull(_datos.Roles);
            Assert.IsNotNull(_datos.Proyectos);
            Assert.IsNotNull(_datos.Logs);

            Assert.IsNotEmpty(_datos.Usuarios, "Debe inicializar usuarios de prueba.");
            Assert.IsNotEmpty(_datos.Roles, "Debe inicializar roles de prueba.");
            Assert.IsNotEmpty(_datos.Proyectos, "Debe inicializar proyectos de prueba.");
            Assert.IsNotEmpty(_datos.Logs, "Debe registrar logs iniciales.");
        }

        [TestMethod]
        public void Reiniciar_VuelveACrearDatosDePrueba()
        {
            _datos.Reiniciar();

            Assert.IsNotNull(_datos.LeerUsuario(1));
            Assert.IsNotNull(_datos.LeerRol(1));
            Assert.IsNotNull(_datos.LeerProyecto(1));
        }
        #endregion

        #region Usuarios
        [TestMethod]
        public void LeerUsuario_PorIdExistente_RetornaUsuario()
        {
            Usuario? usuario = _datos.LeerUsuario(1);

            Assert.IsNotNull(usuario);
            Assert.AreEqual(1, usuario.Id);
            Assert.AreEqual("admin@admin.com", usuario.Email);
        }

        [TestMethod]
        public void LeerUsuario_PorIdInexistente_RetornaNull()
        {
            Usuario? usuario = _datos.LeerUsuario(999);

            Assert.IsNull(usuario);
        }

        [TestMethod]
        public void LeerUsuario_PorEmailExistente_RetornaUsuario()
        {
            Usuario? usuario = _datos.LeerUsuario("admin@admin.com");

            Assert.IsNotNull(usuario);
            Assert.AreEqual("admin@admin.com", usuario.Email);
        }

        [TestMethod]
        public void LeerUsuario_PorEmailInexistente_RetornaNull()
        {
            Usuario? usuario = _datos.LeerUsuario("noexiste@ejemplo.com");

            Assert.IsNull(usuario);
        }

        [TestMethod]
        public void GuardarUsuario_NuevoUsuario_AsignaIdYLoAgrega()
        {
            Rol? rol = _datos.LeerRol(1);
            Assert.IsNotNull(rol);

            Usuario nuevoUsuario = new Usuario(
                "Carlos",
                "Ruiz",
                "carlos@ejemplo.com",
                rol,
                true,
                DateTime.Now.AddDays(30),
                "Password123!");

            Usuario? guardado = _datos.GuardarUsuario(nuevoUsuario);

            Assert.IsNotNull(guardado);
            Assert.IsTrue(guardado.Id > 0, "Debe asignar un ID positivo.");
            Assert.IsNotNull(_datos.LeerUsuario(guardado.Id));
            Assert.AreEqual("carlos@ejemplo.com", _datos.LeerUsuario(guardado.Id)!.Email);
        }

        [TestMethod]
        public void GuardarUsuario_UsuarioExistente_ActualizaDatos()
        {
            Usuario? usuario = _datos.LeerUsuario(1);
            Assert.IsNotNull(usuario);

            usuario.Nombre = "AdminModificado";
            _datos.GuardarUsuario(usuario);

            Usuario? actualizado = _datos.LeerUsuario(1);
            Assert.IsNotNull(actualizado);
            Assert.AreEqual("AdminModificado", actualizado.Nombre);
        }

        [TestMethod]
        public void GuardarUsuario_UsuarioNulo_LanzaArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _datos.GuardarUsuario(null!));
        }

        [TestMethod]
        public void EliminarUsuario_Existente_EliminaYRetornaUsuario()
        {
            Usuario? usuario = _datos.LeerUsuario(1);
            Assert.IsNotNull(usuario);

            Usuario? eliminado = _datos.EliminarUsuario(1);

            Assert.IsNotNull(eliminado);
            Assert.AreEqual(1, eliminado.Id);
            Assert.IsNull(_datos.LeerUsuario(1), "El usuario no debería encontrarse tras ser eliminado.");
        }

        [TestMethod]
        public void EliminarUsuario_Inexistente_RetornaNull()
        {
            Usuario? eliminado = _datos.EliminarUsuario(999);

            Assert.IsNull(eliminado);
        }
        #endregion

        #region Roles
        [TestMethod]
        public void LeerRol_PorIdExistente_RetornaRol()
        {
            Rol? rol = _datos.LeerRol(1);

            Assert.IsNotNull(rol);
            Assert.AreEqual(1, rol.Id);
            Assert.AreEqual("Administrador", rol.Nombre);
        }

        [TestMethod]
        public void LeerRol_PorIdInexistente_RetornaNull()
        {
            Rol? rol = _datos.LeerRol(999);

            Assert.IsNull(rol);
        }

        [TestMethod]
        public void GuardarRol_NuevoRol_AsignaIdYLoAgrega()
        {
            Rol nuevoRol = new Rol("Tester", "Rol para pruebas");

            Rol? guardado = _datos.GuardarRol(nuevoRol);

            Assert.IsNotNull(guardado);
            Assert.IsTrue(guardado.Id > 0);
            Assert.IsNotNull(_datos.LeerRol(guardado.Id));
            Assert.AreEqual("Tester", _datos.LeerRol(guardado.Id)!.Nombre);
        }

        [TestMethod]
        public void GuardarRol_RolExistente_ActualizaDatos()
        {
            Rol? rol = _datos.LeerRol(1);
            Assert.IsNotNull(rol);

            rol.Descripcion = "Nueva descripcion de admin";
            _datos.GuardarRol(rol);

            Rol? actualizado = _datos.LeerRol(1);
            Assert.IsNotNull(actualizado);
            Assert.AreEqual("Nueva descripcion de admin", actualizado.Descripcion);
        }

        [TestMethod]
        public void GuardarRol_RolNulo_LanzaArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _datos.GuardarRol(null!));
        }

        [TestMethod]
        public void EliminarRol_Existente_EliminaYRetornaRol()
        {
            Rol? eliminado = _datos.EliminarRol(2);

            Assert.IsNotNull(eliminado);
            Assert.AreEqual(2, eliminado.Id);
            Assert.IsNull(_datos.LeerRol(2));
        }

        [TestMethod]
        public void EliminarRol_Inexistente_RetornaNull()
        {
            Rol? eliminado = _datos.EliminarRol(999);

            Assert.IsNull(eliminado);
        }
        #endregion

        #region Proyectos
        [TestMethod]
        public void LeerProyecto_PorIdExistente_RetornaProyecto()
        {
            Proyecto? proyecto = _datos.LeerProyecto(1);

            Assert.IsNotNull(proyecto);
            Assert.AreEqual(1, proyecto.Id);
            Assert.AreEqual("SAX DSX", proyecto.Nombre);
        }

        [TestMethod]
        public void LeerProyecto_PorIdInexistente_RetornaNull()
        {
            Proyecto? proyecto = _datos.LeerProyecto(999);

            Assert.IsNull(proyecto);
        }

        [TestMethod]
        public void GuardarProyecto_NuevoProyecto_AsignaIdYLoAgrega()
        {
            Proyecto nuevo = new Proyecto(
                "Proyecto Alfa",
                "PA",
                "Descripcion Alfa",
                DateTime.Now,
                true,
                false);

            Proyecto? guardado = _datos.GuardarProyecto(nuevo);

            Assert.IsNotNull(guardado);
            Assert.IsTrue(guardado.Id > 0);
            Assert.IsNotNull(_datos.LeerProyecto(guardado.Id));
            Assert.AreEqual("Proyecto Alfa", _datos.LeerProyecto(guardado.Id)!.Nombre);
        }

        [TestMethod]
        public void GuardarProyecto_ProyectoExistente_ActualizaDatos()
        {
            Proyecto? proyecto = _datos.LeerProyecto(1);
            Assert.IsNotNull(proyecto);

            proyecto.Descripcion = "Descripcion actualizada";
            _datos.GuardarProyecto(proyecto);

            Proyecto? actualizado = _datos.LeerProyecto(1);
            Assert.IsNotNull(actualizado);
            Assert.AreEqual("Descripcion actualizada", actualizado.Descripcion);
        }

        [TestMethod]
        public void GuardarProyecto_ProyectoNulo_LanzaArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _datos.GuardarProyecto(null!));
        }

        [TestMethod]
        public void EliminarProyecto_Existente_EliminaYRetornaProyecto()
        {
            Proyecto? eliminado = _datos.EliminarProyecto(1);

            Assert.IsNotNull(eliminado);
            Assert.AreEqual(1, eliminado.Id);
            Assert.IsNull(_datos.LeerProyecto(1));
        }

        [TestMethod]
        public void EliminarProyecto_Inexistente_RetornaNull()
        {
            Proyecto? eliminado = _datos.EliminarProyecto(999);

            Assert.IsNull(eliminado);
        }
        #endregion

        #region Logs
        [TestMethod]
        public void AgregarLog_LogNuevo_AsignaIdYLoAgrega()
        {
            int logsIniciales = _datos.Logs.Count;
            EntradaLog nuevoLog = new EntradaLog(
                DateTime.Now,
                TipoEntradaLog.INFO,
                "Prueba de log");

            _datos.AgregarLog(nuevoLog);

            Assert.AreEqual(logsIniciales + 1, _datos.Logs.Count);
            Assert.IsTrue(nuevoLog.Id > 0);
        }

        [TestMethod]
        public void AgregarLog_LogNulo_LanzaArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _datos.AgregarLog(null!));
        }

        [TestMethod]
        public void LimpiarLog_VaciaListaYRetornaCantidadEliminada()
        {
            int cantidadInicial = _datos.Logs.Count;
            Assert.IsTrue(cantidadInicial > 0);

            int eliminados = _datos.LimpiarLog();

            Assert.AreEqual(cantidadInicial, eliminados);
            Assert.AreEqual(0, _datos.Logs.Count);
        }
        #endregion
    }
}
