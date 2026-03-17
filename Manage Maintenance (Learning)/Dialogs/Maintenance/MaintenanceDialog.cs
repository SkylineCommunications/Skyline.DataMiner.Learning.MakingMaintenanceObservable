namespace Skyline.DataMiner.Learning.MakingMaintenanceObservable.ManageMaintenance.Dialogs.Maintenance
{
	using System;
	using DeviceMaintenanceApi.Models;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	/// <summary>
	/// Represents a dialog for creating or editing maintenance windows.
	/// </summary>
	public class MaintenanceDialog : Dialog
	{
		private readonly Label deviceNameLabel;
		private readonly Label deviceDescriptionLabel;
		private readonly Calendar startDateBox;
		private readonly Calendar endDateBox;
		private readonly TextBox descriptionTextBox;
		private readonly EnumDropDown<MaintenanceWindowType> typeDropDown;
		private readonly EnumDropDown<MaintenanceWindowImpact> impactDropDown;

		/// <summary>
		/// Initializes a new instance of the <see cref="MaintenanceDialog"/> class.
		/// </summary>
		/// <param name="engine">The DataMiner engine.</param>
		public MaintenanceDialog(IEngine engine) : base(engine)
		{
			var row = 0;

			// Create and add widgets for device information.
			deviceNameLabel = new Label { Style = TextStyle.Title };
			AddWidget(deviceNameLabel, row++, 0, 1, 2);

			deviceDescriptionLabel = new Label();
			AddWidget(deviceDescriptionLabel, row++, 0, 1, 2);

			AddWidget(new WhiteSpace(), row++, 0);

			// Create and add widgets for maintenance window details.
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

			// Create and add buttons for saving and closing the dialog.
			var saveButton = new Button("Save") { Style = ButtonStyle.CallToAction, Width = 100 };
			saveButton.Pressed += (sender, args) => SaveMaintenance?.Invoke(this, EventArgs.Empty);
			AddWidget(saveButton, row, 0, HorizontalAlignment.Right);

			var cancelButton = new Button("Cancel") { Width = 100 };
			cancelButton.Pressed += (sender, args) => Cancel?.Invoke(this, EventArgs.Empty);
			AddWidget(cancelButton, row, 1, HorizontalAlignment.Right);
		}

		/// <summary>
		/// Occurs when the user clicks the Save button to save the maintenance window.
		/// </summary>
		public event EventHandler SaveMaintenance;

		/// <summary>
		/// Occurs when the user clicks the Cancel button to close the dialog without saving.
		/// </summary>
		public event EventHandler Cancel;

		/// <summary>
		/// Loads the dialog with device and maintenance window data.
		/// </summary>
		/// <param name="device">The device associated with the maintenance window.</param>
		/// <param name="window">The maintenance window to edit, or null to create a new one.</param>
		public void Load(Device device, MaintenanceWindow window)
		{
			deviceNameLabel.Text = $"Device: {device?.Name}";
			deviceDescriptionLabel.Text = $"> {device?.Description}";

			startDateBox.DateTime = window.Start;
			endDateBox.DateTime = window.End;
			descriptionTextBox.Text = window.Description;
			typeDropDown.Selected = window.Type;
			impactDropDown.Selected = window.Impact;
		}

		/// <summary>
		/// Stores the dialog values into the specified maintenance window.
		/// </summary>
		/// <param name="window">The maintenance window to update with the dialog values.</param>
		public void Store(MaintenanceWindow window)
		{
			window.Start = startDateBox.DateTime.ToUniversalTime();
			window.End = endDateBox.DateTime.ToUniversalTime();
			window.Description = descriptionTextBox.Text;
			window.Type = typeDropDown.Selected;
			window.Impact = impactDropDown.Selected;
		}
	}
}
