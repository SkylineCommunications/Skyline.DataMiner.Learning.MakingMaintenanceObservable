namespace Skyline.DataMiner.Learning.MakingMaintenanceObservable.ManageMaintenance.Dialogs.MaintenanceOverview
{
	using DeviceMaintenanceApi.Models;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;
	using System.Collections.Generic;
	using System;

	public class MaintenanceOverviewSection : Section
	{
		public Section Header;

		public event Action AddMaintenance;

		public event Action<MaintenanceWindow> EditMaintenance;

		public event Action<MaintenanceWindow> DeleteMaintenance;

		public MaintenanceOverviewSection()
		{
			InitializeHeader();
		}

		private void InitializeHeader()
		{
			var addButton = new Button("Add");
			addButton.Width = 60;
			addButton.Pressed += (sender, args) => AddMaintenance?.Invoke();

			Header = new Section()
				.AddWidget(new Label("Start") { Width = 180, Style = TextStyle.Bold } , 0, 0)
				.AddWidget(new Label("End") { Width = 180, Style = TextStyle.Bold }, 0, 1)
				.AddWidget(new Label("Impact") { Width = 80, Style = TextStyle.Bold }, 0, 2)
				.AddWidget(new Label("Description") { Width = 250, Style = TextStyle.Bold }, 0, 3)
				.AddWidget(addButton, 0, 4);
		}

		public void Load(IReadOnlyCollection<MaintenanceWindow> maintenanceWindows)
		{
			Clear();
			var row = 0;
			AddSection(Header, row++, 0);

			if (maintenanceWindows.Count == 0)
			{
				AddWidget(new Label("No maintenance windows yet, add a new one"), row, 0, 1, 4);
				return;
			}

			foreach (var maintenanceWindow in maintenanceWindows)
			{
				var maintenanceSection = new MaintenanceSection(maintenanceWindow);
				AddSection(maintenanceSection, row++, 0);
				maintenanceSection.Edit += () => EditMaintenance?.Invoke(maintenanceWindow);
				maintenanceSection.Delete += () => DeleteMaintenance?.Invoke(maintenanceWindow);
			}
		}
	}
}