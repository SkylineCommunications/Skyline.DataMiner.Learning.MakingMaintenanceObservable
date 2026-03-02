namespace DeviceMaintenanceApi.Models
{
	using System;

	public class MaintenanceWindow
	{
		public Guid Id { get; set; }

		public Guid DeviceId { get; set; }

		public string Description { get; set; }

		public DateTime Start { get; set; }

		public DateTime End { get; set; }

		public MaintenanceWindowType Type { get; set; }

		public MaintenanceWindowImpact Impact { get; set; }

		public override string ToString() => $"Maintenance window: {Description} (ID: {Id}; Device ID: {DeviceId}; Start: {Start:O}; End: {End:O})";
	}
}