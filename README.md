# Weather Api Exercise - A .Net Core Web Api for Retrieving Climate Information

## Introduction
### Approach

The approach I used to solve this problem is a relatively simple. It essentially demonstrates how to use .Net Core to build a standard RESTful Api which uses offers meaning responses codes, caching, and a Json data format. As well the project demonstrates the following .Net Core features...

* .Net Core Dependency Injection
* In-Memory Cache (in a prod/real world scenario would be swapped for distributed)
* .Net Core Json Configuration Deserialized to POCO
* Usage of some popular .Net Core third party libraries such as RestSharp, Automapper, Autofac, etc...

Note: I've opted not to include any code comments in the soltion. This is not a common practice for me, but the code is fairly simple and should be relatively self-documenting. Please email me back with any questions if any part of the implementation is unclear.

Note on Caching: Each external service call caches it's resulting data set. The main call to the weather service is cached every 5 minutes. 
I figured the weather does not generally change to drastically within a 5 minute window, although each cache is configurable so the window can be easily modified.

## Building and Running

### Prerequisites
* The program is built using .Net Core version v2.1.4 (https://www.microsoft.com/net/download/dotnet-core/2.1)

* To build and run, it's best to use Visual Studio Code (The IDE that I used to develop the code). You should be able to download the IDE on Windows, Mac, and/or Linux (deb or rpm) from the link here (https://code.visualstudio.com/).

* After the install. Ensure the correct version of .Net Core is running. Open up a terminal window (you can do this directly in the IDE) and type ```dotnet --version```. The output should read `2.1.403`, if not, install the downloads from the link above.

* For more information on using the dotnet CLI. See (https://docs.microsoft.com/en-us/dotnet/core/tools/?tabs=netcore2x) 

### Build
* If you are using Visual Studio Code, In the *File* menu, open the folder where you cloned this git repository, and change your directory to 'src'.

* Open the integrated terminal in Visual Studio Code. If it's not already showing at the bottom of the IDE, Select *Terminal* from the *View* menu.

* The buld will restore any nuget packages referenced by the project.

### Run - Console
* In the terminal (from the repo root), navigate to the Console app (Campspot.Interview.Console)

* Enter the command ```dotnet run```

### Run - Tests

* To run the tests, it's best to use extensions for Visual Studio Code. I used the *.Net Core Test Explorer* found here (https://github.com/formulahendry/vscode-dotnet-test-explorer).

* I would also recommend using a standard set of Visual Studio extensions. A good list of them can be found here (https://stackify.com/top-visual-studio-code-extensions/).

* For more information on VS Code extensions (https://code.visualstudio.com/docs/editor/extension-gallery). 

## Questions?
Please let me know if you have any trouble building or running the program

* Email me at: matt_dev1@hotmail.com
* www.linkedin.com/in/mattschwartznet

