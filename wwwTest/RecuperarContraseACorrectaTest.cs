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
    public class RecuperarContraseACorrecta
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
        public void TheRecuperarContraseACorrectaTest()
        {
            driver.Navigate().GoToUrl("https://localhost:7226/Login/Index");
            driver.Navigate().GoToUrl("https://localhost:7226/Login/Index");
            driver.FindElement(By.Id("txtUsuario")).Click();
            driver.FindElement(By.Id("txtUsuario")).Clear();
            driver.FindElement(By.Id("txtUsuario")).SendKeys("b@b.com");
            driver.FindElement(By.Id("btnSiguiente")).Click();
            driver.FindElement(By.Id("btnRecuperar")).Click();
            driver.Navigate().GoToUrl("https://localhost:7226/Login/Recuperar");
            driver.FindElement(By.Id("txtCalculoPin")).Click();
            driver.FindElement(By.Id("txtCalculoPin")).Clear();
            driver.FindElement(By.Id("txtCalculoPin")).SendKeys("748");
            driver.FindElement(By.Id("btnValidarPin")).Click();
            driver.FindElement(By.Id("txtPasswordNueva")).Click();
            driver.FindElement(By.Id("txtPasswordNueva")).Clear();
            driver.FindElement(By.Id("txtPasswordNueva")).SendKeys("Password@4");
            driver.FindElement(By.Id("txtConfirmacionPassword")).Click();
            driver.FindElement(By.Id("txtConfirmacionPassword")).Clear();
            driver.FindElement(By.Id("txtConfirmacionPassword")).SendKeys("Password@4");
            driver.FindElement(By.Id("btnGuardarPassword")).Click();
            driver.Navigate().GoToUrl("https://localhost:7226/Login");
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)=']'])[1]/following::div[1]")).Click();
            try
            {
                Assert.AreEqual("Identificación-Validación", driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)=']'])[1]/following::div[1]")).Text);
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
