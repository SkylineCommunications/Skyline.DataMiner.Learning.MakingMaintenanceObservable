namespace Skyline.DataMiner.Learning.MakingMaintenanceObservable.ManageMaintenance.Dialogs.MaintenanceOverview
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DeviceMaintenanceApi.Models;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	/// <summary>
	/// Represents a section for selecting a device from a dropdown list.
	/// </summary>
	public class SelectDeviceSection : Section
	{
		private readonly DropDown<Device> deviceDropDown;
		private readonly Label deviceDescriptionLabel;

		/// <summary>
		/// Initializes a new instance of the <see cref="SelectDeviceSection"/> class.
		/// </summary>
		public SelectDeviceSection()
		{
			// Create a label widget
			var selectDeviceLabel = new Label("Device:") { Width = 180 };
			AddWidget(selectDeviceLabel, 0, 0);

			// Create a dropdown widget for device selection
			deviceDropDown = new DropDown<Device>() { Width = 360 };
			deviceDropDown.Changed += OnDeviceDropDownChanged;
			AddWidget(deviceDropDown, 0, 1, 1, 3);

			// Create a label widget
			var selectDeviceInfoLabel = new Label("Device info:") { Width = 180 };
			AddWidget(selectDeviceInfoLabel, 1, 0);

			deviceDescriptionLabel = new Label() { Width = 360 };
			AddWidget(deviceDescriptionLabel, 1, 1, 1, 3);
		}

		/// <summary>
		/// Occurs when a device is selected from the dropdown.
		/// </summary>
		public event Action DeviceSelected;

		/// <summary>
		/// Gets the currently selected device.
		/// </summary>
		public Device SelectedDevice => deviceDropDown.Selected;

		/// <summary>
		/// Loads the device dropdown with the specified list of devices.
		/// </summary>
		/// <param name="devices">The list of devices to display in the dropdown.</param>
		public void Load(List<Device> devices)
		{
			var listOptions = new List<Option<Device>> { new Option<Device>("- Select -", null) };
			listOptions.AddRange(devices
				.OrderBy(device => device.Name ?? string.Empty, StringComparer.InvariantCultureIgnoreCase)
				.Select(device => new Option<Device>(device.Name, device)));
			deviceDropDown.Options = listOptions;
		}

		private void OnDeviceDropDownChanged(object sender, DropDown<Device>.DropDownChangedEventArgs e)
		{
			deviceDescriptionLabel.Text = SelectedDevice?.Description ?? string.Empty;
			DeviceSelected?.Invoke();
		}
	}
}