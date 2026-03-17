namespace DeviceMaintenanceApi.Models
{
	using System;

	/// <summary>
	/// Represents a device that can have maintenance windows.
	/// </summary>
	public class Device
	{
		/// <summary>
		/// Gets or sets the unique identifier of the device.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the device.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the description of the device.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Returns a string representation of the device.
		/// </summary>
		/// <returns>A string containing the device name and identifier.</returns>
		public override string ToString() => $"Device: {Name} ({Id})";
	}
}