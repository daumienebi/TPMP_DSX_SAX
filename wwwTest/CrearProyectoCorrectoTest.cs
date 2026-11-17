using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestClass]
    public class CrearProyectoCorrecto
    {
        private static IWebDriver driver;
        private StringBuilder verificationErrors;
        private static string baseURL;
        private bool acceptNextAlert = true;
        
        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            driver = new FirefoxDriver();
            baseURL = "https://www.google.com/";
        }
        
        [ClassCleanup]
        public static void CleanupClass()
        {
            try
            {
                //driver.Quit();// quit does not close the window
                driver.Close();
                driver.Dispose();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
        
        [TestInitialize]
        public void InitializeTest()
        {
            verificationErrors = new StringBuilder();
        }
        
        [TestCleanup]
        public void CleanupTest()
        {
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [TestMethod]
        public void TheCrearProyectoCorrectoTest()
        {
            driver.Navigate().GoToUrl("https://localhost:7226/Principal");
            driver.FindElement(By.XPath("//button[@type='button']")).Click();
            driver.FindElement(By.LinkText("Seleccionar proyecto")).Click();
            driver.Navigate().GoToUrl("https://localhost:7226/Proyectos/Seleccionar");
            driver.FindElement(By.Id("Nombre")).Click();
            driver.FindElement(By.Id("Nombre")).Clear();
            driver.FindElement(By.Id("Nombre")).SendKeys("Probar1");
            driver.FindElement(By.Id("Siglas")).Click();
            driver.FindElement(By.Id("Siglas")).Clear();
            driver.FindElement(By.Id("Siglas")).SendKeys("PP");
            driver.FindElement(By.Id("Descripcion")).Click();
            driver.FindElement(By.Id("Descripcion")).Clear();
            driver.FindElement(By.Id("Descripcion")).SendKeys("hola mundo!");
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Descripción'])[1]/following::button[1]")).Click();
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Volver'])[1]/following::div[1]")).Click();
            try
            {
                Assert.AreEqual("El proyecto se ha creado y seleccionado.", driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Volver'])[1]/following::div[1]")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
