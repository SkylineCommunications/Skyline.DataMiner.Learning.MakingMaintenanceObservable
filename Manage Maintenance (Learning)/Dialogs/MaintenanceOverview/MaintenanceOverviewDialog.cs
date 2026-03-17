namespace Skyline.DataMiner.Learning.MakingMaintenanceObservable.ManageMaintenance.Dialogs.MaintenanceOverview
{
	using System;
	using System.Linq;

	using DeviceMaintenanceApi.Data;
	using DeviceMaintenanceApi.Models;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	/// <summary>
	/// Represents the main dialog for managing device maintenance windows.
	/// Displays device selection and maintenance overview sections.
	/// </summary>
	public class MaintenanceOverviewDialog : Dialog
	{
		private readonly SelectDeviceSection selectDeviceSection = new SelectDeviceSection();
		private readonly MaintenanceOverviewSection maintenanceOverviewSection = new MaintenanceOverviewSection();
		private readonly Label selectDeviceFirstLabel = new Label("Select a device first") { Width = 810 };

		/// <summary>
		/// Initializes a new instance of the <see cref="MaintenanceOverviewDialog"/> class.
		/// </summary>
		/// <param name="engine">The DataMiner engine.</param>
		public MaintenanceOverviewDialog(IEngine engine) : base(engine)
		{
			Title = "Maintenance Overview";
			maintenanceOverviewSection.AddMaintenance += (sender, args) => AddMaintenance?.Invoke(this, selectDeviceSection.SelectedDevice);
			maintenanceOverviewSection.EditMaintenance += (sender, args) => EditMaintenance?.Invoke(this, (selectDeviceSection.SelectedDevice, args));
			maintenanceOverviewSection.DeleteMaintenance += (sender, args) => DeleteMaintenance?.Invoke(this, (selectDeviceSection.SelectedDevice, args));
		}

		/// <summary>
		/// Occurs when the user wants to add a new maintenance window for a device.
		/// </summary>
		public event EventHandler<Device> AddMaintenance;

		/// <summary>
		/// Occurs when the user wants to edit an existing maintenance window.
		/// </summary>
		public event EventHandler<(Device device, MaintenanceWindow maintenanceWindow)> EditMaintenance;

		/// <summary>
		/// Occurs when the user wants to delete a maintenance window.
		/// </summary>
		public event EventHandler<(Device device, MaintenanceWindow maintenanceWindow)> DeleteMaintenance;

		/// <summary>
		/// Loads the dialog with data from the specified repository.
		/// </summary>
		/// <param name="repository">The repository containing device and maintenance window data.</param>
		public void Load(IRepository repository)
		{
			// Load devices into the dropdown and subscribe to the DeviceSelected event to know when the user selects a device
			selectDeviceSection.Load(repository.GetDevices().ToList());
			selectDeviceSection.DeviceSelected += () => LoadDevice(repository);

			// Subscribe to the events of the MaintenanceOverviewSection to know when the user wants to add, edit or delete a maintenance window
			LoadDevice(repository);

			// Add the sections to the dialog
			BuildDialog();
		}

		private void BuildDialog()
		{
			Clear();
			var row = 0;
			AddSection(selectDeviceSection, row, 0);
			row += selectDeviceSection.RowCount;
			AddWidget(new WhiteSpace(), row++, 0);

			if (selectDeviceSection.SelectedDevice is null)
			{
				AddWidget(selectDeviceFirstLabel, row++, 0, 1, 6);
				AddWidget(new Button("Refresh") { Width = 120 }, row, 0);
				return;
			}

			AddSection(maintenanceOverviewSection, row, 0);
		}

		private void LoadDevice(IRepository repository)
		{
			if (selectDeviceSection.SelectedDevice is null)
			{
				BuildDialog();
				return;
			}

			var windows = repository.GetMaintenancesByDevice(selectDeviceSection.SelectedDevice.Id).ToList();

			maintenanceOverviewSection.Load(windows);
			BuildDialog();
		}
	}
}
