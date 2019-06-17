Feature: ComplianceDataConsolidation
	Upload a Lift assessment upload file


@smoke
Scenario: Upload a lift assessment
	Given I have navigated to the application
	And I click on the uploads button
    And I choose to upload a valid Action update Assessment file

