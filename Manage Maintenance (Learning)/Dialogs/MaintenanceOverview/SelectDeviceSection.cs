namespace Skyline.DataMiner.Learning.MakingMaintenanceObservable.ManageMaintenance.Dialogs.MaintenanceOverview
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DeviceMaintenanceApi.Models;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class SelectDeviceSection : Section
	{
		private DropDown<Device> DeviceDropDown;
		private Label DeviceDescriptionLabel;

		public event Action DeviceSelected;

		public Device SelectedDevice => DeviceDropDown.Selected;

		public SelectDeviceSection()
		{
			// Create a label widget
			var selectDeviceLabel = new Label("Device:") { Width = 180 };
			AddWidget(selectDeviceLabel, 0, 0);

			// Create a dropdown widget for device selection
			DeviceDropDown = new DropDown<Device>() { Width = 360 };
			DeviceDropDown.Changed += OnDeviceDropDownChanged;
			AddWidget(DeviceDropDown, 0, 1, 1, 3);

			// Create a label widget
			var selectDeviceInfoLabel = new Label("Device info:") { Width = 180 };
			AddWidget(selectDeviceInfoLabel, 1, 0);

			DeviceDescriptionLabel = new Label() { Width = 360 };
			AddWidget(DeviceDescriptionLabel, 1, 1, 1, 3);
		}

		public void Load(List<Device> devices)
		{
			var listOptions = new List<Option<Device>> { new Option<Device>("- Select -", null) };
			listOptions.AddRange(devices.Select(device => new Option<Device>(device.Name, device)));
			DeviceDropDown.Options = listOptions;
		}

		private void OnDeviceDropDownChanged(object sender, DropDown<Device>.DropDownChangedEventArgs e)
		{
			DeviceDescriptionLabel.Text = SelectedDevice?.Description ?? string.Empty;
			DeviceSelected?.Invoke();
		}
	}
}