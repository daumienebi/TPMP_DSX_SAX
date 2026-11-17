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
    public class LoginConEmailIncorrecto
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
        public void TheLoginConEmailIncorrectoTest()
        {
            driver.Navigate().GoToUrl("https://localhost:7226/Login/Index");
            driver.FindElement(By.Id("txtUsuario")).Click();
            driver.FindElement(By.Id("txtUsuario")).Clear();
            driver.FindElement(By.Id("txtUsuario")).SendKeys("a@a.com");
            driver.FindElement(By.Id("btnSiguiente")).Click();
            driver.FindElement(By.Id("txtContraseña")).Click();
            driver.FindElement(By.Id("txtContraseña")).Clear();
            driver.FindElement(By.Id("txtContraseña")).SendKeys("password22");
            driver.FindElement(By.Id("btnAceptar")).Click();
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Contraseña'])[1]/following::span[1]")).Click();
            try
            {
                Assert.AreEqual("Usuario o contraseña incorrectos.", driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Contraseña'])[1]/following::span[1]")).Text);
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
