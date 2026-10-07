using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nucleo.Seguridad;

namespace Tests.Nucleo.Seguridad;

[TestClass]
public class ServicioContrasenasTests
{
    [TestMethod]
    public void EsValida_ConContrasenaValida_RetornaTrue()
    {
        // Arrange
        var contrasena = "P@ssw0rd1!";

        // Act
        var resultado = ServicioContrasenas.EsValida(contrasena);

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    public void EsValida_Null_RetornaFalse()
    {
        string? contrasena = null;

        var resultado = ServicioContrasenas.EsValida(contrasena!);

        Assert.IsFalse(resultado);
    }

    [TestMethod]
    public void EsValida_EmptyOrWhitespace_RetornaFalse()
    {
        Assert.IsFalse(ServicioContrasenas.EsValida(""));
        Assert.IsFalse(ServicioContrasenas.EsValida("   "));
    }

    [TestMethod]
    public void EsValida_MenorQue8Caracteres_RetornaFalse()
    {
        var contrasena = "Ab1!aA"; // 6 caracteres
        Assert.IsFalse(ServicioContrasenas.EsValida(contrasena));
    }

    [TestMethod]
    public void EsValida_SinMayusculas_RetornaFalse()
    {
        var contrasena = "password1!";
        Assert.IsFalse(ServicioContrasenas.EsValida(contrasena));
    }

    [TestMethod]
    public void EsValida_SinMinusculas_RetornaFalse()
    {
        var contrasena = "PASSWORD1!";
        Assert.IsFalse(ServicioContrasenas.EsValida(contrasena));
    }

    [TestMethod]
    public void EsValida_SinDigitos_RetornaFalse()
    {
        var contrasena = "Password!";
        Assert.IsFalse(ServicioContrasenas.EsValida(contrasena));
    }

    [TestMethod]
    public void EsValida_SinCaracterEspecial_RetornaFalse()
    {
        var contrasena = "Password1";
        Assert.IsFalse(ServicioContrasenas.EsValida(contrasena));
    }
}