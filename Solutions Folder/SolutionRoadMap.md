[comment]: # "Initialize Document Variables"

<a name="top"></a>

<h2 align="Center">
The VisualPlus Road Map
</h2>

This is the beginning to a guide line of future developments and the current work in progress of the VisualPlus solution for .NET WinForms. The solution will be split into multiple projects to more easily manage the code. This will also help reduce the file size when only the core API is needed. The librarys also now have a Unit test project attached and will begin to have more tests being added to easily run compatibility and error checks that need to pass.

Solution Projects:
- VisualPlus : This library is the core API used for drawing with GDI+ and holds the primitive base structures needed to create a basic component or control. And also a list of often repeated method utilities.

- VisualPlus.Toolbox : This library will reference the `VisualPlus` core library. And will be a container that collects all the components and controls just more enhanced than your default. That will feature greater customizable and allow further optimizations.

- Visual Theme Builder: A tool developed to easily build and preview your custom themes.

Status:
Splitting the solution will require the projects to be nearly rebuilt. Most of the namespaces have been resolved enough to make it run. But there were are errors loading certain crucial objects that are null and crashing the designer. Requiring Visual Studio restart. :'( So I am manually going over every class and attempting to optimize it better for catching those exceptions, logging them and handling the errors. While also adding documentation to API methods which some utitlity classes were missing or lacking. Sometimes resulting in repetitive code. I am breaking down each class optimizing it and then adding the unit tests for it starting from the primitive bases and will be starting with the rebuilding of button and then components depending on the size of classes. To get the repetition and validation methods stable. Doing this will also bring support for using these librarys under different versions of .NET Framework to be provide further use for more projects. One more thing would be to eventually be able to use the core library to possibly draw on further platforms that support .NET Core. The toolbox is simply just a more visual representation of what you can do with the API in WinForms.

Whats New:
- The solution has been split into 2 projects the core API and then the toolbox components.
- Borders have been optimized and given some styling options like custom dash offsets and patterns.
![Border Style Preview](https://github.com/DarkByte7/VisualPlus/blob/master/docs/0.JPG?raw=true)
- [ ] Refactor logger and exception thrower.

... I will be updating here and adding more as I get the time to work on it since I am planning on using it on some other projects as well.

```
Solution
│   VisualPlus.sln
│   README.md 
│   LICENSE.md 
│
├─── .github
│   ├── CONTRIBUTING.md
│   ├── FUNDING.yml
│   ├── PULL_REQUEST.md
│   │
│   └── ISSUE_TEMPLATE
│           ├── BUG_REPORT.md
│           └── FEATURE_REQUEST.md
├─── .nuget
│   ├── VisualPlus.1.0.0.nupkg
│   ├── VisualPlus.Toolbox.1.0.0.nupkg
│   ├── Pack.bat
│   ├── Pack.ps1
|   ├── Pack.sh
|   ├── Publish.bat
|   ├── Publish.ps1
│   └── Publish.sh
│
├──── docs
│   ├── index.html
│   │
│   ├─── Resources
│   │       │
│   │       ├── Documents
│   │       │       ├── DOCUMENTATION.md
│   │       │       └── ROAD-MAP.md
│   │       │       
│   │       ├─── Images
│   │       │       │
│   │       │       └── ...
│   │       │
│   │       └── Scripts
│   │              │
│   │              ├── CSS
│   │              ├── JavaScript
│   │              └── PHP
│   │
|   └─── Sites
│          └─ 404.html
│
├───src
│   │
│   ├───── Tools
│   │       │
│   │       └──── Build
│   │               ├─── Build.bat
│   │               ├─── Build.ps1
│   │               ├─── Build.sh
│   │               └─── BUILD-INSTRUCTIONS.md
│   │
│   └───── Projects
│           │
|           ├─── VisualPlus
|           ├─── VisualPlus.Toolbox
|           ├─── VisualThemeBuilder
|           ├─── Types
|           └─── UnitTests
└──────
```
