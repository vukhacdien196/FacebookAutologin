using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OtpNet;
using System;
using System.Linq;
using System.Threading;

namespace FacebookAutologin
{
    class Webdriver
    {
        private readonly IWebDriver webDriver;
        public Webdriver()
        {

        }
        public Webdriver(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }
        public void Login(string username, string password, string twoFactorAuthSeed = null)
        {
            webDriver.Url = "https://m.facebook.com/";
            var input = webDriver.FindElement(By.CssSelector("#m_login_email"));
            input.SendKeys(username);
            Thread.Sleep(500);
            input = webDriver.FindElement(By.CssSelector("#m_login_password"));
            input.SendKeys(password);
            Thread.Sleep(500);
            ClickAndWaitForPageToLoad(webDriver, By.CssSelector("#u_0_4 > button"));
            Thread.Sleep(2000);
            var _btnOut = By.XPath("/html/body/div[1]/div/div[2]/div/div[1]/div/div/div[3]/div[1]/div/div/a");
            if(_btnOut !=null)
            {
                ClickAndWaitForPageToLoad(webDriver, By.XPath("/html/body/div[1]/div/div[2]/div/div[1]/div/div/div[3]/div[1]/div/div/a"));
            }

            //if (!string.IsNullOrWhiteSpace(twoFactorAuthSeed))
            //{
            //    var otpKeyBytes = Base32Encoding.ToBytes(twoFactorAuthSeed);
            //    var totp = new Totp(otpKeyBytes);
            //    var twoFactorCode = totp.ComputeTotp();
            //    input = webDriver.FindElement(By.Id("approvals_code"));
            //    input.SendKeys(twoFactorCode);
            //}
            //try
            //{
            //    while (webDriver.FindElement(By.Id("checkpointSubmitButton")) != null)
            //    {
            //        ClickAndWaitForPageToLoad(webDriver, By.Id("checkpointSubmitButton"));
            //    }
            //}
            //catch
            //{

            //}
        }
        private void ClickAndWaitForPageToLoad(IWebDriver driver, By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                var elements = driver.FindElements(elementLocator);
                if (elements.Count == 0)
                {
                    Console.WriteLine("Không thấy button");
                    throw new NoSuchElementException("No elements " + elementLocator + " ClickAndWaitForPageToLoad");
                }
                var element = elements.FirstOrDefault(e => e.Displayed);
                Console.WriteLine("Thấy button");
                Console.WriteLine("Click " + element.ToString());

                element.Click();
                wait.Until(ExpectedConditions.StalenessOf(element));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found.");
                throw;
            }
        }
        public static void WaitUntil(IWebDriver driver, Func<bool> predicate, int timeout = 30)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                wait.Until(d =>
                {
                    try
                    {
                        return predicate();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                });
            }
            catch (NoSuchElementException)
            {
                throw;
            }
        }
        public void Post(string text)
        {
            try
            {
                ClickAndWaitForPageToLoad(webDriver, By.XPath("/html/body/div[1]/div/div[4]/div/div[1]/div/div[3]/div/div[1]/div[2]"));
            }
            catch
            {

            }
            var postBox = webDriver.FindElement(By.XPath("/html/body/div[2]/div[1]/div/div[2]/div/div/div[5]/div[3]/form/div[3]/div[3]/textarea"));
            postBox.SendKeys(text);
            Thread.Sleep(500);
            var postButtonSelector = By.XPath("/html/body/div[2]/div[1]/div/div[2]/div/div/div[5]/div[3]/div/button");
            WaitUntil(webDriver, () => webDriver.FindElement(postButtonSelector).Enabled);
            ClickAndWaitForPageToLoad(webDriver, postButtonSelector);
        }
    }
}
