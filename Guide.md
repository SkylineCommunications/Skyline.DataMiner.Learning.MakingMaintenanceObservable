# Making scripts interactive and observable - Hands-on Guide

## The interactive script

1. Clone the GitHub repo on your system and select the "hands-on" branch.
1. Open `Skyline.DataMiner.Learning.MakingMaintenanceObservable.sln` in Visual Studio 2022 or Visual Studio 2026.
1. Verify that the script is interactive:

   1. Select `Manage Maintenance (Learning).xml` in the `Manage Maintenance (Learning)` script project.
   1. Verify that the `<Interactivity>` element is set to *Always*.
      - This element was added in DataMiner 10.5.9.
      - By default this element will be set to *Auto*. The other options are explained in [InteractivityOptions on DataMiner Docs](https://docs.dataminer.services/develop/schemadoc/Automation/InteractivityOptions.html#content-type)
      - Before this DataMiner 10.5.9 [the following comment](https://github.com/SkylineCommunications/Skyline.DataMiner.Utils.InteractiveAutomationScriptToolkit/blob/10.5.8.X/README.md?plain=1#L28) needs to be added in the run method of the script:

        ```csharp
        // engine.ShowUI();
        ```

1. Link the Toolkit (already linked):

   1. Be sure to select the `Manage Maintenance (Learning)` script project.
   1. Click *Menu* > *Manage Nuget Packages*
   1. Verify that the [Skyline.DataMiner.Utils.InteractiveAutomationScriptToolkit](https://www.nuget.org/packages/Skyline.DataMiner.Utils.InteractiveAutomationScriptToolkit) NuGet is selected.
      ![Toolkit NuGet In Script Project](image/Guide/ToolkitNuGetInVSProject.png)
      > **Keep in mind** to select the matching version of the NuGet. The version and the description of NuGet will tell which minimum DataMiner version is required.

## Publish the script

1. Add your DaaS to DIS:
   1. *Extensions* > *DIS* > *Settings*
   1. In *DMA* tab *Add* your DaaS.
      ![Add DaaS to DIS](image/Guide/AddDaaSToDis.png)
   1. Select `MakingMaintenanceObservable Package.xml` the `MakingMaintenanceObservable Package` script project.
   1. Click the down arrow next to publish to select your DaaS DMA.
      ![Publish To DaaS](image/Guide/PublishToDaaS.png)
      Note: During your session this DMA should remain the selected one when clicking *Publish*.

1. Open the *Manage maintenance windows (Hands-On)* app:
   1. In the app you will see a text mentioning that the component should be replaced with the 'Interactive Automation Script' component.
   1. Duplicate the app, using the option when clicking the *...* icon in the upper right corner.
      ![Duplicate packaged app](image/Guide/DuplicatePackagedLca.png)
   1. Rename the app, by selecting the name in the top bar, from *Manage maintenance windows (Hands-on) (1)* to *Manage maintenance windows*.
   1. Remove the text component.
   1. Hover over the blue bar with '+' on the left end to add a 'Interactive Automation Script' component.
      ![Select the 'Interactive Automation Script' component](image/Guide/AddIasComponentToLca.png)
   1. Resize the 'Interactive Automation Script' component so it fills the entire page.
   1. Select the component, in *Component* > *Settings* select the *Manage Maintenance (Learning)* script.
   1. Publish the app.

1. In the app select a *Encoder A1*.
The Manage Maintenance (Learning) already shows an overview over the maintenance windows for a device that you have selected.

![Scripts initial version in app](image/Guide/ScriptInitialVersionInLca.png)

## Keep in mind

- When an interactive automation script is opened in a pop-up it will have a title bar and a close button. The 'Interactive Automation Script' component does not have those.

- The main dialog already has add, edit and delete buttons. When the script has been published using the solution in the 'hands-on', nothing will happen when these buttons are clicked. We will add that functionality in the next chapter.

- The data available in the script **is stored in memory**. When the script is restarted the original data will be there again.

## Add a dialog maintain a Maintenance Window

### Goal

We will add a new dialog to the `Manage Maintenance (Learning)` script that will allow editing or adding a maintenance window.

![Maintenance Dialog in the IAS component](image/Guide/MaintenanceDialogInLca.png)

The goal is to:

- Add a dialog. The dialog should have:

  - Two labels to display the name and the description of the **device**.
  - For the maintenance window properties:
    - A label and a calendar or time component for the **start** time.
    - A label and a calendar or time component for the **end** time.
    - A label and a textbox component for the **description**.
    - A label and a textbox component for the **type**.
  - A Save and a cancel button

- In `ManageMaintenanceController.cs`

  - Hook up the dialog to edit and add a maintenance window.
  - When deleting a maintenance window ask the user for confirmation, before deleting the maintenance window.
    > To create, update and delete a maintenance window, use the corresponding methods exposed in *repository*.

### The dialog

> A full example of this dialog is available at the end of the chapter.

1. In the `Manage Maintenance (Learning)` project, add a new class. For example `MaintenanceDialog.cs` in `Dialogs/Maintenance`.
   The newly added class should inherit from the `Dialog` class available in the `Skyline.DataMiner.Utils.InteractiveAutomationScript` namespace.

   ```csharp
   namespace Skyline.DataMiner.Learning.MakingMaintenanceObservable.ManageMaintenance.Dialogs.Maintenance
   {
       using System;
       using DeviceMaintenanceApi.Models;
       using Skyline.DataMiner.Automation;
       using Skyline.DataMiner.Utils.InteractiveAutomationScript;
   
       public class MaintenanceDialog : Dialog
       {
           // TODO: Add logic to add or edit a MaintenanceWindow
       }
   }
   ```

1. Prepare the components and the exposed events:

   We will define a list of widgets that need to be available to load and store the data later on.

   For example:

   ```csharp
   // ...
   public class MaintenanceDialog : Dialog
   {
        private readonly Label deviceNameLabel;
        private readonly Label deviceDescriptionLabel;
        private readonly Calendar startDateBox;
        private readonly Calendar endDateBox;
        private readonly TextBox descriptionTextBox;
        private readonly EnumDropDown<MaintenanceWindowType> typeDropDown;
        private readonly EnumDropDown<MaintenanceWindowImpact> impactDropDown;

        public event EventHandler SaveMaintenance;

        public event EventHandler Cancel;
   // ...
   ```

1. When initializing the dialog add the widget to the dialog.

   > Note that in this dialog no widgets need to be repositioned. When that would be the case the dialog should be rebuild on every load. See `MaintenanceOverviewDialog.cs` for an example.

   1. Add the widgets for the name and description of the device. Leave a row of whitespace for the next widgets.
   For example:

      ```csharp
      // ...
      public class MaintenanceDialog : Dialog
      {
          // ...

          public event EventHandler SaveMaintenance;

          public event EventHandler Cancel;

          public MaintenanceDialog(IEngine engine) : base(engine)
          {
              var row = 0;

              // Create and add widgets for device information
              deviceNameLabel = new Label { Style = TextStyle.Title };
              AddWidget(deviceNameLabel, row++, 0, 1, 2);

              deviceDescriptionLabel = new Label();
              AddWidget(deviceDescriptionLabel, row++, 0, 1, 2);

              AddWidget(new WhiteSpace(), row++, 0);

      // ...
      ```

   1. Create and add widgets for maintenance window details.
   For example:

      ```csharp
      // ...
      AddWidget(new WhiteSpace(), row++, 0);

      // Create and add widgets for maintenance window details
      AddWidget(new Label("Start:"), row, 0);
      startDateBox = new Calendar();
      AddWidget(startDateBox, row++, 1);

      AddWidget(new Label("End:"), row, 0);
      endDateBox = new Calendar();
      AddWidget(endDateBox, row++, 1);

      AddWidget(new Label("Description:"), row, 0, verticalAlignment: VerticalAlignment.Top);
      descriptionTextBox = new TextBox { IsMultiline = true };
      AddWidget(descriptionTextBox, row++, 1);

      AddWidget(new Label("Type:"), row, 0);
      typeDropDown = new EnumDropDown<MaintenanceWindowType>();
      AddWidget(typeDropDown, row++, 1);

      AddWidget(new Label("Impact:"), row, 0);
      impactDropDown = new EnumDropDown<MaintenanceWindowImpact>();
      AddWidget(impactDropDown, row++, 1);

      AddWidget(new WhiteSpace(), row++, 0);
      AddWidget(new WhiteSpace(), row++, 0);

      // ...
      ```

   1. Create and add buttons for saving and closing the dialog. The events of the buttons will be linked to the `SaveMaintenance` and `Cancel` events.
      For example:

      ```csharp
      // ...
      public MaintenanceDialog(IEngine engine) : base(engine)
      {
          // ...
          AddWidget(new WhiteSpace(), row++, 0);
          AddWidget(new WhiteSpace(), row++, 0);

          // Create and add buttons for saving and closing the dialog
          var saveButton = new Button("Save") { Style = ButtonStyle.CallToAction, Width = 100 };
          saveButton.Pressed += (sender, args) => SaveMaintenance?.Invoke(this, EventArgs.Empty);
          AddWidget(saveButton, row, 0, HorizontalAlignment.Right);

          var cancelButton = new Button("Cancel") { Width = 100 };
          cancelButton.Pressed += (sender, args) => Cancel?.Invoke(this, EventArgs.Empty);
          AddWidget(cancelButton, row, 1, HorizontalAlignment.Right);
      }
      // ...
      ```

1. Add a method to load the data:
   - Set the text of corresponding labels name and the of device
   - Load the values of the widgets for the `MaintenanceWindow` that gets loaded.
   - If the `MaintenanceWindow` is not available load some default values.
   - Optionally set the title of the dialog.
   - For example:

      ```csharp
      // ...
      public void Load(Device device, MaintenanceWindow window)
      {
          deviceNameLabel.Text = $"Device: {device?.Name}";
          deviceDescriptionLabel.Text = $"> {device?.Description}";

          if (window is null)
          {
              Title = "Add Maintenance Window";
              startDateBox.DateTime = DateTime.Now.AddDays(1);
              endDateBox.DateTime = startDateBox.DateTime.AddHours(2);
              descriptionTextBox.Text = string.Empty;
              typeDropDown.Selected = MaintenanceWindowType.Other;
              impactDropDown.Selected = MaintenanceWindowImpact.Normal;
              return;
          }

          Title = "Edit Maintenance Window";
          startDateBox.DateTime = window.Start;
          endDateBox.DateTime = window.End;
          descriptionTextBox.Text = window.Description;
          typeDropDown.Selected = window.Type;
          impactDropDown.Selected = window.Impact;
      }
      // ...
      ```

1. Add a method to store the date.
   For example:

      ```csharp
      // ...
      public void Store(MaintenanceWindow window)
      {
          window.Start = startDateBox.DateTime;
          window.End = endDateBox.DateTime;
          window.Description = descriptionTextBox.Text;
          window.Type = typeDropDown.Selected;
          window.Impact = impactDropDown.Selected;
      }
      // ...
      ```

#### Full example

```csharp
namespace Skyline.DataMiner.Learning.MakingMaintenanceObservable.ManageMaintenance.Dialogs.Maintenance
{
    using System;
    using DeviceMaintenanceApi.Models;
    using Skyline.DataMiner.Automation;
    using Skyline.DataMiner.Utils.InteractiveAutomationScript;

    public class MaintenanceDialog : Dialog
    {
        private readonly Label deviceNameLabel;
        private readonly Label deviceDescriptionLabel;
        private readonly Calendar startDateBox;
        private readonly Calendar endDateBox;
        private readonly TextBox descriptionTextBox;
        private readonly EnumDropDown<MaintenanceWindowType> typeDropDown;
        private readonly EnumDropDown<MaintenanceWindowImpact> impactDropDown;

        public event EventHandler SaveMaintenance;

        public event EventHandler Cancel;

        public MaintenanceDialog(IEngine engine) : base(engine)
        {
            var row = 0;

            // Create and add widgets for device information
            deviceNameLabel = new Label { Style = TextStyle.Title };
            AddWidget(deviceNameLabel, row++, 0, 1, 2);

            deviceDescriptionLabel = new Label();
            AddWidget(deviceDescriptionLabel, row++, 0, 1, 2);

            AddWidget(new WhiteSpace(), row++, 0);

            // Create and add widgets for maintenance window details
            AddWidget(new Label("Start:"), row, 0);
            startDateBox = new Calendar();
            AddWidget(startDateBox, row++, 1);

            AddWidget(new Label("End:"), row, 0);
            endDateBox = new Calendar();
            AddWidget(endDateBox, row++, 1);

            AddWidget(new Label("Description:"), row, 0, verticalAlignment: VerticalAlignment.Top);
            descriptionTextBox = new TextBox { IsMultiline = true };
            AddWidget(descriptionTextBox, row++, 1);

            AddWidget(new Label("Type:"), row, 0);
            typeDropDown = new EnumDropDown<MaintenanceWindowType>();
            AddWidget(typeDropDown, row++, 1);

            AddWidget(new Label("Impact:"), row, 0);
            impactDropDown = new EnumDropDown<MaintenanceWindowImpact>();
            AddWidget(impactDropDown, row++, 1);

            AddWidget(new WhiteSpace(), row++, 0);
            AddWidget(new WhiteSpace(), row++, 0);

            // Create and add buttons for saving and closing the dialog
            var saveButton = new Button("Save") { Style = ButtonStyle.CallToAction, Width = 100 };
            saveButton.Pressed += (sender, args) => SaveMaintenance?.Invoke(this, EventArgs.Empty);
            AddWidget(saveButton, row, 0, HorizontalAlignment.Right);

            var cancelButton = new Button("Cancel") { Width = 100 };
            cancelButton.Pressed += (sender, args) => Cancel?.Invoke(this, EventArgs.Empty);
            AddWidget(cancelButton, row, 1, HorizontalAlignment.Right);
        }

        public void Load(Device device, MaintenanceWindow window)
        {
            deviceNameLabel.Text = $"Device: {device?.Name}";
            deviceDescriptionLabel.Text = $"> {device?.Description}";

            if (window is null)
            {
                Title = "Add Maintenance Window";
                startDateBox.DateTime = DateTime.Now.AddDays(1);
                endDateBox.DateTime = startDateBox.DateTime.AddHours(2);
                descriptionTextBox.Text = string.Empty;
                typeDropDown.Selected = MaintenanceWindowType.Other;
                impactDropDown.Selected = MaintenanceWindowImpact.Normal;
                return;
            }

            Title = "Edit Maintenance Window";
            startDateBox.DateTime = window.Start;
            endDateBox.DateTime = window.End;
            descriptionTextBox.Text = window.Description;
            typeDropDown.Selected = window.Type;
            impactDropDown.Selected = window.Impact;
        }

        public void Store(MaintenanceWindow window)
        {
            window.Start = startDateBox.DateTime;
            window.End = endDateBox.DateTime;
            window.Description = descriptionTextBox.Text;
            window.Type = typeDropDown.Selected;
            window.Impact = impactDropDown.Selected;
        }
    }
}
```

### Implement the actions

In `ManageMaintenanceController.cs`:

1. Hook up the dialog to edit a maintenance window.
   - Look for the `EditMaintenanceWindow` method.
   - Initialize the dialog that was created in the previous chapter.
   - Load the device and maintenance window data.
   - When the save event is triggered:
     - store the user input,
     - update the maintenance window in the repository,
     - and reload and switch to the main dialog. You can use `ShowMaintenanceOverview` for that.
   - When the cancel event is triggered, immediately switch to the main dialog.
   - Switch the controller to the maintenance dialog.
   - For example:

      ```csharp
      // ...
      private void EditMaintenanceWindow(Device device, MaintenanceWindow maintenanceWindow)
      {
          var maintenanceDialog = new MaintenanceDialog(engine);
          maintenanceDialog.Load(device, maintenanceWindow);
          maintenanceDialog.SaveMaintenance += (sender, args) =>
          {
              maintenanceDialog.Store(maintenanceWindow);
              repository.UpdateMaintenance(maintenanceWindow);
              ShowMaintenanceOverview();
          };

          maintenanceDialog.Cancel += (sender, args) => ShowMaintenanceOverview();

          controller.ShowDialog(maintenanceDialog);
      }
      // ...
      ```

1. Hook up the dialog to add a maintenance window.
   - Look for the `AddMaintenanceWindow` method.
   - Initialize the dialog that was created in the previous chapter.
   - Load the device and maintenance window data.
   - When the save event is triggered:
     - initialize the new maintenance window,
     - store the user input,
     - create the maintenance window in the repository,
     - and reload and switch to the main dialog. You can use `ShowMaintenanceOverview` for that.
   - When the cancel event is triggered, immediately switch to the main dialog.
   - Switch the controller to the maintenance dialog.
   - For example:

      ```csharp
      // ...
      private void AddMaintenanceWindow(Device device)
      {
          var maintenanceDialog = new MaintenanceDialog(engine);
          maintenanceDialog.Load(device, null);
          maintenanceDialog.SaveMaintenance += (sender, args) =>
          {
              var maintenanceWindow = new MaintenanceWindow
              {
                  Id = Guid.NewGuid(),
                  DeviceId = device.Id,
              };
              maintenanceDialog.Store(maintenanceWindow);
              repository.CreateMaintenance(maintenanceWindow);
              ShowMaintenanceOverview();
          };

          maintenanceDialog.Cancel += (sender, args) => ShowMaintenanceOverview();

          controller.ShowDialog(maintenanceDialog);
      }
      // ...
      ```

1. Implement deleting a maintenance window:
   - Ask the user for confirmation, before deleting the maintenance window.
     > Tip: The `YesNoDialog` dialog, provided by the toolkit can be used for that.
   - If the user confirms the action, delete the maintenance window from the repository.
   - Reload and, if needed, switch to the main dialog. Again, you can use `ShowMaintenanceOverview` for that.
   - For example:

      ```csharp
      // ...
      private void DeleteMaintenanceWindow(Device device, MaintenanceWindowmaintenanceWindow)
      {
          var message = $"Please confirm deleting the maintenance window '{maintenanceWindow.Description}' for device '{device.Name}'.";
          var result = YesNoDialog.Show(engine, message, "Delete Maintenance Window", YesNoDialog.CallToAction.No);
          if (result)
          {
              repository.DeleteMaintenance(maintenanceWindow.Id);
          }

          ShowMaintenanceOverview();
      }
      // ...
      ```

### We're there

You can now publish the script the `Manage Maintenance (Learning)` script to your DataMiner again, using the *Publish* button available in the `Manage Maintenance (Learning).xml` file.

When you refresh the app you have duplicated as *Manage maintenance windows* you should now be able to add, edit and delete the maintenance windows.

## References

- Session GitHub repo: [Skyline.DataMiner.Learning.MakingMaintenanceObservable](https://github.com/SkylineCommunications/Skyline.DataMiner.Learning.MakingMaintenanceObservable)
- Toolkit NuGet: [Skyline.DataMiner.Utils.InteractiveAutomationScriptToolkit on the NuGet Gallery](https://www.nuget.org/packages/Skyline.DataMiner.Utils.InteractiveAutomationScriptToolkit)
  - [Getting started on DataMiner Docs](https://docs.dataminer.services/develop/devguide/Automation/Howto/Getting_Started_with_the_IAS_Toolkit.html)
  - [GitHub Repo](https://github.com/SkylineCommunications/Skyline.DataMiner.Utils.InteractiveAutomationScriptToolkit)
  - [MVP on DataMiner Community](https://community.dataminer.services/courses/dataminer-automation/lessons/model-view-presenter/)
- [Skyline DataMiner SDK](https://docs.dataminer.services/develop/CICD/Skyline%20DataMiner%20Software%20Development%20Kit/skyline_dataminer_sdk.html)
- [Automation script development guide on DataMiner Docs](https://docs.dataminer.services/develop/devguide/Automation/index.html)
  - [Launching and attaching interactive automation scripts](https://docs.dataminer.services/develop/devguide/Automation/Howto/Launching_and_attaching_interactive_Automation_scripts.html)
  - [Creating a Cypress test](https://docs.dataminer.services/develop/devguide/Automation/Howto/Creating_a_cypress_test_for_an_interactive_automation_script.html)
