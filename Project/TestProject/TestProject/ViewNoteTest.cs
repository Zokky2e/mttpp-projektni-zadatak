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
    public class ViewNoteTest
    {
        private IWebDriver driver;
        private string baseURL;
        private bool acceptNextAlert = true;
        private Note note;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "https://www.google.com/";
            note = new Note();
            note.Title = "Naslov";
            note.Description = "Opis";
            note.Priority = "1";
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
        }
        
        [Test]
        public void TheViewNoteTest()
        {
            
            driver.Navigate().GoToUrl("https://zavrsni-3652b.web.app/");
            //login
            driver.FindElement(By.XPath("//div[@id='root']/div/header/div/li/a/span")).Click();
            driver.FindElement(By.Id("email")).Click();
            driver.FindElement(By.Id("email")).Clear();
            driver.FindElement(By.Id("email")).SendKeys("test1@gmail.com");
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("1234qwer");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            //mainTest
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//div[@id='root']/div/main/div/button/i")).Click();
            driver.FindElement(By.XPath("//div[@id='root']/div/main/div/div/div/div/button[3]/span")).Click();
            driver.FindElement(By.XPath("//div[@id='root']/div/main/div/div/div/div/button[3]/span")).Click();
            driver.FindElement(By.XPath("//div[@id='root']/div/main/div/div/div/div/button[3]/span")).Click();
            driver.FindElement(By.XPath("//div[@id='root']/div/main/div/div/div/div[2]/div/div/button[3]")).Click();
            driver.FindElement(By.XPath("//div[@id='root']/div/main/div/div/div/div[2]/div/div/button[3]")).Click();
            driver.FindElement(By.XPath("//div[@id='root']/div/main/div/div/div/div[2]/div/div/button[2]/abbr")).Click();
            driver.FindElement(By.XPath("//div[@id='root']/div/main/div/div/div/div[2]/div/div/div/div[2]/button[24]/abbr")).Click();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/main/div/div/div/ul/div[1]/li")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.XPath("//*[@id=\"root\"]/div/main/div/div/div/ul/div[2]/div/div/div/h2")).GetAttribute("textContent").ToString() != "");
            var title = (driver.FindElement(By.XPath("//*[@id=\"root\"]/div/main/div/div/div/ul/div[2]/div/div/div/h2")).GetAttribute("textContent").ToString());
            Assert.AreEqual(note.Title, title);
            wait.Until(e => e.FindElement(By.XPath("//*[@id=\"root\"]/div/main/div/div/div/ul/div[2]/div/div/div/p[1]/span")).GetAttribute("textContent").ToString() != "");
            var priority = (driver.FindElement(By.XPath("//*[@id=\"root\"]/div/main/div/div/div/ul/div[2]/div/div/div/p[1]/span")).GetAttribute("textContent").ToString());
            Assert.AreEqual(note.Priority, priority);
            wait.Until(e => e.FindElement(By.XPath("//*[@id=\"root\"]/div/main/div/div/div/ul/div[2]/div/div/div/p[2]")).GetAttribute("textContent").ToString() != "");
            var description = (driver.FindElement(By.XPath("//*[@id=\"root\"]/div/main/div/div/div/ul/div[2]/div/div/div/p[2]")).GetAttribute("textContent").ToString());
            Assert.AreEqual(note.Description, description);
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
