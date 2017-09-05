

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Runtime.Serialization;


using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace SpecFlowDemo
{
    internal class ParkingCalcDriver
    {
        public static IWebDriver Browser;
        public static string currenthwnd;
        public static string mainhwnd;

        public static string path = @"C:\Users\kf\Documents\Visual Studio 2015\Projects\SpecFlowDemo\SpecFlowDemo\";
        public static int readyCondition=-1;
        //private static System.Collections.ObjectModel<IWebElement> Elementlist;
        public static void testClass()
        {

            InitBrowser();
            DoCalcTest();
            DoCalcTest("Long-Term Surface Parking", 2016, 2, 12, 2017, 3, 11, "4:21", "11:21", "AM", "PM", "$ 10,244.00", " (393 Days, 19 Hours, 0 Minutes)");

            //       chooseLot("Long-Term Surface Parking");
            //       var elist = Browser.FindElements(By.TagName("input"));
            //       var eelist = SetCalendar(2000, 7, 7, "EntryDate");
            //       var etime = setTime("5:32", "EntryTime");
            //       setAMPM("AM", "EntryTimeAMPM");
            //       eelist = SetCalendar(2002, 9, 22, "ExitDate");
            //       etime = setTime("9:22", "ExitTime");
            //       setAMPM("PM", "ExitTimeAMPM");
            //       +elist   Count = 10  System.Collections.ObjectModel.ReadOnlyCollection < OpenQA.Selenium.IWebElement >
            //string[] scalc = DoCalcs("$ 6,930.00", "(807 Days, 15 Hours, 50 Minutes)");
            //       if (scalc[(int)CalcsEval.Calc_Eval] != "FAIL")
            //       {
            //           Console.WriteLine("Failed DoCalcs: " + string.Join("|", scalc));
            //       }
            //       else
            //       {
            //           Console.WriteLine("PASSED DoCalcs: " + string.Join("|", scalc));
            //       }
            //       string sout = "";

            Browser.Quit();
        }

        public static bool InitBrowser()
        {
            bool rc = false;
            ParkingCalcDriver.Browser =
                new ChromeDriver(
                    path); //"C:\\Users\\kf\\Documents\\Visual Studio 2015\\Projects\\testSeleniumClass\\testSeleniumClass\\chromedriver.exe");
            Browser.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);


            //SpecFlowDemo.SeleniumFW se = new SeleniumFW("chrome", "http://adam.goucher.ca/parkcalc/index.php", true);
            Browser.Navigate().GoToUrl("http://adam.goucher.ca/parkcalc/index.php");
            rc=WaitForPageLoad(Browser);
            if (rc)
            {
                Console.WriteLine(Browser.Title);
                readyCondition = 1;
            }
            return rc;
        }

        public static bool DoCalcTest(
            string lot = "Long-Term Surface Parking",
            int entrydtyr = 2000,
            int entrydtmn = 7,
            int entrydtdy = 7,
            int exitdtyr = 2002,
            int exitdtmn = 9,
            int exitdtdy = 22,
            string entryTime = "5:32",
            string exitTime = "9:22",
            string entryampm = "AM",
            string exitampm = "PM",
            string calcverify = "$ 6,930.00",
            string timeverify = "(807 Days, 15 Hours, 50 Minutes)"
        )
        {
            bool rc = false;
            var elot = chooseLot(lot);

            var etime = setTime(entryTime, "EntryTime");
            var eampm = setAMPM(entryampm, "EntryTimeAMPM");
            var eEntryDate = SetCalendar(entrydtyr, entrydtmn, entrydtdy, "EntryDate");

            var xtime = setTime(exitTime, "ExitTime");
            var xampm = setAMPM(exitampm, "ExitTimeAMPM");
            var xexitdate = SetCalendar(exitdtyr, exitdtmn, exitdtdy, "ExitDate");

            string[] scalc = DoCalcs(calcverify, timeverify);
            if (scalc[(int)CalcsEval.Calc_Eval] != "FAIL")
            {
                Console.WriteLine("Failed DoCalcs: " + string.Join("|", scalc));
            }
            else
            {
                Console.WriteLine("PASSED DoCalcs: " + string.Join("|", scalc));
                rc = true;
            }

            return rc;
        }

        public static IWebElement chooseLot(string lot)
        {
            IWebElement rc = null;
            //<select name="Lot" size="1" id="Lot">
            //< option value = "STP" selected = "" > Short - Term Parking </ option >

            //< option value = "EP" > Economy Parking </ option >

            //< option value = "LTS" > Long - Term Surface Parking</ option >

            //< option value = "LTG" > Long - Term Garage Parking</ option >

            //< option value = "VP" > Valet Parking </ option >

            //</ select >

            IWebElement elot = FindElementsAtrib(By.TagName("select"), "id", "Lot");
            if (elot != null)
            {
                elot.Click();
                IWebElement elotsel = FindElementsAtrib(By.TagName("option"), "", lot, elot);
                if (elotsel != null)
                {
                    elotsel.Click();
                    Thread.Sleep(200);
                    rc = elotsel;
                }

            }
            return rc;
        }

        enum CalcsEval
        {
            Calc_Eval,
            Calc_Result,
            Calc_Verify,
            Time_Eval,
            Time_Result,
            Time_Verify
            //EntryTime_Eval,
            //EntryTime_Result,
            //EntryTime_Verify,
            //ExitTime_Eval,
            //ExitTime_Result,
            //ExitTime_Verify
        }

        public static string[] DoCalcs(string calcresult, string timeresult)
        {
            string[] rc = new string[] { "", "", "", "", "", "" };//,"","","","","",""};
            rc[(int)CalcsEval.Calc_Verify] = calcresult;
            rc[(int)CalcsEval.Time_Verify] = timeresult;

            //<input type="submit" name="Submit" value="Calculate">
            IWebElement ecalc = FindElementsAtrib(By.TagName("input"), "value", "Calculate");

            //<b>ERROR! Enter A Correctly Formatted Date</b>
            if (ecalc != null)
            {
                ecalc.Click();
                Thread.Sleep(1000);
                //<span class="SubHead"><font color="#FFFFFF"><b>ERROR! Enter A Correctly Formatted Date</b></font></span>
                IWebElement ecalcv = FindElementsAtrib(By.TagName("span"), "class", "SubHead");


                if (ecalcv != null)
                {
                    rc[(int)CalcsEval.Calc_Result] = ecalcv.Text;
                    if (calcresult == ecalcv.Text)
                    { //calc pass
                        rc[(int)CalcsEval.Calc_Eval] = "PASS";
                        //<span class="BodyCopy"><font color="#FFFFFF"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(807 Days, 15 Hours, 50 Minutes)</b></font></span>
                        IWebElement ecalci = FindElementsAtrib(By.TagName("span"), "class", "BodyCopy");
                        if (ecalci != null)
                        {
                            rc[(int)CalcsEval.Time_Verify] = ecalci.Text;
                            if (ecalci.Text == timeresult)
                            {
                                rc[(int)CalcsEval.Time_Eval] = "PASS";
                            }
                            else
                            {
                                rc[(int)CalcsEval.Time_Eval] = "FAIL";
                            }
                        }
                    }
                    else
                    {  //calc fail
                        rc[(int)CalcsEval.Calc_Eval] = "FAIL";
                        if (ecalcv.Text.Contains("ERROR"))
                        {
                            //get the error msg
                            rc[(int)CalcsEval.Calc_Eval] = "FAIL";
                            rc[(int)CalcsEval.Time_Eval] = "FAIL";

                        }
                        else
                        {
                            //calc number mismatch
                            IWebElement ecalci = FindElementsAtrib(By.TagName("span"), "class", "BodyCopy");
                            if (ecalci != null)
                            {
                                rc[(int)CalcsEval.Time_Result] = ecalci.Text;
                                if (ecalci.Text == timeresult)
                                {
                                    rc[(int)CalcsEval.Time_Eval] = "PASS";
                                }
                                else
                                {
                                    rc[(int)CalcsEval.Time_Eval] = "FAIL";
                                }

                            }
                            else
                            {
                                //get the error msg
                                rc[(int)CalcsEval.Time_Eval] = "FAIL";
                            }

                        }

                    }
                }

            }

            return rc;
        }


        public static IWebElement setAMPM(string ampm, string entryleave)
        {
            IWebElement rc = null;
            //Choose Entry Date and Time
            //Choose Leaving Date and Time
            string startend = "EntryTime";
            if (entryleave.Contains("Choose Leaving"))
            {
                startend = "ExitTimeAMPM";
            }
            else if (entryleave.Contains("Choose Entry"))
            {
                startend = "EntryTimeAMPM";
            }
            //<input name="EntryTimeAMPM" type="radio" value="AM" checked="">
            IWebElement etime = FindElementsAtrib(By.TagName("input"), "name", startend, null, "value", ampm);
            if (etime != null)
            {
                etime.Click();
                Thread.Sleep(200);
                rc = etime;
            }
            return rc;
        }
        public static IWebElement setTime(string time, string entryleave)
        {
            IWebElement rc = null;
            //Choose Entry Date and Time
            //Choose Leaving Date and Time
            string startend = "EntryTime";
            if (entryleave.Contains("Choose Leaving"))
            {
                startend = "ExitTime";
            }
            else if (entryleave.Contains("Choose Entry"))
            {
                startend = "EntryTime";
            }
            //<input name="EntryTime" type="text" id="EntryTime" value="12:00" size="10">
            IWebElement etime = FindElementsAtrib(By.TagName("input"), "id", startend);
            if (etime != null)
            {
                etime.Clear();
                etime.SendKeys(time);
                rc = etime;
            }
            return rc;
        }
        public static IWebElement FindElementsAtrib(By by, string atrib, string findtxt, IWebElement FromElement = null, string atrib1 = "", string find1 = "")
        {
            IWebElement rc = null;
            var elList = Browser.FindElements(by);
            if (FromElement != null)
            {
                elList = FromElement.FindElements(by);
            }

            var s = "";
            foreach (IWebElement eitem in elList)
            {
                if (atrib.Length > 0 && findtxt.Length > 0)
                {
                    s = eitem.GetAttribute(atrib);
                    if (s.Length > 0 && s.Contains(findtxt))
                    {
                        if (atrib1.Length > 0 && find1.Length > 0)
                        {
                            s = eitem.GetAttribute(atrib1);
                            if (s == find1)
                            {
                                rc = eitem;
                                break;
                            }
                        }
                        else
                        {
                            rc = eitem;
                            break;
                        }

                    }
                }
                else if (findtxt.Length > 0)
                {
                    if (eitem.Text == findtxt)
                    {
                        rc = eitem;
                        break;
                    }
                }
            }

            return rc;
        }

        public static bool setToManHwnd(string title)
        {
            bool rc = false;
            Browser.SwitchTo().Window(ParkingCalcDriver.mainhwnd);
            if (Browser.Title == title)
            {
                ParkingCalcDriver.currenthwnd = ParkingCalcDriver.mainhwnd;
                rc = true;

            }
            return rc;
        }
        public static bool setToPopupWin(string title)
        {
            bool rc = false;
            ParkingCalcDriver.currenthwnd = Browser.CurrentWindowHandle;
            ParkingCalcDriver.mainhwnd = ParkingCalcDriver.currenthwnd;
            foreach (var hwnd in Browser.WindowHandles)
            {
                if (hwnd != ParkingCalcDriver.currenthwnd)
                {
                    Browser.SwitchTo().Window(hwnd);
                    if (Browser.Title == title)
                    {
                        ParkingCalcDriver.currenthwnd = hwnd;
                        rc = true;
                        break;
                    }

                }
            }
            return rc;
        }

        public static IWebElement SetCalendar(int year, int mn, int day, string entryleave)
        {
            IWebElement rc = null;
            //Choose Entry Date and Time
            //Choose Leaving Date and Time
            string startend = "EntryTime";
            if (entryleave.Contains("Choose Leaving"))
            {
                startend = "ExitDate";
            }
            else if (entryleave.Contains("Choose Entry"))
            {
                startend = "EntryDate";
            }
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            if (mn < 1)
            {
                Console.WriteLine("Invalid month index of less than 1");
                return null;
            }
            string month = months[mn - 1];
            IWebElement cal = FindElementsAtrib(By.TagName("a"), "href", startend);
            if (cal == null)
            {
                Console.WriteLine("Error: Calendar not found");
                return null;
            }
            //invoke cal
            cal.Click();
            Thread.Sleep(1000);
            //set to window
            if (!setToPopupWin("Pick a Date"))
            {
                Console.WriteLine("Error: Calendar popup not found");
                return null;
            }
            //set year
            //body > form > table > tbody > tr:nth-child(1) > td > table > tbody > tr > td:nth-child(2)
            cal = Browser.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(1) > td > table > tbody > tr > td:nth-child(2)"));
            //< a href = "javascript:winMain.Cal.DecYear();winMain.RenderCal()" >< b >< font color = "#00306C" > &lt;</ font ></ b ></ a >
            //body > form > table > tbody > tr:nth-child(1) > td > table > tbody > tr > td:nth-child(2) > a:nth-child(1)
            IWebElement decyr = FindElementsAtrib(By.TagName("a"), "href", "Cal.DecYear()", cal);
            if (decyr.Text != "<")
            {
                Console.WriteLine("Year decreament not found");
                return null;
            }
            //body > form > table > tbody > tr:nth-child(1) > td > table > tbody > tr > td:nth-child(2) > a:nth-child(3)
            IWebElement incyr = FindElementsAtrib(By.TagName("a"), "href", "Cal.IncYear()", cal);
            if (incyr.Text != ">")
            {
                Console.WriteLine("Year increament not found");
                return null;
            }
            //body > form > table > tbody > tr:nth-child(1) > td > table > tbody > tr > td:nth-child(2) > a:nth-child(2)
            IWebElement yr = cal.FindElements(By.TagName("b"))[1];
            int nyr = 0;
            Int32.TryParse(yr.Text, out nyr);
            while (nyr != year)
            {
                if (nyr < year)
                {
                    incyr = FindElementsAtrib(By.TagName("a"), "href", "Cal.IncYear()", cal);
                    incyr.Click();
                    Thread.Sleep(200);
                }
                else if (nyr > year)
                {
                    decyr = FindElementsAtrib(By.TagName("a"), "href", "Cal.DecYear()", cal);
                    decyr.Click();
                    Thread.Sleep(200);
                }
                else
                {
                    break;
                }
                cal = Browser.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(1) > td > table > tbody > tr > td:nth-child(2)"));
                yr = yr = cal.FindElements(By.TagName("b"))[1];
                Int32.TryParse(yr.Text, out nyr);
            }
            //set month
            //body > form > table > tbody > tr:nth-child(1) > td > table > tbody > tr > td:nth-child(1) > select
            cal = Browser.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(1) > td > table > tbody > tr > td:nth-child(1) > select"));
            cal.Click();
            Thread.Sleep(200);
            decyr = FindElementsAtrib(By.TagName("option"), "", month);

            if (decyr != null)
            {
                decyr.Click();
                Thread.Sleep(200);
            }
            else
            {
                Console.WriteLine("Found month");
            }
            //set day

            //<a href="javascript:winMain.document.getElementById('EntryDate').value='9/3/2017';;window.close();">3</a>
            string dt = mn.ToString() + "/" + day.ToString() + "/" +
                        year.ToString();
            string find = @"('" + startend + "').value='" + dt + "'";
            decyr = FindElementsAtrib(By.TagName("a"), "href", find);
            if (decyr != null)
            {
                decyr.Click();
                Thread.Sleep(200);
            }
            if (ParkingCalcDriver.setToManHwnd("Parking Calculator"))
            {
                //<input name="EntryDate" type="text" id="EntryDate" value="MM/DD/YYYY" size="15">
                decyr = FindElementsAtrib(By.TagName("input"), "name", startend);
                var ss = decyr.GetAttribute("value");
                if (ss == dt)
                {
                    rc = decyr;
                }
            }
            //cal closes
            //switch to main hwnd
            //set date field
            //return the target date field
            return rc;
        }

        //static [IWebElement] FindAllTags([Sting] taglist)
        //{

        //}
        //static void WaitUntilDocumentIsReady(TimeSpan timeout)
        //{
        //    var javaScriptExecutor = Browser as IJavaScriptExecutor;
        //    var wait = new WebDriverWait(Browser, timeout);
        //    wait.Until(javaScriptExecutor.ExecuteScript(
        //        @"return (document.readyState == 'complete' && jQuery.active == 0)"));
        //    //Check if document is ready
        //    //Func<OpenQA.Selenium.IWebDriver, bool> readyCondition = webDriver => javaScriptExecutor.ExecuteScript(@"return (document.readyState == 'complete' && jQuery.active == 0)");
        //    //wait.Until(readyCondition);
        //}
        public static bool WaitForPageLoad(IWebDriver driver)
        {
            WaitForDocumentReady(driver);
            bool ajaxReady = WaitForAjaxReady(driver);
            WaitForDocumentReady(driver);

            return ajaxReady;
        }
        private static void WaitForDocumentReady(IWebDriver driver)
        {

            var timeout = new TimeSpan(0, 0, 30);
            var wait = new WebDriverWait(driver, timeout);

            var javascript = driver as IJavaScriptExecutor;
            if (javascript == null)
                throw new ArgumentException("driver", "Driver must support javascript execution");

            wait.Until((d) =>
            {
                try
                {
                    string readyState = javascript.ExecuteScript(
                        "if (document.readyState) return document.readyState;").ToString();
                    return readyState.ToLower() == "complete";
                }
                catch (InvalidOperationException e)
                {
                    //Window is no longer available
                    return e.Message.ToLower().Contains("unable to get browser");
                }
                catch (WebDriverException e)
                {
                    //Browser is no longer available
                    return e.Message.ToLower().Contains("unable to connect");
                }
                catch (Exception)
                {
                    return false;
                }
            });

        }
        private static bool WaitForAjaxReady(IWebDriver driver)
        {
            Thread.Sleep(1000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            return wait.Until<bool>((d) =>
            {
                return driver.FindElements(By.CssSelector(".waiting, .tb-loading")).Count == 0;
            });
        }


    }
}
