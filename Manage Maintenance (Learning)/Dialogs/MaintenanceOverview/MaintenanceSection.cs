namespace Skyline.DataMiner.Learning.MakingMaintenanceObservable.ManageMaintenance.Dialogs.MaintenanceOverview
{
	using System;
	using System.Text;

	using DeviceMaintenanceApi.Models;

	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class MaintenanceSection : Section
	{
		public MaintenanceSection(MaintenanceWindow maintenanceWindow)
		{
			MaintenanceWindow = maintenanceWindow;

			AddWidget(new Label(MaintenanceWindow.Start.ToString("yyyy-MM-dd HH:mm")) { Width = 180 }, 0, 0);
			AddWidget(new Label(MaintenanceWindow.End.ToString("yyyy-MM-dd HH:mm")) { Width = 180 }, 0, 1);
			AddWidget(new Label(MaintenanceWindow.Impact.ToString()) { Width = 80 }, 0, 2);

			var description = new StringBuilder(MaintenanceWindow.Description);
			description.Replace("\r", string.Empty);
			description.Replace('\n', ' ');
			AddWidget(new Label(description.ToString()) { Width = 250 }, 0, 3);

			var editButton = new Button("Edit") { Width = 60 };
			editButton.Pressed += (sender, args) => Edit?.Invoke(this, MaintenanceWindow);
			AddWidget(editButton, 0, 4);

			var deleteButton = new Button("Delete") { Width = 60 };
			deleteButton.Pressed += (sender, args) => Delete?.Invoke(this, MaintenanceWindow);
			AddWidget(deleteButton, 0, 5);
		}

		public event EventHandler<MaintenanceWindow> Edit;

		public event EventHandler<MaintenanceWindow> Delete;

		public MaintenanceWindow MaintenanceWindow { get; }
	}
}