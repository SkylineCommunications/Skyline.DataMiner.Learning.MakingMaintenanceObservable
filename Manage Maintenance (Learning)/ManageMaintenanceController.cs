namespace Skyline.DataMiner.Learning.MakingMaintenanceObservable.ManageMaintenance
{
	using System;

	using DeviceMaintenanceApi.Data;
	using DeviceMaintenanceApi.Models;

	using Dialogs.MaintenanceOverview;
	using Dialogs.Maintenance;

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

		public void ShowMaintenanceOverview()
		{
			maintenanceOverviewDialog.Load(repository);
			controller.ShowDialog(maintenanceOverviewDialog);
		}

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

		private void DeleteMaintenanceWindow(Device device, MaintenanceWindow maintenanceWindow)
		{
			var message = $"Please confirm deleting the maintenance window '{maintenanceWindow.Description}' for device '{device.Name}'.";
			var result = YesNoDialog.Show(engine, message, "Delete Maintenance Window", YesNoDialog.CallToAction.No);
			if (result)
			{
				repository.DeleteMaintenance(maintenanceWindow.Id);
			}

			ShowMaintenanceOverview();
		}
	}
}