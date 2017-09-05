Feature: PARKING_CALCULATOR_Test2


@mytag

Scenario: PARKING_CALCULATOR_Test2
Given that I navigate with browser to "http://adam.goucher.ca/parkcalc/index.php"
   And I select the "Long-Term Surface Parking" option in the "Choose a Lot" dropdown
   And I click on the Calendar Icon in the "Choose Entry Date and Time" section
   And I select "01/01/2011" in the new window that appears
   And I click on the Calendar Icon in the "Choose Leaving Date and Time" section
   And I select "02/01/2011" in the new window that appears

When I click button "Calculate"
Then the "COST" is equal to "$ 270.00"
   And the duration of stay is equal to "(31 Days, 0 Hours, 0 Minutes)"
   And I close the Browser
