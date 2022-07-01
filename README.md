# Teams Meeting Scheduler Website

The code published here can be used to deploy an Azure Web App that can allow users of your Microsoft 365 tenant to schedule Teams meetings using a simple web interface. 
This could be very useful when your mail system is different from Exchange / Exchange Hybrid deployment and/or your email client calendar is not integrated.



## Setup & Prerequisites
Using the steps below you will be able to create all prerequisites and resources required by the solution.


### Register the required application for Graph API usage
	1. Open a browser and navigate to the Azure Active Directory admin center. Login using a personal account (aka: Microsoft Account) or Work or School Account.

	2. Select Azure Active Directory in the left-hand navigation, then select App registrations under Manage.

	3. Select New registration. On the Register an application page, set the values as follows.
	- Set Name to name of your app.
	- Set Supported account types to Accounts in any organizational directory and personal Microsoft accounts.
	- Under Redirect URI, set the first drop-down to Web and set the value to https://localhost:5001/.

	4. Select Register. On the web page, copy the value of the Application (client) ID and save it, you will need it in the next step.

	5. Select Authentication under Manage. Under Redirect URIs add a URI with the value https://your_application_URL.TLD/signin-oidc.

	6. Set the Logout URL to https://your_application_URL.TLD/signout-oidc.

	7. Locate the Implicit grant section and enable ID tokens. Select Save.

	8. Select Certificates & secrets under Manage. Select the New client secret button. Enter a value in Description and select one of the options for Expires and select Add.

	9. Copy the client secret value before you leave this page. You will need it in the next step.

	> IMPORTANT: This client secret is never shown again, so make sure you copy it now.



### Authorize app access to Azure Active Directory

	1. From the created app, select from left menu of the app created API Authorization.
	2. Add Graph Authorization: OnlineMeetings.ReadWrite and Calendars.ReadWrite
	3. Provide Admin consent for the authorizations


## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

## Trademarks

This project may contain trademarks or logos for projects, products, or services. Authorized use of Microsoft 
trademarks or logos is subject to and must follow 
[Microsoft's Trademark & Brand Guidelines](https://www.microsoft.com/en-us/legal/intellectualproperty/trademarks/usage/general).
Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion or imply Microsoft sponsorship.
Any use of third-party trademarks or logos are subject to those third-party's policies.
