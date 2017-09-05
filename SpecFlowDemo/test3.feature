Feature: PARKING_CALCULATOR_Test3


@mytag

Scenario: PARKING_CALCULATOR_Test3
Given that I navigate with browser to "http://adam.goucher.ca/parkcalc/index.php"
   And I select the "Short-Term Parking" option in the "Choose a Lot" dropdown
   And I enter the date of "01/02/2014" in the "Choose Entry Date and Time" section
   And I enter the date of "01/01/2014" in the "Choose Leaving Date and Time" section
When I click button "Calculate"
Then the "COST" is equal to "ERROR! YOUR EXIT DATE OR TIME IS BEFORE YOUR ENTRY DATE OR TIME"