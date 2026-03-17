namespace Skyline.DataMiner.Learning.MakingMaintenanceObservable.ManageMaintenance.Dialogs.Maintenance
{
	using System;
	using DeviceMaintenanceApi.Models;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	/// <summary>
	/// Represents a dialog for creating or editing maintenance windows.
	/// </summary>
	public class MaintenanceDialog : Dialog
	{
		private readonly Label deviceNameLabel;
		private readonly Label deviceDescriptionLabel;
		private readonly Calendar startDateBox;
		private readonly Calendar endDateBox;
		private readonly TextBox descriptionTextBox;
		private readonly EnumDropDown<MaintenanceWindowType> typeDropDown;
		private readonly EnumDropDown<MaintenanceWindowImpact> impactDropDown;

		/// <summary>
		/// Initializes a new instance of the <see cref="MaintenanceDialog"/> class.
		/// </summary>
		/// <param name="engine">The DataMiner engine.</param>
		public MaintenanceDialog(IEngine engine) : base(engine)
		{
		}
	}
}
