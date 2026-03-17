namespace DeviceMaintenanceApi.Models
{
	using System;

	/// <summary>
	/// Represents a scheduled maintenance window for a device.
	/// </summary>
	public class MaintenanceWindow
	{
		/// <summary>
		/// Gets or sets the unique identifier of the maintenance window.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the unique identifier of the device associated with this maintenance window.
		/// </summary>
		public Guid DeviceId { get; set; }

		/// <summary>
		/// Gets or sets the description of the maintenance window.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the start date and time of the maintenance window.
		/// </summary>
		public DateTime Start { get; set; }

		/// <summary>
		/// Gets or sets the end date and time of the maintenance window.
		/// </summary>
		public DateTime End { get; set; }

		/// <summary>
		/// Gets or sets the type of maintenance being performed.
		/// </summary>
		public MaintenanceWindowType Type { get; set; }

		/// <summary>
		/// Gets or sets the impact level of the maintenance window.
		/// </summary>
		public MaintenanceWindowImpact Impact { get; set; }

		/// <summary>
		/// Returns a string representation of the maintenance window.
		/// </summary>
		/// <returns>A string containing the maintenance window details.</returns>
		public override string ToString() => $"Maintenance window: {Description} (ID: {Id}; Device ID: {DeviceId}; Start: {Start:O}; End: {End:O})";
	}
}