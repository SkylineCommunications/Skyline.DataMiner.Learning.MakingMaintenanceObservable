namespace Skyline.DataMiner.Learning.MakingMaintenanceObservable.ManageMaintenance.Dialogs.MaintenanceOverview
{
	using System.Linq;
	using DeviceMaintenanceApi.Data;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class MaintenanceOverviewDialog : Dialog
	{
		private readonly SelectDeviceSection SelectDeviceSection = new SelectDeviceSection();
		private readonly MaintenanceOverviewSection MaintenanceOverviewSection = new MaintenanceOverviewSection();
		private readonly Label SelectDeviceFirstLabel = new Label("Select a device first") { Width = 810 };

		public MaintenanceOverviewDialog(IEngine engine) : base(engine)
		{ }

		public void Load(IRepository repository)
		{
			// Load devices into the dropdown and subscribe to the DeviceSelected event to know when the user selects a device
			SelectDeviceSection.Load(repository.GetDevices().ToList());
			SelectDeviceSection.DeviceSelected += () => LoadDevice(repository);

			// Subscribe to the events of the MaintenanceOverviewSection to know when the user wants to add, edit or delete a maintenance window
			LoadDevice(repository);
			MaintenanceOverviewSection.AddMaintenance += () => Engine.GenerateInformation("Add");
			MaintenanceOverviewSection.EditMaintenance += x => Engine.GenerateInformation($"Edit {x}");
			MaintenanceOverviewSection.DeleteMaintenance += x => Engine.GenerateInformation($"Delete {x}");

			// Add the sections to the dialog
			BuildDialog();
		}

		private void BuildDialog()
		{
			Clear();
			var row = 0;
			AddSection(SelectDeviceSection, row, 0);
			row += SelectDeviceSection.RowCount;
			AddWidget(new WhiteSpace(), row++, 0);

			if (SelectDeviceSection.SelectedDevice is null)
			{
				AddWidget(SelectDeviceFirstLabel, row++, 0, 1, 6);
				AddWidget(new Button("Refresh") { Width = 120 }, row, 0);
				return;
			}

			AddSection(MaintenanceOverviewSection, row, 0);
		}

		private void LoadDevice(IRepository repository)
		{
			if (SelectDeviceSection.SelectedDevice is null)
			{
				BuildDialog();
				return;
			}

			var windows = repository.GetMaintenanceByDevice(SelectDeviceSection.SelectedDevice.Id).ToList();

			// todo
			Engine.GenerateInformation($"Load device {SelectDeviceSection.SelectedDevice?.Name} / {string.Join(",", windows.Select(x => x.Description))}");

			MaintenanceOverviewSection.Load(windows);
			BuildDialog();
		}
	}
}
