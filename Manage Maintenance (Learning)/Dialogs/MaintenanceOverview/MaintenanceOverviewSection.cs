namespace Skyline.DataMiner.Learning.MakingMaintenanceObservable.ManageMaintenance.Dialogs.MaintenanceOverview
{
	using System;
	using System.Collections.Generic;

	using DeviceMaintenanceApi.Models;

	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class MaintenanceOverviewSection : Section
	{
		private Section header;

		public MaintenanceOverviewSection()
		{
			InitializeHeader();
		}

		public event EventHandler AddMaintenance;

		public event EventHandler<MaintenanceWindow> EditMaintenance;

		public event EventHandler<MaintenanceWindow> DeleteMaintenance;

		public void Load(IReadOnlyCollection<MaintenanceWindow> maintenanceWindows)
		{
			Clear();
			var row = 0;
			AddSection(header, row++, 0);

			if (maintenanceWindows.Count == 0)
			{
				AddWidget(new Label("No maintenance windows yet, add a new one"), row, 0, 1, 4);
				return;
			}

			foreach (var maintenanceWindow in maintenanceWindows)
			{
				var maintenanceSection = new MaintenanceSection(maintenanceWindow);
				AddSection(maintenanceSection, row++, 0);
				maintenanceSection.Edit += (sender, args) => EditMaintenance?.Invoke(this, args);
				maintenanceSection.Delete += (sender, args) => DeleteMaintenance?.Invoke(this, args);
			}
		}

		private void InitializeHeader()
		{
			var addButton = new Button("Add") { Width = 60 };
			addButton.Pressed += (sender, args) => AddMaintenance?.Invoke(this, EventArgs.Empty);

			header = new Section()
				.AddWidget(new Label("Start") { Width = 180, Style = TextStyle.Bold }, 0, 0)
				.AddWidget(new Label("End") { Width = 180, Style = TextStyle.Bold }, 0, 1)
				.AddWidget(new Label("Impact") { Width = 80, Style = TextStyle.Bold }, 0, 2)
				.AddWidget(new Label("Description") { Width = 250, Style = TextStyle.Bold }, 0, 3)
				.AddWidget(addButton, 0, 4);
		}
	}
}