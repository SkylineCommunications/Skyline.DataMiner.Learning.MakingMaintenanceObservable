namespace Skyline.DataMiner.Learning.MakingMaintenanceObservable.ManageMaintenance
{
	using System;

	using DeviceMaintenanceApi.Data;
	using DeviceMaintenanceApi.Models;

	using Dialogs.MaintenanceOverview;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ManageMaintenanceController
	{
		private readonly InteractiveController controller;
		private readonly IEngine engine;
		private readonly MaintenanceOverviewDialog maintenanceOverviewDialog;
		private readonly IRepository repository;

		public ManageMaintenanceController(IEngine engine)
		{
			this.engine = engine ?? throw new ArgumentNullException(nameof(engine));

			// Controls the event loop and switch between dialogs
			controller = new InteractiveController(engine);

			// Create an instance of the repository that will be used to manage the data in the dialogs
			repository = new InMemoryRepository();

			// Create an instance of the main overview dialog
			maintenanceOverviewDialog = new MaintenanceOverviewDialog(engine);
			maintenanceOverviewDialog.AddMaintenance += (sender, args) => AddMaintenanceWindow(args);
			maintenanceOverviewDialog.EditMaintenance += (sender, args) => EditMaintenanceWindow(args.device, args.maintenanceWindow);
			maintenanceOverviewDialog.DeleteMaintenance += (sender, args) => DeleteMaintenanceWindow(args.device, args.maintenanceWindow);
		}

		public void LoadMaintenanceOverview()
		{
			maintenanceOverviewDialog.Load(repository);
		}

		public void ShowMaintenanceOverview()
		{
			LoadMaintenanceOverview();
			controller.ShowDialog(maintenanceOverviewDialog);
		}

		private void AddMaintenanceWindow(Device device)
		{
			engine.Log("Add Maintenance Window");

			// TODO: show dialog to create MaintenanceWindow
		}

		private void EditMaintenanceWindow(Device device, MaintenanceWindow maintenanceWindow)
		{
			engine.Log("Add Maintenance Window");

			// TODO: show dialog to create MaintenanceWindow
		}

		private void DeleteMaintenanceWindow(Device device, MaintenanceWindow maintenanceWindow)
		{
			engine.Log($"Delete {maintenanceWindow}");

			// TODO: show dialog to delete MaintenanceWindow
		}
	}
}