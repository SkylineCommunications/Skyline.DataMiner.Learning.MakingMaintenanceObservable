namespace DeviceMaintenanceApi.Models
{
	using System;

	public class Device
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public override string ToString() => $"Device: {Name} ({Id})";
	}
}