namespace DeviceMaintenanceApi.Data
{
	using Models;
	using System.Collections.Generic;
	using System;

	public static class DemoSeedData
	{
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

            // todo add more devices and maintenance windows for testing
            //new Device
            //{
            //    Id = 4,
            //    Name = "Modulator MUX-300",
            //    Description = "Main DVB-T modulator.",
            //    MaintenanceWindows = new List<MaintenanceWindow>
            //    {
            //        new MaintenanceWindow { Id=401, DeviceId=4, Start=new DateTime(2022, 12, 21, 03,00,00), End=new DateTime(2022,12,21,03,45,00), Type="firmware update", Impact="normal" },
            //        new MaintenanceWindow { Id=402, DeviceId=4, Start=new DateTime(2025, 06, 29, 05,00,00), End=new DateTime(2025,06,29,05,40,00), Type="hardware", Impact="high" },
            //        new MaintenanceWindow { Id=403, DeviceId=4, Start=new DateTime(2026, 03, 10, 04,00,00), End=new DateTime(2026,03,10,04,20,00), Type="testing", Impact="low" }
            //    }
            //},

            //new Device
            //{
            //    Id = 5,
            //    Name = "Multiplexer MPX-500",
            //    Description = "Transport stream multiplexer.",
            //    MaintenanceWindows = new List<MaintenanceWindow>
            //    {
            //        new MaintenanceWindow { Id=501, DeviceId=5, Start=new DateTime(2024, 03, 03, 02,00,00), End=new DateTime(2024,03,03,02,45,00), Type="other", Impact="normal" },
            //        new MaintenanceWindow { Id=502, DeviceId=5, Start=new DateTime(2025, 09, 14, 01,00,00), End=new DateTime(2025,09,14,01,30,00), Type="hardware", Impact="high" }
            //    }
            //},

            //new Device
            //{
            //    Id = 6,
            //    Name = "Contribution Link Encoder C-100",
            //    Description = "Encoder used for remote live contribution feeds.",
            //    MaintenanceWindows = new List<MaintenanceWindow>
            //    {
            //        new MaintenanceWindow { Id=601, DeviceId=6, Start=new DateTime(2023, 05, 27, 04,00,00), End=new DateTime(2023,05,27,04,30,00), Type="testing", Impact="low" },
            //        new MaintenanceWindow { Id=602, DeviceId=6, Start=new DateTime(2026, 02, 25, 02,00,00), End=new DateTime(2026,02,25,02,20,00), Type="firmware update", Impact="normal" }
            //    }
            //},

            //new Device
            //{
            //    Id = 7,
            //    Name = "SPTS Processor SP-44",
            //    Description = "Single Program Transport Stream processor.",
            //    MaintenanceWindows = new List<MaintenanceWindow>
            //    {
            //        new MaintenanceWindow { Id=701, DeviceId=7, Start=new DateTime(2024, 08, 20, 01,00,00), End=new DateTime(2024,08,20,01,20,00), Type="other", Impact="low" },
            //        new MaintenanceWindow { Id=702, DeviceId=7, Start=new DateTime(2025, 12, 10, 05,00,00), End=new DateTime(2025,12,10,05,40,00), Type="hardware", Impact="high" },
            //        new MaintenanceWindow { Id=703, DeviceId=7, Start=new DateTime(2026, 03, 05, 03,00,00), End=new DateTime(2026,03,05,03,15,00), Type="testing", Impact="normal" }
            //    }
            //}
            };
		}

		public static List<MaintenanceWindow> GetMaintenanceWindows()
		{
			return new List<MaintenanceWindow>
			{
				new MaintenanceWindow { Id=new Guid("62bfbf1b-46fd-40cc-a4b4-a7f7c283d55d"), DeviceId=new Guid("e1e27b6b-3f76-4074-a846-cf745639b57d"), Description="FW update Jan14@02 /ref A17", Start=new DateTime(2024,01,14,02,00,00), End=new DateTime(2024,01,14,02,30,00), Type = MaintenanceWindowType.FirmwareUpdate, Impact = MaintenanceWindowImpact.Normal },
				new MaintenanceWindow { Id=new Guid("52754a27-f466-43c5-b4fd-e6de521bdd18"), DeviceId=new Guid("e1e27b6b-3f76-4074-a846-cf745639b57d"), Description="Testing Oct05@03 /ticket T-42", Start=new DateTime(2023,10,05,03,00,00), End=new DateTime(2023,10,05,03,20,00), Type = MaintenanceWindowType.Testing, Impact = MaintenanceWindowImpact.Low },
				new MaintenanceWindow { Id=new Guid("e9e98c4d-2176-4b63-b3a4-68e9cac7a78c"), DeviceId=new Guid("e1e27b6b-3f76-4074-a846-cf745639b57d"), Description="Hardware Jul22@01 /rack R2", Start=new DateTime(2022,07,22,01,00,00), End=new DateTime(2022,07,22,01,40,00), Type = MaintenanceWindowType.Hardware, Impact=MaintenanceWindowImpact.High },
				new MaintenanceWindow { Id=new Guid("c6c5d3f0-785b-4db5-979d-f760bbee74a8"), DeviceId=new Guid("e1e27b6b-3f76-4074-a846-cf745639b57d"), Description="General Mar01@04 /note N7", Start=new DateTime(2026,03,01,04,00,00), End=new DateTime(2026,03,01,04,30,00), Type = MaintenanceWindowType.Other, Impact=MaintenanceWindowImpact.Normal },
				new MaintenanceWindow { Id=new Guid("09d01dac-9b1f-42e6-83d3-b63cfba3580b"), DeviceId=new Guid("35f57eca-4c16-4dac-88f6-7721bd8f781b"), Description="Testing Nov11@05 /lab L3", Start=new DateTime(2025,11,11,05,00,00), End=new DateTime(2025,11,11,05,20,00), Type = MaintenanceWindowType.Testing, Impact=MaintenanceWindowImpact.Low },
				new MaintenanceWindow { Id=new Guid("0c7c52f8-292b-4e5c-8bea-e947ecbf3834"), DeviceId=new Guid("35f57eca-4c16-4dac-88f6-7721bd8f781b"), Description="Hardware Feb02@02 /chg C104", Start=new DateTime(2023,02,02,02,00,00), End=new DateTime(2023,02,02,02,45,00), Type = MaintenanceWindowType.Hardware, Impact=MaintenanceWindowImpact.High },
				new MaintenanceWindow { Id=new Guid("dbca7776-6501-4626-a7a4-25ea10841b69"), DeviceId=new Guid("35f57eca-4c16-4dac-88f6-7721bd8f781b"), Description="FW update Feb28@03 /ref B09", Start=new DateTime(2026,02,28,03,00,00), End=new DateTime(2026,02,28,03,30,00), Type = MaintenanceWindowType.FirmwareUpdate, Impact=MaintenanceWindowImpact.Normal },
			};
		}
	}
}