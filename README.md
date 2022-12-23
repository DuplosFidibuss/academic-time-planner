# academic-time-planner

Prototype of a time planning application designed for teachers and students.

## General note

The ATP currently can only be executed on Windows. Cross-platform functionality will be added in future versions.

## Preparing for use

### Create Toggl account

The only time tracking API which is currently supported is [Toggl Track](https://toggl.com/track/). Therefore, users must create a Toggl account in order to use the ATP. To do so, click on the link above and sign up as described on the page that will open up.

### Install .NET

The ATP runs on .NET 6.0 which can be installed from [here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0). Under SDK 6.x.x in the upper left, in the "Windows" row of the table, select the x64 or the x86 installer, depending on your system architecture. Execute the installer on your machine.

## Download and install

The software can be downloaded directly from the [ATP GitHub repository](https://github.com/DuplosFidibuss/academic-time-planner). If you want to install the main branch just go to Code -> Download ZIP. If you want to install an official release version go to tags, choose the tag you want to install from and click on "zip". The recommended version to use at the moment is release 0.1.0. Save the zip file to a location of your choice and extract it.

## Run the ATP

1. Open a command prompt (cmd).
2. Navigate to the directory into which you extracted the zip file in the previous step. Navigate into the directory and further into "AcademicTimePlanner".
3. Execute `dotnet run`. If you execute it for the first time, the project will build first before executing.
4. If you are prompted with an error message indicating that workloads must be installed, run `dotnet workload restore`.
5. If you see an output like `Workload installation failed: One or more errors occurred. (No NuGet sources are defined or enabled)` during step 4, run `dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org`, then repeat step 4.
6. Close the command prompt and repeat steps 1 to 3. The project will now build normally, and after a few seconds you should see an output like this:

```batch
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

7. Now, open a browser window and go to [https://localhost:7034]. The application will be loaded.
8. You can terminate the application at any time by pressing Ctrl+C in the command prompt.

## For developers

### Install Visual Studio

The ATP has been developed using Visual Studio. If you want to contribute to the project we recommend you use Visual Studio as well. To install it click on the link provided [here](https://visualstudio.microsoft.com/) and follow the instructions. In the installer make sure you select the category "ASP.NET and web development" before installing Visual Studio. If you have already installed Visual Studio, open the Visual Studio Installer, click on "Modify" and make sure the catergory "ASP.NET and web development" is selected in the window that shows up. If it is not, select it, click on "Modify" in the bottom right and wait for the installation to complete before continuing.

### Open the ATP in Visual Studio

To open the ATP source code in Visual Studio open Visual Studio and select "Open a project or solution". In the dialog which shows up navigate to the ATP project directory where the .sln file is located. Select the .sln file and proceed. The ATP solution consisting of AcademicTimePlanner and AcademicTimePlanner.Tests will be loaded into Visual Studio.

### Run the ATP from Visual Studio

To run the ATP from Visual Studio click on the button "AcademicTimePlanner" with the start symbol. The application will build and run and the browser window should open on its own. Alternatively, you can right click on the AcademicTimePlanner project in the solution explorer and select Debug -> Start new instance.

### Run the ATP unit tests in Visual Studio

To run the ATP unit tests right click on the AcademicTimePlanner.Tests project in the solution explorer and select "Run Tests" or "Debug Tests". The test project will build and run. You should see the short result of the test execution in the bottom left corner of the Visual Studio window. To see more details, go to Test -> Test Explorer. The test explorer will open, and you will be able to inspect the results of the individual tests there.
