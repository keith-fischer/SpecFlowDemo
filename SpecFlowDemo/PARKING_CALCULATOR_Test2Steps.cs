using System;
using TechTalk.SpecFlow;

namespace SpecFlowDemo
{
    [Binding]
    public class PARKING_CALCULATOR_Test2Steps
    {
        [Given(@"I click on the Calendar Icon in the ""(.*)"" section")]
        public void GivenIClickOnTheCalendarIconInTheSection(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I select ""(.*)"" in the new window that appears")]
        public void GivenISelectInTheNewWindowThatAppears(string p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
