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
			engine.Log($"Add for {device}");

			// TODO: show dialog to create MaintenanceWindow
		}

		private void EditMaintenanceWindow(Device device, MaintenanceWindow maintenanceWindow)
		{
			engine.Log($"Edit {maintenanceWindow} for {device}");

			// TODO: show dialog to edit MaintenanceWindow
		}

		private void DeleteMaintenanceWindow(Device device, MaintenanceWindow maintenanceWindow)
		{
			engine.Log($"Delete {maintenanceWindow} for {device}");

			// TODO: show dialog to delete MaintenanceWindow
		}
	}
}