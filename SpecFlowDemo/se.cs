using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Html5;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Safari;
using Selenium.Helper;


namespace SpecFlowDemo
{
    public class SeleniumFW
    {
        public IWebDriver BrowserDriver = null;
        public string Url = "";
        public SeleniumFW(string browser,
            string url,
            bool srnmax = true)
        {
            this.Url = url;
            
            switch (browser)
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    if (srnmax){ chromeOptions.AddArgument("--start-maximized");}
                    this.BrowserDriver = new ChromeDriver("C:\\Users\\kf\\Documents\\Visual Studio 2015\\Projects\\SpecFlowDemo\\SpecFlowDemo\\drivers\\chromedriver.exe");
                    break;
                case "edge":
                    var edgeOptions = new EdgeOptions();
                    //if (srnmax) { _EdgeOptions.AddAdditionalCapability("--start-maximized"); }
                    this.BrowserDriver = new EdgeDriver(edgeOptions);
                    break;
                case "firefox":
                    this.BrowserDriver = new FirefoxDriver();
                    break;
                case "ie":
                    this.BrowserDriver = new InternetExplorerDriver();
                    break;
                case "opera":
                    this.BrowserDriver = new OperaDriver();
                    break;
                case "phantom":
                    this.BrowserDriver = new PhantomJSDriver();
                    break;
                case "safari":
                    this.BrowserDriver = new SafariDriver();
                    break;
                default:
                    this.BrowserDriver = null;
                    break;
            }
            SeleniumStart();
        }

        public bool SeleniumStart()
        {
            bool rc = false;
            if (BrowserDriver == null)
            {
                Console.WriteLine("Failed BrowserDriver");
            }
            else
            {
                //BrowserDriver.SwitchTo(-1);
                BrowserDriver.Navigate().GoToUrl(Url);
                rc = true;
            }

            return rc;
        }
    }
}


