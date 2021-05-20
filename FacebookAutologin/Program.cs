using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
namespace FacebookAutologin
{
    class Program
    {
        static void Main(string[] args)
        {
            string email = "email";
            string pass = "pass";
            string code = "123123";
            try
            {
                var webDriver = LaunchBrowser();
                var facebookAutomation = new Webdriver(webDriver);
                facebookAutomation.Login(email, pass);
                Thread.Sleep(500);
                webDriver.Url = @"https://m.facebook.com/groups/1591352247781548/";
              //  Thread.Sleep(500);
               /// facebookAutomation.Post($"Xin chào các bạn, mình cần tìm mua đất đường ô tô ra vào thoải mái, điện tích tầm 200m2, tiền tầm dưới 2 tỷ. Anh/chị nào có inbox cho em {DateTime.Now}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Co loi: Error while executing automation");
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                //webDriver.Quit();
            }

        }
        static IWebDriver LaunchBrowser()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            var driver = new ChromeDriver(Environment.CurrentDirectory, options);
            return driver;
        }
    }
}
