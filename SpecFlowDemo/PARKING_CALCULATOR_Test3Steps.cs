using System;
using TechTalk.SpecFlow;

namespace SpecFlowDemo
{
    [Binding]
    public class PARKING_CALCULATOR_Test3Steps
    {
        [Given(@"I enter the date of ""(.*)"" in the ""(.*)"" section")]
        public void GivenIEnterTheDateOfInTheSection(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
