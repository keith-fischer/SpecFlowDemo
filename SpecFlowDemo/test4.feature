Feature: PARKING_CALCULATOR_Test4


@mytag

Scenario: PARKING_CALCULATOR_Test4
Given that I navigate with browser to "http://adam.goucher.ca/parkcalc/index.php"
   And I select the "Short-Term Parking" option in the "Choose a Lot" dropdown
   And I enter time of "10:00" and date of "01/01/2014" in the "Choose Entry Date and Time" section
   And I select the ampm of "PM" option in the "Choose Entry Date and Time" section
   And I enter time of "11:00" and date of "01/01/2014" in the "Choose Leaving Date and Time" section
   And I select the ampm of "PM" option in the "Choose Leaving Date and Time" section

When I click button "Calculate"
Then the "COST" is equal to "$ 2.00"
   And the duration of stay is equal to "(0 Days, 1 Hours, 0 Minutes)"
   And I close the Browser
