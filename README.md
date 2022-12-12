# academic-time-planner

Prototype of a time planning application designed for teachers and students.

## General note

The ATP currently can only be executed on Windows. Cross-platform functionality will be added in future versions.

## Preparing for use

### Create Toggl account

The only time tracking API which is currently supported is [Toggl Track](https://toggl.com/track/). Therefore, users first must create a Toggl account in order to use the ATP. To do so, click on the link above and sign up as described on the page that will open up.

### Install .NET

The ATP runs on .NET, which can be installed following the instructions provided [here](https://learn.microsoft.com/en-us/dotnet/core/install/windows?tabs=net70).

## Download and install

The software can be downloaded directly from the [ATP GitHub repository](https://github.com/DuplosFidibuss/academic-time-planner). If you want to install the main branch, just go to Code -> Download ZIP. If you want to install an official release version, go to tags, choose the tag you want to install from and click on "zip". Save the zip file to a location of your choice and extract it.

## Run the ATP

Open a command prompt (cmd) and navigate to the directory into which you extracted the zip file in the previous step. Navigate into the directory and further into "AcademicTimePlanner". There, execute `dotnet run`. If you execute it for the first time, the project will build first before executing. However, after a few seconds, you should see an output like this:

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7034
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5299
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\path\to\ATP\AcademicTimePlanner
```

Now, open a browser window and go to [https://localhost:7034]. The application will be loaded.

You can terminate the application at any time by typing Ctrl-C in the command prompt.

## For developers

### Install Visual Studio

The ATP has been developed using Visual Studio. If you want to contribute to the project, we recommend you to use Visual Studio as well. To install it, click on the link provided [here](https://visualstudio.microsoft.com/) and follow the instructions. In the installer, make sure you select the category "ASP.NET and web development" before installing Visual Studio.

### Open the ATP in Visual Studio

To open the ATP source code in Visual Studio, open Visual Studio and select "Open a project or solution". In the dialog which shows up, navigate to the ATP project directory where the .sln file is located. Select the .sln file and proceed. The ATP solution, consisting of AcademicTimePlanner and AcademicTimePlanner.Tests will then load so that you can edit the source code in Visual Studio.

### Run the ATP from Visual Studio

To run the ATP from Visual Studio, either click on the button "AcademicTimePlanner" with the start symbol. The application will build and run, and the browser window should open on its own. Alternatively, you can right click on the AcademicTimePlanner project in the solution explorer and select Debug -> Start new instance.

### Run the ATP unit tests in Visual Studio

To run the ATP unit tests, right click on the AcademicTimePlanner.Tests project in the solution explorer and select "Run Tests" or "Debug Tests". The test explorer will open and after a few moments you will see the results of the tests there.
