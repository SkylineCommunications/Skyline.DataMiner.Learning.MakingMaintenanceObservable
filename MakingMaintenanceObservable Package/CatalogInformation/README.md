# Making Scripts Interactive and Observable

This package contains a complete, working example of an interactive automation script that demonstrates how to create embedded script interfaces in DataMiner Low-Code Apps using the Interactive Automation Script (IAS) Toolkit.

**What's included:**

- A fully functional maintenance window management script
- A Low-Code App demonstrating the Interactive Automation Script component
- Ready-to-deploy implementation showcasing the IAS Toolkit

You can deploy this package to immediately see and use the complete implementation, or follow the step-by-step tutorial in the [GitHub repository](https://github.com/SkylineCommunications/Skyline.DataMiner.Learning.MakingMaintenanceObservable) to build it yourself.

## Overview

The project teaches you how to build interactive automation scripts that can be embedded in Low-Code Apps as components. Through a practical example of a maintenance window management system, you'll learn how to:

- Create interactive dialogs with various UI components (labels, buttons, calendars, dropdowns, text boxes)
- Implement the Model-View-Presenter (MVP) pattern for automation scripts
- Handle user interactions and events
- Manage application state with in-memory storage
- Integrate scripts as components in DataMiner Low-Code Apps

## What You'll Build

A **Maintenance Window Management** application that allows users to:

- View maintenance windows for devices
- Add new maintenance windows
- Edit existing maintenance windows
- Delete maintenance windows with confirmation dialogs

![Maintenance Window Overview](./Images/maintenance-overview.png)

The application demonstrates real-world patterns for building interactive automation scripts that provide a seamless user experience within Low-Code Apps.

![Interactive Maintenance Dialog](./Images/maintenance-dialog.png)

## Prerequisites

- Visual Studio 2022 or Visual Studio 2026
- DataMiner Integration Studio (DIS) extension
- Access to a DataMiner system (DaaS or on-premise)
- Basic knowledge of C# and DataMiner automation scripts

## Getting Started

1. **Clone the repository**

   ```bash
   git clone https://github.com/SkylineCommunications/Skyline.DataMiner.Learning.MakingMaintenanceObservable.git
   cd Skyline.DataMiner.Learning.MakingMaintenanceObservable
   ```

2. **Select the appropriate branch**
   - `hands-on`: Starting point for following the tutorial
   - `main`: Complete solution with all features implemented

3. **Open the solution**
   - Open `Skyline.DataMiner.Learning.MakingMaintenanceObservable.sln` in Visual Studio

4. **Follow the guide**
   - See `Guide.md` for detailed step-by-step instructions
   - The guide walks you through implementing all features from scratch

## Key Concepts

### Interactive Automation Script Toolkit

This project uses the [Skyline.DataMiner.Utils.InteractiveAutomationScriptToolkit](https://www.nuget.org/packages/Skyline.DataMiner.Utils.InteractiveAutomationScriptToolkit) NuGet package, which provides:

- Pre-built UI widgets (buttons, labels, text boxes, calendars, dropdowns)
- Dialog management and navigation
- Event handling for user interactions
- Consistent styling and layout

### Making Scripts Interactive

To make a script interactive and embeddable in Low-Code Apps:

1. Add the `<Interactivity>Always</Interactivity>` element to the script XML (DataMiner 10.5.9+)
2. Use the IAS Toolkit to build dialogs and handle user interactions
3. Configure the script as an "Interactive Automation Script" component in Low-Code Apps

## Learning Path

1. **Start with the hands-on branch** - Clone and checkout the `hands-on` branch for a guided learning experience
2. **Follow the Guide** - Work through [Guide.md](https://github.com/SkylineCommunications/Skyline.DataMiner.Learning.MakingMaintenanceObservable/blob/HEAD/Guide.md) step by step
3. **Build incrementally** - Each section builds upon the previous one
4. **Test frequently** - Publish to your DataMiner system and test in a Low-Code App
5. **Compare with main** - Check the `main` branch to see the complete implementation

## Note on Data Persistence

The data in this learning project is stored **in memory only**. When the script restarts, all changes are lost and the original demo data is restored. This is intentional for learning purposes - in a production scenario, you would persist data to a database, parameters, or other storage.

## Contributing

This is a learning resource maintained by Skyline Communications. For issues or suggestions, please refer to the [GitHub repository](https://github.com/SkylineCommunications/Skyline.DataMiner.Learning.MakingMaintenanceObservable).

## License

This project is provided as a learning resource for the DataMiner community and is licensed under the MIT License - see the [LICENSE](https://github.com/SkylineCommunications/Skyline.DataMiner.Learning.MakingMaintenanceObservable/blob/HEAD/LICENSE) file for details.
