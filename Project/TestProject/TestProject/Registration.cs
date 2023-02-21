using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class Registration
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string email;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "https://www.google.com/";
            email = GenerateEmail();
            
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual(email, verificationErrors.ToString());
        }
        
        [Test]
        public void TheRegistrationTest()
        {
            driver.Navigate().GoToUrl("https://zavrsni-3652b.web.app/");
            driver.FindElement(By.XPath("//div[@id='root']/div/header/div/li/a/span")).Click();
            driver.FindElement(By.XPath("//div[@id='root']/div/main/div/div/div[2]/span")).Click();
            driver.FindElement(By.Id("email")).Click();
            driver.FindElement(By.Id("email")).Clear();
            
            driver.FindElement(By.Id("email")).SendKeys(email);
            driver.FindElement(By.Id("password")).Click();
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("1234qwer");
            driver.FindElement(By.Id("re-password")).Click();
            driver.FindElement(By.Id("re-password")).Clear();
            driver.FindElement(By.Id("re-password")).SendKeys("1234qwer");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            Thread.Sleep(5000);
            driver.Navigate().GoToUrl("https://zavrsni-3652b.web.app/profile");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.XPath("//div[@id='root']/div/main/div/div/p")).GetAttribute("textContent")!="");
            verificationErrors.Append(driver.FindElement(By.XPath("//div[@id='root']/div/main/div/div/p")).GetAttribute("textContent"));
        }
       
        private string GenerateEmail()
        {
            StringBuilder email = new StringBuilder();
            email.Append("test");
            email.Append(DateTime.Now.ToString().Replace(" ", "").Replace(":","").Replace("PM", "pm").Replace("AM", "am")); //securing email is unique
            email.Append("@gmail.com");
            return email.ToString();
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
