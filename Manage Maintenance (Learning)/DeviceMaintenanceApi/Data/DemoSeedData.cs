namespace DeviceMaintenanceApi.Data
{
	using System;
	using System.Collections.Generic;
	using DeviceMaintenanceApi.Models;

	/// <summary>
	/// Provides seed data for demonstration and testing purposes.
	/// </summary>
	public static class DemoSeedData
	{
		/// <summary>
		/// Gets a collection of demo devices.
		/// </summary>
		/// <returns>A list of pre-configured demo devices.</returns>
		public static List<Device> GetDevices()
		{
			return new List<Device>
			{
				new Device
				{
					Id = new Guid("e1e27b6b-3f76-4074-a846-cf745639b57d"),
					Name = "Encoder A1",
					Description = "Primary H.264 encoder for Channel 1.",
				},

				new Device
				{
					Id = new Guid("35f57eca-4c16-4dac-88f6-7721bd8f781b"),
					Name = "Encoder B2",
					Description = "Backup encoder for Channel 1.",
				},

				new Device
				{
					Id = new Guid("102840bd-0787-48cf-85d0-affbec2f19e5"),
					Name = "IRD RX-10",
					Description = "Integrated Receiver/Decoder for satellite feed.",
				},

				new Device
				{
					Id = new Guid("0cc7c4b2-0cd6-4872-9cda-4e911d817916"),
					Name = "Modulator MUX-300",
					Description = "Main DVB-T modulator.",
				},

				new Device
				{
					Id = new Guid("511d8801-f143-48d4-9ac3-c569cafa7aad"),
					Name = "Multiplexer MPX-500",
					Description = "Transport stream multiplexer.",
				},

				new Device
				{
					Id = new Guid("823285ac-70e4-4dc4-a60f-1b4fabf1ffae"),
					Name = "Link Encoder Contribution C-100",
					Description = "Encoder used for remote live contribution feeds.",
				},

				new Device
				{
					Id = new Guid("253ea25e-f86a-4616-a4c2-72c5138eb5c0"),
					Name = "SPTS Processor SP-44",
					Description = "Single Program Transport Stream processor.",
				},
			};
		}

		/// <summary>
		/// Gets a collection of demo maintenance windows.
		/// </summary>
		/// <returns>A list of pre-configured demo maintenance windows.</returns>
		public static List<MaintenanceWindow> GetMaintenanceWindows()
		{
			return new List<MaintenanceWindow>
			{
				new MaintenanceWindow { Id = new Guid("62bfbf1b-46fd-40cc-a4b4-a7f7c283d55d"), DeviceId = new Guid("e1e27b6b-3f76-4074-a846-cf745639b57d"), Description = "FW update Jan14@02 /ref A17", Start = new DateTime(2024,01,14,02,00,00, DateTimeKind.Utc), End = new DateTime(2024,01,14,02,30,00, DateTimeKind.Utc), Type = MaintenanceWindowType.FirmwareUpdate, Impact = MaintenanceWindowImpact.Normal },
				new MaintenanceWindow { Id = new Guid("52754a27-f466-43c5-b4fd-e6de521bdd18"), DeviceId = new Guid("e1e27b6b-3f76-4074-a846-cf745639b57d"), Description = "Testing Oct05@03 /ticket T-42", Start = new DateTime(2023,10,05,03,00,00), End = new DateTime(2023,10,05,03,20,00, DateTimeKind.Utc), Type = MaintenanceWindowType.Testing, Impact = MaintenanceWindowImpact.Low },
				new MaintenanceWindow { Id = new Guid("e9e98c4d-2176-4b63-b3a4-68e9cac7a78c"), DeviceId = new Guid("e1e27b6b-3f76-4074-a846-cf745639b57d"), Description = "Hardware Jul22@01 /rack R2", Start = new DateTime(2022,07,22,01,00,00, DateTimeKind.Utc), End = new DateTime(2022,07,22,01,40,00, DateTimeKind.Utc), Type = MaintenanceWindowType.Hardware, Impact = MaintenanceWindowImpact.High },
				new MaintenanceWindow { Id = new Guid("c6c5d3f0-785b-4db5-979d-f760bbee74a8"), DeviceId = new Guid("e1e27b6b-3f76-4074-a846-cf745639b57d"), Description = "General Mar01@04 /note N7", Start = new DateTime(2026,03,01,04,00,00, DateTimeKind.Utc), End = new DateTime(2026,03,01,04,30,00, DateTimeKind.Utc), Type = MaintenanceWindowType.Other, Impact = MaintenanceWindowImpact.Normal },
				new MaintenanceWindow { Id = new Guid("09d01dac-9b1f-42e6-83d3-b63cfba3580b"), DeviceId = new Guid("35f57eca-4c16-4dac-88f6-7721bd8f781b"), Description = "Testing Nov11@05 /lab L3", Start = new DateTime(2025,11,11,05,00,00, DateTimeKind.Utc), End = new DateTime(2025,11,11,05,20,00, DateTimeKind.Utc), Type = MaintenanceWindowType.Testing, Impact = MaintenanceWindowImpact.Low },
				new MaintenanceWindow { Id = new Guid("0c7c52f8-292b-4e5c-8bea-e947ecbf3834"), DeviceId = new Guid("35f57eca-4c16-4dac-88f6-7721bd8f781b"), Description = "Hardware Feb02@02 /chg C104", Start = new DateTime(2023,02,02,02,00,00, DateTimeKind.Utc), End = new DateTime(2023,02,02,02,45,00, DateTimeKind.Utc), Type = MaintenanceWindowType.Hardware, Impact = MaintenanceWindowImpact.High },
				new MaintenanceWindow { Id = new Guid("dbca7776-6501-4626-a7a4-25ea10841b69"), DeviceId = new Guid("35f57eca-4c16-4dac-88f6-7721bd8f781b"), Description = "FW update Feb28@03 /ref B09", Start = new DateTime(2026,02,28,03,00,00, DateTimeKind.Utc), End = new DateTime(2026,02,28,03,30,00, DateTimeKind.Utc), Type = MaintenanceWindowType.FirmwareUpdate, Impact = MaintenanceWindowImpact.Normal },
			};
		}
	}
}