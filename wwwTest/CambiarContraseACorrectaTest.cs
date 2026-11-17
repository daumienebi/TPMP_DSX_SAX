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
    public class CambiarContraseACorrecta
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
        public void TheCambiarContraseACorrectaTest()
        {
            driver.Navigate().GoToUrl("https://localhost:7226/Principal");
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Página principal'])[1]/following::div[2]")).Click();
            driver.FindElement(By.XPath("//button[@type='button']")).Click();
            driver.FindElement(By.LinkText("Ver perfil")).Click();
            driver.Navigate().GoToUrl("https://localhost:7226/Usuarios/Perfil");
            driver.FindElement(By.Id("PasswordActual")).Click();
            driver.FindElement(By.Id("PasswordActual")).Clear();
            driver.FindElement(By.Id("PasswordActual")).SendKeys("Password@2");
            driver.FindElement(By.Id("PasswordNueva")).Click();
            driver.FindElement(By.Id("PasswordNueva")).Clear();
            driver.FindElement(By.Id("PasswordNueva")).SendKeys("Password@2");
            driver.FindElement(By.Id("ConfirmacionPassword")).Click();
            driver.FindElement(By.Id("ConfirmacionPassword")).Clear();
            driver.FindElement(By.Id("ConfirmacionPassword")).SendKeys("Password@2");
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Repetir nueva contraseña'])[1]/following::button[1]")).Click();
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Volver'])[1]/following::div[1]")).Click();
            try
            {
                Assert.AreEqual("La contraseña se ha cambiado correctamente.", driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Volver'])[1]/following::div[1]")).Text);
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
