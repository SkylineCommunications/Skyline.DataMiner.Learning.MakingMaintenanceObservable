namespace Skyline.DataMiner.Learning.MakingMaintenanceObservable.ManageMaintenance
{
	using System;
	using DeviceMaintenanceApi.Data;
	using DeviceMaintenanceApi.Models;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Learning.MakingMaintenanceObservable.ManageMaintenance.Dialogs.Maintenance;
	using Skyline.DataMiner.Learning.MakingMaintenanceObservable.ManageMaintenance.Dialogs.MaintenanceOverview;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	/// <summary>
	/// Controls the interactive flow for managing device maintenance windows.
	/// </summary>
	public class ManageMaintenanceController
	{
		private readonly InteractiveController controller;
		private readonly IEngine engine;
		private readonly MaintenanceOverviewDialog maintenanceOverviewDialog;
		private readonly IRepository repository;

		/// <summary>
		/// Initializes a new instance of the <see cref="ManageMaintenanceController"/> class.
		/// </summary>
		/// <param name="engine">The DataMiner engine.</param>
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

		/// <summary>
		/// Shows the maintenance overview dialog to the user.
		/// </summary>
		public void ShowMaintenanceOverview()
		{
			maintenanceOverviewDialog.Load(repository);
			controller.ShowDialog(maintenanceOverviewDialog);
		}

		private void AddMaintenanceWindow(Device device)
		{
			var maintenanceDialog = new MaintenanceDialog(engine) { Title = "Add Maintenance Window" };
			var maintenanceWindow = new MaintenanceWindow
			{
				Id = Guid.NewGuid(),
				DeviceId = device.Id,
				Start = DateTime.Now.AddDays(1),
				End = DateTime.Now.AddDays(1).AddHours(2),
				Description = string.Empty,
				Type = MaintenanceWindowType.Other,
				Impact = MaintenanceWindowImpact.Normal,
			};
			maintenanceDialog.Load(device, maintenanceWindow);
			maintenanceDialog.SaveMaintenance += (sender, args) =>
			{
				maintenanceDialog.Store(maintenanceWindow);
				repository.CreateMaintenance(maintenanceWindow);
				ShowMaintenanceOverview();
			};

			maintenanceDialog.Cancel += (sender, args) => ShowMaintenanceOverview();

			controller.ShowDialog(maintenanceDialog);
		}

		private void EditMaintenanceWindow(Device device, MaintenanceWindow maintenanceWindow)
		{
			var maintenanceDialog = new MaintenanceDialog(engine) { Title = "Edit Maintenance Window" };
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