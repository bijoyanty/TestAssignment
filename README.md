# TestAssignment

This repo consists of the solution in .net 8 with tests written using Nunit and playwright.
This solution is compatible with Windos platform

# TestAssignmentUI

## Prerequisites for Running the Tests Locally

Before you can run the tests on your local machine, ensure that you have the following:

- **.NET 8.0 or later**: Download and install the latest .NET SDK from the official [.NET website](https://dotnet.microsoft.com/download).
- **Visual Studio 2022 or later** (Optional): Although not required, Visual Studio is recommended for an enhanced development experience. You can download it from the [Visual Studio website](https://visualstudio.microsoft.com/downloads/).
- **Chrome Browser**: The default browser for the tests is Chrome. If you wish to use a different browser, you'll need to update the `appsettings.json` file as described below.

### Browser Configuration
To run the tests in a different local browser, follow these steps:

1. Navigate to the `appsettings.json` file located at the same level as the project file (e.g., `"TestAssignment/TestAssignmentUI"`).
2. Update the following values in `appsettings.json`:
    - **BrowserName**: Set to the browser you want to use (e.g., `"chromium"`).
    - **Channel**: Set to the browser channel, such as `"msedge"` for Microsoft Edge.

Example:
```json
{
  "TestUrls": {
    "TestSiteUrl": "https://www.eneco.nl"
  },
  "PlaywrightConfig": {
    "BrowserName": "chromium",
    "ExpectTimeout": 5000,
    "LaunchOptions": {
      "Headless": false,
      "Channel": "msedge"
    }
  }
}
```

### Steps for running the Test locally

## Clone the Repository

First, clone the repository to your local machine:

```bash
git clone [Repo](https://github.com/bijoyanty/TestProject)
```

## Steps to run the actual Test

	-Using Visual Studio
		Open the solution in Visual Studio.
		Build the solution to ensure all dependencies are resolved. 
  		Update 
		Go to the Test Explorer (Test -> Test Explorer).
		Run or debug the tests directly from Test Explorer.
	
	-Using Command Line
		You can run tests using the .NET CLI:
		dotnet test
