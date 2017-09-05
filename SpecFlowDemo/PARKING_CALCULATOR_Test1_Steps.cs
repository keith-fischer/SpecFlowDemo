using System;
using TechTalk.SpecFlow;

namespace SpecFlowDemo
{
    [Binding]
    public class PARKING_CALCULATOR_Test1_Steps
    {
        [Given(@"that I navigate with browser to ""(.*)""")]
        public void GivenThatINavigateWithBrowserTo(string p0)
        {
            //ScenarioContext.Current.Pending();
            //SeleniumFW se=new SeleniumFW("chrome",p0,true);
            //Console.WriteLine(se.BrowserDriver.Title);
            //ScenarioContext.Current.Pending();
            ParkingCalcDriver.testClass();
        }
        
        [Given(@"I select the ""(.*)"" option in the ""(.*)"" dropdown")]
        public void GivenISelectTheOptionInTheDropdown(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I enter time of ""(.*)"" and date of ""(.*)"" in the ""(.*)"" section")]
        public void GivenIEnterTimeOfAndDateOfInTheSection(string p0, string p1, string p2)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I select the ampm of ""(.*)"" option in the ""(.*)"" section")]
        public void GivenISelectTheAmpmOfOptionInTheSection(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I click button ""(.*)""")]
        public void WhenIClickButton(string p0)
        {
            ScenarioContext.Current.Pending();
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
