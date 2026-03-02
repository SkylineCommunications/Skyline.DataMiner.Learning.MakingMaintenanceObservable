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
