using System;
using TechTalk.SpecFlow;

namespace SpecFlowDemo
{
    [Binding]
    public class PARKING_CALCULATOR_Test1_Steps
    {
        private string[] results = null;
        [Given(@"that I navigate with browser to ""(.*)""")]
        public void GivenThatINavigateWithBrowserTo(string p0)
        {
            //ScenarioContext.Current.Pending();
            //SeleniumFW se=new SeleniumFW("chrome",p0,true);
            //Console.WriteLine(se.BrowserDriver.Title);
            //ScenarioContext.Current.Pending();
            ParkingCalcDriver.InitBrowser();
        }
        
        [Given(@"I select the ""(.*)"" option in the ""(.*)"" dropdown")]
        public void GivenISelectTheOptionInTheDropdown(string p0, string p1)
        {
            ParkingCalcDriver.chooseLot(p0);
        }
        
        [Given(@"I enter time of ""(.*)"" and date of ""(.*)"" in the ""(.*)"" section")]
        public void GivenIEnterTimeOfAndDateOfInTheSection(string p0, string p1, string p2)
        {//Choose Entry Date and Time
            ParkingCalcDriver.setTime(p0, p2);
            string[] sdt = p1.Split(new char[]{ '/'},StringSplitOptions.None);
            int[] dt = Array.ConvertAll(sdt, int.Parse);
            ParkingCalcDriver.SetCalendar(dt[2], dt[0], dt[1], p2);
        }
        
        [Given(@"I select the ampm of ""(.*)"" option in the ""(.*)"" section")]
        public void GivenISelectTheAmpmOfOptionInTheSection(string p0, string p1)
        {
            ParkingCalcDriver.setAMPM(p0, p1);
        }
        
        [When(@"I click button ""(.*)""")]
        public void WhenIClickButton(string p0)
        {//	$ 2.00        (0 Days, 1 Hours, 0 Minutes)
            results = ParkingCalcDriver.DoCalcs("$ 2.00", "(0 Days, 1 Hours, 0 Minutes)");
            Console.WriteLine(string.Join("|",results));
        }
        
        [Then(@"the ""(.*)"" is equal to ""(.*)""")]
        public void ThenTheIsEqualTo(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the duration of stay is equal to ""(.*)""")]
        public void ThenTheDurationOfStayIsEqualTo(string p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
